<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="payment.aspx.cs" Inherits="Rc.Cloud.Web.teacher.payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/plug-in/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript">
        function userPayment(tp) {
            var _key = "bank";
            var _defaultbank = "";
            if (tp == 1) {//平台支付：支付宝、微信
                _key = "platform";
                _defaultbank = $("#platform input[type='radio']:checked").val();
            }
            else {//支付宝银行卡支付
                _defaultbank = $("#backCard input[type='radio']:checked").val();
            }
            if (tp == 1 && _defaultbank == "wxpay") {
                layer.open({
                    type: 2,
                    title: "微信支付",
                    closeBtn: 1,
                    area: ['260px', '310px'],
                    content: "../Payment/WxPayAPI/NativePayPage.aspx?rid=<%=enOrderNo %>",
                    cancel: function (index) {
                        layer.close(index);
                        clearInterval(funWXInterval);
                    }
                });
                funWXInterval = setInterval(verifyWXPayState, 2000);
            }
            else {
                if (_defaultbank == "alipay") { // 支付宝 扫码支付
                    layer.open({
                        type: 2,
                        title: "支付宝扫码支付",
                        closeBtn: 1,
                        area: ['260px', '310px'],
                        content: "../Payment/AlipayPayment.aspx?key=" + _key + "&rid=<%=enOrderNo %>" + "&defaultbank=" + _defaultbank,
                        success: function () {
                            $(".layui-layer-content").css("padding", "28px").find("iframe").css("height", "230px");
                        },
                        cancel: function (index) {
                            layer.close(index);
                            clearInterval(funAlipayInterval);
                        }
                    });
                    funAlipayInterval = setInterval(verifyAlipayState, 2000);
                }
                else { // 支付宝 银行卡支付
                    layer.open({
                        type: 2,
                        title: false,
                        closeBtn: 0,
                        area: ['350px', '140px'],
                        content: "../Payment/PlatformTips.aspx?orderType=<%=orderType%>" //iframe的url orderType:1教师 2学生
                    });
                    window.open("../Payment/AlipayPayment.aspx?key=" + _key + "&rid=<%=enOrderNo %>" + "&defaultbank=" + _defaultbank);
                }

            }
        }
        function payErrTips(msg) {
            layer.alert(msg, { closeBtn: 0 }, function () { window.history.back(); });
        }
        function verifyWXPayState() {
            $.get("../Payment/WxPayAPI/GetWxPayState.ashx", { rid: "<%=enOrderNo %>", x: new Date().getTime() },
                function (data) {
                    if (data == "success") {
                        clearInterval(funWXInterval);
                        layer.closeAll();
                        layer.msg("支付成功", { icon: 1, time: 2000 }, function () { window.location.href = "../teacher/allOrder.aspx"; })
                    }
                });
        }
        function verifyAlipayState() {
            $.get("../Payment/GetAlipayState.ashx", { rid: "<%=enOrderNo %>", x: new Date().getTime() },
                function (data) {
                    if (data == "success") {
                        clearInterval(funAlipayInterval);
                        layer.closeAll();
                        layer.msg("支付成功", { icon: 1, time: 2000 }, function () { window.location.href = "../teacher/allOrder.aspx"; })
                    }
                });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--子导航-->
    <%--  <div class="iframe-subnav">
        <ul class="subnav">
            <%--<li><a href="teachingPlan.aspx">已购买教案</a></li>
            <li><a href="allTeachingPlan.aspx">全部教案</a></li>
            <%--<li><a href="teaching.aspx">已购买习题集</a></li>
            <li><a href="allTeaching.aspx">全部习题集</a></li>
        </ul>
    </div>--%>
    <!--内容-->
    <div class="iframe-container">
        <div class="container pt">
            <div class="alert alert-warning order_info">
                <div class="row">
                    <div class="col-xs-10">
                        <h4>订单提交成功，请您尽快付款！订单号：<asp:Literal runat="server" ID="ltlUserOrder_No"></asp:Literal></h4>
                        <p>请您在提交订单后 24小时 内完成支付</p>
                    </div>
                    <div class="col-xs-2 text-right payable">
                        应付金额<span><asp:Literal runat="server" ID="ltlAmount"></asp:Literal></span>元
                    </div>
                </div>
            </div>

            <ul class="nav nav-tabs mb" id="myTab">
                <li role="presentation" class="active"><a href="#platform" data-toggle="tab">平台支付</a></li>
                <li role="presentation"><a href="#backCard" data-toggle="tab">银行卡支付</a></li>
            </ul>

            <div class="tab-content">
                <div role="tabpanel" class="tab-pane active" id="platform">
                    <div class="row payment_method">
                        <div class="col-xs-2 item">
                            <input id="platform_1" type="radio" name="bankIdPlatform" checked="checked" value="alipay">
                            <label for="platform_1">
                                <img src="./../../Images/bank/zfb.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                        <div class="col-xs-2 item">
                            <input id="platform_2" type="radio" name="bankIdPlatform" value="wxpay">
                            <label for="platform_2">
                                <img src="./../../Images/bank/wxzf.jpg" alt="" width="170" height="34" align="absmiddle"></label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4 col-xs-offset-4">
                            <input type="button" class="btn btn-primary btn-block btn-lg" value="立即支付" onclick="userPayment('1');" />
                        </div>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane" id="backCard">
                    <div class="row payment_method">
                        <div class="col-xs-2 item">
                            <input id="backCard_1" type="radio" name="bank" value="ICBCB2C" checked="checked">
                            <label for="backCard_1">
                                <img src="./../../Images/bank/gsyh.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                        <div class="col-xs-2 item">
                            <input id="backCard_2" type="radio" name="bank" value="ABC">
                            <label for="backCard_2">
                                <img src="./../../Images/bank/nyyh.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                        <div class="col-xs-2 item">
                            <input id="backCard_3" type="radio" name="bank" value="BOCB2C">
                            <label for="backCard_3">
                                <img src="./../../Images/bank/zgyh.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                        <div class="col-xs-2 item">
                            <input id="backCard_4" type="radio" name="bank" value="CCB">
                            <label for="backCard_4">
                                <img src="./../../Images/bank/jsyh.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                        <div class="col-xs-2 item">
                            <input id="backCard_5" type="radio" name="bank" value="COMM-DEBIT">
                            <label for="backCard_5">
                                <img src="./../../Images/bank/jtyh.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                        <div class="col-xs-2 item">
                            <input id="backCard_6" type="radio" name="bank" value="POSTGC">
                            <label for="backCard_6">
                                <img src="./../../Images/bank/yzcxyh.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                        <div class="col-xs-2 item">
                            <input id="backCard_7" type="radio" name="bank" value="CITIC-DEBIT">
                            <label for="backCard_7">
                                <img src="./../../Images/bank/zxyh.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                        <div class="col-xs-2 item">
                            <input id="backCard_8" type="radio" name="bank" value="CIB">
                            <label for="backCard_8">
                                <img src="./../../Images/bank/xyyh.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                        <div class="col-xs-2 item">
                            <input id="backCard_9" type="radio" name="bank" value="CEB-DEBIT">
                            <label for="backCard_9">
                                <img src="./../../Images/bank/gdyh.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                        <div class="col-xs-2 item">
                            <input id="backCard_10" type="radio" name="bank" value="CMBC">
                            <label for="backCard_10">
                                <img src="./../../Images/bank/msyh.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                        <div class="col-xs-2 item">
                            <input id="backCard_11" type="radio" name="bank" value="GDB">
                            <label for="backCard_11">
                                <img src="./../../Images/bank/gfyh.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                        <div class="col-xs-2 item">
                            <input id="backCard_12" type="radio" name="bank" value="CMB">
                            <label for="backCard_12">
                                <img src="./../../Images/bank/zsyh.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                        <div class="col-xs-2 item">
                            <input id="backCard_13" type="radio" name="bank" value="SPDB">
                            <label for="backCard_13">
                                <img src="./../../Images/bank/pfyh.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                        <div class="col-xs-2 item">
                            <input id="backCard_14" type="radio" name="bank" value="SPABANK">
                            <label for="backCard_14">
                                <img src="./../../Images/bank/payh.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                        <div class="col-xs-2 item">
                            <input id="backCard_15" type="radio" name="bank" value="BJBANK">
                            <label for="backCard_15">
                                <img src="./../../Images/bank/bjyh.jpg" alt="" width="170" height="34" align="absmiddle">
                            </label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4 col-xs-offset-4">
                            <input type="button" class="btn btn-primary btn-block btn-lg" value="立即支付" onclick="userPayment('2');" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>
