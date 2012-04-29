using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

namespace ResultViewer.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [GET("/")]
        public ActionResult Index() {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        //---------------------------------------------------------------------------------------------------

        [GET("/about")]
        public ActionResult About() {
            return View();
        }
    }
}
