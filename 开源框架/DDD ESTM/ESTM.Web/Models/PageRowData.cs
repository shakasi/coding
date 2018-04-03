using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESTM.Web
{
    /// <summary>
    /// 用于table服务端分页的类
    /// </summary>
    public class PageRowData
    {
        public object rows { get; set; }

        public int total { get; set; }
    }
}