using System;

namespace _2._1.SignletonHungry
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    /*

    单例模式非常常见，某个对象全局只需要一个实例时，就可以使用单例模式。
    它的优点也显而易见：
        它能够避免对象重复创建，节约空间并提升效率
        避免由于操作不同实例导致的逻辑错误

    单例模式有两种实现方式：饿汉式和懒汉式。

    懒加载方式在平时非常常见，比如打开我们常用的美团、饿了么、支付宝 app，应用首页会立刻刷新出来，但其他标签页在我们点击到时才会刷新。这样就减少了流量消耗，并缩短了程序启动时间。再比如游戏中的某些模块，当我们点击到时才会去下载资源，而不是事先将所有资源都先下载下来，这也属于懒加载方式，避免了内存浪费。

    但懒汉式的缺点就是将程序加载时间从启动时延后到了运行时，虽然启动时间缩短了，但我们浏览页面时就会看到数据的 loading 过程。如果用饿汉式将页面提前加载好，我们浏览时就会特别的顺畅，也不失为一个好的用户体验。比如我们常用的 QQ、微信 app，作为即时通讯的工具软件，它们会在启动时立即刷新所有的数据，保证用户看到最新最全的内容。著名的软件大师 Martin 在《代码整洁之道》一书中也说到：不提倡使用懒加载方式，因为程序应该将构建与使用分离，达到解耦。饿汉式在声明时直接初始化变量的方式也更直观易懂。所以在使用饿汉式还是懒汉式时，需要权衡利弊。

    一般的建议是：对于构建不复杂，加载完成后会立即使用的单例对象，推荐使用饿汉式。对于构建过程耗时较长，并不是所有使用此类都会用到的单例对象，推荐使用懒汉式。
     */

    /// <summary>
    /// 单例模式-饥汉式
    /// 
    /// 可以看到，我们将构造方法定义为 private，这就保证了其他类无法实例化此类，必须通过 getInstance 方法才能获取到唯一的 instance 实例，非常直观。
    /// 但饿汉式有一个弊端，那就是即使这个单例不需要使用，它也会在类加载之后立即创建出来，占用一块内存，并增加类初始化时间。
    /// 就好比一个电工在修理灯泡时，先把所有工具拿出来，不管是不是所有的工具都用得上。就像一个饥不择食的饿汉，所以称之为饿汉式。
    /// </summary>
    public sealed class SignletonHungry
    {
        private static SignletonHungry instance = new SignletonHungry();

        private SignletonHungry()
        {
            
        }

        public static SignletonHungry GetInstance()
        {
            return instance;
        }
    }

    /// <summary>
    /// 单例模式-懒汉式-线程不安全
    /// 
    /// 我们先声明了一个初始值为 null 的 instance 变量，当需要使用时判断此变量是否已被初始化，没有初始化的话才 new 一个实例出来。就好比电工在修理灯泡时，开始比较偷懒，什么工具都不拿，当发现需要使用螺丝刀时，才把螺丝刀拿出来。当需要用钳子时，再把钳子拿出来。就像一个不到万不得已不会行动的懒汉，所以称之为懒汉式。
    /// 
    /// 懒汉式解决了饿汉式的弊端，好处是按需加载，避免了内存浪费，减少了类初始化时间。
    /// 
    /// 不是线程安全的。如果有多个线程同一时间调用 getInstance 方法，instance 变量可能会被实例化多次。为了保证线程安全，我们需要给判空过程加上锁：
    /// 
    /// 注意：不要使用本版本
    /// </summary>
    public sealed class SignletonLazyNotThreadSafe
    {
        private static SignletonLazyNotThreadSafe instance = null;

        private SignletonLazyNotThreadSafe()
        {
           
        }

        public static SignletonLazyNotThreadSafe GetInstance()
        {
            if (instance == null)
            {
                instance = new SignletonLazyNotThreadSafe();
            }
            return instance;
        }
    }

    /// <summary>
    /// 单例模式-懒汉式-线程安全-加锁方式
    /// 
    /// 这样就能保证多个线程调用 getInstance 时，一次最多只有一个线程能够执行判空并 new 出实例的操作，所以 instance 只会实例化一次。但这样的写法仍然有问题，当多个线程调用 getInstance 时，每次都需要执行 lock 同步化方法，这样会严重影响程序的执行效率。所以更好的做法是在同步化之前，再加上一层检查：
    /// 
    ///  注意：不要使用本版本
    /// </summary>
    public sealed class SignleLazySafeOneCheck
    {
        private static SignleLazySafeOneCheck instance = null;
        private static readonly object objLock = new object();
        private SignleLazySafeOneCheck()
        {

        }

        public static SignleLazySafeOneCheck GetInstance()
        {
            lock (objLock)
            {
                if (instance == null)
                {
                    instance = new SignleLazySafeOneCheck();
                }
                return instance;
            }
        }
    }


    /// <summary>
    /// 单例模式-懒汉式-线程安全-双检锁方式
    /// 
    /// 这样增加一种检查方式后，如果 instance 已经被实例化，则不会执行同步化操作，大大提升了程序效率。上面这种写法也就是我们平时较常用的双检锁方式实现的线程安全的单例模式。
    /// 
    /// 但这样的懒汉式单例仍然有一个问题，为了优化程序运行效率，可能会对我们的代码进行指令重排序，在一些特殊情况下会导致单例模式线程不安全，为了防止这个问题，更进一步的优化是给 instance 变量加上 volatile 关键字。
    /// 
    /// 不可靠的并且容易出错,如果在实现Singleton时出现双重检查锁定的要求，那么最好的方法就是使用System.Lazy
    /// 
    ///  注意：不要使用本版本
    /// </summary>
    public sealed class SignletonLazySafeDoubleCheck
    {
        private static SignletonLazySafeDoubleCheck instance = null;
        private static readonly object objLock = new object();
        private SignletonLazySafeDoubleCheck()
        {

        }

        public static SignletonLazySafeDoubleCheck GetInstance()
        {
            if (instance == null)
            {
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new SignletonLazySafeDoubleCheck();
                    }
                }
            }
            return instance;
        }
    }
  

    /// <summary>
    /// 单例模式-懒汉式-线程安全-不完全懒加载
    /// 
    /// not quite as lazy, but thread-safe without using locks
    /// </summary>
    public sealed class SingletonNotFullLazy
    {
        private static readonly SingletonNotFullLazy instance = new SingletonNotFullLazy();
        static SingletonNotFullLazy()
        {

        }

        private SingletonNotFullLazy()
        {

        }

        public static SingletonNotFullLazy GetInstance()
        {
            return instance;
        }
    }

    /// <summary>
    /// 单例模式-懒汉式-线程安全-完全懒加载
    /// 
    /// 为了使实例化变得懒惰，代码有些复杂。
    /// </summary>
    public sealed class SingletonFullLazy
    {
        private SingletonFullLazy()
        {

        }

        public static SingletonFullLazy GetInstance()
        {
            return Nested.instance;
        }

        private class Nested
        {
            static Nested()
            {

            }

            internal static readonly SingletonFullLazy instance = new SingletonFullLazy();
        }
    }

    /// <summary>
    /// 单例模式-懒汉式-线程安全-完全懒加载
    /// 
    /// 需要.NET 4.0以上版本，推荐使用的版本
    /// </summary>
    public sealed class SingletonLazyFinal
    {
        private static readonly Lazy<SingletonLazyFinal> lazy = new Lazy<SingletonLazyFinal>(()=>new SingletonLazyFinal());
        private SingletonLazyFinal() { 
        
        }

        public static SingletonLazyFinal GetInstance()
        {
            return lazy.Value;
        }
    }

}
