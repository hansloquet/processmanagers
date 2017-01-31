namespace ConsoleApp
{
    public class OrderCalculated : Message
    {
        public Order Order { get; }

        public OrderCalculated(Order order)
        {
            Order = order;
        }
    }
}