using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace jqGrid
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //Url取参方式
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            //response.ContentType = "text/plain";
            string _search = request["_search"];
            string numberOfRows = request["rows"];
            string pageIndex = request["page"];
            string sortColumnName = request["sidx"];
            string sortOrderBy = request["sord"];
            string pars = request["pars"];

            string json = new StreamReader(request.InputStream).ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            QueryConfig m = null;
            if (!string.IsNullOrEmpty(pars))
            {
                m = js.Deserialize<QueryConfig>(pars);
            }
            try
            {
                List<ChinaUser> dataSourcelist = new List<ChinaUser>()
                    {
                        new ChinaUser() { ID=1,UserName="Wujy",CreateTime=Convert.ToDateTime("2016/7/13")},
                        new ChinaUser() { ID=2,UserName="22",CreateTime=Convert.ToDateTime("2016/7/13")},
                        new ChinaUser() { ID=3,UserName="33",CreateTime=Convert.ToDateTime("2016/7/13")},
                        new ChinaUser() { ID=4,UserName="44",CreateTime=Convert.ToDateTime("2016/7/13")},
                        new ChinaUser() { ID=5,UserName="55",CreateTime=Convert.ToDateTime("2016/7/13")},
                        new ChinaUser() { ID=6,UserName="66",CreateTime=Convert.ToDateTime("2016/7/13")}
                    };
                List<ChinaUser> dataList = dataSourcelist;
                if (m != null && m.ID != null)
                {
                    dataList = dataSourcelist.Where(a => a.ID == m.ID).ToList();
                }

                dataList=QueryByPage(Convert.ToInt32(numberOfRows), Convert.ToInt32(pageIndex), dataList).ToList();

                GridData model = new GridData();
                model.page = pageIndex;
                model.records = Convert.ToString(dataSourcelist.Count);//总行数
                model.total = "2";//算出总页数
                model.rows = dataList;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string Resul = serializer.Serialize(model);
                context.Response.Write(Resul);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected List<ChinaUser> QueryByPage(int PageSize, int CurPage, IList<ChinaUser> objs)
        {
            var query = from cms_contents in objs select cms_contents;
            return query.Take(PageSize * CurPage).Skip(PageSize * (CurPage - 1)).ToList();
        }
    }

    /// <summary>
    /// 界面jqGrid 分页信息+数据源
    /// </summary>
    public class GridData
    {
        public string page { set; get; }

        public string total { get; set; }

        public string records { get; set; }

        public List<ChinaUser> rows { get; set; }
    }

    /// <summary>
    /// 界面jqGrid数据源
    /// </summary>
    public class ChinaUser
    {
        public int ID { set; get; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class QueryConfig
    {
        public int? ID { get; set; }

        //public bool _search { get; set; }
        //public string nd { get; set; }
        //public string rows { get; set; }
        //public string page { get; set; }
        //public string sidx { get; set; }
        //public string sord { get; set; }
    }
}