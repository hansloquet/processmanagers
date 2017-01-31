using System;

namespace ConsoleApp
{
    public class Waiter
    {


        public Waiter(IPublisher publisher)
        {
           
            _publisher = publisher;
        }

        private static int _counter = 1;
        private IPublisher _publisher;

        public Guid PlaceOrder()
        {
            var order = new Order {TableNumber = _counter++};
            order.AddItem(3, "French Fries", 1);
    
            _publisher.Publish(new OrderPlaced(order));
            return Guid.Empty;
        }
    }
}