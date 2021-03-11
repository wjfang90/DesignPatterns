using System;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("工厂方法模式");

            AppleFactory appleFactory = new AppleFactory();
            var apple = appleFactory.Create();
            apple.Eat();

            PearFactory pearFactory = new PearFactory();
            var pear = pearFactory.Create();
            pear.Eat();

            Console.ReadKey();
        }
    }


    /*
     
    工厂方法模式解决了简单工厂模式的两个弊端。

    当生产的产品种类越来越多时，工厂类不会变成超级类。工厂类会越来越多，保持灵活。不会越来越大、变得臃肿。如果苹果的生产过程需要修改时，只需修改苹果工厂。梨子的生产过程需要修改时，只需修改梨子工厂。符合单一职责原则。
    
    当需要生产新的产品时，无需更改既有的工厂，只需要添加新的工厂即可。保持了面向对象的可扩展性，符合开闭原则。
     */

    public class AppleFactory
    {
        public Fruit Create()
        {
            return new Apple();
        }
    }

    public class PearFactory
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
