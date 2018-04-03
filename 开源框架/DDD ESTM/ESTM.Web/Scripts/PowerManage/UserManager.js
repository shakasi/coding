

$(function () {
    $('#tb_users').bootstrapTable({
        url: '/PowerManager/GetUsers',
        method: 'get',
        toolbar: '#toolbar',
        striped: true,
        cache: false,
        striped: true,
        pagination: true,
        sortable: true,
        queryParams: queryParams,
        queryParamsType: "limit",
        sidePagination: "server",
        pageSize: 10,
        pageList: [10, 25, 50, 100],
        search: true,
        showColumns: true,
        showRefresh: true,
        minimumCountColumns: 2,
        clickToSelect: true,
        columns: [{
            checkbox: true
        }, {
            field: 'USER_NAME',
            title: '用户名'
        }, {
            field: 'FULLNAME',
            title: '全称'
        }, {
            field: 'REMARK',
            title: '备注'
        }, ]
    });

    var oButtonInit = new ButtonInit();
    oButtonInit.Init();
    
});

function queryParams(params) {  //配置参数
    var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
        limit: params.limit,   //页面大小
        offset: params.offset,  //页码
        username: $("#txt_search_username").val(),
        fullname: $("#txt_search_fullname").val()
    };
    return temp;
}

var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};

    oInit.Init = function () {
        $("#btn_add").click(function () {
            $("#myModalLabel").text("新增");
            $("#myModal").find(".form-control").val("");
            $('#myModal').modal();

            postdata.USER_ID = "";
        });

        $("#btn_edit").click(function () {
            var arrselections = $("#tb_users").bootstrapTable('getSelections');
            if (arrselections.length > 1) {
                //alert("只能选择一行进行编辑");
                $("#btn_alert").alert();
                return;
            }
            if (arrselections.length <= 0) {
                //alert("请先选择需要编辑的行");
                $("#btn_alert").alert()
                return;
            }
            $("#myModalLabel").text("编辑");
            $("#txt_username").val(arrselections[0].USER_NAME);
            $("#txt_fullname").val(arrselections[0].FULLNAME);
            $("#txt_remark").val(arrselections[0].REMARK);

            postdata.USER_ID = arrselections[0].USER_ID;
            $('#myModal').modal();
        });

        $("#btn_delete").click(function () {
            var arrselections = $("#tb_users").bootstrapTable('getSelections');
            if (arrselections.length <= 0) {
                //alert("请先选择需要编辑的行");
                $("#btn_alert").alert()
                return;
            }

            if (!confirm("确定要删除选定的数据吗？")) {
                return;
            }

            $.ajax({
                type: "post",
                url: "/PowerManager/DeleteUser",
                data: { strID: arrselections[0].USER_ID },
                success: function (data, status) {
                    if (status == "success") {
                        alert("提交数据成功");
                        $("#tb_users").bootstrapTable('refresh');
                    }
                },
                error: function () {
                    alert("error");
                },
                complete: function () {
                    //alert("complete");
                }

            });
        });

        $("#btn_submit").click(function () {
            postdata.USER_NAME = $("#txt_username").val();
            postdata.FULLNAME = $("#txt_fullname").val();
            postdata.REMARK = $("#txt_remark").val();
            $.ajax({
                type: "post",
                url: "/PowerManager/GetUserEdit",
                data: { strPostData: JSON.stringify(postdata) },
                success: function (data, status) {
                    if (status == "success") {
                        alert("提交数据成功");
                        $("#tb_users").bootstrapTable('refresh');
                    }
                },
                error: function () {
                    //alert("error");
                },
                complete: function () {
                    //alert("complete");
                }

            });
        });

        $("#btn_query").click(function () {
            $("#tb_users").bootstrapTable('refresh');
        });

        //角色设置功能click事件注册
        $("#btn_roleset").click(function () {
            var arrselections = $("#tb_users").bootstrapTable('getSelections');
            if (arrselections.length > 1) {
                //alert("只能选择一行进行编辑");
                $("#btn_alert").alert();
                return;
            }
            if (arrselections.length <= 0) {
                //alert("请先选择需要编辑的行");
                $("#btn_alert").alert()
                return;
            }
            $.ajax({
                type: "get",
                url: "/PowerManager/GetRole",
                data: { strUserID: arrselections[0].USER_ID },
                //contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, status) {
                    if (status == "success") {
                        var strHtml = "";
                        for (var i = 0; i < data.Roles.length; i++) {
                            strHtml += "<div class='form-group'><input type='checkbox' id='" + data.Roles[i].ROLE_ID +
                                       "' /><label for='" + data.Roles[i].ROLE_ID + "'>" + data.Roles[i].ROLE_NAME +
                                       "</label></div>";
                        }

                        
                        $("#myModal_roleset").find(".modal-body").html(strHtml);
                    }
                },
                error: function () {
                    //alert("error");
                },
                complete: function () {
                    //alert("complete");
                }

            });

            $('#myModal_roleset').modal();
        });

        //角色设置功能保存按钮click事件注册
        $("#btn_submit_roleset").click(function () {
            
        });
    };

    return oInit;
};