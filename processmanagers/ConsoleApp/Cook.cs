using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Messages
{
    public class Cook : HandleOrder
    {
        private readonly HandleOrder _handleOrder;

        public Cook(HandleOrder handleOrder)
        {
            _handleOrder = handleOrder;
        }

        public void Handle(Order order)
        {
            Thread.Sleep(2000);
            _handleOrder.Handle(order);
        }
    }
}
