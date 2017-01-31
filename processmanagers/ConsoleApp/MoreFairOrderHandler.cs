using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp
{
    internal class MoreFairHandler<T> : IHandle<T>
    {
        private Queue<ThreadedHandler<T>> _handlers;

        public MoreFairHandler(List<ThreadedHandler<T>> handlers)
        {
            _handlers = new Queue<ThreadedHandler<T>>(handlers);
        }

        public void Handle(T message)
        {
            while (true)
            {
                var handler = _handlers.Peek();
                _handlers.Enqueue(_handlers.Dequeue());
                if(handler.Wip < 5)
                {
                    handler.Handle(message);
                    return;
                }
                Thread.Sleep(2000);
            }
        }
    }
    internal class MoreFairOrderHandler : IHandleOrder
    {
        private readonly Queue<ThreadedOrderHandler> _handlers;

        public MoreFairOrderHandler(IEnumerable<ThreadedOrderHandler> handlers)
        {
            _handlers = new Queue<ThreadedOrderHandler>(handlers);
        }

        public void Handle(Order order)
        {
            while (true)
            {
                var handler = _handlers.Peek();
                _handlers.Enqueue(_handlers.Dequeue());
                if(handler.Wip < 5)
                {
                    handler.Handle(order);
                    return;
                }
                Thread.Sleep(2000);
            }
        }
    }
}