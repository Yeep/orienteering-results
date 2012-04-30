using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using ResultStore.Data.Orienteering;
using ResultStore.Model.Orienteering;
using ResultStore.Repository;

namespace ResultStore.Controllers
{
    public class ClubController : ContextAwareController
    {
        private static IClubRepository s_repository;

        static ClubController() {
            s_repository = new ClubRepository();
            //DebugData data = new DebugData();
        }

        //---------------------------------------------------------------------------------------------------

        [GET("club/{id}")]
        [Authorize]
        public ActionResult Club(int id) {
            return ContextAwareReturn(s_repository.GetById(id));
        }

        //---------------------------------------------------------------------------------------------------

        [GET("clubs")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public ActionResult List() {
            return ContextAwareReturn(s_repository.List());
        }

        //---------------------------------------------------------------------------------------------------

        [POST("club")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public ActionResult Create(Club club) {
            s_repository.Save(club);
            return new EmptyResult();
        }

        //---------------------------------------------------------------------------------------------------

        [DELETE("club/{id}")]
        [Authorize(Roles="ADMINISTRATOR")]
        public ActionResult Delete(int id) {
            s_repository.Delete(id);
            return new EmptyResult();
        }

        //---------------------------------------------------------------------------------------------------

        [GET("club/{id}/events")]
        [Authorize(Roles="ADMINISTRATOR")]
        public ActionResult Events(int id) {
            return ContextAwareReturn(s_repository.Events(id));
        }

        //---------------------------------------------------------------------------------------------------

        [GET("club/{id}/events/recent/{number}")]
        [Authorize]
        public ActionResult RecentEvents(int id, int number) {
            return ContextAwareReturn(s_repository.RecentEvents(id, number));
        }
    }
}
