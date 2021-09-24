using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Yesil.Data;
using Yesil.Model;

namespace Yesil.Web.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {

        public YesilEntities db = new YesilEntities();

        public int Id = 0;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                Id = int.Parse(this.ControllerContext.RouteData.Values["id"].ToString());
            }
            catch
            {
                Id = 0;
            }

            if (__session() == 0)
            {
                filterContext.Result = new RedirectResult("/Admin/Login");
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        int __session()
        {
            try
            {
                return int.Parse(Session["AdminId"].ToString());
            }
            catch
            {
                return 0;
            }
        }

    }
}