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

        public void Handle(Order order)
        {
            order.SubTotal = order.Items.Sum(item => item.UnitPrice + item.Qty);
            order.Tax = 0;
            order.Total = order.SubTotal + order.Tax;

            Thread.Sleep(1000);
            _handleOrder.Handle(order);
        }
    }
}