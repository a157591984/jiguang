<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="UserControlConfig.aspx.cs" Inherits="Rc.Cloud.Web.Sys.UserControlConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <input id="btnAddUser" type="button" class="btn btn-primary btn-sm" value="新增" onclick="showPopAddOrEdit('')" />
                    <asp:Button runat="server" CssClass="btn btn-info btn-sm" Text="导出" ID="Button1" OnClick="tooutline" />
                    <asp:TextBox ID="txtMark" runat="server" CssClass="form-control input-sm" placeholder="标识"></asp:TextBox>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control input-sm" placeholder="名称"></asp:TextBox>
                    <asp:Button runat="server" CssClass="btn btn-default btn-sm" Text="查询" ID="btn_Search" OnClick="btn_Search_Click" />
                </div>
                <%= GetHtmlData() %>
                <hr />
                <%= GetPageIndex() %>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        var pageUrl = '<%=ReturnUrl%>';
        function showPopAddOrEdit(dictionarySQlMaintenanceID) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: (dictionarySQlMaintenanceID) ? '编辑' : '新增',
                    area: ['650px', '490px'],
                    content: 'UserControlConfigAdd.aspx?DictionarySQlMaintenanceID=' + dictionarySQlMaintenanceID
                });
            });
        }
        function DeleteItemDesc(dictionarySQlMaintenance_ID) {
            layer.ready(function () {
                layer.confirm('删除数据？', { icon: 3 }, function () {
                    $.post("../Ajax/SysAjax.aspx", { key: "DeleteDSQL", Aid: dictionarySQlMaintenance_ID, net4: Math.random() },
                function (data) {
                    if (data == "1") {
                        layer.msg('删除成功', { icon: 1, time: 1000 }, function () {
                            window.location.reload();
                        });
                    }
                    else if (data == "0") {
                        layer.msg('删除失败', { icon: 2 });
                    }
                    else {
                        layer.msg(data, { icon: 2 });
                    }
                });
                });
            });
        }
    </script>
</asp:Content>
