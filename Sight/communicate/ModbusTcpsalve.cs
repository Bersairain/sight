using Microsoft.VisualBasic.Devices;
using NModbus;
using NModbus.Device;
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
    public class ModbusTcpsalve : Icommun
    {
        public TcpListener listener;
        //public IModbusSlave slave;
        public Thread _serverThread;
        public IModbusSlaveNetwork network;
        public bool IsRunning = false;
        private CancellationTokenSource _cancellationTokenSource;

        public event Action<string> OnDataReceived;
        public event Action<string> OnStatusChanged;

        public void closeport()
        {
            try
            {
                // 1. 取消异步监听
                _cancellationTokenSource?.Cancel();

                // 2. 停止监听线程
                if (_serverThread != null && _serverThread.IsAlive)
                {
                    // 安全终止线程
                    if (!_serverThread.Join(500)) // 等待500ms
                    {
                        _serverThread.Interrupt();
                    }
                }

                // 3. 停止网络和监听器
                network?.RemoveSlave(1);
                listener?.Stop();

                // 4. 释放资源
                _cancellationTokenSource?.Dispose();
                

                IsRunning = false;
                OnStatusChanged?.Invoke("从站已停止");
            }
            catch (Exception ex)
            {
                OnStatusChanged?.Invoke($"关闭从站时出错: {ex.Message}");
            }
        }

        public bool openport()
        {
            MessageBox.Show("用错实例");
            return false;
        }

        public bool openport(string ip, string port)
        {
            if (IsRunning)
            {
                closeport(); // 先关闭现有连接
            }

            try
            {
                _cancellationTokenSource = new CancellationTokenSource();

                _serverThread = new Thread(() =>
                {
                    try
                    {
                        // 创建TCP监听器
                        listener = new TcpListener(IPAddress.Parse(ip), Convert.ToInt32(port));
                        listener.Start();

                        // 创建Modbus工厂和网络
                        IModbusFactory factory = new ModbusFactory();
                        network = factory.CreateSlaveNetwork(listener);

                        // 创建从站实例
                        IModbusSlave slave = factory.CreateSlave(1);
                        network.AddSlave(slave);

                        IsRunning = true;
                        OnStatusChanged?.Invoke($"从站已启动 {ip}:{port}");

                        // 开始异步监听（使用取消令牌）
                        network.ListenAsync(_cancellationTokenSource.Token).GetAwaiter().GetResult();
                    }
                    catch (OperationCanceledException)
                    {
                        // 正常取消操作，不报错
                        OnStatusChanged?.Invoke("监听已取消");
                    }
                    catch (Exception exp)
                    {
                        IsRunning = false;
                        OnStatusChanged?.Invoke($"从站启动失败: {exp.Message}");
                    }
                })
                {
                    IsBackground = true,
                    Name = "ModbusSlaveThread"
                };

                _serverThread.Start();
                return true;
            }
            catch (Exception exp)
            {
                IsRunning = false;
                OnStatusChanged?.Invoke($"从站初始化失败: {exp.Message}");
                return false;
            }
        }

        public bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        public ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        public bool[] ReadInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        public void sendmessage(string mes)
        {
            // 从站不需要主动发送消息
            OnStatusChanged?.Invoke("警告：从站不能主动发送消息");
        }

        public void WriteMultipleCoils(byte slaveAddress, ushort startAddress, bool[] data)
        {
            throw new NotImplementedException();
        }

        public void WriteMultipleRegisters(byte slaveAddress, ushort startAddress, ushort[] data)
        {
            throw new NotImplementedException();
        }

        public void WriteSingleCoil(byte slaveAddress, ushort coilAddress, bool value)
        {
            throw new NotImplementedException();
        }

        public void WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort value)
        {
            throw new NotImplementedException();
        }
    }
}
