using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace FishDemoCodeLib
{
	public static class MyExtensions
	{
		public static string HtmlEncode(this string str)
		{
			if( string.IsNullOrEmpty(str) )
				return string.Empty;

			return HttpUtility.HtmlEncode(str);
		}

		public static string HtmlAttributeEncode(this string str)
		{
			if( string.IsNullOrEmpty(str) )
				return string.Empty;

			return HttpUtility.HtmlAttributeEncode(str);
		}

		/// <summary>
		/// 返回对象的JSON序列化的字符串
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string ToJson(this object obj)
		{
			if( obj == null )
				return string.Empty;

			return (new JavaScriptSerializer()).Serialize(obj);
		}

		public static string StringToHtml(this string str)
		{
			return str.HtmlEncode().Replace(" ", "&nbsp;").Replace("\r\n", "<br />");
		}


	}

}
