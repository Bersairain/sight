using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconControl;
using HalconDotNet;


namespace Sight
{
    public class cameraserve
    {
        //1. 私有静态变量（在第四步供外界使用），创建类的实例
        //2. 私有构造函数，确保外部无法直接实例化（确保是单个实例）
        //3. 确定供外界调用的代码资源
        //4. 公开静态属性，供外界使用（把第一步类的实例，开放出去）
        //5. 外界使用

        //1. 创建类的实例,同时变成私有的静态变量（在第四步供外界使用），

        private static cameraserve instance = new cameraserve();

        //2. 私有构造函数，确保外部无法直接实例化（确保是单个实例）
        private cameraserve() { }

        //4. 公开静态属性，供外界使用（把第一步类的实例，开放出去）
        public static cameraserve Instance { get { return instance; } }

        //3. 确定供外界调用的代码资源


        Hik hkcamera= new Hik();

        /// <summary>
        /// 获取所有相机的序列号
        /// </summary>
        /// <returns></returns>
        /// 
        public List<string> Get_DeviceList()
        {
            return hkcamera.GetDeviceList();
        }


        /// <summary>
        /// 打开相机
        /// </summary>
        /// <param name="SerialNum"></param>
        /// <returns></returns>
        public bool OpenDevice(string SerialNum)
        {

            //【4】将具体方法和委托变量关联

            // 5. 委托变量和方法进行绑定
            hkcamera.grabHImage += GrabImage;
            hkcamera.SerialNumber = SerialNum;
            if (hkcamera.OpenDevice())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CloseDevice()
        {

            hkcamera.CloseDevice();
        }

        /// <summary>
        /// 设置采集和触发模式
        /// </summary>
        public void SetTriggerMode(TriggerMode triggerMode, TriggerSource triggerSource)
        {
            hkcamera.SetTriggerMode(triggerMode, triggerSource);
        }

        /// <summary>
        /// 停止采集
        /// </summary>
        public void StopGrab()
        {
            hkcamera.StopGrab();
        }

        /// <summary>
        /// 软触发一次
        /// </summary>
        public void SnapImage()
        {
            hkcamera.SnapImage();
        }
        /// <summary>
        /// 开始采集
        /// </summary>
        public void StartGrab()
        {
            hkcamera.StartGrab();
        }

        public bool InitCameras(string CameraSerialNum)
        {

            hkcamera.SerialNumber = CameraSerialNum;
            //hkcamera.grabHImage += GrabDetectImage;

            return true;
        }
        #region 图像回调方法
        //【2】根据委托编写具体方法（参数要传递到的位置）
        // 4.编写需要使用参数的方法
        public void GrabImage(HImage hImg)
        {
            // 把图像显示到 MainForm的Halcon 控件 窗口上

            // 1.获取到mainform窗口
            // Application 表示整个软件程序
            // OpenForms ：打开的窗口
            // OfType<MainForm>():找到MainForm这个类型的窗口
            // FirstOrDefault() 第一个
            var mainForm = Application.OpenForms.OfType<Form1>().FirstOrDefault();

            // 2.获取窗口上的Halcon的显示控件
            HWindow_Final hWindow = mainForm.Get_hWindow_Final_ShowCameraImg();

            // 3. 显示图像
            hWindow.HobjectToHimage(hImg);
        }


        /// <summary>
        /// 用于检测的图像函数
        /// </summary>
        /// <param name="hImg"></param>
        //public void GrabDetectImage(HImage hImg)
        //{
        //    // 1.获取到mainform窗口
        //    var mainForm = Application.OpenForms.OfType<Form1>().FirstOrDefault();
        //    // 2.获取窗口上的Halcon的显示控件
        //    HWindow_Final hWindow = mainForm.Get_hWindow_CameraImg();

        //    // 3. 显示图像
        //    hWindow.HobjectToHimage(hImg);

        //    // 获取当前时间
        //    DateTime dateTime = DateTime.Now;
        //    // 4.把图像加到图像队列里

        //    //(3) 利用添加图像函数，把图像添加到workflowService的图像队列里
        //    ImageInfo imageInfo = new ImageInfo(
        //        dateTime.ToString("yyyy_MM_dd"),
        //        dateTime.ToString("HH_mm_ss_fff"),
        //        hImg
        //        );

        //    WorkFlowService.Instance.AddImageInfo1(imageInfo);
        //}

        #endregion
    }
}
