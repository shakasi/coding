using System;
using System.IO;
using System.Web;
using System.Text;

namespace FishDemoCodeLib
{
	public static class HtmlExtension
	{
		public static string RefJsFileHtml(string path)
		{
			string filePath = HttpRuntime.AppDomainAppPath + path.Replace("/", "\\");
			string version = File.GetLastWriteTimeUtc(filePath).Ticks.ToString();
			return string.Format("<script type=\"text/javascript\" src=\"{0}?_t={1}\"></script>\r\n", path, version);
		}

		public static string RefCssFileHtml(string path)
		{
			string filePath = HttpRuntime.AppDomainAppPath + path.Replace("/", "\\");
			string version = File.GetLastWriteTimeUtc(filePath).Ticks.ToString();
			return string.Format("<link type=\"text/css\" rel=\"Stylesheet\" href=\"{0}?_t={1}\" />\r\n", path, version);
		}
	}
}
