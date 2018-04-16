<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TeacherStats.Master" AutoEventWireup="true" CodeBehind="StudentAchievementTrack.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.StudentAchievementTrack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../plugin/tablesorter/js/jquery.tablesorter.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[data-name='tablesorter']").tablesorter({
                headers: {
                    0: { sorter: false },
                    1: { sorter: false },
                    7: { sorter: false },
                    8: { sorter: false },
                }
            });
            StatsClassHW_ScoreOverviewID = "-1";
            if (getUrlVar("ClassCode") == "" || getUrlVar("ClassCode") == null || getUrlVar("ClassCode") == undefined) {
                Class = "<%=ClassID%>";
            }
            else { Class = getUrlVar("ClassCode") }
            if (getUrlVar("index") == "" || getUrlVar("index") == null || getUrlVar("index") == undefined) {
                sd = 0;
            }
            else { sd = getUrlVar("index"); }

            //$("#Classes li a[tt]").removeClass("active");
            //$("#Classes li a[tt]").eq(sd).addClass("active");
            $("#Classes li a[tt]").bind({
                click: function () {
                    if (!$(this).hasClass("disabled")) {
                        $("#Classes li a[tt]").removeClass("active").eq(sd).addClass("active");
                        StatsClassHW_ScoreOverviewID = $(this).attr("tt");
                        Class = $(this).attr("cs");
                        sd = $("#Classes li a[tt]").index(this);
                        $("#Classes li a[tt]").removeClass("active");
                        $(this).addClass("active");
                        loadData();
                    }
                }
            });
            $("#btnSeacher").click(function () {
                loadData();
            })
            $(document).keydown(function (e) {
                if (e.keyCode == 13) {
                    $('#btnSeacher').click();
                    return false;
                }
            })
            loadData();
        })
        var loadData = function () {
            var dto = {
                Class: $("#Classes li a[tt].active").attr("cs"),
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                UserId: "<%=UserId%>",
                x: Math.random()
            };
            $.ajaxWebService("StudentAchievementTrack.aspx/GetStatsClassStudentHW_Score", JSON.stringify(dto), function (data) {
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
                        window.open("StudentAnalysisReports.aspx?ResourceToResourceFolder_Id=" + ResourceToResourceFolder_Id + "&HomeWork_Id=" + HomeWork_Id + "&StudentId=" + StudentId + "&Student_HomeWork_Id=" + Student_Homework_Id);
                    }
                    else {
                        window.open("StudentAnalysisReports.aspx?ResourceToResourceFolder_Id=" + ResourceToResourceFolder_Id + "&HomeWork_Id=" + HomeWork_Id + "&StudentId=" + StudentId);
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
<asp:Content ID="Content2" ContentPlaceHolderID="Container" runat="server">
    <div class="filter analysis_filter">
        <div class="filter_section">
            <div class="filter_row">
                <span class="row_name">班级：</span>
                <div class="row_item">
                    <ul id="Classes">
                        <asp:Literal ID="ltlClasses" runat="server"></asp:Literal>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <div class="panel mn">
        <div class="panel-body">
            <table class="table table-bordered text-center" data-name="tablesorter">
                <thead>
                    <tr>
                        <th>姓名</th>
                        <th>班级</th>
                        <th>成绩</th>
                        <%--<td>年级排名</td>
                        <td>年级进步情况</td>--%>
                        <th>排名</th>
                        <th>进步情况</th>
                        <!-- <td>层次对比</td> -->
                        <th width="165">操作</th>
                    </tr>
                </thead>
                <tbody id="tb1">
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
