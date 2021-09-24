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
    public class NewsController : BaseController
    {
        private YesilEntities db = new YesilEntities();

        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News news, HttpPostedFileBase file)
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
                            news.Image = String.Format("/assets/uploads/{0}{1}", fileName, extension);
                            SaveToFolder(img, fileName, extension, new Size(800, 700), news.Image);
                        }
                    }
                    catch { }
                }

                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News news, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                var _new = (from x in db.News where x.NewsId == news.NewsId select x).FirstOrDefault();

                if (_new != null)
                {

                    if (file != null)
                    {
                        try
                        {
                            var fileName = Guid.NewGuid().ToString();
                            var extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                            using (var img = System.Drawing.Image.FromStream(file.InputStream))
                            {
                                news.Image = String.Format("/assets/uploads/{0}{1}", fileName, extension);
                                SaveToFolder(img, fileName, extension, new Size(800, 700), news.Image);
                            }
                        }
                        catch { }
                    }

                    _new.Keyword = news.Keyword;
                    _new.Description = news.Description;
                    _new.Title = news.Title;
                    _new.Details = news.Details;

                    if (file != null) _new.Image = news.Image;

                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(news);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
