using System;
using System.Collections.Generic;

namespace ProcessManagers
{
    public class Order
    {
        public int TableNumber { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public int SubTotal { get; set; }
        public Guid OrderId { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();
        public int Tax { get; set; }
        public int Total { get; set; }
        public TimeSpan CookingTime { get; set; }
        public DateTime DueTime { get; set; } = DateTime.Now.Add(TimeSpan.FromSeconds(10));

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
