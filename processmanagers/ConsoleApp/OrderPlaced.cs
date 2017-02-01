using System;

namespace ProcessManagers
{
    public class OrderPlaced : Message
    {
        public Order Order { get; }

        public OrderPlaced(Order order) : base(Guid.NewGuid(), Guid.Empty)
        {
            Order = order;
        }

        public DateTime DueTime => Order.DueTime;
    }
}