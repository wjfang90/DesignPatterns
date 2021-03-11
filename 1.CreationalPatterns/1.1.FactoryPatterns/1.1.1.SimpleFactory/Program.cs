using System;

namespace SimpleFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("简单工厂模式");

            var fruitFactory = new FruitFactory();
            
            var apple = fruitFactory.Create("Apple");
            apple.Eat();

            var pear = fruitFactory.Create("Pear");
            pear.Eat();

            Console.ReadKey();
        }
    }


    /// <summary>
    /// 简单工厂模式
    /// 
    /// 优点
    /// 调用者的代码则完全不需要变化，而且调用者不需要在每次需要苹果时，自己去构建苹果种子、阳光、水分以获得苹果。苹果的生产过程再复杂，也只是工厂的事。这就是封装的好处，假如某天科学家发明了让苹果更香甜的肥料，要加入苹果的生产过程中的话，也只需要在工厂中修改，调用者完全不用关心。
    /// 
    /// 弊端
    /// 一是如果需要生产的产品过多，此模式会导致工厂类过于庞大，承担过多的职责，变成超级类。当苹果生产过程需要修改时，要来修改此工厂。梨子生产过程需要修改时，也要来修改此工厂。也就是说这个类不止一个引起修改的原因。违背了单一职责原则。
    /// 二是当要生产新的产品时，必须在工厂类中添加新的分支。而开闭原则告诉我们：类应该对修改封闭。我们希望在添加新功能时，只需增加新的类，而不是修改既有的类，所以这就违背了开闭原则。
    /// 
    /// </summary>
    public class FruitFactory
    {
        public Fruit Create(string fruitType)
        {
            switch (fruitType)
            {
                case nameof(Apple):
                    return new Apple();
                case nameof(Pear):
                    return new Pear();
                default:
                    new NotImplementedException("未支持此类水果");
                    return null;
            }
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
