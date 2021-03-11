using System;

namespace _3._5.ChainofResponsibilityPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    /*
     责任链模式

     责任链模式是一种行为设计模式， 允许你将请求沿着处理者链进行发送。 收到请求后， 每个处理者均可对请求进行处理， 或将其传递给链上的下个处理者。
     */

    /// <summary>
    /// Handler接口声明了一种用于构建链的方法处理程序。 它还声明了一种用于执行请求的方法。
    /// </summary>
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        object Handle(object request);
    }

    /// <summary>
    /// 可以在基本处理程序类中实现默认的链接行为。
    /// </summary>
    public abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public object Handle(object request)
        {
            if (_nextHandler == null)
                return null;

            return _nextHandler.Handle(request);
        }

        public IHandler SetNext(IHandler handler)
        {
            return this._nextHandler = handler;
        }
    }

    public class MonkeyHandler : AbstractHandler
    {

    }
}
