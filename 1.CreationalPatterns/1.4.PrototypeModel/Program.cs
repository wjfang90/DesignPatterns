using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace _4._1.PrototypeModel
{
    class Program
    {
        static void Main(string[] args)
        {
            MilkTea milkTea = new MilkTea("原味", "中杯");
            var hasPearl = milkTea.HasPearl ? "加珍珠" : "不加珍珠";
            var hasIce= milkTea.HasIce ? "加冰" : "不加冰";

            Console.WriteLine($"周杰伦喜欢的是{milkTea.Type}，{milkTea.Size}，{hasPearl}，{hasIce} 奶茶");

            milkTea.Type = "香草";
            Console.WriteLine($"周杰伦现在喜欢的{milkTea.Type}奶茶了");

            var myMilkTea =  milkTea.LightClone();
            hasPearl = myMilkTea.HasPearl ? "加珍珠" : "不加珍珠";
            hasIce = myMilkTea.HasIce ? "加冰" : "不加冰";

            Console.WriteLine($"我也喜欢的是{myMilkTea.Type}，{myMilkTea.Size}，{hasPearl}，{hasIce} 奶茶");


            var joyFunMilkTea = milkTea.DeepClone<MilkTea>(milkTea);
            hasPearl = joyFunMilkTea.HasPearl ? "加珍珠" : "不加珍珠";
            hasIce = joyFunMilkTea.HasIce ? "加冰" : "不加冰";

            Console.WriteLine($"周杰伦的fun也喜欢的是{joyFunMilkTea.Type}，{joyFunMilkTea.Size}，{hasPearl}，{hasIce} 奶茶");
            
            Console.ReadKey();
        }
    }

    /*
     
     原型模式

     用原型实例指定创建对象的种类，并且通过拷贝这些原型创建新的对象。

    举个例子，比如有一天，周杰伦到奶茶店点了一份不加冰的原味奶茶，你说我是周杰伦的忠实粉，我也要一份跟周杰伦一样的。用程序表示如下：
     */

    [Serializable]
    public class MilkTea 
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 大中小杯
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 是否加珍珠
        /// </summary>
        public bool HasPearl { get; private set; }
        /// <summary>
        /// 是否加冰
        /// </summary>
        public bool HasIce { get; private set; }

        public MilkTea(string type, string size, bool hasPearl = false, bool hasIce = false)
        {
            this.Type = type;
            this.Size = size;
            this.HasPearl = hasPearl;
            this.HasIce = hasIce;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MilkTea LightClone()
        {
            //注意 MemberwiseClone 是浅拷贝，只有基本类型的参数会被拷贝一份，非基本类型的对象不会被拷贝一份，而是继续使用传递引用的方式
            return this.MemberwiseClone() as MilkTea;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public T DeepClone<T>(T source)
        {
            //深拷贝
            MemoryStream memoryStream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();

            using (memoryStream)
            {
                binaryFormatter.Serialize(memoryStream, source);

                memoryStream.Seek(0, SeekOrigin.Begin);

                return (T)binaryFormatter.Deserialize(memoryStream);
            }
           
        }
    }
}
