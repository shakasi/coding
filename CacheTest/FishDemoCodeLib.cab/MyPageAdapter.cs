using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;


// 为了不写一堆机械式的判断代码，这里就写个简单的基类，统一处理页面的提交动作。
// 注意：代码仅做演示用，并没有过多考虑性能及安全细节。

// 代码源于博客：http://www.cnblogs.com/fish-li/archive/2011/12/27/2304063.html
// 现整理于此。


namespace FishDemoCodeLib
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class SubmitMethodAttribute : Attribute
	{
		public bool AutoRedirect { get; set; }
	}

	internal sealed class MethodInvokeInfo
	{
		public MethodInfo MethodInfo;
		public SubmitMethodAttribute MethodAttribute;
	}

	public class MyPageAdapter : System.Web.UI.Adapters.PageAdapter
	{
		private static readonly Hashtable s_table = Hashtable.Synchronized(new Hashtable());

		private static MethodInvokeInfo[] GetMethodInfo(Type type)
		{
			MethodInvokeInfo[] array = s_table[type.AssemblyQualifiedName] as MethodInvokeInfo[];
			if( array == null ) {
				array = (from m in type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
						 let a = m.GetCustomAttributes(typeof(SubmitMethodAttribute), false) as SubmitMethodAttribute[]
						 where a.Length > 0
						 select new MethodInvokeInfo { MethodInfo = m, MethodAttribute = a[0] }).ToArray();

				s_table[type.AssemblyQualifiedName] = array;
			}
			return array;
		}


		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if( Page.Request.Form.AllKeys.Length == 0 )
				return;	// 没有提交表单

			MethodInvokeInfo[] array = GetMethodInfo(Page.GetType().BaseType);
			if( array.Length == 0 )
				return;

			foreach( MethodInvokeInfo m in array ) {
				if( string.IsNullOrEmpty(Page.Request.Form[m.MethodInfo.Name]) == false ) {
					m.MethodInfo.Invoke(Page, null);

					if( m.MethodAttribute.AutoRedirect && Page.Response.IsRequestBeingRedirected == false )
						Page.Response.Redirect(Page.Request.RawUrl);

					return;
				}
			}
		}
	}
}
