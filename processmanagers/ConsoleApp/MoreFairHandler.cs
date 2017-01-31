using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp
{
    internal class MoreFairHandler : IHandleOrder
    {
        private readonly List<ThreadedHandler> _cooks;

        public MoreFairHandler(List<ThreadedHandler> cooks)
        {
            _cooks = cooks;
        }

        public void Handle(Order order)
        {
            while (true)
            {
                foreach (var cook in _cooks)
                {
                    if (cook.Wip < 2)
                    {
                        cook.Handle(order);
                        return;
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}