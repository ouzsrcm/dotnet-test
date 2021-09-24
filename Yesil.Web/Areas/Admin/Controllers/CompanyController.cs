using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Yesil.Data;

namespace Yesil.Web.Areas.Admin.Controllers
{
    public class CompanyController : BaseController
    {
        private YesilEntities db = new YesilEntities();

        public ActionResult Index()
        {
            Company company = db.Companies.Find(1);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View("Edit", company);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
