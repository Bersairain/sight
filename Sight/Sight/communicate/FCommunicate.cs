using Sight.camera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sight.communicate
{
    public static class FCommunicate
    {
        public static Icommun icommuns(string communmode)
        {
            if (communmode == "TCP服务器")
            {
                return new TcpSocketSev();
            }
            else if (communmode == "ModbusTcp主站")
            {
                return new ModbusTcpmaster();
            }
            else if (communmode == "TCP客户端")
            {
                return new TcpSocketCus();
            }
            else if (communmode == "ModbusTcp从站")
            {
                return new ModbusTcpsalve();
            }
            else
            {
                return null;
            }

        }







    }
}
