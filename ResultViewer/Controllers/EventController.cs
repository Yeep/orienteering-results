using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

namespace ResultViewer.Controllers
{
    public class EventController : Controller
    {
        [GET("event/{index}")]
        public ActionResult Index(int index) {
            ViewBag.EventIndex = index;
            return View();
        }

    }
}
