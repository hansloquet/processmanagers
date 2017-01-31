using System;
using Newtonsoft.Json;

namespace ConsoleApp
{
    public class OrderPrinter : IHandleOrder
    {
        public void Handle(Order order)
        {
            Console.WriteLine(JsonConvert.SerializeObject(order));
        }
    }
}