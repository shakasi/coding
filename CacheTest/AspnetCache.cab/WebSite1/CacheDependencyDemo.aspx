<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CacheDependencyDemo.aspx.cs" Inherits="CacheDependencyDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>http://www.cnblogs.com/fish-li</title>
</head>
<body>
    <p>Key1 的缓存内容：<%= HttpRuntime.Cache["key1"] %></p>
    <hr />
        
    <form action="CacheDependencyDemo.aspx" method="post">
		<input type="submit" name="SetKey1Cache" value="设置Key1的值" />
		<input type="submit" name="SetKey2Cache" value="设置Key2的值" />
    </form>
</body>
</html>
