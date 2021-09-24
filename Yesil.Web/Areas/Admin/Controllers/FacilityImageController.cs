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
    public class FacilityImageController : Controller
    {
        private YesilEntities db = new YesilEntities();

        // GET: Admin/FacilityImage
        public ActionResult Index()
        {
            var facilityImages = db.FacilityImages.Include(f => f.Facility);
            return View(facilityImages.ToList());
        }

        // GET: Admin/FacilityImage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacilityImage facilityImage = db.FacilityImages.Find(id);
            if (facilityImage == null)
            {
                return HttpNotFound();
            }
            return View(facilityImage);
        }

        // GET: Admin/FacilityImage/Create
        public ActionResult Create()
        {
            ViewBag.FacilityId = new SelectList(db.Facilities, "FacilityId", "Title");
            return View();
        }

        // POST: Admin/FacilityImage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacilityImage facilityImage, HttpPostedFileBase file)
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
                            facilityImage.Image = String.Format("/assets/uploads/{0}{1}", fileName, extension);
                            SaveToFolder(img, fileName, extension, new Size(800, 700), facilityImage.Image);
                        }
                    }
                    catch { }
                }

                db.FacilityImages.Add(facilityImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FacilityId = new SelectList(db.Facilities, "FacilityId", "Title", facilityImage.FacilityId);
            return View(facilityImage);
        }

        // GET: Admin/FacilityImage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacilityImage facilityImage = db.FacilityImages.Find(id);
            if (facilityImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacilityId = new SelectList(db.Facilities, "FacilityId", "Title", facilityImage.FacilityId);
            return View(facilityImage);
        }

        // POST: Admin/FacilityImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FacilityImage facilityImage, HttpPostedFileBase file)
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
                            facilityImage.Image = String.Format("/assets/uploads/{0}{1}", fileName, extension);
                            SaveToFolder(img, fileName, extension, new Size(800, 700), facilityImage.Image);
                        }
                    }
                    catch { }
                }
                
                db.Entry(facilityImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FacilityId = new SelectList(db.Facilities, "FacilityId", "Title", facilityImage.FacilityId);
            return View(facilityImage);
        }

        // GET: Admin/FacilityImage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacilityImage facilityImage = db.FacilityImages.Find(id);
            if (facilityImage == null)
            {
                return HttpNotFound();
            }
            return View(facilityImage);
        }

        // POST: Admin/FacilityImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FacilityImage facilityImage = db.FacilityImages.Find(id);
            db.FacilityImages.Remove(facilityImage);
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

    }
}
