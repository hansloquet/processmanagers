using System;
using System.Linq;
using System.Threading;

namespace ProcessManagers
{
    public class AssistantManager : IHandle<CalculateOrder>
    {
        private readonly IPublisher _publisher;

        public AssistantManager(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(CalculateOrder message)
        {
            Thread.Sleep(1000);

            var order = message.Order;
            order.SubTotal = order.Items.Sum(item => item.UnitPrice + item.Qty);
            order.Tax = 0;
            order.Total = order.SubTotal + order.Tax;

            _publisher.Publish(new OrderCalculated(order, message));
        }
    }

    public class CalculateOrder : Message
    {
        public Order Order { get; private set; }

        public CalculateOrder(Order order, Guid correlationId, Guid causeId) : base(correlationId, causeId)
        {
            Order = order;
        }
    }
}
