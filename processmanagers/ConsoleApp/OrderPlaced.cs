using System;

namespace ProcessManagers
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