<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateDataItemEdit.aspx.cs" Inherits="Rc.Cloud.Web.Sys.UpdateDataItemEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <script type="text/javascript" src="../scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../scripts/function.js"></script>
    <script type="text/javascript" src="../scripts/jQuery_Dialog.js"></script>

    <link href="../styles/styles003/layOut.css" rel="stylesheet" type="text/css" />
    <link href="../styles/styles003/style01.css?css=a" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../scripts/jquery.easydrag.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" class="table_content">
            <tr>
                <td class="td_content_001" width="30%" >数据项名称<span style="color: Red;">*</span>：
                </td>
                <td class="td_content_002">
                    <asp:TextBox ID="txtd_Remark" CssClass="txt" runat="server" ClientIDMode="Static" IsRequired="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_content_001" width="30%" >数据表名称<span style="color: Red;">*</span>：
                </td>
                <td class="td_content_002">
                    <asp:TextBox ID="txtd_name" CssClass="txt " runat="server" ClientIDMode="Static" IsRequired="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_content_001" width="30%" >数据表唯一ID<span style="color: Red;">*</span>：
                </td>
                <td class="td_content_002">
                    <asp:TextBox ID="txtD_code" CssClass="txt" runat="server" ClientIDMode="Static" IsRequired="true"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="div_page_bottom_operation">
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="保 存" OnClick="btnSave_Click"
                OnClientClick="CheckInput()" />
        </div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">

    function CheckInput() {
        if ($("#txtName").val() == "") {
            $.dialog.alert("数据项名称不能为空！");
            return false;
        }
        if ($("#HidCutomerInfo_ID").val() == "") {
            $.dialog.alert("数据表名称不能为空！");
            return false;
        }
        if ($("#txtD_code").val() == "") {
            $.dialog.alert("数据表唯一ID不能为空！");
            return false;
        }
        return true;

    }
</script>
