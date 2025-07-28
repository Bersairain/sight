using Microsoft.VisualBasic.Devices;
using NModbus;
using NModbus.Data;
using NModbus.Device;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
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
        public event EventHandler<DataChangedEventArgs> DataChanged;
        //private slavedata dataStore;
        private SlaveDataStore dataStore;
        private readonly string _dataFilePath = "ModbusSlaveData.json";
        private const ushort MAX_ADDRESS = 10; // 限制地址范围以提高效率
        //private ushort a = 1;
        private ushort address = 0;//起始地址
        private bool[] colivalue;//记录线圈
        private ushort[] registervalue;//记录寄存器
        public ModbusTcpsalve()
        {
            dataStore = new SlaveDataStore();
            //dataStore = new slavedata();
            //dataStore.DataChanged += (s, e) => DataChanged?.Invoke(this, e);
            LoadData();
        }
        public bool IsConnected => throw new NotImplementedException();

        public event Action<string> OnDataReceived;
        public event Action<string> OnStatusChanged;

        public void closeport()
        {
            try
            {
                SaveData();
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
        // 字节顺序转换辅助方法
     

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
                        IModbusSlave slave = factory.CreateSlave(1, dataStore);
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

        //public bool PingTest(string ip)
        //{
        //    throw new NotImplementedException();
        //}

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



        private void LoadData()
        {
            if (!File.Exists(_dataFilePath))
                return;

            try
            {
                string json = File.ReadAllText(_dataFilePath);
                var savedData = JsonSerializer.Deserialize<SavedData>(json);

                // 加载线圈数据
                foreach (var kvp in savedData.Coils)
                {
                    dataStore.CoilDiscretes.WritePoints(kvp.Key, new[] { kvp.Value });
                }

                // 加载保持寄存器数据
                foreach (var kvp in savedData.HoldingRegisters)
                {
                    dataStore.HoldingRegisters.WritePoints(kvp.Key, new[] { kvp.Value });
                }

                OnStatusChanged?.Invoke($"成功加载 {savedData.Coils.Count} 个线圈和 {savedData.HoldingRegisters.Count} 个寄存器的数据");
            }
            catch (Exception ex)
            {
                OnStatusChanged?.Invoke($"加载数据失败: {ex.Message}");
            }
        }

        // 保存数据
        private void SaveData()
        {
            try
            {
                var dataToSave = new SavedData();

                // 保存线圈数据
                dataToSave.Coils = new Dictionary<ushort, bool>();
                
                    try
                    {
                        // 使用ReadPoint方法读取值
                        colivalue = dataStore.CoilDiscretes.ReadPoints(address, MAX_ADDRESS);
                       
                    }
                    catch
                    {
                        // 忽略读取错误
                    }
                for (ushort a = 0; a < MAX_ADDRESS; a++)
                {
                    dataToSave.Coils[a] = colivalue[a];
                }

                // 保存保持寄存器数据
                dataToSave.HoldingRegisters = new Dictionary<ushort, ushort>();
               
                    try
                    {
                        // 使用ReadPoint方法读取值
                        registervalue = dataStore.HoldingRegisters.ReadPoints(address, MAX_ADDRESS);
                    }
                    catch
                    {
                        // 忽略读取错误
                    }
                for (ushort a = 0; a < MAX_ADDRESS; a++)
                {
                    dataToSave.HoldingRegisters[a] = registervalue[a];
                }

                string json = JsonSerializer.Serialize(dataToSave);
                File.WriteAllText(_dataFilePath, json);
                OnStatusChanged?.Invoke($"成功保存 {dataToSave.Coils.Count} 个线圈和 {dataToSave.HoldingRegisters.Count} 个寄存器的数据");
            }
            catch (Exception ex)
            {
                OnStatusChanged?.Invoke($"保存数据失败: {ex.Message}");
            }
        }

        private class SavedData
        {
            public Dictionary<ushort, bool> Coils { get; set; } = new Dictionary<ushort, bool>();
            public Dictionary<ushort, ushort> HoldingRegisters { get; set; } = new Dictionary<ushort, ushort>();
        }
    }


    public class SlaveDataStore : ISlaveDataStore
    {
        private readonly PointSource<ushort> holdingRegisters =
            new PointSource<ushort>();
        private readonly PointSource<bool> coils =
            new PointSource<bool>();

        public IPointSource<ushort> HoldingRegisters => holdingRegisters;
        public IPointSource<ushort> InputRegisters => holdingRegisters; // 简化处理
        public IPointSource<bool> CoilInputs => coils;
        public IPointSource<bool> CoilDiscretes => coils; // 简化处理

        // 可选：添加数据访问方法供其他模块使用
        public void UpdateHoldingRegister(ushort address, ushort value)
            => holdingRegisters.WritePoints(address, new[] { value });
        public void UpdateCoilInputs(ushort address, bool value)
            => coils.WritePoints(address, new[] { value });


        // 添加文件路径字段
        private readonly string _dataFilePath = "ModbusSlaveData.json";

        // 添加构造函数


        // 添加数据加载方法
        

    }


}

