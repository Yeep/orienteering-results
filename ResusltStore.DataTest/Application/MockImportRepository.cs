using System.Collections.Generic;
using ResultStore.Model.Orienteering;
using ResultStore.Repository;

namespace ResultStore.Data.Application
{
    public class MockImportRepository : IImportRepository
    {
        private IList<Event> m_events = new List<Event>();
        private IList<Course> m_courses = new List<Course>();
        private IList<Result> m_results = new List<Result>();

        private int m_event_id = 0;
        private int m_course_id = 0;
        private int m_result_id = 0;

        public IList<Event> Events {
            get { return m_events; }
        }

        public IList<Course> Courses {
            get { return m_courses; }
        }

        public IList<Result> Results {
            get { return m_results; }
        }

        public void Event(ref Event e) {
            e.Id = m_event_id++;
            m_events.Add(e);
        }

        public void Course(ref Course c) {
            c.Id = m_course_id++;
            m_courses.Add(c);
        }

        public void Result(ref Result r) {
            r.Id = m_result_id++;
            m_results.Add(r);
        }
    }
}
