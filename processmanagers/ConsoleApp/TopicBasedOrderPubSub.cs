//using System;
//using System.Collections.Generic;
//
//namespace ConsoleApp
//{
//    internal class TopicBasedOrderPubSub : IOrderPublisher
//    {
//        private readonly Dictionary<string, List<IHandleOrder>> _topics = new Dictionary<string, List<IHandleOrder>>();
//        private readonly object _lock = new object();
//
//        public void Publish(string topic, Order order)
//        {
//            if(_topics.ContainsKey(topic))
//                _topics[topic].ForEach(subscriber => subscriber.Handle(order));
//        }
//
//        public void Subscribe(string topic, IHandleOrder subscriber)
//        {
//            lock (_lock)
//            {
//                if (_topics.ContainsKey(topic))
//                {
//                    var subscribers = _topics[topic];
//                    var newSubscribers = new List<IHandleOrder>(subscribers) { subscriber };
//                    _topics[topic] = newSubscribers;
//                }
//                else
//                {
//                    _topics.Add(topic, new List<IHandleOrder> { subscriber });
//                }
//            }
//
//
//        }
//
//        public void Publish<T>(Order order)
//        {
//            Publish(typeof(T).Name, order);
//        }
//
//        internal void Subscribe<T>(IHandleOrder subscriber)
//        {
//            Subscribe(typeof(T).Name, subscriber);
//        }
//    }
//}