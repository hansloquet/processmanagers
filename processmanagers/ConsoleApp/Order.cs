using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class Order
    {
        public int TableNumber { get; set; }
        public List<object> Items { get; set; }
        public int SubTotal { get; set; }
        public Guid OrderId { get; set; }
        public List<object> Ingredients { get; set; }
        public int Tax { get; set; }
        public int Total { get; set; }
    }
}
