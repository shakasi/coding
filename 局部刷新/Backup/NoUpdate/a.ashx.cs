using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;

namespace NoUpdate
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class a : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request["ID"] == "a")
            {
                countryModel cm1 = new countryModel() { ID = "0", NAME = "中国" };
                countryModel cm2 = new countryModel() { ID = "1", NAME = "美国" };
                IList<countryModel> cmlist = new List<countryModel>();
                cmlist.Add(cm1);
                cmlist.Add(cm2);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                string str = jss.Serialize(cmlist);
                context.Response.Write(str);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
