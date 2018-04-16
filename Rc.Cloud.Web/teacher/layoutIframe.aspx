<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="layoutIframe.aspx.cs" Inherits="Homework.teacher.layoutIframe" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="renderer" content="webkit" />
    <title><%=Rc.Common.ConfigHelper.GetConfigString("WebSiteName") %></title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../js/common.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../plugin/laydate/laydate.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnSubmit").click(function () {
                var arrChkVal = new Array();
                $("input[name^='stuId']:checked").each(function () {
                    arrChkVal.push($(this).val());
                });
                if (arrChkVal.length == 0) {
                    layer.msg("请选择学生", { icon: 4 });
                    return false;
                }
                if ($.trim($("#txtHomeWork_Name").val()) == "") {
                    layer.msg("请填写作业名称", { icon: 4 });
                    return false;
                }
                if ($.trim($("#txtBeginTime").val()) == "") {
                    layer.msg("请选择起始时间", { icon: 4 });
                    return false;
                }
                if ($.trim($("#txtStopTime").val()) == "") {
                    layer.msg("请选择截止时间", { icon: 4 });
                    return false;
                }
                if (new Date($("#txtBeginTime").val().replace("-", "/").replace("-", "/")) > new Date($("#txtStopTime").val().replace("-", "/").replace("-", "/"))) {
                    layer.msg("起始时间不能大于截止时间", { icon: 4 });
                    return false;
                }
                if (parseInt($("#txtTimeLength").val()) <= 0) {
                    layer.msg("请填写有效时长", { icon: 4 });
                    return false;
                }
                arrChkVal.join(",");
                $("#hidStudentId").val(arrChkVal);
                var index = layer.load();

            });

            $('#txtBeginTime').val(laydate.now(0, 'YYYY-MM-DD hh:mm:ss'));
            $('#txtStopTime').val(
                laydate.now(+1) + ' 02:00:00'
            );
            var starTime = {
                elem: '#txtBeginTime',
                format: 'YYYY-MM-DD hh:mm:ss',
                istime: true, //是否开启时间选择
                min: laydate.now(),//设定最小日期为当前日期
                choose: function (datas) {
                    endTime.min = datas; //开始日选好后，重置结束日的最小日期
                    endTime.start = datas //将结束日的初始值设定为开始日
                }
            };
            var endTime = {
                elem: '#txtStopTime',
                format: 'YYYY-MM-DD hh:mm:ss',
                istime: true, //是否开启时间选择
                min: laydate.now(0, 'YYYY-MM-DD hh:mm:ss'),//设定最小日期为当前日期
                choose: function (datas) {
                    starTime.max = datas; //结束日选好后，重置开始日的最大日期
                }
            };
            laydate(starTime);
            laydate(endTime);

            //作业OR考试
            $('input:radio[name="hwType"]').on('click', function () {
                var index = parseInt($(this).val()) - 1;
                $('[data-name="hwType"]:eq(' + index + ')').removeClass('hidden').siblings('[data-name="hwType"]').addClass('hidden');
            })
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid ph assign_hw_cont">
            <h5>选择学生<label class="pull-right" title="<%=className %>">
                <input id="Checkbox1" name="checkAll" type="checkbox" data-name="CheckAll" data-mark="stuId"><%=className %></label>
                <%--<asp:HiddenField runat="server" ID="hidUGroupId" Value='<%#Eval("UserGroup_Id") %>' />--%></h5>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <asp:Repeater runat="server" ID="rptStudentList">
                            <ItemTemplate>
                                <div class="col-xs-2">
                                    <label>
                                        <input id="Checkbox2" name="stuId[]" type="checkbox" value='<%#Eval("User_Id") %>' parentid='<%#Eval("UserGroup_Id") %>' /><%#string.IsNullOrEmpty(Eval("TrueName").ToString())?Eval("UserName"):Eval("TrueName") %></label>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label>作业名称</label>
                <asp:TextBox ID="txtHomeWork_Name" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group hw_type">
                <label class="mr">
                    <input type="radio" name="hwType" runat="server" checked id="hwType_1" value="1">作业</label>
                <label>
                    <input type="radio" name="hwType" runat="server" id="hwType_2" value="2">考试</label>
            </div>
            <div class="form-group">
                <label>
                    <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkIsShowAnswer" />
                    学生提交后立即显示正确答案</label>
            </div>
            <div class="row hw_time">
                <div class="col-xs-4">
                    <div class="form-group">
                        <label>开始时间</label>
                        <asp:TextBox ID="txtBeginTime" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                        <label>
                            <p class="help-block">
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkIsHide" />
                            在此时间前对学生隐藏作业</label></p>
                    </div>
                </div>
                <div class="col-xs-4" data-name="hwType">
                    <div class="form-group">
                        <label>截止时间</label>
                        <asp:TextBox ID="txtStopTime" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                        <p class="help-block"><span class="text-danger">超时后不可提交</span></p>
                    </div>
                </div>
                <div class="col-xs-3 hidden" data-name="hwType">
                    <div class="form-group">
                        <label>时长</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtTimeLength" runat="server" ClientIDMode="Static" CssClass="form-control" onkeyup="this.value=this.value.replace(/\D/g,'');" MaxLength="3">60</asp:TextBox>
                            <div class="input-group-addon">分钟</div>
                        </div>
                        <p class="help-block"><span class="text-danger">超时后不可提交</span></p>
                    </div>
                </div>
            </div>
            <div class="text-right">
                <asp:HiddenField runat="server" ClientIDMode="Static" ID="hidStudentId" />
                <asp:Button ID="btnSubmit" runat="server" ClientIDMode="Static" Text="布置" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </form>
</body>
</html>
