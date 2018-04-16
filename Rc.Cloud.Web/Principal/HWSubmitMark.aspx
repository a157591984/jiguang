<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="HWSubmitMark.aspx.cs" Inherits="Rc.Cloud.Web.Principal.HWSubmitMark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="EachGreadAnalysisList.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">每次作业数据分析</a></li>
            <li><a href="HisKlgAnalysis.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">历次知识点数据分析</a></li>
            <li><a href="ClassGrowthTranjectory.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">班级成长轨迹</a></li>
            <li><a href="STGrowthTranjectory.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">学生成长轨迹</a></li>
            <li><a href="HWSubmitMark.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>" class="active">作业提交及批改情况</a></li>
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
            <h2 class="page_title">
                <asp:Literal runat="server" ID="ltlGradeName"></asp:Literal>
            </h2>
            <div class="filter">
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
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>日期</th>
                        <th>班级</th>
                        <th>布置人次</th>
                        <th>交作业人次</th>
                        <th>批改人次</th>
                        <th>交作业率</th>
                        <th>批改率</th>
                        <!-- <td width="80">查看</td> -->
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
        <td>{$T.record.AssignedCount}</td>
        <td>{$T.record.CommittedCount}</td>
        <td>{$T.record.CorrectedCount}</td>
        <td>{$T.record.CommittedCountRate}</td>
        <td>{$T.record.CorrectedCountRate}</td>
        <!-- <td class="table_opera">
            <a href="HWList.aspx?SubjectID={\$T.record.SubjectID}&ClassID={\$T.record.ClassID}&Date=">相关作业</a>
        </td> -->
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;//默认页码
            loadData();//初始化数据

            //筛选
            $(document).on({
                click: function () {
                    if (!$(this).children("a").hasClass("disabled")) {
                        $(this).closest('ul').find('a').removeClass('active');
                        $(this).children('a').addClass('active');
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
        })
        var loadData = function () {
            var $_objBox = $("#listBox");
            var objBox = "listBox";
            var $_objList = $("#list");
            var $_pagination = $(".pagination_container");
            var dto = {
                GradeID: "<%=GradeId%>",
                SubjectID: $('.mtree-subject-hook').find('.mtree-link-hook.active').attr('ajax-value'),
                ClassID: $('[ajax-name="class"]').find('a.active').attr('ajax-value'),
                DateType: $('[ajax-name="date"]').find('a.active').attr('ajax-value'),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("HWSubmitMark.aspx/GetList", JSON.stringify(dto), function (data) {
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
