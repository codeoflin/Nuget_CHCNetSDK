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
            if (!Directory.Exists(LibPath)) LibPath = BasePath;
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
            public byte[] Frame = null;

            /// <summary>
            /// 
            /// </summary>
            public Process Process = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<VideoPort> Ports = new List<VideoPort>();

        private void LogForError(string str)
        {

        }

        /// <summary>
        /// 将Base64字符串转换为图片
        /// </summary>
        /// <param name="base64">base64字符串</param>
        /// <returns>图片</returns>
        private static Image Base64ToImg(string base64)
        {
            base64 = base64.Replace("data:image/png;base64,", "").Replace("data:image/jgp;base64,", "").Replace("data:image/jpg;base64,", "").Replace("data:image/jpeg;base64,", ""); //将base64头部信息替换
            var stream = new MemoryStream(Convert.FromBase64String(base64));
            var img = (Bitmap)Bitmap.FromStream(stream);
            if (img == null)
            {
                stream.Dispose();
                return null;
            }

            /* 去除透明通道方案1
			var data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			var raw = new byte[img.Width * img.Height * 4];
			//uint* bckInt = (uint*)data.Scan0;
			Marshal.Copy(data.Scan0, raw, 0, raw.Length);
			for (int i = 0; i < img.Width * img.Height; i++)
			{
				raw[i * 4] = 0xFF;
				raw[i * 4 + 1] = 0xFF;
				raw[i * 4 + 2] = 0xFF;
				raw[i * 4 + 3] = 0xFF;
			}
			Marshal.Copy(raw, 0, data.Scan0, raw.Length);
			img.UnlockBits(data);
			//*/

            //复制一个图,与旧的图片抛弃关联
            var newimg = DeepCopyBitmap(img);
            img.Dispose();
            stream.Dispose();
            return newimg;

            //创建文件夹
            //var folderPath = savePath.Substring(0, savePath.LastIndexOf('\\'));
            ////FileHelper.CreateDir(folderPath);
            //if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            //图片后缀格式
            //var suffix = savePath.Substring(savePath.LastIndexOf('.') + 1, savePath.Length - savePath.LastIndexOf('.') - 1).ToLower();
            /*
			var suffixName = suffix == "png"
					? ImageFormat.Png
					: suffix == "jpg" || suffix == "jpeg"
							? ImageFormat.Jpeg
							: suffix == "bmp"
									? ImageFormat.Bmp
									: suffix == "gif"
											? ImageFormat.Gif
											: ImageFormat.Jpeg;
			// */
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
                var run = true;
                var filename = $"HCNVR{(run ? "_" : "")}{(run ? Process.GetCurrentProcess().Id.ToString() : "")}_{Ports.Count}";
                int portid = Ports.Count - 1;
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
                    videoport.Process.StartInfo = new ProcessStartInfo($"{LibPath}/CHCTCPSender.exe", $"{ip} {port.ToString()} {username} {password} {portid} {filename}") { CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden };

                    if (run) videoport.Process.Start();
                    while ((!run) || !videoport.Process.HasExited)
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
                        lock (videoport) videoport.Frame = buff;
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
            var heximg = ReadImageBytes(port);
            if (heximg == null) return null;
            if (heximg.Length == 0) return null;
            using (var mem = new MemoryStream(heximg)) return Image.FromStream(mem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public string ReadImageBase64(int port = 0)
        {
            if (port >= Ports.Count) return null;
            var imgbuff = ReadImageBytes(port);
            if (imgbuff == null) return null;
            return Convert.ToBase64String(imgbuff);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public byte[] ReadImageBytes(int port = 0)
        {
            if (port >= Ports.Count) return null;
            byte[] buff = null;
            lock (Ports[port])
            {
                if (Ports[port].Frame != null)
                {
                    buff = new byte[Ports[port].Frame.Length];
                    Ports[port].Frame.CopyTo(buff, 0);
                }
            }
            return buff;
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