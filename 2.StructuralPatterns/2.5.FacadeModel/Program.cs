using System;

namespace _2._5.FacadeModel
{
    class Program
    {
        static void Main(string[] args)
        {
            Client.Test();

            Console.ReadKey();
        }
    }


    /*
      外观模式

     外观模式：外部与一个子系统的通信必须通过一个统一的外观对象进行，为子系统中的一组接口提供一个一致的界面，外观模式定义了一个高层接口，这个接口使得这一子系统更加容易使用。外观模式又称为门面模式。

    外观模式非常简单，体现的就是封装的思想。将多个子系统封装起来，提供一个更简洁的接口供外部调用。

    举个例子，比如我们每天打开电脑时，都需要做三件事：    
    打开浏览器
    打开IDE
    打开微信

    每天下班时，关机前需要做三件事：
    关闭浏览器
    关闭IDE
    关闭微信


    外观模式就是这么简单，它使得两种不同的类不用直接交互，而是通过一个中间件——也就是外观类——间接交互。外观类中只需要暴露简洁的接口，隐藏内部的细节，所以说白了就是封装的思想。

    外观模式非常常用，尤其是在第三方库的设计中，我们应该提供尽量简洁的接口供别人调用。另外，在 MVC 架构中，C 层（Controller）就可以看作是外观类，Model 和 View 层通过 Controller 交互，减少了耦合。
    */

    public class Browser
    {
        public void Open()
        {
            Console.WriteLine($"{ nameof(Browser)}.{nameof(Browser.Open)}");
        }

        public void Close()
        {
            Console.WriteLine($"{ nameof(Browser)}.{nameof(Browser.Close)}");
        }
    }

    public class IDE
    {
        public void Open()
        {
            Console.WriteLine($"{ nameof(IDE)}.{nameof(IDE.Open)}");
        }

        public void Close()
        {
            Console.WriteLine($"{ nameof(IDE)}.{nameof(IDE.Close)}");
        }
    }

    public class WebChat
    {
        public void Open()
        {
            Console.WriteLine($"{ nameof(WebChat)}.{nameof(WebChat.Open)}");
        }

        public void Close()
        {
            Console.WriteLine($"{ nameof(WebChat)}.{nameof(WebChat.Close)}");
        }
    }

    public class Facade
    {
        public void Open()
        {
            var browser = new Browser();
            browser.Open();

            var ide = new IDE();
            ide.Open();

            var webchat = new WebChat();
            webchat.Open();
        }

        public void Close()
        {
            var browser = new Browser();
            browser.Close();

            var ide = new IDE();
            ide.Close();

            var webchat = new WebChat();
            webchat.Close();
        }
    }

    public class Client
    {
        public static void Test()
        {

            Console.WriteLine("上班了：");
            var facade = new Facade();
            facade.Open();

            Console.WriteLine();
            Console.WriteLine("下班了：");
            facade.Close();
        }
    }
}
