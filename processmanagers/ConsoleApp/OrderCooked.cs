namespace ConsoleApp
{
    public class OrderCooked : Message
    {
        public Order Order { get; }

        public OrderCooked(Order order)
        {
            Order = order;
        }
    }
}