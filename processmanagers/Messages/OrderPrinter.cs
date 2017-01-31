using System;
using Newtonsoft.Json;

namespace Messages
{
    public class OrderPrinter : HandleOrder
    {
        public void Handle(Order order)
        {
            Console.Write(JsonConvert.SerializeObject(order));
        }
    }
}