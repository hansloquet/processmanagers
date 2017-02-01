using System;
using System.Collections.Generic;

namespace ProcessManagers
{
    internal class TopicBasedPubSub : IPublisher
    {
        private readonly Dictionary<Type, List<IHandler>> _handlers;
        private readonly object _lock = new object();

        public TopicBasedPubSub()
        {
            _handlers = new Dictionary<Type, List<IHandler>>();
        }
        public void Publish<T>(T message)
        {
            if (!_handlers.ContainsKey(typeof(T))) return;

            foreach (var handler in _handlers[typeof(T)])
            {
                ((IHandle<T>) handler).Handle(message);
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
    }

    public interface IHandle<T> : IHandler
    {
        void Handle(T message);
    }

    public interface IHandler
    {
    }
}