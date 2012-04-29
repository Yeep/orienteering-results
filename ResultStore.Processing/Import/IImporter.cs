using ResultStore.Model.Orienteering;
using ResultStore.Repository;

namespace ResultStore.Processing.Import
{
    public interface IImporter
    {
        string Name { get; }
        string Description { get; }

        decimal Probability();
        Event Sample();
        void Import(IImportRepository repository);
    }
}
