using System;

namespace _2._1.AdapterModel
{
    class Program
    {
        static void Main(string[] args)
        {

            var homeVoltage = new HomeVoltage(220);
            var usbLine = new UsbLine();

            var result= PhoneAdapter.ChangeVoltage(homeVoltage, usbLine);

            Console.WriteLine($"手机电源适配器可正常冲电: {result== usbLine.Voltage}");


             homeVoltage = new HomeVoltage(80);
            result = PhoneAdapter.ChangeVoltage(homeVoltage, usbLine);

            Console.WriteLine($"手机电源适配器可正常冲电: {result == usbLine.Voltage}");

            Console.ReadKey();
        }
    }


    /*
     适配器模式

    电源适配器，也就是手机的充电头。它就是适配器模式的一个应用。

    试想一下，你有一条连接电脑和手机的 USB 数据线，连接电脑的一端从电脑接口处接收 5V 的电压，连接手机的一端向手机输出 5V 的电压，并且他们工作良好。

     中国的家用电压都是 220V，所以 USB 数据线不能直接拿来给手机充电，这时候我们有两种方案：
        1.单独制作手机充电器，接收 220V 家用电压，输出 5V 电压。
        2.添加一个适配器，将 220V 家庭电压转化为类似电脑接口的 5V 电压，再连接数据线给手机充电。


    以前的手机厂商采用的就是第一种方案：早期的手机充电器都是单独制作的，充电头和充电线是连在一起的。
    现在的手机都采用了电源适配器加数据线的方案。


    开发中经常会使用到各种各样的 Adapter，都属于适配器模式的应用。

    但适配器模式并不推荐多用。因为未雨绸缪好过亡羊补牢，如果事先能预防接口不同的问题，不匹配问题就不会发生，只有遇到源接口无法改变时，才应该考虑使用适配器。
    比如现代的电源插口中很多已经增加了专门的充电接口，让我们不需要再使用适配器转换接口，这又是社会的一个进步。
     */
    public class HomeVoltage
    {
        public int Voltage { get; private set; }
        public HomeVoltage(int voltage)
        {
            this.Voltage = voltage;
        }
    }

    public class PhoneAdapter
    {
        public static int ChangeVoltage(HomeVoltage homeVoltage, UsbLine usbLine)
        {
            if (homeVoltage == null || usbLine == null)
            {
                throw new ArgumentNullException("输入参数为空");
            }

            if (homeVoltage.Voltage >= 100 && homeVoltage.Voltage <= 220)
            {
                return usbLine.Voltage;
            }
            return -1;
        }
    }

    /// <summary>
    /// USB数据线只支持5V电压
    /// </summary>
    public class UsbLine
    {
        public int Voltage { get; } = 5;
    }
}
