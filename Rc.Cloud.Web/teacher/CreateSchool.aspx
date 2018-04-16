<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateSchool.aspx.cs" Inherits="Rc.Cloud.Web.teacher.CreateSchool" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>创建新学校</title>
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/jquery.min-1.11.1.js"></script>
    <script type="text/javascript" src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../plugin/layer/layer.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#txtClassName").on('blur', function () { this.value = js_validate.Filter(this.value); })
            $("#txtClassIntro").on('keyup blur', function () { this.value = this.value.slice(0, 2000); })

        });
        function Check() {
            if ($.trim($('#txtClassName').val()) == '') {
                layer.msg('学校名称不能为空！', { icon: 4, offset: '10px' });
                return false;
            }
            if ($.trim($('#txtClassIntro').val()) == '') {
                layer.msg('学校简介不能为空！', { icon: 4, offset: '10px' });
                return false;
            }
            layer.load(1, { shadeShadow: false, time: 0 });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pv pt">
            <div class="form-group">
                <label class="required">学校名称：</label>
                <asp:TextBox ID="txtClassName" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="30"></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="required">学校简介：</label>
                <asp:TextBox ID="txtClassIntro" Rows="8" runat="server" ClientIDMode="Static" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group text-right">
                <asp:Button ID="ButtonOK" runat="server" CssClass="btn btn-primary" Text="确定" OnClientClick="return Check();" OnClick="ButtonOK_Click" />
            </div>
        </div>
    </form>
</body>
</html>
