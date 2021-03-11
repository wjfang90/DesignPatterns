using System;

namespace _1._3.AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("抽象工厂模式");

            IFactory appleFactory = new AppleFactory();
            var apple = appleFactory.Create();
            apple.Eat();

            IFactory pearFactory = new PearFactory();
            var pear = pearFactory.Create();
            pear.Eat();

            Console.ReadKey();
        }
    }



    /*
     * 
     IFactory 中只有一个抽象方法时，或许还看不出抽象工厂模式的威力。实际上抽象工厂模式主要用于替换一系列方法。例如将程序中的 SQL Server 数据库整个替换为 Access 数据库，使用抽象方法模式的话，只需在 IFactory 接口中定义好增删改查四个方法，让 SQLFactory 和 AccessFactory 实现此接口，调用时直接使用 IFactory 中的抽象方法即可，调用者无需知道使用的什么数据库，我们就可以非常方便的整个替换程序的数据库，并且让客户端毫不知情。

     抽象工厂模式很好的发挥了开闭原则、依赖倒置原则，但缺点是抽象工厂模式太重了，如果 IFactory 接口需要新增功能，则会影响到所有的具体工厂类。使用抽象工厂模式，替换具体工厂时只需更改一行代码，但要新增抽象方法则需要修改所有的具体工厂类。所以抽象工厂模式适用于增加同类工厂这样的横向扩展需求，不适合新增功能这样的纵向扩展。
     */
    public interface IFactory
    {
        Fruit Create();
    }

    public class AppleFactory:IFactory
    {
        public Fruit Create()
        {
            return new Apple();
        }
    }

    public class PearFactory: IFactory
    {
        public Fruit Create()
        {
            return new Pear();
        }
    }

    public abstract class Fruit
    {
        public virtual void Eat()
        {
            Console.WriteLine($"{nameof(Fruit)}.{nameof(Eat)}()");
        }
    }

    public class Apple : Fruit
    {
        public override void Eat()
        {
            Console.WriteLine($"{nameof(Apple)}.{nameof(Eat)}()");
        }
    }

    public class Pear : Fruit
    {
        public override void Eat()
        {
            Console.WriteLine($"{nameof(Pear)}.{nameof(Eat)}()");
        }
    }
}
