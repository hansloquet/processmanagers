using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var printer = new OrderPrinter();
            var cashier = new Cashier(printer);

            var assistentManager1 = new ThreadedHandler("Assistant1", new AssistantManager(cashier));
            var assistentManager2 = new ThreadedHandler("Assistant2", new AssistantManager(cashier));

            var assistentManagers = new RoundRobin(assistentManager1, assistentManager2);

            Random random = new Random();
            var tom = new ThreadedHandler("Cook1", new Cook("Tom", random.Next(0, 4000), assistentManagers));
            var basil = new ThreadedHandler("Cook2", new Cook("Basil", random.Next(0, 4000), assistentManagers));
            var frank = new ThreadedHandler("Cook3", new Cook("Frank", random.Next(0, 4000), assistentManagers));
            var cooks = new RoundRobin(tom, basil, frank);

            var waiter = new Waiter(cooks);

            assistentManager1.Start();
            assistentManager2.Start();

            tom.Start();
            basil.Start();
            frank.Start();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Console.WriteLine($"{tom.Name} {tom.Wip}");
                    Console.WriteLine($"{basil.Name} {basil.Wip}");
                    Console.WriteLine($"{frank.Name} {frank.Wip}");
                    Thread.Sleep(1000);
                }
            });

            for (var i = 0; i < 100; i++)
            {
                waiter.PlaceOrder();
            }

            Console.ReadLine();
        }
    }

    internal interface IStartable
    {
        void Start();
    }
}