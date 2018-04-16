<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="visit_client_detail.aspx.cs" Inherits="Rc.Cloud.Web.Principal.visit_client_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Rc.Common.ConfigHelper.GetConfigString("WebSiteName") %></title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../js/function.js"></script>
    <script src="../js/jq.pagination.js"></script>
    <script src="../js/jquery-jtemplates.js"></script>
    <script src="../js/json2.js"></script>
</head>
<body class="body_bg">
    <form id="form1" runat="server">
        <div class="container ph">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <td>NO.</td>
                        <td>老师</td>
                        <td>资源类型</td>
                        <td>资源名称</td>
                        <td>时间</td>
                    </tr>
                </thead>
                <tbody id="list">
                </tbody>
            </table>
            <div class="pagination_container">
                <ul class="pagination"></ul>
            </div>
        </div>
        <textarea id="listBox" style="display: none">
        {#foreach $T.list as record}
        <tr>
            <td>{$T.record.num}</td>
            <td>{$T.record.TeacherName}</td>
            <td>{$T.record.typeName}</td>
            <td>{$T.record.Resource_Name}</td>
            <td>{$T.record.open_time}</td>
        </tr>
        {#/for}
        </textarea>
        <script type="text/javascript">
            $(function () {
                pageIndex = 1;//默认页码
                loadData();//初始化数据

            })
            var loadData = function () {
                var $_objBox = $("#listBox");
                var objBox = "listBox";
                var $_objList = $("#list");
                var $_pagination = $(".pagination_container");
                var dto = {
                    TeacherId: "<%=TeacherId%>",
                    DateType: "<%=DateType%>",
                    DateData: "<%=DateData%>",
                    PageIndex: pageIndex,
                    PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                    x: Math.random()
                };
                $.ajaxWebService("visit_client_detail.aspx/GetList", JSON.stringify(dto), function (data) {
                    var json = $.parseJSON(data.d);
                    if (json.err == "null") {
                        $_objList.setTemplateElement(objBox, null, { filter_data: false });
                        $_objList.processTemplate(json);
                        $_pagination.pagination(json.TotalCount, {
                            current_page: json.PageIndex - 1,
                            callback: pageselectCallback,
                            items_per_page: json.PageSize
                        });
                    }
                    else {
                        $_objList.html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                        $_pagination.find("ul").html("");
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
        </script>
    </form>
</body>
</html>
