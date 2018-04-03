

$(function () {
    $('#tb_menus').bootstrapTable({
        url: '/PowerManager/GetMenus',
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
        pageSize: 5,
        pageList: [5, 25, 50, 100],
        search: true,
        showColumns: true,
        showRefresh: true,
        minimumCountColumns: 2,
        clickToSelect: true,
        columns: [{
            checkbox: true
        }, {
            field: 'MENU_NAME',
            title: '菜单名称'
        }, {
            field: 'MENU_URL',
            title: '菜单URL'
        }, {
            field: 'PARENT_ID',
            title: '父级菜单'
        }, {
            field: 'MENU_LEVEL',
            title: '菜单级别'
        }, ]
    });

    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

});

function queryParams(params) {  //配置参数
    var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
        limit: params.limit,   //页面大小
        offset: params.offset,  //页码
        menuname: $("#txt_search_menuname").val(),
        menuurl: $("#txt_search_menuurl").val()
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
            $('#myModal').modal()

            postdata.MENU_ID = "";
        });

        $("#btn_edit").click(function () {
            var arrselections = $("#tb_menus").bootstrapTable('getSelections');
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
            $("#txt_menuname").val(arrselections[0].MENU_NAME);
            $("#txt_menuurl").val(arrselections[0].MENU_URL);
            $("#txt_parentmenu").val(arrselections[0].PARENT_ID);
            $("#txt_menulevel").val(arrselections[0].MENU_LEVEL);

            postdata.MENU_ID = arrselections[0].MENU_ID;
            $('#myModal').modal();
        });

        $("#btn_delete").click(function () {
            var arrselections = $("#tb_menus").bootstrapTable('getSelections');
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
                url: "/PowerManager/Delete",
                data: { "": arrselections[0].MENU_ID },
                success: function (data, status) {
                    if (status == "success") {
                        alert("提交数据成功");
                        $("#tb_menus").bootstrapTable('refresh');
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
            postdata.MENU_NAME = $("#txt_menuname").val();
            postdata.MENU_URL = $("#txt_menuurl").val();
            postdata.PARENT_ID = $("#txt_parentmenu").val();
            postdata.MENU_LEVEL = $("#txt_menulevel").val();
            $.ajax({
                type: "post",
                url: "/PowerManager/GetEdit",
                data: { "": JSON.stringify(postdata) },
                success: function (data, status) {
                    if (status == "success") {
                        alert("提交数据成功");
                        $("#tb_menus").bootstrapTable('refresh');
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
            $("#tb_menus").bootstrapTable('refresh');
        });
    };

    return oInit;
};