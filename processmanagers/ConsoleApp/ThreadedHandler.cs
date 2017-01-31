using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class ThreadedHandler : IHandleOrder, IStartable
    {
        
        public int Wip => orders.Count;
        public string Name { get; private set; }

        private readonly IHandleOrder _handler;
        readonly ConcurrentQueue<Order> orders = new ConcurrentQueue<Order>();

        public ThreadedHandler(string name, IHandleOrder handler)
        {
            Name = name;
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
                            Order order;
                            if (orders.TryDequeue(out order))
                            {
                                _handler.Handle(order: order);
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