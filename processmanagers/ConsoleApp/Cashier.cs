using System.Linq;
using System.Threading;

namespace ConsoleApp
{
    public class Cashier : IHandleOrder
    {
        private readonly IHandleOrder _handleOrder;

        public Cashier(IHandleOrder handleOrder)
        {
            _handleOrder = handleOrder;
        }

        public int Done { get; set; }

        public void Handle(Order order)
        {
            Thread.Sleep(1000);
            _handleOrder.Handle(order);
            Done++;
        }
    }
}