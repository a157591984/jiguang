<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysMenuManageSynchronous.aspx.cs" Inherits="Rc.SysManagement.Web.Sys.SysMenuManageSynchronous" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../styles/styles003/layOut.css" rel="stylesheet" type="text/css" />
    <link href="../styles/styles003/style01.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/styles003/sdmenu.css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../scripts/jQuery_Dialog.js"></script>
    <script type="text/javascript" src="../scripts/jquery.easydrag.js"></script>
    <script type="text/javascript" src="../scripts/function.js"></script>
    <link href="../Styles/Dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/sdmenu.js"></script>
    <script src="../Scripts/ToolAnalytics.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" class="table_content">
        <tr>
            <td class="td_content_001" width="20%">
                请选择数据库：
            </td>
            <td class="td_content_002">
                <asp:CheckBoxList ID="dataBase" runat="server" RepeatColumns="1" RepeatLayout="UnorderedList">
                </asp:CheckBoxList>
            </td>
        </tr>
    </table>
    <div class="clearDiv">
    </div>
    <div class="div_page_bottom_operation">
        <%--<asp:Button ID="btnSave" runat="server" CssClass="btn" Text="保 存" OnClick="btnSave_Click" />--%>
                       <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="覆盖更新" OnClick="btnSave_Click" OnClientClick="btnClient_Click()"/>
                       <asp:Button ID="Button1" runat="server" CssClass="btn" Text="增量更新" OnClick="btnAdd_Click"/>
    </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript" language="javascript" >
    function btnClient_Click() {
        alert("1");

        return;
      
     }
</script>
