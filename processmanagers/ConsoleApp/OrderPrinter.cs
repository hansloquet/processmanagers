namespace ConsoleApp
{
    public class OrderPrinter : IHandleOrder
    {

        public void Handle(Order order)
        {
            Done++;
            Total += order.Total;
        }

        public int Done { get; set; }
        public int Total { get; set; }
    }
}