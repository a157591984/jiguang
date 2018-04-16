<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassCorrectDetail.aspx.cs" Inherits="Rc.Cloud.Web.Principal.ClassCorrectDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>班级批改详情</title>
    <link href="../css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="pa">
            <table class="table table-bordered text-center">
                <thead>
                    <tr>
                        <th>班级</th>
                        <th>任课老师</th>
                        <th>已交/未交</th>
                        <th>已批/未批</th>
                        <th>是否完成</th>
                        <th>完成时间</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody id="tb1">
                </tbody>
            </table>
            <!-- 翻页 -->
            <div class="pagination_container">
                <ul class="pagination">
                </ul>
            </div>
        </div>
        <textarea id="template_1" class="hidden">
        {#foreach $T.list as record}
        <tr>
            <td>{$T.record.ClassName}</td>
            <td class="align_left">{$T.record.TeacherName}</td>
            <td>{$T.record.TJ}</td>
            <td>{$T.record.PG}</td>
            <td>{$T.record.IsFinish}</td>
            <td>{$T.record.HomeWork_FinishTime}</td>
             {#if $T.record.TJData=="0"}
            <td>
            <a href='##' class="text-muted">数据分析</a></td>
        {#else}
            <td>
              <a href="../Evaluation/CheckClassStatsHelper.aspx?HomeWork_ID={$T.record.HomeWork_ID}&HomeWork_Name={$T.record.HomeWork_Name}&ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&ClassID={$T.record.ClassID}&TeacherID={$T.record.TeacherID}&ClassName={$T.record.ClassName}&IsTeacherData=1" target="_blank">数据分析</a></td>
        {#/if}
        </tr>
        {#/for}
        </textarea>
    </form>
    <script src="../js/jquery.min-1.8.2.js"></script>
    <script src="../js/function.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../plugin/laydate/laydate.js"></script>
    <script type="text/javascript" src="../js/json2.js"></script>
    <script type="text/javascript" src="../js/jq.pagination.js"></script>
    <script type="text/javascript" src="../js/jquery-jtemplates.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            $('[data-name="subject"] a').live({
                click: function () {
                    pageIndex = 1;
                    loadData();
                }
            })
            $('#btnSearch').click(function () {
                loadData();
                pageIndex = 1;
            })

            $(document).keydown(function (e) {
                if (e.keyCode == 13) {
                    $('#btnSearch').click();
                    return false;
                }
            })
            loadData();
        })
        var loadData = function () {
            var dto = {
                GradeId: "<%=GradeId%>",
                SubjectId: "<%=SubjectId%>",
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("AnalysisClassList.aspx/GetEachHWAnalysis", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template_1", null, { filter_data: false });
                    $("#tb1").processTemplate(json);
                    $(".pagination_container").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tb1").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $(".pagination_container").find("ul").html("");
                }
                if (json.list == null || json.list == "") {
                    pageIndex--;
                    if (pageIndex > 0) {
                        loadData();
                    }
                    else {
                        pageIndex = 1;
                    }
                }
            }, function () { });
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
    </script>
</body>
</html>
