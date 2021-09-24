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
    public class SocialAccountController : BaseController
    {
        private YesilEntities db = new YesilEntities();

        public ActionResult Index()
        {
            return View(db.SocialAccounts.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialAccount socialAccount = db.SocialAccounts.Find(id);
            if (socialAccount == null)
            {
                return HttpNotFound();
            }
            return View(socialAccount);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SocialAccount socialAccount)
        {
            if (ModelState.IsValid)
            {
                db.SocialAccounts.Add(socialAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(socialAccount);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialAccount socialAccount = db.SocialAccounts.Find(id);
            if (socialAccount == null)
            {
                return HttpNotFound();
            }
            return View(socialAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SocialAccount socialAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socialAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(socialAccount);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialAccount socialAccount = db.SocialAccounts.Find(id);
            if (socialAccount == null)
            {
                return HttpNotFound();
            }
            return View(socialAccount);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SocialAccount socialAccount = db.SocialAccounts.Find(id);
            db.SocialAccounts.Remove(socialAccount);
            db.SaveChanges();
            return RedirectToAction("Index");
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
