<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="basicSetting.aspx.cs" Inherits="Homework.teacher.basicSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                                <li class="active"><a href="basicSetting.aspx">基本设置</a></li>
                                <li><a href="safeSetting.aspx">安全设置</a></li>
                                <li><a href="allOrder.aspx">我的订单</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="panel_right">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-xs-2 control-label">登录名</div>
                            <div class="col-xs-3 form-control-static">
                                <asp:Label ID="labUserName" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-2 control-label">用户名</div>
                            <div class="col-xs-3">
                                <asp:TextBox ID="txtTrueUserName" runat="server" CssClass="form-control" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-xs-1 form-control-static text-muted">必填</div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-2 control-label">出生日期</div>
                            <div class="col-xs-3">
                                <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-2 control-label">性别</div>
                            <div class="col-xs-2">
                                <asp:DropDownList ID="ddlSex" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">-请选择-</asp:ListItem>
                                    <asp:ListItem Value="M">男</asp:ListItem>
                                    <asp:ListItem Value="F">女</asp:ListItem>
                                    <asp:ListItem Value="S">保密</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-2 control-label">E-mail</div>
                            <div class="col-xs-3">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="80"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-2 control-label">手机</div>
                            <div class="col-xs-3">
                                <asp:TextBox ID="txtMoblie" runat="server" CssClass="form-control" MaxLength="11" ClientIDMode="Static" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-2 control-label">地区</div>
                            <div class="col-xs-2">
                                <asp:DropDownList ID="ddlProvince" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPro_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>-省-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-2">
                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>-市-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-2">
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control">
                                    <asp:ListItem>-县-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-2 control-label">学科</div>
                            <div class="col-xs-2">
                                <asp:DropDownList runat="server" ID="ddlSubject" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-3 col-xs-offset-2">
                                <asp:Button ID="btn_Save" runat="server" CssClass="btn btn-primary" Text="保存" OnClick="btn_Save_Click" OnClientClick="return Save_chk()" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%--<div class="tree">
                        <ul data-level="0">
                            <li>
                                <div class="name active">
                                    <i class="tree_btn fa"></i>
                                    <a href="basicSetting.aspx">基本设置</a>
                                </div>
                            </li>
                            <li>
                                <div class="name">
                                    <i class="tree_btn fa"></i>
                                    <a href="safeSetting.aspx">安全设置</a>
                                </div>
                            </li>
                            <li>
                                <div class="name">
                                    <i class="tree_btn fa fa-angle-down"></i>
                                    <a href='##' data-disabled="true">订单管理</a>
                                </div>
                                <ul data-level="1">
                                    <li>
                                        <div class="name">
                                            <i class="tree_btn fa"></i>
                                            <a href="allOrder.aspx">全部订单</a>
                                        </div>
                                    </li>
                                    <li>
                                        <div class="name">
                                            <i class="tree_btn fa"></i>
                                            <a href="waitPay.aspx">待付款</a>
                                        </div>
                                    </li>
                                    <li>
                                        <div class="name">
                                            <i class="tree_btn fa"></i>
                                            <a href="waitRate.aspx">我的评价</a>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>--%>
        </div>
    </div>
    <script src="../plugin/laydate/laydate.js"></script>
    <script type="text/javascript">
        $(function () {
            var starTime = {
                elem: '#txtAge',
                format: 'YYYY-MM-DD',
                max: laydate.now(),
            }
            laydate(starTime);
            $("#txtTrueUserName").bind({
                blur: function () { this.value = js_validate.Filter(this.value); }
            });
        });
        //表单验证
        function Save_chk() {
            if ($.trim($("#txtTrueUserName").val()) == "") {
                layer.alert("请填写真实姓名");
                return false;
            }
            if ($.trim($("#txtEmail").val()) != "" && !js_validate.IsEmail($("#txtEmail").val())) {
                layer.alert("请填写正确的邮箱！");
                return false;
            }
            if ($.trim($("#txtMoblie").val()) != "" && !js_validate.IsMobileNum2($("#txtMoblie").val())) {
                layer.alert("请填写正确的手机号码！");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
