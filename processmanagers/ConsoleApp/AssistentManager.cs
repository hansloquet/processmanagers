﻿using System.Threading;

namespace ConsoleApp
{
    public class AssistentManager : IHandleOrder
    {
        private readonly IHandleOrder _handleOrder;


        public AssistentManager(IHandleOrder handleOrder)
        {
            _handleOrder = handleOrder;
        }
        public void Handle(Order order)
        {
            Thread.Sleep(1000);
            _handleOrder.Handle(order);
        }
    }
}
