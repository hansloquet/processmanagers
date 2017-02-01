using System.Threading;

namespace ProcessManagers
{
    public class Cashier : IHandle<OrderCalculated>
    {
        private readonly IPublisher _publisher;

        public Cashier(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public int Done { get; set; }

        public void Handle(OrderCalculated message)
        {
            Thread.Sleep(1000);
            _publisher.Publish(new OrderPaid(message.Order, message));
            Done++;
        }
    }
}