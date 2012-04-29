using System.Collections.Generic;
using ResultStore.Model.Orienteering;
using ResultStore.Repository;

namespace ResultStore.Data.Application
{
    public class ImportRepository : IImportRepository
    {
        public void AddEvent(ref Event e) {
            AddThing(ref e);
        }

        //---------------------------------------------------------------------------------------------------

        public void AddCourse(ref Course c) {
            AddThing(ref c);
        }

        //---------------------------------------------------------------------------------------------------

        public void AddResult(ref Result r) {
            AddThing(ref r);
        }

        //---------------------------------------------------------------------------------------------------

        public Club TryGetClubFromName(string name) {
            IList<Club> clubs = SessionProvider.Instance.GetSession().CreateQuery("FROM Club c WHERE c.Name like :clubname OR c.ShortName like :clubname")
                .SetString("clubname", name)
                .List<Club>();

            if (clubs.Count == 1) {
                return clubs[0];
            }

            return null;
        }

        //---------------------------------------------------------------------------------------------------

        private void AddThing<T>(ref T thing) {
            using (var transaction = SessionProvider.Instance.GetSession().BeginTransaction()) {
                SessionProvider.Instance.GetSession().SaveOrUpdate(thing);
                transaction.Commit();
            }
        }
    }
}
