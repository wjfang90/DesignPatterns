using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2._6.FlyWeightModel
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new FlyweightFactory(
              new Car { Company = "Chevrolet", Model = "Camaro2018", Color = "pink" },
              new Car { Company = "Mercedes Benz", Model = "C300", Color = "black" },
              new Car { Company = "Mercedes Benz", Model = "C500", Color = "red" },
              new Car { Company = "BMW", Model = "M5", Color = "red" },
              new Car { Company = "BMW", Model = "X6", Color = "white" }
          );
            factory.listFlyweights();

            AddCarToDb(factory, new Car
            {
                Number = "CL234IR",
                Owner = "James Doe",
                Company = "BMW",
                Model = "M5",
                Color = "red"
            });

            AddCarToDb(factory, new Car
            {
                Number = "CL234IR",
                Owner = "James Doe",
                Company = "BMW",
                Model = "X1",
                Color = "red"
            });

            factory.listFlyweights();

            Console.ReadKey();
        }

        public static void AddCarToDb(FlyweightFactory flyweightFactory, Car car)
        {
            var flyweight = flyweightFactory.GetFlyweight(car);
            flyweight.Operation(car);
        }
    }

    /*
     享元模式

    享元模式体现的是程序可复用的特点，为了节约宝贵的内存，程序应该尽可能地复用，就像《极限编程》作者Kent在书里说到的那样：Don't repeat yourself.
    简单来说享元模式就是共享对象，提高复用性。

    享元模式：运用共享技术有效地支持大量细粒度对象的复用。系统只使用少量的对象，而这些对象都很相似，状态变化很小，可以实现对象的多次复用。由于享元模式要求能够共享的对象必须是细粒度对象，因此它又称为轻量级模式。

    ​有个细节值得注意：有些对象本身不一样，但通过一点点变化后就可以复用，我们编程时可能稍不注意就会忘记复用这些对象。比如说伟大的《超级玛丽》，谁能想到草和云更改一下颜色就可以实现复用呢？



    享元模式适合应用场景
    仅在程序必须支持大量对象且没有足够的内存容量时使用享元模式。

    应用该模式所获的收益大小取决于使用它的方式和情景。 它在下列情况中最有效：

        这将耗尽目标设备的所有内存
        程序需要生成数量巨大的相似对象
        对象中包含可抽取且能在多个对象间共享的重复状态。
     */


    /// <summary>
    /// Flyweight存储状态的公共部分（也称为固有状态），该状态属于多个实际业务实体。 Flyweight通过其方法参数接受其余状态（外部状态，对于每个实体都是唯一的）。
    /// </summary>
    public class Flyweight
    {
        private Car _sharedState;
        public Flyweight(Car car)
        {
            this._sharedState = car;
        }

        public void Operation(Car uniqueState)
        {
            string s = JsonConvert.SerializeObject(this._sharedState);
            string u = JsonConvert.SerializeObject(uniqueState);
            Console.WriteLine();
            Console.WriteLine($"Flyweight: Displaying shared {s} and unique {u} state.");
        }
    }

    /// <summary>
    /// Flyweight Factory创建并管理Flyweight对象。 它确保正确分配举重。 当客户端请求重量级时，工厂将返回现有实例或创建一个实例。  新的（如果尚不存在）。
    /// </summary>
    public class FlyweightFactory
    {
        private List<Tuple<Flyweight, string>> flyweights = new List<Tuple<Flyweight, string>>();

        public FlyweightFactory(params Car[] cars)
        {
            cars.ToList().ForEach(item =>
            {
                var flyweight = new Flyweight(item);
                flyweights.Add(new Tuple<Flyweight, string>(flyweight, GetKey(item)));
            });

        }

        public string GetKey(Car car)
        {
            var properties = car.GetType().GetProperties().Where(t => !string.IsNullOrEmpty(t.Name));

            var propertyValues = properties.Where(t => !string.IsNullOrEmpty(t.GetValue(car)?.ToString()))
                                .Select(t => t.GetValue(car)?.ToString())
                                .OrderBy(t => t);

            return string.Join("_", propertyValues);
        }

        /// <summary>
        ///  Returns an existing Flyweight with a given state or creates a new one.
        /// </summary>
        /// <param name="sharedState"></param>
        /// <returns></returns>
        public Flyweight GetFlyweight(Car sharedState)
        {
            var key = GetKey(sharedState);
            Console.WriteLine();

            if (flyweights.Count(t => t.Item2 == key) == 0)
            {
                Console.WriteLine("FlyweightFactory: Can't find a flyweight, creating new one.");

                var flyweight = new Flyweight(sharedState);
                flyweights.Add(new Tuple<Flyweight, string>(flyweight, GetKey(sharedState)));
            }
            else
            {
                Console.WriteLine("FlyweightFactory: Reusing existing flyweight.");
            }

            return flyweights.FirstOrDefault(t => t.Item2 == key).Item1;
        }


        public void listFlyweights()
        {
            var count = flyweights.Count;
            Console.WriteLine();
            Console.WriteLine($"\nFlyweightFactory: I have {count} flyweights:");

            flyweights.ForEach(flyweight =>
            {
                Console.WriteLine(flyweight.Item2);
            });
        }
    }


    public class Car
    {
        public string Owner { get; set; }
        public string Number { get; set; }
        public string Company { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
    }
}
