<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SysProduct.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--弹出层类库-->
    <script src="../Scripts/PhhcCommon.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ScriptManager>
    <div class="div_right_title">
        <div class="div_right_title_icon">
        </div>
        <%=siteMap%>
    </div>
    <div class="clearDiv">
    </div>
    <div class="div_right_search">
        <label for="txtSysCodeName">
            系统名称：</label>
        <asp:TextBox ID="txtSysCodeName" runat="server" CssClass="txt" Width="117px"></asp:TextBox>
        <asp:Button ID="btnSearch" Text="查询" runat="server" CssClass="btn" OnClick="btnSearch_Click" />
        <input type="button" value="添加" class="btn" onclick="showPopSysCode('', 1);" />
    </div>
    <div class="clearDiv">
    </div>

    <div class="div_right_listtitle">
        系统列表
    </div>
    <div class="clearDiv">
    </div>
    <%= GetHtmlData() %>
    <hr />
    <%= GetPageIndex()%>
    <asp:HiddenField ID="hidItemDescID" runat="server" />
    <asp:HiddenField ID="hidHandel" runat="server" />
    <!-- 弹出层操作 -->
    <div class="div_ShowDailg" id="div_Pop" style="width: 500px; height: 300px;">
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
    <!--#include file="../Common/Tips.inc"-->
    <script language="javascript" type="text/javascript">
        $("#div_Pop").easydrag();
        $("#div_Pop").setHandler("div_Pop_Title");

        function DeleteItemDesc(id) {
            if (confirm('确定要删除吗？')) {
                jQuery.get("../Ajax/SysAjax.aspx", { key: "SysProductTempDelete", Aid: id, net4: Math.random() },
            function (data) {
                if (data == "1") {
                    showTips('删除成功', '/Sys/SysProduct.aspx', '1')
                }
                else {
                    showTipsErr('删除失败', '3')
                }
            });
            }
        }
        function showPopSysCode(id, handel) {
            var page = "SysProductAdd.aspx?id=" + id;
            if (handel == "1") {//添加
                $("#div_ShowDailg_Title_left").html("添加系统信息");
            }
            else {
                $("#div_ShowDailg_Title_left").html("修改系统信息");
            }
            $("#divIfram").html("<iframe id='iframDrugFiled' frameborder='0' width='100%' style='margin: 0px' src='" + page + "'  height='270px'></iframe>");
            $("#div_Pop").show();
            ShowDocumentDivBG();
            SetDialogPosition("div_Pop");
        }
        function Handel(sign, error) {
            if (sign == "1") {
                showTips('操作成功', '/Sys/SysProduct.aspx', '1');
                CloseDialog();
                CloseDocumentDivBG();
            }
            else {
                showTipsErr('操作失败' + " " + error, '4')
            }
        }
        function ShowAlert(str) {
            $.dialog.alert(str);
        }

    </script>
</asp:Content>
