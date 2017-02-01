using System;

namespace ProcessManagers
{
    internal class PayFirstProcessManager : IProcessManager
    {
        private readonly IPublisher _publisher;

        public PayFirstProcessManager(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(Message message)
        {
            if(message is OrderPlaced) Handle(message as OrderPlaced);
            if(message is OrderCooked) Handle(message as OrderCooked);
            if(message is OrderCalculated) Handle(message as OrderCalculated);
            if(message is OrderPaid) Handle(message as OrderPaid);
        }

        public void Handle(OrderPlaced message)
        {
            _publisher.Publish(new CalculateOrder(message.Order, message.CorrelationId, message.Id));
        }

        public void Handle(OrderCalculated message)
        {
            _publisher.Publish(new TakePayment(message.Order, message.CorrelationId, message.Id));
        }

        public void Handle(OrderPaid message)
        {
            _publisher.Publish(new CookFood(message.Order, message.CorrelationId, message.Id));
        }

        public void Handle(OrderCooked message)
        {
            _publisher.Publish(new PrintOrder(message.Order, message.CorrelationId, message.Id));
        }
    }

    internal class PayLastProcessManager : IProcessManager
    {
        private IPublisher _publisher;

        public PayLastProcessManager(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(Message message)
        {
            if(message is OrderPlaced) Handle(message as OrderPlaced);
            if(message is OrderCooked) Handle(message as OrderCooked);
            if(message is CookFoodTimedOut) Handle(message as CookFoodTimedOut);
            if(message is OrderCalculated) Handle(message as OrderCalculated);
            if(message is OrderPaid) Handle(message as OrderPaid);
        }

        public void Handle(OrderPlaced message)
        {
            _publisher.Publish(new PublishAt(DateTime.Now.AddSeconds(5.0), new CookFoodTimedOut(message.Order, message.CorrelationId, message.Id)));
            _publisher.Publish(new CookFood(message.Order, message.CorrelationId, message.Id));
        }

        public void Handle(CookFoodTimedOut message)
        {
            if(message.Order.Cooked) return;
            Console.WriteLine("Wake up the cook");
            _publisher.Publish(new PublishAt(DateTime.Now.AddSeconds(5.0), new CookFoodTimedOut(message.Order, message.CorrelationId, message.Id)));
            _publisher.Publish(new CookFood(message.Order, message.CorrelationId, message.Id));
        }

        public void Handle(OrderCooked message)
        {
            _publisher.Publish(new CalculateOrder(message.Order, message.CorrelationId, message.Id));
        }

        public void Handle(OrderCalculated message)
        {
            _publisher.Publish(new TakePayment(message.Order, message.CorrelationId, message.Id));
        }

        public void Handle(OrderPaid message)
        {
            _publisher.Publish(new PrintOrder(message.Order, message.CorrelationId, message.Id));
        }

        public void SetPublisher(IPublisher publisher)
        {
            _publisher = publisher;
        }
    }

    internal class CookFoodTimedOut : Message
    {
        public CookFoodTimedOut(Order order, Guid correlationId, Guid causeId) : base(correlationId, causeId)
        {
            Order = order;
        }

        public Order Order { get; set; }
    }
}