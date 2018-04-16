<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/parent.Master" AutoEventWireup="true" CodeBehind="JobTrack.aspx.cs" Inherits="Rc.Cloud.Web.parent.JobTrack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../plugin/laydate/laydate.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container">
        <div class="container pt">
            <div class="filter">
                <div class="form-inline">
                    <div class="form-group">
                        <label>时间：</label>
                        <input class="form-control input-sm" id="StartTime" placeholder="开始时间" clientidmode="Static" type="text">
                        <input class="form-control input-sm" id="EndTime" placeholder="结束时间" clientidmode="Static" type="text">
                    </div>
                    <%--<div class="form-group">
                        <label>书目名称：</label>
                        <input class="form-control input-sm" id="txtBookName" type="text" clientidmode="Static">
                    </div>--%>
                    <input class="btn btn-primary btn-sm" id="btnSearch" value="确定" clientidmode="Static" type="button">
                </div>
                <div class="filter_section">
                    <div class="filter_row clearfix">
                        <span class="row_name">我家宝贝：</span>
                        <div class="row_item">
                            <ul ajax-name="student">
                                <asp:Literal ID="ltlBabyName" runat="server" ClientIDMode="Static"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">学科：</span>
                        <div class="row_item">
                            <ul ajax-name="subject">
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-bordered text-center">
                <thead>
                    <tr>
                        <th class="text-left">考试名称</th>
                        <th width="15%" class="text-left">答题时间</th>
                        <th width="8%">时长</th>
                        <th width="10%">IP</th>
                        <th width="6%">成绩</th>
                        <th width="8%">班级排名</th>
                        <th width="8%">班级层次</th>
                        <th width="8%">年级排名</th>
                        <th width="8%">年级层次</th>
                        <th width="8%">操作</th>
                    </tr>
                </thead>
                <tbody id="List"></tbody>
            </table>
            <div class="pagination_container">
                <ul class="pagination"></ul>
            </div>
        </div>
    </div>
    <textarea id="ListBox" style="display: none">
    {#foreach $T.list as record}
    <tr>
            <td class="text-left">{$T.record.HomeWork_Name}</td>
            <td class="text-left">
                <p>开始：{$T.record.AnswerStart}</p>
                <p>结束：{$T.record.AnswerEnd}</p>
            </td>
            <td>{$T.record.DateDiffMin}</td>
            <td>{$T.record.StudentIP}</td>
            <td>{$T.record.StudentScore}</td>
            <td>{$T.record.StudentScoreOrder}{$T.record.ScoreImprove}</td>
            <td>{$T.record.Hierarchy}</td>
            <td>{$T.record.GradeStudentScoreOrder}{$T.record.GradeScoreImprove}</td>
            <td>{$T.record.GradeHierarchy}</td>
            <td>
                {#if $T.record.Student_HomeWork_CorrectStatus=="1"}
                {#if $T.record.CorrectMode=="1"}
                <a href="../student/ohomeworkview_client.aspx?HomeWork_ID={$T.record.HomeWork_ID}&StudentId={$T.record.StudentID}&ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&Student_HomeWork_Id={$T.record.Student_Homework_Id}" target="_blank">作业详情</a>
                {#else}
                <a href="../student/oHomeWorkViewTT.aspx?HomeWork_ID={$T.record.HomeWork_ID}&StudentId={$T.record.StudentID}&ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}" target="_blank">作业详情</a>
                {#/if}
                {#else}
                <a href='##' class="text-muted">作业详情</a>
                {#/if}
            </td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;//默认页码
            $('[ajax-name="student"] li a').removeClass("active");
            $('[ajax-name="student"] li a').eq(0).addClass("active");
            $('[ajax-name="student"] li a').on({
                click: function () {
                    $('[ajax-name="student"] li a').removeClass("active").eq(0).addClass("active");
                    $('[ajax-name="student"] li a').removeClass("active");
                    $(this).addClass("active");
                    pageIndex = 1;
                    loadData();
                    loadSubject();
                }
            });
            loadSubject();
            $('[ajax-name="subject"] li a').removeClass("active");
            $('[ajax-name="subject"] li a').eq(0).addClass("active");
            $(document).on({
                click: function () {
                    $('[ajax-name="subject"] li a').removeClass("active").eq(0).addClass("active");
                    //SubjectId = $(this).attr("SubjectId");
                    $('[ajax-name="subject"] li a').removeClass("active");
                    $(this).addClass("active");
                    pageIndex = 1;
                    loadData();
                }
            }, '[ajax-name="subject"] li a');
            loadData();
            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();

            })
        })
        var loadSubject = function () {
            var dto = {
                BabyId: $('[ajax-name="student"]').find('a.active').attr('BabyId'),
                x: Math.random()
            };
            $.ajaxWebService("JobTrack.aspx/GetSubject", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $('[ajax-name="subject"]').html(data.d);
                }
            }, function () { });

        }
        var loadData = function () {
            var $_objBox = $("#ListBox");
            var objBox = "ListBox";
            var $_objList = $("#List");
            var $_pagination = $(".pagination_container");
            var subjectId = $('[ajax-name="subject"]').find('a.active').attr('SubjectId');
            var dto = {
                StudentID: $('[ajax-name="student"]').find('a.active').attr('BabyId'),
                SubjectID: (subjectId) ? subjectId : '-1',
                HomeWorkCreateTime: $("#StartTime").val(),
                HomeWorkFinishTime: $("#EndTime").val(),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };

            $.ajaxWebService("JobTrack.aspx/GetHWList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $_objList.setTemplateElement(objBox, null, { filter_data: false });
                    $_objList.processTemplate(json);
                    $_pagination.pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $_objList.html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $_pagination.find("ul").html("");
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
    </script>
</asp:Content>
