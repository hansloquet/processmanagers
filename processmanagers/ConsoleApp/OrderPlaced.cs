namespace ConsoleApp
{
    public class OrderPlaced
    {
        public Order Order { get; }

        public OrderPlaced(Order order)
        {
            Order = order;
        }
    }
}