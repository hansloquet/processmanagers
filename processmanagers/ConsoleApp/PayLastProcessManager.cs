namespace ProcessManagers
{
    internal interface IProcessManager: IHandle<Message>,
        IHandle<OrderPlaced>,
        IHandle<OrderCooked>,
        IHandle<OrderCalculated>,
        IHandle<OrderPaid>
    {
    }

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
        private readonly IPublisher _publisher;

        public PayLastProcessManager(IPublisher publisher)
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
    }
}