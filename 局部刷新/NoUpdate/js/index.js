jQuery(function () 
{
    $("#Text1").blur
    (
        function() 
        {
            /*1、POST请求，不能使用客户端缓存
              2、GET请求，可以使用客户端缓存（而且只要地址一样，它总是会使用客户端缓存）.
             【备注】一个TCP包的尺寸大约为1452字节。
              当然，现实的项目中，并不是总能使用GET的，例如长度方面可能会有限制：
              The maximum URL length in IE is 2K, so if you send more than 2K data you might not be able to use GET.
            */
            $.get("a.ashx", { ID: "a" }, function (data, status) {
                if (status == "success") 
                {
                    if (data != null) {
                        var deplist = $.parseJSON(data);
                        var str = "<option value=0>请选择</option>";
                        //alert(data);
                        for (var i = 0; i < deplist.length; i++) {
                            str += "<option value=" + deplist[i].ID + ">" + deplist[i].NAME + "</option>";
                        }
                        $("#select").html(str);
                    }
                }
            }
        )
        }
    )
});