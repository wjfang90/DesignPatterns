using System;
using System.Collections.Generic;
using System.Linq;

namespace _2._3.CompoundModel
{
    class Program
    {
        static void Main(string[] args)
        {

            //不使用组合模式
            Client.Test();

            //组合模式-透明方式
            ClientComponmentDirectory.Test();

            //组合模式-安全方式
            ClientComponmentSelf.Test();


            Console.ReadKey();
        }
    }

    /*
     组合模式

    组合模式：又叫部分整体模式，是用于把一组相似的对象当作一个单一的对象。组合模式依据树形结构来组合对象，用来表示部分以及整体层次。这种类型的设计模式属于结构型模式，它创建了对象组的树形结构。

    组合模式用于整体与部分的结构，当整体与部分有相似的结构，在操作时可以被一致对待时，就可以使用组合模式。
    
    例如：
    文件夹和子文件夹的关系：文件夹中可以存放文件，也可以新建文件夹，子文件夹也一样。
    总公司子公司的关系：总公司可以设立部门，也可以设立分公司，子公司也一样。
    树枝和分树枝的关系：树枝可以长出叶子，也可以长出树枝，分树枝也一样。


    考虑这样一个实际应用：设计一个公司的人员分布结构

    老板
        人力资源
        项目经理
            设计师
            运营人员
            技术主管
                程序员
                后台程序员
        财务主管
            会计
            文员

    人员结构中有两种结构，一是管理者，如老板，PM，CFO，CTO，二是职员。其中有的管理者不仅仅要管理职员，还会管理其他的管理者。这就是一个典型的整体与部分的结构。

     */



    #region    不使用组合模式方案

    /*
     
     这样我们就设计出了公司的结构，但是这样的设计有两个弊端：
        name 字段，job字段，work方法重复了。
        管理者对其管理的管理者和职员需要区别对待。

    大量的重复显然是很丑陋的代码，分析一下可以发现， Manager 类只比 Employee 类多一个管理人员的列表字段，多几个增加 / 移除人员的方法，其他的字段和方法全都是一样的。

    我们可以将重复的字段和方法提取到一个工具类中，让 Employee 和 Manager 都去调用此工具类，就可以消除重复了。
    这样固然可行，但属于 Employee 和 Manager 类自己的东西却要通过其他类调用，并不利于程序的高内聚。

    关于第二个弊端，此方案无法解决，此方案中 Employee 和 Manager 类完全是两个不同的对象，两者的相似性被忽略了。
    所以我们有更好的设计方案，那就是组合模式！
     */

    public class Manager
    {
        public string Position { get; private set; }
        public string Job { get; private set; }

        /// <summary>
        /// 管理的管理者
        /// </summary>
        public List<Manager> Managers { get; set; }

        /// <summary>
        /// 管理的员工
        /// </summary>
        public List<Employee> Employees { get; set; }

        public Manager(string position, string job)
        {
            this.Position = position;
            this.Job = job;

        }

        public void DoWork()
        {
            Console.WriteLine($"我是{Position}，我的工作是{Job}");
        }

        /// <summary>
        /// 检查下属工作
        /// </summary>
        public void CheckWork()
        {
            DoWork();

            if (Employees != null && Employees.Any())
            {
                Employees.ForEach(t =>
                {
                    t.DoWork();
                });
            }

            if (Managers != null && Managers.Any())
            {
                Managers.ForEach(t =>
                {
                    t.CheckWork();
                });
            }
        }

        public void AddManager(Manager manager)
        {
            if (Managers == null)
            {
                Managers = new List<Manager>();
            }

            Managers.Add(manager);
        }

        public void RemoveManager(Manager manager)
        {
            if (Managers != null && Managers.Any() && manager != null)
            {
                Managers.Remove(manager);
            }
        }

        public void AddEmployee(Employee employee)
        {
            if (Employees == null)
            {
                Employees = new List<Employee>();
            }

            Employees.Add(employee);
        }

        public void RemoveEmployee(Employee employee)
        {
            if (Employees != null && Employees.Any() && employee != null)
            {
                Employees.Remove(employee);
            }
        }
    }

    public class Employee
    {
        public string Position { get; private set; }
        public string Job { get; private set; }


        public Employee(string position, string job)
        {
            this.Position = position;
            this.Job = job;
        }

        public void DoWork()
        {
            Console.WriteLine($"我是{Position}，我的工作是{Job}");
        }
    }


    public class Client
    {

