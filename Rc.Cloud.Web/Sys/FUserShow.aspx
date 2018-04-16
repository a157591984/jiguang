<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FUserShow.aspx.cs" Inherits="Rc.Cloud.Web.Sys.FUserShow1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/plugin/datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../SysLib/plugin/datetimepicker/js/locales/bootstrap-datetimepicker.zh-CN.js"></script>
    <script src="../SysLib/js/base64.js"></script>
    <script src="../SysLib/js/jq.pagination.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            loadData();
        })


        function loadData() {
            var dto = {
                UserId: "<%=UserId%>",
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            }
            $.ajaxWebService("FUserShow.aspx/GetFUserList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#list").setTemplateElement("listBox", null, { filter_data: false });
                    $("#list").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#list").html("<tr class='tr_con_002'><td colspan='100' align='center'>暂无数据</td></tr>");
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
            }, function () { })
        }
        function pageselectCallback(page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel">
            <div class="panel-body">
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>班级ID</th>
                            <th>班级名称</th>
                            <th>年级ID</th>
                            <th>年级名称</th>
                            <th>学校ID</th>
                            <th>学校名称</th>
                        </tr>
                    </thead>
                    <tbody id="list">
                    </tbody>
                </table>
                <textarea id="listBox" class="hidden">
                    {#foreach $T.list as record}
                    <tr>
                        <td>{$T.record.ClassId}</td>
                        <td>{$T.record.ClassName}</td>
                        <td>{$T.record.GradeId}</td>
                        <td>{$T.record.GradeName}</td>
                        <td>{$T.record.SchoolId}</td>
                        <td>{$T.record.SchoolName}</td>
                    </tr>
                    {#/for}
                </textarea>
                <hr />
                <div class="page"></div>
            </div>
        </div>
    </form>
</body>
</html>
