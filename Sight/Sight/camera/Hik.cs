using HalconDotNet;
using MvCamCtrl.NET;
using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static MvCamCtrl.NET.MyCamera;
using static System.Net.Mime.MediaTypeNames;

namespace Sight
{

    /// <summary>
    /// 触发模式
    /// </summary>
    public enum TriggerMode
    {
        /// <summary>
        /// 连续模式
        /// </summary>
        Continues,

        /// <summary>
        /// 触发模式
        /// </summary>
        Trigger
    }

    /// <summary>
    /// 触发源
    /// </summary>
    public enum TriggerSource
    {
        /// <summary>
        /// 外部触发
        /// </summary>
        Hardware,

        /// <summary>
        /// 软触发
        /// </summary>
        Software
    }

    //【1】声明委托(方法的原型)

    /// <summary>
    /// 取图委托
    /// </summary>
    /// <param name="hImg"></param>
    /// 
    /// 委托就是数组，专门存放函数的一个数组
    ///  public void fun1(HImage img)
    /// 目的：要把HiKCamera里的图像数据，传到MainForm里
    public delegate void GrabHImage(HImage hImg);
    // 访问修饰符 + delegate+ 能存放函数的返回类型 + 委托名称 + （能存放函数的变量）
    internal class Hik
    {
        /// <summary>*****************************
        ///海康相机类
        /// </summary>****************************
        /// //【3】定义委托变量

        // 函数是放到委托变量里的，委托变量才是真正能用的存放函数的数组
        public event GrabHImage grabHImage;
        public Hik()
        {
            m_stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
            m_MyCamera = new MyCamera();
        }

        #region 共有属性
        /// <summary>
        /// 相机名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 相机序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 相机是否已连接
        /// </summary>
        public bool IsConnect { get; private set; }

        /// <summary>
        /// 是否为触发模式
        /// </summary>
        public bool isTriggerMode { get; private set; }

        /// <summary>
        /// 是否开始采集
        /// </summary>
        public bool isGrabbing { get; private set; }

        #endregion

        #region 内部变量

        // 设备列表
        private MyCamera.MV_CC_DEVICE_INFO_LIST m_stDeviceList;

        // 相机对象
        private MyCamera m_MyCamera = null;

        // 取像线程
        Thread m_hReceiveThread = null;

        // 帧信息
        MyCamera.MV_FRAME_OUT_INFO_EX m_stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

        // 用于从驱动获取图像的缓存
        IntPtr m_BufForDriver;
        UInt32 m_nBufSizeForDriver = 0;
        byte[] m_pDataForRed = null;
        byte[] m_pDataForGreen = null;
        byte[] m_pDataForBlue = null;
        private Object BufForDriverLock = new Object();
        private Object BufForImageLock = new Object();  // 读写图像时锁定
        #endregion

        /// <summary>*****************************
        /// 海康相机方法
        /// </summary>****************************
        /// 
        //遍历读取所有相机
        /// <summary>
        /// 获取所有相机的序列号
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// 获取所有相机的序列号
        /// </summary>
        /// <returns></returns>
        public List<string> GetDeviceList()
        {
            List<string> deviceList = new List<string>();

            EnumDevices();

            for (int i = 0; i < m_stDeviceList.nDeviceNum; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    MyCamera.MV_USB3_DEVICE_INFO usb3Info = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                    deviceList.Add($"{usb3Info.chSerialNumber}");
                }
                else if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    deviceList.Add($"{gigeInfo.chSerialNumber}");
                }
            }

