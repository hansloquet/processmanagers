namespace ProcessManagers
{
    internal class ProcessManager : IHandle<Message>,
        IHandle<OrderPlaced>,
        IHandle<OrderCooked>,
        IHandle<OrderCalculated>,
        IHandle<OrderPaid>
    {
        private readonly IPublisher _publisher;

        public ProcessManager(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(Message message)
        {
//            Console.WriteLine($"Message recieved {message.GetType().Name} - {message.CorrelationId}");
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
            _publisher.Publish(new PriceOrder(message.Order, message.CorrelationId, message.Id));
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