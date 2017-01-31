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
            var topicBasedPubSub = new TopicBasedPubSub();

            var random = new Random();
            var printer = new OrderPrinter();
            var cashier = new Cashier(printer);


            var assistantManagers = Enumerable.Range(0, 2)
                .Select(index => new AssistantManager(cashier))
                .Select(manager => new ThreadedHandler("Assistant", manager))
                .ToList();

            var cooks = Enumerable.Range(0, 3)
                .Select(index => new RoundRobin(assistantManagers))
                .Select(managers => new Cook(random.Next(0, 4000), managers))
                .Select(cook => new TtlChecker(cook))
                .Select(checker => new ThreadedHandler("Cook", checker))
                .ToList();

            var orderDispatcher = new ThreadedHandler("More Fair Handler", new MoreFairHandler(cooks));
            topicBasedPubSub.Subscribe("OrderPlaced", orderDispatcher);

            var waiter = new Waiter(topicBasedPubSub);


            orderDispatcher.Start();
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
                    Console.WriteLine($"{orderDispatcher.Name} {orderDispatcher.Wip}");
                    foreach (var cook in cooks)
                    {
                        Console.WriteLine($"{cook.Name} - WIP: {cook.Wip} - DONE: {cook.Done}");
                    }
                    foreach (var manager in assistantManagers)
                    {
                        Console.WriteLine($"{manager.Name} - WIP: {manager.Wip} - DONE: {manager.Done}");
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