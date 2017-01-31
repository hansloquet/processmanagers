using System.Threading;

namespace ConsoleApp
{
    public class Cashier : IHandleOrder, IHandle<OrderCalculated>
    {
        private readonly IPublisher _publisher;

        public Cashier(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public int Done { get; set; }

        public void Handle(Order order)
        {
            Thread.Sleep(1000);
            _publisher.Publish(new OrderPaid(order));
            Done++;
        }

        public void Handle(OrderCalculated message)
        {
            Handle(message.Order);
        }
    }
}