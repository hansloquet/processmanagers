namespace ConsoleApp
{
    public interface IPublisher
    {
        void Publish(string topic, Order order);
        void Publish(OrderPlaced orderPlaced);
    }
}