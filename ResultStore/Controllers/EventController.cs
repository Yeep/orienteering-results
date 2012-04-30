using System;
using System.Linq;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using MvcContrib.ActionResults;
using ResultStore.Data.Orienteering;
using ResultStore.Model.Orienteering;
using ResultStore.Repository;

namespace ResultStore.Controllers
{
    public class EventController : ContextAwareController
    {
        private static IEventRepository s_repository;
        private static ICourseRepository s_course_repository;

        static EventController() {
            s_repository = new EventRepository();
            s_course_repository = new CourseRepository();
        }

        //---------------------------------------------------------------------------------------------------

        [GET("event/{id}")]
        [Authorize]
        public ActionResult Event(int id) {
            var e = s_repository.GetById(id);
            return ContextAwareReturn(e);
        }

        //---------------------------------------------------------------------------------------------------

        [GET("events")]
        [Authorize(Roles="ADMINISTRATOR")]
        public ActionResult List() {
            return ContextAwareReturn(s_repository.List());
        }

        //---------------------------------------------------------------------------------------------------

        [GET("events/recent/{number}")]
        [Authorize]
        public ActionResult RecentEvents(int number) {
            return ContextAwareReturn(s_repository.Recent(number));
        }

        //---------------------------------------------------------------------------------------------------

        [POST("event")]
        [Authorize(Roles="ADMINISTRATOR")]
        public ActionResult Create(Event new_event) {
            s_repository.Save(new_event);
            return new EmptyResult();
        }

        //---------------------------------------------------------------------------------------------------

        [DELETE("event/{id}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public ActionResult Delete(int id) {
            s_repository.Delete(id);
            return new EmptyResult();
        }

        //---------------------------------------------------------------------------------------------------

        [GET("event/{id}/courses")]
        [Authorize]
        public ActionResult Courses(int id) {
            return ContextAwareReturn(s_course_repository.ForEvent(new Event { Id = id }));
        }

        //---------------------------------------------------------------------------------------------------

        [GET("event/{id}/course/{course}")]
        [Authorize]
        public ActionResult Course(int id, int course) {
            return ContextAwareReturn(s_course_repository.GetById(course));

        }

        //---------------------------------------------------------------------------------------------------

        [GET("event/{id}/course/{course}/results")]
        [Authorize]
        public ActionResult ResultsByCourse(int id, int course) {
            return ContextAwareReturn(s_course_repository.Results(s_course_repository.GetById(course)));
        }
    }
}
