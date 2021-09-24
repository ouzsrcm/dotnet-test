using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Yesil.Data;
using Yesil.Model;

namespace Yesil.Web.Controllers
{
    public class BaseController : Controller
    {

        public YesilEntities db = new YesilEntities();

        public string ControllerName, MethodName = "";
        public int Id = 0;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            MethodName = this.ControllerContext.RouteData.Values["action"].ToString();

            _id();

            try
            {

                IPage page = new IPage(ControllerName);

                ViewBag.Js = page.Js;
                ViewBag.Css = page.Css;
                ViewBag.Keyword = page.Keyword;
                ViewBag.Description = page.Description;

                ViewBag.PreTitle = page.PreTitle;
                ViewBag.ShortAbout = page.ShortAbout;
                ViewBag.SocialAccounts = page.SocialAccounts;

                ViewBag.Company = page.Company;
                ViewBag.Categories = page.Categories;

                ViewBag.News = db.News.ToList();
                ViewBag.Facilities = db.Facilities.ToList();

                page = null;

            }
            catch (Exception ex)
            {
                Session.Add("ERROR", ex.Message);
                filterContext.Result = new RedirectResult("/Error");
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        void _id()
        {
            try
            {
                this.Id = int.Parse(this.ControllerContext.RouteData.Values["id"].ToString());
            }
            catch
            {
                this.Id = 0;
            }
        }

        public bool __isnumeric(int word)
        {
            try
            {
                int _word = int.Parse(word.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int getCustomerIdBySession()
        {
            try
            {
                return int.Parse(Session["CustomerId"].ToString());
            }
            catch
            {
                return 0;
            }
        }

    }
}