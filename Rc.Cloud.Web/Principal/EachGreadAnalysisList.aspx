<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="EachGreadAnalysisList.aspx.cs" Inherits="Rc.Cloud.Web.Principal.EachGreadAnalysisList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            $('.mtree-subject-hook').mtree({
                onClick: function () {
                    pageIndex = 1;
                    loadData();
                }
            });
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
            loadData();
        })
        var loadData = function () {
            var dto = {
                HWName: $("#txtHomeWork_Name").val(),
                GradeID: "<%=GradeId%>",
                GradeName: "<%=GradeName%>",
                SubjectID: $('.mtree-subject-hook').find('.mtree-link-hook.active').attr('ajax-value'),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("EachGreadAnalysisList.aspx/GetEachHWAnalysis", JSON.stringify(dto), function (data) {
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
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="EachGreadAnalysisList.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>" class="active">每次作业数据分析</a></li>
            <li><a href="HisKlgAnalysis.aspx?GradeId=<%=GradeId %>&GradeName=<%=Server.UrlEncode(GradeName) %>">历次知识点数据分析</a></li>
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
            <h3 class="page_title">
                <asp:Literal runat="server" ID="ltlGradeName"></asp:Literal>
            </h3>
            <div class="filter">
                <div class="form-inline">
                    <div class="form-group">
                        <label>作业名称：</label>
                        <input type="text" id="txtHomeWork_Name" name="" placeholder="作业名称" class="form-control input-sm" maxlength="30" clientidmode="Static">
                    </div>
                    <input type="button" id="btnSearch" value="确定" class="btn btn-sm btn-primary" clientidmode="Static">
                </div>
            </div>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th width="120">日期</th>
                        <th>作业名称</th>
                        <th width="120">年级</th>
                        <th width="100">学科</th>
                        <th width="120">批改完成班级数</th>
                        <th width="120">班级批改详情</th>
                        <th width="200">操作</th>
                    </tr>
                </thead>
                <tbody id="tb1">
                </tbody>
            </table>
            <div class="pagination_container">
                <ul class="pagination">
                </ul>
            </div>

        </div>
    </div>
    <textarea id="template_1" class="hidden">
    {#foreach $T.list as record}
    <tr>
        <td>{$T.record.HW_CreatTime}</td>
        <td>{$T.record.HomeWork_Name}</td>
        <td>{$T.record.GradeName}</td>
        <td>{$T.record.SubjectName}</td>
        <td>{$T.record.PG}</td>
        <td>
            <a href='##' data-name="correctDetail" data-title="{$T.record.GradeName}" data-url="ClassCorrectDetail.aspx?GradeId={$T.record.GradeId}&SubjectId={$T.record.SubjectId}&ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}">查看</a>
        </td>
        <td>
            <%--<a href='##' data-name="dataAnalysis" data-rtrfid="{$T.record.ResourceToResourceFolder_Id}" data-href="StatsGradeHW_TQ.aspx?GradeId={$T.record.GradeId}&ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}">数据分析</a>--%>
            {#if $T.record.SubmitStudent==0}
                <a href='##' class="disabled">数据分析</a>
            {#else}
                <a href="CheckGradeStatsHelper.aspx?GradeId={$T.record.GradeId}&ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}" target="_blank">数据分析</a>
                <a href='##' onclick="ReCalculation('{$T.record.ResourceToResourceFolder_Id}','{$T.record.GradeId}');">重新计算</a>
            {#/if}
        </td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        //重新计算
        function ReCalculation(ResourceToResourceFolder_Id, gradeId) {
            layer.confirm("确定要重新计算吗？", { icon: 3, title: "提示" }, function () {
                $.ajaxWebService("EachGreadAnalysisList.aspx/ReCalculation", "{rtrfId:'" + ResourceToResourceFolder_Id + "',gradeId:'" + gradeId + "',x:" + Math.random() + "}", function (data) {
                    if (data.d == "1") {
                        layer.msg("重新计算成功", { icon: 1, time: 1000 }, function () { loadData(); });
                    }
                    else {
                        layer.msg("重新计算失败", { icon: 2, time: 2000 });
                    }
                }, function () { })
            });
        }

        $(function () {
            //班级批改详情
            $(document).on('click', '[data-name="correctDetail"]', function (e) {
                var url = $(this).data('url');
                var title = $(this).data('title');
                layer.open({
                    type: 2,
                    title: title,
                    area: ['800px', '550px'],
                    content: url,
                });
                e.preventDefault();
            })
        });

    </script>
</asp:Content>
