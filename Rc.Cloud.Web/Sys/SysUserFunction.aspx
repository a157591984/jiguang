<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true"
    CodeBehind="SysUserFunction.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysUserFunction" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_right_title">
        <div class="div_right_title_icon">
        </div>
        <%=siteMap%>
    </div>
    <div class="clearDiv">
    </div>
    <div class="div_right_search">
        <table class="table_search_001">
            <tr>
                <td>
                    用户名称：
                </td>
                <td>
                    <asp:TextBox ID="txtDoctorInfoName"  runat="server" CssClass="txt_Search"></asp:TextBox>
                </td>
                <td>
                    <asp:Button runat="server" CssClass="btn" Text="查 询" ID="btn_Search" OnClick="btn_Search_Click" />
                </td>
                <td>
                    <asp:Button runat="server" CssClass="btn" Text="清除缓存" ID="btnClearCatch" OnClick="btnClearCatch_Click" Visible="false" />
                </td>
            <%--    <td>
                    <input id="btnAddUser" type="button" class="btn" value="新增用户" onclick="showPopAddUser('')" />
                </td>--%>
            </tr>
        </table>
    </div>
    <div class="clearDiv">
    </div>
    <div class="div_right_listtitle">
        用户列表
    </div>
    <div class="clearDiv">
    </div>
    <!--主数据-->
    <%= GetHtmlData() %>
    <div class="clearDiv">
    </div>
    <!--分页-->
    <%= GetPageIndex() %>
    <!-- 弹出层操作-->
    <div class="div_ShowDailg" id="div_Pop" runat="server" style="width: 800px; height: 650px;">
        <div class="div_ShowDailg_Title" id="div_Pop_Title" runat="server">
            <div class="div_ShowDailg_Title_left" id="div_ShowDailg_Title_left">
            </div>
            <div class="div_Close_Dailg" id="div_Close_Dailg" title="关闭" onclick="ClosePopByIdAddUser();">
            </div>
            <!--关闭-->
        </div>
        <div class="clearDiv" id="div_iframe" runat="server">
        </div>
    </div>
    <script type="text/javascript" >
        function ClosePopByIdAddUser() {
            CloseDialogById('<%=div_Pop.ClientID %>');
        }

        function showPopAddUser(id) {
            $("#<%=div_Pop.ClientID %>").show();
            if (id == "") {
                $("#div_ShowDailg_Title_left").html("新增用户");
            }
            else {
                $("#div_ShowDailg_Title_left").html("修改用户");
            }
            $("#<%=div_iframe.ClientID %>").html("<iframe  height=\"620\"  frameborder='0' width='100%' style='margin: 0px' src='DoctorInfoAdd.aspx?id=" + id + "'></iframe>");
            SetDialogPosition("<%=div_Pop.ClientID %>", 20);
        }
        function HandelAddUser(sign, strMessage) {
            if (sign == "1") {
                showTips('操作成功', '', '1');
                CloseDialogById('<%=div_Pop.ClientID %>');
                setTimeout('document.location.reload()', 500);
            }
            else {
                showTipsErr(strMessage, '3')
            }
        }
    </script>
</asp:Content>
