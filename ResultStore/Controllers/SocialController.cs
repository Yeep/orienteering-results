using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResultStore.Repository;
using ResultStore.Data.Orienteering;

namespace ResultStore.Controllers
{
    public class SocialController : ContextAwareController
    {
        private static ISocialRepository s_repository;

        static SocialController() {
            s_repository = new SocialRepository();
        }

        //---------------------------------------------------------------------------------------------------
    }
}
