using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using FishDemoCodeLib;

public partial class CacheDependencyDemo : System.Web.UI.Page
{
	[SubmitMethod(AutoRedirect=true)]
	public void SetKey1Cache()
	{
		SetKey2Cache();

		CacheDependency dep = new CacheDependency(null, new string[] { "key2" });
		HttpRuntime.Cache.Insert("key1", DateTime.Now.ToString(), dep, 
									Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
	}

	[SubmitMethod(AutoRedirect=true)]
	public void SetKey2Cache()
	{
		HttpRuntime.Cache.Insert("key2", Guid.NewGuid().ToString());
	}
}
