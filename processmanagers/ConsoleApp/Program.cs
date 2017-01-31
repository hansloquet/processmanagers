using System;
using System.Collections.Generic;
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
            var assistentManager = new AssistentManager(cashier);
            var tom = new ThreadedHandler(new Cook("Tom", assistentManager));
            var basil = new ThreadedHandler( new Cook("Basil", assistentManager));
            var frank = new ThreadedHandler( new Cook("Frank", assistentManager));

            var repeater = new RoundRobin(tom, basil, frank);

            var waiter = new Waiter(repeater);

            tom.Start();
            basil.Start();
            frank.Start();

            for (var i = 0; i < 10; i++)
            {
                waiter.PlaceOrder();
            }

            Console.ReadLine();
        }
    }

    internal class ThreadedHandler : IHandleOrder, IStartable
    {
        private readonly IHandleOrder _handler;
        Queue<Order> orders = new Queue<Order>();

        public ThreadedHandler(IHandleOrder handler)
        {
            _handler = handler;
        }

        public void Handle(Order order)
        {
            orders.Enqueue(order);
        }

        public void Start()
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (orders.Count > 0)
                    {
                        _handler.Handle(orders.Dequeue());
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
            }).Start();
        }
    }

    internal interface IStartable

    {
        void Start();
    }
}