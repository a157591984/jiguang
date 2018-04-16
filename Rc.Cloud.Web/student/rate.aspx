<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rate.aspx.cs" Inherits="Rc.Cloud.Web.student.rate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>评价</title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/raty/lib/jquery.raty.min.js"></script>
    <script src="../Scripts/plug-in/layer/layer.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="form-horizontal rate_panel">
                <div class="form-group">
                    <div class="col-xs-2 control-label">满意度：</div>
                    <div class="col-xs-2 form-control-static">
                        <div id="star" data-score="5"></div>
                        <asp:HiddenField ID="hidscore" runat="server" />
                    </div>
                    <div class="col-xs-3 form-control-static text-success" id="hint"></div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 control-label">评价：</div>
                    <div class="col-xs-8">
                        <textarea rows="4" class="form-control" id="txtcomment" runat="server" placeholder="填写评论内容"></textarea>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 col-xs-offset-2">
                        <asp:Button runat="server" ID="btnSubmit" Text="提交" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
    <link href="../Scripts/plug-in/layer/skin/layer.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $('#star').raty({
                path: '../plugin/raty/lib/img',
                target: '#hint',
                targetType: 'number',
                targetKeep: true,
                targetText: '5',
                targetFormat: '{score}分',
                score: function () {
                    return $(this).data('score');
                }
            });
            $("#txtcomment").bind({
                blur: function () { if (this.value.length > 200) this.value = this.value.slice(1, 200) },
                keyup: function () { if (this.value.length > 200) this.value = this.value.slice(1, 200) }
            });
            //弹窗标记
            index = parent.layer.getFrameIndex(window.name);
            $("#btnSubmit").click(function () {
                $("#hidscore").val($("input[name='score']").val());
                if ($.trim($("#txtcomment").val()) == "") {
                    layer.msg('请输入评论内容', { icon: 4, time: 1000 }, function () { $("#txtcomment").focus(); });
                    return false;
                }
            })
        })
    </script>
</body>
</html>
