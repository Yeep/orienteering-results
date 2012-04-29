using ResultStore.Model.Orienteering;

namespace ResultStore.Repository
{
    public interface IImportRepository
    {
        void AddEvent(ref Event e);
        void AddCourse(ref Course c);
        void AddResult(ref Result r);

        Club TryGetClubFromName(string name);
    }
}
