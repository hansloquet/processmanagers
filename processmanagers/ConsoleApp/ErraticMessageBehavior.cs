using System;

namespace ProcessManagers
{
    internal class ErraticMessageBehavior<T> : IHandle<T>
    {
        private readonly IHandle<T> _handler;
        private readonly Random _random = new Random();

        public ErraticMessageBehavior(IHandle<T> handler)
        {
            _handler = handler;
        }

        public void Handle(T message)
        {
            var aRandomNumber = _random.Next(100);
            if (aRandomNumber < 10) return;
            _handler.Handle(message);
            if (aRandomNumber > 90) return;
            _handler.Handle(message);
        }
    }
}