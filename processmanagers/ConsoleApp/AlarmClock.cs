using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessManagers
{
    internal class AlarmClock : IHandle<PublishAt>
    {
        private readonly List<PublishAt> _delayedMessages = new List<PublishAt>();
        private readonly IPublisher _publisher;

        public AlarmClock(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(PublishAt message)
        {
            _delayedMessages.Add(message);
        }

        public void Start()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    foreach (var message in _delayedMessages.Where(m => m.DueDate < DateTime.Now).ToList())
                    {
                        _publisher.Publish(message.DelayedMessage);
                        _delayedMessages.Remove(message);
                    }
                    Thread.Sleep(100);
                }
            },TaskCreationOptions.LongRunning);
        }
    }
}