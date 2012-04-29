namespace ResultStore.Model.Application
{
    public class FileEvent : QueuedEvent
    {
        public virtual string Filename { get; set; }
    }
}
