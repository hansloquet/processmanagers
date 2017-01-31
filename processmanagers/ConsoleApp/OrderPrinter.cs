namespace ConsoleApp
{
    public class OrderPrinter : IHandle<OrderPaid>
    {
        public int Done { get; set; }
        public int Total { get; set; }

        public void Handle(OrderPaid message)
        {
            Done++;
            Total += message.Order.Total;
        }
    }
}