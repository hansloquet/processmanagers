using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp;

namespace Messages
{
    public class AssistentManager : HandleOrder
    {
        private readonly HandleOrder _handleOrder;


        public AssistentManager(HandleOrder handleOrder)
        {
            _handleOrder = handleOrder;
        }
        public void Handle(Order order)
        {
            Thread.Sleep(5000);
            _handleOrder.Handle(order);
        }
    }
}
