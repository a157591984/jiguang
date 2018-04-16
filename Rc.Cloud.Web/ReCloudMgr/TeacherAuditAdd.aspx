<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherAuditAdd.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.TeacherAuditAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="../scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../scripts/function.js"></script>
    <script type="text/javascript" src="../scripts/jQuery_Dialog.js"></script>
    <link href="../styles/styles003/layOut.css" rel="stylesheet" type="text/css" />
    <link href="../styles/styles003/style01.css?css=a" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true">
        </asp:ScriptManager>
        <table cellpadding="0" cellspacing="0" class="table_content">

            <tr>
                <td class="td_content_001" width="23%">部门名称 <font color="red">*</font>：
                </td>
                <td class="td_content_002">
                    <asp:TextBox ID="TbStepName" runat="server" CssClass="txt" IsRequired="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_content_001" width="20%">负责人<font color="red">*</font>：
                </td>
                <td class="td_content_002">
                    <asp:DropDownList ID="DropDownListUser" runat="server" Width="100px" CssClass="txt" IsAddEmptyItem="true" EmptyItemType="Choice" IsRequired="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td_content_001" width="20%">联系电话<font color="red">*</font>：
                </td>
                <td class="td_content_002">
                    <asp:TextBox ID="TbTel" runat="server" CssClass="txt" IsRequired="true"></asp:TextBox>
                </td>
            </tr>
            <%--<tr>
            <td class="td_content_001" width="20%">
                <font color="red">*</font>上级部门：
            </td>
            <td class="td_content_002">
                 <asp:DropDownList ID="DropDownListParentID" runat="server" Height="22px" 
                     Width="128px">
                 </asp:DropDownList>
            </td>
        </tr>--%>
            <tr>
                <td class="td_content_001" width="20%">部门状态<font color="red">*</font>：
                </td>
                <td class="td_content_002">
                    <asp:DropDownList ID="DropDownListState" runat="server" IsRequired="true" Width="100px" IsAddEmptyItem="true" EmptyItemType="Choice">
                        <asp:ListItem Text="有效" Value="1"></asp:ListItem>
                        <asp:ListItem Text="无效" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td_content_001" width="20%">备注：
                </td>
                <td class="td_content_002">
                    <asp:TextBox ID="TbRemark" runat="server" Height="70px" CssClass="txt_area"></asp:TextBox></td>
            </tr>
        </table>


        <div class="div_page_bottom_operation">
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="保 存" OnClick="btnSave_Click" />
        </div>
    </form>
</body>
</html>
