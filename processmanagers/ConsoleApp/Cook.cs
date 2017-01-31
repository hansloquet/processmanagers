using System.Threading;

namespace ConsoleApp
{
    public class Cook : IHandleOrder
    {
        private readonly int _millisecondsTimeout;
        private readonly IOrderPublisher _publisher;

        public Cook(int timeOut, IOrderPublisher publisher)
        {
            
            _publisher = publisher;
            _millisecondsTimeout = timeOut;
        }

        public void Handle(Order order)
        {
            Thread.Sleep(_millisecondsTimeout);
            order.Ingredients.Add($"potatoes");
            _publisher.Publish<OrderCooked>(order);
            //_publisher.Publish("OrderCooked", order);
        }
    }
}
