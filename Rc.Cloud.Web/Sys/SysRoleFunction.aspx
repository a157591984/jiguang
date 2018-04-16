<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true"
    CodeBehind="SysRoleFunction.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysRoleFunction" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
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
                    角色名称：
                </td>
                <td>
                    <asp:TextBox ID="txtRoleName" runat="server" CssClass="txt_Search"></asp:TextBox>
                </td>
                <td>
                    <asp:Button runat="server" CssClass="btn" Text="查 询" ID="btn_Search" OnClick="btn_Search_Click" />
                </td>
                <td style="display: none;">
                    <asp:Button runat="server" CssClass="btn" Text="添加角色" ID="btnAdd" />
                </td>
                <td>
                    <input type="button" value="新 增" class="btn" onclick="showPop('','');" />
                </td>
            </tr>
        </table>
    </div>
    <div class="clearDiv">
    </div>
    <div class="div_right_listtitle">
       角色列表
    </div>
    <div class="clearDiv">
    </div>
    <!--主数据-->
    <%= GetHtmlData() %>
    <div class="clearDiv">
    </div>
    <!--分页-->
    <%= GetPageIndex() %>
    <!-- 弹出层操作  	控件 -->
    <div class="div_ShowDailg" id="div_Pop" runat="server">
        <div class="div_ShowDailg_Title" id="div_Pop_Title" runat="server">
            <div class="div_ShowDailg_Title_left" id="div_ShowDailg_Title_left">
            </div>
            <div class="div_Close_Dailg" id="div_Close_Dailg" title="关闭" onclick="ClosePopById();">
            </div>
            <!--关闭-->
        </div>
        <div class="clearDiv" id="div_iframe" runat="server">
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        function showPop(SysRole_ID, Name) {
            var prul = "";
            prul = "?id=" + SysRole_ID + "&n=" + Name;

            if (SysRole_ID == "") {
                $("#<%=div_Pop.ClientID %>").css({ width: "300px", height: "200px" });
            }
            else {
                $("#<%=div_Pop.ClientID %>").css({ width: "300px", height: "200px" });
            }
            if (SysRole_ID == "") {
                $("#div_ShowDailg_Title_left").html("新增角色");
            }
            else {
                $("#div_ShowDailg_Title_left").html("修改角色");
            }
            $("#<%=div_Pop.ClientID %>").show();
            $("#<%=div_iframe.ClientID %>").html("<iframe    frameborder='0' width='100%' style='margin: 0px' src='Drug_RoleAddOrUpdate.aspx" + prul + "'></iframe>");
            SetDialogPosition("<%=div_Pop.ClientID %>");
        }

        function Handel(sign, strMessage) {
            if (sign == "1") {
                showTips('操作成功', '<%=strPath %>', '1');
                //SearchData();
                ClosePopById();
            }
            else {
                showTipsErr('操作失败. ' + strMessage, '4')
            }
        }
        function ClosePopById() {
            CloseDialogById('<%=div_Pop.ClientID %>');

        }
    </script>
</asp:Content>
