using System.Threading;

namespace ConsoleApp
{
    public class Cook : IHandleOrder
    {
        private readonly int _millisecondsTimeout;
        private readonly IPublisher _publisher;

        public Cook(int timeOut, IPublisher publisher)
        {
            
            _publisher = publisher;
            _millisecondsTimeout = timeOut;
        }

        public void Handle(Order order)
        {
            Thread.Sleep(_millisecondsTimeout);
            order.Ingredients.Add($"potatoes");
            _publisher.Publish("OrderCooked", order);
        }
    }
}
