using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Yesil.Data;

namespace Yesil.Model
{
    public class IShoppingCart : ShoppingCart
    {

        YesilEntities db = new YesilEntities();

        public bool AlertStatus { get; set; }
        public string AlertTitle { get; set; }
        public string AlertBody { get; set; }
        public string AlertCssClass { get; set; }

        public List<ShoppingCart> List { get; set; }
        public IOrder Order { get; set; }
        public bool CreditCard { get; set; }

        public IShoppingCart(int Id = 0)
        {
            try
            {
                this.CustomerId = Id;
                this.List = db.ShoppingCarts.Where(x => x.CustomerId == Id).ToList();
            }
            catch (Exception ex)
            {
                _showMessage(true, "Uyarı", ex.Message, "danger");
            }

        }

        public void Add(ShoppingCart cart = null)
        {
            try
            {

                db.ShoppingCarts.Add(cart);
                db.SaveChanges();

                _showMessage(true, "", "Ürün sepetinize başarıyla eklendi.", "success");

            }
            catch (Exception ex)
            {
                _showMessage(true, "Uyarı", ex.Message, "danger");
            }
        }

        public void Update(ShoppingCart cart = null)
        {
            try
            {
                ShoppingCart _cart = (from x in db.ShoppingCarts where x.ShoppingCartId == cart.ShoppingCartId select x).FirstOrDefault();
                if (_cart != null)
                {

                    if (cart.CustomerId != null) _cart.CustomerId = cart.CustomerId;
                    if (cart.ProductId != null) _cart.ProductId = cart.ProductId;
                    if (cart.SoldPrice != null) _cart.SoldPrice = cart.SoldPrice;
                    if (cart.Quantity != null) _cart.Quantity = cart.Quantity;

                    db.SaveChanges();

                    _showMessage(true, "", "Sepetiniz başarıyla güncellendi.", "success");

                }
                else
                {
                    _showMessage(true, "Uyarı", "Sepetinizde böyle bir ürün bulunmuyor.", "warning");
                }
            }
            catch (Exception ex)
            {
                _showMessage(true, "Uyarı", ex.Message, "danger");
            }
        }

        public void Remove(int Id = 0)
        {
            try
            {
                ShoppingCart _cart = (from x in db.ShoppingCarts where x.ShoppingCartId == Id select x).FirstOrDefault();
                if (_cart != null)
                {

                    db.ShoppingCarts.Remove(_cart);

                    db.SaveChanges();

                    _showMessage(true, "", "Sepetiniz başarıyla güncellendi.", "success");

                }
                else
                {
                    _showMessage(true, "Uyarı", "Sepetinizde böyle bir ürün bulunmuyor.", "warning");
                }
            }
            catch (Exception ex)
            {
                _showMessage(true, "Uyarı", ex.Message, "danger");
            }
        }

        public void Get(int Id = 0)
        {
            ShoppingCart cart;
            try
            {
                cart = db.ShoppingCarts.Where(x => x.ShoppingCartId == Id).FirstOrDefault();
                if (cart != null)
                {
                    this.CustomerId = cart.CustomerId;
                    this.ProductId = cart.ProductId;
                    this.SoldPrice = cart.SoldPrice;
                    this.Date = cart.Date;
                    this.Quantity = cart.Quantity;
                    this.UnitId = cart.UnitId;
                }
                else
                {
                    _showMessage(true, "Uyarı", "Sepetinizde böyle bir ürün bulunamadı.", "warning");
                }
            }
            catch (Exception ex)
            {
                _showMessage(true, "Uyarı", ex.Message, "danger");
            }
        }

        public void GetList(int Id = 0)
        {
            try
            {
                this.List = db.ShoppingCarts.Where(x => x.CustomerId == Id).ToList();
            }
            catch (Exception ex)
            {
                _showMessage(true, "", ex.Message, "danger");
            }
        }

        public void CreateOrder()
        {
            // Kullanıcı adres bilgisi alınıyor. Tanımlı adres bilgisi yoksa Adres kısmına geri döndürülür.
            CustomerAddress addr = db.CustomerAddresses.Where(x => x.CustomerId == this.CustomerId).FirstOrDefault();

            if (addr != null)
            {

                {
                    // Sipariş oluşturuluyor.
                    Order order = new Order();
                    OrderProduct prod;

                    order.CustomerId = this.CustomerId;
                    order.BillingAddressId = addr.CustomerAddressId;
                    order.DeliveryAddressId = addr.CustomerAddressId;
                    order.Description = this.Order.Description;
                    order.Date = (DateTime)DateTime.Now;
                    order.PaymentType = ((CreditCard) ? "card" : "bankaccount");
                    order.OrderStateId = 1;
                    db.Orders.Add(order);
                    db.SaveChanges();

                    // Kullanıcı kart bilgilerini veritabanına yaz.
                    if (CreditCard)
                    {
                        OrderCard card = new OrderCard();
                        card.HolderName = this.Order.HolderName;
                        card.Date = (DateTime)DateTime.Now;
                        card.ExpirationYear = this.Order.ExpirationYear;
                        card.ExpirationMonth = this.Order.ExpirationMonth;
                        card.Ccv = this.Order.Ccv;
                        card.OrderId = order.OrderId;

                        db.OrderCards.Add(card);
                        db.SaveChanges();
                    }

                    // Kullanıcının almış olduğu ürünler sepetten sipariş tablosuna yazılıyor.
                    this.GetList((int.Parse(this.CustomerId.ToString())));
                    foreach (ShoppingCart item in this.List)
                    {
                        prod = new OrderProduct();
                        prod.OrderId = order.OrderId;
                        prod.ProductId = item.ProductId;
                        prod.Price = item.SoldPrice;
                        prod.Quantity = item.Quantity;
                        prod.Date = DateTime.Now;
                        prod.UnitId = item.UnitId;

                        db.OrderProducts.Add(prod);
                        db.SaveChanges();
                        prod = null;
                    }

                    // Kullanıcıya ait sepet boşaltılıyor.
                    IEnumerable<ShoppingCart> cart = (from x in db.ShoppingCarts where x.CustomerId == this.CustomerId select x).ToList();
                    
                    db.ShoppingCarts.RemoveRange(cart);
                    db.SaveChanges();

                    _showMessage(true, "", "Siparişinizi aldık. En kısa zamanda sizi bilgilendireceğiz.", "warning");

                }

            }
            else
            {
                _showMessage(true, "", "Lütfen adres bilginizi <a href='/ShoppingCart/Addresses'>girin</a>.", "warning");
            }
        }

        public void CreateCreditCard(OrderCard card)
        {

        }

        void _showMessage(bool status = false, string title = "", string body = "", string cssclass = "")
        {
            this.AlertStatus = status;
            this.AlertTitle = title;
            this.AlertBody = body;
            this.AlertCssClass = cssclass;
        }

    }
}
