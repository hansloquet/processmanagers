using System;

namespace ConsoleApp
{
    internal class TtlOrderChecker : IHandleOrder
    {
        private readonly IHandleOrder _handler;

        public TtlOrderChecker(IHandleOrder handler)
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