<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="TeacherAudit.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.TeacherAudit" %>

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
        <table class="table_search_001">
            <tr>
                <td>用户名：
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" CssClass="txt_Search"></asp:TextBox>
                </td>
                <td>
                    <asp:Button runat="server" CssClass="btn" Text="查 询" ID="btn_Search" OnClick="btn_Search_Click" />
                </td>
                <td>
                    <input id="btnAddDepartment" type="button" class="btn" value="新增老师" onclick="showPopAddDepartment('', 2)" />
                </td>
            </tr>
        </table>
    </div>
    <div class="clearDiv">
    </div>
    <div class="div_right_listtitle">
        注册老师列表
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
    <!-- 弹出层操作 -->
    <div class="div_ShowDailg" id="div_Pop" style="width: 500px; height: 400px;">
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
    <script src="../Scripts/PhhcCommon.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $("#div_Pop").easydrag();
        $("#div_Pop").setHandler("div_Pop_Title");
        function showPopAddDepartment(id, handel) {
            var page = "TeacherAuditAdd.aspx?id=" + id + "&handel=" + handel;
            if (handel == "1") {
                $("#div_ShowDailg_Title_left").html("修改用户信息");
            }
            else {
                $("#div_ShowDailg_Title_left").html("添加用户信息");
            }
            $("#divIfram").html("<iframe id='iframDrugFiled' frameborder='0' width='100%' style='margin: 0px' src='" + page + "'  height='350px'></iframe>");
            $("#div_Pop").show();
            SetDialogPosition("div_Pop", 70);
        }

        //弹出层操作后处理
        function Handel(sign, strMessage) {
            if (sign == "1") {
                showTips('操作成功', '<%=strPageNameAndParm %>', '1');
        }
        else {
            showTipsErr('操作失败. ' + strMessage, '4')
        }
    }

    function Delete(id) {
        var con;
        con = "确定要删除吗？";
        $.dialog.confirm(con,
        function () {
            var exists;
            $.post("../Ajax/Execution.aspx", { key: "SysDepartment", Aid: id, net4: Math.random() },
            function (data) {
                if (data == "1") {
                    showTips('删除成功', '/Sys/SysDepartment.aspx', '1')
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
