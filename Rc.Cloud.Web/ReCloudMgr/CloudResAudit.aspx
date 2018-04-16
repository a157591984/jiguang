<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="CloudResAudit.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.CloudResAudit" %>

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
        <dl>
            <dd><span class="spanTitle">年级学期：</span><span><a href="javascript:;" class="active">高三上学期</a></span><span>高三下学期</span></dd>
        </dl>
        <dl>
            <dd><span class="spanTitle">学科：</span><span>语文</span><span>数学</span></dd>
        </dl>
        <dl>
            <dd><span class="spanTitle">教材版本：</span><span>人教版</span><span>苏教版</span></dd>
        </dl>
        <table>
            <tr>
                <td>
                    <label for="txtSysCodeName">
                        名称：</label>
                </td>
                <td>
                    <asp:TextBox ID="txtD_Name" runat="server" MaxLength="50" CssClass="txt_Search"></asp:TextBox>
                </td>
                <td>
                    <label for="txtD_Type">
                        类型：</label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlD_Type" runat="server" ClientIDMode="Static">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" Text="查询" runat="server" CssClass="btn" OnClick="btnSearch_Click" />
                </td>
                <td>
                    <input type="button" value="新增字典" class="btn" onclick="showPopCommon_Dict('', 1);" />
                </td>
            </tr>
        </table>
    </div>
    <div class="clearDiv">
    </div>

    <div class="div_right_listtitle">
        字典列表
    </div>
    <div class="clearDiv">
    </div>
    <%= GetHtmlData() %>
    <hr />
    <%= GetPageIndex()%>
    <asp:HiddenField ID="hidItemDescID" runat="server" />
    <asp:HiddenField ID="hidHandel" runat="server" />
    <!-- 弹出层操作 -->
    <div class="div_ShowDailg" id="div_Pop" style="width: 600px; height: 400px;">
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
    <script type="text/javascript">
        $("#div_Pop").easydrag();
        $("#div_Pop").setHandler("div_Pop_Title");

        function DeleteItemDesc(id) {
            var con;
            con = "确定要删除吗？";
            $.dialog.confirm(con,
            function () {
                var exists;
                $.get("../Ajax/SysAjax.aspx", { key: "SysCommon_DictTempDelete", Aid: id, net4: Math.random() },
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
        function showPopCommon_Dict(id, handel) {
            var page = "SysCommon_DictAdd.aspx?id=" + id;
            if (handel == "1") {//添加
                $("#div_ShowDailg_Title_left").html("新增字典信息");
            }
            else {
                $("#div_ShowDailg_Title_left").html("修改字典信息");
            }
            $("#divIfram").html("<iframe id='iframDrugFiled' frameborder='0' width='100%' style='margin: 0px' src='" + page + "'  height='370px'></iframe>");
            $("#div_Pop").show();
            SetDialogPosition("div_Pop", 70);
        }
        function Handel(sign, error) {
            if (sign == "1") {
                showTips('操作成功', '<%=ReturnUrl %>', '1');
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
