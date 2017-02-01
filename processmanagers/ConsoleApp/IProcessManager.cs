namespace ProcessManagers
{
    internal interface IProcessManager: IHandle<Message>,
        IHandle<OrderPlaced>,
        IHandle<OrderCooked>,
        IHandle<OrderCalculated>,
        IHandle<OrderPaid>
    {
    }
}