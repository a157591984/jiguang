<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="ListStructure.aspx.cs" Inherits="Rc.Cloud.Web.Sys.ListStructure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap %>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <asp:TextBox ID="txtMark" runat="server" CssClass="form-control input-sm" placeholder="表名"></asp:TextBox>
                    <asp:TextBox ID="TableDescription" runat="server" CssClass="form-control input-sm" placeholder="表说明"></asp:TextBox>
                    <label>表说明是否为空</label>
                    <asp:DropDownList ID="tdyon" runat="server" CssClass="form-control input-sm">
                        <asp:ListItem Value="01">全部</asp:ListItem>
                        <asp:ListItem Value="11">是</asp:ListItem>
                        <asp:ListItem Value="00">否</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-inline search_bar mb">
                    <asp:TextBox ID="Field" runat="server" CssClass="form-control input-sm" placeholder="字段名"></asp:TextBox>
                    <asp:TextBox ID="FieldDescribe" runat="server" CssClass="form-control input-sm" placeholder="字段说名"></asp:TextBox>
                    <label>字段是否为空</label>
                    <asp:DropDownList ID="yesorno" runat="server" CssClass="form-control input-sm">
                        <asp:ListItem Value="01">全部</asp:ListItem>
                        <asp:ListItem Value="11">是</asp:ListItem>
                        <asp:ListItem Value="00">否</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button runat="server" CssClass="btn btn-default btn-sm" Text="查询" ID="btn_Search" OnClick="btn_Search_Click" />
                </div>
                <%= GetHtmlData() %>
                <hr />
                <%= GetPageIndex() %>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function AddModule(database, tablename, tableinfo, tableColumn, tablecolumninfo) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '编辑表说明',
                    area: ['385px', '250px'],
                    content: "ListStructureItem.aspx?tablename=" + tablename + "&tableinfo=" + tableinfo + "&tableColumn=" + tableColumn + "&tablecolumninfo=" + tablecolumninfo + "&database=" + database
                });
            })
        }
        function AddModuleColumns(database, tablename, tableColumn, tablecolumninfo) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '编辑字段说明',
                    area: ['385px', '250px'],
                    content: "ListStructureItemColumn.aspx?tablename=" + tablename + "&tableColumn=" + tableColumn + "&tablecolumninfo=" + tablecolumninfo + "&database=" + database
                });
            })
        }
        function HandelAddUser(sign, strMessage) {
            layer.ready(function () {
                if (sign == "1") {
                    layer.msg('编辑成功', { icon: 1, time: 1000 }, function () {
                        window.location.reload();
                    })
                } else {
                    layer.msg(strMessage, { icon: 4 });
                }
            })
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>
