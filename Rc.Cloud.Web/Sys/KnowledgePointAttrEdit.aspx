<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KnowledgePointAttrEdit.aspx.cs" Inherits="Rc.Cloud.Web.Sys.KnowledgePointAttrEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/auto-complete/css/style.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/auto-complete/js/AutoComplete.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            $("#txtS_KnowledgePointAttrValue").bind({
                blur: function () { this.value = this.value.replace(/\D/g, ''); },
                keyup: function () { this.value = this.value.replace(/\D/g, ''); }
            });

            $("#btnSubmit").click(function () {
                if ($.trim($("#txtS_KnowledgePointAttrValue").val()) == "") {
                    layer.ready(function () {
                        layer.msg("知识点属性值不能为空", { icon: 2, time: 1000 }, function () { $("#txtS_KnowledgePointAttrValue").focus(); });

                    })
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
                <label>知识点属性名称&nbsp;<span class="text-danger">*</span></label>
                <asp:DropDownList ID="ddlAttr" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group">
                <label>知识点属性值&nbsp;<span class="text-danger">*</span></label>
                <asp:TextBox runat="server" ID="txtS_KnowledgePointAttrValue" CssClass="form-control" MaxLength="2"></asp:TextBox>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
