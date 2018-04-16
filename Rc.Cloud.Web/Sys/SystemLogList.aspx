<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SystemLogList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SystemLogList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control input-sm" placeholder="操作人名称"></asp:TextBox>
                    <asp:TextBox ID="txtContent" runat="server" CssClass="form-control input-sm" placeholder="操作内容"></asp:TextBox>
                    <asp:Button runat="server" CssClass="btn btn-default btn-sm" Text="查询" ID="btn_Search"
                        OnClick="btn_Search_Click" />
                </div>
                <%= selectAllSystemLogModel()%>
                <%= GetPageIndex() %>
            </div>
        </div>
    </div>
    <script>
        function showInfo(id, handel, sysUser_Name) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '查看',
                    area: ['650px', '550px'],
                    content: 'SystemLogListView.aspx?id=' + id + '&sysUser_Name=' + sysUser_Name
                })
            });
        }
    </script>
</asp:Content>

