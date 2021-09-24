using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yesil.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ERROR = "";
            try
            {
                ViewBag.ERROR = Session["ERROR"].ToString();
            }
            catch
            {

            }
            return View();
        }
    }
}