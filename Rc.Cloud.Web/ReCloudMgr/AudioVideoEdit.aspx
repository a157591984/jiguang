<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AudioVideoEdit.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.AudioVideoEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            $("#txtBookName").bind({
                blur: function () { if (this.value.length > 30) this.value = this.value.slice(0, 30) },
                keyup: function () { if (this.value.length > 30) this.value = this.value.slice(0, 30) }
            });
            $("#btnSubmit").click(function () {
                if ($("#ddlGradeTerm").val() == "-1") {
                    layer.ready(function () {
                        layer.msg("请选择年级学期", { icon: 4 }, function () { $("#ddlGradeTerm").focus(); });
                    });
                    return false;
                }
                if ($("#ddlSubject").val() == "-1") {
                    layer.ready(function () {
                        layer.msg("请选择学科", { icon: 4 }, function () { $("#ddlSubject").focus(); });
                    });
                    return false;
                }
                if ($.trim($("#txtBookName").val()) == "") {
                    layer.ready(function () {
                        layer.msg("请填写书名", { icon: 4 }, function () { $("#txtBookName").focus(); });
                    });
                    return false;
                }
            });
        });

    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>年份</label>
                <asp:DropDownList ID="ddlYear" CssClass="form-control" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>年级学期 <span class="text-danger">*</span></label>
                <asp:DropDownList ID="ddlGradeTerm" CssClass="form-control" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>教材版本</label>
                <asp:DropDownList ID="ddlResource_Version" CssClass="form-control" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>学科 <span class="text-danger">*</span></label>
                <asp:DropDownList ID="ddlSubject" CssClass="form-control" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>书本名称 <span class="text-danger">*</span></label>
                <asp:TextBox runat="server" ID="txtBookName" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </div>

    </form>
</body>
</html>
