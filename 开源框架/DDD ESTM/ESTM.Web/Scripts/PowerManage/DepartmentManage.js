

$(function () {
    $('#tb_departments').bootstrapTable({
        url: '/PowerManager/GetDepartments',
        method: 'post',
        toolbar: '#toolbar',
        pagination: true,
        queryParams: queryParams,
        queryParamsType: "limit",
        //ajaxOptions: { departmentname: "", statu: "" },
        sidePagination: "server",
        pageSize: 5,
        pageList: [5, 25, 50, 100],
        search: true,
        strictSearch: true,
        showColumns: true,
        showRefresh: true,
        minimumCountColumns: 2,
        clickToSelect: true,
        columns: [{
            checkbox: true
        }, {
            field: 'DEPARTMENT_NAME',
            title: '部门名称'
        }, {
            field: 'PARENT_ID',
            title: '上级部门'
        }, {
            field: 'DEPARTMENT_LEVEL',
            title: '部门级别'
        }, {
            field: 'STATUS',
            title: '状态'
        }, ],
        onLoadSuccess: function (data) {
            var odata = data;
        }
    });

    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

});

function queryParams(params) {  //配置参数
    var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
        limit: params.limit,   //页面大小
        offset: params.offset,  //页码
        departmentname: $("#txt_search_departmentname").val(),
        statu: $("#txt_search_statu").val()
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

            postdata.DEPARTMENT_ID = "";
        });

        $("#btn_edit").click(function () {
            var arrselections = $("#tb_departments").bootstrapTable('getSelections');
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
            $("#txt_departmentname").val(arrselections[0].DEPARTMENT_NAME);
            $("#txt_parentdepartment").val(arrselections[0].PARENT_ID);
            $("#txt_departmentlevel").val(arrselections[0].DEPARTMENT_LEVEL);
            $("#txt_statu").val(arrselections[0].STATUS);

            postdata.DEPARTMENT_ID = arrselections[0].DEPARTMENT_ID;
            $('#myModal').modal();
        });

        $("#btn_delete").click(function () {
            var arrselections = $("#tb_departments").bootstrapTable('getSelections');
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
                url: "/PowerManager/DeleteDept",
                data: { strID: arrselections[0].DEPARTMENT_ID },
                success: function (data, status) {
                    if (status == "success") {
                        alert("提交数据成功");
                        $("#tb_departments").bootstrapTable('refresh');
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
            postdata.DEPARTMENT_NAME = $("#txt_departmentname").val();
            postdata.PARENT_ID = $("#txt_parentdepartment").val();
            postdata.DEPARTMENT_LEVEL = $("#txt_departmentlevel").val();
            postdata.STATUS = $("#txt_statu").val();
            $.ajax({
                type: "post",
                url: "/PowerManager/GetDepartmentEdit",
                data: { strPostData: JSON.stringify(postdata) },
                success: function (data, status) {
                    if (status == "success") {
                        alert("提交数据成功");
                        $("#tb_departments").bootstrapTable('refresh');
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
            $("#tb_departments").bootstrapTable('refresh');
        });
    };

    return oInit;
};