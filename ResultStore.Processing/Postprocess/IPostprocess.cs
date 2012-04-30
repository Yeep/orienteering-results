using ResultStore.Model.Orienteering;
using ResultStore.Repository;

namespace ResultStore.Processing.Postprocess
{
    public interface IPostprocess
    {
        void Execute(IImportRepository repository, Event e);
    }
}
