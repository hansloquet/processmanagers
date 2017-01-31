using System;
using Newtonsoft.Json;

namespace ConsoleApp
{
    public class OrderPrinter : IHandleOrder
    {
        private readonly Action<string> _action;

        public OrderPrinter(Action<string> action)
        {
            _action = action;
        }

        public void Handle(Order order)
        {
            _action("");
            _action(JsonConvert.SerializeObject(order, Formatting.Indented));
        }
    }
}