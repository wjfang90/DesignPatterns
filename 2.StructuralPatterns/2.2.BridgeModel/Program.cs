using System;

namespace _2._2.BridgeModel
{
    class Program
    {
        static void Main(string[] args)
        {

            var rectangle = new Rectanlge();
            rectangle.SetColor(new Red());
            rectangle.Drap();

            var round = new Round();
            round.SetColor(new Blue());
            round.Drap();

            Console.ReadKey();
        }
    }


    /*
     桥接模式

    桥接模式：将抽象部分与它的实现部分分离，使它们都可以独立地变化。它是一种对象结构型模式，又称为柄体模式或接口模式。 

    主要用于两个或多个同等级的接口

    1.考虑这样一个需求：绘制矩形、圆形、三角形这三种图案。按照面向对象的理念，我们至少需要三个具体类，对应三种不同的图形。
    2.接下来我们有了新的需求，每种形状都需要有四种不同的颜色：红、蓝、黄、绿。

    这时我们很容易想到两种设计方案：
    1.为了复用形状类，将每种形状定义为父类，每种不同颜色的图形继承自其形状父类。此时一共有12个类。
    2.为了复用颜色类，将每种颜色定义为父类，每种不同颜色的图形继承自其颜色父类。此时一共有12个类。

    但仔细想一想，如果以后要增加一种颜色，比如黑色，那么我们就需要增加三个类；如果再要增加一种形状，我们又需要增加五个类，对应 5 种颜色。
    更不用说遇到增加 20 个形状，20 种颜色的需求，不同的排列组合将会使工作量变得无比的庞大。看来我们不得不重新思考设计方案。

    形状和颜色，都是图形的两个属性。他们两者的关系是平等的，所以不属于继承关系。
    更好的的实现方式是：将形状和颜色分离，根据需要对形状和颜色进行组合，这就是桥接模式的思想。

    通俗地说，如果一个对象有两种或者多种分类方式，并且两种分类方式都容易变化，比如本例中的形状和颜色。这时使用继承很容易造成子类越来越多，所以更好的做法是把这种分类方式分离出来，让他们独立变化，使用时将不同的分类进行组合即可。

    说到这里，不得不提一个设计原则：合成 / 聚合复用原则。虽然它没有被划分到六大设计原则中，但它在面向对象的设计中也非常的重要。
    合成 / 聚合复用原则：优先使用合成 / 聚合，而不是类继承。

    继承虽然是面向对象的三大特性之一，但继承会导致子类与父类有非常紧密的依赖关系，它会限制子类的灵活性和子类的复用性。
    而使用合成 / 聚合，也就是使用接口实现的方式，就不存在依赖问题，一个类可以实现多个接口，可以很方便地拓展功能。

     */


    public interface ISharp
    {
        void Drap();
    }

    /// <summary>
    /// 矩形
    /// </summary>
    public class Rectanlge : ISharp
    {
        public void Drap()
        {
            Console.WriteLine($"Drap a {GetColor()} Rectanlge");
        }

        private IColor color;
        public void SetColor(IColor color)
        {
            this.color = color;
        }

        public string GetColor()
        {
            return color.GetColor();
        }
    }

    /// <summary>
    /// 圆形
    /// </summary>
    public class Round : ISharp
    {
        public void Drap()
        {
            Console.WriteLine($"Drap a {GetColor()} Round");
        }


        private IColor color;
        public void SetColor(IColor color)
        {
            this.color= color;
        }

        public string GetColor()
        {
            return color.GetColor();
        }
    }

    /// <summary>
    /// 三角形
    /// </summary>
    public class Triangel : ISharp
    {
        public void Drap()
        {
            Console.WriteLine($"Drap a {GetColor()} Triangel");
        }


        private IColor color;
        public string GetColor()
        {
            return color.GetColor();
        }

        public void SetColor(IColor color)
        {
            this.color = color;
        }
    }

    public interface IColor
    {
        string ColorName { get; set; }
        string GetColor();
    }

    public class Red : IColor
    {
        public string ColorName { get; set; } = nameof(Red);
        public string GetColor()
        {
            return ColorName;
        }
    }

    public class Green : IColor
    {
        public string ColorName { get; set; } = nameof(Green);
        public string GetColor()
        {
            return ColorName;
        }
    }

    public class Blue : IColor
    {
        public string ColorName { get; set; } = nameof(Blue);
        public string GetColor()
        {
            return ColorName;
        }
    }
}
