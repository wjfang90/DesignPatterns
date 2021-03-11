using System;

namespace _2._4.DecorativeModel
{
    class Program
    {
        static void Main(string[] args)
        {
            Client.Test();

            ClientHalfTransparent.Test();

            Console.ReadKey();
        }
    }

    /*
     装饰模式

    装饰模式：动态地给一个对象增加一些额外的职责，就增加对象功能来说，装饰模式比生成子类实现更为灵活。其别名也可以称为包装器，与适配器模式的别名相同，但它们适用于不同的场合。根据翻译的不同，装饰模式也有人称之为“油漆工模式”。

    装饰模式不会改变原有的类，只是起到一个锦上添花的作用。
    它的主要作用就是：
        增强一个类原有的功能
        为一个类添加新的功能
     */

    #region 1. 透明装饰模式（用于增强功能的装饰模式）


    /*
     可以看到，装饰器也实现了IBeauty接口，并且没有添加新的方法，也就是说这里的装饰器仅用于增强功能，并不会改变Person原有的功能，这种装饰模式称之为透明装饰模式，由于没有改变接口，也没有新增方法，所以透明装饰模式可以无限装饰。

    装饰模式是继承的一种替代方案。
    本例如果不使用装饰模式，而是改用继承实现的话，戴着戒指的Person需要派生一个子类、戴着项链的Person需要派生一个子类、戴着耳环的Person需要派生一个子类、戴着戒指+项链的需要派生一个子类......各种各样的排列组合会造成类爆炸。
    而采用了装饰模式就只需要为每个装饰品生成一个装饰类即可，所以说就增加对象功能来说，装饰模式比生成子类实现更为灵活。
     */

    public interface IBeauty
    {
        int GetBeautyValue();
    }

    public class Person : IBeauty
    {
        public int BeautyValue { get; private set; } = 70;
        public int GetBeautyValue()
        {
            return BeautyValue;
        }
    }

    public class RingDecorator : IBeauty
    {
        private IBeauty person;

        public int BeautyValue { get; private set; } = 5;


        public RingDecorator(IBeauty person)
        {
            this.person = person;
        }

        public int GetBeautyValue()
        {
            return person.GetBeautyValue() + BeautyValue;
        }
    }

    public class Client
    {
        public static void Test()
        {

            Console.WriteLine("透明装饰模式");
            Console.WriteLine();

            var person = new Person();
            Console.WriteLine($"我的基础颜值是{ person.GetBeautyValue()}");

            var ringDecorator = new RingDecorator(person);
            Console.WriteLine($"我的装扮后的颜值是{ ringDecorator.GetBeautyValue()}");
        }
    }

    #endregion

    #region 2. 半透明装饰模式（用于添加功能的装饰模式）

    /*
    
    这就是用于新增功能的装饰模式。我们在接口中新增了方法：Light，然后在装饰器中将House类包装起来，之前MyHouse中的方法仍然调用Bed去执行，也就是说我们并没有修改原有的功能，只是扩展了新的功能，这种模式在装饰模式中称之为半透明装饰模式。


    为什么叫半透明呢？
    由于新的接口ILightDecorator拥有之前IHouse不具有的方法，所以我们如果要使用装饰器中添加的功能，就不得不区别对待装饰前的对象和装饰后的对象。
    也就是说客户端要使用新方法，必须知道具体的装饰类LightDecorator，所以这个装饰类对客户端来说是可见的、不透明的。
    而被装饰者不一定要是MyHouse，它可以是实现了IHouse接口的任意对象，所以被装饰者对客户端是不可见的、透明的。由于一半透明，一半不透明，所以称之为半透明装饰模式。


    装饰类不应该存在依赖关系，而应该在原本的类上进行装饰。半透明装饰模式中，我们无法多次装饰。


    只要添加了新功能的装饰模式都称之为半透明装饰模式，他们都具有不可以多次装饰的特点。
    仔细理解上文半透明名称的由来就知道了，“透明”指的是我们无需知道被装饰者具体的类，既增强了功能，又添加了新功能的装饰模式仍然具有半透明特性。

     */

    public interface IHouse
    {
        /// <summary>
        /// 有床
        /// </summary>
        void Bed();
    }

    public class MyHouse : IHouse
    {
        public void Bed()
        {
            Console.WriteLine("我的房子有张床，可以睡觉");
        }
    }

    public interface ILightDecorator:IHouse
    {
        /// <summary>
        /// 有灯
        /// </summary>
        void Light();
    }

    public class LightDecorator : ILightDecorator
    {
        private IHouse house;

        public LightDecorator(IHouse house)
        {
            this.house = house;
        }

        public void Bed()
        {
            Console.WriteLine("我的房子有张床，可以睡觉");
        }
        public void Light()
        {
            Console.WriteLine("我的房子有台灯，可以照明");
        }
    }


    public class ClientHalfTransparent
    {
        public static void Test()
        {

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("半透明装饰模式");
            Console.WriteLine();

            var myHouse = new MyHouse();
            myHouse.Bed();

            var lightHouse = new LightDecorator(myHouse);
            lightHouse.Bed();
            lightHouse.Light();
        }
    }

    #endregion
}
