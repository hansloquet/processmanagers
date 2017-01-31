using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var random = new Random();
            var printer = new OrderPrinter();
            var cashier = new Cashier(printer);


            var assistantManagers = new[] {"Assistent 1", "Assistent 2"}
                .Select(name => new Tuple<string, IHandleOrder>(name, new AssistantManager(cashier)))
                .Select(tuple => new ThreadedHandler(tuple.Item1, tuple.Item2)).ToList();

            var cooks = new[] {"Tom", "Basil", "Frank", "Jef"}
                .Select(name => new Tuple<string, IHandleOrder>(name, new RoundRobin(assistantManagers)))
                .Select(tuple => new Tuple<string, IHandleOrder>(tuple.Item1, new Cook(random.Next(0, 4000), tuple.Item2)))
                .Select(tuple => new ThreadedHandler(tuple.Item1, tuple.Item2))
                .ToList();

            var waiter = new Waiter(new RoundRobin(cooks));

            foreach (var cook in cooks)
            {
                cook.Start();
            }

            foreach (var assistentManager in assistantManagers)
            {
                assistentManager.Start();
            }

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Console.WriteLine("*******************");
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