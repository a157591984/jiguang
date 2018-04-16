<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="NoticeBoardEdit.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.NoticeBoardEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/bootstrap-3.3.7/js/bootstrap.min.js"></script>
    <script src="../SysLib/plugin/datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../SysLib/plugin/datetimepicker/js/locales/bootstrap-datetimepicker.zh-CN.js"></script>
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
                    layer.msg('请填写标题', { time: 2000, icon: 2 }, function () { $("#txt_title").focus(); })
                    return false;
                }
                if (intro.html() == "") {
                    layer.msg('请填写内容', { time: 2000, icon: 2 }, function () { $("#txt_content").focus(); })
                    return false;
                }
                if ($("#txtStartTime").val() == "") {
                    layer.msg('请填写开始时间', { time: 2000, icon: 2 }, function () { $("#txtStartTime").focus(); })
                    return false;
                }
                if ($("#txtEndTime").val() == "") {
                    layer.msg('请填写结束时间', { time: 2000, icon: 2 }, function () { $("#txtEndTime").focus(); })
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
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-6">
                        <label>开始时间</label>
                        <input type="text" id="txtStartTime" runat="server" class="form-control" />
                    </div>
                    <div class="col-xs-6">
                        <label>结束时间</label>
                        <input type="text" id="txtEndTime" runat="server" class="form-control" />
                    </div>
                </div>
            </div>
            <asp:Button runat="server" ID="btnSave" Text="保存" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
    </form>
    <script>
        $(function () {
            $('#txtStartTime').datetimepicker({
                language: 'zh-CN',
                pickerPosition: 'top-right',
                autoclose: true,
            }).on('changeDate', function (e) {
                $('#txtEndTime').datetimepicker('setStartDate', $('#txtStartTime').val())
            });
            $('#txtEndTime').datetimepicker({
                language: 'zh-CN',
                pickerPosition: 'top-right',
                autoclose: true,
            }).on('changeDate', function (e) {
                $('#txtStartTime').datetimepicker('setEndDate', $('#txtEndTime').val())
            });
        })
    </script>
</body>
</html>
