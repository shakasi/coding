
$(function () {
    var oPostData = {};
    RefreshGrid(oPostData);
});


//刷新表格数据
function RefreshGrid(PostData) {
    if (!$("#table_FirstPassInfo").getGridParam("url")) {
        GetFirstPassInfoList(PostData); //此处的data是开头定义的，值都为空
        return;
    }
    $("#table_FirstPassInfo").setGridParam({ page: 1 })
    $("#table_FirstPassInfo").appendPostData(PostData); //此处的data是传入的搜索条件
    $("#table_FirstPassInfo").trigger("reloadGrid");
}

function GetFirstPassInfoList(PostData) {
    var tableName = "#table_FirstPassInfo";
    var tablePager = "#div_FirstPassInfoPager";
    $(tableName).jqGrid({
        url: '/Home/GetFirstPassReportList',
        datatype: 'json',
        mtype: 'POST',
        colModel: [{ label: "用户名", name: 'USER_NAME', index: 'USER_NAME', align: 'center', sortable: false, search: false, width: 100 },
                   { label: "部门", name: 'DEPARTMENT_NAME', index: 'DEPARTMENT_NAME', align: 'center', sortable: false, search: false, width: 100 },
                   { label: "全称", name: 'FULLNAME', index: 'FULLNAME', align: 'center', sortable: false, search: false, width: 100 },
                   { label: "用户角色", name: 'ROLE_NAME', index: 'ROLE_NAME', align: 'center', sortable: false, search: false, width: 200 },
                   { label: "创建日期", name: 'CREATETIME', index: 'CREATETIME', align: 'center', sortable: false, search: false, width: 100 },
                   { label: "更新日期", name: 'MODIFYTIME', index: 'MODIFYTIME', align: 'center', sortable: false, search: false, width: 100 },
                   { label: "备注", name: 'REMARK', index: 'REMARK', align: 'center', sortable: false, search: false, width: 100 },
                   { label: "操作", name: 'REMARK', index: 'REMARK', align: 'center', sortable: false, search: false, width: 100 },
                   { label: "用户ID", name: 'USER_ID', index: 'USER_ID', hidden: true }        ],
        pager: $(tablePager),
        postData: PostData,
        autowidth: true,
        shrinkToFit: false,
        scrollrows: false, // 是否显示行滚动条
        forceFit: false,
        //width: '1310',
        height: '570',
        rowNum: 20,
        rowList: [20, 50, 80],
        altRows: true,
        formEdit: true,
        scroll: false,
        caption: "用户列表",
        modal: false,
        drag: true,
        sortname: 'USER_NAME',
        sortorder: 'desc',
        viewrecords: true,
        multiselect: false,
        beforeRequest: function () { },
        loadComplete: function (jsondata) {
            //alert(jsondata);
        },
        loadError: function (xhr, status, error) {
           
        },
        ondblClickRow: function (rowid) {
            
        },
        onSortCol: function (index, iCol, sortorder) {
            
        }
    }).navGrid(tablePager, { edit: false, add: false, del: false, search: false });
}