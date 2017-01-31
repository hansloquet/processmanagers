using Messages;
using processmanagers;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            HandleOrder handle = new OrderPrinter();
            var waiter = new Waiter(handle);
            waiter.PlaceOrder();
            var order = new Order();
            handle.Handle(order);
            var printer = new OrderPrinter();
            printer.Handle(order);
        }
    }
}