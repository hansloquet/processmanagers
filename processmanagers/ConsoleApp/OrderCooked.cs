namespace ProcessManagers
{
    public class OrderCooked : Message
    {
        public Order Order { get; }

        public OrderCooked(Order order, Message message) : base(message.CorrelationId, message.Id)
        {
            Order = order;
        }
    }
}