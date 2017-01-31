using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class Order
    {
        public int TableNumber { get; set; }
        public List<OrderItem> Items { get; set; }
        public int SubTotal { get; set; }
        public Guid OrderId { get; set; }
        public List<object> Ingredients { get; set; } = new List<object>();
        public int Tax { get; set; }
        public int Total { get; set; }

        public void AddItem(int qty, string description, int unitPrice)
        {
            Items.Add(new OrderItem {Qty = qty, Description = description, UnitPrice = unitPrice});
        }
    }

    public class OrderItem
    {
        public int Qty { get; set; }
        public string Description { get; set; }
        public int UnitPrice { get; set; }
    }
}
