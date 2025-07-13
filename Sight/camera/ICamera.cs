using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sight.camera
{
    //接口类尝试(用于不同相机连接)<暂未实现>
    internal interface ICamera
    {
        //搜索相机
        List<string> GetDeviceList();

        //打开相机
        bool opendevice();

        //关闭相机
        bool closedevice();
        //开始采集
        bool StartGrab();
        //停止采集
        bool StopGrab();
        //模式设置
        bool SetMode();
        

    }
}
