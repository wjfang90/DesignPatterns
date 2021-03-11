using System;

namespace _3._3.MediatorPattern
{
    class Program
    {
        static void Main(string[] args)
        {

            Client.Test();

            ClientMediator.Test();

            Console.ReadKey();
        }
    }

    /*
     中介者模式

     中介者模式（Mediator Pattern）：定义一个中介对象来封装一系列对象之间的交互，使原有对象之间的耦合松散，且可以独立地改变它们之间的交互。

     中介者模式就是用于将类与类之间的多对多关系简化成多对一、一对多关系的设计模式

     举个例子，在我们打麻将时，每两个人之间都可能存在输赢关系。如果每笔交易都由输家直接发给赢家，就会出现一种网状耦合关系。

     */


    /// <summary>
    /// 此类中有一个 money 变量，表示自己的余额。
    /// 当自己赢了某位玩家的钱时，调用 win 方法修改输钱的人和自己的余额。
    /// </summary>
    public class Player
    {
        /// <summary>
        /// 初始金额
        /// </summary>
        public decimal Money { get; set; } = 100;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="money"></param>
        public void Win(Player player,decimal money)
        {
            // 输钱的人扣减相应的钱
            player.Money -= money;
            // 自己的余额增加
            this.Money += money;
        }
    }

  

    public class Client
    {
        public static void Test()
        {
            Player player1 = new Player();
            Player player2 = new Player();
            Player player3 = new Player();
            Player player4 = new Player();
            // player1 赢了 player3 5 元
            player1.Win(player3, 5);
            // player2 赢了 player1 10 元
            player2.Win(player1, 10);
            // player2 赢了 player4 10 元
            player2.Win(player4, 10);
            // player4 赢了 player3 7 元
            player4.Win(player3, 7);

            Console.WriteLine($"{nameof(player1)}剩余的钱：{player1.Money}");
            Console.WriteLine($"{nameof(player2)}剩余的钱：{player2.Money}");
            Console.WriteLine($"{nameof(player3)}剩余的钱：{player3.Money}");
            Console.WriteLine($"{nameof(player4)}剩余的钱：{player4.Money}");
        }
    }


    public class Mediator
    {
        public decimal Money { get; set; }
    }

    /// <summary>
    /// 此类中有一个 money 变量，表示自己的余额。
    /// </summary>
    public class PlayerWithMediator
    {
        /// <summary>
        /// 初始金额
        /// </summary>
        public decimal Money { get; set; } = 100;

        private Mediator _mediator;

        public PlayerWithMediator(Mediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="money"></param>
        public void ChangeMoney(decimal money)
        {
            // 输了钱将钱发到群里 或 在群里领取自己赢的钱
            _mediator.Money += money;
            // 自己的余额增加
            this.Money += money;
        }
    }

    public class ClientMediator
    {
        public static void Test()
        {
            Mediator group = new Mediator();

            PlayerWithMediator player1 = new PlayerWithMediator(group);
            PlayerWithMediator player2 = new PlayerWithMediator(group);
            PlayerWithMediator player3 = new PlayerWithMediator(group);
            PlayerWithMediator player4 = new PlayerWithMediator(group);
            // player1 赢了 5 元
            player1.ChangeMoney(5);
            // player2 赢了 10 元
            player2.ChangeMoney(10);
            // player2 输了 12 元
            player3.ChangeMoney(-12);
            // player4 输了3 元
            player4.ChangeMoney(-3);

            Console.WriteLine();
            Console.WriteLine("中介者模式");
            Console.WriteLine($"{nameof(player1)}剩余的钱：{player1.Money}");
            Console.WriteLine($"{nameof(player2)}剩余的钱：{player2.Money}");
            Console.WriteLine($"{nameof(player3)}剩余的钱：{player3.Money}");
            Console.WriteLine($"{nameof(player4)}剩余的钱：{player4.Money}");
        }
    }
}
