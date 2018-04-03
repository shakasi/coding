using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;

namespace jqGrid
{
    /// <summary>
    /// 本类是用来测试get和post提交方式的细节的
    /// </summary>
    public class Handler2 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //这是第三方的解析Json, JsonConvert.DeserializeObject<T>(json, jsetting)
            string json = new StreamReader(context.Request.InputStream).ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var a = context.Request["aa"];
            object m = null;
            try
            {
               m = js.Deserialize<AAModel>(json);
               //m = js.Deserialize<AAModel>(a);
            }
            catch (Exception ex)
            {

            }
            string str = js.Serialize(m);
            context.Response.Write(str);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class AAModel
    {
        public string AA { get; set; }
    }
}