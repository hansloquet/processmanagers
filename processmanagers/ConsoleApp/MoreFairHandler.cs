using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp
{
    internal class MoreFairHandler : IHandleOrder
    {
        private readonly Queue<ThreadedHandler> _handlers;

        public MoreFairHandler(IEnumerable<ThreadedHandler> handlers)
        {
            _handlers = new Queue<ThreadedHandler>(handlers);
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