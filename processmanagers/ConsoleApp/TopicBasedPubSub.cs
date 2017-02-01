using System;
using System.Collections.Generic;

namespace ProcessManagers
{
    internal class TopicBasedPubSub : IPublisher
    {
        private readonly Dictionary<Type, List<IHandler>> _handlers =new Dictionary<Type, List<IHandler>>();
        private readonly object _lock = new object();
        private readonly Dictionary<Guid, List<IHandler>> _correlationHandlers = new Dictionary<Guid, List<IHandler>>();

        public void Publish<T>(T message) where T : Message
        {
            if (!_handlers.ContainsKey(typeof(T))) return;
            foreach (var handler in _handlers[typeof(T)])
            {
                ((IHandle<T>) handler).Handle(message);
            }

            if (!_correlationHandlers.ContainsKey(message.CorrelationId)) return;
            foreach (var handler in _correlationHandlers[message.CorrelationId])
            {
                ((IHandle<Message>) handler).Handle(message);
            }
        }

        public void Subscribe<T>(IHandle<T> handler)
        {
            if (_handlers.ContainsKey(typeof(T)))
            {
                lock (_lock)
                {
                    var handlers = _handlers[typeof(T)];
                    _handlers[typeof(T)] = new List<IHandler>(handlers) {handler};
                }
            }
            else
            {
                _handlers[typeof(T)] = new List<IHandler>{handler};
            }
        }

        public void Subscribe(Guid correlationId, IHandle<Message> handler)
        {
            if (_correlationHandlers.ContainsKey(correlationId))
            {
                lock (_lock)
                {
                    var handlers = _correlationHandlers[correlationId];
                    _correlationHandlers[correlationId] = new List<IHandler>(handlers) {handler};
                }
            }
            else
            {
                _correlationHandlers[correlationId] = new List<IHandler>{handler};
            }
        }
    }

    public interface IHandle<T> : IHandler
    {
        void Handle(T message);
    }

    public interface IHandler
    {
    }

    public abstract class GenericHandler<T> : IHandle<T> where T : Message
    {
        public abstract void Handle(T message);

        public void Handle(Message message)
        {
            Handle(message as T);
        }
    }
}