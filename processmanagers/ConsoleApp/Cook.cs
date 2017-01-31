using System.Threading;

namespace ConsoleApp
{
    public class Cook : IHandleOrder
    {
        private readonly string _name;
        private readonly IHandleOrder _handleOrder;

        public Cook(string name, IHandleOrder handleOrder)
        {
            _name = name;
            _handleOrder = handleOrder;
        }

        public void Handle(Order order)
        {
            Thread.Sleep(1000);
            order.Ingredients.Add("potatoes");
            _handleOrder.Handle(order);
        }
    }
}
