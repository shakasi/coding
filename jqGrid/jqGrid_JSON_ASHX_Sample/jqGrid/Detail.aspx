<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="jqGrid.Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="/Scripts/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">
    function GetQueryString(name)
    {
            var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if(r!=null)return  unescape(r[2]); return null;
    }
    jQuery(function () {
        // 调用方法
        $("#parentId")[0].innerText = GetQueryString("id");
    })

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            我是弹出的详细页面啊,父ID是：<span id="parentId"></span>
        </div>
    </form>
</body>
</html>
