using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

namespace ResultStore.Controllers
{
    public class HomeController : Controller
    {
        [GET("/")]
        public ActionResult Index()
        {
            return View();
        }

        [GET("regenerate")]
        public ActionResult Regenerate() {
            DebugData.Generate();
            return new EmptyResult();
        }
    }
}
