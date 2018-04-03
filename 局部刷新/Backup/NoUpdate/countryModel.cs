using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace NoUpdate
{
    public class countryModel
    {
        private string id;
        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        private string name;

        public string NAME
        {
            get { return name; }
            set { name = value; }
        }
    }
}
