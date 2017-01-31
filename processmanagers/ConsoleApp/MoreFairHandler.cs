using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp
{
    internal class MoreFairHandler<T> : IHandle<T>
    {
        private readonly Queue<ThreadedHandler<T>> _handlers;

        public MoreFairHandler(IEnumerable<ThreadedHandler<T>> handlers)
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
}