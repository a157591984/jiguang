<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserControlConfigAdd.aspx.cs" Inherits="Rc.Cloud.Web.BaseConfig.UserControlConfigAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <link href="/styles/styles003/layOut.css" rel="stylesheet" type="text/css" />
    <link href="/styles/styles003/style01.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/styles003/sdmenu.css" />
    <script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/jQuery_Dialog.js"></script>
    <script type="text/javascript" src="/scripts/jquery.easydrag.js"></script>
    <script type="text/javascript" src="/scripts/function.js"></script>
    <link href="/Styles/Dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/sdmenu.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%;margin-top:20px;" cellpadding="0" cellspacing="0" class="table_content">
                <tr>
                    <td class="td_content_001" width="20%">配置标识<font color="red">*</font>：</td>
                    <td class="td_content_002">
                        <asp:TextBox ID="txtConfigName" CssClass="txt" runat="server" Rows="5"  IsRequired="true" MaxLength="100"
                            Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td_content_001" width="20%">配置名称<font color="red">*</font>：</td>
                    <td class="td_content_002">
                        <asp:TextBox ID="txtName" CssClass="txt" runat="server" Rows="5" IsRequired="true"  MaxLength="100"
                            Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td  class="td_content_001" width="20%">配置说明：</td>
                    <td class="td_content_002">
                        <asp:TextBox ID="txtDesc" CssClass="txt_area" runat="server" Rows="5" TextMode="MultiLine"  IsFilterSpecialChars="false" IsFilterSqlChars ="false"
                            MaxLength="10000" Width="85%"></asp:TextBox>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  class="td_content_001" width="20%">SQL语句：</td>
                    <td class="td_content_002">
                        <asp:TextBox ID="txtSqlString" CssClass="txt_area" runat="server" Rows="5" TextMode="MultiLine" IsFilterSpecialChars="false" IsFilterSqlChars ="false"
                          MaxLength="10000"   Width="85%" Height="135"></asp:TextBox>
                        &nbsp;</td>
                </tr>
            </table>
            <div class="div_page_bottom_operation">
                <asp:Button runat="server" CssClass="btn" Text="提 交" ID="btn_Search" OnClick="btnSave_Click" />
                        <input id="Button1" class="btn" type="button" value="关闭" onclick="window.parent.CloseDialog();" />
            </div>

    </form>
</body>
</html>