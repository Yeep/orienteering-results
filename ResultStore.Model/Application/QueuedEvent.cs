using System;

namespace ResultStore.Model.Application
{
    public abstract class QueuedEvent
    {
        public enum QueueStatus { Pending, Done, Failed, Blocked }

        public virtual int? Id { get; set; }
        public virtual DateTime Submitted { get; set; }
        public virtual QueueStatus Status { get; set; }
        public virtual string Processor { get; set; }
    }
}
