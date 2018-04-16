<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigSchool_BookAttr.aspx.cs" Inherits="Rc.Cloud.Web.Sys.ConfigSchool_BookAttr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>学校自有资源权限控制</title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/auto-complete/css/style.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script type="text/javascript">
        $(function () {

        });
        function OperateReAuth(attrEnum, obj) {
            var dto = {
                SysUserId: "<%=SysUserId%>",
                SchoolId: "<%=SchoolId%>",
                AttrEnum: attrEnum,
                AttrValue: $(obj).val(),
                x: Math.random()
            }
            $.ajaxWebService("ConfigSchool_BookAttr.aspx/OperateResourceAuth", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    layer.ready(function () {
                        layer.msg('操作成功', { icon: 1, time: 1000 })
                    });
                }
            }, function () { }, false);
        }
    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">

            <div class="form-group">
                <label></label>
                <asp:DropDownList runat="server" ID="ddlPrint" CssClass="form-control" onchange="OperateReAuth('Print',this);">
                    <asp:ListItem Value="0">禁止打印</asp:ListItem>
                    <asp:ListItem Value="1" Selected>允许打印</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:DropDownList runat="server" ID="ddlSave" CssClass="form-control" onchange="OperateReAuth('Save',this);">
                    <asp:ListItem Value="0">禁止存盘</asp:ListItem>
                    <asp:ListItem Value="1" Selected>允许存盘</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:DropDownList runat="server" ID="ddlCopy" CssClass="form-control" onchange="OperateReAuth('Copy',this);">
                    <asp:ListItem Value="0">禁止复制</asp:ListItem>
                    <asp:ListItem Value="1" Selected>允许复制</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

    </form>
</body>
</html>
