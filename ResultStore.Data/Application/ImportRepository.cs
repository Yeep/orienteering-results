using System.Collections.Generic;
using ResultStore.Model.Orienteering;
using ResultStore.Repository;

namespace ResultStore.Data.Application
{
    public class ImportRepository : IImportRepository
    {
        public void Event(ref Event e) {
            Thing(ref e);
        }

        //---------------------------------------------------------------------------------------------------

        public void Course(ref Course c) {
            Thing(ref c);
        }

        //---------------------------------------------------------------------------------------------------

        public void Result(ref Result r) {
            Thing(ref r);
        }

        //---------------------------------------------------------------------------------------------------

        private void Thing<T>(ref T thing) {
            using (var transaction = SessionProvider.Instance.GetSession().BeginTransaction()) {
                SessionProvider.Instance.GetSession().SaveOrUpdate(thing);
                transaction.Commit();
            }
        }
    }
}
