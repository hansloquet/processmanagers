namespace ConsoleApp
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