<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="AnalysisClassList.aspx.cs" Inherits="Rc.Cloud.Web.Principal.AnalysisClassList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../plugin/laydate/laydate.js"></script>
    <script type="text/javascript" src="../js/json2.js"></script>
    <script type="text/javascript" src="../js/jq.pagination.js"></script>
    <script type="text/javascript" src="../js/jquery-jtemplates.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            $('[data-name="subject"] a').on('click', function () {
                pageIndex = 1;
                loadData();
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

            $('.subject-mtree-hook').mtree();
        })
        var loadData = function () {
            var dto = {
                GradeId: "<%=GradeId%>",
                SubjectId: $('[data-name="subject"]').find('.mtree-link-hook.active').attr('ajax-value'),
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- 二级菜单 -->
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="EachGreadAnalysisList.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>" class="active">每次作业数据分析</a></li>
            <li><a href="HisKlgAnalysis.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">历次知识点数据分析</a></li>
            <li><a href="ClassGrowthTranjectory.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">班级成长轨迹</a></li>
            <li><a href="STGrowthTranjectory.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">学生成长轨迹</a></li>
            <li><a href="HWSubmitMark.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">作业提交及批改情况</a></li>
        </ul>
    </div>
    <div class="iframe-container">
        <!-- 左侧内容 -->
        <div class="iframe-sidebar">
            <div class="mtree subject-mtree-hook">
                <ul data-name="subject">
                    <asp:Literal runat="server" ID="ltlSubject"></asp:Literal>
                </ul>
            </div>
        </div>
        <!-- 右侧内容 -->
        <div class="iframe-main pa" data-name="main-auto">
            <h2 class="page_title hidden">
                <asp:Literal runat="server" ID="ltlGradeName"></asp:Literal>
            </h2>
            <div class="filter mn">
                <div class="form-inline">
                    <div class="form-group">
                        <input type="text" id="txtHomeWork_Name" name="" placeholder="作业名称" class="form-control input-sm" maxlength="30" clientidmode="Static">
                        <input type="button" id="btnSearch" value="确定" class="btn btn-primary btn-sm" clientidmode="Static">
                    </div>
                </div>
            </div>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>班级</th>
                        <th>任课老师</th>
                        <th>已交/未交</th>
                        <th>已批/未批</th>
                        <th>是否完成</th>
                        <th>完成时间</th>
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
    </div>
    <textarea id="template_1" class="hidden">
    {#foreach $T.list as record}
        <tr>
            <td>{$T.record.ClassName}</td>
            <td>{$T.record.TeacherName}</td>
            <td>{$T.record.TJ}</td>
            <td>{$T.record.PG}</td>
            <td>{$T.record.IsFinish}</td>
            <td>{$T.record.HomeWork_FinishTime}</td>
        </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>
