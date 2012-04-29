using ResultStore.Model.Application;

namespace ResultStore.Repository
{
    public interface IQueuedEventRepository : IRepository<QueuedEvent, int>
    {
        QueuedEvent NextEvent();
    }
}
