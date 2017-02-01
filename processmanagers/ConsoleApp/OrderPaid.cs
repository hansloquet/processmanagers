namespace ProcessManagers
{
    public class OrderPaid : Message
    {
        public Order Order { get; }

        public OrderPaid(Order order, Message message) : base(message.CorrelationId, message.CauseId)
        {
            Order = order;
        }
    }
}