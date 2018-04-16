<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/student.Master" AutoEventWireup="true" CodeBehind="oHistoryHomework.aspx.cs" Inherits="Homework.student.oHistoryHomework" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="oHomework.aspx">最新作业</a></li>
            <%if (Convert.ToBoolean(Rc.Common.ConfigHelper.GetConfigString("IsShowNewStuAnalysis")))
                { %>
            <li><a href="St_oHistoryHomework.aspx" class="active">历次作业</a></li>
            <%}
                else
                { %>
            <li><a href="oHistoryHomework.aspx" class="active">历次作业</a></li>
            <%} %>
            <li><a href="oWrongHomework.aspx">错题集</a></li>
            <li><a href="AchievementTrack.aspx">成绩跟踪</a></li>
            <%if (Convert.ToBoolean(Rc.Common.ConfigHelper.GetConfigString("IsShowNewStuAnalysis")))
                { %>
            <li><a href="CheckStuHWAnalysis.aspx">综合分析</a></li>
            <%} %>
        </ul>
    </div>
    <div class="iframe-container">
        <div class="container pt">
            <div class="filter">
                <div class="form-inline">
                    <div class="form-group">
                        <label>书目名称：</label>
                        <input class="form-control input-sm" id="txtBookName" type="text" clientidmode="Static">
                    </div>
                    <input class="btn btn-primary btn-sm" id="btnSearch" type="button" value="确定" clientidmode="Static">
                </div>
                <div class="filter_section">
                    <div class="filter_row clearfix">
                        <span class="row_name">学科：</span>
                        <div class="row_item">
                            <ul data-name="subject">
                                <asp:Literal ID="ltlSubjectName" runat="server"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-bordered text-center">
                <thead>
                    <tr>
                        <td width="14%">作业布置时间</td>
                        <td width="20%" class="text-left">作业名称</td>
                        <td width="23%" class="text-left">作业来源</td>
                        <td width="5%">学科</td>
                        <td width="8%">试卷总分</td>
                        <td width="8%">我的得分</td>
                        <td width="8%">班级排名</td>
                        <td>操作</td>
                    </tr>
                </thead>
                <tbody id="tbHW">
                </tbody>
            </table>
            <div class="pagination_container">
                <ul class="pagination">
                </ul>
            </div>
        </div>
    </div>
    <textarea id="template_HW" style="display: none">
    {#foreach $T.list as record}
    <tr>
        <td>{$T.record.MakeTime}</td>
        <td class="text-left">{$T.record.HomeWork_Name}</td>
        <td class="text-left">{$T.record.BookName}</td>
        <td>{$T.record.SubjectName}</td>
        <td>{$T.record.HWScore}</td>
        <td>{$T.record.StudentScore}</td>
        <td>{$T.record.StudentScoreOrder}</td>
        <td class="align_center table_opera"> 
            {#if $T.record.TJ=="0"}
            <a href='##' class="disabled" title="学生未提交">批改详情</a>  
            <a href='##' class="disabled" title="学生未提交">分析报告</a> 
            {#else}
            {#if $T.record.PG=="0"}
                {#if $T.record.CorrectMode=='1'}
                <a href="ohomeworkview_client.aspx?ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&HomeWork_Id={$T.record.HomeWork_Id}&StudentId={$T.record.Student_Id}&Student_HomeWork_Id={$T.record.Student_HomeWork_Id}" class="danger" target="_blank" title="老师未批改">批改详情</a> 
                {#else}
                <a href="oHomeWorkViewTT.aspx?ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&HomeWork_Id={$T.record.HomeWork_Id}&StudentId={$T.record.Student_Id}" class="danger" target="_blank" title="老师未批改">批改详情</a> 
                {#/if}
                <a href="##" class="disabled" title="老师未批改">分析报告</a> 
            {#else}
            <a href="CheckStudentStatsHelper.aspx?ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&HomeWork_Id={$T.record.HomeWork_Id}&StudentId={$T.record.Student_Id}&HomeWork_Name={$T.record.HomeWork_Name}&type=1&Student_HomeWork_Id={$T.record.Student_HomeWork_Id}&CorrectMode={$T.record.CorrectMode}" target="_blank">批改详情</a>
             <a href="CheckStudentStatsHelper.aspx?ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&HomeWork_Id={$T.record.HomeWork_Id}&StudentId={$T.record.Student_Id}&HomeWork_Name={$T.record.HomeWork_Name}&type=2&Student_HomeWork_Id={$T.record.Student_HomeWork_Id}&CorrectMode={$T.record.CorrectMode}" target="_blank">分析报告</a> 
            {#/if}
            {#/if}
        </td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        function ShowSubDocument(strDoctumentID, obj) {
            homeWork_AssignTeacher = strDoctumentID;
            $(".left_tree_list div").removeClass();
            $(obj).parent().addClass("active");

            pageIndex = 1;
            loadData();
        }
        var loadData = function () {
            var dto = {
                SubjectId: $('[data-name="subject"]').find('a.active').attr('SubjectId'),
                BookName: $("#txtBookName").val(),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("oHistoryHomework.aspx/GetoHomework", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbHW").setTemplateElement("template_HW", null, { filter_data: false });
                    $("#tbHW").processTemplate(json);
                    $(".pagination_container").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbHW").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
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
        $(function () {
            pageIndex = 1;
            homeWork_AssignTeacher = $(".left_tree_list a:eq(0)").attr("tt");
            docName = "";
            ShowFolderIn = "1";
            $('[data-name="subject"] li a').removeClass("active");
            $('[data-name="subject"] li a').eq(0).addClass("active");
            $('[data-name="subject"] li a').bind({
                click: function () {
                    $('[data-name="subject"] li a').removeClass("active").eq(0).addClass("active");
                    SubjectId = $(this).attr("SubjectId");
                    $('[data-name="subject"] li a').removeClass("active");
                    $(this).addClass("active");
                    loadData();

                }
            });
            loadData();
            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();
            })
        });
        function OpenNew(url) {
            window.open(url, 'newwindow', 'width=1300,height=600,top=0,left=0,scrollbars=yes');
        }
    </script>
</asp:Content>
