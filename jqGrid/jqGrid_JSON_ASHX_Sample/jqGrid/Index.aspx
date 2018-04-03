<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="jqGrid.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>JQGrid的DEMO</title>
    <link href="/Content/smoothness/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="/Content/ui.jqgrid.css" rel="stylesheet" type="text/css" />

    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/date-time/daterangepicker.css" rel="stylesheet" type="text/css" />

    <link href="bootstrap/css/file_input/fileinput.min.css" rel="stylesheet" type="text/css" />

    <script src="/Scripts/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="/Scripts/i18n/grid.locale-en.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.jqGrid.min.js" type="text/javascript"></script>

    <script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="bootstrap/js/date-time/bootstrap-datepicker.js" type="text/javascript"></script>   
    <script src="bootstrap/js/date-time/moment.js" type="text/javascript"></script>
    <script src="bootstrap/js/date-time/daterangepicker.js" type="text/javascript"></script>

    <link href="bootstrap/js/file_input/fileinput.min.js" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function Query() {
            var m = new QueryConfig();
            m.ID = $('#txtQuery').val();
            //传参，方法二：OK
            $("#grid1").jqGrid('setGridParam', {
                postData: { 'pars': JSON.stringify(m) }
            }).trigger('reloadGrid');
        }
   
        jQuery(function () {
            //学习Bootstrap 日期控件
            $('.input-daterange').datepicker({ autoclose: true });

            //var m = new QueryConfig();
            //m.ID = 11;
            //var m = { '_search': true, 'nd': '1466042760511', 'rows': '10', 'page': '1', 'sidx': 'ID', 'sord': 'desc' };

            //传参，方法一： url: 'Handler1.ashx?aa=' + JSON.stringify(m), 这种每次查询，值都固定，用控件的值也是这样，所以不可取
            $("#list #grid1").jqGrid({
                url: 'Handler1.ashx',
                mtype: "POST",
                datatype: "json",
                shrinkToFit: false,
                colNames: ['ID', 'UserName','CreateTime'],
                colModel: [
                            { name: 'ID', index: 'ID', editable: true, width: 100},
                            {
                                name: 'UserName', index: 'UserName', width: 100, align: 'left',
                                formatter:function(cellvalue, options, rowObject){
                                    var temp = '<span style="color:';
                                    if (cellvalue == 'Wujy') {
                                        temp = temp + 'blue';
                                    }
                                    temp = temp +';">'+ cellvalue + '</span>';
                                    return temp;
                                }
                            },
                            {
                                name: 'CreateTime', index: 'CreateTime', width: 120, align: 'center',
                                formatter: "date",formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d H:i:s' }
                            }
                ],

                onCellSelect: function (rowid, iCol, cellcontent, e) {
                    if (iCol == 2)
                    {
                        var data = $("#"+rowid +">td"); //获取这个行里所有的td元素，即：获取所有子元素
                        var aa = data[0].innerHTML;
                        var bb = data[1].innerHTML;
                        var cc = data[2].innerHTML;
                        //window.returnValue  = '123';
                        //self.close();

                        //这里写弹出新页面
                        //window.open("Detail.aspx?id=" + rowid, "newwindow", "height=1000, width=500, toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no");
                        //window.showModalDialog("addSave.asp?" + psAddStr, '', "dialogHeight:250px;dialogWidth:250px;status:no;");
                        window.showModalDialog("Detail.aspx?id=" + rowid, "", "dialogWidth:500px; dialogHeight:400px;status:no;scroll:no;");
                    }
                },
                edit: true,
                autowidth: false,
                multiselect: false,//是否多选
                width: 900,
                height:300,
                rowNum: 5,
                rowList: [5,10],
                pager: '#pager1',
                sortname: 'ID',
                viewrecords: true,
                sortorder: 'desc',
                caption: "客户列表",
                jsonReader: {
                    repeatitems: false,
                    root: function (obj) { return obj.rows; },
                    page: function (obj) { return obj.page; },
                    total: function (obj) { return obj.total; },
                    records: function (obj) { return obj.records; }
                }
            }).navGrid("#pager1", { edit: true, add: false, del: false, search: false });
        });

        //定义查询条件对象
        var QueryConfig = function () {
            this.ID = null;
        }

        function TestAjax() {
            var m = { AA:"123"};
            //JSON.stringify(m)
            $.ajax({
                url: 'Handler2.ashx?aa=123',
                data: {'aa':'456'},
                type: 'POST',
                contentType: 'application/json; charset=utf8',
                cache: false,
                dataType: 'text',
                success: function (data) {
                    if (data != null) {
                        var deplist = $.parseJSON(data);
                        //dataType: 'json',就不用 parseJSON
                        var str = "";
                        for (var i = 0; i < deplist.length; i++) {
                            str += "<option value=" + deplist[i].Number + ">" + deplist[i].Name + "</option>";
                        }
                        queryDom.html(str);
                    }
                },
                error: function (xhr) {
                    alert("出现错误，请稍后再试:" + xhr.responseText);
                }
            });
        }
    </script>
</head>
<body>
    <div id="list">
        <table id="grid1">
        </table>
        <div id="pager1">
        </div>
    </div>
    <div>
        <input type="button" value="查询(测试Grid)" onclick="Query()" />
        <input id="txtQuery" type="text" value="1" />
    </div>
    <br/>
    <br/>
    <%--各种Bootstrap元素--%>
    <div>
        <button type="button" class="btn btn-primary" onclick="TestAjax()">原始按钮</button>
        <input type="text" class="form-control" id="name" style="width:200px;"
            placeholder="请输入名称"/>

        <label>Range Picker</label>
	    <div class="row">
		    <div class="col-xs-8 col-sm-11">
			    <div class="input-daterange input-group">
				    <input type="text" class="input-sm form-control" name="start" />
				    <span class="input-group-addon">
					    -
				    </span>

				    <input type="text" class="input-sm form-control" name="end" />
			    </div>
		    </div>
	    </div>

        <form class="form-inline">
            <span>123</span>
            <select class="form-control" id="form-field-select-1" style="width:200px;">
                <option value=""></option>
                <option value="AL">Alabama</option>
            </select>
        </form>

        <table>
            <tr>
                <td>
                    <label>456</label>
                    <span class="colonStyle">:</span>
                </td>
                <td>
                     <input type="text" class="form-control" id="txtStoreDataSend" style="width:77px;"
                        placeholder="gaga"/>
                </td>
            </tr>   
        </table>

        <div>
            <label class="control-label">Select File</label>
<input id="input-2" name="input2[]" type="file" class="file" multiple data-show-upload="false" data-show-caption="true">
        </div>
    </div>
</body>
</html>