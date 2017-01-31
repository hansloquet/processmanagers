﻿using System.Collections.Generic;

namespace ConsoleApp
{
    internal class TopicBasedPubSub : IPublisher
    {
        private readonly Dictionary<string, List<IHandleOrder>> _topics = new Dictionary<string, List<IHandleOrder>>();
        private readonly object _lock = new object();

        public void Publish(string topic, Order order)
        {
            _topics[topic].ForEach(subscriber => subscriber.Handle(order));
        }

        public void Subscribe(string topic, IHandleOrder subscriber)
        {
            lock (_lock)
            {
                if (_topics.ContainsKey(topic))
                {
                    var subscribers = _topics[topic];
                    var newSubscribers = new List<IHandleOrder>(subscribers) { subscriber };
                    _topics[topic] = newSubscribers;
                }
                else
                {
                    _topics.Add(topic, new List<IHandleOrder> { subscriber });
                }
            }


        }
    }
}