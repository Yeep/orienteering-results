using System.Collections.Generic;

namespace ResultStore.Repository
{
    public interface IRepository<TD, TK> where TD: class
    {
        TD GetById(TK id);
        void Save(TD obj);
        void Delete(TK id);
        IList<TD> List();
    }
}
