<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="./../js/js001/common.js?t=2015-08-11" type="text/javascript"></script>
    <link href="../Styles/style001/Style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_right_title">
        <div class="div_right_title_icon"></div>
        <%=siteMap%>
    </div>
    <div class="clearDiv">
    </div>
    <div class="div_right_search">
        <table class="table_search_001">
            <tr>
                <td class="td_search_001">系统名称：
                </td>
                <td class="td_search_002">
                    <asp:DropDownList ID="ddlSysCode" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="td_search_001">文件名称：
                </td>
                <td class="td_search_002">
                    <asp:TextBox ID="txtResource_Name" runat="server" CssClass="txt" Width="130px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button runat="server" CssClass="btn" Text="上传" ID="btn_Search" />
                </td>
                <td>
                    <asp:Button runat="server" CssClass="btn" Text="创建目录" ID="Button1" />
                </td>
            </tr>
        </table>
    </div>
    <div class="main" style="border-top: 1px solid #E6EBF1;">
        <!--树形菜单-->
        <asp:Literal ID="litTree" ClientIDMode="Static" runat="server"></asp:Literal>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>
