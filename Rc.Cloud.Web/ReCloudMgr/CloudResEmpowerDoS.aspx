<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CloudResEmpowerDoS.aspx.cs" Inherits="Rc.Cloud.Web.Sys.CloudResEmpowerDoS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/auto-complete/css/style.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/plugin/auto-complete/js/AutoComplete.js"></script>
    <script src="../SysLib/js/function.js"></script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-inline search_bar mb">
                <asp:Button ID="btnBePower" Text="批量授权" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnBePower_Click" OnClientClick="return SelectAllChk()" />
                <asp:Button ID="Button1" Text="批量删除授权" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnDeleteBePower_Click" OnClientClick="return SelectAllChk()" />
                <asp:TextBox runat="server" ID="txt_SName" CssClass="form-control input-sm" placeholder="学生姓名"></asp:TextBox>
                <input type="text" id="txtTeacherNameS" clientidmode="Static" class="form-control input-sm" runat="server" placeholder="老师姓名" />
                <asp:TextBox runat="server" ID="txt_BanJI" CssClass="form-control input-sm" placeholder="班号"></asp:TextBox>
                <asp:DropDownList ID="ddlPower" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="已授权" Value="1"></asp:ListItem>
                    <asp:ListItem Text="未授权" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnSearch" Text="查询" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnSearch_Click" data-name="submit" />
            </div>
            <asp:Literal ID="litContent" ClientIDMode="Static" runat="server"></asp:Literal>
            <%= GetPageIndex() %>
            <input type="hidden" id="hidUserIDs" runat="server" />
        </div>
    </form>
</body>
</html>
<script>
    $(function () {
        $(".table input[name='checkAll']").click(function () {
            $(this).closest('thead').next('tbody').find("input").prop("checked", $(this).is(":checked"));
        });
    });
    function SelectAllChk() {
        var result = false;
        var _IDs = "";
        $(".table tbody input[name!='checkAll']").each(function () {
            if ($(this).is(":checked")) {
                _IDs += $(this).val() + ",";
            }
        });
        if (_IDs != "") {
            $("#hidUserIDs").val(_IDs);
            result = true;
        }
        else {
            layer.ready(function () {
                layer.msg('没有选择任何数据', { icon: 4 });
            })
        }
        return result;
    }
</script>
