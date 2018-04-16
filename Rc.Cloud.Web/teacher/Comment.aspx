<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="Comment.aspx.cs" Inherits="Rc.Cloud.Web.teacher.Comment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../plugin/laydate/laydate.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container pa">
        <div class="container">
            <div class="filter">
                <div class="form-inline">
                    <div class="form-group">
                        <label>作业名称：</label>
                        <input type="text" id="txtHomeWork_Name" name="" class="form-control input-sm" clientidmode="Static">
                    </div>
                    <div class="form-group">
                        <label>布置时间：</label>
                        <input type="text" name="" id="StartTime" placeholder="开始时间" class="form-control input-sm" clientidmode="Static">
                        <input type="text" name="" id="EndTime" placeholder="结束时间" class="form-control input-sm" clientidmode="Static">
                    </div>
                    <input type="button" id="btnSearch" value="检索" class="btn btn-primary btn-sm" clientidmode="Static">
                </div>
                <div class="filter_section">
                    <div class="filter_row clearfix">
                        <span class="row_name">学科：</span>
                        <div class="row_item">
                            <ul ajax-name="Subject">
                                <asp:Literal ID="ltlSubject" runat="server" ClientIDMode="Static"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">班级：</span>
                        <div class="row_item">
                            <ul ajax-name="class">
                                <asp:Literal ID="ltlClass" runat="server"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>日期</th>
                        <th>讲评报告</th>
                        <th>班级</th>
                        <th>最高分</th>
                        <th>最低分</th>
                        <th>平均分</th>
                        <th>中位数</th>
                        <th>众数</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody id="tb1">
                </tbody>
            </table>
            <div class="pagination_container">
                <ul class="pagination"></ul>
            </div>
        </div>
    </div>
    <textarea id="template_1" class="hidden">
        {#foreach $T.list as record}
            <tr>
                <td>{$T.record.CreateTime}</td>
                <td>{$T.record.HomeWork_Name}讲评报告</td>
                <td>{$T.record.ClassName}</td>
                <td>{$T.record.HighestScore}</td>
                <td>{$T.record.LowestScore}</td>
                <td>{$T.record.AVGScore}</td>
                <td>{$T.record.Median}</td>
                <td>{$T.record.Mode}</td>
                <td>
                    <%--{#if $T.record.StatsClassHW_ScoreID !=""}
                    <a href="../Evaluation/CommentReport.aspx?ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&HomeWork_Id={$T.record.HomeWork_Id}" target="_blank">讲评报告</a>
                    {#else}
                    <a href="../Evaluation/CommentReportH.aspx?ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&StatsClassHW_ScoreID={$T.record.StatsClassHW_ScoreID}&HomeWorkId={$T.record.HomeWork_Id}" target="_blank">讲评报告</a>
                    {#/if}--%>
                    {#if $T.record.WTJ==1}
                    <a href="CheckCommentStatsHelper.aspx?ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&HomeWork_Name={$T.record.HomeWork_Name}&HomeWork_Id={$T.record.HomeWork_Id}" target="_blank">讲评报告</a>
                    <a href="CheckCommentStatsHelper.aspx?ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&HomeWork_Name={$T.record.HomeWork_Name}&HomeWork_Id={$T.record.HomeWork_Id}&pagetype=1" target="_blank">讲评概述</a>
                    {#else}
                    <a href='##' class="disabled">讲评报告</a>
                    <a href='##' class="disabled">讲评概述</a>
                    {#/if}
                </td>
            </tr>
        {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            //筛选
            $(document).on({
                click: function () {
                    if (!$(this).children("a").hasClass("disabled")) {
                        $(this).closest('ul').find('a').removeClass('active');
                        $(this).children('a').addClass('active');
                        if ($(this).closest('ul').attr('ajax-name') == 'Subject') {
                            loadClass();
                        }
                        else {
                            pageIndex = 1;
                            loadData();
                        }
                    }
                }
            }, '[ajax-name] li')
            // 日期
            var StarTime = {
                elem: '#StartTime',
                format: 'YYYY-MM-DD',
                choose: function (datas) {
                    EndTime.min = datas; //开始日选好后，重置结束日的最小日期
                    EndTime.start = datas //将结束日的初始值设定为开始日
                }
            }
            var EndTime = {
                elem: '#EndTime',
                format: 'YYYY-MM-DD',
                choose: function (datas) {
                    StarTime.max = datas; //结束日选好后，重置开始日的最大日期
                }
            }
            laydate(StarTime);
            laydate(EndTime);
            $('#btnSearch').click(function () {
                pageIndex = 1;
                loadData();
            })
            $(document).keydown(function (e) {
                if (e.keyCode == 13) {
                    $('#btnSearch').click();
                    return false;
                }
            })
            loadClass();

        })
        function loadClass() {
            var _subjectId = $('[ajax-name="Subject"]').find('a.active').attr('ajax-value');
            var _teacherId = $('[ajax-name="Subject"]').find('a.active').attr('ajax-TeacherId');
            var dto = {
                SubjectID: _subjectId,
                TeacherID: _teacherId,//"<%=UserId%>",
                x: Math.random()
            };
            $.ajaxWebService("Comment.aspx/GetClass", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $('[ajax-name="class"]').html(data.d);
                    pageIndex = 1;
                    loadData();
                }
            }, function () { }, false);

        }
        function loadData() {
            var _subjectId = $('[ajax-name="Subject"]').find('a.active').attr('ajax-value');
            var _teacherId = $('[ajax-name="Subject"]').find('a.active').attr('ajax-TeacherId');
            var dto = {
                HWName: $("#txtHomeWork_Name").val(),
                HomeWorkCreateTime: $("#StartTime").val(),
                HomeWorkFinishTime: $("#EndTime").val(),
                SubjectID: _subjectId,
                ClassID: $('[ajax-name="class"]').find('a.active').attr('ajax-value'),
                TeacherID: _teacherId,//"<%=UserId%>",
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("Comment.aspx/GetEachHWAnalysis", JSON.stringify(dto), function (data) {
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
        function pageselectCallback(page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
    </script>
</asp:Content>
