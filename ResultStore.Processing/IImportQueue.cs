using ResultStore.Model.Application;
using ResultStore.Processing.Postprocess;

namespace ResultStore.Processing
{
    public interface IImportQueue
    {
        void Process(QueuedEvent e);

        void ProcessQueue();

        void RegisterPostprocess(IPostprocess process);
        void UnregisterPostprocess(IPostprocess process);
    }
}
