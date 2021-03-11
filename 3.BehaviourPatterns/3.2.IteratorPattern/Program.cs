using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _3._2.IteratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {

            //客户端代码可能不知道具体的Iterator或Collection类，这取决于要在程序中保留的间接级别。

            var collection = new WordsCollection();
            collection.AddItem("first item");
            collection.AddItem("second item");
            collection.AddItem("third item");


            Console.WriteLine("Straight traversal:");
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }

            collection.ReverseDirection();

            Console.WriteLine();
            Console.WriteLine("reverse traversal:");

            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }

    /*
     迭代器模式

    迭代器模式是一种行为设计模式， 让你能在不暴露集合底层表现形式 （列表、 栈和树等） 的情况下遍历集合中所有的元素。
     */

    public abstract class Iterator : IEnumerator
    {
        object IEnumerator.Current => Current();

        public abstract bool MoveNext();
        public abstract void Reset();

        // Returns the key of the current element
        public abstract int Key();

        // Returns the current element
        public abstract object Current();
    }

    public abstract class IteratorAggregate : IEnumerable
    {
        public abstract IEnumerator GetEnumerator();
    }

    /// <summary>
    /// 具体集合提供了一种或几种与集合类兼容的方法来检索新的迭代器实例。
    /// </summary>
    public class WordsCollection : IteratorAggregate
    {
        List<string> _collection = new List<string>();

        bool _direction = false;

        public void ReverseDirection()
        {
            this._direction = !_direction;
        }

        public List<string> GetItems()
        {
            return _collection;
        }

        public void AddItem(string item)
        {
            this._collection.Add(item);
        }

        public override IEnumerator GetEnumerator()
        {
            return new AlphabeticalOrderIterator(this, _direction);
        }
    }

    /// <summary>
    /// 具体的迭代器实现各种遍历算法。 这些类始终存储当前的遍历位置。
    /// </summary>
    public class AlphabeticalOrderIterator : Iterator
    {
        private WordsCollection _collection;
        /// <summary>
        /// 存储当前的遍历位置。 迭代器可能还有很多其他字段用于存储迭代状态，尤其是在应该与特定种类的集合一起使用时。
        /// </summary>
        private int _postsion = -1;
        private bool _reverse = false;

        public AlphabeticalOrderIterator(WordsCollection collection, bool reverse)
        {
            _collection = collection;
            _reverse = reverse;

            if (reverse)
            {
                _postsion = collection.GetItems().Count;
            }
        }

        public override object Current()
        {
            return _collection.GetItems()[_postsion];
        }

        public override int Key()
        {
            return _postsion;
        }

        public override bool MoveNext()
        {
            var updatedPosition = _postsion + (_reverse ? -1 : 1);

            if (updatedPosition >= 0 && updatedPosition < _collection.GetItems().Count)
            {
                _postsion = updatedPosition;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Reset()
        {
            _postsion = _reverse ? _collection.GetItems().Count - 1 : 0;
        }
    }
}
