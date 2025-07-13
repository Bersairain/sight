using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sight.communicate
{
    public class TcpSocketSev: Icommun
    {
        //1、创建socket
        public Socket socketsev;

        // 创建接受客户端的字典（就是成对放的数组），发送数据给客户端的时候要用
        public Dictionary<string, Socket> CurrentClientlist = new Dictionary<string, Socket>();

        /// <summary>
        /// 服务IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        // 添加运行标志
        private bool _isRunning = false;
        private readonly object _lock = new object();
        public event Action<string> OnDataReceived;
        public event Action<string> OnStatusChanged;
        public bool openport()
        {
            MessageBox.Show("用错实例");
            return false;
        }
        public bool openport(string ip,string port)
        {
            try
            {
                // 1：创建socket()
                socketsev = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // 2：设置IP和端口。
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(port));
                //var form1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
                //RichTextBox box1 = form1.Get_RichTextBoxSev();
                try
                {
                    // 3. 绑定ip和端口
                    socketsev.Bind(ipe);
                    //socketcustom.Connect(ipe);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("服务器开启失败：" + ex.Message);
                    return false;
                }
                // 4：listen()，确定能连接多少个客户端: 10 允许最多10个连接在队列中等待
                socketsev.Listen(10);
                _isRunning = true;
                // 5.创建一个监听的线程
                Task.Run(new Action(() =>
                {
                    AcceptClients();
                }));

                return true;
            }
            catch (Exception exp)
            {
                MessageBox.Show("服务器开启失败:" + exp.Message);
                return false;

            }
        }
        /// <summary>
        /// 监听客户端连接
        /// </summary>
        public void AcceptClients()
        {
            while (_isRunning)
            {
                try
                {
                    // 设置超时时间，避免Accept无限阻塞
                    // 这样我们可以定期检查_isRunning标志
                    if (socketsev.Poll(1000, SelectMode.SelectRead))
                    {
                        // 5：accept()函数接受客户端的连接
                        Socket socketClient = socketsev.Accept();

                        string client = socketClient.RemoteEndPoint.ToString();

                        // 将客户端保存起来
                        lock (_lock)
                        {
                            CurrentClientlist.Add(client, socketClient);
                        }

                        OnStatusChanged?.Invoke($"客户端连接: {client}");

                        // 6：接受数据
                        Thread receiveThread = new Thread(() => ReceiveMessage(socketClient))
                        {
                            IsBackground = true
                        };
                        receiveThread.Start();
                    }
                }
                catch (SocketException ex) when (ex.SocketErrorCode == SocketError.Interrupted)
                {
                    // 服务器被关闭时的正常退出
                    break;
                }
                catch (ObjectDisposedException)
                {
                    // Socket已被关闭
                    break;
                }
                catch (Exception ex)
                {
                    if (_isRunning) // 只在服务器运行时报告错误
                    {
                        OnStatusChanged?.Invoke($"接受客户端错误: {ex.Message}");
                    }
                    Thread.Sleep(1000); // 错误后稍作等待
                }
            }
        }

        /// <summary>
        /// 监听接收客户端数据
        /// </summary>
        /// <param name="socketClient"></param>
        private void ReceiveMessage(Socket socketClient)
        {
            while (_isRunning)
            {
                // 创建一个缓冲区
                byte[] buffer = new byte[1024 * 1024 * 10];
                // 数据长度
                int length = -1;
                string client = socketClient.RemoteEndPoint.ToString();
                try
                {
                    length = socketClient.Receive(buffer);
                }
                catch (Exception)
                {
                    MessageBox.Show(client + "下线了");
                    CurrentClientlist.Remove(client);
                    break;
                }
                if (length > 0)
                {
                    string msg = string.Empty;
                    // 以utf8的格式接受
                    msg = Encoding.UTF8.GetString(buffer, 0, length);

                    //MessageBox.Show("接受信息："+msg);

                    // 触发拍照（在上位机或者PLC发送这个通讯信息的时候，我们进行解析以后，进行拍照）
                    //cameraserve.Instance.SnapImage();
                    //OnDataReceived?.Invoke($"[TCP接收] {msg}");
                    OnDataReceived?.Invoke(msg);
                    // var form1 = Application.OpenForms.OfType<Form1>.FirstOrDefault();
                    // 显示接受内容。需要使用Invoke,跨线程，跨UI
                    //Invoke(new Action(() =>
                    //{
                    //    rtb_Receive_server.AppendText(msg + "\n");
                    //}));
                }
                else
                {
                    MessageBox.Show(client + "下线了");
                    CurrentClientlist.Remove(client);
                    break;
                }
            }
        }


        /// <summary>
        /// 发送数据utf8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void sendmessage(string Content)
        {
            // 获取信息
            byte[] sendMsg = Encoding.UTF8.GetBytes(Content);

            // 对客户端发送信息
            foreach (var item in this.CurrentClientlist)
            {
                // 发送数据
                item.Value?.Send(sendMsg);
            }

        }


        public void closeport()
        {
            lock (_lock)
            {
                if (!_isRunning) return; // 已经关闭

                _isRunning = false; // 设置停止标志

                try
                {
                    // 关闭所有客户端连接
                    foreach (var client in CurrentClientlist.Values)
                    {
                        try
                        {
                            client.Shutdown(SocketShutdown.Both);
                            client.Close();
                        }
                        catch { }
                    }
                    CurrentClientlist.Clear();

                    // 关闭服务器Socket
                    socketsev?.Close();

                    OnStatusChanged?.Invoke("服务器已关闭");
                }
                catch (Exception exp)
                {
                    OnStatusChanged?.Invoke($"服务器关闭失败: {exp.Message}");
                }
            }
        }

        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        public ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        public bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        public bool[] ReadInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        public void WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort value)
        {
            throw new NotImplementedException();
        }

        public void WriteSingleCoil(byte slaveAddress, ushort coilAddress, bool value)
        {
            throw new NotImplementedException();
        }

        public void WriteMultipleRegisters(byte slaveAddress, ushort startAddress, ushort[] data)
        {
            throw new NotImplementedException();
        }

        public void WriteMultipleCoils(byte slaveAddress, ushort startAddress, bool[] data)
        {
            throw new NotImplementedException();
        }
    }
}
