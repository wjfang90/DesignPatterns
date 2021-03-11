using System;
using System.Collections;
using System.Collections.Generic;

namespace _3._1.InterpreterPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var expression = "一加一";
            Console.WriteLine($"{expression} { Calculator.Calculate(expression)}");
            
            expression = "一加一加一";
            Console.WriteLine($"{expression} { Calculator.Calculate(expression)}");
            
            expression = "二加五减三";
            Console.WriteLine($"{expression} { Calculator.Calculate(expression)}");
            
            expression = "七减五加四减一";
            Console.WriteLine($"{expression} { Calculator.Calculate(expression)}");
           
            expression = "九减五加三减一";
            Console.WriteLine($"{expression} { Calculator.Calculate(expression)}");

            Console.ReadKey();
        }
    }

    /*
     解释器模式
     
    解释器模式（Interpreter Pattern）：给定一门语言，定义它的文法的一种表示，并定义一个解释器，该解释器使用该表示来解释语言中的句子。

    
    一个简单的例子来学习解释器模式：
    使用中文编写出十以内的加减法公式。比如：
        输入“一加一”，输出结果2
        输入“一加一加一”，输出结果3
        输入“二加五减三”，输出结果4
        输入“七减五加四减一”，输出结果5
        输入“九减五加三减一”，输出结果6

    分析本例中公式的组成，我们可以发现几条显而易见的性质：
    数字类不可被拆分，属于计算中的最小单元；加法类、减法类可以被拆分成两个数字（或两个公式）加一个计算符，他们不是计算的最小单元。

    在解释器模式中，我们将不可拆分的最小单元称之为终结表达式，可以被拆分的表达式称之为非终结表达式。
    解释器模式具有一定的拓展性，当需要添加其他计算符时，我们可以通过添加Operator的子类来完成。但添加后需要按照运算优先级修改计算规则。

    解释器模式有一个常见的应用，在我们平时匹配字符串时，用到的正则表达式就是一个解释器。正则表达式中，表示一个字符的表达式属于终结表达式，除终结表达式外的所有表达式都属于非终结表达式。
     */

    public interface IExpression
    {
        int Intercept();
    }

    /// <summary>
    /// 数字类
    /// </summary>
    public class Number : IExpression
    {
        private int number;
        public Number(char cnNumber)
        {
            switch (cnNumber)
            {
                case '一':
                    number = 1;
                    break;
                case '二':
                    number = 2;
                    break;
                case '三':
                    number = 3;
                    break;
                case '四':
                    number = 4;
                    break;
                case '五':
                    number = 5;
                    break;
                case '六':
                    number = 6;
                    break;
                case '七':
                    number = 7;
                    break;
                case '八':
                    number = 8;
                    break;
                case '九':
                    number = 9;
                    break;
                case '零':
                    number = 0;
                    break;
            }
        }
        public int Intercept()
        {
            return number;
        }
    }

    /// <summary>
    /// 操作符抽象类
    /// </summary>
    public abstract class Operator : IExpression
    {
        public IExpression Left { get; private set; }
        public IExpression Right { get; private set; }

        public Operator(IExpression left, IExpression right)
        {
            this.Left = left;
            this.Right = right;
        }

        public abstract int Intercept();
    }

    /// <summary>
    /// 加法类
    /// </summary>
    public class Add : Operator
    {
        public Add(IExpression left, IExpression right) : base(left, right)
        {

        }
        public override int Intercept()
        {
            return Left.Intercept() + Right.Intercept();
        }
    }

    /// <summary>
    /// 减法类
    /// </summary>
    public class Subtract : Operator
    {
        public Subtract(IExpression left, IExpression right) : base(left, right)
        {

        }

        public override int Intercept()
        {
            return Left.Intercept() - Right.Intercept();
        }
    }

    /// <summary>
    /// 计算类
    /// 
    /// 在计算类中，我们使用栈结构保存每一步操作。遍历 expression 公式：
    /// 遇到数字则将其压入栈中；
    /// 遇到计算符时，先将栈顶元素pop出来，再和下一个数字一起传入计算符的构造函数中，组成一个计算符公式压入栈中。
    /// 
    /// 需要注意的是，入栈出栈过程并不会执行真正的计算，栈操作只是将表达式组装成一个嵌套的类对象而已
    /// 最后一步stack.pop().intercept()，将栈顶的元素弹出，执行intercept()，这时才会执行真正的计算。计算时会将中文的数字和运算符分别解释成计算机能理解的指令。
    /// </summary>
    public class Calculator
    {
        public static int Calculate(string cnExpression)
        {
            var stack = new Stack<IExpression>();
            for (var index = 0; index < cnExpression.Length; index++)
            {
                var item = cnExpression[index];
                switch (item)
                {
                    case '加':
                        var rightNumber = new Number(cnExpression[++index]);
                        var add = new Add(stack.Pop(), rightNumber);
                        stack.Push(add);
                        break;
                    case '减':
                        rightNumber = new Number(cnExpression[++index]);
                        var subtrack = new Subtract(stack.Pop(), rightNumber);
                        stack.Push(subtrack);
                        break;
                    default:
                        var currentNumber = new Number(item);
                        stack.Push(currentNumber);
                        break;
                }
            }

            return stack.Pop().Intercept();
        }
    }
}
