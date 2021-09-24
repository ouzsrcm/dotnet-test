using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using Yesil.Data;
using Yesil.Model;

namespace Yesil.Web.Controllers
{
    public class CustomerController : BaseController
    {
        public IProduct product;
        public IShoppingCart cart;
        public ICustomer customer;

        IShop shop;

        public ActionResult Index()
        {
            shop = new IShop();
            if (getCustomerIdBySession() != 0)
            {
                shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                shop.Customer.Get();
                shop.Customer.ShoppingCart.GetList(int.Parse(Session["CustomerId"].ToString()));
                return View("Profile", shop);
            }
            else
            {
                return View(shop);
            }
        }

        public ActionResult Account()
        {
            shop = new IShop();
            if (Session["CustomerId"] != null)
            {
                shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                shop.Customer.Get();
                return View("Profile", shop);
            }
            else
            {
                return Redirect("/Customer");
            }
        }

        public ActionResult Create()
        {
            shop = new IShop();
            if (getCustomerIdBySession() != 0)
            {
                shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                shop.Customer.Get();
                shop.Customer.ShoppingCart.GetList(int.Parse(Session["CustomerId"].ToString()));
                return View("Profile", shop);
            }
            else
            {
                return View(shop);
            }
        }

        [HttpPost]
        public ActionResult Create(ICustomer model)
        {
            shop = new IShop();
            try
            {
                Customer cust = new Customer();
                cust.Firstname = model.Firstname;
                cust.Lastname = model.Lastname;
                cust.Email = model.Email;
                shop.Customer.Create(cust);
                if (shop.Customer.CreateStatus)
                {
                    CustomerCredential cre = new CustomerCredential();
                    cre.CustomerId = shop.Customer.CustomerId;
                    cre.Email = model.Email;
                    cre.Password = model.Password;
                    cre.IsActive = true;
                    shop.Customer.Create(cre);
                }
                return View(shop);
            }
            catch
            {
                return View("Index", shop);
            }

        }

        public ActionResult Credential()
        {
            shop = new IShop();
            if (getCustomerIdBySession() != 0)
            {
                shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                shop.Customer.Get();
                return View("Credential", shop);
            }
            else
            {
                return Redirect("/Customer");
            }
        }

        public ActionResult Order()
        {
            shop = new IShop();
            if (getCustomerIdBySession() != 0)
            {
                shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                shop.Customer.Get();

                shop.Customer.Orders(shop.Customer.CustomerId);

                return View("Order", shop);
            }
            else
            {
                return Redirect("/Customer");
            }
        }

        [HttpPost]
        public ActionResult SaveInfo(Customer model)
        {
            shop = new IShop();
            if (getCustomerIdBySession() != 0)
            {
                shop.Customer.Update(model);
                shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                shop.Customer.Get();
                return View("Profile", shop);
            }
            else
            {
                return Redirect("/Customer");
            }
        }

        [HttpPost]
        public ActionResult SaveCredential(CustomerCredential model)
        {
            shop = new IShop();
            if (getCustomerIdBySession() != 0)
            {
                shop.Customer.UpdateCredential(model);
                shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                shop.Customer.Get();
                return View("Credential", shop);
            }
            else
            {
                return Redirect("/Customer");
            }
        }

        [HttpPost]
        public ActionResult Login(CustomerCredential cre)
        {
            shop = new IShop();
            try
            {
                shop.Customer.Login(cre);
                if (shop.Customer.LoginStatus)
                {
                    Session["CustomerId"] = shop.Customer.CustomerId.ToString();
                    return Redirect("/Customer");
                }
                else
                {
                    return View("Index", shop);
                }
            }
            catch
            {
                return View("Index", shop);
            }

        }

        public ActionResult Logout()
        {
            if (Session["CustomerId"] != null)
                Session["CustomerId"] = null;

            Session.Abandon();
            return Redirect("/Products");
        }

    }
}