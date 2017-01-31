namespace ConsoleApp
{
    public class OrderPlaced
    {
        public OrderPlaced(Order order)
        {
            Order = order;
        }

        public Order Order { get; set; }
    }
}