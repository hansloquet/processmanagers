using System;

namespace ProcessManagers
{
    public class Waiter
    {
        private static int _counter = 1;
        private readonly IPublisher _publisher;

        public Waiter(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public Guid PlaceOrder()
        {
            var order = new Order {TableNumber = _counter++};
            order.AddItem(3, "French Fries", 1);

            var orderPlaced = new OrderPlaced(order);
            _publisher.Publish(orderPlaced);
            return orderPlaced.CorrelationId;
        }
    }
}