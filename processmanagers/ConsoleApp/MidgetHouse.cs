using System;
using System.Collections.Generic;

namespace ProcessManagers
{
    internal class MidgetHouse : IHandle<OrderPlaced>, IHandle<Message>
    {
        private readonly TopicBasedPubSub _pubSub;
        private readonly Dictionary<Guid, ProcessManager> _processManagers = new Dictionary<Guid, ProcessManager>();

        public MidgetHouse(TopicBasedPubSub pubSub)
        {
            _pubSub = pubSub;
        }

        public IHandle<Message> Wrapper { get; set; }

        public void Handle(OrderPlaced message)
        {
            var processManager = new ProcessManager(_pubSub);
            _pubSub.Subscribe(message.CorrelationId, Wrapper);
            _processManagers[message.CorrelationId] = processManager;
        }

        public void Handle(Message message)
        {
            var processManager = _processManagers[message.CorrelationId];
            processManager.Handle(message);
        }
    }

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