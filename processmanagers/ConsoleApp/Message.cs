using System;

namespace ProcessManagers
{
    public class Message
    {
        public Message(Guid correlationId, Guid causeId)
        {
            Id = Guid.NewGuid();
            CorrelationId = correlationId;
            CauseId = causeId;
        }

        public Guid Id { get; set; }
        public Guid CorrelationId { get; set; }
        public Guid CauseId { get; set; }
    }
}