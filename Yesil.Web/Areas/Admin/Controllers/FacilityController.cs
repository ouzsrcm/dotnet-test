using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Yesil.Data;

namespace Yesil.Web.Areas.Admin.Controllers
{
    public class FacilityController : BaseController
    {
        private YesilEntities db = new YesilEntities();

        // GET: Admin/Facility
        public ActionResult Index()
        {
            return View(db.Facilities.ToList());
        }

        // GET: Admin/Facility/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // GET: Admin/Facility/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Facility facility, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    try
                    {
                        var fileName = Guid.NewGuid().ToString();
                        var extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                        using (var img = System.Drawing.Image.FromStream(file.InputStream))
                        {
                            facility.Image = String.Format("/assets/uploads/{0}{1}", fileName, extension);
                            SaveToFolder(img, fileName, extension, new Size(800, 700), facility.Image);
                        }
                    }
                    catch { }
                }
                db.Facilities.Add(facility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(facility);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        private void SaveToFolder(Image img, string fileName, string extension, Size newSize, string pathToSave)
        {
            Size imgSize = NewImageSize(img.Size, newSize);
            using (System.Drawing.Image newImg = new Bitmap(img, imgSize.Width, imgSize.Height))
            {
                newImg.Save(Server.MapPath(pathToSave), img.RawFormat);
            }
        }

        public Size NewImageSize(Size imageSize, Size newSize)
        {
            Size finalSize;
            double tempval;
            if (imageSize.Height > newSize.Height || imageSize.Width > newSize.Width)
            {
                if (imageSize.Height > imageSize.Width)
                    tempval = newSize.Height / (imageSize.Height * 1.0);
                else
                    tempval = newSize.Width / (imageSize.Width * 1.0);

                finalSize = new Size((int)(tempval * imageSize.Width), (int)(tempval * imageSize.Height));
            }
            else
                finalSize = imageSize;

            return finalSize;
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Facility facility, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    try
                    {
                        var fileName = Guid.NewGuid().ToString();
                        var extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                        using (var img = System.Drawing.Image.FromStream(file.InputStream))
                        {
                            facility.Image = String.Format("/assets/uploads/{0}{1}", fileName, extension);
                            SaveToFolder(img, fileName, extension, new Size(800, 700), facility.Image);
                        }
                    }
                    catch { }
                }

                Facility fac = (from x in db.Facilities where x.FacilityId == facility.FacilityId select x).FirstOrDefault();

                fac.Title = facility.Title;
                fac.Keyword = facility.Keyword;
                fac.Description = facility.Description;
                fac.Details = facility.Details;
                if (file != null) fac.Image = facility.Image;
                fac.Address = facility.Address;
                fac.Image = facility.Image;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(facility);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facility facility = db.Facilities.Find(id);
            db.Facilities.Remove(facility);
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
