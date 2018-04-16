<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileSyncViewList.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FileSyncViewList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />

    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/bootstrap-3.3.7/js/bootstrap.min.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script src="../SysLib/js/jq.pagination.js"></script>
    <script src="../SysLib/js/json2.js"></script>
    <script src="../SysLib/js/base64.js"></script>
    <script src="../SysLib/js/index.js"></script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="container-fluid pv">
            <div class="form-inline search_bar mb">
                <asp:DropDownList ID="ddlGradeTerm" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlSubject" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlResource_Version" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlResource_Type" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
                <asp:TextBox ID="txtKey" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
            </div>
            <table class="table table-hover table-bordered">
                <thead>
                    <tr>
                        <th>年级学期</th>
                        <th>学科</th>
                        <th>教材版本</th>
                        <th>文档类型</th>
                        <th>名称</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody id="tbRes">
                </tbody>
            </table>
            <div class="page"></div>
        </div>

        <textarea id="template_Res" class="hidden">
        {#foreach $T.list as record}
        <tr>
            <td>{$T.record.GradeTerm}</td>
            <td>{$T.record.Subject}</td>
            <td>{$T.record.Resource_Version}</td>
            <td>{$T.record.Resource_Type}</td>
            <td>{$T.record.docName}</td>
            <td class="opera">
                <a title=""  href="FileSyncView.aspx?resourceFolderId={$T.record.docId}&resTitle={$T.record.GradeTerm}--{$T.record.Subject}--{$T.record.Resource_Version}--{$T.record.Resource_Type}--{$T.record.docName}&FileSyncExecRecord_Type=<%=FileSyncExecRecord_Type %>" >资源列表</a>
            </td>
        </tr>
        {#/for}
        </textarea>
        <script type="text/javascript">

            var loadData = function () {
                var strResource_Type = $("#ddlResource_Type").val();
                var strResource_Class = '<%=strResource_Class%>';

                var strGradeTerm = $("#ddlGradeTerm").val();
                var strSubject = $("#ddlSubject").val();
                var strResource_Version = $("#ddlResource_Version").val();
                var dto = {
                    FileSyncExecRecord_Type: "<%=FileSyncExecRecord_Type%>",
                    DocName: $.trim($("#txtKey").val()),
                    strResource_Type: strResource_Type,
                    strResource_Class: strResource_Class,
                    strGradeTerm: strGradeTerm,
                    strSubject: strSubject,
                    strResource_Version: strResource_Version,
                    PageIndex: pageIndex,
                    PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                    x: Math.random()
                };
                $.ajaxWebService("FileSyncViewList.aspx/GetDataList", JSON.stringify(dto), function (data) {
                    var json = $.parseJSON(data.d);
                    if (json.err == "null") {
                        $("#tbRes").setTemplateElement("template_Res", null, { filter_data: false });
                        $("#tbRes").processTemplate(json);
                        $(".page").pagination(json.TotalCount, {
                            current_page: json.PageIndex - 1,
                            callback: pageselectCallback,
                            items_per_page: json.PageSize
                        });
                    }
                    else {
                        $("#tbRes").html("<tr class='tr_con_002'><td colspan='100' align='center'>暂无数据</td></tr>");
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

            $(function () {
                pageIndex = 1;

                loadData();

                $("#btnSearch").click(function () {
                    pageIndex = 1;
                    loadData();
                });
                $("#ddlGradeTerm").change(function () {
                    pageIndex = 1;
                    loadData();
                });
                $("#ddlResource_Version").change(function () {
                    pageIndex = 1;
                    loadData();
                });
                $("#ddlResource_Type").change(function () {
                    pageIndex = 1;
                    loadData();
                });
                $("#ddlSubject").change(function () {
                    pageIndex = 1;
                    loadData();
                });

            });

        </script>
    </form>
</body>
</html>
