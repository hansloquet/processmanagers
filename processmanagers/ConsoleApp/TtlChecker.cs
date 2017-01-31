using System;

namespace ConsoleApp
{
    internal class TtlChecker : IHandleOrder
    {
        private readonly IHandleOrder _handler;

        public TtlChecker(IHandleOrder handler)
        {
            _handler = handler;
        }

        public void Handle(Order order)
        {
            if (order.DueTime < DateTime.Now)
            {
                Console.WriteLine("DROPPING ORDER!");
            }
            else
            {
                _handler.Handle(order);
            }
        }
    }
}