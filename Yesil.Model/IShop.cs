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
    public class IShop
    {

        public IProduct Product;
        public ICustomer Customer;
        public IShoppingCart Cart;
        public IAlert Alert;

        public IShop()
        {
            Alert = new IAlert();
            Product = new IProduct();
            Customer = new ICustomer();
            Cart = new IShoppingCart(this.Customer.CustomerId);
            Product.Cart = Cart.List;
        }

    }
}
