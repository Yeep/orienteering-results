using ResultStore.Model.Orienteering;

namespace ResultStore.Repository
{
    public interface IImportRepository
    {
        void Event(ref Event e);
        void Course(ref Course c);
        void Result(ref Result r);
    }
}
