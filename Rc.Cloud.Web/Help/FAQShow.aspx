<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FAQShow.aspx.cs" Inherits="Rc.Cloud.Web.Help.FAQShow" %>

<%@ Register Src="~/control/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/control/footer.ascx" TagPrefix="uc1" TagName="footer" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><asp:Literal ID="ltlHtltel" runat="server"></asp:Literal></title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/function.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../Scripts/base64.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnBack").click(function () {
                var b = new Base64();
                var backurl = getUrlVar("backurl");

                if (backurl != null && backurl != undefined && backurl != "") {
                    window.location.href = b.decode(backurl);
                }
                else {
                    window.location.href = "FAQ.aspx";
                }
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:header runat="server" ID="header" />
        <div class="help_cont">
            <div class="container mt">
                <ol class="breadcrumb">
                    <li><a href="FAQ.aspx">帮助中心</a></li>
                    <li><a href="javascript:;" id="btnBack">常见问题</a></li>
                    <li class="active">
                        <asp:Literal ID="ltl_title" runat="server"></asp:Literal></li>
                </ol>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <asp:Literal ID="ltl_titles" runat="server"></asp:Literal>
                    </div>
                    <div class="panel-body">
                        <asp:Literal ID="ltl_content" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
        <uc1:footer runat="server" ID="footer" />
    </form>
</body>
</html>
