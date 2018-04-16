<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SysDepartment.aspx.cs" Inherits="Rc.Cloud.Web.Sys.TeacherAudit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-body">
                    <div class="form-inline search_bar pb">
                        <input id="btnAddDepartment" type="button" class="btn btn-primary btn-sm" value="新增部门" onclick="showPopAddDepartment('')" />
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control input-sm" placeholder="部门名称"></asp:TextBox>
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
        var strPageNameAndParm = '<%=strPageNameAndParm%>';
        function showPopAddDepartment(handel,id) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: (handel == '1') ? '编辑' : '新增',
                    area: ['385px', '500px'],
                    content: 'SysDepartmentAdd.aspx?id=' + id
                });
            });
        }

        function Delete(id) {
            layer.ready(function () {
                layer.confirm("删除数据？", { icon: 3 }, function () {
                    $.post("../Ajax/Execution.aspx", { key: "SysDepartment", Aid: id, net4: Math.random() },
                        function (data) {
                            if (data == "1") {
                                layer.msg('删除成功', { icon: 1, time: 1000 }, function () {
                                    window.location.reload();
                                })
                            }
                            else if (data == "0") {
                                layer.msg('删除失败', { icon: 2 });
                            }
                            else {
                                layer.msg(data);
                            }
                        }
                    );
                });
            });
        }
    </script>
</asp:Content>

