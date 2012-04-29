using System;
using System.Collections.Generic;
using ResultStore.Model.Orienteering;

namespace ResultStore.Repository
{
    public interface IClubRepository : IRepository<Club, Int32?>
    {
        IList<Event> Events(int id);
    }
}
