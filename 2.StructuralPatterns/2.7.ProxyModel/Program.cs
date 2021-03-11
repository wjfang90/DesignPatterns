using System;
using System.Linq;

namespace _2._7.ProxyModel
{
    class Program
    {
        static void Main(string[] args)
        {

            Client.Test();
            Client.TestDynamicProxy();

            Console.ReadKey();
        }
    }

    /*
     代理模式

    代理模式：给某一个对象提供一个代理，并由代理对象控制对原对象的引用。

    这就是代理模式的一个应用，除了打印日志，它还可以用来做权限管理。读者看到这里可能已经发现了，这个代理类看起来和装饰模式一模一样，但两者的目的不同，装饰模式是为了增强功能或添加功能，代理模式主要是为了加以控制。

    在实际工作中，我们可能会遇到这样的需求：在网络请求前后，分别打印将要发送的数据和接收到数据作为日志信息。此时我们就可以新建一个网络请求的代理类，让它代为处理网络请求，并在代理类中打印这些日志信息。
     */

    #region 代理模式-静态代理
    public interface IHttp
    {
        void Request(string sendData);
        void OnSuccess(string receiveData);
    }

    public class HttpUtil : IHttp
    {
        public void Request(string sendData)
        {
            Console.WriteLine($"{nameof(HttpUtil)}.{nameof(HttpUtil.Request)}({sendData})");
        }

        public void OnSuccess(string receiveData)
        {
            Console.WriteLine($"{nameof(HttpUtil)}.{nameof(HttpUtil.OnSuccess)}({receiveData})");
        }
    }

    public class HttpProxy : IHttp
    {
        private HttpUtil httpUtil;
        public HttpProxy(HttpUtil httpUtil)
        {
            this.httpUtil = httpUtil;
        }

        public void Request(string sendData)
        {
            Console.WriteLine($"{nameof(HttpProxy)}.{nameof(HttpProxy.Request)}({sendData})");
        }

        public void OnSuccess(string receiveData)
        {
            Console.WriteLine($"{nameof(HttpProxy)}.{nameof(HttpProxy.OnSuccess)}({receiveData})");
        }
    }

    public class Client
    {
        public static void Test()
        {
            var httpUtil = new HttpUtil();
            var httpProxy = new HttpProxy(httpUtil);

            Console.WriteLine("代理模式-静态代理");

            httpProxy.Request("发送一个表情");
            httpProxy.OnSuccess("接收一个狗头");
        }

        public static void TestDynamicProxy()
        {
            var httpUtil = new HttpUtil();
            var httpProxy = new HttpDynamicProxy(httpUtil);

            Console.WriteLine();
            Console.WriteLine("代理模式-动态代理");

            httpProxy.Invoke(nameof(httpUtil.Request),new object[] { "发送一个表情" });
            httpProxy.Invoke(nameof(httpUtil.OnSuccess), new object[] { "接收一个狗头" });
        }
    }

    #endregion

    #region 代理模式 - 动态代理

    public class HttpDynamicProxy
    {
        private HttpUtil httpUtil;
        public HttpDynamicProxy(HttpUtil httpUtil)
        {
            this.httpUtil = httpUtil;
        }

        public object Invoke(string methodName,object[] args)
        {
            var methodInfo= httpUtil.GetType().GetMethod(methodName);
            if (methodInfo == null)
            {
                return null;
            }

            Console.WriteLine($"{nameof(HttpDynamicProxy)}.{nameof(HttpDynamicProxy.Invoke)}({methodName}_{args?.FirstOrDefault()?.ToString()})");

            return methodInfo.Invoke(httpUtil, args);
        }
    }

    #endregion

}
