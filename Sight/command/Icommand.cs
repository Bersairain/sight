using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconControl;
using HalconDotNet;

namespace Sight.command
{
    public interface Icommand
    {
        void setpara(HWindow_Final hWindow_Final);
        void start(HWindow_Final hWindow_Final, HObject CurrImage,string mode);
        void match(HWindow_Final hWindow_final);
    }
}
