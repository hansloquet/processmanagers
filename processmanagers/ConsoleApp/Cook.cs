using System.Threading;

namespace ConsoleApp
{
    public class Cook : IHandle<OrderPlaced>
    {
        private readonly int _millisecondsTimeout;
        private readonly IPublisher _pubSub;

        public Cook(int timeOut, IPublisher pubSub)
        {
            _pubSub = pubSub;
            _millisecondsTimeout = timeOut;
        }

        public void Handle(Order order)
        {
            Thread.Sleep(_millisecondsTimeout);
            order.Ingredients.Add($"potatoes");
            _pubSub.Publish(new OrderCooked(order));
        }

        public void Handle(OrderPlaced message)
        {
            Handle(message.Order);
        }
    }
}
