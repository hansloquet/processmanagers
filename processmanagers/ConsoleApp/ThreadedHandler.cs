using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp
{
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
}