using Messages;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
          

            var waiter = new Waiter(new Cook(new AssistentManager(new OrderPrinter())));
            
            waiter.PlaceOrder();
        }
    }
}