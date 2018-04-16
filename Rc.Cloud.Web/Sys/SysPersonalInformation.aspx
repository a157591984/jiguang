<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SysPersonalInformation.aspx.cs" Inherits="Rc.Cloud.Web.Sys.PersonalInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="div_right_title"><div class="div_right_title_icon"></div>
    <%=siteMap%>
</div>
 <table cellpadding="0" cellspacing="0" class="table_content">
        <tr>
            <td class="td_content_001" width="20%">
                登录名：
            </td>
            <td class="td_content_002">
                <asp:Label ID="lblNamelogin" runat="server" Text=""></asp:Label>
            </td>
        </tr>
       <%-- <tr>
            <td class="td_content_001" width="20%">
                <font color="red">*</font>密码：
            </td>
            <td class="td_content_002">
                <asp:TextBox ID="txtpwdlogin" TextMode="Password" runat="server" MaxLength="6" CssClass="txt"></asp:TextBox>
                不填则为原密码<asp:TextBox ID="TextBox2" TextMode="Password" Style="width: 0%; height: 0%;
                    display: none;" runat="server"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <td class="td_content_001" width="20%">
                用户名：
            </td>
            <td class="td_content_002">

               <%-- <asp:TextBox ID="txtSysUserName" runat="server"  Enabled="false" CssClass="txt" ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;--%>
               <asp:Label ID="txtSysUserName" runat="server" Text=""></asp:Label>
               
            </td>
        </tr>
        <tr>
            <td class="td_content_001">
                部门：
            </td>
            <td class="td_content_002">
                <asp:Label ID="lblDepartmentName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <%-- <tr>
            <td class="td_content_001">
                是否可用:
            </td>
            <td class="td_content_002">
                <asp:CheckBox ID="ckbIsEnable" Checked="true" ClientIDMode="Static" runat="server" />
            </td>
        </tr>--%>
        <tr>
            <td class="td_content_001" width="20%">
                手机：
            </td>
             <td class="td_content_002">
                <asp:TextBox ID="txtTel" runat="server"  CssClass="txt" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_content_001" width="20%">
                备注：
            </td>
            <td class="td_content_002">
                <asp:TextBox ID="txtContent" TextMode="MultiLine" Height="50px" Style="width: 95%;"
                    CssClass="txt_area" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="div_page_bottom_operation">
        <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="保 存"   OnClick="btnSave_Click" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>
