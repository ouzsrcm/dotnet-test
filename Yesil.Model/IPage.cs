using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Yesil.Data;

namespace Yesil.Model
{
    public class IPage : Page
    {

        public YesilEntities db = new YesilEntities();


        public string Js { get; set; }
        public string Css { get; set; }
        public string PreTitle { get; set; }
        public string ShortAbout { get; set; }

        public Company Company { get; set; }
        public List<SocialAccount> SocialAccounts { get; set; }
        public PagePlugin Plugin { get; set; }
        public List<Category> Categories { get; set; }

        public IPage(string Name)
        {

            this.Company = db.Companies.Where(x => x.IsDefault == true).FirstOrDefault();

            this.Categories = db.Categories.ToList();

            if (this.Company != null)
            {
                this.PreTitle = this.Company.Title;
                this.ShortAbout = this.Company.Description;
            }

            this.SocialAccounts = db.SocialAccounts.ToList();

            var _page = db.Pages.Where(x => x.Name == Name).FirstOrDefault();

            this.Keyword = _page.Keyword;
            this.Description = _page.Description;

            if (_page != null)
            {

                Plugin = (from x in db.PagePlugins where ((int)x.PageId) == _page.PageId && x.PluginType == "css" select x).FirstOrDefault();

                if (Plugin != null)
                {
                    this.Css = Plugin.Details;
                }

                Plugin = (from x in db.PagePlugins where ((int)x.PageId) == _page.PageId && x.PluginType == "js" select x).FirstOrDefault();

                if (Plugin != null)
                {
                    this.Js = Plugin.Details;
                }

            }

        }

    }
}
