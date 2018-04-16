<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="safeSetting.aspx.cs" Inherits="Homework.teacher.safeSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            //表单验证
            $("#btnConfirm").click(function () {
                if ($.trim($("#txtOldPass").val()) == "") {
                    layer.msg("请输入原始密码", { icon: 4 });
                    return false;
                }
                if ($.trim($("#txtNewPass").val()) == "") {
                    layer.msg("请输入新密码", { icon: 4 });
                    return false;
                }
                if ($.trim($("#txtNewPass2").val()) == "") {
                    layer.msg("请输入重复密码", { icon: 4 });
                    return false;
                }
                if ($.trim($("#txtNewPass2").val()) != $.trim($("#txtNewPass").val())) {
                    layer.msg("两次输入的密码不一致", { icon: 4 });
                    return false;
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container">
        <div class="container ph">
            <div class="personal_center_panel">
                <div class="panel_left">
                    <div class="sidebar_menu_panel">
                        <div class="panel_heading">
                            个人中心
                        </div>
                        <div class="panel_body">
                            <ul>
                                <li><a href="basicSetting.aspx">基本设置</a></li>
                                <li class="active"><a href="safeSetting.aspx">安全设置</a></li>
                                <li><a href="allOrder.aspx">我的订单</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="panel_right">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-xs-2 control-label">原始密码</div>
                            <div class="col-xs-3">
                                <asp:TextBox ID="txtOldPass" TextMode="Password" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-xs-2 form-control-static text-muted">必填</div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-2 control-label">新密码</div>
                            <div class="col-xs-3">
                                <asp:TextBox ID="txtNewPass" TextMode="Password" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-xs-2 form-control-static text-muted">必填</div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-2 control-label">重复密码</div>
                            <div class="col-xs-3">
                                <asp:TextBox ID="txtNewPass2" TextMode="Password" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-xs-2 form-control-static text-muted">必填</div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-3 col-xs-offset-2">
                                <asp:Button ID="btnConfirm" runat="server" ClientIDMode="Static" CssClass="btn btn-primary" Text="保存" OnClick="btnConfirm_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
