<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignExercise.aspx.cs" Inherits="Rc.Cloud.Web.teacher.AssignExercise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../plugin/nanoscroller/nanoscroller.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../js/common.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../plugin/nanoscroller/jquery.nanoscroller.min.js"></script>
    <script src="../js/function.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script src="../SysLib/js/jq.pagination.js"></script>
    <script src="../SysLib/js/json2.js"></script>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            $('.nano').nanoScroller();
            if ($("#ddlClass").length > 0) {
                loadTQ($("#ddlClass").val(), "<%=rtrfId%>");
            }
            $("#ddlClass").change(function () {
                loadTQ($("#ddlClass").val(), "<%=rtrfId%>");
            });
            $("#txtTimeLength").bind({
                blur: function () { this.value = this.value.replace(/\D/g, ""); },
                keyup: function () { this.value = this.value.replace(/\D/g, ""); }
            });

            idx = null;
            $("#btnSubmit").click(function () {
                var arrTQ = new Array();
                $('[data-name="divTQ"] input:checked:enabled').each(function () {
                    arrTQ.push($(this).attr("id") + "^" + $(this).attr("tnum"));
                });

                if (arrTQ.length == 0) {
                    layer.msg('请选择题目', { icon: 4, time: 1000 });
                    return false;
                }
                $("#hidTQ").val(arrTQ.join(","));

                if ($.trim($("#txtHomeWork_Name").val()) == "") {
                    layer.msg('请填写作业名称', { icon: 4, time: 1000 }, function () { $("#txtHomeWork_Name").focus(); });
                    return false;
                }
                if ($.trim($("#txtTimeLength").val()) == "") {
                    layer.msg('请填写时长', { icon: 4, time: 1000 }, function () { $("#txtTimeLength").focus(); });
                    return false;
                }
                idx = layer.load();
                $("#btnSubmit").hide();


            });
        });
        var loadTQ = function (classId, rtrfId) {
            var dto = {
                rtrfName: "<%=rtrfName%>",
                classId: classId,
                rtrfId: rtrfId,
                x: Math.random()
            }
            $.ajaxWebService("AssignExercise.aspx/GetTQList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("[data-name='divTQ']").setTemplateElement("template_TQ", null, { filter_data: false });
                    $("[data-name='divTQ']").processTemplate(json);
                    
                    $("#txtHomeWork_Name").val(json.hwName);
                }
                else {
                    $("[data-name='divTQ']").html("暂无数据");
                }
            }, function () {
                layer.ready(function () {
                    layer.msg('加载试题异常', { icon: 4 })
                });
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid ph">
            <div class="form-group">
                <label>选择班级</label>
                <asp:DropDownList runat="server" ID="ddlClass" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group">
                <label>选择题目</label>
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="nano question_num_list">
                            <div class="nano-content" data-name="divTQ">
                                <%--<input type="checkbox" name="" id="1" class="already">
                                        <label for="1">1</label>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label>作业名称</label>
                <asp:TextBox runat="server" ID="txtHomeWork_Name" CssClass="form-control" MaxLength="50"></asp:TextBox>
            </div>
            <div class="row">
                <div class="form-group col-xs-4">
                    <label>时长（分钟）</label>
                    <asp:TextBox runat="server" ID="txtTimeLength" CssClass="form-control" MaxLength="5">10</asp:TextBox>
                </div>
            </div>
            <div class="checkbox">
                <label>
                    <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkIsCountdown" Checked="true" />
                    讲评页面显示倒计时
                </label>
            </div>
            <asp:HiddenField runat="server" ID="hidTQ" />
            <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary" Text="确定" OnClick="btnSubmit_Click" />
        </div>
        <textarea id="template_TQ" class="hidden">
        {#foreach $T.list as record}
            <input type="checkbox" name="" id="{$T.record.TestQuestions_Id}" tnum="{$T.record.topicNumber}"  {#if $T.record.IsExists==1}class="already" disabled{#/if}>
            <label for="{$T.record.TestQuestions_Id}">{$T.record.topicNumber}</label>
        {#/for}
        </textarea>
    </form>
</body>
</html>
