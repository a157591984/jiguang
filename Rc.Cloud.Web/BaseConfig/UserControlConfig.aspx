<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="UserControlConfig.aspx.cs" Inherits="Rc.Cloud.Web.BaseConfig.UserControlConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        <table class="table_search">
            <tr>
                <td>标识：
                </td>
                <td>
                    <asp:TextBox ID="txtMark" runat="server" CssClass="txt_Search"></asp:TextBox>
                </td>
                <td>名称：
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" CssClass="txt_Search"></asp:TextBox>
                </td>
                <td>
                    <asp:Button runat="server" CssClass="btn" Text="查 询" ID="btn_Search" OnClick="btn_Search_Click" />
                </td>
                <td>
                    <input id="btnAddUser" type="button" class="btn" value="新 增" onclick="showPopAddOrEdit('')" />
                </td>
                  <td>
                    <asp:Button runat="server" CssClass="btn" Text="导 出" ID="Button1" OnClick="tooutline"  />
                </td>
            </tr>
        </table>
    </div>
    <div class="clearDiv">
    </div>
    <div class="div_right_listtitle">
      字典SQL配置列表
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
    <div class="div_ShowDailg" id="div_Pop" style="width:650px;height:500px;">
        <div class="div_ShowDailg_Title" id="div_Pop_Title">
            <div class="div_ShowDailg_Title_left" id="div_ShowDailg_Title_left">
            </div>
            <div class="div_Close_Dailg" id="div_Close_Dailg" title="关闭" onclick="ClosePopByIdAddUser();">
            </div>
            <!--关闭-->
        </div>
        <div class="clearDiv" id="div_iframe">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
     <!--#include file="../Common/Tips.inc"-->
    <script language="javascript" type="text/javascript"  >

        $("#div_Pop").easydrag();
        $("#div_Pop").setHandler("div_Pop_Title");

        function ClosePopByIdAddUser() {
            CloseDialogById('div_Pop');
        }

        function showPopAddOrEdit(dictionarySQlMaintenanceID) {
            var src = "UserControlConfigAdd.aspx?DictionarySQlMaintenanceID=" + dictionarySQlMaintenanceID;
            $("#div_Pop").show();
            if (dictionarySQlMaintenanceID == "") {
                $("#div_ShowDailg_Title_left").html("新增通用控件SQL配置");
            }
            else {
                $("#div_ShowDailg_Title_left").html("修改通用控件SQL配置");
            }
            $("#div_iframe").html("<iframe  height=\"450\"  frameborder='0' width='100%' style='margin: 0px' src='" + src + "'></iframe>");

            SetDialogPosition("div_Pop", 50);
        }

        function ShowAlert(str) {
            $.dialog.alert(str);
        }

        //弹出层操作后处理
        function Handel(sign, strMessage) {
            if (sign == "1") {
                showTips('操作成功', '<%=ReturnUrl %>', '1');
        }
        else {
            showTipsErr('操作失败. ' + strMessage, '4')
        }
    }
        function DeleteItemDesc(dictionarySQlMaintenance_ID) {
        var con;
        con = "确定要删除吗？";
        $.dialog.confirm(con,
        function () {
            var exists;
            $.post("../Ajax/SysAjax.aspx", { key: "DeleteDSQL", Aid: dictionarySQlMaintenance_ID, net4: Math.random() },
            function (data) {
                if (data == "1") {
                    showTips('删除成功', '<%=ReturnUrl %>', '1')
                }
                else if (data == "0") {
                    showTipsErr('删除失败', '3')
                }
                else {
                    showTipsErr(data, '3')
                }
            });
        },
            function () {
            });
    }
    </script>
</asp:Content>
