using System.Collections.Generic;

namespace ConsoleApp
{
    public class RoundRobin : IHandleOrder
    {
        private readonly Queue<IHandleOrder> _handlers;

        public RoundRobin(params IHandleOrder[] handlers)
        {
            _handlers = new Queue<IHandleOrder>(handlers);
        }

        public RoundRobin(IEnumerable<IHandleOrder> handlers)
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