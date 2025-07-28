using NModbus;
using NModbus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sight.communicate
{
    internal class slavedata : ISlaveDataStore
    {
        public event EventHandler<DataChangedEventArgs> DataChanged;

        private readonly DefaultSlaveDataStore _baseStore = new DefaultSlaveDataStore();

        public IPointSource<ushort> HoldingRegisters =>
            new ObservablePointSource<ushort>(
                _baseStore.HoldingRegisters,
                (addr, data) => RaiseDataChanged(ModbusDataType.HoldingRegister, addr, data));

        public IPointSource<ushort> InputRegisters =>
            new ObservablePointSource<ushort>(
                _baseStore.InputRegisters,
                (addr, data) => RaiseDataChanged(ModbusDataType.InputRegister, addr, data));

        public IPointSource<bool> CoilInputs =>
            new ObservablePointSource<bool>(
                _baseStore.CoilInputs,
                (addr, data) => RaiseDataChanged(ModbusDataType.Coil, addr, data));

        public IPointSource<bool> CoilDiscretes =>
            new ObservablePointSource<bool>(
                _baseStore.CoilDiscretes,
                (addr, data) => RaiseDataChanged(ModbusDataType.DiscreteInput, addr, data));

        private void RaiseDataChanged<T>(ModbusDataType dataType, ushort startAddress, T[] values)
        {
            DataChanged?.Invoke(this, new DataChangedEventArgs
            {
                DataType = dataType,
                StartAddress = startAddress,
                Values = values
            });
        }

        private class ObservablePointSource<T> : IPointSource<T>
        {
            private readonly IPointSource<T> _baseSource;
            private readonly Action<ushort, T[]> _onWrite;

            public ObservablePointSource(IPointSource<T> baseSource, Action<ushort, T[]> onWrite)
            {
                _baseSource = baseSource;
                _onWrite = onWrite;
            }

            public T[] ReadPoints(ushort startAddress, ushort numberOfPoints)
                => _baseSource.ReadPoints(startAddress, numberOfPoints);

            public void WritePoints(ushort startAddress, T[] points)
            {
                _baseSource.WritePoints(startAddress, points);
                _onWrite(startAddress, points);
            }
        }
        

        

        //public IPointSource<bool> CoilDiscretes => throw new NotImplementedException();

        //public IPointSource<bool> CoilInputs => throw new NotImplementedException();

        

    }
    

    public enum ModbusDataType
    {
        HoldingRegister,
        InputRegister,
        Coil,
        DiscreteInput
    }

    public class DataChangedEventArgs : EventArgs
    {
        public ModbusDataType DataType { get; set; }
        public ushort StartAddress { get; set; }
        public object Values { get; set; }
    }
}
