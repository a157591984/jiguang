<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlatformTips.aspx.cs" Inherits="Rc.Web.Payment.PlatformTips" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>支付提示</title>
    <script src="../js/jquery.min-1.8.2.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <style type="text/css">
        .PayTips_ul li
        {
            height: 24px;
            line-height: 24px;
            list-style-type:none;
            font-family:'Microsoft YaHei';
        }

            .PayTips_ul li a
            {
                font-weight: bold;
                text-decoration: none;
                color: #325e7f;
            }
    </style>
    <script lang="ja" type="text/javascript">
        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }
        $(function () {
            var arrPara = getUrlVars();
            var type = (arrPara["orderType"] == undefined || arrPara["orderType"] == null || arrPara["orderType"] == "") ? "" : arrPara["orderType"];
            var redUrl = "../teacher/allOrder.aspx";
            if (type == "1") {//教师订单
                redUrl = "../teacher/allOrder.aspx";
            }
            else if (type == "2") {//学生订单
                redUrl = "../student/allOrder.aspx";
            }

            $("#suc").click(function () {
                parent.location.href = redUrl;
            });
            $("#err").click(function () { parent.layer.closeAll('iframe'); });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-top: 15px;">
            <ul class="PayTips_ul" style="padding-left:15px;">
                <li><span class="orange" style="font-size: 16px; font-weight: bold; color: #FF6600;">请您在新打开的页面中完成支付</span></li>
                <li>付款完成前请不要关闭此窗口！</li>
                <li>完成付款后请根据您的情况点击下面的按钮</li>
                <li style="padding-top: 5px; margin:0 auto;"><a href="javascript:;" id="suc">已完成支付</a>&nbsp;&nbsp;&nbsp;&nbsp;
                <a href="javascript:;" id="err">支付遇到问题</a> </li>
            </ul>
        </div>
    </form>
</body>
</html>
