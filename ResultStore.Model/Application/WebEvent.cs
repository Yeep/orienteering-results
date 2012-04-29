namespace ResultStore.Model.Application
{
    public class WebEvent : QueuedEvent
    {
        public virtual string Url { get; set; }
    }
}
