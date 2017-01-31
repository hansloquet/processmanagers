using System;

namespace ConsoleApp
{
    public class Waiter
    {
        private readonly IHandleOrder _handleOrder;

        public Waiter(IHandleOrder handleOrder)
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