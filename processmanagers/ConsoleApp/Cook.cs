using System.Threading;

namespace ConsoleApp
{
    public class Cook : IHandleOrder
    {
        private readonly string _name;
        private readonly IHandleOrder _handleOrder;
        private readonly int _millisecondsTimeout;

        public Cook(string name, int timeOut, IHandleOrder handleOrder)
        {
            _name = name;
            _handleOrder = handleOrder;
            _millisecondsTimeout = timeOut;
        }

        public void Handle(Order order)
        {
            Thread.Sleep(_millisecondsTimeout);
            order.Ingredients.Add($"potatoes by {_name}");
            _handleOrder.Handle(order);
        }
    }
}
