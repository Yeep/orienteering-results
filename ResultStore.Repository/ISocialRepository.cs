using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResultStore.Model.Orienteering;

namespace ResultStore.Repository
{
    public interface ISocialRepository
    {
        IDictionary<int, int> GetCompetitorsForPerson(Event e, Person person);
    }
}
