using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcContrib.ActionResults;

namespace ResultStore.Controllers
{
    public abstract class ContextAwareController : Controller
    {
        public ActionResult ContextAwareReturn(object obj) {
            if (Request.AcceptTypes.Contains("application/json")) {
                return Json(obj, JsonRequestBehavior.AllowGet);
            } else if (Request.AcceptTypes.Contains("application/xml") ||
                       Request.AcceptTypes.Contains("text/xml") ||
                       Request.AcceptTypes.Contains("application/xml;q=0.9")) {
                return new XmlResult(obj);
            }

            throw new Exception("Something went wrong");
        }
    }
}