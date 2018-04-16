<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="MessageTemplateEdit.aspx.cs" Inherits="Rc.Cloud.Web.Sys.MessageTemplateEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Styles/style001/Style.css" rel="stylesheet">
    <link rel="stylesheet" href="./../Styles/style001/LayerForm.css">
    <script type="text/javascript" src="../Scripts/js001/jquery.min-1.8.2.js"></script>
    <script type="text/javascript" src="../Scripts/plug-in/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            $("#btnSubmit").click(function () {
                if ($.trim($("#ddlSType").val()) == "") {
                    layer.msg("请选择类型", { time: 1000, icon: 2 }, function () { $("#ddlSType").select(); });
                    return false;
                }
                if ($.trim($("#txtUserName").val()) == "") {
                    layer.msg("请填写用户名", { time: 1000, icon: 2 }, function () { $("#txtUserName").focus(); });
                    return false;
                }
                if ($.trim($("#txtPassword").val()) == "") {
                    layer.msg("请填写密码", { time: 1000, icon: 2 }, function () { $("#txtPassword").focus(); });
                    return false;
                }

                if ($.trim($("#txtUrl").val()) == "") {
                    layer.msg("请填写平台URL", { time: 1000, icon: 2 }, function () { $("#txtUrl").focus(); });
                    return false;
                }
                if ($.trim($("#ddlMethod").val()) == "") {
                    layer.msg("请选择请求方式", { time: 1000, icon: 2 }, function () { $("#ddlMethod").focus(); });
                    return false;
                }
                //if ($.trim($("#txtContent").val()) == "") {
                //    layer.msg("请填写内容", { time: 1000, icon: 2 }, function () { $("#txtContent").focus(); });
                //    return false;
                //}
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="default_form_box form_box">
            <dl class="clearfix">
                <dt>类型<span>*</span></dt>
                <dd>
                    <asp:DropDownList runat="server" ID="ddlSType" CssClass="select">
                    </asp:DropDownList>
                </dd>
            </dl>
            <dl class="clearfix">
                <dt>企业ID</dt>
                <dd>
                    <asp:TextBox runat="server" ID="txtUserId" CssClass="input_text" MaxLength="50"></asp:TextBox>
                </dd>
            </dl>
            <dl class="clearfix">
                <dt>用户名<span>*</span></dt>
                <dd>
                    <asp:TextBox runat="server" ID="txtUserName" CssClass="input_text" MaxLength="50"></asp:TextBox>
                </dd>
            </dl>
            <dl class="clearfix">
                <dt>密码<span>*</span></dt>
                <dd>
                    <asp:TextBox runat="server" ID="txtPassword" CssClass="input_text" MaxLength="50"></asp:TextBox>

                </dd>
            </dl>
            <dl class="clearfix">
                <dt>URL/KEY<span>*</span></dt>
                <dd>
                    <asp:TextBox runat="server" ID="txtUrl" CssClass="input_text" TextMode="MultiLine" Height="40"></asp:TextBox>
                </dd>
            </dl>
            <dl class="clearfix">
                <dt>请求方式<span>*</span></dt>
                <dd>
                    <asp:DropDownList runat="server" ID="ddlMethod" CssClass="input_text">
                        <asp:ListItem Value="POST">POST</asp:ListItem>
                        <asp:ListItem Value="GET">GET</asp:ListItem>
                    </asp:DropDownList>                    
                </dd>
            </dl>
            <dl class="clearfix">
                <dt>签名</dt>
                <dd>
                    <asp:TextBox runat="server" ID="txtContent" CssClass="input_text" TextMode="MultiLine" Height="60"></asp:TextBox>
                    <p class="pb10">例：【xx平台】</p>
                </dd>
            </dl>
            <dl class="clearfix">
                <dt>是否启用</dt>
                <dd>
                    <asp:RadioButton ID="rbtisLast1" value="1" GroupName="isLast" ClientIDMode="Static"
                        Text="是" Checked="true" runat="server" /><asp:RadioButton ID="rbtisLast0" GroupName="isLast" ClientIDMode="Static"
                            value="0" Text="否" runat="server" />
                </dd>
            </dl>
            <dl class="clearfix">
                <dt>手机号</dt>
                <dd>
                    <asp:TextBox runat="server" ID="txtMobile" CssClass="input_text"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt></dt>
                <dd>
                    <asp:Button ID="btnSubmit" runat="server" Text="确定" CssClass="input_btn" OnClick="btnSubmit_Click" />
                </dd>
            </dl>
        </div>
    </form>
</body>
</html>
