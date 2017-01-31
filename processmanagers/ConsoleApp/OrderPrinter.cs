namespace ConsoleApp
{
    public class OrderPrinter : IHandle<OrderPaid>
    {
        public int Done { get; private set; }
        public int Total { get; private set; }

        public void Handle(OrderPaid message)
        {
            Done++;
            Total += message.Order.Total;
        }
    }
}