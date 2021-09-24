using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;
using Yesil.Data;
using Yesil.Model;

namespace Yesil.Web.Controllers
{
    public class ProductsController : BaseController
    {

        IShop shop;

        public IProduct product;
        public IShoppingCart cart;
        
        public ActionResult Index()
        {
            shop = new IShop();

            if (getCustomerIdBySession() != 0)
            {
                shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                shop.Customer.Get();
                shop.Customer.ShoppingCart.GetList(shop.Customer.CustomerId);
            }

            return View(shop);
        }

        public ActionResult Category()
        {
            if (__isnumeric(Id) == true)
            {
                shop = new IShop();
                shop.Product.Category(Id);
                return View("Index", shop);
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult Show()
        {
            if (__isnumeric(Id) == true)
            {
                shop = new IShop();
                shop.Product.Get(Id);
                ViewBag.Title = shop.Product.Product.Title;
                return View(shop);
            }
            else
            {
                return Redirect("/");
            }
        }

    }
}