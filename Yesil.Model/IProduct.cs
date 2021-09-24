using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PagedList;
using PagedList.Mvc;
using Yesil.Data;

namespace Yesil.Model
{
    public class IProduct : Data.Product
    {

        YesilEntities db = new YesilEntities();

        public bool AlertStatus { get; set; }
        public string AlertTitle { get; set; }
        public string AlertBody { get; set; }
        public string AlertCssClass { get; set; }

        public Product Product { get; set; }
        public List<Product> Products { get; set; }

        public List<ShoppingCart> Cart;

        public IProduct(int Id = 0)
        {
            try
            {
                if (Id != 0)
                {
                    this.Product = db.Products.Where(x => x.ProductId == Id).FirstOrDefault();
                }
                else
                {
                    this.Products = db.Products.ToList();
                }
            }
            catch (Exception ex)
            {
                _showMessage(true, "Uyarı", ex.Message, "danger");
            }
        }

        public void Get(int Id)
        {
            this.Product = db.Products.Where(x => x.ProductId == Id).FirstOrDefault();
        }

        public void Category(int Id=0)
        {
            try
            {
                this.Products = db.Products.Where(x => x.CategoryId == Id).ToList();
            }
            catch (Exception ex)
            {
                _showMessage(true, "Uyarı", ex.Message, "danger");
            }
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
