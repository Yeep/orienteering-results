using System;
using System.Collections.Generic;
using ResultStore.Model.Orienteering;

namespace ResultStore.Repository
{
    public interface ICourseRepository : IRepository<Course, Int32?>
    {
        IList<Course> ForEvent(Event e);
        IList<Result> Results(Course c);
    }
}
