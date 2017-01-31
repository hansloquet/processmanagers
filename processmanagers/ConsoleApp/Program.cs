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

            var random = new Random();

            var cooks = new[]
            {
                new ThreadedHandler("Tom", new Cook(random.Next(0, 4000), assistentManagers)),
                new ThreadedHandler("Basil", new Cook(random.Next(0, 4000), assistentManagers)),
                new ThreadedHandler("Frank", new Cook(random.Next(0, 4000), assistentManagers))
            };

            var cookDispatcher = new RoundRobin(cooks);

            var waiter = new Waiter(cookDispatcher);

            assistentManager1.Start();
            assistentManager2.Start();

            Task.Factory.StartNew(() =>
            {
                foreach (var cook in cooks)
                {
                    cook.Start();
                }
                while (true)
                {
                    foreach (var cook in cooks)
                    {
                        Console.WriteLine($"{cook.Name} {cook.Wip}");
                    }
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