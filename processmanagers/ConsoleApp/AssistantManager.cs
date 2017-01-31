using System.Linq;
using System.Threading;

namespace ConsoleApp
{
    public class AssistantManager : IHandleOrder
    {
        private readonly IPublisher _publisher;

        public AssistantManager(IPublisher publisher)
        {
            _publisher = publisher;
        }
        public void Handle(Order order)
        {
            order.SubTotal = order.Items.Sum(item => item.UnitPrice + item.Qty);
            order.Tax = 0;
            order.Total = order.SubTotal + order.Tax;

            Thread.Sleep(1000);

            _publisher.Publish<OrderCalculated>(order);
        }
    }
}
