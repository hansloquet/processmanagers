using System;

namespace ConsoleApp
{
    public class Waiter
    {
        private readonly HandleOrder _handleOrder;

        public Waiter(HandleOrder handleOrder)
        {
            _handleOrder = handleOrder;
        }

        public Guid PlaceOrder()
        {
            var order = new Order();
            _handleOrder.Handle(order);
            return Guid.Empty;
        }
    }
}