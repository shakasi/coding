using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace FishDemoCodeLib
{
	public static class MyHttpClient
	{
		public static HttpWebRequest CreateHttpWebRequest(string url)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.UserAgent = "Fish's C# Client";
			return request;
		}

		public static void WritePostData(HttpWebRequest request, string postData, Encoding encoding)
		{
			if( request == null )
				throw new ArgumentNullException("request");

			if( string.IsNullOrEmpty(postData) )
				return;

			if( encoding == null )
				encoding = Encoding.UTF8;

			if( request.Method != "POST" ) {
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded; charset=" + encoding.WebName;
			}

			using( BinaryWriter bw = new BinaryWriter(request.GetRequestStream()) ) {
				bw.Write(encoding.GetBytes(postData));
			}
		}

		public static string GetResponseText(HttpWebRequest request)
		{
			if( request == null )
				throw new ArgumentNullException("request");

			HttpWebResponse response = null;

			try {
				response = (HttpWebResponse)request.GetResponse();
			}
			catch( WebException wex ) {
				if( wex.Response == null )
					return wex.Message;

				response = (HttpWebResponse)wex.Response;
			}

			using( response ) {
				Stream strem = response.GetResponseStream();
				using( StreamReader reader = new StreamReader(strem, Encoding.GetEncoding(response.CharacterSet)) ) {
					return reader.ReadToEnd();
				}
			}
		}




		public static string HttpGet(string url, CookieContainer cookieContainer)
		{
			HttpWebRequest request = CreateHttpWebRequest(url);

			if( cookieContainer != null )
				request.CookieContainer = cookieContainer;

			return GetResponseText(request);
		}


		public static string HttpPost(string url, string postData, CookieContainer cookieContainer)
		{
			HttpWebRequest request = CreateHttpWebRequest(url);

			if( cookieContainer != null )
				request.CookieContainer = cookieContainer;

			WritePostData(request, postData, Encoding.UTF8);

			return GetResponseText(request);
		}

	}
}
