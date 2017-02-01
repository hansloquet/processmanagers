namespace ProcessManagers
{
    public interface IPublisher
    {
        void Publish<T>(T message) where T : Message;
    }
}