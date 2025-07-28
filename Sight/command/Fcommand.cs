using Sight.communicate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sight.command
{
    public static class Fcommand
    {
        public static Icommand icommand(string communmode)
        {
            if (communmode == "blob")
            {
                //return new TcpSocketSev();
            }
            else if (communmode == "keymatch")
            {
                return new KeyMatch();
            }
            else if (communmode == "TCP客户端")
            {
                //return new TcpSocketCus();
            }
            else if (communmode == "ModbusTcp从站")
            {
                //return new ModbusTcpsalve();
            }
            else
            {
                return null;
            }
            return null;
        }

    }
}
