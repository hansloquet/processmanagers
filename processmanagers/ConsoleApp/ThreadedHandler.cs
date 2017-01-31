using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class ThreadedHandler : IHandleOrder, IStartable
    {
        private readonly IHandleOrder _handler;
        readonly Queue<Order> orders = new Queue<Order>();

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
            Task.Factory
                .StartNew(() =>
                    {
                        while (true)
                        {
                            if (orders.Any())
                            {
                                _handler.Handle(order: orders.Dequeue());
                            }
                            else
                            {
                                Thread.Sleep(millisecondsTimeout: 1);
                            }
                        }
                    }, TaskCreationOptions.LongRunning);

        }
    }
}