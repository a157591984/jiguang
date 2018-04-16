<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="UpdateDataItemList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.UpdateDataItemList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="div_right_title"><div class="div_right_title_icon"></div>
    <%=siteMap%>
</div>
<div class="clearDiv"></div>
<div class="div_right_search">
    <table class="table_search_001">
        <tr>
            <td>数据项名称：</td>
            <td><asp:TextBox ID="txtCName" runat="server" CssClass="txt_Search"></asp:TextBox></td>

            <td><asp:Button runat="server" CssClass="btn" Text="查 询" id="btn_Search" 
                    onclick="btn_Search_Click" /> </td>
                     <td>
                <input id="btnAdd" type="button" class="btn" value="新增数据项" runat="server" onclick="showUpdateDataItemEdit(''); return false;" />
            </td>
        </tr>
    </table>
</div>
<div class="clearDiv" ></div>
    <div class="div_right_listtitle">
   数据项列表
        </div>
<div class="clearDiv"></div>
<!--主数据-->
<%= GetHtmlData() %>
<div class="clearDiv"></div>
<!--分页-->
 <%= GetPageIndex()%>

    <!-- 弹出层操作-->
<div class="div_ShowDailg" id="divPop_Hospital" runat="server" clientidmode="Static" style="width: 600px; height: 350px;">
        <div class="div_ShowDailg_Title" id="divPop_Hospital_Title" runat="server" clientidmode="Static">
            <div class="div_ShowDailg_Title_left" id="divPop_Hospital_Title_Left"></div>
            <div class="div_Close_Dailg" id="divPop_Hospital_Close" title="关闭" onclick="ClosePopHospitalEdit();"></div><!--关闭-->
        </div>     
        <div class="clearDiv" id="div_iframehp" runat="server"></div>              
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
<script language="javascript" type="text/javascript">
    $("#divPop_Hospital").easydrag();
    $("#divPop_Hospital").setHandler("divPop_Hospital_Title");
    function ClosePopHospitalEdit() {
        CloseDialogById('<%=divPop_Hospital.ClientID %>');
    }

    function showUpdateDataItemEdit(id) {
        if (id != "") {
            $("#divPop_Hospital_Title_Left").html("编辑数据项信息");
        } else {
            $("#divPop_Hospital_Title_Left").html("新增数据项信息");
        }
        $("#<%=divPop_Hospital.ClientID %>").show();
        $("#<%=div_iframehp.ClientID %>").html("<iframe  height=\"320\"  frameborder='0' width='100%' style='margin: 0px' src='UpdateDataItemEdit.aspx?id=" + id + "'></iframe>");
        SetDialogPosition("<%=divPop_Hospital.ClientID %>");
    }

    function div_PopEdit(sign, strMessage, id) {
        if (sign == "1") {
            showTips('操作成功', '', '1');
            CloseDialogById('<%=divPop_Hospital.ClientID %>');
            setTimeout("window.location.reload()", 2000);
        }
        else {
            showTipsErr(strMessage, '3')
        }
    }

    function div_PopEditS(sign, strMessage, id) {
        if (sign == "1") {
            showTips('操作成功', '', '1');
        }
        else {
            showTipsErr(strMessage, '3')
        }
    }

    function DeleteUpdateDataItem(id) {
        var con;
        con = "确认删除此数据项吗？";
        $.dialog.confirm(con,
            function () {
                var exists;
                $.get("../Ajax/SysAjax.aspx", { key: "DeleteUpdateDataItem", id: id, net4: Math.random() },
                function (data) {
                    if (data == "1") {
                        showTips('删除成功', '', '1')
                        setTimeout("window.location.reload()", 2000);
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
