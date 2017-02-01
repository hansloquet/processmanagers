using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessManagers
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var random = new Random();
            var pubSub = new TopicBasedPubSub();
            var waiter = new Waiter(pubSub);

            var timout = 1000;
            var cooks = Enumerable.Range(0, 3)
                .Select(index => new Cook(timout += 1000, pubSub))
                .Select(cook => new TtlChecker<CookFood>(cook))
                .Select(c => new ThreadedHandler<CookFood>("Cook", c))
                .ToList();
            var kitchenDispatcher = new ThreadedHandler<CookFood>("Kitchen Dispatcher", new MoreFairHandler<CookFood>(cooks));

            var assistantManagers = Enumerable.Range(0, 2)
                .Select(index => new AssistantManager(pubSub))
                .Select(manager => new ThreadedHandler<CalculateOrder>("Assistant", manager))
                .ToList();
            var assistantManager = new RoundRobin<CalculateOrder>(assistantManagers);

            var cashier = new Cashier(pubSub);
            var printer = new OrderPrinter();

            var midgetHouse = new MidgetHouse(pubSub);
            var threadedMidgetHouse = new ThreadedHandler<Message>("Midget House", midgetHouse);
            midgetHouse.SubscribeWith(correlationId => pubSub.Subscribe(correlationId, threadedMidgetHouse));

            // subscribe
            pubSub.Subscribe(kitchenDispatcher);
            pubSub.Subscribe(assistantManager);
            pubSub.Subscribe(cashier);
            pubSub.Subscribe(printer);
            pubSub.Subscribe<OrderPlaced>(midgetHouse);

            // start processes
            kitchenDispatcher.Start();
            foreach (var c in cooks)
            {
                c.Start();
            }

            foreach (var manager in assistantManagers)
            {
                manager.Start();
            }
            threadedMidgetHouse.Start();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Console.WriteLine("*******************");
                    Console.WriteLine($"{kitchenDispatcher.Name} {kitchenDispatcher.Wip}");
                    foreach (var c in cooks)
                    {
                        Console.WriteLine($"{c.Name} - WIP: {c.Wip} - DONE: {c.Done}");
                    }
                    foreach (var manager in assistantManagers)
                    {
                        Console.WriteLine($"{manager.Name} - WIP: {manager.Wip} - DONE: {manager.Done}");
                    }
                    Console.WriteLine($"Cashier - DONE: {cashier.Done}");
                    Console.WriteLine($"Paid - DONE: {printer.Done} - Total income: {printer.Total}");
                    Thread.Sleep(1000);
                }
            }, TaskCreationOptions.LongRunning);

            for (var i = 0; i < 100; i++)
            {
                waiter.PlaceOrder();
                Thread.Sleep(500);
            }

            Console.ReadLine();
        }
    }
}