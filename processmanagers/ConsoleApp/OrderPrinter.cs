using System;
using Newtonsoft.Json;

namespace ConsoleApp
{
    public class OrderPrinter : HandleOrder
    {
        public void Handle(Order order)
        {
            Console.Write(JsonConvert.SerializeObject(order));
        }
    }
}