<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="HisKlgAnalysis.aspx.cs" Inherits="Rc.Cloud.Web.Principal.HisKlgAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="EachGreadAnalysisList.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">每次作业数据分析</a></li>
            <li><a href="HisKlgAnalysis.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>" class="active">历次知识点数据分析</a></li>
            <li><a href="ClassGrowthTranjectory.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">班级成长轨迹</a></li>
            <li><a href="STGrowthTranjectory.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">学生成长轨迹</a></li>
            <li><a href="HWSubmitMark.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">作业提交及批改情况</a></li>
            <li><a href="visit_client.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">教案使用情况</a></li>
            <li><a href="visit_web.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">讲评报告使用情况</a></li>

        </ul>
    </div>
    <div class="iframe-container">
        <div class="iframe-sidebar">
            <div class="mtree mtree-subject-hook">
                <ul>
                    <asp:Literal runat="server" ID="ltlSubject"></asp:Literal>
                </ul>
            </div>
        </div>
        <div class="iframe-main pa">
            <div class="page_title">
                <asp:Literal runat="server" ID="ltlGradeName"></asp:Literal>
            </div>
            <div class="filter">
                <div class="form-inline">
                    <div class="form-group">
                        <label>知识点名称：</label>
                        <input type="text" name="key" class="form-control input-sm" />
                    </div>
                    <div class="form-group">
                        <label>平均得分率低于：</label>
                        <input type="text" name="KPScoreAvgRate" class="form-control input-sm" />&nbsp;%
                    </div>
                    <input type="button" name="search" id="btnSearch" value="检索" class="btn btn-primary btn-sm" />
                </div>
                <div class="filter_section">
                    <div class="filter_row clearfix">
                        <span class="row_name">班级：</span>
                        <div class="row_item">
                            <ul ajax-name="class">
                                <asp:Literal ID="ltlClass" runat="server" ClientIDMode="Static"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">日期范围：</span>
                        <div class="row_item">
                            <ul data-name="date_range" ajax-name="datetype">
                                <li data-tab-bar="date" class="active"><a href='##' class="active" ajax-value="month">月度</a></li>
                                <li data-tab-bar="date"><a href='##' ajax-value="quarter">季度</a></li>
                                <li data-tab-bar="date"><a href='##' ajax-value="halfyear">年度</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">日期：</span>
                        <div class="row_item">
                            <ul data-tab-box="date" data-name="month" ajax-name="datedata"></ul>
                            <ul data-tab-box="date" data-name="quarter" ajax-name="datedata"></ul>
                            <ul data-tab-box="date" data-name="year" ajax-name="datedata"></ul>
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th width="120">年级</th>
                        <th width="120">学科</th>
                        <th width="120">班级</th>
                        <th>知识点名称</th>
                        <th width="150">平均得分率</th>
                        <th width="80">来源</th>
                    </tr>
                </thead>
                <tbody id="knowledgeList">
                </tbody>
            </table>
            <div class="pagination_container">
                <ul class="pagination"></ul>
            </div>
        </div>
    </div>
    <textarea id="knowledgeBox" class="hidden">
    {#foreach $T.list as record}
    <tr>
        <td>{$T.record.GradeName}</td>
        <td>{$T.record.SubjectName}</td>
        <td>{$T.record.ClassName}</td>
        <td class="align_left">{$T.record.KPName}</td>
        <td>{$T.record.KPScoreAvgRate}</td>
        <td class="table_opera"><!--HisKlgAnalysisSU-->
<a href="HisKlgAnalysisSource.aspx?GradeId={$T.record.GradeID}&GradeName=<%= Server.UrlEncode(GradeName) %>&SubjectID={$T.record.SubjectID}&ClassID={$T.record.ClassID}&KPName={$T.record.KPNameEncode}&DateData={$T.record.DateData}&DateType={$T.record.DateType}" target="_blank">来源</a>
        </td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;//默认页码
            loadData();//初始化数据
            getMonth();
            getQuarter();
            getYear();
            $(document).on({
                click: function () {
                    if (!$(this).children("a").hasClass("disabled")) {
                        $(this).closest('ul').find('a').removeClass('active');
                        $(this).children('a').addClass('active');
                        if ($(this).closest('ul').attr('ajax-name') == 'datetype') {
                            var index = $(this).index();
                            $('[data-tab-box="date"]').find('a').removeClass('active');
                            $('[data-tab-box="date"]').eq(index).find('li:eq(0) a').addClass('active');
                        }
                        pageIndex = 1;
                        loadData();
                    }
                }
            }, '[ajax-name] li')
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

            $('.mtree-subject-hook').mtree({
                onClick: function () {
                    pageIndex = 1;
                    loadData();
                }
            });

            //切换日期范围时默认当前范围下的第一个条件选中
            //$('[data-name="date_range"] li a').on({
            //    click: function () {
            //        var index = $(this).closest('li').index();
            //        $('[data-tab-box="date"]').find('a').removeClass('active');
            //        $('[data-tab-box="date"]').eq(index).find('li:eq(0)').click();
            //    }
            //});

        })

        var loadData = function () {
            var $_objBox = $("#knowledgeBox");
            var objBox = "knowledgeBox";
            var $_objList = $("#knowledgeList");
            var $_pagination = $(".pagination_container");
            var dto = {
                Gradeid: "<%=GradeId%>",
                SubjectID: $('.mtree-subject-hook').find('.mtree-link-hook.active').attr('ajax-value'),
                ClassID: $('[ajax-name="class"]').find('a.active').attr('ajax-value'),
                key: $('input[name="key"]').val(),
                KPScoreAvgRate: $('input[name="KPScoreAvgRate"]').val(),
                DateType: $('[ajax-name="datetype"]').find('a.active').attr('ajax-value'),
                DateData: ($('[ajax-name="datedata"]').find('a.active').length > 0) ? $('[ajax-name="datedata"]').find('a.active').attr('ajax-value') : '',
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("HisKlgAnalysis.aspx/GetKnowledge", JSON.stringify(dto), function (data) {
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
