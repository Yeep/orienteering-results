using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using ResultStore.Data.Application;
using ResultStore.Model.Application;
using ResultStore.Processing;
using ResultStore.Processing.Postprocess;
using ResultStore.Processing.Import;

namespace ResultStore.Controllers
{
    public class ImportController : ContextAwareController
    {
        static IImportQueue s_import_queue;

        //---------------------------------------------------------------------------------------------------

        static ImportController() {
            s_import_queue = new ImportQueue();
            s_import_queue.RegisterPostprocess(new ClubRelationships());
        }

        //---------------------------------------------------------------------------------------------------

        [POST("import")]
        public ActionResult Import(WebEvent e) {
            s_import_queue.Process(e);
            return new EmptyResult();
        }

        //---------------------------------------------------------------------------------------------------

        [POST("import/short")]
        public ActionResult ImportShort(WebEvent e) {
            return ContextAwareReturn((s_import_queue as ImportQueue).Summary(e));
        }
    }
}
