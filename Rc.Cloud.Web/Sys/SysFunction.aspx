<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SysFunction.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysFunction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <input type="button" runat="server" id="btnAdd" class="btn btn-primary btn-sm" value="添加功能" onclick="showPop('1');" />
                </div>
                <%= GetHtmlData() %>
            </div>
        </div>
    </div>

    <!-- 弹出层操作 -->
    <div id="div_Pop" class="hidden">
        <div class="pa">
            <input type="hidden" id="hidCtrl" runat="server" />
            <div class="form-group">
                <label>编码</label>
                <asp:TextBox ID="txtFunctionId" runat="server" CssClass="form-control" IsRequired="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>名称</label>
                <asp:TextBox ID="txtFunctionName" runat="server" CssClass="form-control" IsRequired="true"></asp:TextBox>
            </div>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="保 存" OnClick="btnSave_Click" />
        </div>
    </div>

    <script language="javascript" type="text/javascript">
        function showPop(type, id, name) {
            $('#div_Pop').removeClass('hidden');
            if (type == "1") {
                $("#<%=hidCtrl.ClientID %>").val(1); //添加
                $("#<%=txtFunctionId.ClientID %>").val("");
                $("#<%=txtFunctionId.ClientID %>").attr("readonly", false);
                $("#<%=txtFunctionName.ClientID %>").val("");
            }
            else {
                $("#<%=hidCtrl.ClientID %>").val(2); //修改
                $("#<%=txtFunctionId.ClientID %>").val(id);
                $("#<%=txtFunctionId.ClientID %>").attr("readonly", true);
                $("#<%=txtFunctionName.ClientID %>").val(name);
            }
            layer.ready(function () {
                layer.open({
                    type: 1,
                    title: (type == 1) ? '新增' : '编辑',
                    area: ['385px', '255px'],
                    shade: false,
                    content: $('#div_Pop'),
                    cancel: function () {
                        $('#div_Pop').addClass('hidden');
                    }
                });
            });

        }
        function DeleteFunction(FunctionID) {
            layer.ready(function () {
                layer.confirm('删除数据？', { icon: 3 }, function () {
                    $.get("../ajax/SysAjax.aspx", { key: "DeleteFunction", FunctionID: FunctionID, net4: Math.random() },
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
                        layer.msg(data, { icon: 4 });
                    }
                });
                });
            });
        }
    </script>
</asp:Content>
