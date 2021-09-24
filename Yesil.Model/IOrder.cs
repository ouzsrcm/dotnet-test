using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PagedList;
using PagedList.Mvc;
using Yesil.Data;
using Yesil.Model;

namespace Yesil.Model
{
    public class IOrder : Order
    {

        public IAlert Alert;

        public List<Order> Orders;

        public OrderCard OrderCard;

        public string HolderName { get; set; }
        public string Number { get; set; }
        public int ExpirationYear { get; set; }
        public int ExpirationMonth { get; set; }
        public int Ccv { get; set; }

        YesilEntities db = new YesilEntities();

        public Customer Customer;
        public CustomerAddress CustomerAddress;
        public List<OrderProduct> Products;
        public List<OrderState> OrderStatus;

        public IOrder()
        {
            Alert = new IAlert();
            OrderCard = new OrderCard();
        }

        public void List()
        {
            try
            {
                this.Orders = db.Orders.Where(x => x.CustomerId == this.CustomerId).ToList();
            }
            catch
            {
                this.Alert.Set(true, "", "Sipariş listesi alınırken bir hata ile karşılaşıldı.", "warning");
            }
        }

        public void ListAll(bool asc = true)
        {
            try
            {
                if (asc)
                {
                    this.Orders = db.Orders.ToList();
                }
                else
                {
                    this.Orders = db.Orders.OrderByDescending(x => x.OrderId).ToList();
                }
            }
            catch
            {
                this.Alert.Set(true, "", "Sipariş listesi alınırken bir hata ile karşılaşıldı.", "warning");
            }
        }

        public void Details(int Id = 0)
        {
            try
            {
                var _item = db.Orders.Where(x => x.OrderId == Id).FirstOrDefault();
                this.OrderId = Id;
                this.CustomerId = _item.CustomerId;
                this.DeliveryAddressId = _item.DeliveryAddressId;
                this.BillingAddressId = _item.BillingAddressId;
                this.Description = _item.Description;
                this.PaymentType = _item.PaymentType;
                this.Date = _item.Date;
                this.OrderStateId = _item.OrderStateId;

                this.Customer = db.Customers.Where(x => x.CustomerId == this.CustomerId).FirstOrDefault();
                this.CustomerAddress = db.CustomerAddresses.Where(x => x.CustomerId == this.Customer.CustomerId).FirstOrDefault();
                this.Products = db.OrderProducts.Where(x => x.OrderId == Id).ToList();
                this.OrderStatus = db.OrderStates.ToList();

                if (this.PaymentType == "card")
                {
                    this.OrderCard = db.OrderCards.Where(x => x.OrderId == this.OrderId).FirstOrDefault();
                }

            }
            catch
            {
                this.Alert.Set(true, "", "Sipariş listesi alınırken bir hata ile karşılaşıldı.", "warning");
            }
        }

    }
}
