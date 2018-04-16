﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="studentRegister.aspx.cs" Inherits="Homework.studentRegister" %>

<%@ Register Src="~/control/header.ascx" TagPrefix="uc1" TagName="header" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="renderer" content="webkit" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Rc.Common.ConfigHelper.GetConfigString("WebSiteName") %>-学生注册</title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/function.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../plugin/layer/layer.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="index_panel pt">
            <uc1:header runat="server" ID="header" />
            <div class="hidden">
                <asp:RadioButtonList ID="radButType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="学生" Value="S" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="老师" Value="T"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="register_panel form-horizontal">
                <div class="form-group">
                    <div class="col-xs-5 col-xs-offset-2">
                        <ul class="nav nav-tabs">
                            <li><a href="teacherRegister.aspx">老师注册</a></li>
                            <li class="active"><a href="javascript:;">学生注册</a></li>
                            <li><a href="parentRegister.aspx">家长注册</a></li>
                        </ul>
                    </div>
                </div>
                <div class="form-group">
                    <div class="control-label col-xs-2">登录名</div>
                    <div class="col-xs-5">
                        <asp:TextBox ID="txt_username" CssClass="form-control" runat="server" ClientIDMode="Static" MaxLength="50" AutoComplete="off"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 text-muted form-control-static">建议使用邮箱/手机号</div>
                </div>
                <div class="form-group">
                    <div class="control-label col-xs-2">用户名</div>
                    <div class="col-xs-5">
                        <asp:TextBox ID="txtTrueName" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="30"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 text-muted form-control-static"></div>
                </div>
                <div class="form-group">
                    <div class="control-label col-xs-2">密码</div>
                    <div class="col-xs-5">
                        <asp:TextBox ID="txt_password" TextMode="Password" CssClass="form-control" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 text-muted form-control-static">建议使用字母、数字组合，6-20个字符</div>
                </div>
                <div class="form-group">
                    <div class="control-label col-xs-2">确认密码</div>
                    <div class="col-xs-5">
                        <asp:TextBox ID="txt_confirmPassword" TextMode="Password" CssClass="form-control" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 text-muted form-control-static">再次输入密码</div>
                </div>
                <div class="form-group">
                    <div class="control-label col-xs-2">验证码</div>
                    <div class="col-xs-5">
                        <div class="input-group">
                            <input type="text" maxlength="4" runat="server" id="TxtVerify" class="form-control" />
                            <div class="input-group-addon">
                                <img src="../AdminHandler/ValidateCodeHandler.ashx?c=<%=DateTime.Now.ToString("O") %>" alt="验证码" id="imgValiRegister" onclick="GetCode()" title="点击更换验证码" align="absmiddle" />
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-4 text-muted form-control-static"></div>
                </div>
                <div class="form-group">
                    <div class="col-xs-offset-2 col-xs-5">
                        <asp:Button ID="btn_Save" CssClass="btn btn-primary btn-block" runat="server" Text="立即注册" OnClientClick="return check()" OnClick="btn_Save_Click" />
                        <p class="help-block">已有账号？<a href="/" class="text-primary">立即登录</a></p>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    $(function () {
        //检测浏览器类型
        var currBrowser = $.browser;

        //if (currBrowser.msie) {
        //    if (currBrowser.version == '6.0') {
        //        alert("您当前使用的浏览器版本过低，请升级到高级版本或使用其他浏览器");
        //    }
        //}
        $("#txt_username").focus();
        $("#txt_confirmPassword").blur(function () {
            if ($.trim($("#txt_password").val()) != $.trim($("#txt_confirmPassword").val())) {
                layer.msg("两次输入的密码不一致", { time: 1000, icon: 4 });
                return false;
            }
        });
        $("#txt_username,#txtTrueName").bind({
            blur: function () { this.value = js_validate.Filter(this.value); }
        });
        $("#txt_username").on({
            blur: function () {
                this.value = js_validate.Filter(this.value);
                var userName = $.trim($(this).val());

                if (userName != "") {
                    if (!js_validate.VerifyUserNameFormat(userName)) {
                        layer.msg("用户名格式不正确", { icon: 4, time: 1000 });
                        return false;

                    }
                    var ckMsg = CheckUserNameIsExist(userName);
                    if (ckMsg != "") {
                        layer.msg(ckMsg, { icon: 4, time: 1000 });
                        return false;
                    }
                }
            }
        });
    });

    function CheckUserNameIsExist(userName) {
        var temp = "";

        $.ajaxWebService("teacherRegister.aspx/CheckUserNameIsExist", "{userName:'" + userName + "',x:'" + Math.random() + "'}", function (data) {
            if (data.d == "1") {
                temp = "用户名已被注册,请重新输入。";
            }
        }, function () { temp = "用户名验证失败,请重新操作。"; }, false);
        return temp;
    }

    function check() {
        if ($.trim($("#txt_username").val()) == "") {
            layer.msg("请输入用户名", { time: 1000, icon: 4 });
            $("#txt_username").focus();
            return false;
        }
        if (!js_validate.VerifyUserNameFormat($.trim($("#txt_username").val()))) {
            layer.msg("用户名格式不正确", { icon: 4, time: 1000 })
            return false;
        }
        if ($.trim($("#txtTrueName").val()) == "") {
            layer.msg("请输入真实姓名", { time: 1000, icon: 4 });
            $("#txtTrueName").focus();
            return false;
        }
        //var regexChinese = /[\u4E00-\u9FA5]{2,8}/;
        if ($.trim($("#txt_password").val()) == "") {
            layer.msg("请输入密码", { time: 1000, icon: 4 });
            $("#txt_password").focus();
            return false;
        }
        if ($.trim($("#txt_confirmPassword").val()) == "") {
            layer.msg("请输入确认密码", { time: 1000, icon: 4 });
            $("#txt_confirmPassword").focus();
            return false;
        }
        if ($.trim($("#txt_confirmPassword").val()) != $.trim($("#txt_password").val())) {
            layer.msg("两次输入的密码不一致", { time: 1000, icon: 4 });
            $("#txt_confirmPassword").focus();
            return false;
        }
        if ($.trim($("#TxtVerify").val()) == "") {
            layer.msg("请输入验证码", { time: 1000, icon: 4 });
            $("#TxtVerify").focus();
            return false;
        }
        return true;
    }


    function GetCode() {
        var num = Math.random();
        var tempImg = document.getElementById("imgValiRegister");

        tempImg.src = "../AdminHandler/ValidateCodeHandler.ashx?by=register&t=" + num;
        return false;
    }

    //返回val的字节长度 
    function getByteLen(val) {
        var len = 0;
        for (var i = 0; i < val.length; i++) {
            if (val[i].match(/[^\x00-\xff]/ig) != null) //全角 
                len += 2;
            else
                len += 1;
        }
        return len;
    }
    //返回val在规定字节长度max内的值 
    function getByteVal(val, max) {
        var returnValue = '';
        var byteValLen = 0;
        for (var i = 0; i < val.length; i++) {
            if (val[i].match(/[^\x00-\xff]/ig) != null)
                byteValLen += 2;
            else
                byteValLen += 1;
            if (byteValLen > max)
                break;
            returnValue += val[i];
        }
        return returnValue;
    }
    $(function () {
        var _area = $('#txt_username');
        var _info = _area.next();
        var _max = _area.attr('maxlength');
        var _val;
        _area.bind('keyup change', function () { //绑定keyup和change事件 
            if (_info.find('span').size() < 1) {//避免每次弹起都会插入一条提示信息 
                _info.append(_max);
            }
            _val = $(this).val();
            _cur = getByteLen(_val);
            if (_cur == 0) {//当默认值长度为0时,可输入数为默认maxlength值 
                _info.text(_max);
            } else if (_cur < _max) {//当默认值小于限制数时,可输入数为max-cur 
                _info.text(_max - _cur);
            } else {//当默认值大于等于限制数时 
                _info.text(0);
                $(this).val(getByteVal(_val, _max)); //截取指定字节长度内的值 
            }
        });
    });
</script>
