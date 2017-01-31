namespace ConsoleApp
{
    public interface IPublisher
    {
        void Publish<T>(T message);
    }
}