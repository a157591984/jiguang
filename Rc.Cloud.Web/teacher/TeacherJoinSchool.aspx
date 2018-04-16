<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherJoinSchool.aspx.cs" Inherits="Rc.Cloud.Web.teacher.TeacherJoinSchool" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatibale" content="ie=edge,chrome=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>加入学校</title>
    <script type="text/javascript" src="../Scripts/js001/jquery.min-1.8.2.js"></script>
    <link href="../Styles/style001/Style.css" rel="stylesheet" />
    <link rel="stylesheet" href="./../Styles/style001/LayerForm.css" />
    <script src="../Scripts/plug-in/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#txtUser_ApplicationReason").bind({
                keyup: function () { this.value = this.value.slice(0, 100); },
                blur: function () { this.value = this.value.slice(0, 100); }
            });
            $("#btnBack").click(function () { window.location.href = "classList.aspx"; });

            $("#btnSubmit").click(function () {
                if ($.trim($("#txtUserGroup_Id").val()) == "") {
                    layer.msg("学校号不能为空", { time: 1000, icon: 2 });
                    return false;
                }
                if ($.trim($("#txtUser_ApplicationReason").val()) == "") {
                    layer.msg("验证消息不能为空", { time: 1000, icon: 2 });
                    return false;
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="default_form_box form_box">
            <dl class="clearfix">
                <dt>学校号<span>*</span></dt>
                <dd>
                    <asp:TextBox ID="txtUserGroup_Id" runat="server" ClientIDMode="Static" CssClass="input_text" MaxLength="6"></asp:TextBox></dd>
            </dl>
            <dl class="clearfix">
                <dt>验证消息<span>*</span></dt>
                <dd>
                    <asp:TextBox ID="txtUser_ApplicationReason" runat="server" ClientIDMode="Static" TextMode="MultiLine" CssClass="textarea"></asp:TextBox></dd>
            </dl>
            <dl>
                <dt></dt>
                <dd>
                    <asp:Button ID="btnSubmit" runat="server" ClientIDMode="Static" CssClass="input_btn" Text="确定" OnClick="btnSubmit_Click" />
                </dd>
            </dl>
        </div>
    </form>
</body>
</html>
