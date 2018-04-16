<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true"
    CodeBehind="MenuManageSy.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysMenuManageSy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>菜单管理</title>
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
                    模块名称：
                </td>
                <td>
                    <asp:TextBox ID="moduleNameTxt" runat="server" ClientIDMode="Static" CssClass="txt"></asp:TextBox>
                </td>
                <td>
                    <asp:Button runat="server" CssClass="btn" Text="查 询" ID="btn_Search" OnClick="btn_Search_Click" />
                    <input class="btn" value="添加模块" type="button" id="btn_Add" onclick="showEdit('0','');" />
                     <input class="btn" value="同步" type="button" id="tongbu" onclick="Synchronous();" />
                </td>
            </tr>
        </table>
    </div>
    <div class="div_right_listtitle">
        菜单列表
    </div>
    <div class="clearDiv">
    </div>
    <div style="width: 100%; height: 500px; overflow-y: scroll; overflow-x: scroll;">
        <asp:Repeater ID="rptModule" runat="server">
            <HeaderTemplate>
                <table class='table_list' cellpadding='0' id='listtable' cellspacing='0'>
                    <tr class="tr_title">
                        <%--<th style="width: 100px; display:none;">
                            系统名称
                        </th>--%>
                        <th style="width: 85px">
                            模块编码
                        </th>
                        <th style="width: 100px">
                            模块名称
                        </th>
                        <th style="width: 85px">
                            父模块编码
                        </th>
                        <th style="width: 85px">
                            SLEVEL
                        </th>
                        <th>
                            链接地址
                        </th>
                        <th style="width: 63px">
                            是否显示
                        </th>
                        <th style="width: 63px">
                            菜单级别
                        </th>
                        <th style="width: 105px">
                            是否最后一级菜单
                        </th>
                        <th style="width: 65px" title="最大值是默认页">
                            默认页
                        </th>
                        <th style="width: 135px">
                            操作
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="tr_con_002">
                   <%-- <td class="aui_td_content" style="display:none";>
                        <%//#Eval("SysName")%>
                    </td>--%>
                    <td class="aui_td_content">
                        <%# Eval("MODULEID") %>
                    </td>
                    <td class="aui_td_content">
                        <%#Eval("MODULENAME")%>
                    </td>
                    <td class="aui_td_content">
                        <%#Eval("PARENTID")%>
                    </td>
                    <td class="aui_td_content">
                        <%#Eval("SLEVEL")%>
                    </td>
                    <td class="aui_td_content">
                        <%#Eval("URL")%>
                    </td>
                    <td class="aui_td_content">
                        <%#Eval("ISINTREE").ToString()=="Y"?"是":"否"%>
                    </td>
                    <td class="aui_td_content">
                        <%#Eval("Depth")%>
                    </td>
                    <td class="aui_td_content">
                        <%#Eval("isLast").ToString()=="1"?"是":"否"%>
                    </td>
                    <td class="aui_td_content" title="最大值是默认页">
                        <%#Eval("DefaultOrder")%>
                    </td>
                    <td class="aui_td_content">
                    <a href="javascript:void(0)" onclick="showEdit('1','<%#Eval("MODULEID")%>')">类似新增</a>
                    <a href="javascript:void(0)" onclick="showEdit('2','<%#Eval("MODULEID") %>')">修改</a>
                    <a href="javascript:void(0)" onclick="DeleteSysModuleByID('<%#Eval("MODULEID") %>')"> 删除</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div class="div_ShowDailg" id="div_Pop" runat="server">
        <div class="div_ShowDailg_Title" id="div_ShowDailg_Title">
            <div class="div_ShowDailg_Title_left" id="div_Pop_Title">
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
    <script type="text/javascript">
        $("#<%=div_Pop.ClientID %>").easydrag();
        $("#<%=div_Pop.ClientID %>").setHandler("div_ShowDailg_Title");

        function showEdit(action, moduleID, sysCode) {
            var src = "SysMenuManageEdit.aspx?action=" + action + "&moduleID=" + moduleID ;
            if (action == "0") {
                $("#div_Pop_Title").html("新增模块信息");
            }
            else if (action == "1") {
                $("#div_Pop_Title").html("类似新增模块信息");
            }
            else if (action == "2") {
                $("#div_Pop_Title").html("修改模块信息");
            }
            $("#<%=div_Pop.ClientID %>").css({ width: "800px", height: "600px" });
            $("#<%=div_iframe.ClientID %>").html("<iframe  height=\"550\"  frameborder='0' width='100%' style='margin: 0px' src='" + src + "'></iframe>");
            SetDialogPosition("<%=div_Pop.ClientID %>", 70);
            $("#<%=div_Pop.ClientID %>").show();
        }

        function Synchronous() {
            var src = "SysMenuManageSynchronous.aspx";
            $("#div_Pop_Title.ClientID").html("同步菜单");

            $("#<%=div_Pop.ClientID %>").css({ width: "800px", height: "600px" });
            $("#<%=div_iframe.ClientID %>").html("<iframe  height=\"550\"  frameborder='0' width='100%' style='margin: 0px' src='" + src + "'></iframe>");
            SetDialogPosition("<%=div_Pop.ClientID %>", 70);
            $("#<%=div_Pop.ClientID %>").show();
        }

        function DeleteSysModuleByID(id) {
            if (confirm('确定要删除吗？')) {
                jQuery.get("../Ajax/SysAjax.aspx", { key: "DeleteSysModuleByID", id: id,  net4: Math.random() },
            function (data) {
                if (data == "1") {
                    showTips('删除成功', 'SysMenuManage.aspx', '2');
                }
                else {
                    showTipsErr('删除失败', '3');
                }
            });
            }
        }
        function Handel(message, url) {
            showTips(message, '', '2');
            $("#<%=btn_Search.ClientID %>").click();
        }
       
    </script>
</asp:Content>
