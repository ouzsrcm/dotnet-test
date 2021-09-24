using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;
using PagedList.Mvc;
using Yesil.Data;
using Yesil.Model;

namespace Yesil.Web.Controllers
{
    public class ShoppingCartController : BaseController
    {

        IShop shop;

        public ActionResult Index()
        {
            shop = new IShop();
            if (getCustomerIdBySession() != 0)
            {
                shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                shop.Customer.Get();
                shop.Customer.ShoppingCart.GetList(int.Parse(Session["CustomerId"].ToString()));
                return View(shop);
            }
            else
            {
                return Redirect("/Customer");
            }
        }

        public ActionResult Addresses()
        {
            shop = new IShop();
            if (getCustomerIdBySession() != 0)
            {
                shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                shop.Customer.Get();
                shop.Customer.ShoppingCart.GetList(shop.Customer.CustomerId);
                shop.Customer.GetAddress();
                return View(shop);
            }
            else
            {
                return Redirect("/Customer");
            }
        }

        [HttpPost]
        public ActionResult Addresses(CustomerAddress model)
        {
            shop = new IShop();
            if (getCustomerIdBySession() != 0)
            {

                try
                {
                    if (model.Title != null && model.Phone1 != null && model.Address != null && model.City != null)
                    {
                        if (model.CustomerAddressId != 0)
                        {
                            shop.Customer.UpdateAddress(model);
                        }
                        else
                        {
                            shop.Customer.InsertAddress(model);
                        }

                        return Redirect("/ShoppingCart/Payment");
                    }
                    else
                    {
                        shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                        shop.Customer.Get();
                        shop.Customer.ShoppingCart.GetList(shop.Customer.CustomerId);
                        shop.Customer.GetAddress();
                        shop.Customer.Alert.Set(true, "", "Lütfen boş alanların hepsini doldurun.", "danger");
                        return View("Addresses", shop);
                    }

                }
                catch
                {
                    return Redirect("/ShoppingCart/Addresses");
                }

            }
            else
            {
                return Redirect("/Customer");
            }
        }

        public ActionResult Payment()
        {
            shop = new IShop();
            if (getCustomerIdBySession() != 0)
            {
                shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                shop.Customer.Get();
                shop.Customer.ShoppingCart.GetList(shop.Customer.CustomerId);
                shop.Customer.GetBankAccounts();
                shop.Customer.GetFaqs();
                return View(shop);
            }
            else
            {
                return Redirect("/Customer");
            }
        }

        [HttpPost]
        public ActionResult Order(IOrder order)
        {
            shop = new IShop();
            if (getCustomerIdBySession() != 0)
            {

                shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                shop.Customer.Get();
                shop.Customer.ShoppingCart.GetList(shop.Customer.CustomerId);
                shop.Customer.ShoppingCart.CustomerId = shop.Customer.CustomerId;

                shop.Customer.GetBankAccounts();
                shop.Customer.GetFaqs();

                try
                {

                    var _list = db.ShoppingCarts.Where(x => x.CustomerId == shop.Customer.CustomerId).ToList();

                    if (_list != null && _list.Count < 1)
                    {
                        Redirect("/Products");
                    }

                    if (order.PaymentType != "0")
                    {
                        if (order.PaymentType == "1")
                        {
                            if (order.HolderName != null && order.Number != null && order.ExpirationYear != null && order.ExpirationMonth != null && order.Ccv != null)
                            {
                                if ((order.HolderName.Trim() != "") && (order.Number.Trim() != "")
                                    && (order.ExpirationMonth.ToString().Trim() != "") && (order.ExpirationYear.ToString().Trim() != "") && (order.Ccv.ToString().Trim() != ""))
                                {
                                    shop.Customer.ShoppingCart.CreditCard = true;
                                }
                                else
                                {
                                    shop.Customer.ShoppingCart.AlertStatus = true;
                                    shop.Customer.ShoppingCart.AlertTitle = "";
                                    shop.Customer.ShoppingCart.AlertCssClass = "warning";
                                    shop.Customer.ShoppingCart.AlertBody = "Girmiş olduğunuz bilgiler doğrulanamadı. Lütfen kontrol edip yeniden deneyin.";
                                    return View(shop);
                                }

                            }
                        }

                        shop.Customer.ShoppingCart.Order = order;
                        shop.Customer.ShoppingCart.CreateOrder();

                        return View("Order", shop);

                    }
                    else
                    {
                        shop.Customer.ShoppingCart.AlertStatus = true;
                        shop.Customer.ShoppingCart.AlertTitle = "";
                        shop.Customer.ShoppingCart.AlertCssClass = "warning";
                        shop.Customer.ShoppingCart.AlertBody = "Lütfen ödeme türü belirtin.";
                        return View("Payment", shop);
                    }

                }
                catch
                {
                    return View("Payment", shop);
                }
            }
            else
            {
                return Redirect("/Customer");
            }
        }

        [HttpPost]
        public ActionResult Update(ShoppingCart model)
        {
            try
            {
                shop = new IShop();
                if (getCustomerIdBySession() != 0)
                {
                    shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                    shop.Customer.Get();
                    shop.Customer.ShoppingCart.Update(model);
                    shop.Customer.ShoppingCart.GetList(shop.Customer.CustomerId);
                    return Redirect("/ShoppingCart");
                }
                else
                {
                    return Redirect("/Customer");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Remove()
        {
            try
            {
                shop = new IShop();
                if (getCustomerIdBySession() != 0)
                {
                    shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                    shop.Customer.Get();
                    shop.Customer.ShoppingCart.Remove(Id);
                    shop.Customer.ShoppingCart.GetList(shop.Customer.CustomerId);
                    return Redirect("/ShoppingCart");
                }
                else
                {
                    return Redirect("/Customer");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Add()
        {
            try
            {
                shop = new IShop();
                if (getCustomerIdBySession() != 0)
                {

                    shop.Customer.CustomerId = int.Parse(Session["CustomerId"].ToString());
                    shop.Customer.Get();

                    var _cartList = (from x in db.ShoppingCarts
                                     where x.ProductId == Id && x.CustomerId == shop.Customer.CustomerId
                                     select x).FirstOrDefault();

                    if (_cartList != null)
                    {
                        _cartList.Quantity += 1;
                        db.SaveChanges();
                        _cartList = null;
                    }
                    else
                    {

                        var _product = (from x in db.Products where x.ProductId == Id select x).FirstOrDefault();

                        if (_product != null)
                        {
                            ShoppingCart cart = new ShoppingCart();
                            cart.CustomerId = shop.Customer.CustomerId;
                            cart.ProductId = _product.ProductId;
                            cart.SoldPrice = ((bool)_product.IsCampaign) ? _product.CampaignPrice : _product.Price;
                            cart.Date = DateTime.Now;
                            cart.UnitId = _product.UnitId;
                            cart.Quantity = 1;

                            db.ShoppingCarts.Add(cart);
                            db.SaveChanges();

                            cart = null;

                        }

                    }

                    shop.Customer.ShoppingCart.GetList(shop.Customer.CustomerId);

                    return Redirect("/Products");

                }
                else
                {
                    return Redirect("/Customer");
                }
            }
            catch (Exception)
            {
                return Redirect("/Products");
            }
        }

    }
}