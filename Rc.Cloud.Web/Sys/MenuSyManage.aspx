<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="MenuSyManage.aspx.cs" Inherits="Rc.Cloud.Web.Sys.MenuSyManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>菜单管理</title>
    <!--当前页面的js-->
    <script src="/Scripts/DialogDiv.js" type="text/javascript"></script>
    <script src="/Scripts/ProductDrugView.js" type="text/javascript"></script>
</asp:Content>

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
                <td class="td_search_001">
                    系统名称：
                </td>
                <td class="td_search_002">
                    <asp:DropDownList ID="ddlSysCode" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="td_search_001">
                    模块名称：
                </td>
                <td class="td_search_002">
                    <asp:TextBox ID="moduleName" runat="server" CssClass="txt" Width="130px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button runat="server" CssClass="btn" Text="查 询" ID="btn_Search" OnClick="btn_Search_Click" />
                </td>
                <td>
                   <%-- <input id="btnSetDisplay" type="button" class="btn"  value="设置显示的列" onclick="SetDisplay()" />--%>
                    <input class="btn" value="添加模块" type="button" id="btn_Add" onclick="AddModule('','','');" />
                </td>
                <td>
                   <%-- <input id="btnSetDisplay" type="button" class="btn"  value="设置显示的列" onclick="SetDisplay()" />--%>
                    <%--<input class="btn" value="同步" type="button" id="tongbu" runat="server" onclick="Synchronous();" />--%>
                      <asp:Button runat="server" CssClass="btn" Text="同 步" ID="tongbu" OnClick="btnTongBu_Click" />
                    <asp:Button runat="server" CssClass="btn" Text="生成脚本" ID="Button1" OnClick="btnout_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="clearDiv">
    </div>
    <div class="div_right_listtitle">
        药品列表
    </div>
    <div class="clearDiv">
    </div>
    <!--主数据-->
    <%= GetHtmlData() %>
    <div class="clearDiv">
    </div>
    <!--分页-->
    <%= GetPageIndex() %>
    <div class="clearDiv">
    </div>
    <!-- 弹出层操作-->
    <div class="div_ShowDailg pop_Big" id="div_Pop">
        <div class="div_ShowDailg_Title" id="div_Pop_Title">
            <div class="div_ShowDailg_Title_left" id="div_ShowDailg_Title_left">
            </div>
            <div class="div_Close_Dailg" id="div_Close_Dailg" title="关闭" onclick="ClosePopByIdAddUser();">
            </div>
            <!--关闭-->
        </div>
        <div class="clearDiv" id="div1">
        </div>
        <div id="divIfram" class="divIfram">
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
<script type="text/javascript" language="javascript" >
    $("#div_Pop").easydrag();
    $("#div_Pop").setHandler("div_Pop_Title");
    function ClosePopByIdAddUser() {
        CloseDialogById('div_Pop');
    }
    
    function AddModule(action, moduleID, sysCode) {
        var src = "MenuSyManageEdit.aspx?action=" + action + "&moduleID=" + moduleID + "&sysCode=" + sysCode;
        $("#div_Pop").show();
        if (action == 1) {
            $("#div_ShowDailg_Title_left").html("类似新增模块信息");
        }
        else if (action == 2) {
            $("#div_ShowDailg_Title_left").html("修改模块信息");
        }
        else {
            $("#div_ShowDailg_Title_left").html("新增模块信息");
        }
        $("#divIfram").html("<iframe  height=\"550\"  frameborder='0' width='100%' style='margin: 0px' src='" + src + "'></iframe>");

        SetDialogPosition("div_Pop", 30);
    }

    function Synchronous() {
        var src = "SysMenuManageSynchronous.aspx";
        $("#div_Pop").show();
        $("#div_ShowDailg_Title_left").html("同步菜单");

        $("#divIfram").html("<iframe  height=\"550\"  frameborder='0' width='100%' style='margin: 0px' src='" + src + "'></iframe>");

        SetDialogPosition("div_Pop", 30);
    }

    function DeleteSysModuleByID(id, syscode) {
        $.dialog.confirm('确定要删除吗？',
            function () {
                $.ajax({
                    type: "Post",     //AJAX提交方式为GET提交
                    url: "../Ajax/SysAjax.aspx",   //处理页的URL地址
                    data: { Key: "DeleteBaseSysModuleByID", id: id, syscode: syscode
                    },
                    success: function (result) {   //成功后执行的方法
                        if (result == "1") {
                            showTips('删除成功！', '<%=ReturnUrl %>', '1')
                        }
                        else {
                            showTipsErr('删除失败！', '4');
                        }
                    }
                });
            },
           function () { });
    }

    function Handel(message, url) {
        showTips(message, '<%=ReturnUrl %>', '2');
        //        $("#<%=btn_Search.ClientID %>").click();
    }
</script>

</asp:Content>
