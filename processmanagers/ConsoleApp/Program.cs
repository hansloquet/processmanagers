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
            var repeater = new Repeater(tom, basil, frank);
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
}