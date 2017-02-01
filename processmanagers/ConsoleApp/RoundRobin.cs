using System.Collections.Generic;

namespace ProcessManagers
{
    internal class RoundRobin<T> : IHandle<T>
    {
        private readonly Queue<IHandle<T>> _handlers ;

        public RoundRobin(IEnumerable<IHandle<T>> handlers)
        {
            _handlers = new Queue<IHandle<T>>(handlers);
        }

        public void Handle(T message)
        {
            var handler = _handlers.Dequeue();
            handler.Handle(message);
            _handlers.Enqueue(handler);
        }
    }
}