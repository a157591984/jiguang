<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SysUser.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-body">
                    <div class="form-inline search_bar mb">
                        <input id="btnAddUser" type="button" class="btn btn-primary btn-sm" value="新增用户" onclick="showPopAddUser('')" />
                        <asp:TextBox ID="txtsysUserName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:Button runat="server" CssClass="btn btn-default btn-sm" Text="查询" ID="btn_Search" OnClick="btn_Search_Click" />
                    </div>
                    <%= GetHtmlData() %>
                    <hr />
                    <%= GetPageIndex() %>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        var pageUrl = '<%=strPageNameAndParm %>';
        function showPopAddUser(SysUser_ID) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: (SysUser_ID) ? '编辑' : '新增',
                    area: ['385px', '628px'],
                    content: 'SysUserAdd.aspx?SysUser_ID=' + SysUser_ID
                });
            });
        }
        function tasking(SysUser_ID) {
            var src = "SysTasking.aspx?SysUser_ID=" + SysUser_ID;
            $("#div_Pop").show();
            if (SysUser_ID == "") {
                $("#div_ShowDailg_Title_left").html("任务分配");
            }
            else {
                $("#div_ShowDailg_Title_left").html("任务分配");
            }
            $("#div_iframe").html("<iframe  height=\"320\"  frameborder='0' width='100%' style='margin: 0px' src='" + src + "'></iframe>");

            SetDialogPosition("div_Pop", 70);
        }
        function DeleteItemDesc(id) {
            layer.ready(function () {
                layer.confirm('删除数据？', { icon: 3 }, function () {
                    $.post("../Ajax/SysAjax.aspx", { key: "SysUserTempDelete", Aid: id, net4: Math.random() },
                    function (data) {
                        if (data == "1") {
                            layer.ready(function () {
                                layer.msg('删除成功', { icon: 1, time: 1000 }, function () {
                                    window.location.reload();
                                });
                            });
                        }
                        else if (data == "0") {
                            layer.msg('删除失败', { icon: 2 });
                        }
                        else {
                            layer.msg('data', { icon: 2 });
                        }
                    });
                });
            });
        }
    </script>
</asp:Content>
