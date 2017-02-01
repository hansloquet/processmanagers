using System;

namespace ProcessManagers
{
    internal class ErraticPubSub : TopicBasedPubSub
    {
        private readonly TopicBasedPubSub _topicBasedPubSub;
        private readonly Random _random = new Random();

        public ErraticPubSub(TopicBasedPubSub topicBasedPubSub)
        {
            _topicBasedPubSub = topicBasedPubSub;
        }

        public override void Publish<T>(T message)
        {
            if (_random.Next(100) < 5)
            {
                Console.WriteLine("***************");
                Console.WriteLine($"{message.GetType().Name} MESSAGE LOST");
                Console.WriteLine("***************");
                return;
            }
            base.Publish(message);

            if (_random.Next(100) > 95)
            {
                Console.WriteLine("***************");
                Console.WriteLine($"{message.GetType().Name} MESSAGE DUPLICATED");
                Console.WriteLine("***************");
                base.Publish(message);
            }
        }
    }
}