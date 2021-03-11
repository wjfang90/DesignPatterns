using System;

namespace _3._4.MementoPattern
{
    class Program
    {
        static void Main(string[] args)
        {

            Client.Test();
            Console.ReadKey();
        }
    }

    /*
      备忘录模式

      备忘录模式：在不破坏封装的条件下，通过备忘录对象存储另外一个对象内部状态的快照，在将来合适的时候把这个对象还原到存储起来的状态。

      备忘录模式最常见的实现莫过于游戏中的存档、读档功能了，通过存档、读档，使得我们可以随时恢复到之前的状态。
      当我们在玩游戏时，打大 Boss 之前，通常会将自己的游戏进度存档保存，以防打不过 Boss 的话，还能重新读档恢复状态。

      总体而言，备忘录模式是利大于弊的，所以许多程序都为用户提供了备份方案。
      比如IDE中，用户可以将自己的设置导出成zip，当需要恢复设置时，再将导出的zip文件导入即可。这个功能内部的原理就是备忘录模式。
     */

    public class Player
    {
        public int Life { get; private set; } = 500;

        public int Magic { get; private set; } = 200;

        public void ChangeLife(int life)
        {
            Life += life;
            if (Life <= 0)
            {
                Life = 0;
            }

            if (Life > 500)
            {
                Life = 500;
            }

        }

        public void GetPlayerState()
        {
            Console.WriteLine($"{nameof(Life)}={Life}，{nameof(Magic)}={Magic}");
        }


        public void FightedByBoss(int life)
        {
            ChangeLife(life);

            if (Life == 0)
            {
                Console.WriteLine("任务失败，角色已死亡");
            }
        }

        public Memento SaveState()
        {
            Console.WriteLine("存档中...");
            return new Memento(Life, Magic);
        }

        public void ReadState(Memento memento)
        {
            Console.WriteLine("读档中...");

            Life = memento.Life;
            Magic = memento.Magic;
        }
    }

    public class Memento
    {
        public int Life { get; private set; }
        public int Magic { get; private set; }

        public Memento(int life, int magic)
        {
            this.Life = life;
            this.Magic = magic;
        }
    }

    public class Client
    {
        public static void Test()
        {
            var player = new Player();

            player.GetPlayerState();

            var memento = player.SaveState();

            player.FightedByBoss(-600);

            player.ReadState(memento);

            player.GetPlayerState();
        }
    }
}
