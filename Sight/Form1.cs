using HalconControl;
using HalconDotNet;
using Microsoft.VisualBasic.Logging;
using MvCamCtrl.NET;
using ReaLTaiizor.Forms;
using Sight.communicate;
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
using System.Windows.Input;

namespace Sight
{
    public partial class Form1 : MaterialForm
    {
        //#region
        ///// <summary>*****************************
        /////海康相机类
        ///// </summary>****************************
        //MyCamera.MV_CC_DEVICE_INFO_LIST m_stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
        //bool m_bGrabbing = false;
        //Thread m_hReceiveThread = null;
        //MyCamera.MV_FRAME_OUT_INFO_EX m_stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
        //// ch:用于从驱动获取图像的缓存 | en:Buffer for getting image from driver
        //UInt32 m_nBufSizeForDriver = 0;
        //IntPtr m_BufForDriver = IntPtr.Zero;
        //private static Object BufForDriverLock = new Object();
        //private MyCamera m_MyCamera = new MyCamera();
        ///// <summary>*****************************
        ///// 海康相机方法
        ///// </summary>****************************

        //#endregion
        //相机名称
        String camerakind = null;
        bool grablock = true;
        Icommun icommun;
        //bitmap(测试用)
        //Bitmap bitmap_t = null; 

        // 声明INI文件的写操作函数 WritePrivateProfileString()
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool WritePrivateProfileString(string section, string key, string value, string filePath);

        // 声明INI文件的读操作函数 GetPrivateProfileString()
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);

        //halcon相关
        public HObject CurrImage;
        #region 属性
        /// <summary>
        /// 获取Hwindow控件
        /// </summary>
        /// <returns></returns>
        public HWindow_Final Get_hWindow_Final_ShowCameraImg()
        {
            return hWindow_Final1_grab;
        }

        //public HWindow_Final Get_hWindow_CameraImg()
        //{
        //    return hWindow_Final_CameraImg;
        //}

        //public HWindow_Final Get_hWindow_Final_NGImg()
        //{
        //    return hWindow_Final_NGImg;
        //}

        //public RichTextBox Get_RichTextBoxSev()
        //{
        //    return RTX_receivesev;
        //}

        #endregion
        public Form1()
        {
            //读取ini
            StringBuilder lpReturnedString = new StringBuilder(200);
            string INIStr = Directory.GetCurrentDirectory() + "\\Cogfig.ini";
            int p = GetPrivateProfileString("Cameratype", "type", "null", lpReturnedString, 10,INIStr);
            camerakind = lpReturnedString.ToString();
            InitializeComponent();
            
            List<string> cameranum = cameraserve.Instance.Get_DeviceList();
            try
            {
                cbcamera.Text = cameranum[0];
            }
            catch
            {
                MessageBox.Show("未找到相机");
            }
            //cbcamera.Text = cameranum[0];
            foreach (string item in cameranum)
            {
                 cbcamera.Items.Add(item);
            }


            //将通讯选项置为Tcp
            communcombo.SelectedItem = communcombo.Items[3];
            //设置初始服务器ip和端口(无论用不用)（后续改为ini或其他）
            txt_ipsev.Text = "127.0.0.5";
            txt_portsev.Text = "3000";

            //设置初始客户端ip和端口(无论用不用)
            icommun = FCommunicate.icommuns(communcombo.Text.Trim());
            icommun.OnDataReceived += data => UpdateLog(data);
            icommun.OnStatusChanged += status => UpdateLog($"[状态] {status}");
            try
            {
                if (icommun.openport(txt_ipsev.Text, txt_portsev.Text))
                {
                    MessageBox.Show("通讯连接成功");
                    
                }
            }
            catch
            {
                MessageBox.Show("通讯连接失败");
            }
 
        }
        // 线程安全的日志更新
        private void UpdateLog(string data)
        {
            if (RTX_socketreceive.InvokeRequired)
            {
                RTX_socketreceive.Invoke(new Action<string>(UpdateLog), data);
            }
            else
            {
                //RTX_receivesev.AppendText($"{DateTime.Now:HH:mm:ss} - {data}\n");
                RTX_socketreceive.AppendText(data);
                RTX_socketreceive.SelectionStart = RTX_socketreceive.Text.Length;
                RTX_socketreceive.ScrollToCaret();
            }
        }

        

        private void tabPage1_Click(object sender, EventArgs e)
        {
            
        }


        private void Searchcamera_Click(object sender, EventArgs e)
        {

            List<string> cameranum = cameraserve.Instance.Get_DeviceList();
            try
            {
                cbcamera.Text = cameranum[0];
            }
            catch 
            {
                MessageBox.Show("未找到相机");
            }
            foreach (string item in cameranum)
            {
                cbcamera.Items.Add(item);
            }

        }

        private void camopen_Click(object sender, EventArgs e)
        {


            if (cameraserve.Instance.OpenDevice(cbcamera.Text))
            {
                MessageBox.Show("打开相机成功！");
            }

        }

        private void camclose_Click(object sender, EventArgs e)
        {
            cameraserve.Instance.CloseDevice();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            camclose_Click(sender, e);
            icommun?.closeport();
        }

        private void Continuegarb_Click(object sender, EventArgs e)
        {
            cameraserve.Instance.SetTriggerMode(TriggerMode.Continues, TriggerSource.Software);
            cameraserve.Instance.StartGrab();
            grablock = true;

            // 面对问题，图像数据在另一个类，还另一个线程里


            //【1】声明委托(方法的原型)
            //【2】根据委托编写具体方法
            //【3】定义委托变量
            //【4】将具体方法和委托变量关联
            //【5】使用委托变量


            // 1 声明委托(方法的原型)
            // 2 定义委托变量
            // 3 使用委托变量（数据变量产生的位置）
            // 4 根据委托编写具体方法（参数要传递到的位置）
            // 5  将具体方法和委托变量关联
        }

