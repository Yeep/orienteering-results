using ResultStore.Model.Orienteering;
using ResultStore.Repository;
using ResultStore.Model.Application;

namespace ResultStore.Processing.Import
{
    public interface IImporter
    {
        string Name { get; }
        string Description { get; }
        QueuedEvent Event { get; set; }

        decimal Probability();
        Event Sample();
        Event Import(IImportRepository repository);
    }
}
