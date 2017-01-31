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
                .Select(name_managers => new Tuple<string, IHandleOrder>(name_managers.Item1, new Cook(random.Next(0, 4000), name_managers.Item2)))
                .Select(name_cook => new Tuple<string, IHandleOrder>(name_cook.Item1, new TtlChecker(name_cook.Item2)))
                .Select(name_checker => new ThreadedHandler(name_checker.Item1, name_checker.Item2))
                .ToList();


            var threadedHandler = new ThreadedHandler("More Fair Handler", new MoreFairHandler(cooks));
            var waiter = new Waiter(threadedHandler);


            threadedHandler.Start();
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
                    Console.WriteLine($"{threadedHandler.Name} {threadedHandler.Wip}");
                    foreach (var cook in cooks)
                    {
                        Console.WriteLine($"{cook.Name} - WIP: {cook.Wip} - DONE: {cook.Done}");
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
}