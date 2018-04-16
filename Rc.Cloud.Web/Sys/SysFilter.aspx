<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SysFilter.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-body">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtKeyWord" TextMode="MultiLine" Rows="30" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                        <p class="help-block">说明：每个关键字请用逗号(支持全角半角)隔开。例如：你，我,他</p>
                    </div>
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="保存" OnClick="btnSave_Click" ClientIDMode="Static" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript" src="../Scripts/js001/jquery.min-1.8.2.js"></script>
    <script type="text/javascript" src="../Scripts/plug-in/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnSave").click(function () {
                layer.ready(function () {
                    if ($.trim($("#txtKeyWord").val()) == "") {
                        layer.msg('关键字不能为空', { time: 2000, icon: 2 })
                        $("#txtKeyWord").focus();
                        return false;
                    }
                })
            })
        })
    </script>
</asp:Content>

