using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.IO;
using MySimpleServiceFramework;

// 【为了简单，几个相关的类就直接混在这个文件中了。】


/// <summary>
/// 移动记录的相关信息。
/// </summary>
public class MoveRecInfo
{
	public string RowGuid;
	public int Direction;
	public string Reason;
}



// 下面这个服务端的实现使用了我在博客【用Asp.net写自己的服务框架】中的服务框架，
// 网址：http://www.cnblogs.com/fish-li/archive/2011/09/05/2168073.html

[MyService]
public class AjaxDelaySendMail
{
	[MyServiceMethod]
	public int MoveRec(MoveRecInfo info)
	{
		// 这里就不验证从客户端传入的参数了。实际开发中这个是必须的。

		// 先来调整记录的顺序，示例程序没有数据库，就用Cache来代替。
		int sequence = 0;
		int.TryParse(HttpRuntime.Cache[info.RowGuid] as string, out sequence);
		// 简单地示例一下调整顺序。
		sequence += info.Direction;
		HttpRuntime.Cache[info.RowGuid] = sequence.ToString();


		string key = info.RowGuid +"_DelaySendMail";
		// 这里我不直接发邮件，而是把这个信息放入Cache中，并设置2秒的滑过过期时间，并指定移除通知委托
		// 将操作信息放在缓存，并且以覆盖形式放入，这样便可以实现保存最后状态。
		// 注意：这里我用Insert方法。
		HttpRuntime.Cache.Insert(key, info, null, Cache.NoAbsoluteExpiration,
			TimeSpan.FromMinutes(2.0), CacheItemPriority.NotRemovable, MoveRecInfoRemovedCallback);

		return sequence;
	}	

	private void MoveRecInfoRemovedCallback(string key, object value, CacheItemRemovedReason reason)
	{
		if( reason == CacheItemRemovedReason.Removed )
			return;		// 忽略后续调用HttpRuntime.Cache.Insert()所触发的操作

		// 能运行到这里，就表示是肯定是缓存过期了。
		// 换句话说就是：用户2分钟再也没操作过了。

		// 从参数value取回操作信息
		MoveRecInfo info = (MoveRecInfo)value;
		// 这里可以对info做其它的处理。

		// 最后发一次邮件。整个延迟发邮件的过程就处理完了。
		MailSender.SendMail(info);
	}
}


public static class MailSender
{
	public static void SendMail(MoveRecInfo info)
	{
		// 为了演示简单，这里就不发邮件了，直接写文件。

		string mailBody = info.Reason + "\r\n操作时间：" + DateTime.Now.ToString() + "\r\n\r\n\r\n";
		// 为了便于验证这个方法的调用次数，写文件时，并不覆盖，而是追加内容。
		string filePath = Path.Combine(WebSiteApp.AppDataPath, info.RowGuid + ".txt");
		File.AppendAllText(filePath, mailBody, System.Text.Encoding.UTF8);
	}
}