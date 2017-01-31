using System.Threading;

namespace ConsoleApp
{
    public class Cook : IHandleOrder
    {
        private readonly IHandleOrder _handleOrder;

        public Cook(IHandleOrder handleOrder)
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
