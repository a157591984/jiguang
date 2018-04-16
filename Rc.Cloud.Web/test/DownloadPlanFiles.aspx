<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownloadPlanFiles.aspx.cs" Inherits="Rc.Cloud.Web.test.DownloadPlanFiles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../js/jquery.min-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../plugin/layer/layer.js"></script>
    <script type="text/javascript" src="../../js/function.js"></script>
    <script type="text/javascript" src="../../js/json2.js"></script>
    <script type="text/javascript" src="../../js/jq.pagination.js"></script>
    <script type="text/javascript" src="../../js/jquery-jtemplates.js"></script>
    <script type="text/javascript" src="../../js/base64.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-inline mb">
            &nbsp;&nbsp;<asp:TextBox runat="server" ID="txtFolder" placeholder="存储目录">Resource5</asp:TextBox>
            <asp:DropDownList ID="ddlYear" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlGradeTerm" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlResource_Version" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlSubject" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
            </asp:DropDownList>
            <asp:Button ID="btnDown" runat="server" Text="下载" CssClass="btn btn-primary btn-sm" OnClick="btnDown_Click" />
        </div>
    </form>
</body>
</html>
