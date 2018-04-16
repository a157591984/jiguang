<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true"
    CodeBehind="BasicRole.aspx.cs" Inherits="Rc.Cloud.Web.Sys.BasicRole" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-body">
                    <div class="form-inline search_bar mb">
                        <input type="button" value="新增角色" class="btn btn-primary btn-sm" onclick="showPop('', '');" />
                        <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control input-sm" placeholder="角色名称"></asp:TextBox>
                        <asp:Button runat="server" CssClass="btn btn-default btn-sm" Text="查询" ID="btn_Search" OnClick="btn_Search_Click" />
                    </div>
                    <%= GetHtmlData() %>
                    <hr />
                    <%= GetPageIndex() %>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var pageUrl = '<%=strPath%>';
        function showPop(SysRole_ID, Name) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: (SysRole_ID) ? '编辑' : '新增',
                    area: ['385px', '258px'],
                    content: 'Drug_RoleAddOrUpdate.aspx?id=' + SysRole_ID + '&n=' + Name
                });
            });
        }
    </script>
</asp:Content>
