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

        private static int counter = 1;
        public Guid PlaceOrder()
        {
            var order = new Order();
            order.TableNumber = counter++;
            order.AddItem(3, "French Fries", 1);
            _handleOrder.Handle(order);
            return Guid.Empty;
        }
    }
}