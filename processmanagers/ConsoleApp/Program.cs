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
}