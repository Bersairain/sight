using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sight.xml
{
    internal class dataconvet
    {
        public mode ByteOrder { get; set; } = mode.BigEndian;

        // 添加枚举定义字节顺序
        public enum mode
        {
            BigEndian,          // ABCD (默认)
            LittleEndian,       // DCBA
            BigEndianByteSwap,  // BADC
            LittleEndianByteSwap // CDAB
        }

        public byte[] RegistersToBytes(ushort[] registers, int byteCount)
        {
            byte[] bytes = new byte[byteCount];

            for (int i = 0; i < registers.Length; i++)
            {
                byte[] registerBytes = BitConverter.GetBytes(registers[i]);

                switch (ByteOrder)
                {
                    case mode.BigEndian:
                        bytes[i * 2] = registerBytes[0];
                        bytes[i * 2 + 1] = registerBytes[1];
                        break;
                    case mode.LittleEndian:
                        bytes[(registers.Length - i - 1) * 2] = registerBytes[0];
                        bytes[(registers.Length - i - 1) + 1] = registerBytes[1];
                        break;
                    case mode.BigEndianByteSwap:
                        bytes[i * 2] = registerBytes[1];
                        bytes[i * 2 + 1] = registerBytes[0];
                        break;
                    case mode.LittleEndianByteSwap:
                        bytes[(registers.Length - i - 1) * 2] = registerBytes[1];
                        bytes[(registers.Length - i - 1) * 2 + 1] = registerBytes[0];
                        break;
                    default:
                        break;
                }
            }

            return bytes;
        }

        public ushort[] BytesToRegisters(byte[] bytes, int registerCount)
        {
            ushort[] registers = new ushort[registerCount];

            for (int i = 0; i < registerCount; i++)
            {
                byte b1, b2;

                switch (ByteOrder)
                {
                    case mode.BigEndian:
                        b1 = bytes[i * 2];
                        b2 = bytes[i * 2 + 1];
                        break;
                    case mode.LittleEndian:
                        b1 = bytes[i * 2 + 1];
                        b2 = bytes[i * 2];
                        break;
                    case mode.BigEndianByteSwap:
                            b1 = bytes[(registerCount - i - 1) * 2 ];
                            b2 = bytes[(registerCount - i - 1) * 2 + 1];

                        break;
                    case mode.LittleEndianByteSwap:
                        
                            b1 = bytes[(registerCount - i - 1) * 2 + 1];
                            b2 = bytes[(registerCount - i - 1) * 2];

                        break;
                    default:
                        b1 = bytes[i * 2];
                        b2 = bytes[i * 2 + 1];
                        break;
                }

                registers[i] = (ushort)((b1 << 8) | b2);
            }

            return registers;
        }

    }
}
