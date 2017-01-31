using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace processmanagers
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

    interface HandleOrder
    {
        void Handle(Order order);
    }

    public class OrderPrinter : HandleOrder
    {
        
    }
}
