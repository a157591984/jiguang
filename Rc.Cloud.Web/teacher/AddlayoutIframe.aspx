<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddlayoutIframe.aspx.cs" Inherits="Homework.teacher.AddlayoutIframe" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="renderer" content="webkit" />
    <title><%=Rc.Common.ConfigHelper.GetConfigString("WebSiteName") %></title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../js/common.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../plugin/laydate/laydate.js"></script>
    <script lang="ja" type="text/javascript">
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
                if ($("#txtStopTime").length > 0) {
                    if ($.trim($("#txtStopTime").val()) == "") {
                        layer.msg("请选择截止时间", { icon: 4 });
                        return false;
                    }
                }
                arrChkVal.join(",");
                $("#hidStudentId").val(arrChkVal);
                var index = layer.load();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid ph assign_hw_cont">
            <h5>选择学生
                <label class="pull-right" title="<%=className %>">
                    <input id="Checkbox1" name="checkAll" type="checkbox" data-name="CheckAll" data-mark="stuId"><%=className %></label>
            </h5>
            <div class="panel panel-default">
                <div class="panel-body continue_stu_list">
                    <div class="row" id="ddStu">
                        <asp:Repeater runat="server" ID="rptStudentList">
                            <ItemTemplate>
                                <div class="col-xs-2">
                                    <label <%#string.IsNullOrEmpty(Eval("HomeWork_Id").ToString())? "class='text-danger'":""%>>
                                        <input id="Checkbox2" name="stuId[]" <%#string.IsNullOrEmpty(Eval("HomeWork_Id").ToString())? "":"disabled"%> type="checkbox" value='<%#Eval("User_Id") %>' parentid='<%#Eval("UserGroup_Id") %>' /><%#string.IsNullOrEmpty(Eval("TrueName").ToString())?Eval("UserName"):Eval("TrueName") %></label>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4">
                    <div class="form-group">
                        <label>截止日期</label>
                        <asp:TextBox ID="txtStopTime" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                        <p class="help-block">
                            <span class="text-danger">超时后不可提交
                            </span>
                        </p>
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
<script lang="ja" type="text/javascript">
    $(function () {
        //$('#txtStopTime').val(laydate.now(0, 'YYYY-MM-DD hh:mm:ss'));
        if ($("#txtStopTime").length > 0) {
            var endTime = {
                elem: '#txtStopTime',
                format: 'YYYY-MM-DD hh:mm:ss',
                istime: true, //是否开启时间选择
                min: laydate.now(0, 'YYYY-MM-DD hh:mm:ss'),//设定最小日期为当前日期
            };
            laydate(endTime);
        }

        if ($("#ddStu label[class]").length == 0) {
            $("#btnSubmit").attr("value", "已全部布置").attr("disabled", "disabled");
        }
    });
</script>
</html>
