using System.Linq;
using System.Threading;

namespace ProcessManagers
{
    public class AssistantManager : IHandle<OrderCooked>
    {
        private readonly IPublisher _publisher;

        public AssistantManager(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(OrderCooked message)
        {
            Order order = message.Order;
            order.SubTotal = order.Items.Sum(item => item.UnitPrice + item.Qty);
            order.Tax = 0;
            order.Total = order.SubTotal + order.Tax;

            Thread.Sleep(1000);

            _publisher.Publish(new OrderCalculated(order, message));
        }
    }
}
