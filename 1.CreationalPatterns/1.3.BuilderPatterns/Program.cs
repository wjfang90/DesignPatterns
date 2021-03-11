using System;

namespace BuilderPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            var milkTeaBuilder = new MilkTea.MilkTeaBuilder("原味")
                                    .SetSize("大杯")
                                    .SetHasPearl(true)
                                    .SetHasIce(true);

            var milkTea = milkTeaBuilder.Build();

            var hasPearl= milkTea.HasPearl ? "加" : "不加";
            var hasIce= milkTea.HasIce ? "加" : "不加";

            Console.WriteLine($"一杯{milkTea.Type}奶茶，{milkTea.Size}，{hasPearl}珍珠，{hasIce}冰");

            Console.ReadKey();
        }
    }

    /*
     
    建造型模式

    建造型模式用于创建过程稳定，但配置多变的对象。在《设计模式》一书中的定义是：
    将一个复杂的构建与其表示相分离，使得同样的构建过程可以创建不同的表示。

    经典的「建造者-指挥者」模式现在已经不太常用了，现在建造者模式主要用来通过链式调用生成不同的配置。
    比如我们要制作一杯珍珠奶茶。它的制作过程是稳定的，除了必须要知道奶茶的种类和规格外，是否加珍珠和是否加冰是可选的。
    使用建造者模式表示如下：


    可以看到，我们将 MilkTea 的构造方法设置为私有的，所以外部不能通过 new 构建出 MilkTea 实例，只能通过 Builder 构建。
    对于必须配置的属性，通过 Builder 的构造方法传入，可选的属性通过 Builder 的链式调用方法传入，如果不配置，将使用默认配置，也就是中杯、加珍珠、不加冰。根据不同的配置可以制作出不同的奶茶：
     */

    public class MilkTea
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; private set; }
        /// <summary>
        /// 大中小杯
        /// </summary>
            public string Size { get; private set; }
        /// <summary>
        /// 是否加珍珠
        /// </summary>
            public bool HasPearl { get; private set; }
        /// <summary>
        /// 是否加冰
        /// </summary>
        public bool HasIce { get; private set; }

        private MilkTea(MilkTeaBuilder builder)
        {
            this.Type = builder.Type;
            this.Size = builder.Size;
            this.HasPearl = builder.HasPearl;
            this.HasIce = builder.HasIce;
        }

        public class MilkTeaBuilder
        {

            public string Type { get; private set; }
            public string Size { get; private set; } = "中杯";
            public bool HasPearl { get; private set; } = true;
            public bool HasIce { get; private set; } = false;

            public MilkTeaBuilder(string type)
            {
                this.Type = type;
            }

            public MilkTeaBuilder SetSize(string size)
            {
                this.Size = size;
                return this;
            }

            public MilkTeaBuilder SetHasPearl(bool hasPearl)
            {
                this.HasPearl = hasPearl;
                return this;
            }

            public MilkTeaBuilder SetHasIce(bool hasIce)
            {
                this.HasIce = hasIce;
                return this;
            }

            public MilkTea Build()
            {
                return new MilkTea(this);
            }
        }
    }
   
}
