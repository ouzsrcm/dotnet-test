using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Yesil.Data;
using Yesil.Model;

namespace Yesil.Web.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {

        public IOrder Order;

        public ActionResult Index()
        {
            Order = new IOrder();
            Order.ListAll(false);
            return View(Order);
        }

        public ActionResult Details()
        {
            Order = new IOrder();
            Order.Details(Id);
            return View("Details", Order);
        }

        [HttpPost]
        public ActionResult SaveDetails(Order model)
        {
            var _order = (from x in db.Orders where x.OrderId == model.OrderId select x).FirstOrDefault();
            if (_order != null)
            {
                _order.OrderStateId = model.OrderStateId;
                db.SaveChanges();
            }
            return Redirect("/Admin/Order/Details/" + model.OrderId);
        }

    }
}