using System.Threading;
using Messages;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var printer = new OrderPrinter();
            var cashier = new Cashier(printer);
            var assistentManager = new AssistentManager(cashier);
            var cook = new Cook(assistentManager);
            var waiter = new Waiter(cook);

            for (var i = 0; i < 10; i++)
            {
                waiter.PlaceOrder();
            }
        }
    }

    internal class Cashier : HandleOrder
    {
        private readonly HandleOrder _handleOrder;

        public Cashier(HandleOrder handleOrder)
        {
            _handleOrder = handleOrder;
    
        }

        public void Handle(Order order)
        {
            Thread.Sleep(2000);
           _handleOrder.Handle(order);
        }
    }
}