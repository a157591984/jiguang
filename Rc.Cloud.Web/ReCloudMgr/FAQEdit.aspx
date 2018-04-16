<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="FAQEdit.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FAQEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script type="text/javascript" charset="utf-8" src="../SysLib/plugin/Kindeditor/kindeditor-all-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../SysLib/plugin/Kindeditor/lang/zh-CN.js"></script>
    <script type="text/javascript" charset="utf-8" src="../SysLib/plugin/Kindeditor/plugins/code/prettify.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script type="text/javascript">
        var intro;
        KindEditor.ready(function (K) {
            intro = K.create('#txt_content', {});
        });
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            $("#btnSave").click(function () {
                if ($("#txt_title").val() == "") {
                    layer.ready(function () {
                        layer.msg('请填写标题', { icon: 4 }, function () { $("#txt_title").focus(); })
                    });
                    return false;
                }
                if (intro.html() == "") {
                    layer.ready(function () {
                        layer.msg('请填写内容', { icon: 4 }, function () { $("#txt_content").focus(); })
                    });
                    return false;
                }
                return true;

            })
        })
    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>标题</label>
                <asp:TextBox ID="txt_title" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>内容</label>
                <textarea id="txt_content" style="width: 100%; height: 350px; visibility: hidden;" runat="server" clientidmode="Static"></textarea>
            </div>
            <asp:Button runat="server" ID="btnSave" Text="保存" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
    </form>
</body>
</html>
