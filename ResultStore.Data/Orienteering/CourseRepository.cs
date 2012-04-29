using System.Collections.Generic;
using ResultStore.Model.Orienteering;
using ResultStore.Repository;

namespace ResultStore.Data.Orienteering
{
    public class CourseRepository : ICourseRepository
    {
        public IList<Course> ForEvent(Event e) {
            return SessionProvider.Instance.GetSession().CreateQuery("FROM Course c WHERE c.Event = :event")
                .SetEntity("event", e)
                .List<Course>();
        }

        //---------------------------------------------------------------------------------------------------

        public Course GetById(int? id) {
            return SessionProvider.Instance.GetSession().Get<Course>(id);
        }

        //---------------------------------------------------------------------------------------------------

        public void Save(Course obj) {
            using (var transaction = SessionProvider.Instance.GetSession().BeginTransaction()) {
                SessionProvider.Instance.GetSession().SaveOrUpdate(obj);
                transaction.Commit();
            }
        }

        //---------------------------------------------------------------------------------------------------

        public void Delete(int? id) {
            using (var transaction = SessionProvider.Instance.GetSession().BeginTransaction()) {
                var e = SessionProvider.Instance.GetSession().Load<Course>(id);
                SessionProvider.Instance.GetSession().Delete(e);
                transaction.Commit();
            }
        }

        //---------------------------------------------------------------------------------------------------

        public IList<Course> List() {
            return SessionProvider.Instance.GetSession().CreateQuery("FROM Course c")
                .List<Course>();
        }

        //---------------------------------------------------------------------------------------------------

        public IList<Result> Results(Course c) {
            return SessionProvider.Instance.GetSession().CreateQuery("FROM Result r WHERE r.Course = :course ORDER BY r.Code ASC, r.Position ASC")
                .SetEntity("course", c)
                .List<Result>();
        }
    }
}
