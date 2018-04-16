<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSchoolSMS_Person.aspx.cs" Inherits="Rc.Cloud.Web.Sys.AddSchoolSMS_Person" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/auto-complete/css/style.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/auto-complete/js/AutoComplete.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script src="../SysLib/js/jq.pagination.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            index = parent.layer.getFrameIndex(window.name);
            _School_Id = "<%=School_Id%>";
            loadData();
            $("#btnAdd").click(function () {

                layer.ready(function () {
                    layer.open({
                        type: 2,
                        title: '新增',
                        fix: false,
                        area: ["450px", "450px"],
                        content: "Person_Edit.aspx?School_Id=" + _School_Id
                    })
                });
            })

        });
        var loadData = function () {
            var dto = {
                School_Id: _School_Id,
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 3 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("AddSchoolSMS_Person.aspx/GetDataList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbPerson").setTemplateElement("template_Person", null, { filter_data: false });
                    $("#tbPerson").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbPerson").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $(".page").html("");
                }
                if (json.list == null || json.list == "") {
                    pageIndex--;
                    if (pageIndex > 0) {
                        loadData();
                    }
                    else {
                        pageIndex = 1;
                    }
                }
            }, function () { });
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        //编辑
        function UpdateData(SchoolSMS_Person_Id) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '修改',
                    fix: false,
                    area: ["450px", "450px"],
                    content: "Person_Edit.aspx?SchoolSMS_Person_Id=" + SchoolSMS_Person_Id
                })
            });
        }

        //删除
        function DeleteData(SchoolSMS_Person_Id) {
            layer.ready(function () {
                layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
                    $.ajaxWebService("AddSchoolSMS_Person.aspx/DeleteData", "{SchoolSMS_Person_Id:'" + SchoolSMS_Person_Id + "',x:" + Math.random() + "}", function (data) {
                        if (data.d == "1") {
                            layer.msg("删除成功", { icon: 1, time: 2000 }, function () { loadData(); });
                        }
                        else {
                            layer.msg("删除失败", { icon: 2, time: 2000 });
                        }
                    }, function () { })
                });
            })
        }
    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="panel">
                <div class="panel-heading">
                    <div class="panel-title">
                        <input type="button" id="btnAdd" class="btn btn-primary" value="新增收件人" />
                    </div>
                </div>
                <div class="panel-body">
                    <table class="table table-hover table-bordered">
                        <thead>
                            <tr class="tr_title">
                                <th>姓名</th>
                                <th>手机号</th>
                                <th>职务</th>
                                <th>公司名称</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody id="tbPerson"></tbody>
                    </table>
                    <textarea id="template_Person" class="hidden">
                        {#foreach $T.list as record}
                            <tr>
                                <td>{$T.record.Name}</td>
                                <td>{$T.record.PhoneNum}</td>
                                <td>{$T.record.Job}</td>
                                <td>{$T.record.Company}</td>
                                <td class="opera">      
                                    <a href="javascript:;" onclick="UpdateData('{$T.record.SchoolSMS_Person_Id}')">修改</a>
                                    <a href="javascript:;" onclick="DeleteData('{$T.record.SchoolSMS_Person_Id}')">删除</a>
                                </td>
                            </tr>
                        {#/for}
                    </textarea>
                    <hr />
                    <div class="page"></div>
                </div>
            </div>
        </div>
        <!--智能匹配载体-->
        <div id="AutoCompleteVectors" class="AutoCompleteVectors">
            <div id="topAutoComplete" class="topAutoComplete">
                简拼/汉字或↑↓
            </div>
            <div id="divAutoComplete" class="divAutoComplete">
                <ul id="AutoCompleteDataList" class="AutoCompleteDataList">
                </ul>
            </div>
        </div>
    </form>
</body>
</html>
