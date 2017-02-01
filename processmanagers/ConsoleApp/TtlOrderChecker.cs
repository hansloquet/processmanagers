using System;

namespace ProcessManagers
{
    internal class TtlChecker<T> : IHandle<T>
    {
        private readonly IHandle<T> _handler;

        public TtlChecker(IHandle<T> handler)
        {
            _handler = handler;
        }

        public void Handle(T message)
        {
            var ttlMessage = message as IHaveATimeToLive;
            if (ttlMessage != null && ttlMessage.DueTime < DateTime.Now)
            {
                Console.WriteLine("DROPPING ORDER!");
            }
            else
            {
                _handler.Handle(message);
            }
        }
    }
}