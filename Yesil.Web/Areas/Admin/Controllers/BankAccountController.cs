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
    public class BankAccountController : BaseController
    {
        private YesilEntities db = new YesilEntities();

        public ActionResult Index()
        {
            return View(db.BankAccounts.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BankAccount bankAccount, HttpPostedFileBase file)
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
                            bankAccount.Image = String.Format("/assets/uploads/{0}{1}", fileName, extension);
                            SaveToFolder(img, fileName, extension, new Size(800, 700), bankAccount.Image);
                        }
                    }
                    catch { }
                }

                db.BankAccounts.Add(bankAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bankAccount);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BankAccount bankAccount, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                BankAccount b = (from x in db.BankAccounts where x.BankAccountId == bankAccount.BankAccountId select x).FirstOrDefault();

                if (b != null)
                {

                    if (file != null)
                    {
                        try
                        {
                            var fileName = Guid.NewGuid().ToString();
                            var extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                            using (var img = System.Drawing.Image.FromStream(file.InputStream))
                            {
                                b.Image = String.Format("/assets/uploads/{0}{1}", fileName, extension);
                                SaveToFolder(img, fileName, extension, new Size(800, 700), b.Image);
                            }
                        }
                        catch { }
                    }

                    b.Title = bankAccount.Title;
                    if (file != null) b.Image = bankAccount.Image;
                    b.Number = bankAccount.Number;

                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bankAccount);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankAccount bankAccount = db.BankAccounts.Find(id);
            db.BankAccounts.Remove(bankAccount);
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
