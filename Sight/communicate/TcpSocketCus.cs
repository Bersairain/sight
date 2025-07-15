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
    public class TcpSocketCus : Icommun
    {
        public Socket socketcus;

        private Thread _receiveThread;
        private volatile bool _isRunning = false;

        public bool IsConnected => throw new NotImplementedException();

        public event Action<string> OnDataReceived;
        public event Action<string> OnStatusChanged;

        public void closeport()
        {
            try
            {
                socketcus.Close();
                //return true;
            }
            catch (Exception exp)
            {
                MessageBox.Show("客户端关闭失败:" + exp.Message);
                //return false;
            }
        }

        public bool openport()
        {
            MessageBox.Show("用错实例");
            return false;
        }

        public bool openport(string ip, string port)
        {
            try
            {
                // 1：创建socket()
                socketcus = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // 2：设置IP和端口。
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(port));
                try
                {
                    socketcus.Connect(ipe);
                    _isRunning = true;

                    // 启动接收线程
                    _receiveThread = new Thread(ReceiveMessages)
                    {
                        IsBackground = true
                    };
                    _receiveThread.Start();

                    OnStatusChanged?.Invoke($"已连接到服务器 {ip}:{port}");
                    return true;
                }
                catch(Exception exp)
                {
                    MessageBox.Show("客户端开启失败:" + exp.Message);
                    return false;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("客户端开启失败:" + exp.Message);
                return false;
            }
         }

        private void ReceiveMessages()
        {
            try
            {
                while (_isRunning && socketcus != null && socketcus.Connected)
                {
                    // 检查是否有数据可读
                    if (socketcus.Poll(1000, SelectMode.SelectRead))
                    {
                        byte[] buffer = new byte[1024 * 1024];
                        int bytesRead = socketcus.Receive(buffer);

                        if (bytesRead > 0)
                        {
                            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            OnDataReceived?.Invoke($"[接收] {message}");
                        }
                        else
                        {
                            // 服务器断开连接
                            //OnStatusChanged?.Invoke("服务器断开连接");
                            break;
                        }
                    }
                    else
                    {
                        // 超时，检查连接状态
                        if (!socketcus.Connected)
                        {
                            //OnStatusChanged?.Invoke("连接已断开");
                            break;
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                if (_isRunning) // 仅在连接应活动时报告错误
                {
                    OnStatusChanged?.Invoke($"接收错误: {ex.SocketErrorCode}");
                }
            }
            catch (ObjectDisposedException)
            {
                // Socket已被关闭，忽略
            }
            catch (Exception ex)
            {
                if (_isRunning)
                {
                    OnStatusChanged?.Invoke($"接收错误: {ex.Message}");
                }
            }
            finally
            {
                _isRunning = false;
            }
        }

        public void sendmessage(string mes)
        {
            byte[] sendMsg = Encoding.UTF8.GetBytes(mes);
            socketcus.Send(sendMsg);
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

        //public bool PingTest(string ip)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
