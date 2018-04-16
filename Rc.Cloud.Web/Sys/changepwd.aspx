<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changepwd.aspx.cs" Inherits="Rc.Cloud.Web.Sys.changepwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            $("#txt_confirmpassword").blur(function () {
                if ($.trim($("#txt_confirmpassword").val()) != $.trim($("#txt_newpassword").val())) {
                    layer.ready(function () {
                        layer.msg("两次输入的支付密码不一致", { icon: 4 });
                    })
                    return false;
                }

            });
            $("#btnSubmit").click(function () {

                //if ($.trim($("#txt_oldpassword").val()) == "") {
                //    layer.msg("请输入原始密码", { time: 1000, icon: 2 });
                //    $("#txt_oldpassword").focus();
                //    return false;
                //}

                if ($.trim($("#txt_newpassword").val()) == "") {
                    layer.ready(function () {
                        layer.msg("请输入新密码", { icon: 4 }, function () {
                            $("#txt_newpassword").focus();
                        });
                    })
                    return false;
                }
                if ($.trim($("#txt_confirmpassword").val()) == "") {
                    layer.ready(function () {
                        layer.msg("请输入确认新密码", { icon: 4 }, function () {
                            $("#txt_confirmpassword").focus();

                        });
                    })
                    return false;
                }
                if ($.trim($("#txt_newpassword").val()) != $.trim($("#txt_confirmpassword").val())) {
                    layer.ready(function () {
                        layer.msg("两次输入的支付密码不一致", { icon: 4 }, function () {
                            $("#txt_newpassword").focus();
                        });
                    })
                    return false;
                }
                return true;
            })
        })
        function CheckLoginPasswordIsExist(Password) {
            var temp = "";
            $.ajaxWebService("paypsw.aspx/CheckLoginPasswordIsExist", "{password:'" + Password + "',x:'" + Math.random() + "'}", function (data) {
                if (data.d == "1") {
                    temp = "登录密码输入有误,请重新操作。";
                }
            }, function () { temp = "登录密码输入有误,请重新操作。"; }, false);
            return temp;
        }
    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>新密码&nbsp;<span class="text-danger">*</span></label>
                <asp:TextBox ID="txt_newpassword" CssClass="form-control" TextMode="Password" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>确认密码</label>
                <asp:TextBox ID="txt_confirmpassword" CssClass="form-control" TextMode="Password" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
