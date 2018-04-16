<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="Rc.Cloud.Web.Help.FAQ" %>

<%@ Register Src="~/control/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/control/footer.ascx" TagPrefix="uc1" TagName="footer" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Rc.Common.ConfigHelper.GetConfigString("WebSiteName") %>-常见问题</title>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/common.js"></script>
    <script src="../js/function.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script type="text/javascript" src="../js/jq.pagination.js"></script>
    <script type="text/javascript" src="../js/jquery-jtemplates.js"></script>
    <script src="../Scripts/base64.js"></script>
    <script type="text/javascript">
        $(function () {
            loadData();
            $("#btnSearch").click(function () {
                loadData();
            })
        })
        var loadData = function () {
            var dto = {
                key: $("#txt_key").val(),
                x: Math.random()
            };
            $.ajaxWebService("FAQ.aspx/GetHelpList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#accordion").setTemplateElement("template_list", null, { filter_data: false });
                    $("#accordion").processTemplate(json);
                }
                else {
                    $("#accordion").html("<div class='pa text-center'>暂无信息</div>");
                }
            }, function () { });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="index_panel">
            <uc1:header runat="server" ID="header" />

            <div class="help_panel">
                <div class="mask"></div>
                <div class="container">
                    <div class="panel_heading">帮助中心</div>
                    <div class="panel_search">
                        <asp:TextBox ID="txt_key" runat="server" CssClass="form-control" placeholder="搜索帮助" ClientIDMode="Static"></asp:TextBox>
                        <button type="button" class="btn" id="btnSearch"><i class="material-icons">&#xE8B6;</i></button>
                    </div>
                    <div class="panel_body">
                        <div class="panel-group" id="accordion"></div>
                    </div>
                </div>
            </div>

            <uc1:footer runat="server" ID="footer" />
        </div>
    </form>
    <textarea id="template_list" class="hidden">
    {#foreach $T.list as record}
        <div class="panel">
            <div class="panel-heading" id="heading{$T.record.help_id}" data-toggle="collapse" data-target="#collapse{$T.record.help_id}">
                <div class="panel-control">
                    <span class="btn"><i class="material-icons"></i></span>
                </div>
                <div class="panel-title">
                    {$T.record.help_title}
                </div>
            </div>
            <div id="collapse{$T.record.help_id}" class="panel-collapse collapse">
                <div class="panel-body">
                    {$T.record.help_content}
                </div>
            </div>
        </div>
    {#/for}
    </textarea>
</body>
</html>
