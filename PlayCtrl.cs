using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CHCNetSDK
{
    /// <summary>
    /// 
    /// </summary>
    public static class PlayCtrl
    {
        [DllImport("kernel32")]
        private static extern IntPtr LoadLibraryA([MarshalAs(UnmanagedType.LPStr)] string fileName);
        static PlayCtrl()
        {
            //File.WriteAllText(@"E:\2.TXT",$"123");
            var basepath = $"{ Path.GetDirectoryName(typeof(PlayCtrl).Assembly.Location)}/runtimes/win-{(Environment.Is64BitProcess ? "x64" : "x86")}/native/";//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
            if (!Directory.Exists(basepath)) basepath = Path.GetDirectoryName(typeof(PlayCtrl).Assembly.Location);
            //File.WriteAllText("E://1.TXT",$"{basepath}/HCCore.dll");
            //为了实现x64和x86共兼容,此处必须手动加载dll
            LoadLibraryA($"{basepath}/AnalyzeData.dll");
            LoadLibraryA($"{basepath}/AudioIntercom.dll");
            LoadLibraryA($"{basepath}/AudioRender.dll");
            LoadLibraryA($"{basepath}/EagleEyeRender.dll");
            LoadLibraryA($"{basepath}/HCAlarm.dll");
            LoadLibraryA($"{basepath}/HCCore.dll");
            LoadLibraryA($"{basepath}/HCCoreDevCfg.dll");
            LoadLibraryA($"{basepath}/HCDisplay.dll");
            LoadLibraryA($"{basepath}/HCGeneralCfgMgr.dll");
            LoadLibraryA($"{basepath}/HCIndustry.dll");
            LoadLibraryA($"{basepath}/HCNetSDK.dll");
            LoadLibraryA($"{basepath}/HCPlayBack.dll");
            LoadLibraryA($"{basepath}/HCPreview.dll");
            LoadLibraryA($"{basepath}/HCVoiceTalk.dll");
            LoadLibraryA($"{basepath}/hlog.dll");
            LoadLibraryA($"{basepath}/hpr.dll");
            LoadLibraryA($"{basepath}/HXVA.dll");
            LoadLibraryA($"{basepath}/libeay32.dll");
            LoadLibraryA($"{basepath}/libiconv2.dll");
            LoadLibraryA($"{basepath}/libmmd.dll");
            LoadLibraryA($"{basepath}/MP_Render.dll");
            LoadLibraryA($"{basepath}/NPQos.dll");
            LoadLibraryA($"{basepath}/OpenAL32.dll");
            LoadLibraryA($"{basepath}/PlayCtrl.dll");
            LoadLibraryA($"{basepath}/ssleay32.dll");
            LoadLibraryA($"{basepath}/StreamTransClient.dll");
            LoadLibraryA($"{basepath}/SuperRender.dll");
            LoadLibraryA($"{basepath}/SystemTransform.dll");
            LoadLibraryA($"{basepath}/YUVProcess.dll");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public const string PLAYM4_API = "extern \"C\"__declspec(dllexport)";

        // public const string PLAYM4_API = "extern \"C\" __declspec(dllimport)";


        //Max channel numbers
        public const int PLAYM4_MAX_SUPPORTS = 500;
        //Wave coef range;
        public const int MIN_WAVE_COEF = -100;
        /// <summary>
        /// 
        /// </summary>
        public const int MAX_WAVE_COEF = 100;

        //Timer type
        public const int TIMER_1 = 1; //Only 16 timers for every process.Default TIMER;
        public const int TIMER_2 = 2;//Not limit;But the precision less than TIMER_1; 

        //BUFFER TYPE
        public const int BUF_VIDEO_SRC = 1;
        public const int BUF_AUDIO_SRC = 2;
        public const int BUF_VIDEO_RENDER = 3;
        public const int BUF_AUDIO_RENDER = 4;

        //Error code
        public const int PLAYM4_NOERROR = 0;//no error
        public const int PLAYM4_PARA_OVER = 1;//input parameter is invalid;
        public const int PLAYM4_ORDER_ERROR = 2;//The order of the function to be called is error.
        public const int PLAYM4_TIMER_ERROR = 3;//Create multimedia clock failed;
        public const int PLAYM4_DEC_VIDEO_ERROR = 4;//Decode video data failed.
        public const int PLAYM4_DEC_AUDIO_ERROR = 5;//Decode audio data failed.
        public const int PLAYM4_ALLOC_MEMORY_ERROR = 6;//Allocate memory failed.
        public const int PLAYM4_OPEN_FILE_ERROR = 7;//Open the file failed.
        public const int PLAYM4_CREATE_OBJ_ERROR = 8;//Create thread or event failed
        public const int PLAYM4_CREATE_DDRAW_ERROR = 9;//Create DirectDraw object failed.
        public const int PLAYM4_CREATE_OFFSCREEN_ERROR = 10;//failed when creating off-screen surface.
        public const int PLAYM4_BUF_OVER = 11;//buffer is overflow
        public const int PLAYM4_CREATE_SOUND_ERROR = 12;//failed when creating audio device.	
        public const int PLAYM4_SET_VOLUME_ERROR = 13;//Set volume failed
        public const int PLAYM4_SUPPORT_FILE_ONLY = 14;//The function only support play file.
        public const int PLAYM4_SUPPORT_STREAM_ONLY = 15;//The function only support play stream.
        public const int PLAYM4_SYS_NOT_SUPPORT = 16;//System not support.
        public const int PLAYM4_FILEHEADER_UNKNOWN = 17;//No file header.
        public const int PLAYM4_VERSION_INCORRECT = 18;//The version of decoder and encoder is not adapted.  
        public const int PLAYM4_INIT_DECODER_ERROR = 19;//Initialize decoder failed.
        public const int PLAYM4_CHECK_FILE_ERROR = 20;//The file data is unknown.
        public const int PLAYM4_INIT_TIMER_ERROR = 21;//Initialize multimedia clock failed.
        public const int PLAYM4_BLT_ERROR = 22;//Blt failed.
        public const int PLAYM4_UPDATE_ERROR = 23;//Update failed.
        public const int PLAYM4_OPEN_FILE_ERROR_MULTI = 24;//openfile error, streamtype is multi
        public const int PLAYM4_OPEN_FILE_ERROR_VIDEO = 25;//openfile error, streamtype is video
        public const int PLAYM4_JPEG_COMPRESS_ERROR = 26;//JPEG compress error
        public const int PLAYM4_EXTRACT_NOT_SUPPORT = 27;//Don't support the version of this file.
        public const int PLAYM4_EXTRACT_DATA_ERROR = 28;//extract video data failed.
        public const int PLAYM4_SECRET_KEY_ERROR = 29;//Secret key is error //add 20071218

        //Max display regions.
        public const int MAX_DISPLAY_WND = 4;

        //Display type
        public const int DISPLAY_NORMAL = 1;
        public const int DISPLAY_QUARTER = 2;

        //Display buffers
        public const int MAX_DIS_FRAMES = 50;
        public const int MIN_DIS_FRAMES = 1;

        //Locate by
        /// <summary>
        /// 
        /// </summary>
        public const int BY_FRAMENUM = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int BY_FRAMETIME = 2;

        //Source buffer
        /// <summary>
        /// 
        /// </summary>
        public const int SOURCE_BUF_MAX = 1024 * 100000;
        /// <summary>
        /// 
        /// </summary>
        public const int SOURCE_BUF_MIN = 1024 * 50;

        //Stream type
        /// <summary>
        /// 
        /// </summary>
        public const int STREAME_REALTIME = 0;
        /// <summary>
        /// 
        /// </summary>
        public const int STREAME_FILE = 1;

        //frame type
        /// <summary>
        /// 
        /// </summary>
        public const int T_AUDIO16 = 101;
        /// <summary>
        /// 
        /// </summary>
        public const int T_AUDIO8 = 100;
        /// <summary>
        /// 
        /// </summary>
        public const int T_UYVY = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int T_YV12 = 3;
        /// <summary>
        /// 
        /// </summary>
        public const int T_RGB32 = 7;

        //capability
        public const int SUPPORT_DDRAW = 1;
        public const int SUPPORT_BLT = 2;
        public const int SUPPORT_BLTFOURCC = 4;
        public const int SUPPORT_BLTSHRINKX = 8;
        public const int SUPPORT_BLTSHRINKY = 16;
        public const int SUPPORT_BLTSTRETCHX = 32;
        public const int SUPPORT_BLTSTRETCHY = 64;
        public const int SUPPORT_SSE = 128;
        public const int SUPPORT_MMX = 256;

        // 以下宏定义用于HIK_MEDIAINFO结构
        public const int FOURCC_HKMI = 0x484B4D49;  // "HKMI" HIK_MEDIAINFO结构标记
                                                    // 系统封装格式
        public const int SYSTEM_NULL = 0;// 没有系统层，纯音频流或视频流	
        public const int SYSTEM_HIK = 1;// 海康文件层
        public const int SYSTEM_MPEG2_PS = 2;// PS封装
        public const int SYSTEM_MPEG2_TS = 3;// TS封装
        public const int SYSTEM_RTP = 4;// rtp封装

        // 视频编码类型
        public const int VIDEO_NULL = 0;// 没有视频
        public const int VIDEO_H264 = 1;// 标准H.264和海康H.264都可以用这个定义
        public const int VIDEO_MPEG4 = 3;// 标准MPEG4

        // 音频编码类型
        /// <summary>
        /// 没有音频
        /// </summary>
        public const int AUDIO_NULL = 0;
        /// <summary>
        /// ADPCM
        /// </summary>
        public const int AUDIO_ADPCM = 4096;
        /// <summary>
        /// MPEG 系列音频，解码器能自适应各种MPEG音频
        /// </summary>
        public const int AUDIO_MPEG = 8192;

        //G系列音频
        /// <summary>
        /// 
        /// </summary>
        public const int AUDIO_G711_U = 28944;
        /// <summary>
        /// 
        /// </summary>
        public const int AUDIO_G711_A = 28945;
        /// <summary>
        /// 
        /// </summary>
        public const int AUDIO_G722_1 = 29217;
        /// <summary>
        /// 
        /// </summary>
        public const int AUDIO_G723_1 = 29233;
        /// <summary>
        /// 
        /// </summary>
        public const int AUDIO_G726 = 29280;
        /// <summary>
        /// 
        /// </summary>
        public const int AUDIO_G729 = 29328;

        //Frame position
        public struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;

            public void Init()
            {
                wYear = 0;
                wMonth = 0;
                wDayOfWeek = 0;
                wDay = 0;
                wHour = 0;
                wMinute = 0;
                wSecond = 0;
                wMilliseconds = 0;
            }
        }

        public struct FRAME_POS
        {
            public int nFilePos;
            public int nFrameNum;
            public int nFrameTime;
            public int nErrorFrameNum;
            public IntPtr pErrorTime;
            public int nErrorLostFrameNum;
            public int nErrorFrameSize;

            public void Init()
            {
                nFilePos = 0;
                nFrameNum = 0;
                nFrameTime = 0;
                nErrorFrameNum = 0;
                pErrorTime = new IntPtr();
                nErrorLostFrameNum = 0;
                nErrorFrameSize = 0;
            }
        }

        //Frame Info
        public struct FRAME_INFO
        {
            public int nWidth;
            public int nHeight;
            public int nStamp;
            public int nType;
            public int nFrameRate;
            public uint dwFrameNum;

            public void Init()
            {
                nWidth = 0;
                nHeight = 0;
                nStamp = 0;
                nType = 0;
                nFrameRate = 0;
                dwFrameNum = 0;
            }
        }

        //Frame
        public struct FRAME_TYPE
        {
            [MarshalAsAttribute(UnmanagedType.LPStr)]
            public string pDataBuf;
            public int nSize;
            public int nFrameNum;
            public bool bIsAudio;
            public int nReserved;

            public void Init()
            {
                pDataBuf = "";
                nSize = 0;
                nFrameNum = 0;
                bIsAudio = false;
                nReserved = 0;
            }
        }

        //Watermark Info	//add by gb 080119
        public struct WATERMARK_INFO
        {
            [MarshalAsAttribute(UnmanagedType.LPStr)]
            public string pDataBuf;
            public int nSize;
            public int nFrameNum;
            public bool bRsaRight;
            public int nReserved;
            /// <summary>
            /// 
            /// </summary>
            public void Init()
            {
                pDataBuf = "";
                nSize = 0;
                nFrameNum = 0;
                bRsaRight = false;
                nReserved = 0;
            }
        }

        /// <summary>
        /// modified by gb 080425
        /// </summary>
        public struct HIK_MEDIAINFO
        {
            /// <summary>
            /// "HKMI": 0x484B4D49 Hikvision Media Information
            /// </summary>
            public uint media_fourcc;
            /// <summary>
            /// 版本号：指本信息结构版本号，目前为0x0101,即1.01版本，01：主版本号；01：子版本号。
            /// </summary>
            public ushort media_version;
            /// <summary>
            /// 设备ID，便于跟踪/分析
            /// </summary>
            public ushort device_id;
            /// <summary>
            /// 系统封装层
            /// </summary>
            public ushort system_format;
            /// <summary>
            /// 视频编码类型
            /// </summary>
            public ushort video_format;
            /// <summary>
            /// 音频编码类型
            /// </summary>
            public ushort audio_format;
            /// <summary>
            /// 通道数
            /// </summary>
            public byte audio_channels;
            /// <summary>
            /// 样位率
            /// </summary>
            public byte audio_bits_per_sample;
            /// <summary>
            /// 采样率
            /// </summary>
            public uint audio_samplesrate;
            /// <summary>
            ///  压缩音频码率,单位：bit
            /// </summary>
            public uint audio_bitrate;

            /// <summary>
            /// 保留
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] reserved;
            /// <summary>
            /// 
            /// </summary>
            public void Init()
            {
                media_fourcc = 0;
                media_version = 0;
                device_id = 0;
                system_format = 0;
                video_format = 0;
                audio_format = 0;
                audio_channels = 0;
                audio_bits_per_sample = 0;
                audio_samplesrate = 0;
                audio_bitrate = 0;
                reserved = new uint[4];
            }
        }

        //自己定义的函数接口
        /// <summary>
        /// ConverUiTimeToDateTime
        /// 把用uint表示的时间转换成DateTime表示的时间
        /// </summary>
        /// <param name="uiTime"></param>
        /// <returns></returns>
        public static DateTime ConverUiTimeToDateTime(uint uiTime)
        {
            int iYear = (int)((uiTime >> 26) + 2000);
            int iMonth = (int)((uiTime >> 22) & 15);
            int iDay = (int)((uiTime >> 17) & 31);
            int iHour = (int)((uiTime >> 12) & 31);
            int iMinute = (int)((uiTime >> 6) & 63);
            int iSecond = (int)((uiTime >> 0) & 63);
            DateTime dateTime = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond);
            return dateTime;
        }

        /// <summary>
        /// 从uint表示的时间获取时间
        /// </summary>
        /// <param name="uiTime"></param>
        /// <param name="uiHour"></param>
        /// <param name="uiMinute"></param>
        /// <param name="uiSecond"></param>
        public static void GetTimeFromUiTime(uint uiTime, ref uint uiHour, ref uint uiMinute, ref uint uiSecond)
        {
            uiHour = ((uiTime >> 12) & 31);
            uiMinute = ((uiTime >> 6) & 63);
            uiSecond = ((uiTime >> 0) & 63);
        }

        //API
        //Initialize DirecDraw.Now invalid.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_InitDDraw(IntPtr hWnd);
        /// <summary>
        /// Release directDraw; Now invalid.
        /// </summary>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_RealeseDDraw();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="sFileName"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_OpenFile(int nPort, String sFileName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_CloseFile(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_Play(int nPort, IntPtr hWnd);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_Stop(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nPause"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_Pause(int nPort, uint nPause);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_Fast(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_Slow(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_OneByOne(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="fRelativePos"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetPlayPos(int nPort, float fRelativePos);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern float PlayM4_GetPlayPos(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="hWnd"></param>
        /// <param name="nMsg"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetFileEndMsg(int nPort, IntPtr hWnd, uint nMsg);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nVolume"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetVolume(int nPort, ushort nVolume);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_StopSound();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_PlaySound(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pFileHeadBuf"></param>
        /// <param name="nSize"></param>
        /// <param name="nBufPoolSize"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_OpenStream(int nPort, IntPtr pFileHeadBuf, uint nSize, uint nBufPoolSize);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pBuf"></param>
        /// <param name="nSize"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_InputData(int nPort, IntPtr pBuf, uint nSize);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_CloseStream(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern int PlayM4_GetCaps();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetFileTime(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pStart"></param>
        /// <param name="pStop"></param>
        /// <param name="pRev"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_GetFileTimeEx(int nPort, ref uint pStart, ref uint pStop, ref uint pRev);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetPlayedTime(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetPlayedFrames(int nPort);

        ////////////////ver 2.0 added///////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pBuf"></param>
        /// <param name="nSize"></param>
        /// <param name="pFrameInfo"></param>
        /// <param name="nReserved1"></param>
        /// <param name="nReserved2"></param>
        public delegate void DECCBFUN(int nPort, IntPtr pBuf, int nSize, ref FRAME_INFO pFrameInfo, int nReserved1, int nReserved2);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="DecCBFun"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetDecCallBack(int nPort, DECCBFUN DecCBFun);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pBuf"></param>
        /// <param name="nSize"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="nStamp"></param>
        /// <param name="nType"></param>
        /// <param name="nReserved"></param>
        public delegate void DISPLAYCBFUN(int nPort, IntPtr pBuf, int nSize, int nWidth, int nHeight, int nStamp, int nType, int nReserved);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="DisplayCBFun"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetDisplayCallBack(int nPort, DISPLAYCBFUN DisplayCBFun);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pBuf"></param>
        /// <param name="nSize"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="nType"></param>
        /// <param name="sFileName"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PLayM4_ConvertToBmpFile(IntPtr pBuf, int nSize, int nWidth, int nHeight, int nType, string sFileName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetFileTotalFrames(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetCurrentFrameRate(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetPlayedTimeEx(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nTime"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetPlayedTimeEx(int nPort, uint nTime);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetCurrentFrameNum(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nMode"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetStreamOpenMode(int nPort, uint nMode);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetFileHeadLength();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetSdkVersion();

        ////////////////ver 2.2 added///////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetLastError(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_RefreshPlay(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="bOverlay"></param>
        /// <param name="colorKey"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetOverlayMode(int nPort, int bOverlay, uint colorKey);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pWidth"></param>
        /// <param name="pHeight"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_GetPictureSize(int nPort, ref int pWidth, ref int pHeight);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="bHighQuality"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetPicQuality(int nPort, int bHighQuality);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_PlaySoundShare(int nPort);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_StopSoundShare(int nPort);

        ////////////////ver 2.4 added///////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern int PlayM4_GetStreamOpenMode(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern int PlayM4_GetOverlayMode(int nPort);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetColorKey(int nPort);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern ushort PlayM4_GetVolume(int nPort);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="bHighQuality"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_GetPictureQuality(int nPort, ref int bHighQuality);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetSourceBufferRemain(int nPort);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_ResetSourceBuffer(int nPort);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nBufSize"></param>
        /// <param name="dwUser"></param>
        /// <param name="pResvered"></param>
        public delegate void SOURCEBUFCALLBACKI(int nPort, uint nBufSize, uint dwUser, IntPtr pResvered);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nThreShold"></param>
        /// <param name="SourceBufCallBack"></param>
        /// <param name="dwUser"></param>
        /// <param name="pReserved"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetSourceBufCallBack(int nPort, uint nThreShold, SOURCEBUFCALLBACKI SourceBufCallBack, uint dwUser, IntPtr pReserved);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_ResetSourceBufFlag(int nPort);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nNum"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetDisplayBuf(int nPort, uint nNum);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetDisplayBuf(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_OneByOneBack(int nPort);

        // PLAYM4_API BOOL __stdcall PlayM4_SetFileRefCallBack(LONG nPort, void (__stdcall *pFileRefDone)(DWORD nPort,DWORD nUser),DWORD nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nUser"></param>
        public delegate void PFILEREFDONE(uint nPort, uint nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pFileRefDone"></param>
        /// <param name="nUser"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetFileRefCallBack(int nPort, PFILEREFDONE pFileRefDone, uint nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nValue"></param>
        /// <param name="nType"></param>
        /// <param name="pFramePos"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_GetKeyFramePos(int nPort, uint nValue, uint nType, ref FRAME_POS pFramePos);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nValue"></param>
        /// <param name="nType"></param>
        /// <param name="pFramePos"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_GetNextKeyFramePos(int nPort, uint nValue, uint nType, ref FRAME_POS pFramePos);


        //#if (WINVER >= 0x0400)
        /// <summary>
        /// Note: These funtion must be builded under win2000 or above with Microsoft Platform sdk.
	    /// You can download the sdk from "http://www.microsoft.com/msdownload/platformsdk/sdkupdate/";
        /// </summary>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_InitDDrawDevice();
        /// <summary>
        /// 
        /// </summary>
        [DllImport(@"PlayCtrl.dll")]
        public static extern void PlayM4_ReleaseDDrawDevice();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetDDrawDeviceTotalNums();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nDeviceNum"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetDDrawDevice(int nPort, uint nDeviceNum);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nDDrawDeviceNum"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern int PlayM4_GetCapsEx(uint nDDrawDeviceNum);

        //#endif
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nNum"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_ThrowBFrameNum(int nPort, uint nNum);

        ////////////////ver 2.5 added///////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nType"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetDisplayType(int nPort, int nType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern int PlayM4_GetDisplayType(int nPort);

        ////////////////ver 3.0 added///////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public struct tagRECT
        {
            /// <summary>
            /// 
            /// </summary>
            public int left;
            /// <summary>
            /// 
            /// </summary>
            public int top;
            /// <summary>
            /// 
            /// </summary>
            public int right;
            /// <summary>
            /// 
            /// </summary>
            public int bottom;
            /// <summary>
            /// 
            /// </summary>
            public void Init()
            {
                left = 0;
                top = 0;
                right = 0;
                bottom = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nStream"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetDecCBStream(int nPort, uint nStream);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nRegionNum"></param>
        /// <param name="pSrcRect"></param>
        /// <param name="hDestWnd"></param>
        /// <param name="bEnable"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetDisplayRegion(int nPort, uint nRegionNum, ref tagRECT pSrcRect, System.IntPtr hDestWnd,
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)] bool bEnable);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nRegionNum"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_RefreshPlayEx(int nPort, uint nRegionNum);
        //#if (WINVER >= 0x0400)
        /// <summary>
        /// Note: The funtion must be builded under win2000 or above with Microsoft Platform sdk.
	    /// You can download the sdk from http://www.microsoft.com/msdownload/platformsdk/sdkupdate/;
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nRegionNum"></param>
        /// <param name="nDeviceNum"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetDDrawDeviceEx(int nPort, uint nRegionNum, uint nDeviceNum);

        //#endif
        /////////////////v3.2 added/////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pBuffer"></param>
        /// <param name="pSize"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_GetRefValue(int nPort, ref byte pBuffer, ref uint pSize);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pBuffer"></param>
        /// <param name="nSize"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetRefValue(int nPort, ref byte pBuffer, uint nSize);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pFileHeadBuf"></param>
        /// <param name="nSize"></param>
        /// <param name="nBufPoolSize"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_OpenStreamEx(int nPort, ref byte pFileHeadBuf, uint nSize, uint nBufPoolSize);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_CloseStreamEx(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pBuf"></param>
        /// <param name="nSize"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_InputVideoData(int nPort, IntPtr pBuf, uint nSize);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pBuf"></param>
        /// <param name="nSize"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_InputAudioData(int nPort, ref byte pBuf, uint nSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="hDc"></param>
        /// <param name="nUser"></param>
        public delegate void DRAWFUN(int nPort, System.IntPtr hDc, int nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="DrawFun"></param>
        /// <param name="nUser"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_RigisterDrawFun(int nPort, DRAWFUN DrawFun, int nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="DrawFun"></param>
        /// <param name="nUser"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_RegisterDrawFun(int nPort, DRAWFUN DrawFun, int nUser);

        //////////////////v3.4/////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nTimerType"></param>
        /// <param name="nReserved"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetTimerType(int nPort, uint nTimerType, uint nReserved);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pTimerType"></param>
        /// <param name="pReserved"></param>
        /// <returns></returns>

        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_GetTimerType(int nPort, ref uint pTimerType, ref uint pReserved);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nBufType"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_ResetBuffer(int nPort, uint nBufType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nBufType"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetBufferValue(int nPort, uint nBufType);

        //////////////////V3.6/////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nCoefficient"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_AdjustWaveAudio(int nPort, int nCoefficient);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pFilePos"></param>
        /// <param name="bIsVideo"></param>
        /// <param name="nUser"></param>
        public delegate void FUNVERYFY(int nPort, ref FRAME_POS pFilePos, uint bIsVideo, uint nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nBeginTime"></param>
        /// <param name="nEndTime"></param>
        /// <param name="funVerify"></param>
        /// <param name="nUser"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetVerifyCallBack(int nPort, uint nBeginTime, uint nEndTime, FUNVERYFY funVerify, uint nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pAudioBuf"></param>
        /// <param name="nSize"></param>
        /// <param name="nStamp"></param>
        /// <param name="nType"></param>
        /// <param name="nUser"></param>
        public delegate void FUNAUDIO(int nPort, string pAudioBuf, int nSize, int nStamp, int nType, int nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="funAudio"></param>
        /// <param name="nUser"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetAudioCallBack(int nPort, FUNAUDIO funAudio, int nUser);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nUser"></param>
        public delegate void FUNENCCHANGE(int nPort, int nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="funEncChange"></param>
        /// <param name="nUser"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetEncTypeChangeCallBack(int nPort, FUNENCCHANGE funEncChange, int nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nRegionNum"></param>
        /// <param name="nBrightness"></param>
        /// <param name="nContrast"></param>
        /// <param name="nSaturation"></param>
        /// <param name="nHue"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetColor(int nPort, uint nRegionNum, int nBrightness, int nContrast, int nSaturation, int nHue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nRegionNum"></param>
        /// <param name="pBrightness"></param>
        /// <param name="pContrast"></param>
        /// <param name="pSaturation"></param>
        /// <param name="pHue"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_GetColor(int nPort, uint nRegionNum, ref int pBrightness, ref int pContrast, ref int pSaturation, ref int pHue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="hWnd"></param>
        /// <param name="nMsg"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetEncChangeMsg(int nPort, System.IntPtr hWnd, uint nMsg);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="frameType"></param>
        /// <param name="nUser"></param>
        public delegate void FUNGETORIGNALFRAME(int nPort, ref FRAME_TYPE frameType, int nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="bIsChange"></param>
        /// <param name="bNormalSpeed"></param>
        /// <param name="nStartFrameNum"></param>
        /// <param name="nStartStamp"></param>
        /// <param name="nFileHeader"></param>
        /// <param name="funGetOrignalFrame"></param>
        /// <param name="nUser"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_GetOriginalFrameCallBack(int nPort, int bIsChange, int bNormalSpeed, int nStartFrameNum, int nStartStamp, int nFileHeader, FUNGETORIGNALFRAME funGetOrignalFrame, int nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pTimeStamp"></param>
        /// <param name="pFileNum"></param>
        /// <param name="pReserved"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_GetFileSpecialAttr(int nPort, ref uint pTimeStamp, ref uint pFileNum, ref uint pReserved);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetSpecialData(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pWatermarkInfo"></param>
        /// <param name="nUser"></param>
        public delegate void FUNCHECKWATERMARK(int nPort, ref WATERMARK_INFO pWatermarkInfo, uint nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="funCheckWatermark"></param>
        /// <param name="nUser"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetCheckWatermarkCallBack(int nPort, FUNCHECKWATERMARK funCheckWatermark, uint nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nLevel"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetImageSharpen(int nPort, uint nLevel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nBFrame"></param>
        /// <param name="nUser"></param>
        public delegate void FUNTHROWBFRAME(int nPort, uint nBFrame, uint nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="funThrowBFrame"></param>
        /// <param name="nUser"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_GetThrowBFrameCallBack(int nPort, FUNTHROWBFRAME funThrowBFrame, uint nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nFrameType"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetDecodeFrameType(int nPort, uint nFrameType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="bNormal"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetPlayMode(int nPort, int bNormal);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pUserBuf"></param>
        /// <param name="nBufLen"></param>
        /// <param name="nUser"></param>
        public delegate void FUNGETUSERDATA(int nPort, ref byte pUserBuf, uint nBufLen, uint nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="funGetUserData"></param>
        /// <param name="nUser"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetGetUserDataCallBack(int nPort, FUNGETUSERDATA funGetUserData, uint nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="bTrue"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetOverlayFlipMode(int nPort, int bTrue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern uint PlayM4_GetAbsFrameNum(int nPort);

        //////////////////V4.7.0.0//////////////////////////////////////////////////////
        /// <summary>
        /// convert yuv to jpeg
        /// </summary>
        /// <param name="pBuf"></param>
        /// <param name="nSize"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="nType"></param>
        /// <param name="sFileName"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_ConvertToJpegFile(IntPtr pBuf, int nSize, int nWidth, int nHeight, int nType, string sFileName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nQuality"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetJpegQuality(int nQuality);

        /// <summary>
        /// set deflash
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="bDefalsh"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetDeflash(int nPort, int bDefalsh);

        //////////////////V4.8.0.0/////////////////////////////////////////////////////////
        //check discontinuous frame number as error data?
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="bCheck"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_CheckDiscontinuousFrameNum(int nPort, int bCheck);
        /// <summary>
        /// get bmp or jpeg
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pBitmap"></param>
        /// <param name="nBufSize"></param>
        /// <param name="pBmpSize"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_GetBMP(int nPort, byte[] pBitmap, uint nBufSize, ref uint pBmpSize);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pJpeg"></param>
        /// <param name="nBufSize"></param>
        /// <param name="pJpegSize"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_GetJPEG(int nPort, IntPtr pJpeg, uint nBufSize, ref uint pJpegSize);

        //dec call back mend
        //public delegate void DECCBFUN(int nPort, string pBuf, int nSize, ref FRAME_INFO pFrameInfo, int nUser, int nReserved2);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="DecCBFun"></param>
        /// <param name="nUser"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetDecCallBackMend(int nPort, DECCBFUN DecCBFun, int nUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="lKeyType"></param>
        /// <param name="pSecretKey"></param>
        /// <param name="lKeyLen"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetSecretKey(int nPort, int lKeyType, string pSecretKey, int lKeyLen);

        // add by gb 2007-12-23
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pUser"></param>
        public delegate void FILEENDCALLBACK(int nPort, System.IntPtr pUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="FileEndCallback"></param>
        /// <param name="pUser"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetFileEndCallback(int nPort, FILEENDCALLBACK FileEndCallback, IntPtr pUser);

        // add by gb 080131 version 4.9.0.1
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_GetPort(ref int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_FreePort(int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="bSyncToAudio"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SyncToAudio(int nPort, int bSyncToAudio);

        //public delegate void Anonymous_b532dad6_7470_4b10_9638_c82a363cd853(int nPort, System.IntPtr pBuf, int nSize, ref FRAME_INFO pFrameInfo, int nReserved1, int nReserved2);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="DecCBFun"></param>
        /// <param name="pDest"></param>
        /// <param name="nDestSize"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetDecCallBackEx(int nPort, DECCBFUN DecCBFun, IntPtr pDest, int nDestSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nIntelType"></param>
        /// <param name="bTrue"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_RenderPrivateData(int nPort, int nIntelType, bool bTrue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nIntelType"></param>
        /// <param name="nSubType"></param>
        /// <param name="bTrue"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_RenderPrivateDataEx(int nPort, int nIntelType, int nSubType, bool bTrue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nIntelType"></param>
        /// <param name="bTrue"></param>
        /// <returns></returns>
        [DllImport(@"PlayCtrl.dll")]
        public static extern bool PlayM4_SetOverlayPriInfoFlag(int nPort, int nIntelType, bool bTrue);

    }
}