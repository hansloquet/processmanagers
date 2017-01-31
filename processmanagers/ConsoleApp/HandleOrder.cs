using Messages;

namespace ConsoleApp
{
    public interface HandleOrder
    {
        void Handle(Order order);
    }
}