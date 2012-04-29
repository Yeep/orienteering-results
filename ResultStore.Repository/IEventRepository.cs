using System;
using System.Collections.Generic;
using ResultStore.Model.Orienteering;

namespace ResultStore.Repository
{
    public interface IEventRepository : IRepository<Event, Int32?>
    {
        IList<Event> Recent(int number);
    }
}
