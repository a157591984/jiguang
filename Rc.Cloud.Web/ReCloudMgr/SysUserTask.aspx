<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SysUserTask.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.SysUserTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-body">
                    <div class="form-inline search_bar pb">
                        <asp:TextBox ID="txtsysUserName" runat="server" CssClass="form-control input-sm" placeholder="用户名"></asp:TextBox>
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
        function tasking(SysUser_ID) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '任务分配',
                    area: ['850px', '585px'],
                    content: '/sys/SysTasking.aspx?SysUser_ID=' + SysUser_ID

                });
            });
        }
    </script>
</asp:Content>
