using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yesil.Web.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}