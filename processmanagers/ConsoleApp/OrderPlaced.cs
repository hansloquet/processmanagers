using System;

namespace ConsoleApp
{
    public class OrderPlaced : IHaveATimeToLive
    {
        public Order Order { get; }

        public OrderPlaced(Order order)
        {
            Order = order;
        }

        public DateTime DueTime => Order.DueTime;
    }
}