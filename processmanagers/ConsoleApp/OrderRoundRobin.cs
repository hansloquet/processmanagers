using System.Collections.Generic;

namespace ConsoleApp
{
    internal class RoundRobin<T> : IHandle<T>
    {
        private readonly Queue<IHandle<T>> _handlers ;

        public RoundRobin(List<IHandle<T>> handlers)
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
    public class OrderRoundRobin : IHandleOrder
    {
        private readonly Queue<IHandleOrder> _handlers;

        public OrderRoundRobin(params IHandleOrder[] handlers)
        {
            _handlers = new Queue<IHandleOrder>(handlers);
        }

        public OrderRoundRobin(IEnumerable<IHandleOrder> handlers)
        {
            _handlers = new Queue<IHandleOrder>(handlers);
        }

        public void Handle(Order order)
        {
            _handlers.Peek().Handle(order);
            _handlers.Enqueue(_handlers.Dequeue());
        }
    }
}