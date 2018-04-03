using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Caching;
using System.IO;

public static class WebSiteApp
{
    #region 初始化

    public static readonly string AppDataPath =
        Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data");


    public static void Init()
    {
        LoadRunOptions();

        System.Timers.Timer t = new System.Timers.Timer(2000);
        t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
        t.Start();
    }

    static void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {

    }

    #endregion


    private static readonly string RunOptionsCacheKey = "aa";//Guid.NewGuid().ToString();
    private static readonly string _fileName = "22.mp4";
    private static int _cacheNumber = 0;


    private static int s_RunOptionsCacheDependencyFlag = 0;

    public static void LoadRunOptions()
    {
        string path = Path.Combine(AppDataPath, _fileName);
        // 注意啦：访问文件是可能会出现异常。不要学我，我写的是示例代码。

        var aadf= HttpRuntime.Cache.Get("asfasf");
        int flag = System.Threading.Interlocked.CompareExchange(ref s_RunOptionsCacheDependencyFlag, 1, 0);

        // 确保只调用一次就可以了。
        if (flag == 0)
        {
            // 让Cache帮我们盯住这个配置文件。
            CacheDependency dep = new CacheDependency(path);
            HttpRuntime.Cache.Insert(RunOptionsCacheKey, _cacheNumber++, dep, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, RunOptionsUpdateCallback);
            //HttpRuntime.Cache.Insert(RunOptionsCacheKey, _cacheNumber++, dep, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
        }
    }


    public static void RunOptionsUpdateCallback(
        string key, CacheItemUpdateReason reason,
        out object expensiveObject,
        out CacheDependency dependency,
        out DateTime absoluteExpiration,
        out TimeSpan slidingExpiration)
    {
        // 注意哦：在这个方法中，不要出现【未处理异常】，否则缓存对象将被移除。

        // 说明：这里我并不关心参数reason，因为我根本就没有使用过期时间
        //        所以，只有一种原因：依赖的文件发生了改变。
        //        参数key我也不关心，因为这个方法是【专用】的。

        string file=Path.Combine(AppDataPath, _fileName);
        if(File.Exists(file))
        {
            expensiveObject = _cacheNumber++;
            dependency = new CacheDependency(Path.Combine(AppDataPath, _fileName));
        }
        else
        {
            expensiveObject = _cacheNumber++;
            dependency = new CacheDependency(Path.Combine(AppDataPath, _fileName));
            HttpRuntime.Cache.Remove(key);
        }

        absoluteExpiration = Cache.NoAbsoluteExpiration;
        slidingExpiration = Cache.NoSlidingExpiration;

        //// 检验代码是否被同一个修改文件的事件执行了二次，【注意目录是我机器上的】
        //System.IO.File.WriteAllText("c:\\Temp\\" + Guid.NewGuid().ToString() + ".txt", key);
        // 由于事件发生时，文件可能还没有完全关闭，所以只好让程序稍等。
        System.Threading.Thread.Sleep(50);
    }


    #region 使用移除【后】通知的版本
    //public static RunOptions LoadRunOptions()
    //{
    //    string path = Path.Combine(AppDataPath, "RunOptions.xml");
    //    // 注意啦：访问文件是可能会出现异常。不要学我，我写的是示例代码。
    //    RunOptions options = RwConfigDemo.XmlHelper.XmlDeserializeFromFile<RunOptions>(path, Encoding.UTF8);


    //    // 让Cache帮我们盯住这个配置文件。
    //    CacheDependency dep = new CacheDependency(path);
    //    HttpRuntime.Cache.Insert(RunOptionsCacheKey, "Fish Li", dep,
    //        Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, RunOptionsRemovedCallback);

    //    return options;
    //}


    //private static void RunOptionsRemovedCallback(string key, object value, CacheItemRemovedReason reason)
    //{
    //    //// 检验代码是否被同一个修改文件的事件执行了二次，【注意目录是我机器上的】
    //    //System.IO.File.WriteAllText("c:\\Temp\\" + Guid.NewGuid().ToString() + ".txt", key);
    //    // 由于事件发生时，文件可能还没有完全关闭，所以只好让程序稍等。
    //    System.Threading.Thread.Sleep(50);

    //    // 重新加载配置参数
    //    s_RunOptions = LoadRunOptions();
    //}
    #endregion



    #region 最老的版本，直接使用Cache
    //public static RunOptions RunOptions
    //{
    //    get
    //    {
    //        // 首先尝试从缓存中获取运行参数
    //        RunOptions options = HttpRuntime.Cache[RunOptionsCacheKey] as RunOptions;
    //        if( options == null ) {
    //            // 缓存中没有，则从文件中加载
    //            string path = Path.Combine(AppDataPath, "RunOptions.xml");
    //            options = RwConfigDemo.XmlHelper.XmlDeserializeFromFile<RunOptions>(path, Encoding.UTF8);

    //            // 把从文件中读到的结果放入缓存，并设置与文件的依赖关系。
    //            CacheDependency dep = new CacheDependency(path);
    //            // 如果您的参数较复杂，与多个文件相关，那么也可以使用下面的方式，传递多个文件路径。
    //            //CacheDependency dep = new CacheDependency(new string[] { path });
    //            HttpRuntime.Cache.Insert(RunOptionsCacheKey, options, dep);
    //        }
    //        return options;
    //    }
    //}
    #endregion
}





///// <summary>
///// 定义一个回调方法，用于在从缓存中移除缓存项之前通知应用程序。
///// </summary>
///// <param name="key">要从缓存中移除的项的标识符。</param>
///// <param name="reason">要从缓存中移除项的原因。</param>
///// <param name="expensiveObject">此方法返回时，包含含有更新的缓存项对象。</param>
///// <param name="dependency">此方法返回时，包含新的依赖项的对象。</param>
///// <param name="absoluteExpiration">此方法返回时，包含对象的到期时间。</param>
///// <param name="slidingExpiration">此方法返回时，包含对象的上次访问时间和对象的到期时间之间的时间间隔。</param>
//public delegate void CacheItemUpdateCallback(string key, CacheItemUpdateReason reason, 
//                out object expensiveObject, 
//                out CacheDependency dependency, 
//                out DateTime absoluteExpiration, 
//                out TimeSpan slidingExpiration);

///// <summary>
///// 指定要从 Cache 对象中移除缓存项的原因。
///// </summary>
//public enum CacheItemUpdateReason
//{
//    /// <summary>
//    /// 指定要从缓存中移除项的原因是绝对到期或可调到期时间间隔已到期。
//    /// </summary>
//    Expired = 1,
//    /// <summary>
//    /// 指定要从缓存中移除项的原因是关联的 CacheDependency 对象发生了更改。
//    /// </summary>
//    DependencyChanged = 2,
//}

