using System;
using System.Threading;

namespace ProcessManagers
{
    public class Cook : IHandle<CookFood>
    {
        private readonly int _millisecondsTimeout;
        private readonly IPublisher _pubSub;

        public Cook(int timeOut, IPublisher pubSub)
        {
            _pubSub = pubSub;
            _millisecondsTimeout = timeOut;
        }

        public void Handle(CookFood message)
        {
            Thread.Sleep(_millisecondsTimeout);

            var order = message.Order;
            order.Ingredients.Add($"potatoes");
            _pubSub.Publish(new OrderCooked(order, message));
        }
    }

    public class CookFood : Message
    {
        public Order Order { get; private set; }

        public CookFood(Guid correlationId, Guid causeId) : base(correlationId, causeId)
        {
        }
    }
}
