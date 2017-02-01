namespace ProcessManagers
{
    public class OrderCalculated : Message
    {
        public Order Order { get; }

        public OrderCalculated(Order order, Message message) : base(message.CorrelationId, message.Id)
        {
            Order = order;
        }
    }
}