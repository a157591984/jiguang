<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PrincipalAnalysis.Master" AutoEventWireup="true" CodeBehind="StudentAchievementTrackGrade.aspx.cs" Inherits="Rc.Cloud.Web.Principal.StudentAchievementTrackGrade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="filter analysis_filter">
        <div class="filter_section">
            <div class="filter_row clearfix">
                <span class="row_name">班级：</span>
                <div class="row_item">
                    <ul id="Classes">
                        <asp:Literal ID="ltlClasses" runat="server"></asp:Literal>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <div class="panel">
        <div class="panel-body">
            <table class="table table-bordered text-center" data-name="tablesorter">
                <thead>
                    <tr>
                        <th width="150" class="text-left">姓名</th>
                        <th class="text-left">班级</th>
                        <th width="80">成绩</th>
                        <th width="90">年级排名</th>
                        <th width="120">年级进步情况</th>
                        <th width="90">班级排名</th>
                        <th width="120">班级进步情况</th>
                        <th width="150">操作</th>
                    </tr>
                </thead>
                <tbody id="tb1">
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <script src="../plugin/tablesorter/js/jquery.tablesorter.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[data-name='sidebar'] li:eq(2)").addClass("active");

            $("[data-name='tablesorter']").tablesorter({
                sortList: [[9, 1]],
                headers: {
                    0: { sorter: false },
                    1: { sorter: false },
                    7: { sorter: false },
                    8: { sorter: false },
                }
            });
            Class = "";
            $("#Classes li a").removeClass();
            $("#Classes li a").eq(0).addClass("active");
            $("#Classes li a").bind({
                click: function () {
                    StatsClassHW_ScoreOverviewID = $(this).attr("tt");
                    Class = $(this).attr("ajax-value");
                    $("#Classes li a").removeClass("active");
                    $(this).addClass("active");
                    loadData();
                }
            });
            loadData();
        })
        var loadData = function () {
            var dto = {
                Class: Class,
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                GradeId: "<%=GradeId%>",
                x: Math.random()
            };
            $.ajaxWebService("StudentAchievementTrackGrade.aspx/GetStatsClassStudentHW_Score", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $("#tb1").html(data.d);
                }
                else { $("#tb1").html("<tr><td colspan='100'>无数据</td></tr>"); }
                $("[data-name='tablesorter']").trigger("update", [true]);
            }, function () { });
        }
        var AnalysisReport = function (ResourceToResourceFolder_Id, HomeWork_Id, StudentId, IsB, IsA, CorrectMode, Student_Homework_Id) {
            if (IsB == "1")
            { } else {
                if (IsA == "1")
                { } else {
                    if (CorrectMode == "1") {//客户端批改
                        window.open("../Evaluation/StudentAnalysisReports.aspx?ResourceToResourceFolder_Id=" + ResourceToResourceFolder_Id + "&HomeWork_Id=" + HomeWork_Id + "&StudentId=" + StudentId + "&Student_HomeWork_Id=" + Student_Homework_Id);
                    }
                    else {
                        window.open("../Evaluation/StudentAnalysisReports.aspx?ResourceToResourceFolder_Id=" + ResourceToResourceFolder_Id + "&HomeWork_Id=" + HomeWork_Id + "&StudentId=" + StudentId);
                    }
                }
            }
        }
        var CorrectView = function (ResourceToResourceFolder_Id, HomeWork_Id, StudentId, IsB, CorrectMode, Student_Homework_Id) {
            if (IsB == "1")
            { } else {
                if (CorrectMode == "1") {//客户端批改
                    window.open("../student/ohomeworkview_client.aspx?ResourceToResourceFolder_Id=" + ResourceToResourceFolder_Id + "&HomeWork_Id=" + HomeWork_Id + "&StudentId=" + StudentId + "&Student_HomeWork_Id=" + Student_Homework_Id);
                }
                else {
                    window.open("../student/OHomeWorkViewTT.aspx?ResourceToResourceFolder_Id=" + ResourceToResourceFolder_Id + "&HomeWork_Id=" + HomeWork_Id + "&StudentId=" + StudentId);
                }
            }
        }
    </script>
</asp:Content>
