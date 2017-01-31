using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var orderPubSub = new TopicBasedOrderPubSub();
            var pubSub = new TopicBasedPubSub();

            var random = new Random();
            var cashier = new Cashier(pubSub);
            var printer = new OrderPrinter();

            var assistantManagers = Enumerable.Range(0, 2)
                .Select(index => new AssistantManager(pubSub))
                .Select(manager => new ThreadedHandler<OrderCooked>("Assistant", manager))
                .ToList();
            var assistantManager = new RoundRobin<OrderCooked>(assistantManagers);

//            var cooks = Enumerable.Range(0, 3)
//                .Select(index => assistantManager)
//                .Select(managers => new Cook(random.Next(1000, 4000), pubSub))
//                .Select(cook => new TtlOrderChecker(cook))
//                .Select(checker => new ThreadedOrderHandler("Cook", checker))
//                .ToList();

//            var kitchenDispatcher = new ThreadedOrderHandler("More Fair Handler", new MoreFairHandler(cooks));
            var cook = new ThreadedHandler<OrderPlaced>("Cook", new Cook(500, pubSub));

            var waiter = new Waiter(pubSub);

            // subscribe
//            orderPubSub.Subscribe<OrderPlaced>(new Cook(500, pubSub));
            pubSub.Subscribe(cook);
            pubSub.Subscribe(assistantManager);
            pubSub.Subscribe(cashier);
            pubSub.Subscribe(printer);

            //orderpaid
            
//            kitchenDispatcher.Start();
//            foreach (var cook in cooks)
//            {
                cook.Start();
//            }

            foreach (var manager in assistantManagers.Cast<ThreadedHandler<OrderCooked>>())
            {
                manager.Start();
            }

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Console.WriteLine("*******************");
//                    Console.WriteLine($"{kitchenDispatcher.Name} {kitchenDispatcher.Wip}");
//                    foreach (var cook in cooks)
//                    {
                        Console.WriteLine($"{cook.Name} - WIP: {cook.Wip} - DONE: {cook.Done}");
//                    }
                    foreach (var manager in assistantManagers.Cast<ThreadedHandler<OrderCooked>>())
                    {
                        Console.WriteLine($"{manager.Name} - WIP: {manager.Wip} - DONE: {manager.Done}");
                    }
                    Console.WriteLine($"Cashier - DONE: {cashier.Done}");
                    Console.WriteLine($"Paid - DONE: {printer.Done} - Total income: {printer.Total}");
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