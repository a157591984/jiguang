<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuSyManageEdit.aspx.cs" Inherits="Rc.Cloud.Web.Sys.MenuSyManageEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/styles/styles003/layOut.css" rel="stylesheet" type="text/css" />
    <link href="/styles/styles003/style01.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/styles003/sdmenu.css" />
    <script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/jQuery_Dialog.js"></script>
    <script type="text/javascript" src="/scripts/jquery.easydrag.js"></script>
    <script type="text/javascript" src="/scripts/function.js"></script>
    <link href="/Styles/Dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/sdmenu.js"></script>
    <script src="/Scripts/ToolAnalytics.js" type="text/javascript"></script>
</head>
<body>
    <form id="form2" name="form1" runat="server">
    <!------------------------>

    <table style="width: 100%;margin-top:20px;" cellspacing="0" cellpadding="0" class="table_content">
        <tr>
            <td align="right" class="td_content_001">
                模块编码:<font color="red">*</font>
            </td>
            <td class="td_content_002">
                <asp:TextBox ID="txtMODULEID" runat="server" CssClass="txt" IsRequired="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="td_content_001">
                模块名称<font color="red">*</font>
            </td>
            <td class="td_content_002">
                <asp:TextBox ID="txtMODULENAME" runat="server" CssClass="txt" IsRequired="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="td_content_001">
                系统名称<font color="red">*</font>
            </td>
            <td class="td_content_002">
                <asp:DropDownList ID="ddlSysCode" ClientIDMode="Static" runat="server"  IsRequired="true" IsAddEmptyItem="true" EmptyItemType="Choice"
                    CssClass="txt">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="td_content_001">
                父模块编码<font color="red">*</font>
            </td>
            <td class="td_content_002">
                <asp:TextBox ID="txtPARENTID" runat="server" CssClass="txt" IsRequired="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="td_content_001">
                SLEVEL<font color="red">*</font>
            </td>
            <td class="td_content_002">
                <asp:TextBox ID="txtSLEVEL" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="td_content_001">
                链接地址<font color="red">*</font>
            </td>
            <td class="td_content_002">
                <asp:TextBox ID="txtURL" runat="server" CssClass="txt_area" Width="80%"  IsRequired="true" MaxLength="10000"
                    Height="59px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="td_content_001">
                是否显示
            </td>
            <td class="td_content_002">
                <asp:RadioButton ID="rbtISINTREE1" GroupName="ISINTREE" ClientIDMode="Static" value="1"
                    Text="是" runat="server" /><asp:RadioButton ID="rbtISINTREE0" GroupName="ISINTREE"
                        ClientIDMode="Static" value="0" Text="否" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right" class="td_content_001">
                菜单级别<font color="red">*</font>
            </td>
            <td class="td_content_002">
                <asp:TextBox ID="txtDepth" runat="server" CssClass="txt" IsRequired="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="td_content_001">
                是否最后一级菜单
            </td>
            <td class="td_content_002">
                <asp:RadioButton ID="rbtisLast1" value="1" GroupName="isLast" ClientIDMode="Static"
                    Text="是" runat="server" /><asp:RadioButton ID="rbtisLast0" GroupName="isLast" ClientIDMode="Static"
                        value="0" Text="否" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right" class="td_content_001">
                默认排序<font color="red">*</font>
            </td>
            <td class="td_content_002">
                <asp:TextBox ID="txtDefaultOrder" runat="server" CssClass="txt" IsRequired="true"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="div_page_bottom_operation">
        <asp:Button runat="server" CssClass="btn" Text="提 交" ID="btn" OnClick="btn_Search_Click" />
        <input id="Button1" class="btn" type="button" value="取 消" onclick="window.parent.CloseDialog();" />
    </div>
    <!-- 弹出层操作 -->
    <div class="div_ShowDailg" id="div_Pops" runat="server">
        <div class="div_ShowDailg_Title">
            <div class="div_ShowDailg_Title_left" id="div_Pops_Title">
                编辑</div>
            <div class="div_Close_Dailg" title="关闭" onclick="CloseDialog();">
            </div>
            <!--关闭-->
        </div>
        <div class="clearDiv" id="div_iframe" runat="server">
        </div>
        <div>
        </div>
    </div>
    <!-- 背景层DIV -->
    <div class="div_documentbg" id="div_documentbg">
    </div>
    </form>
    <script type="text/javascript">
        //显示成功信息
        function ShowSuccess(url) {
            showTips('保存成功', url, '2');
        }
        //显示错误信息
        function ShowError(msg) {
            showTipsErr('保存失败：' + msg, '4');
        }       
    </script>
</body>
</html>
