using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class ThreadedHandler<TMessage> : IHandle<TMessage>, IStartable
    {
        public int Wip => _messages.Count;
        public string Name { get; private set; }
        public int Done { get; private set; }

        private readonly IHandle<TMessage> _handler;
        readonly ConcurrentQueue<TMessage> _messages = new ConcurrentQueue<TMessage>();

        public ThreadedHandler(string name, IHandle<TMessage> handler)
        {
            _handler = handler;
            Name = name;
        }

        public void Handle(TMessage message)
        {
            _messages.Enqueue(message);
        }

        public void Start()
        {
            Task.Factory
                .StartNew(() =>
                    {
                        while (true)
                        {
                            TMessage message;
                            if (_messages.TryDequeue(out message))
                            {
                                _handler.Handle(message);
                                Done++;
                            }
                            else
                            {
                                Thread.Sleep(millisecondsTimeout: 1);
                            }
                        }
                    }, TaskCreationOptions.LongRunning);
        }
    }

    internal class ThreadedOrderHandler : IHandleOrder, IStartable
    {
        private static int _counter;
        public int Wip => orders.Count;
        public string Name { get; private set; }
        public int Done { get; private set; }

        private readonly IHandleOrder _handler;
        readonly ConcurrentQueue<Order> orders = new ConcurrentQueue<Order>();

        public ThreadedOrderHandler(string name, IHandleOrder handler)
        {
            Name = $"{name} {++_counter}";
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
                                Done++;
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