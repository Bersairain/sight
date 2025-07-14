using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sight.communicate
{
    public interface Icommun
    {
        /// <summary>
        /// 该重载暂未启用
        /// </summary>
        /// <returns></returns>
        bool openport();
        /// <summary>
        /// 用于SOCKET，Modbus主站
        /// </summary>
        /// <param name="st1"></param>
        /// <param name="st2"></param>
        /// <returns></returns>  
        bool openport(string st1,string st2);
        void closeport();

        // 添加连接状态属性
        bool IsConnected { get; }
        /// <summary>
        /// 用于Socket发送
        /// </summary>
        /// <param name="mes"></param>
        void sendmessage(string mes);

        // 添加数据接收事件
        event Action<string> OnDataReceived;
        // 添加状态通知事件
        event Action<string> OnStatusChanged;
        //void getmessage();


        //modbus通讯专用
        ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        bool[] ReadInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints);

        void WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort value);
        void WriteSingleCoil(byte slaveAddress, ushort coilAddress, bool value);
        void WriteMultipleRegisters(byte slaveAddress, ushort startAddress, ushort[] data);
        void WriteMultipleCoils(byte slaveAddress, ushort startAddress, bool[] data);
    }
}
