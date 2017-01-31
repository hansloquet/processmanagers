using System.Threading;
using Messages;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
          

            var waiter = new Waiter(new Cook(new AssistentManager(new Cashier(new OrderPrinter()))));
            
            waiter.PlaceOrder();
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