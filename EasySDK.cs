using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading.Tasks;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.Threading;

namespace CHCNetSDK
{
    /// <summary>
    /// 
    /// </summary>
    public class EasySDK
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string BasePath = $"{ Path.GetDirectoryName(typeof(EasySDK).Assembly.Location)}/";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string LibPath = $"{ BasePath}/runtimes/win-{(Environment.Is64BitProcess ? "x64" : "x86")}/native/";
        static EasySDK()
        {
            CHCNetSDK.CHCNet.NET_DVR_Init();
        }

        /// <summary>
        /// 视频通道信息
        /// </summary>
        public class VideoPort
        {
            /// <summary>
            /// 
            /// </summary>
            public int Channal = 0;

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public Image Frame = null;

            /// <summary>
            /// 
            /// </summary>
            public Process Process = null;
        }
        private List<VideoPort> Ports = new List<VideoPort>();

        private void LogForError(string str)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public EasySDK(string ip, int port, string username, string password)
        {
            Ports.Clear();
            CHCNet.NET_DVR_Init();
            var deviceInfo = new CHCNet.NET_DVR_DEVICEINFO_V30();
            //登录设备 Login the device
            var uid = CHCNet.NET_DVR_Login_V30(ip, port, username, password, ref deviceInfo);
            if (uid < 0)
            {
                LogForError($"登录失败，输出错误号,NET_DVR_Login_V30 failed, error code= {CHCNet.NET_DVR_GetLastError()}");
                return;
            }
            var m_struIpParaCfgV40 = new CHCNet.NET_DVR_IPPARACFG_V40();
            var ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)Marshal.SizeOf(m_struIpParaCfgV40));
            Marshal.StructureToPtr(m_struIpParaCfgV40, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            int iGroupNo = 0;  //该Demo仅获取第一组64个通道，如果设备IP通道大于64路，需要按组号0~i多次调用NET_DVR_GET_IPPARACFG_V40获取
            if (!CHCNet.NET_DVR_GetDVRConfig(uid, CHCNet.NET_DVR_GET_IPPARACFG_V40, iGroupNo, ptrIpParaCfgV40, (uint)Marshal.SizeOf(m_struIpParaCfgV40), ref dwReturn))
            {
                LogForError($"获取IP资源配置信息失败，输出错误号,NET_DVR_GET_IPPARACFG_V40 failed, error code= {CHCNet.NET_DVR_GetLastError()}");
                Console.WriteLine($"获取IP资源配置信息失败，输出错误号,NET_DVR_GET_IPPARACFG_V40 failed, error code= {CHCNet.NET_DVR_GetLastError()}");
                Marshal.FreeHGlobal(ptrIpParaCfgV40);
                return;
            }
            m_struIpParaCfgV40 = (CHCNet.NET_DVR_IPPARACFG_V40)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(CHCNet.NET_DVR_IPPARACFG_V40));
            Marshal.FreeHGlobal(ptrIpParaCfgV40);
            //"获取IP资源配置信息成功!".LogForInfomation();

            uint iDChanNum = (uint)deviceInfo.byIPChanNum + 256 * (uint)deviceInfo.byHighDChanNum;
            if (iDChanNum > 64) iDChanNum = 64; //如果设备IP通道大于64路，按只取64

            for (int i = 0; i < iDChanNum; i++)
            {
                var dwSize = (uint)Marshal.SizeOf(m_struIpParaCfgV40.struStreamMode[i].uGetStream);
                var videoport = new VideoPort() { Channal = (int)(m_struIpParaCfgV40.dwStartDChan + i) };
                var isenable = false;
                var databuff = IntPtr.Zero;
                databuff = Marshal.AllocHGlobal((Int32)dwSize);
                Marshal.StructureToPtr(m_struIpParaCfgV40.struStreamMode[i].uGetStream, databuff, false);
                switch (m_struIpParaCfgV40.struStreamMode[i].byGetStreamType)
                {
                    //目前NVR仅支持直接从设备取流 NVR supports only the mode: get stream from device directly
                    case 0:
                        {
                            var info = (CHCNet.NET_DVR_IPCHANINFO)Marshal.PtrToStructure(databuff, typeof(CHCNet.NET_DVR_IPCHANINFO));
                            //videoport.DevID = info.byIPID + info.byIPIDHigh * 256 - iGroupNo * 64 - 1;
                            isenable = info.byEnable == 1;
                            break;
                        }
                    case 4:
                        {
                            var info = (CHCNet.NET_DVR_PU_STREAM_URL)Marshal.PtrToStructure(databuff, typeof(CHCNet.NET_DVR_PU_STREAM_URL));
                            //videoport.DevID = info.wIPID - iGroupNo * 64 - 1;
                            isenable = info.byEnable == 1;
                            break;
                        }
                    case 6:
                        {
                            var info = (CHCNet.NET_DVR_IPCHANINFO_V40)Marshal.PtrToStructure(databuff, typeof(CHCNet.NET_DVR_IPCHANINFO_V40));
                            //videoport.DevID = info.wIPID - iGroupNo * 64 - 1;
                            isenable = info.byEnable == 1;
                            break;
                        }
                    default:
                        break;
                }
                Marshal.FreeHGlobal(databuff);
                if (!isenable) continue;
                Ports.Add(videoport);
                var filename = $"HCNVR_{Ports.Count}";//_{Process.GetCurrentProcess().Id}
                Task.Factory.StartNew(() =>
                {
                    //创建或者打开共享内存 32MB
                    var mmf = MemoryMappedFile.CreateNew(filename, 32 * 1024 * 1024, MemoryMappedFileAccess.ReadWrite);

                    //通过MemoryMappedFile的CreateViewAccssor方法获得共享内存的访问器
                    var viewAccessor = mmf.CreateViewAccessor(0, 32 * 1024 * 1024);
                    //循环写入，使在这个进程中可以向共享内存中写入不同的字符串值
                    viewAccessor.Write(0, 0);
                    viewAccessor.Write(1, 0);
                    videoport.Process = new Process();
                    videoport.Process.StartInfo = new ProcessStartInfo($"{LibPath}/CHCTCPSender.exe", $"{ip} {port.ToString()} {username} {password} {Ports.Count - 1} {filename}") { CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden };
                    videoport.Process.Start();
                    while (!videoport.Process.HasExited)
                    {
                        Thread.Sleep(10);
                        var index1 = viewAccessor.ReadByte(0);
                        var index2 = viewAccessor.ReadByte(1);
                        if (index1 == index2) continue;
                        var imglen = viewAccessor.ReadUInt32(2);
                        if (imglen == 0) continue;
                        var buff = new byte[imglen];
                        viewAccessor.ReadArray<byte>(6, buff, 0, buff.Length);
                        viewAccessor.Write(0, index2);
                        var mem = new MemoryStream(buff);
                        var img = (Bitmap)Bitmap.FromStream(mem);

                        lock (videoport)
                        {
                            if (videoport.Frame != null) videoport.Frame.Dispose();
                            videoport.Frame = DeepCopyBitmap(img);
                        }
                        img.Dispose();
                        mem.Dispose();
                    }
                    videoport.Process.Dispose();
                    viewAccessor.Dispose();
                    mmf.Dispose();
                });
                //*/
            }//EndFor
            CHCNet.NET_DVR_Logout(uid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public Image ReadImage(int port = 0)
        {
            if (port >= Ports.Count) return null;
            Image img = null;
            lock (Ports[port]) if (Ports[port].Frame != null) img = DeepCopyBitmap(Ports[port].Frame);
            return img;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="insteadtransparent">用来顶替透明区的颜色,默认为null则不顶替</param>
        /// <returns></returns>
        private static Image DeepCopyBitmap(Image bitmap, Brush insteadtransparent = null)
        {
            try
            {
                var img = new Bitmap(bitmap.Width, bitmap.Height);
                var g = Graphics.FromImage(img);
                if (insteadtransparent != null) g.FillRectangle(insteadtransparent, 0, 0, img.Width, img.Height);
                g.DrawImage(bitmap, 0, 0);
                g.Flush();
                g.Dispose();
                return img;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0}", ex.Message);
                return null;
            }
        }


    }//End Class
}