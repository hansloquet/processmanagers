using System;

namespace ProcessManagers
{
    public class OrderPrinter : IHandle<PrintOrder>
    {
        public int Done { get; private set; }
        public int Total { get; private set; }

        public void Handle(PrintOrder message)
        {
            Done++;
            Total += message.Order.Total;
        }
    }

    public class PrintOrder : Message
    {
        public Order Order { get; private set; }

        public PrintOrder(Order order, Guid correlationId, Guid causeId) : base(correlationId, causeId)
        {
            Order = order;
        }
    }
}