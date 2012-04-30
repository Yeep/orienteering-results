using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

namespace ResultViewer.Controllers
{
    public class ClubController : Controller
    {
        [GET("/club/{index}")]
        [Authorize]
        public ActionResult Index(int index) {
            ViewBag.ClubIndex = index;
            return View();
        }

    }
}
