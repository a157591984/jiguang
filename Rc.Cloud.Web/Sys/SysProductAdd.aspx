<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysProductAdd.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysProductAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register src="../UserControl/XUV_CommonDict.ascx" tagname="XUV_CommonDict" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../styles/styles003/layOut.css" rel="stylesheet" type="text/css" />
    <link href="../styles/styles003/style01.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/styles003/sdmenu.css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../scripts/jQuery_Dialog.js"></script>
    <script type="text/javascript" src="../scripts/jquery.easydrag.js"></script>
    <script type="text/javascript" src="../scripts/function.js"></script>
    <link href="../Styles/Dialog.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/PrintStytle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/sdmenu.js"></script>
    <script src="../Scripts/ToolAnalytics.js" type="text/javascript"></script>
    <script src="../Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/ui/jquery.ui.dialog.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" class="table_content">
        <tr>
            <td class="td_content_001" width="30%">    
                系统编码：
            </td>
            <td class="td_content_002" width="70%">
            <asp:TextBox ID="txtSysCode" runat="server" CssClass="txt" clientidmode="Static" maxlength="5" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" ></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td class="td_content_001" width="30%">    
                系统名称：
            </td>
            <td class="td_content_002" width="70%">
                <asp:TextBox ID="txtSysProductName" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
       <%-- <tr>
            <td class="td_content_001" width="30%">    
                系统图标：
            </td>
            <td class="td_content_002" width="70%">
                <asp:TextBox ID="txtSysIcon" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>--%>
         <tr>
            <td class="td_content_001" width="30%">    
                系统排序：
            </td>
            <td class="td_content_002" width="70%">
                <asp:TextBox ID="txtSysProductSort" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
             <%--<tr>
            <td class="td_content_001" width="30%">
                系统地址：
            </td>
            <td class="td_content_002" width="70%">
              <asp:TextBox ID="txtSysURL" TextMode="MultiLine" Height="50px" Style="width: 95%;"
                    CssClass="txt_area" runat="server" MaxLength="255"></asp:TextBox>

                </td>
        </tr>       --%>
        </table>
        <div class="div_page_bottom_operation">
                    <asp:Button ID="btnSave" runat="server" Text="保存"  CssClass="btn"  onclick="btnSave_Click" OnClientClick="return btnClient_Click()" />
                    <input id="Button1" class="btn" type="button" value="取 消" onclick="window.parent.CloseDialog();" />
        </div>
    </div>
    </form>
</body>
</html>
