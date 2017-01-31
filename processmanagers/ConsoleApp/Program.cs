﻿using System;
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
                .Select(index => new AssistantManager(orderPubSub))
                .Select(manager => new ThreadedHandler("Assistant", manager))
                .ToList();

           var assistantManagerDispatcher = new RoundRobin(assistantManagers);

            var cooks = Enumerable.Range(0, 3)
                .Select(index => assistantManagerDispatcher)
                .Select(managers => new Cook(random.Next(1000, 4000), orderPubSub))
                .Select(cook => new TtlChecker(cook))
                .Select(checker => new ThreadedHandler("Cook", checker))
                .ToList();

            var kitchenDispatcher = new ThreadedHandler("More Fair Handler", new MoreFairHandler(cooks));

            var waiter = new Waiter(orderPubSub);

            // subscribe
            orderPubSub.Subscribe<OrderPlaced>(kitchenDispatcher);
            orderPubSub.Subscribe<OrderCooked>(assistantManagerDispatcher);
            orderPubSub.Subscribe<OrderCalculated>(cashier);
            pubSub.Subscribe(printer);

            //orderpaid
            
            kitchenDispatcher.Start();
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
                    Console.WriteLine($"{kitchenDispatcher.Name} {kitchenDispatcher.Wip}");
                    foreach (var cook in cooks)
                    {
                        Console.WriteLine($"{cook.Name} - WIP: {cook.Wip} - DONE: {cook.Done}");
                    }
                    foreach (var manager in assistantManagers)
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