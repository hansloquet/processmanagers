namespace ConsoleApp
{
    public class Repeater : IHandleOrder
    {
        private readonly IHandleOrder[] _handlers;

        public Repeater(params IHandleOrder[] handlers)
        {
            _handlers = handlers;
        }

        public void Handle(Order order)
        {
            foreach (var handler in _handlers)
            {
                handler.Handle(order);
            }
        }
    }
}