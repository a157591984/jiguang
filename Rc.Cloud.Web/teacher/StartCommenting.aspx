<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartCommenting.aspx.cs" Inherits="Rc.Cloud.Web.teacher.StartCommenting" %>

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
    <script src="../js/function.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script src="../SysLib/js/jq.pagination.js"></script>
    <script src="../SysLib/js/json2.js"></script>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);

            if ($("#ddlClass").length > 0) {
                loadHW($("#ddlClass").val(), "<%=rtrfId%>");
            }
            $("#ddlClass").change(function () {
                loadHW($("#ddlClass").val(), "<%=rtrfId%>");
            });

        });
        var loadHW = function (classId, rtrfId) {
            var dto = {
                userId: "<%=userId%>",
                classId: classId,
                rtrfId: rtrfId,
                x: Math.random()
            }
            $.ajaxWebService("StartCommenting.aspx/GetHWList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("[data-name='tbHW']").setTemplateElement("template_HW", null, { filter_data: false });
                    $("[data-name='tbHW']").processTemplate(json);
                }
                else {
                    $("[data-name='tbHW']").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                }
            }, function () {
                layer.ready(function () {
                    layer.msg('加载作业异常', { icon: 4 })
                });
            });
        }
        var StartComment = function (hwId) {
            window.parent.location.href = "CommentCountdown.aspx?hwId=" + hwId;
        }

        function classDisband(hwid, rtrId) {
            var index = layer.confirm("确定要撤销作业吗？作业撤销后系统将清除学生已提交作业的所有相关数据！", { icon: 4, title: '提示' }, function () {
                layer.close(index);//关闭确认弹窗
                $.ajaxWebService("cHomework.aspx/DeleteHw", "{HomeWorkId:'" + hwid + "',x:'" + Math.random() + "'}", function (data) {
                    if (data.d == "1") {
                        loadHW($("#ddlClass").val(), "<%=rtrfId%>");
                    }
                    if (data.d == "2") {
                        layer.msg('撤销作业失败', { icon: 2 });
                        return false;
                    }

                }, function () {
                    layer.msg('撤销作业失败！', { icon: 2 });
                    return false;
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
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <td width="10%">NO.</td>
                        <td>题目</td>
                        <td width="20%">操作</td>
                    </tr>
                </thead>
                <tbody data-name="tbHW">
                </tbody>
            </table>
        </div>
        <textarea id="template_HW" class="hidden">
        {#foreach $T.list as record}
        <tr>
            <td>{$T.record.inum}</td>
            <td>{$T.record.topicNumbers}</td>
            <td>
                <a href="##" onclick="classDisband('{$T.record.HomeWork_Id}','{$T.record.ResourceToResourceFolder_Id}');">撤销</a>
                <a href="##" onclick="StartComment('{$T.record.HomeWork_Id}');">选择</a>
            </td>
        </tr>
        {#/for}
        </textarea>
    </form>
</body>
</html>
