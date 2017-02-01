using System;
using System.Collections.Generic;

namespace ProcessManagers
{
    internal class MidgetHouse : IHandle<OrderPlaced>, IHandle<Message>
    {
        private readonly TopicBasedPubSub _pubSub;
        private readonly Dictionary<Guid, ProcessManager> _processManagers = new Dictionary<Guid, ProcessManager>();
        private Action<Guid> _subscribeTo;

        public MidgetHouse(TopicBasedPubSub pubSub)
        {
            _pubSub = pubSub;
        }

        public void Handle(OrderPlaced message)
        {
            var processManager = new ProcessManager(_pubSub);
            _processManagers[message.CorrelationId] = processManager;
            _subscribeTo(message.CorrelationId);
        }

        public void Handle(Message message)
        {
            var processManager = _processManagers[message.CorrelationId];
            processManager.Handle(message);
        }

        public void SubscribeWith(Action<Guid> action)
        {
            _subscribeTo = action;
        }
    }
}