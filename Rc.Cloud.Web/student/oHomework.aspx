<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/student.Master" AutoEventWireup="true" CodeBehind="oHomework.aspx.cs" Inherits="Homework.student.oHomework" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="oHomework.aspx" class="active">最新作业</a></li>
            <%if (Convert.ToBoolean(Rc.Common.ConfigHelper.GetConfigString("IsShowNewStuAnalysis")))
                { %>
            <li><a href="St_oHistoryHomework.aspx">历次作业</a></li>
            <%}
                else
                { %>
            <li><a href="oHistoryHomework.aspx">历次作业</a></li>
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
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th width="170">作业布置时间</th>
                            <th>作业名称</th>
                            <th width="170">作业来源</th>
                            <th width="85">学科</th>
                            <th width="170">发作业时间</th>
                            <th width="170">要求交作业时间</th>
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
        <td>{#if $T.record.BeginTime==''}-{#else} {$T.record.BeginTime}{#/if}</td>
        <td>{#if $T.record.StopTime==''}-{#else} {$T.record.StopTime}{#/if}</td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            homeWork_AssignTeacher = $(".left_tree_list a:eq(0)").attr("tt");
            docName = "";
            ShowFolderIn = "1";
            SubjectId = "-1";
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
        });

        function ShowSubDocument(strDoctumentID, obj) {
            homeWork_AssignTeacher = strDoctumentID;
            $(".left_tree_list div").removeClass();
            $(obj).parent().addClass("active");
            pageIndex = 1;

            loadData();

        }

        function loadData() {
            var dto = {
                SubjectId: $('[data-name="subject"]').find('a.active').attr('SubjectId'),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("oHomework.aspx/GetoHomework", JSON.stringify(dto), function (data) {
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

        function pageselectCallback(page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
    </script>
</asp:Content>
