<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerfectInformation.aspx.cs" Inherits="Rc.Cloud.Web.RE_Register.PerfectInformation" %>

<%@ Register Src="~/control/header.ascx" TagPrefix="uc1" TagName="header" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="renderer" content="webkit" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Rc.Common.ConfigHelper.GetConfigString("WebSiteName") %>-完善信息</title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/function.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../js/function.js"></script>
    <script type="text/javascript">
        $(function () {
            userpost = "T";
            LaodGrade();
            LaodClass();
            $(".ddlgrade").change(function () {
                LaodClass();
            })
            $(".ddlgrade1").change(function () {
                LaodClass();
            })
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                userpost = $(this).data('value');
                LaodGrade();
                LaodClass();
            })
            $("#btnSave").click(function () {
                if (userpost == "T") {
                    if ($("#ddlUserPost").val() == "-1" || $("#ddlUserPost").val() == "") {
                        layer.msg("请选择职务", { icon: 2, time: 2000 }, function () {
                            $("#ddlUserPost").focus();
                        });
                        return false;
                    }
                }
                var _classId = "";
                if (userpost == "T") {
                    _classId = $(".ddlclass").val();
                }
                else {
                    _classId = $(".ddlclass1").val();
                }
                var dto = {
                    UserId: "<%=UserId%>",
                    UserPost: $("#ddlUserPost").val(),
                    Subject: $("#ddlSubject").val(),
                    ClassId: _classId,
                    type: userpost,
                    x: Math.random()
                };

                $.ajaxWebService("PerfectInformation.aspx/loginIndex", JSON.stringify(dto), function (data) {
                    var json = $.parseJSON(data.d);
                    if (json.err == "null") {
                        layer.load();
                        location.href = json.iurl;
                    }
                    else {
                        layer.msg(json.err, { icon: 2, time: 2000 });
                    }
                }, function () {
                    layer.msg('登录异常', { icon: 2, time: 2000 });
                });
            })
        })
        //加载年级
        var LaodGrade = function () {
            var dto = {
                SchoolId: "<%=SchoolId%>",
                x: Math.round()
            };
            $.ajaxWebService("PerfectInformation.aspx/LaodGrade", JSON.stringify(dto), function (data) {
                if (userpost == "" || userpost == "T") {
                    $(".ddlgrade").html(data.d);
                }
                else {
                    $(".ddlgrade1").html(data.d);
                }
            }, function () {
                layer.msg('异常', { icon: 2, time: 2000 });
            }, false);
        }
        //加载班级
        var LaodClass = function () {
            var gradeid = "";
            if (userpost == "" || userpost == "T") {
                gradeid = $(".ddlgrade").val();
            }
            else {
                gradeid = $(".ddlgrade1").val();
            }
            var dto = {
                GradeId: gradeid,
                x: Math.round()
            };
            $.ajaxWebService("PerfectInformation.aspx/LaodClass", JSON.stringify(dto), function (data) {
                if (userpost == "" || userpost == "T") {
                    $(".ddlclass").html(data.d);
                }
                else {
                    $(".ddlclass1").html(data.d);
                }
            }, function () {
                layer.msg('异常', { icon: 2, time: 2000 });
            });
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="index_panel pt">
            <uc1:header runat="server" ID="header" />
            <div class="register_panel form-horizontal">
                <div class="form-group">
                    <div class="col-xs-5 col-xs-offset-2">
                        <ul class="nav nav-tabs" id="UserPost">
                            <li class="active"><a href="#teacher" data-toggle="tab" data-value="T">老师</a></li>
                            <li><a href="#student" data-toggle="tab" data-value="S">学生</a></li>
                        </ul>
                    </div>
                </div>
                <div class="tab-content">
                    <%-- 老师 --%>
                    <div class="tab-pane active" id="teacher">
                        <div class="form-group">
                            <div class="control-label col-xs-2">职务</div>
                            <div class="col-xs-5">
                                <asp:DropDownList ID="ddlUserPost" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-xs-4 text-muted form-control-static"></div>
                        </div>
                        <div class="form-group">
                            <div class="control-label col-xs-2">学科</div>
                            <div class="col-xs-5">
                                <asp:DropDownList ID="ddlSubject" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-xs-4 text-muted form-control-static"></div>
                        </div>
                        <div class="form-group">
                            <div class="control-label col-xs-2">年级</div>
                            <div class="col-xs-5">
                                <select class="form-control ddlgrade">
                                </select>
                            </div>
                            <div class="col-xs-4 text-muted form-control-static"></div>
                        </div>
                        <div class="form-group">
                            <div class="control-label col-xs-2">班级</div>
                            <div class="col-xs-5">
                                <select class="form-control ddlclass"></select>
                            </div>
                            <div class="col-xs-4 text-muted form-control-static"></div>
                        </div>
                    </div>
                    <%-- 学生 --%>
                    <div id="student" class="tab-pane">
                        <div class="form-group">
                            <div class="control-label col-xs-2">年级</div>
                            <div class="col-xs-5">
                                <select class="form-control ddlgrade1"></select>
                            </div>
                            <div class="col-xs-4 text-muted form-control-static"></div>
                        </div>
                        <div class="form-group">
                            <div class="control-label col-xs-2">班级</div>
                            <div class="col-xs-5">
                                <select class="form-control ddlclass1"></select>
                            </div>
                            <div class="col-xs-4 text-muted form-control-static"></div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-5 col-xs-offset-2">
                        <input type="button" id="btnSave" value="保存" class="btn btn-primary" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
