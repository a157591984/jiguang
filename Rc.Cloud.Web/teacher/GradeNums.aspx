<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GradeNums.aspx.cs" Inherits="Rc.Cloud.Web.teacher.GradeNums" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>详情</title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/jquery-jtemplates.js"></script>
    <script src="../js/common.js"></script>
    <script src="../js/json2.js"></script>
    <script src="../js/jquery-jtemplates.js"></script>
    <script src="../js/base64.js"></script>
    <script src="../js/function.js"></script>
    <script src="../plugin/layer/layer.js"></script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="container-fluid ph">
            <div class="form-inline mb">
                <div class="form-group">
                    <label>名称</label>
                    <input type="text" name="" class="form-control input-sm" id="txtName" />
                </div>
                <input type="button" value="搜索" class="btn btn-primary btn-sm" id="btnSearch" />
            </div>
            <table class="table table-bordered text-center">
                <thead>
                    <tr>
                        <td>名称</td>
                        <td>身份</td>
                        <td>老师数</td>
                        <td>学生数</td>
                        <td>操作</td>
                    </tr>
                </thead>
                <tbody id="tbMember">
                </tbody>
            </table>
            <textarea id="template_Member" class="hidden">
            {#foreach $T.list as record}
            <tr>
                <td>{$T.record.UserGroup_Name}</td>
                <td>{$T.record.PostName}</td>
                <td>{$T.record.TeacherCount}</td>
                <td>{$T.record.StudentCount}</td>
                <td>
                    {#if $T.record.MembershipEnum=='classrc'}
                    <a href='##' data-name="ChangeLayerTitle" data-title="{$T.record.UserGroup_Name}({$T.record.UserGroup_Id})" val="{$T.record.UserGroup_Id}">班级成员</a>
                    <%--<a href='##'>查看评测</a>--%>
                    {#/if}
                </td> 
            </tr>
            {#/for}
            </textarea>
        </div>
    </form>
</body>
<script type="text/javascript">
    $(function () {
        //窗口索引
        var index = parent.layer.getFrameIndex(window.name);
        $(document).on({
            'click': function () {
                var LayerTitle = $(this).attr('data-title');
                parent.layer.title(LayerTitle, index);
                window.location.href = "TeacherNums.aspx?ugroupId=" + $.trim($(this).attr("val")) + "&backurl=" + backurl;
            }
        }, '[data-name="ChangeLayerTitle"]');
        parent.layer.title("<%=ugroupTitle%>", index);
        //parent.layer.title('标题变了',index);
        ////全部老师
        //$('[data-name="TeacherNums"]').on({
        //    "click": function () {
        //        //打开新窗口
        //        parent.layer.open({
        //            type: 2,
        //            title: '全部老师',
        //            area: ['580px', '360px'],
        //            content: 'TeacherNums.aspx',
        //            success: function () {
        //                //关闭父窗口
        //                parent.layer.close(index);
        //            }
        //        })
        //    }
        //});
        ////全部学生
        //$('[data-name="StudentNums"]').on({
        //    "click": function () {
        //        //打开新窗口
        //        parent.layer.open({
        //            type: 2,
        //            title: '全部学生',
        //            area: ['580px', '360px'],
        //            content: 'StudentNums.aspx',
        //            success: function () {
        //                //关闭父窗口
        //                parent.layer.close(index);
        //            }
        //        })
        //    }
        //});
        b = new Base64();
        loadParaFromLink();
        $("#btnSearch").click(function () { loadData(); });
        loadData();
    })

    var setBasicUrl = function () {
        basicUrl = "GradeNums.aspx?ugroupId=<%=ugroupId%>&uname=" + escape($("#txtName").val());
        backurl = b.encode(basicUrl + "&pageIndex=" + pageIndex);
    }
    var loadParaFromLink = function () {
        pageIndex = getUrlVar("pageIndex") == "" ? 1 : getUrlVar("pageIndex");
        $("#txtName").val(unescape(getUrlVar("uname")));
    }

    var loadData = function () {
        setBasicUrl();
        var dto = {
            ClassName: $("#txtName").val(),
            x: Math.random()
        };
        $.ajaxWebService("GradeNums.aspx/GetGradeList", JSON.stringify(dto), function (data) {
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
