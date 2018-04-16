<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommentCountdown.aspx.cs" Inherits="Rc.Cloud.Web.teacher.CommentCountdown" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>讲评</title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../js/function.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script src="../SysLib/js/jq.pagination.js"></script>
    <script src="../SysLib/js/json2.js"></script>
    <script type="text/javascript">
        $(function () {
            var endTime = new Date("<%=strEndTime%>");
            var intMinutes = parseInt("<%=intTimeLength%>");
            var isCountdown = "<%=isCountdown%>";
            endTime.setMinutes(endTime.getMinutes() + intMinutes);
            funInterval = setInterval(function () { ShowCountDown(endTime); }, 1000);
            if (isCountdown == "0") {
                $('#divCountdown').hide()
            }

            funIntervalLoadStu = setInterval(loadStu, 5000);

        });

        var loadStu = function () {
            var dto = {
                hwId: "<%=hwId%>",
                x: Math.random()
            }
            $.ajaxWebService("CommentCountdown.aspx/GetStuSubmitInfo", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("[data-name='divStu']").setTemplateElement("template_Stu", null, { filter_data: false });
                    $("[data-name='divStu']").processTemplate(json);
                    if (json.CommittedCount > 0) {
                        $("#btnComment").attr("disabled", false);
                    }
                    if (json.UnCommittedCount == 0) {
                        clearInterval(funIntervalLoadStu);
                    }
                    $("#ltlStu_Committed").html("已交" + json.CommittedCount + "位同学");
                    $("#ltlStu_UnCommitted").html("未交" + json.UnCommittedCount + "位同学");
                }
                else {
                    //$("[data-name='divStu']").html("暂无数据");
                }
            }, function () {
                clearInterval(funIntervalLoadStu);
                layer.ready(function () {
                    layer.msg('加载试题异常', { icon: 4 })
                });
            });
        }

        function ShowCountDown(EndTime) {
            var NowTime = new Date();
            var t = EndTime.getTime() - NowTime.getTime();
            var d = 0;
            var h = 0;
            var m = 0;
            var s = 0;
            if (t >= 0) {
                d = Math.floor(t / 1000 / 60 / 60 / 24);
                h = Math.floor(t / 1000 / 60 / 60 % 24);
                m = Math.floor(t / 1000 / 60 % 60);
                s = Math.floor(t / 1000 % 60);
            }

            var strTime = "";
            if (d > 0) strTime += d + "天";
            if (h > 0) strTime += h + "时";
            if (m > 0) strTime += m + "分钟";
            if (s > 0) strTime += s + "秒";
            $('#divCountdown span').html(strTime);

            if (d == 0 && h == 0 && m == 0 && s == 0) {
                clearInterval(funInterval);
                $('#divCountdown').hide()
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="comment_countdown">
            <div class="mb" id="divCountdown">
                倒计时：<span class="text-danger"></span>
            </div>
            <div class="mb" id="div">
                <asp:Label runat="server" ID="ltlStu_Assign"></asp:Label>，
                <asp:Label runat="server" ID="ltlStu_Committed"></asp:Label>，
                <asp:Label runat="server" ID="ltlStu_UnCommitted"></asp:Label>
            </div>
            <div class="mb">
                已交作业的前十名
            </div>
            <div class="mb">
                <div class="row" data-name="divStu">
                    <asp:Repeater runat="server" ID="rptStu">
                        <ItemTemplate>
                            <div class="col-xs-2-5"><%#Eval("StuName") %></div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <asp:Button runat="server" ID="btnComment" CssClass="btn btn-block btn-lg btn-info" Text="开始讲评" OnClick="btnComment_Click" />
        </div>
        <textarea id="template_Stu" class="hidden">
        {#foreach $T.list as record}
            <div class="col-xs-2-5">{$T.record.StuName}</div>
        {#/for}
        </textarea>
    </form>
</body>
</html>
