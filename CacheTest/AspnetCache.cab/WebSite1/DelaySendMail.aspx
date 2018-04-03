<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>http://www.cnblogs.com/fish-li</title>
    <style type="text/css">
    *{ font-family: Courier New;
       font-size: 10pt;
    }
    </style>
    <script type="text/javascript" src="js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="js/DelaySendMail.js"></script>
</head>
<body>
	<p> 为了简单，示例页面只处理一条记录，且将记录的RowGuid直接显示出来。<br />
		实际场景中，这个RowGuid应该可以从一个表格的【当前选择行】中获取到。
	</p>
	<p> 当前选择行的 RowGuid = <span id="spanRowGuid"><%= Guid.NewGuid().ToString() %></span><br />
		当前选择行的 Sequence= <span id="spanSequence">0</span>
	</p>
	<p><input type="button" id="btnMoveUp" value="上移" />
		<input type="button" id="btnMoveDown" value="下移" />
	</p>
</body>
</html>
