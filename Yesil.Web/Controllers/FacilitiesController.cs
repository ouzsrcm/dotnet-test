using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yesil.Web.Controllers
{
    public class FacilitiesController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {

            Data.Facility fac;

            try
            {
                fac = db.Facilities.Where(x => x.FacilityId == Id).FirstOrDefault();

                ViewBag.Title = fac.Title;
            }
            catch
            {
                return Redirect("/");
            }

            return View(fac);
        }

    }
}