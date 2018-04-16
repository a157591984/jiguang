<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemLogErrorView.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SystemLogErrorAdd" %>

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
    
     <table cellpadding="0" cellspacing="0" class="table_content" >
        <tr>
            <td class="td_content_001" width="20%">    
                IP地址：
            </td>
           <td class="td_content_002" width="80%">
               <asp:Literal ID="txtaddress" runat="server"></asp:Literal>
                </td>
        </tr>
         <tr>
            <td class="td_content_001" width="20%">    
                操作页面路径：
            </td>
            <td class="td_content_002" width="80%">
                 <asp:Literal ID="txtpagepath" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="td_content_001" width="20%">    
                操作系统路径：
            </td>
            <td class="td_content_002" width="80%">
                 <asp:Literal ID="txtsyspath" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="td_content_001" width="20%">    
                错误内容：
            </td>
            <td class="td_content_002" width="80%">
                <asp:Literal ID="txtcontent" runat="server"></asp:Literal>
                </td>
        </tr>
         <tr>
            <td class="td_content_001" width="20%">    
                操作人：
            </td>
            <td class="td_content_002" width="80%">
                 <asp:Literal ID="txtperson" runat="server"></asp:Literal>
            </td>
        </tr>
             <tr>
            <td class="td_content_001" width="20%">
                操作时间：
            </td>
            <td class="td_content_002" width="80%">
              <asp:Literal ID="txttime" runat="server"></asp:Literal>
                </td>
        </tr>
    </table>
    <div class="clearDiv"></div>
    <div class="div_page_bottom_operation">
                <input id="Button1" class="btn" type="button" value="关 闭" onclick="window.parent.CloseDialog();" /> 
    </div>

    </div>
    </form>
</body>
</html>
