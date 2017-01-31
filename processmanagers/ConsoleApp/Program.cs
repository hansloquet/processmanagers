using System.Collections.Generic;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var printer = new OrderPrinter();
            var cashier = new Cashier(printer);
            var assistentManager = new AssistentManager(cashier);
            var tom = new Cook("Tom", assistentManager);
            var basil = new Cook("Basil", assistentManager);
            var frank = new Cook("Frank", assistentManager);
            var repeater = new RoundRobin(tom, basil, frank);
            var waiter = new Waiter(repeater);

            for (var i = 0; i < 10; i++)
            {
                waiter.PlaceOrder();
            }
        }
    }

    public class Repeater : IHandleOrder
    {
        private readonly IHandleOrder[] _handlers;

        public Repeater(params IHandleOrder[] handlers)
        {
            _handlers = handlers;
        }

        public void Handle(Order order)
        {
            foreach (var handler in _handlers)
            {
                handler.Handle(order);
            }
        }
    }

    public class RoundRobin : IHandleOrder
    {
        private readonly Queue<IHandleOrder> _handlers;

        public RoundRobin(params IHandleOrder[] handlers)
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