using System;

namespace ConsoleApp
{
    public class Waiter
    {


        public Waiter(IOrderPublisher publisher)
        {
           
            _publisher = publisher;
        }

        private static int _counter = 1;
        private IOrderPublisher _publisher;

        public Guid PlaceOrder()
        {
            var order = new Order {TableNumber = _counter++};
            order.AddItem(3, "French Fries", 1);
    
            _publisher.Publish<OrderPlaced>(order);
            return Guid.Empty;
        }
    }
}