        public static void Test()
        {
            Console.WriteLine("不使用组合模式");
            Console.WriteLine();

            Manager boss = new Manager("老板", "唱怒放的生命");

            Employee HR = new Employee("人力资源", "聊微信");
            Manager PM = new Manager("项目经理", "不知道干啥");
            Manager CFO = new Manager("财务主管", "看剧");

            Manager CTO = new Manager("技术主管", "划水");
            Employee UI = new Employee("设计师", "画画");
            Employee operater = new Employee("运营人员", "兼职客服");

            Employee webProgrammer = new Employee("程序员", "学习设计模式");
            Employee backgroundProgrammer = new Employee("后台程序员", "CRUD");

            Employee accountant = new Employee("会计", "背九九乘法表");
            Employee clerk = new Employee("文员", "给老板递麦克风");

            boss.AddEmployee(HR);
            boss.AddManager(PM);
            boss.AddManager(CFO);

            PM.AddEmployee(UI);
            PM.AddManager(CTO);
            PM.AddEmployee(operater);

            CTO.AddEmployee(webProgrammer);
            CTO.AddEmployee(backgroundProgrammer);

            CFO.AddEmployee(accountant);
            CFO.AddEmployee(clerk);

            boss.CheckWork();
        }
    }

    #endregion

    #region 组合模式-透明方式(实现同一接口方法，即使有些实现类不需要某些功能方法)

    /*
     透明方式：在 ComponentByDirectory 中声明所有管理子对象的方法，包括 add 、remove 等，这样继承自 ComponentByDirectory 的子类都具备了 add、remove 方法。对于外界来说叶节点和枝节点是透明的，它们具备完全一致的接口。

    但它的缺点也显而易见：Employee 类并不支持管理子对象，不仅违背了接口隔离原则，而且客户端可以用 Employee 类调用 addComponent 和 removeComponent 方法，导致程序出错，所以这种方式是不安全的。

    安全方式和透明方式各有好处，在使用组合模式时，需要根据实际情况决定。但大多数使用组合模式的场景都是采用的透明方式，虽然它有点不安全，但是客户端无需做任何判断来区分是叶子结点还是枝节点，用起来是真香。
     */

    public abstract class ComponentByDirectory
    {
        public string Position { get; set; }
        public string Job { get; set; }


        public ComponentByDirectory(string position, string job)
        {
            this.Position = position;
            this.Job = job;
        }
        public virtual void DoWork()
        {
            Console.WriteLine($"我是{Position}，我的工作是{Job}");
        }
        public abstract void CheckWork();

        public abstract void AddComponent(ComponentByDirectory manager);
        public abstract void RemoveComponent(ComponentByDirectory manager);

    }

    public class ManagerByDirectory : ComponentByDirectory
    {

        public List<ComponentByDirectory> Components { get; set; }

        public ManagerByDirectory(string position, string job) : base(position, job)
        {

        }

        /// <summary>
        /// 检查下属工作
        /// </summary>
        public override void CheckWork()
        {
            DoWork();
            if (Components != null && Components.Any())
            {
                Components.ForEach(t =>
                {
                    t.CheckWork();
                });
            }
        }

        public override void AddComponent(ComponentByDirectory component)
        {
            if (Components == null)
            {
                Components = new List<ComponentByDirectory>();
            }

            Components.Add(component);
        }

        public override void RemoveComponent(ComponentByDirectory component)
        {

            if (Components != null && Components.Any() && component != null)
            {
                Components.Remove(component);
            }
        }
    }

    public class EmployeeByDirectory : ComponentByDirectory
    {
        public EmployeeByDirectory(string position, string job) : base(position, job)
        {

        }
        public override void CheckWork()
        {
            base.DoWork();
        }

        public override void AddComponent(ComponentByDirectory manager)
        {
            throw new NotImplementedException();
        }

        public override void RemoveComponent(ComponentByDirectory manager)
        {
            throw new NotImplementedException();
        }
    }


    public class ClientComponmentDirectory
    {

        public static void Test()
        {

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("组合模式-透明方式");
            Console.WriteLine();

            var boss = new ManagerByDirectory("老板", "唱怒放的生命");

            var HR = new EmployeeByDirectory("人力资源", "聊微信");
            var PM = new ManagerByDirectory("项目经理", "不知道干啥");
            var CFO = new ManagerByDirectory("财务主管", "看剧");

            var CTO = new ManagerByDirectory("技术主管", "划水");
            var UI = new EmployeeByDirectory("设计师", "画画");
            var operater = new EmployeeByDirectory("运营人员", "兼职客服");

            var webProgrammer = new EmployeeByDirectory("程序员", "学习设计模式");
            var backgroundProgrammer = new EmployeeByDirectory("后台程序员", "CRUD");

            var accountant = new EmployeeByDirectory("会计", "背九九乘法表");
            var clerk = new EmployeeByDirectory("文员", "给老板递麦克风");

            boss.AddComponent(HR);
            boss.AddComponent(PM);
            boss.AddComponent(CFO);

            PM.AddComponent(UI);
            PM.AddComponent(CTO);
            PM.AddComponent(operater);

            CTO.AddComponent(webProgrammer);
            CTO.AddComponent(backgroundProgrammer);

            CFO.AddComponent(accountant);
            CFO.AddComponent(clerk);

            boss.CheckWork();
        }
    }
    #endregion



