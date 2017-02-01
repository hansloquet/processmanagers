using System;
using System.Threading;

namespace ProcessManagers
{
    public class Cashier : IHandle<TakePayment>
    {
        private readonly IPublisher _publisher;

        public Cashier(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public int Done { get; set; }

        public void Handle(TakePayment message)
        {
            Thread.Sleep(1000);

            _publisher.Publish(new OrderPaid(message.Order, message));
            Done++;
        }
    }

    public class TakePayment : Message
    {
        public Order Order { get; private set; }

        public TakePayment(Guid correlationId, Guid causeId) : base(correlationId, causeId)
        {
        }
    }
}