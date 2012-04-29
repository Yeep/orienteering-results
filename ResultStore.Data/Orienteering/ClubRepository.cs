using System;
using System.Collections.Generic;
using ResultStore.Model.Orienteering;
using ResultStore.Repository;

namespace ResultStore.Data.Orienteering
{
    public class ClubRepository : IClubRepository
    {
        public Club GetById(Int32? id) {
            return SessionProvider.Instance.GetSession().Get<Club>(id);
        }

        //---------------------------------------------------------------------------------------------------

        public void Save(Club obj) {
            using (var transaction = SessionProvider.Instance.GetSession().BeginTransaction()) {
                SessionProvider.Instance.GetSession().SaveOrUpdate(obj);
                transaction.Commit();
            }
        }

        //---------------------------------------------------------------------------------------------------

        public void Delete(Int32? id) {
            using (var transaction = SessionProvider.Instance.GetSession().BeginTransaction()) {
                var e = SessionProvider.Instance.GetSession().Load<Club>(id);
                SessionProvider.Instance.GetSession().Delete(e);
                transaction.Commit();
            }
        }

        //---------------------------------------------------------------------------------------------------

        public IList<Club> List() {
            return SessionProvider.Instance.GetSession().CreateQuery("FROM Club c")
                .List<Club>();
        }

        //---------------------------------------------------------------------------------------------------

        public IList<Event> Events(int id) {
            return SessionProvider.Instance.GetSession().CreateQuery("FROM Event e WHERE e.Club = :id")
                .SetParameter("id", id)
                .List<Event>();
        }
    }
}
