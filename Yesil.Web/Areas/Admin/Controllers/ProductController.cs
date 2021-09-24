using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Yesil.Data;

namespace Yesil.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {

        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Unit);
            return View(products.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Title");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase file)
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
                            product.Image = String.Format("/assets/uploads/{0}{1}", fileName, extension);
                            SaveToFolder(img, fileName, extension, new Size(800, 700), product.Image);
                        }
                    }
                    catch { }
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Title", product.CategoryId);
            return View(product);
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

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Title", product.CategoryId);
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Title", product.UnitId);
            return View(product);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase file)
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
                            product.Image = String.Format("/assets/uploads/{0}{1}", fileName, extension);
                            SaveToFolder(img, fileName, extension, new Size(800, 700), product.Image);
                        }
                    }
                    catch { }
                }

                var prd = (from x in db.Products where x.ProductId == product.ProductId select x).FirstOrDefault();
                prd.CategoryId = product.CategoryId;
                prd.UnitId = product.UnitId;
                prd.Title = product.Title;
                prd.Keyword = product.Keyword;
                prd.Description = product.Description;
                prd.Details = product.Details;
                if (file != null) prd.Image = product.Image;
                prd.Tags = product.Tags;
                prd.Price = product.Price;
                prd.CampaignPrice = product.CampaignPrice;
                prd.IsCampaign = product.IsCampaign;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Title", product.CategoryId);
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
