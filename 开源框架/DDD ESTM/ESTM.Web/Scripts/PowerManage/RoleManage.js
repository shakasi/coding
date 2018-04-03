

$(function () {
    $('#tb_roles').bootstrapTable({
        url: '/PowerManager/GetRole',
        method: 'get',
        toolbar: '#toolbar',
        striped: true,
        cache: false,
        striped: true,
        pagination: true,
        sortable: true,
        queryParams: queryParams,
        queryParamsType: "limit",
        detailView: true,//父子表
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
            field: 'ROLE_NAME',
            title: '角色名称'
        }, {
            field: 'DESCRIPTION',
            title: '角色描述'
        }, {
            field: 'CREATETIME',
            title: '创建日期'
        }, {
            field: 'MODIFYTIME',
            title: '修改日期'
        }, {
            field: 'ROLE_DEFAULTURL',
            title: '默认页面'
        }, ],
        onExpandRow: function (index, row, $detail) {
            $($detail.html('<table></table>').find('table')).bootstrapTable({
                url: '/PowerManager/GetParentMenu',
                method: 'get',
                queryParams: {  },
                ajaxOptions: {  },
                detailView: true,//父子表
                //sidePagination: "server",
                pageSize: 10,
                pageList: [10, 25],
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
                }, ],
                onExpandRow: function (index, row, $detail) {
                    var parentid = row.MENU_ID;
                    $($detail.html('<table></table>').find('table')).bootstrapTable({
                        url: '/PowerManager/GetChildrenMenu',
                        method: 'get',
                        queryParams: { strParentID: parentid },
                        ajaxOptions: { strParentID: parentid },
                        pageSize: 10,
                        pageList: [10, 25],
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
                }
            });
        }
    });

    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

});

function queryParams(params) {  //配置参数
    var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
        limit: params.limit,   //页面大小
        offset: params.offset,  //页码
        rolename: $("#txt_search_rolename").val(),
        desc: $("#txt_search_desc").val()
    };
    return temp;
}

var ButtonInit = function () {
    var oInit = new Object();
    var strrole_id = "";
    var postdata = {};
    var arrsubmenutable = [];

    oInit.Init = function () {
        //新增数据click事件注册
        $("#btn_add").click(function () {
            $("#myModalLabel").text("新增");
            $("#myModal").find(".form-control").val("");
            $('#myModal').modal();

            postdata.ROLE_ID = "";
        });

        //编辑数据click事件注册
        $("#btn_edit").click(function () {
            var arrselections = $("#tb_roles").bootstrapTable('getSelections');
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
            $("#txt_rolename").val(arrselections[0].ROLE_NAME);
            $("#txt_roledesc").val(arrselections[0].DESCRIPTION);
            $("#txt_defaulturl").val(arrselections[0].ROLE_DEFAULTURL);

            postdata.ROLE_ID = arrselections[0].ROLE_ID;
            $('#myModal').modal();
        });

        //删除数据click事件注册
        $("#btn_delete").click(function () {
            var arrselections = $("#tb_roles").bootstrapTable('getSelections');
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
                data: { "": arrselections[0].ROLE_ID },
                success: function (data, status) {
                    if (status == "success") {
                        alert("提交数据成功");
                        $("#tb_roles").bootstrapTable('refresh');
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

        //保存编辑数据click事件注册
        $("#btn_submit").click(function () {
            postdata.ROLE_NAME = $("#txt_rolename").val();
            postdata.DESCRIPTION = $("#txt_roledesc").val();
            postdata.ROLE_DEFAULTURL = $("#txt_defaulturl").val();
            $.ajax({
                type: "post",
                url: "/PowerManager/GetEdit",
                data: { "": JSON.stringify(postdata) },
                //contentType: "application/json; charset=utf-8",
                //dataType: "json",
                success: function (data, status) {
                    if (status == "success") {
                        alert("提交数据成功");
                        $("#tb_roles").bootstrapTable('refresh');
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

        //条件查询click事件注册
        $("#btn_query").click(function () {
            $("#tb_roles").bootstrapTable('refresh');
        });

        //设置权限click事件注册
        $("#btn_powerset").click(function () {
            var arrselections = $("#tb_roles").bootstrapTable('getSelections');
            if (arrselections.length > 1) {
                //alert("只能选择一行进行编辑");
                //$("#btn_alert").alert();
                return;
            }
            if (arrselections.length <= 0) {
                //alert("请先选择需要编辑的行");
                //$("#btn_alert").alert()
                return;
            }

            strrole_id = arrselections[0].ROLE_ID;
            $("#myModalLabel_powerset").text("设置" + arrselections[0].ROLE_NAME + "菜单权限");
            $("#tb_powerset").bootstrapTable({
                url: '/PowerManager/GetParentMenu',
                method: 'get',
                detailView: true,//父子表
                //sidePagination: "server",
                pageSize: 10,
                pageList: [10, 25],
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
                }, ],
                onExpandRow: function (index, row, $detail) {
                    var parentid = row.MENU_ID;
                    var cur_table = $detail.html('<table></table>').find('table');
                    arrsubmenutable.push(cur_table);
                    $(cur_table).bootstrapTable({
                        url: '/PowerManager/GetChildrenMenu',
                        method: 'get',
                        queryParams: { strParentID: parentid },
                        ajaxOptions: { strParentID: parentid },
                        clickToSelect: true,
                        pageSize: 10,
                        pageList: [10, 25],
                        columns: [{
                            checkbox: true,
                            formatter: function (value, row, index) {
                                //var obj = null;
                                //return "<input data-index='0' type='checkbox' name='btSelectItem'/>";
                            }
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
                        }, ],
                        onLoadSuccess: function (data) {
                            var obj = data;

                        }
                    });
                }
            });

            $('#myModal_powerset').modal();
        });

        //角色设置功能保存按钮click事件注册
        $("#btn_submit_powerset").click(function () {
            var arr_selected_menu = [];
            for (var i = 0; i < arrsubmenutable.length; i++) {
                //如果对应的子表不存在
                if ($(arrsubmenutable[i]).length <= 0) {
                    continue;
                }
                var arr_curtable_menu = $(arrsubmenutable[i]).bootstrapTable('getSelections');
                for (var j = 0; j < arr_curtable_menu.length; j++) {
                    arr_selected_menu.push(arr_curtable_menu[j]);
                }
               
            }

            $.ajax({
                type: "post",
                url: "/PowerManager/PowerSet",
                data: JSON.stringify({ strRoleID: strrole_id, str_Selected_menu: arr_selected_menu }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, status) {
                    if (status == "success") {
                        //alert("提交数据成功");
                        //$("#tb_roles").bootstrapTable('refresh');
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
    };

    return oInit;
};