<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DictRelation_Add.aspx.cs" Inherits="Rc.Cloud.Web.Sys.DictRelation_Add" %>

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
    <script src="../SysLib/js/function.js"></script>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            //$("#ddlSon").click(function () {
            //    var val = $("#ddlHead").val();
            //    $("#ddlSon option[value='" + val + "']").remove();   //删除Option 
            //})
            $("#btnSave").click(function () {
                if ($("#ddlHead").val() == $("#ddlSon").val()) {
                    layer.msg('不能关联相同关系', { icon: 2, time: 2000 });
                    return false;
                }
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>字典名称</label>
                <asp:DropDownList ID="ddlHead" CssClass="form-control input-sm" runat="server" ClientIDMode="Static">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>对应关系字典名称</label>
                <asp:DropDownList ID="ddlSon" CssClass="form-control input-sm" runat="server" ClientIDMode="Static">
                </asp:DropDownList>
            </div>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="保存" OnClick="btnSave_Click" />
        </div>
    </form>
</body>
</html>
