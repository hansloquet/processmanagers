using System;

namespace ProcessManagers
{
    internal class PublishAt : Message
    {
        public Message DelayedMessage { get; }

        public PublishAt(DateTime dueDate, Message delayedMessage) : base(delayedMessage.CorrelationId, delayedMessage.Id)
        {
            DueDate = dueDate;
            DelayedMessage = delayedMessage;
        }

        public DateTime DueDate { get; }
    }
}