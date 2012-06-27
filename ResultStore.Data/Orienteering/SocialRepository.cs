using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResultStore.Repository;
using ResultStore.Model.Orienteering;
using System.Collections;

namespace ResultStore.Data.Orienteering
{
    public class SocialRepository : ISocialRepository
    {
        public IDictionary<int, int> GetCompetitorsForPerson(Event e, Person person) {
            IList results = SessionProvider.Instance.GetSession().CreateQuery(
                @"SELECT r.Person, COUNT(r.Person) AS Number FROM Result r
                    JOIN r.Course as c
                    JOIN c.Event as e
                WHERE r.Person IN (
                    SELECT person.Id FROM Result result
                        JOIN result.Person
                    WHERE result.Course = some (
                        SELECT course.Id FROM Course course
                            JOIN course.
                    )
                )
            ").SetParameter("event", e)
             .SetParameter("person", person)
             .List();

            return new Dictionary<int, int>();
        }
    }
}
