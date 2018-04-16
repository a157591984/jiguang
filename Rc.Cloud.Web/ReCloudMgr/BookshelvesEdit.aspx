<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="BookshelvesEdit.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.BookshelvesEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/bootstrap-3.3.7/js/bootstrap.min.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/plugin/Kindeditor/kindeditor-all-min.js"></script>
    <script src="../SysLib/plugin/Kindeditor/lang/zh-CN.js"></script>
    <script src="../SysLib/plugin/Kindeditor/plugins/code/prettify.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>封皮</label>
                <div class="input-group">
                    <asp:FileUpload ID="fpBookImg_Url" runat="server" CssClass="form-control" ClientIDMode="Static" />
                    <div class="input-group-addon" data-toggle="popover">
                        <asp:Image ID="Image1" runat="server" ClientIDMode="Static" Width="20" Height="20" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label>图书价格</label>
                <asp:TextBox ID="txtBookPrice" runat="server" CssClass="form-control" ClientIDMode="Static" onkeyup="clearNoNum(this)" MaxLength="6"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>图书简介</label>
                <textarea id="BookBrief" style="width: 100%; height: 320px;" runat="server" clientidmode="Static"></textarea>
            </div>
            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-primary" OnClick="btnSave_Click" OnClientClick="return btnClient_Click()" />
        </div>
        <input type="hidden" runat="server" id="hidResourceFolder_Id" />
        <input type="hidden" runat="server" id="hidBook_Name" />
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $('[data-toggle="popover"]').popover({
            placement: 'left',
            content: '<img src="' + $('[data-toggle="popover"]').find('img').attr('src') + '" style="height:80px" alt="">',
            html: 'true',
            trigger: 'hover'
        })
    })
    var intro;
    KindEditor.ready(function (K) {
        intro = K.create('#BookBrief', {});
    });
    function btnClient_Click() {
        var result = true;
        if ($("#Image1").attr("src") == "" || $("#Image1").attr("src") == undefined) {
            if ($("#fpBookImg_Url").val() == "") {
                layer.ready(function () { layer.msg('请选择封皮', { icon: 4, time: 2000 }); });
                result = false;                
            }
        }
        if ($("#txtBookPrice").val() == "") {
            layer.ready(function () { layer.msg('请填写图书价格', { icon: 4, time: 2000 }); });
            result = false;
        }
        if (parseFloat($("#txtBookPrice").val()) > 9999.99) {
            layer.ready(function () { layer.msg('图书价格过大', { icon: 4, time: 2000 }); });
            result = false;
        }
        return result;
    }
    function clearNoNum(obj) {
        obj.value = obj.value.replace(/[^\d.]/g, "");  //清除“数字”和“.”以外的字符
        obj.value = obj.value.replace(/^\./g, "");  //验证第一个字符是数字而不是.
        obj.value = obj.value.replace(/\.{2,}/g, "."); //只保留第一个. 清除多余的.
        obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
    }
</script>
