using System.Threading;

namespace ProcessManagers
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

        public void Handle(OrderPlaced message)
        {
            Order order = message.Order;
            Thread.Sleep(_millisecondsTimeout);
            order.Ingredients.Add($"potatoes");
            _pubSub.Publish(new OrderCooked(order, message));
        }
    }
}