    #region 组合模式-安全方式(不在同一实现接口中添加方法，有些实现类某些功能方法放到自己的类中实现)

    /*
     安全方式：在 ComponentBySelf 中不声明 add 和 remove 等管理子对象的方法，这样叶节点就无需实现它，只需在枝节点中实现管理子对象的方法即可。

    安全方式遵循了接口隔离原则，但由于不够透明，Manager 和 Employee 类不具有相同的接口，在客户端中，我们无法将 Manager 和 Employee 统一声明为 Component 类了，必须要区别对待，带来了使用上的不方便。

    安全方式和透明方式各有好处，在使用组合模式时，需要根据实际情况决定。但大多数使用组合模式的场景都是采用的透明方式，虽然它有点不安全，但是客户端无需做任何判断来区分是叶子结点还是枝节点，用起来是真香。
     
     */

    public abstract class ComponentBySelf
    {
        public string Position { get; set; }
        public string Job { get; set; }


        public ComponentBySelf(string position, string job)
        {
            this.Position = position;
            this.Job = job;
        }
        public virtual void DoWork()
        {
            Console.WriteLine($"我是{Position}，我的工作是{Job}");
        }
    }

    public class ManagerBySelf : ComponentBySelf
    {


        /// <summary>
        /// 管理的管理者
        /// </summary>
        public List<ManagerBySelf> Managers { get; set; }

        /// <summary>
        /// 管理的员工
        /// </summary>
        public List<EmployeeBySelf> Employees { get; set; }

        public ManagerBySelf(string position, string job) : base(position, job)
        {

        }

        /// <summary>
        /// 检查下属工作
        /// </summary>
        public void CheckWork()
        {
            DoWork();

            if (Employees != null && Employees.Any())
            {
                Employees.ForEach(t =>
                {
                    t.DoWork();
                });
            }

            if (Managers != null && Managers.Any())
            {
                Managers.ForEach(t =>
                {
                    t.CheckWork();
                });
            }
        }

        public void AddComponentManager(ManagerBySelf component)
        {
            if (Managers == null)
            {
                Managers = new List<ManagerBySelf>();
            }

            Managers.Add(component);
        }

        public void RemoveComponentManager(ManagerBySelf component)
        {
            if (Managers != null && Managers.Any() && component != null)
            {
                Managers.Remove(component);
            }
        }


        public void AddComponentEmployee(EmployeeBySelf component)
        {
            if (Employees == null)
            {
                Employees = new List<EmployeeBySelf>();
            }

            Employees.Add(component);
        }

        public void RemoveComponentEmployee(EmployeeBySelf component)
        {
            if (Employees != null && Employees.Any() && component != null)
            {
                Employees.Remove(component);
            }
        }
    }

    public class EmployeeBySelf : ComponentBySelf
    {
        public EmployeeBySelf(string position, string job) : base(position, job)
        {

        }
    }


    public class ClientComponmentSelf
    {

        public static void Test()
        {

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("组合模式-安全方式");
            Console.WriteLine();

            var boss = new ManagerBySelf("老板", "唱怒放的生命");

            var HR = new EmployeeBySelf("人力资源", "聊微信");
            var PM = new ManagerBySelf("项目经理", "不知道干啥");
            var CFO = new ManagerBySelf("财务主管", "看剧");

            var CTO = new ManagerBySelf("技术主管", "划水");
            var UI = new EmployeeBySelf("设计师", "画画");
            var operater = new EmployeeBySelf("运营人员", "兼职客服");

            var webProgrammer = new EmployeeBySelf("程序员", "学习设计模式");
            var backgroundProgrammer = new EmployeeBySelf("后台程序员", "CRUD");

            var accountant = new EmployeeBySelf("会计", "背九九乘法表");
            var clerk = new EmployeeBySelf("文员", "给老板递麦克风");

            boss.AddComponentEmployee(HR);
            boss.AddComponentManager(PM);
            boss.AddComponentManager(CFO);

            PM.AddComponentEmployee(UI);
            PM.AddComponentManager(CTO);
            PM.AddComponentEmployee(operater);

            CTO.AddComponentEmployee(webProgrammer);
            CTO.AddComponentEmployee(backgroundProgrammer);

            CFO.AddComponentEmployee(accountant);
            CFO.AddComponentEmployee(clerk);

            boss.CheckWork();
        }
    }

    #endregion 
}
