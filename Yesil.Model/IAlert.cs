using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yesil.Model
{
    public class IAlert
    {
        public bool AlertStatus { get; set; }
        public string AlertTitle { get; set; }
        public string AlertBody { get; set; }
        public string AlertCssClass { get; set; }

        public void Set(bool status = false, string title = "", string body = "", string cssclass = "")
        {
            this.AlertStatus = status;
            this.AlertTitle = title;
            this.AlertBody = body;
            this.AlertCssClass = cssclass;
        }

    }
}
