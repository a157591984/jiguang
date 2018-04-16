<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SystemLogErrorList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SystemLogErrorList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <!--弹出层类库-->
    <script src="../Scripts/PhhcCommon.js" type="text/javascript"></script>
<div class="div_right_title">
<div class="div_right_title_icon"></div>
    <%=siteMap%>
</div>
<div class="clearDiv"></div>
<div class="div_right_search">
    <table class="table_search_001">
        <tr>
            <td >操作人名称：</td>
            <td><asp:TextBox ID="txtRoleName" runat="server" CssClass="txt_Search"></asp:TextBox></td>
            <td >错误内容：</td>
            <td><asp:TextBox ID="txtErrorContent" runat="server" CssClass="txt_Search"></asp:TextBox></td>
            <td><asp:Button runat="server" CssClass="btn" Text="查 询" id="btn_Search" 
                    onclick="btn_Search_Click" /> </td>
        </tr>
    </table>
</div>
<div class="clearDiv" ></div>
    <div class="div_right_listtitle">
    错误日志列表
</div>
<div class="clearDiv"></div>
<!--主数据-->
<%= selectAllSystemLogErrorModel()%>
<div class="clearDiv"></div>
<!--分页-->
<%= GetPageIndex() %>
  <!-- 弹出层操作 -->
    <div class="div_ShowDailg" id="div_Pop" style="width: 800px; height: 500px; border: #9995px solid;">
        <div class="div_ShowDailg_Title" id="div_Pop_Title">
            <div class="div_ShowDailg_Title_left" id="div_ShowDailg_Title_left">
            </div>
            <div class="div_Close_Dailg" id="div_Close_Dailg" title="关闭" onclick="CloseDialog();">
            </div>
            <!--关闭-->
        </div>
        <div class="clearDiv">
        </div>
        <div id="divIfram" class="divIfram">
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script src="../Scripts/Sys/SystemLogErrorList.js" type="text/javascript"></script>
</asp:Content>
