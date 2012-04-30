using System;
using System.Collections.Generic;
using ResultStore.Model.Orienteering;

namespace ResultStore.Repository
{
    public interface IClubRepository : IRepository<Club, Int32?>
    {
        Club GetByShortName(string shortname);
        Club GetByName(string name);

        IList<Event> Events(int id);
        IList<Event> RecentEvents(int id, int number);
    }
}
