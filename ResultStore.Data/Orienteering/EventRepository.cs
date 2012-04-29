using System;
using System.Collections.Generic;
using ResultStore.Model.Orienteering;
using ResultStore.Repository;

namespace ResultStore.Data.Orienteering
{
    public class EventRepository : IEventRepository
    {
        public Event GetById(Int32? id) {
            return SessionProvider.Instance.GetSession().Get<Event>(id);
        }

        //---------------------------------------------------------------------------------------------------

        public void Save(Event obj) {
            using (var transaction = SessionProvider.Instance.GetSession().BeginTransaction()) {
                SessionProvider.Instance.GetSession().SaveOrUpdate(obj);
                transaction.Commit();
            }
        }

        //---------------------------------------------------------------------------------------------------

        public void Delete(Int32? id) {
            using (var transaction = SessionProvider.Instance.GetSession().BeginTransaction()) {
                var e = SessionProvider.Instance.GetSession().Load<Event>(id);
                SessionProvider.Instance.GetSession().Delete(e);
                transaction.Commit();
            }
        }

        //---------------------------------------------------------------------------------------------------

        public IList<Event> List() {
            return SessionProvider.Instance.GetSession().CreateQuery("FROM Event e")
                .List<Event>();
        }

        //---------------------------------------------------------------------------------------------------

        public IList<Event> Recent(int number) {
            return SessionProvider.Instance.GetSession().CreateQuery("FROM Event e ORDER BY e.Date DESC")
                .SetMaxResults(number)
                .List<Event>();
        }
    }
}
