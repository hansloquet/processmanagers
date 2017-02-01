using System;
using System.Collections.Generic;

namespace ProcessManagers
{
    internal class TopicBasedPubSub : IPublisher
    {
        private readonly Dictionary<string, List<IHandler>> _handlers = new Dictionary<string, List<IHandler>>();
        private readonly object _lock = new object();

        private void Publish<T>(string topic, T message)
        {
            if (_handlers.ContainsKey(topic))
                _handlers[topic].ForEach(handler =>
                {
                    var handle = handler as IHandle<T>;
                    handle.Handle(message);
                });
        }

        public virtual void Publish<T>(T message) where T : Message
        {
            Publish(message.GetType().Name, message);
            Publish<Message>(message.CorrelationId.ToString(), message);
        }

        public void Subscribe<T>(IHandle<T> handler)
        {
            lock (_lock)
            {
                if (_handlers.ContainsKey(typeof(T).Name))
                {
                    var handlers = _handlers[typeof(T).Name];
                    _handlers[typeof(T).Name] = new List<IHandler>(handlers) {handler};
                }
                else
                {
                    _handlers[typeof(T).Name] = new List<IHandler> {handler};
                }
            }
        }

        public void Subscribe(Guid correlationId, IHandle<Message> handler)
        {
            lock (_lock)
            {
                if (_handlers.ContainsKey(correlationId.ToString()))
                {
                    var handlers = _handlers[correlationId.ToString()];
                    _handlers[correlationId.ToString()] = new List<IHandler>(handlers) {handler};
                }
                else
                {
                    _handlers[correlationId.ToString()] = new List<IHandler> {handler};
                }
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
}