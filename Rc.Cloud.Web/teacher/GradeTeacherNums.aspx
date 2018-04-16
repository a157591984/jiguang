<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GradeTeacherNums.aspx.cs" Inherits="Rc.Cloud.Web.teacher.GradeTeacherNums" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>全部老师</title>
    <link rel="stylesheet" href="./../Styles/style001/Style.css" />
    <link href="../Styles/style001/pagination.css" rel="stylesheet" />
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="searchbar pt20 pb20 pl20 clearfix">
            <span>姓名</span>
            <input type="text" class="mr20" id="txtUserName" />
            <span>身份</span>
            <select id="ddlIdentity" class="mr20">
                <option value="">-请选择-</option>
                <option value="headmaster">班主任</option>
                <option value="teacher">代课老师</option>
            </select>
            <input type="button" value="搜索" class="input_btn pointer mr20" id="btnSearch" />
            <!-- <input type="button" onclick="javascript: history.back();" value="返回" class="input_btn pointer" /> -->
        </div>
        <table class="table_list teacher_nums_list">
            <thead>
                <tr>
                    <td>姓名</td>
                    <td>身份</td>
                    <td>学科</td>
                    <td>加入时间</td>
                    <td>学生数</td>
                    <!-- <td>操作</td> -->
                </tr>
            </thead>
            <tbody id="tbMember">
            </tbody>
        </table>
        <textarea id="template_Member" style="display: none">
        {#foreach $T.list as record}
        <tr>
            <td>{$T.record.UserName}</td>
            <td>{$T.record.MembershipEnumText}</td>
            <td>{$T.record.SubjectName}</td>
            <td>{$T.record.User_ApplicationPassTime}</td>
            <td>{$T.record.StudentCount}</td>
            <!-- <td class="table_opera">
                <a href="StudentNums.aspx?ugroupId={\$T.record.UserGroup_Id}">班级成员</a>
            </td> -->
        </tr>
        {#/for}
        </textarea>
    </form>
</body>
<script type="text/javascript" src="./../Scripts/js001/jquery.min-1.8.2.js"></script>
<script type="text/javascript" src="./../Scripts/plug-in/layer/layer.js"></script>
<script type="text/javascript" src="../scripts/function.js"></script>
<script type="text/javascript" src="../Scripts/json2.js"></script>
<script type="text/javascript" src="../Scripts/jquery-jtemplates.js"></script>
<script type="text/javascript">
    $(function () {
        //窗口索引
        var index = parent.layer.getFrameIndex(window.name);
        parent.layer.title('全部老师', index);
        $("#btnSearch").click(function () { loadData(); });
        loadData();
    });

    var loadData = function () {
        var dto = {
            UserName: $("#txtUserName").val(),
            UserIdentity: $("#ddlIdentity").val(),
            x: Math.random()
        };
        $.ajaxWebService("TeacherNums.aspx/GetTeacherList", JSON.stringify(dto), function (data) {
            var json = $.parseJSON(data.d);
            if (json.err == "null") {
                $("#tbMember").setTemplateElement("template_Member", null, { filter_data: false });
                $("#tbMember").processTemplate(json);
            }
            else {
                $("#tbMember").html("<tr><td colspan='7' align='center'>暂无数据</td></tr>");
            }
        }, function () { });
    }

</script>
</html>