        private void Stopgarb_Click(object sender, EventArgs e)
        {
            cameraserve.Instance.StopGrab();
        }

        private void Signalgarb_Click(object sender, EventArgs e)
        {
            if (grablock)
            {
                cameraserve.Instance.SetTriggerMode(TriggerMode.Trigger, TriggerSource.Software);
                cameraserve.Instance.StartGrab();
                grablock = false;
            }
            cameraserve.Instance.SnapImage();
        }

        private void savebmp_Click(object sender, EventArgs e)
        {

        }

        private void savejpg_Click(object sender, EventArgs e)
        {

        }

        private void readin_Click(object sender, EventArgs e)
        {

        }

        private void writein_Click(object sender, EventArgs e)
        {

        }

        private void bt_sendcus_Click(object sender, EventArgs e)
        {
            icommun.sendmessage(RTX_socketsend.Text.Trim());
        }
        /// <summary>
        /// SocketTcp连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_comfirmsev_Click(object sender, EventArgs e)
        {
            icommun = FCommunicate.icommuns(communcombo.Text.Trim());
            icommun.OnDataReceived += data => UpdateLog(data);
            icommun.openport(txt_ipsev.Text.Trim(), txt_portsev.Text.Trim());

        }
        /// <summary>
        /// SocketTcp断开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            icommun.closeport();
        }

        private void btn_modbustcpoff_Click(object sender, EventArgs e)
        {
            icommun.closeport();
        }

        private void btn_modbustcpon_Click(object sender, EventArgs e)
        {
            icommun = FCommunicate.icommuns(communcombo.Text.Trim());
            icommun.openport(txt_modbusip.Text.Trim(), txt_modbusport.Text.Trim());
        }

        private void btn_modbustcpsend_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取输入参数
                byte slaveId = byte.Parse(txt_slaveid.Text);
                ushort startAddress = ushort.Parse(txt_startaddress.Text);
                string function = comboBox1.Text;
                string data = txt_modbustcpwrite.Text.Trim();

                // 根据选择的功能执行写入操作
                switch (function)
                {
                    case "写单个线圈":
                        bool coilValue = bool.Parse(data);
                        icommun.WriteSingleCoil(slaveId, startAddress, coilValue);
                        txt_modbustcpwrite.AppendText($"写入线圈成功: {coilValue}\n");
                        break;

                    case "写多个线圈":
                        string[] coilValues = data.Split(',');
                        bool[] coilData = coilValues.Select(v => bool.Parse(v.Trim())).ToArray();
                        icommun.WriteMultipleCoils(slaveId, startAddress, coilData);
                        txt_modbustcpwrite.AppendText($"写入多个线圈成功: {data}\n");
                        break;

                    case "写单个寄存器":
                        ushort regValue = ushort.Parse(data);
                        icommun.WriteSingleRegister(slaveId, startAddress, regValue);
                        txt_modbustcpwrite.AppendText($"写入寄存器成功: {regValue}\n");
                        break;

                    case "写多个寄存器":
                        string[] regValues = data.Split(',');
                        ushort[] regData = regValues.Select(v => ushort.Parse(v.Trim())).ToArray();
                        icommun.WriteMultipleRegisters(slaveId, startAddress, regData);
                        txt_modbustcpwrite.AppendText($"写入多个寄存器成功: {data}\n");
                        break;

                    default:
                        MessageBox.Show("请选择有效的写入功能");
                        break;
                }
            }
            catch (Exception ex)
            {
                txt_modbustcpwrite.AppendText($"写入错误: {ex.Message}\n");
            }
        }

        private void btn_modbustcpread_Click(object sender, EventArgs e)
        {
            //if (icommun == null || !((ModbusTcpmaster)icommun).IsConnected)
            //{
            //    MessageBox.Show("请先连接Modbus主站");
            //    return;
            //}
            try
            {
                // 获取输入参数
                byte slaveId = byte.Parse(txt_slaveid.Text);
                ushort startAddress = ushort.Parse(txt_startaddress.Text);
                ushort numberOfPoints = ushort.Parse(txt_number.Text);
                string function = comboBox1.Text;

                // 根据选择的功能执行读取操作
                switch (function)
                {
                    case "读取线圈":
                        bool[] coils = icommun.ReadCoils(slaveId, startAddress, numberOfPoints);
                        txt_modbustcpread.AppendText($"线圈状态: {string.Join(", ", coils)}\n");
                        break;

                    case "读取离散输入":
                        bool[] inputs = icommun.ReadInputs(slaveId, startAddress, numberOfPoints);
                        txt_modbustcpread.AppendText($"离散输入状态: {string.Join(", ", inputs)}\n");
                        break;

                    case "读取保持寄存器":
                        ushort[] holdingRegs = icommun.ReadHoldingRegisters(slaveId, startAddress, numberOfPoints);
                        txt_modbustcpread.AppendText($"保持寄存器值: {string.Join(", ", holdingRegs)}\n");
                        break;

                    case "读取输入寄存器":
                        ushort[] inputRegs = icommun.ReadInputRegisters(slaveId, startAddress, numberOfPoints);
                        txt_modbustcpread.AppendText($"输入寄存器值: {string.Join(", ", inputRegs)}\n");
                        break;

                    default:
                        MessageBox.Show("请选择有效的读取功能");
                        break;
                }
            }
            catch (Exception ex)
            {
                txt_modbustcpread.AppendText($"读取错误: {ex.Message}\n");
            }
        }
    }
}
