﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="visit_web.aspx.cs" Inherits="Rc.Cloud.Web.Principal.visit_web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../Scripts/json2.js"></script>
    <script type="text/javascript" src="../Scripts/jq.pagination.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-jtemplates.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="EachGreadAnalysisList.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">每次作业数据分析</a></li>
            <li><a href="HisKlgAnalysis.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">历次知识点数据分析</a></li>
            <li><a href="ClassGrowthTranjectory.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">班级成长轨迹</a></li>
            <li><a href="STGrowthTranjectory.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">学生成长轨迹</a></li>
            <li><a href="HWSubmitMark.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">作业提交及批改情况</a></li>
            <li><a href="visit_client.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">教案使用情况</a></li>
            <li><a href="visit_web.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>" class="active">讲评报告使用情况</a></li>

        </ul>
    </div>
    <div class="iframe-container">
        <div class="container pt">
            <h2 class="page_title">
                <asp:Literal runat="server" ID="ltlGradeName"></asp:Literal>
            </h2>
            <div class="filter">
                <div class="form-inline">
                    <div class="form-group">
                        <label>老师：</label>
                        <input type="text" id="txtTName" name="" class="form-control input-sm" maxlength="30" clientidmode="Static">
                    </div>
                    <input type="button" id="btnSearch" value="确定" class="btn btn-primary btn-sm" clientidmode="Static">
                </div>
                <div class="filter_section">
                    <div class="filter_row clearfix">
                        <span class="row_name">班级：</span>
                        <div class="row_item">
                            <ul ajax-name="class">
                                <asp:Literal ID="ltlClass" runat="server"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">统计方式：</span>
                        <div class="row_item">
                            <ul ajax-name="date">
                                <li><a href='##' ajax-value="day" class="active">日统计</a></li>
                                <li><a href='##' ajax-value="week">周统计</a></li>
                                <li><a href='##' ajax-value="month">月统计</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-bordered text-center">
                <thead>
                    <tr>
                        <th rowspan="2">日期</th>
                        <th rowspan="2">班级</th>
                        <th rowspan="2">老师</th>
                        <th colspan="3">访问频次</th>
                        <th colspan="3">访问文件数</th>
                        <th rowspan="2">总时长</th>
                        <th rowspan="2">平均时长</th>
                        <th rowspan="2">操作</th>
                    </tr>
                    <tr>
                        <th>所有</th>
                        <th>云</th>
                        <th>自有</th>
                        <th>所有</th>
                        <th>云</th>
                        <th>自有</th>
                    </tr>
                </thead>
                <tbody id="list">
                </tbody>
            </table>
            <div class="pagination_container">
                <ul class="pagination"></ul>
            </div>
        </div>
    </div>
    <textarea id="listBox" class="hidden">
    {#foreach $T.list as record}
    <tr>
        <td>{$T.record.DateData}</td>
        <td>{$T.record.ClassName}</td>
        <td>{$T.record.TeacherName}</td>
        <td>{$T.record.VisitCount_All}</td>
        <td>{$T.record.VisitCount_Cloud}</td>
        <td>{$T.record.VisitCount_Own}</td>
        <td>{$T.record.VisitFile_All}</td>
        <td>{$T.record.VisitFile_Cloud}</td>
        <td>{$T.record.VisitFile_Own}</td>
        <td>{$T.record.VistiDuration_Count}</td>
        <td>{$T.record.VistiDuration_Avg}</td>
        <td>
            {#if $T.record.VisitCount_All!=0}
            <a href="visit_web_detail.aspx?DateType={$T.record.DateType}&DateData={$T.record.DateData}&TeacherId={$T.record.TeacherId}&ClassId={$T.record.ClassId}" target="_blank">查看</a>
            {#/if}
        </td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
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

            pageIndex = 1;//默认页码
            loadData();//初始化数据

            //筛选
            $('[ajax-name] li').on({
                click: function () {
                    if (!$(this).children("a").hasClass("disabled")) {
                        $(this).closest('ul').find('a').removeClass('active');
                        $(this).children('a').addClass('active');
                        pageIndex = 1;
                        loadData();
                    }
                }
            })
            $('[data-name="subject"] a').on({
                click: function () {
                    pageIndex = 1;
                    loadData();
                }
            })
            //检索
            $('input[name="search"]').on({
                click: function () {
                    pageIndex = 1;
                    loadData();
                }
            })
        })
        var loadData = function () {
            var $_objBox = $("#listBox");
            var objBox = "listBox";
            var $_objList = $("#list");
            var $_pagination = $(".pagination_container");
            var dto = {
                GradeID: "<%=GradeId%>",
                ClassID: $('[ajax-name="class"]').find('a.active').attr('ajax-value'),
                DateType: $('[ajax-name="date"]').find('a.active').attr('ajax-value'),
                TeacherName: $.trim($("#txtTName").val()),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("visit_web.aspx/GetList", JSON.stringify(dto), function (data) {
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
    </script>
</asp:Content>