            return deviceList;
        }

        /// <summary>
        /// 打开相机
        /// </summary>
        /// <returns></returns>
        public bool OpenDevice()
        {
            // 枚举并搜索指定ID的相机是否存在
            EnumDevices();
            int camIdx = GetDeviceIndex(SerialNumber);
            if (camIdx == -1)
            {
                MessageBox.Show("找不到该ID的相机!");
                return false;
            }
            MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[camIdx],
                                                              typeof(MyCamera.MV_CC_DEVICE_INFO));

            // 建立设备对象
            if (null == m_MyCamera)
            {
                m_MyCamera = new MyCamera();
                if (null == m_MyCamera)
                {
                    MessageBox.Show("初始化相机对象失败");

                    return false;
                }
            }

            // 创建设备
            int nRet = m_MyCamera.MV_CC_CreateDevice_NET(ref device);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show($"创建设备失败，失败代码：{nRet}");
                return false;
            }

            // 尝试打开设备
            nRet = m_MyCamera.MV_CC_OpenDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                m_MyCamera.MV_CC_DestroyDevice_NET();

                MessageBox.Show($"设备打开失败,失败代码：{nRet}");
                return false;
            }

            // 探测网络最佳包大小(只对GigE相机有效)
            if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                int nPacketSize = m_MyCamera.MV_CC_GetOptimalPacketSize_NET();
                if (nPacketSize > 0)
                {
                    nRet = m_MyCamera.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                    if (nRet != MyCamera.MV_OK)
                    {
                        MessageBox.Show($"设置包大小失败,失败代码：{nRet}");
                    }
                }
                else
                {
                    MessageBox.Show($"获取包大小失败,返回的包大小为：{nPacketSize}");
                }
            }

            // 设置采集连续模式
            m_MyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", (uint)MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
            m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);

            // 注册异常回调
            m_MyCamera.MV_CC_RegisterExceptionCallBack_NET(cbException, IntPtr.Zero);

            IsConnect = true;
            return true;
        }

        private void cbException(uint nMsgType, IntPtr pUser)
        {
            IsConnect = false;

            if (nMsgType == MyCamera.MV_EXCEPTION_DEV_DISCONNECT)
            {
                // 先关闭设备
                CloseDevice();

                // 在尝试重新打开设备
                if (OpenDevice())
                {
                    MessageBox.Show("尝试重新连接设备失败!");
                }
            }
        }

        /// <summary>
        /// 关闭相机
        /// </summary>
        public void CloseDevice()
        {
            // 取流标志位清零
            if (isGrabbing == true)
            {
                isGrabbing = false;
                m_hReceiveThread.Join();
            }

            if (m_BufForDriver != IntPtr.Zero)
            {
                Marshal.Release(m_BufForDriver);
            }

            // 关闭设备
            m_MyCamera.MV_CC_CloseDevice_NET();
            m_MyCamera.MV_CC_DestroyDevice_NET();

            IsConnect = false;
        }

        /// <summary>
        /// 开始采集
        /// </summary>
        /// <returns></returns>
        public bool StartGrab()
        {
            // 标志位置位true
            isGrabbing = true;

            m_hReceiveThread = new Thread(ReceiveThreadProcess);
            m_hReceiveThread.Start();

            // 取流之前先清除帧长度
            m_stFrameInfo.nFrameLen = 0;
            m_stFrameInfo.enPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8;

            // 开始采集
            int nRet = m_MyCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                isGrabbing = false;
                m_hReceiveThread.Join();

                MessageBox.Show($"连续采集失败,失败代码：{nRet}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 停止采集
        /// </summary>
        /// <returns></returns>
        public bool StopGrab()
        {
            // 标志位设为false
            isGrabbing = false;
            m_hReceiveThread.Join();

            // 停止采集
            int nRet = m_MyCamera.MV_CC_StopGrabbing_NET();
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show($"停止采集失败,失败代码：{nRet}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 设置采集和触发模式
        /// </summary>
        /// <param name="triggerMode"></param>
        /// <param name="triggerSource"></param>
        /// <returns></returns>
        public bool SetTriggerMode(TriggerMode triggerMode, TriggerSource triggerSource)
        {
            if (triggerMode == TriggerMode.Continues)
            {
                int nRet = m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);
                if (nRet == MyCamera.MV_OK)
                    isTriggerMode = false;
            }
            else if (triggerMode == TriggerMode.Trigger)
            {
                int nRet = m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
                if (nRet == MyCamera.MV_OK)
                    isTriggerMode = true;

                if (triggerSource == TriggerSource.Hardware)
                {
                    // Line0触发
                    m_MyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);

                    // 采用上升沿触发
                    m_MyCamera.MV_CC_SetEnumValue_NET("TriggerActivation", 0);

                    // 线路防抖 250us
                    m_MyCamera.MV_CC_SetIntValue_NET("LineDebouncerTime", 250);
                }
                else if (triggerSource == TriggerSource.Software)
                {
                    m_MyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
                }
                else
                {
                    MessageBox.Show("不支持的触发源");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("不支持的触发模式");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 软触发拍照
        /// </summary>
        /// <returns></returns>
        public bool SnapImage()
        {
            // 触发命令
            int nRet = m_MyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show($"单张采集触发失败,失败代码：{nRet}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 设置曝光
        /// </summary>
        /// <param name="exp"></param>
        public void SetExposure(float exp)
        {
            // 关闭自动曝光
            m_MyCamera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);
            int nRet = m_MyCamera.MV_CC_SetFloatValue_NET("ExposureTime", exp);
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show($"设置曝光失败，失败代码：{nRet}");
            }
        }


        /// <summary>
        /// 获取曝光
        /// </summary>
        /// <returns></returns>
        public float GetExposure()
        {
            // 曝光时间
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            int nRet = m_MyCamera.MV_CC_GetFloatValue_NET("ExposureTime", ref stParam);
            if (MyCamera.MV_OK == nRet)
            {
                return stParam.fCurValue;
            }

            return 0;
        }

        /// <summary>
        /// 设置帧率
        /// </summary>
        /// <param name="fr"></param>
        public void SetFrameRate(float fr)
        {
            int nRet = m_MyCamera.MV_CC_SetFloatValue_NET("AcquisitionFrameRate", fr);
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show($"设置帧率失败，失败代码：{nRet}");
            }
        }


        /// <summary>
        /// 获取帧率
        /// </summary>
        /// <returns></returns>
        public float GetFrameRate()
        {
            // 帧率
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            int nRet = m_MyCamera.MV_CC_GetFloatValue_NET("ResultingFrameRate", ref stParam);
            if (MyCamera.MV_OK == nRet)
            {
                return stParam.fCurValue;
            }

            return 0;
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="gain"></param>
        public void SetGain(float gain)
        {
            m_MyCamera.MV_CC_SetEnumValue_NET("GainAuto", 0);
            int nRet = m_MyCamera.MV_CC_SetFloatValue_NET("Gain", gain);
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show($"设置增益失败，失败代码：{nRet}");
            }
        }

        /// <summary>
        /// 获取增益
        /// </summary>
        /// <returns></returns>
        public float GetGain()
        {
            // 增益
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            int nRet = m_MyCamera.MV_CC_GetFloatValue_NET("Gain", ref stParam);
            if (MyCamera.MV_OK == nRet)
            {
                return stParam.fCurValue;
            }

            return 0;
        }

        public void SetGammaEnable(bool bEnable)
        {
            int nRet = m_MyCamera.MV_CC_SetBoolValue_NET("GammaEnable", bEnable);
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show($"设置Gamma使能失败，失败代码：{nRet}");
            }
        }

        public bool GetGammaEnable()
        {
            // 增益使能
            bool bGamma = false;
            int nRet = m_MyCamera.MV_CC_GetBoolValue_NET("GammaEnable", ref bGamma);
            if (MyCamera.MV_OK == nRet)
            {
                return bGamma;
            }

            return false;
        }

        public void SetGammaValue(float gamma)
        {
            bool bEnable = false;
            int nRet = m_MyCamera.MV_CC_GetBoolValue_NET("GammaEnable", ref bEnable);
            if (bEnable)
            {
                nRet = m_MyCamera.MV_CC_SetFloatValue_NET("Gamma", gamma);
                if (nRet != MyCamera.MV_OK)
                {
                    //MessageBox.Show($"设置Gamma失败，失败代码：{nRet}");
                }
            }
        }

        public float GetGammaValue()
        {
            // Gamma值
            bool bGamma = false;
            int nRet = m_MyCamera.MV_CC_GetBoolValue_NET("GammaEnable", ref bGamma);

            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            if (MyCamera.MV_OK == nRet && bGamma)
            {
                m_MyCamera.MV_CC_GetFloatValue_NET("Gamma", ref stParam);
                return stParam.fCurValue;
            }

            return 0;
        }

        public override string ToString()
        {
            return $"{Name}_{SerialNumber}";
        }

        #region 私有方法



        /// <summary>
        /// 取像线程
        /// </summary>
        private void ReceiveThreadProcess()
        {
            //1\获取单帧图像数据的有效负载大小(单位为字节)
            MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
            int nRet = m_MyCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show($"读取PayloadSize失败，失败代码：{nRet}");
                return;
            }
            UInt32 nPayloadSize = stParam.nCurValue;

            //2\ 获取图像高
            nRet = m_MyCamera.MV_CC_GetIntValue_NET("Height", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show($"获取图像高失败，失败代码：{nRet}");
                return;
            }
            uint nHeight = stParam.nCurValue;

            // 3\获取图像宽
            nRet = m_MyCamera.MV_CC_GetIntValue_NET("Width", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show($"获取图像宽失败，失败代码：{nRet}");
                return;
            }
            uint nWidth = stParam.nCurValue;

            // 4\根据图像大小设置图像缓存
            m_pDataForRed = new byte[nWidth * nHeight];
            m_pDataForGreen = new byte[nWidth * nHeight];
            m_pDataForBlue = new byte[nWidth * nHeight];
            if (3 * nPayloadSize > m_nBufSizeForDriver)
            {
                if (m_BufForDriver != IntPtr.Zero)
                {
                    Marshal.Release(m_BufForDriver);
                }
                m_nBufSizeForDriver = 3 * nPayloadSize;
                m_BufForDriver = Marshal.AllocHGlobal((Int32)m_nBufSizeForDriver);
            }
            if (m_BufForDriver == IntPtr.Zero)
            {
                return;
            }

            IntPtr pImageBuffer = Marshal.AllocHGlobal((int)nPayloadSize * 3);
            if (pImageBuffer == IntPtr.Zero)
            {
                MessageBox.Show($"申请图像缓存区失败！");
                return;
            }

            MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
            IntPtr RedPtr = IntPtr.Zero;
            IntPtr GreenPtr = IntPtr.Zero;
            IntPtr BluePtr = IntPtr.Zero;
            IntPtr pTemp = IntPtr.Zero;
            DateTime ProStartTime = DateTime.MinValue;
            //5\循环监听，触发相机的图像采集信号
            while (isGrabbing)
            {
                //6\获取一帧图像数据(核心)等待，软触发或者硬触发的信号
                lock (BufForDriverLock)
                {
                    nRet = m_MyCamera.MV_CC_GetOneFrameTimeout_NET(m_BufForDriver, m_nBufSizeForDriver, ref stFrameInfo, 1000);
                    //如果采集成功，则进行记录显示
                    if (nRet == MyCamera.MV_OK)
                    {
                        ProStartTime = DateTime.Now;
                        m_stFrameInfo = stFrameInfo;

                    }
                }
                HImage hImage = new HImage();

                if (nRet == MyCamera.MV_OK)
                {
                    // 彩色相机
                    if (IsColorData(stFrameInfo.enPixelType))
                    {
                        //如果是彩色图像格式，则直接使用数据，给到pTemp
                        if (stFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed)
                        {
                            pTemp = m_BufForDriver;
                        }
                        //否则转换成RGB三通道数据，再给pTemp
                        else
                        {
                            nRet = ConvertToRGB(m_MyCamera, m_BufForDriver, stFrameInfo.nHeight, stFrameInfo.nWidth, stFrameInfo.enPixelType, pImageBuffer);
                            if (MyCamera.MV_OK != nRet)
                            {
                                return;
                            }
                            pTemp = pImageBuffer;
                        }
                        //获取RGB三个通道的数据
                        unsafe
                        {
                            byte* pBufForSaveImage = (byte*)pTemp;

                            UInt32 nSupWidth = (stFrameInfo.nWidth + (UInt32)3) & 0xfffffffc;//5120

                            for (int nRow = 0; nRow < stFrameInfo.nHeight; nRow++)
                            {
                                for (int col = 0; col < stFrameInfo.nWidth; col++)
                                {
                                    m_pDataForRed[nRow * nSupWidth + col] = pBufForSaveImage[nRow * stFrameInfo.nWidth * 3 + (3 * col)];
                                    m_pDataForGreen[nRow * nSupWidth + col] = pBufForSaveImage[nRow * stFrameInfo.nWidth * 3 + (3 * col + 1)];
                                    m_pDataForBlue[nRow * nSupWidth + col] = pBufForSaveImage[nRow * stFrameInfo.nWidth * 3 + (3 * col + 2)];
                                }
                            }
                        }
                        //rgb三通道的指针数据转bitmap(该代码中已经修改为转为halcon使用的图像)
                        RedPtr = Marshal.UnsafeAddrOfPinnedArrayElement(m_pDataForRed, 0);
                        GreenPtr = Marshal.UnsafeAddrOfPinnedArrayElement(m_pDataForGreen, 0);
                        BluePtr = Marshal.UnsafeAddrOfPinnedArrayElement(m_pDataForBlue, 0);

                        lock (BufForImageLock)
                        {

                            hImage.GenImage3Extern("byte", stFrameInfo.nWidth, stFrameInfo.nHeight, RedPtr, GreenPtr, BluePtr, IntPtr.Zero);
                            //进行显示
                            if (grabHImage != null)
                                //【5】使用委托变量
                                // 3. 调用委托变量
                                grabHImage(hImage);
                        }
                    }
                    else if (IsMonoData(stFrameInfo.enPixelType))                     // 黑白图像
                    {
                        if (stFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                        {
                            pTemp = m_BufForDriver;
                        }
                        else
                        {
                            nRet = ConvertToMono8(m_MyCamera, m_BufForDriver, pImageBuffer, stFrameInfo.nHeight, stFrameInfo.nWidth, stFrameInfo.enPixelType);
                            if (MyCamera.MV_OK != nRet)
                            {
                                return;
                            }
                            pTemp = pImageBuffer;
                        }

                        // 通过事件传递图像数据
                        lock (BufForImageLock)
                        {
                            hImage.GenImage1("byte", stFrameInfo.nWidth, stFrameInfo.nHeight, pTemp);
                            //【5】使用委托变量
                            grabHImage(hImage);
                        }

                    }
                    else
                    {
                        continue;
                    }
                }
                //如果采集失败，线程沉睡5ms(如果是触发模式的话)
                else
                {
                    if (isTriggerMode)
                    {
                        Thread.Sleep(5);
                    }
                }
            }
        }

        // 枚举海康相机（GIGE，USB3）
        public void EnumDevices()
        {
            // 枚举设备列表
            m_stDeviceList.nDeviceNum = 0;
            int nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_stDeviceList);
            if (0 != nRet)
            {
                MessageBox.Show("枚举HIK相机设备失败!");
                return;
            }
        }

        // 获取相机对应的枚举索引
        private int GetDeviceIndex(string CameraID)
        {
            for (int i = 0; i < m_stDeviceList.nDeviceNum; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    if (gigeInfo.chSerialNumber == CameraID)
                        return i;
                }
                else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    MyCamera.MV_USB3_DEVICE_INFO usb3Info = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                    if (usb3Info.chSerialNumber == CameraID)
                        return i;
                }
            }

            return -1;
        }

        // 判断是否为黑白图像
        private Boolean IsMonoData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;

                default:
                    return false;
            }
        }

        // 判断是否为彩色图像
        private Boolean IsColorData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YCBCR411_8_CBYYCRYY:
                    return true;
                default:
                    return false;
            }
        }

        // 转换为RGB格式
        private Int32 ConvertToRGB(object obj, IntPtr pSrc, ushort nHeight, ushort nWidth, MyCamera.MvGvspPixelType nPixelType, IntPtr pDst)
        {
            if (IntPtr.Zero == pSrc || IntPtr.Zero == pDst)
            {
                return MyCamera.MV_E_PARAMETER;
            }

            int nRet = MyCamera.MV_OK;
            MyCamera device = obj as MyCamera;
            MyCamera.MV_PIXEL_CONVERT_PARAM stPixelConvertParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

            stPixelConvertParam.pSrcData = pSrc;//源数据
            if (IntPtr.Zero == stPixelConvertParam.pSrcData)
            {
                return -1;
            }

            stPixelConvertParam.nWidth = nWidth;//图像宽度
            stPixelConvertParam.nHeight = nHeight;//图像高度
            stPixelConvertParam.enSrcPixelType = nPixelType;//源数据的格式
            stPixelConvertParam.nSrcDataLen = (uint)(nWidth * nHeight * ((((uint)nPixelType) >> 16) & 0x00ff) >> 3);

            stPixelConvertParam.nDstBufferSize = (uint)(nWidth * nHeight * ((((uint)MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed) >> 16) & 0x00ff) >> 3);
            stPixelConvertParam.pDstBuffer = pDst;//转换后的数据
            stPixelConvertParam.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
            stPixelConvertParam.nDstBufferSize = (uint)nWidth * nHeight * 3;

            nRet = device.MV_CC_ConvertPixelType_NET(ref stPixelConvertParam);//格式转换
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }

            return MyCamera.MV_OK;
        }

        // 转换为Mono8格式
        private Int32 ConvertToMono8(object obj, IntPtr pInData, IntPtr pOutData, ushort nHeight, ushort nWidth, MyCamera.MvGvspPixelType nPixelType)
        {
            if (IntPtr.Zero == pInData || IntPtr.Zero == pOutData)
            {
                return MyCamera.MV_E_PARAMETER;
            }

            int nRet = MyCamera.MV_OK;
            MyCamera device = obj as MyCamera;
            MyCamera.MV_PIXEL_CONVERT_PARAM stPixelConvertParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

            stPixelConvertParam.pSrcData = pInData;//源数据
            if (IntPtr.Zero == stPixelConvertParam.pSrcData)
            {
                return -1;
            }

            stPixelConvertParam.nWidth = nWidth;//图像宽度
            stPixelConvertParam.nHeight = nHeight;//图像高度
            stPixelConvertParam.enSrcPixelType = nPixelType;//源数据的格式
            stPixelConvertParam.nSrcDataLen = (uint)(nWidth * nHeight * ((((uint)nPixelType) >> 16) & 0x00ff) >> 3);

            stPixelConvertParam.nDstBufferSize = (uint)(nWidth * nHeight * ((((uint)MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed) >> 16) & 0x00ff) >> 3);
            stPixelConvertParam.pDstBuffer = pOutData;//转换后的数据
            stPixelConvertParam.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
            stPixelConvertParam.nDstBufferSize = (uint)(nWidth * nHeight * 3);

            nRet = device.MV_CC_ConvertPixelType_NET(ref stPixelConvertParam);//格式转换
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }

            return nRet;
        }


        #endregion




    }
}
