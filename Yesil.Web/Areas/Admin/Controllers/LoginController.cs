using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Yesil.Data;
using Yesil.Model;

namespace Yesil.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {

        YesilEntities db = new YesilEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Data.Admin model)
        {
            if (model.Username != null && model.Password != null)
            {
                if (!(model.Username.Trim().Length < 1 && model.Password.Trim().Length < 1))
                {

                    var admin = db.Admins.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();

                    if (admin != null)
                    {
                        Session["AdminId"] = admin.AdminId;
                        return Redirect("/Admin/Dashboard");
                    }
                    else return Redirect("/Admin/Login");

                }
                else return Redirect("/Admin/Login");
            }
            else return Redirect("/Admin/Login");
            
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return Redirect("/");
        }

    }
}