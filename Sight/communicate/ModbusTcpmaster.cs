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
        public Socket socketcus;
        public IModbusMaster modbusMaster;

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
                socketcus = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // 2：设置IP和端口。
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(port));
                try
                {
                    socketcus.Connect(ipe);
                    var factory = new ModbusFactory();
                    modbusMaster = factory.CreateMaster(socketcus);

                    OnStatusChanged?.Invoke($"已连接到服务器 {ip}:{port}");
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
