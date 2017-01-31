using System.Threading;
using Messages;

namespace ConsoleApp
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
