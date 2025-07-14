using NModbus;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Sight.communicate
{

    public class ModbusTcpmaster : Icommun
    {
        //用于创建主站的客户端
        public TcpClient socketcus;
        public IModbusMaster modbusMaster;
        ModbusFactory factory;
        // 添加连接状态属性
        //public bool IsConnected => socketcus?.Connected ?? false;
        // 添加心跳定时器
        private System.Threading.Timer keepAliveTimer;

        //Socket显示用
        public event Action<string> OnDataReceived;
        public event Action<string> OnStatusChanged;

        public void closeport()
        {
            // 释放主站资源
            modbusMaster?.Dispose();
            // 关闭TCP连接
            socketcus?.Close();
        }

        public bool openport()
        {
            throw new NotImplementedException();
        }

        public bool openport(string ip, string port)
        {
            try
            {
                // 1：创建socket()
                //socketcus = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // 2：设置IP和端口。
                //IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(port));
                try
                {
                    // 先关闭现有连接
                    closeport();

                    // 创建新的TCP连接
                    socketcus = new TcpClient();

                    // 设置连接超时
                    var connectResult = socketcus.BeginConnect(ip, Convert.ToInt32(port), null, null);
                    if (!connectResult.AsyncWaitHandle.WaitOne(3000)) // 3秒连接超时
                    {
                        OnStatusChanged?.Invoke("连接超时");
                        return false;
                    }

                    socketcus.EndConnect(connectResult);

                    // 设置发送和接收超时
                    socketcus.SendTimeout = 3000;
                    socketcus.ReceiveTimeout = 3000;
                    //socketcus.Connect(ipe);
                    factory = new ModbusFactory();
                        modbusMaster = factory.CreateMaster(socketcus);

                        OnStatusChanged?.Invoke($"已连接到服务器 {ip}:{port}");
                        // 添加心跳机制（每5秒发送一次）
                        //keepAliveTimer = new System.Threading.Timer(_ =>
                        //{
                        //    if (IsConnected)
                        //    {
                        //        try
                        //        {
                        //            // 读取0号寄存器（无实际操作，仅保持连接）
                        //            modbusMaster.ReadHoldingRegisters(0, 0, 1);
                        //        }
                        //        catch
                        //        {
                        //            // 心跳失败时关闭连接
                        //            closeport();
                        //            OnStatusChanged?.Invoke("心跳检测失败，连接已关闭");
                        //        }
                        //    }
                        //}, null, 5000, 5000);
                    
                    return true;
                }
                catch (Exception exp)
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

        // 实现Modbus读写功能
        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return modbusMaster.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
        }

        public ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return modbusMaster.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
        }

        public bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return modbusMaster.ReadCoils(slaveAddress, startAddress, numberOfPoints);
        }

        public bool[] ReadInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return modbusMaster.ReadInputs(slaveAddress, startAddress, numberOfPoints);
        }

        public void WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort value)
        {
            modbusMaster.WriteSingleRegister(slaveAddress, registerAddress, value);
        }

        public void WriteSingleCoil(byte slaveAddress, ushort coilAddress, bool value)
        {
            modbusMaster.WriteSingleCoil(slaveAddress, coilAddress, value);
        }

        public void WriteMultipleRegisters(byte slaveAddress, ushort startAddress, ushort[] data)
        {
            modbusMaster.WriteMultipleRegisters(slaveAddress, startAddress, data);
        }

        public void WriteMultipleCoils(byte slaveAddress, ushort startAddress, bool[] data)
        {
            modbusMaster.WriteMultipleCoils(slaveAddress, startAddress, data);
        }

        public void sendmessage(string mes)
        {
            throw new NotImplementedException();
        }
    }
}
