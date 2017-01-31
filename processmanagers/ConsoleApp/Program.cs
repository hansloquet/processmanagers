using System;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var printer = new OrderPrinter();
            var cashier = new Cashier(printer);

            var assistentManager1 = new ThreadedHandler(new AssistantManager(cashier));
            var assistentManager2 = new ThreadedHandler(new AssistantManager(cashier));
            var assistentManagers = new RoundRobin(assistentManager1, assistentManager2);

            var tom = new ThreadedHandler(new Cook("Tom", assistentManagers));
            var basil = new ThreadedHandler( new Cook("Basil", assistentManagers));
            var frank = new ThreadedHandler( new Cook("Frank", assistentManagers));
            var cooks = new RoundRobin(tom, basil, frank);

            var waiter = new Waiter(cooks);

            tom.Start();
            basil.Start();
            frank.Start();

            assistentManager1.Start();
            assistentManager2.Start();

            for (var i = 0; i < 10; i++)
            {
                waiter.PlaceOrder();
            }

            Console.ReadLine();
        }
    }

    internal interface IStartable
    {
        void Start();
    }
}