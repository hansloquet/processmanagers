﻿using System;

namespace ConsoleApp
{
    public class Waiter
    {
        private readonly IHandleOrder _handleOrder;

        public Waiter(IHandleOrder handleOrder)
        {
            _handleOrder = handleOrder;
        }

        private static int _counter = 1;
        public Guid PlaceOrder()
        {
            var order = new Order {TableNumber = _counter++};
            order.AddItem(3, "French Fries", 1);
            _handleOrder.Handle(order);
            return Guid.Empty;
        }
    }
}