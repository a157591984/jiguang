<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListStructureItem.aspx.cs" Inherits="Rc.Cloud.Web.BaseConfig.ListStructureItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../styles/styles003/layOut.css" rel="stylesheet" type="text/css" />
    <link href="../styles/styles003/style01.css?css=a" rel="stylesheet" type="text/css" /> 
    <script type="text/javascript" src="../scripts/jquery-1.4.1.min.js"></script>    
    <link href="../styles/Dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../scripts/jQuery_Dialog.js"></script>   
    <script type="text/javascript" src="../scripts/jquery.easydrag.js"></script>   
    <script type="text/javascript" src="../scripts/function.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <div  class="div_search_pop">
           <table cellpadding="0" cellspacing="0" class="table_content">
                <tr>
                    <td class="td_content_001" width="30%">表名称<font color="red">*</font>:</td>
                    <td class="td_content_002" width="70%"><asp:TextBox ID="tablename" Enabled="false" runat="server" IsRequired="true" MaxLength="100" CssClass="txt"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="td_content_001" width="30%">表说明<font color="red">*</font>:</td>
                    <td class="td_content_002" width="70%"><asp:TextBox ID="tableinfo"   runat="server" IsRequired="true" MaxLength="100" CssClass="txt"></asp:TextBox></td>
                </tr>               
            </table>
        </div>
        <div class="div_page_bottom_operation">
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="保 存" onclick="btnSave_Click" OnClientClick="btnClientSave_Click()" />
            <input class="btn" type="button" onclick="parent.ClosePopByIdAddUser();" value="关 闭" style="width:60px"/>            
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function btnClientSave_Click() {        
        var tableinfo = $.trim($("#tableinfo").val());
        if (tableinfo == "") {
            layer.ready(function () { layer.msg('请填写表说明', { icon: 4 }); });
            return false;
        }        
    }
</script>
