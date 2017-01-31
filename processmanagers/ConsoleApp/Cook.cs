using System.Threading;

namespace ConsoleApp
{
    public class Cook : IHandleOrder
    {
        private readonly IHandleOrder _handleOrder;
        private readonly int _millisecondsTimeout;

        public Cook(int timeOut, IHandleOrder handleOrder)
        {
            _handleOrder = handleOrder;
            _millisecondsTimeout = timeOut;
        }

        public void Handle(Order order)
        {
            Thread.Sleep(_millisecondsTimeout);
            order.Ingredients.Add($"potatoes");
            _handleOrder.Handle(order);
        }
    }
}
