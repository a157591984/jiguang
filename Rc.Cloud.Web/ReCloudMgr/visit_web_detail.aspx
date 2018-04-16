<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="visit_web_detail.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.visit_web_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Rc.Common.ConfigHelper.GetConfigString("WebSiteName") %></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script src="../SysLib/js/jq.pagination.js"></script>
    <script src="../SysLib/js/json2.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="panel">
                <div class="panel-body">
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>NO.</th>
                                <th>班级</th>
                                <th>老师</th>
                                <th>作业名称</th>
                                <th>资源类型</th>
                                <th>时间</th>
                                <th>时长</th>
                            </tr>
                        </thead>
                        <tbody id="list">
                        </tbody>
                    </table>
                    <hr />
                    <div class="page"></div>
                </div>
            </div>
        </div>
        <textarea id="listBox" class="hidden">
        {#foreach $T.list as record}
        <tr>
            <td>{$T.record.num}</td>
            <td>{$T.record.ClassName}</td>
            <td>{$T.record.TeacherName}</td>
            <td>{$T.record.HomeWork_Name}</td>
            <td>{$T.record.typeName}</td>
            <td><p>{$T.record.open_time}</p><p>{$T.record.close_time}</p></td>
            <td>{$T.record.VistiDuration_Count}</td>
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
                var $_pagination = $(".page");
                var dto = {
                    TeacherId: "<%=TeacherId%>",
                    ClassId: "<%=ClassId%>",
                    DateType: "<%=DateType%>",
                    DateData: "<%=DateData%>",
                    PageIndex: pageIndex,
                    PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                    x: Math.random()
                };
                $.ajaxWebService("visit_web_detail.aspx/GetList", JSON.stringify(dto), function (data) {
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
                        $_pagination.html("");
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
