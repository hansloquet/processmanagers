namespace ProcessManagers
{
    public class OrderPaid
    {
        public Order Order { get; }

        public OrderPaid(Order order)
        {
            Order = order;
        }
    }
}