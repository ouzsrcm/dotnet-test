using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Yesil.Data;

namespace Yesil.Model
{
    public class ICustomer : Data.Customer
    {

        YesilEntities db = new YesilEntities();

        public IAlert Alert;
        public IOrder Order;
        public CustomerCredential Credential;
        public IShoppingCart ShoppingCart;

        public bool CreateStatus { get; set; }
        public bool LoginStatus { get; set; }
        public string Password { get; set; }

        public List<Order> OrderList { get; set; }
        public List<BankAccount> BankAccounts { get; set; }
        public List<Faq> Faqs { get; set; }

        public CustomerAddress Address;

        public ICustomer()
        {
            Alert = new IAlert();
            Order = new IOrder();
            ShoppingCart = new IShoppingCart();
            this.ShoppingCart.CustomerId = this.CustomerId;
        }

        public void Create(Customer model)
        {
            try
            {
                if (!this.Check(model))
                {
                    db.Customers.Add(model);
                    db.SaveChanges();
                    this.CreateStatus = true;
                    this.CustomerId = model.CustomerId;
                }
                else Alert.Set(true, "", "Sistemde tanımlı böyle bir eposta adresi zaten bulunuyor. Lütfen farklı bir eposta adresi deneyin.", "danger");
            }
            catch (Exception ex)
            {
                Alert.Set(true, "", "Hesap oluşturulurken bir hata oluştu. Detaylar:" + ex.Message, "danger");
            }
        }

        public void Create(CustomerCredential model)
        {
            try
            {
                if (!this.Check(model))
                {
                    db.CustomerCredentials.Add(model);
                    db.SaveChanges();
                    this.CreateStatus = true;
                    Alert.Set(true, "", "Hesabınız başarıyla oluşturuldu. Lütfen giriş yapın.", "success");
                }
                else Alert.Set(true, "", "Sistemde tanımlı böyle bir eposta adresi zaten bulunuyor. Lütfen farklı bir eposta adresi deneyin.", "danger");
            }
            catch (Exception ex)
            {
                Alert.Set(true, "", "Kayıt oluşturulurken bir hata oluştu. Detaylar:" + ex.Message, "danger");
            }
        }

        public bool Check(Customer model)
        {
            try
            {
                var customer = db.Customers.Where(x => x.Email == model.Email).FirstOrDefault();
                if (customer != null)
                    return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Check(CustomerCredential model)
        {
            try
            {
                var cre = db.CustomerCredentials.Where(x => x.Email == model.Email).FirstOrDefault();
                if (cre != null)
                    return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

        public new void Orders(int Id = 0)
        {
            this.Order.CustomerId = Id;
            this.Order.List();
            this.OrderList = this.Order.Orders;
        }

        public void Login(CustomerCredential _cre = null)
        {
            Alert = new IAlert();
            this.LoginStatus = false;
            try
            {
                Credential = (from x in db.CustomerCredentials
                              where x.Email == _cre.Email && x.Password == _cre.Password && x.IsActive == true
                              select x).FirstOrDefault();
                if (Credential != null)
                {
                    var _customer = (from x in db.Customers where x.CustomerId == Credential.CustomerId select x).FirstOrDefault();

                    this.CustomerId = _customer.CustomerId;
                    this.Firstname = _customer.Firstname;
                    this.Lastname = _customer.Lastname;
                    this.Email = _customer.Email;
                    this.Phone1 = _customer.Phone1;
                    this.Phone2 = _customer.Phone2;
                    _customer = null;

                    this.LoginStatus = true;

                    Alert.Set(true, "", "İşlem başarılı.", "success");
                }
                else
                {
                    Alert.Set(true, "", "Sistemde tanımlı böyle bir kullanıcı bulunamadı.", "warning");
                }
            }
            catch (Exception ex)
            {
                Alert.Set(true, "Uyarı", ex.Message, "danger");
            }
        }

        public void Get()
        {
            var _customer = (from x in db.Customers where x.CustomerId == this.CustomerId select x).FirstOrDefault();
            this.CustomerId = _customer.CustomerId;
            this.Firstname = _customer.Firstname;
            this.Lastname = _customer.Lastname;
            this.Email = _customer.Email;
            this.Phone1 = _customer.Phone1;
            this.Phone2 = _customer.Phone2;

            this.Credential = db.CustomerCredentials.Where(x => x.CustomerId == _customer.CustomerId).FirstOrDefault();

            _customer = null;
        }

        public void GetAddress()
        {
            try
            {
                this.Address = db.CustomerAddresses.Where(x => x.CustomerId == this.CustomerId).FirstOrDefault();
                if (this.Address == null)
                {
                    this.Address = new CustomerAddress();
                }
            }
            catch (Exception ex)
            {
                this.Address = new CustomerAddress();
                Alert.Set(true, "", ex.Message, "warning");
            }
        }

        public void UpdateAddress(CustomerAddress model)
        {
            try
            {
                var addr = (from x in db.CustomerAddresses where x.CustomerAddressId == model.CustomerAddressId select x).FirstOrDefault();
                if (addr != null)
                {
                    addr.Title = model.Title;
                    addr.Phone1 = model.Phone1;
                    addr.Phone2 = model.Phone2;
                    addr.Phone3 = model.Phone3;
                    addr.Email = model.Email;
                    addr.Address = model.Address;
                    addr.City = model.City;
                    addr.PostalCode = model.PostalCode;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Alert.Set(true, "", ex.Message, "warning");
            }
        }

        public void InsertAddress(CustomerAddress model)
        {
            try
            {
                db.CustomerAddresses.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Alert.Set(true, "", ex.Message, "warning");
            }
        }

        public void Update(Customer model)
        {
            try
            {
                var customer = (from x in db.Customers where x.CustomerId == model.CustomerId select x).FirstOrDefault();
                if (customer != null)
                {
                    customer.Firstname = model.Firstname;
                    customer.Lastname = model.Lastname;
                    customer.Email = model.Email;
                    customer.Phone1 = model.Phone1;
                    customer.Phone2 = model.Phone2;
                    db.SaveChanges();
                    Alert.Set(true, "", "Değişiklikler başarıyla kaydedildi.", "success");
                }
                else
                {
                    Alert.Set(true, "", "Sisteme böyle bir kullanıcı bulunamadı.", "warning");
                }
            }
            catch (Exception ex)
            {
                Alert.Set(true, "", ex.Message, "danger");
            }
        }

        public void UpdateCredential(CustomerCredential model)
        {
            try
            {
                var cre = (from x in db.CustomerCredentials where x.CustomerCredentialId == model.CustomerCredentialId select x).FirstOrDefault();
                if (cre != null)
                {
                    cre.Email = model.Email;
                    cre.Password = model.Password;
                    db.SaveChanges();
                    Alert.Set(true, "", "Değişiklikler başarıyla kaydedildi.", "success");
                }
                else
                {
                    Alert.Set(true, "", "Sisteme böyle bir kullanıcı bulunamadı.", "warning");
                }
            }
            catch (Exception ex)
            {
                Alert.Set(true, "", ex.Message, "danger");
            }
        }

        public void GetBankAccounts()
        {
            this.BankAccounts = db.BankAccounts.ToList();
        }

        public void GetFaqs()
        {
            this.Faqs = db.Faqs.ToList();
        }

    }
}
