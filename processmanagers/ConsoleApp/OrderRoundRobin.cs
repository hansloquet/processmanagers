using System.Collections.Generic;

namespace ConsoleApp
{
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