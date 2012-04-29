using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using ResultStore.Data.Orienteering;
using ResultStore.Model.Orienteering;
using ResultStore.Repository;

namespace ResultStore.Controllers
{
    public class ClubController : Controller
    {
        private static IClubRepository s_repository;

        static ClubController() {
            s_repository = new ClubRepository();
            //DebugData data = new DebugData();
        }

        //---------------------------------------------------------------------------------------------------

        [GET("club/{id}")]
        public ActionResult Club(int id) {
            return Json(s_repository.GetById(id), JsonRequestBehavior.AllowGet);
        }

        //---------------------------------------------------------------------------------------------------

        [GET("clubs")]
        public ActionResult List() {
            return Json(s_repository.List(), JsonRequestBehavior.AllowGet);
        }

        //---------------------------------------------------------------------------------------------------

        [POST("club")]
        public ActionResult Create(Club club) {
            s_repository.Save(club);
            return new EmptyResult();
        }

        //---------------------------------------------------------------------------------------------------

        [DELETE("club/{id}")]
        public ActionResult Delete(int id) {
            s_repository.Delete(id);
            return new EmptyResult();
        }

        //---------------------------------------------------------------------------------------------------

        [GET("club/{id}/events")]
        public ActionResult Events(int id) {
            return Json(s_repository.Events(id), JsonRequestBehavior.AllowGet);
        }
    }
}
