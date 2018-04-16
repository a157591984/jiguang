<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PrincipalAnalysis.Master" AutoEventWireup="true" CodeBehind="ResultsPreview.aspx.cs" Inherits="Rc.Cloud.Web.Principal.ResultsPreview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <ul class="nav nav-tabs bg_white pv mt">
        <li class="active"><a href='##'>成绩概览</a></li>
        <li><a href="StatsGradeClassHW_TOPN.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">年级排名分布</a></li>
        <li><a href="AverageContrast.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">平均分对比</a></li>
        <li><a href="StatsGradeHW_ScoreLevel.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">等级分布对比</a></li>
        <li><a href="StatsGradeHW_Subsection.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">分段统计对比</a></li>
        <li><a href="StatsGradeClassHW_Hierarchy.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">四个层次对比</a></li>
    </ul>
    <div class="panel mn">
        <div class="panel-body">
            <table class="table table-bordered text-center" data-name="tablesorter">
                <thead>
                    <tr>
                        <th class="text-left">班级</th>
                        <th width="180" class="text-left">老师</th>
                        <th width="90">最高分</th>
                        <th width="90">最低分</th>
                        <th width="90">平均分</th>
                        <th width="90">中位数</th>
                        <th width="80">众数</th>
                        <th width="80">总数</th>
                        <th width="110">已发/未发</th>
                        <th width="110">已交/未交</th>
                    </tr>
                </thead>
                <tbody id="tb_01"></tbody>
                <tbody id="tb_02"></tbody>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <script src="../plugin/tablesorter/js/jquery.tablesorter.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[data-name='sidebar'] li:eq(1)").addClass("active");

            $("[data-name='tablesorter']").tablesorter({
                headers: {
                    0: { sorter: false },
                    1: { sorter: false },
                }
            });
            loadData();
        })
        var loadData = function () {
            var dto = {
                GradeId: "<%=GradeId%>",
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                x: Math.random()
            };
            $.ajaxWebService("ResultsPreview.aspx/GetStatsGradeHW_Score", JSON.stringify(dto), function (data) {
                var arrHtml = data.d.split("†");
                if (data.d != "") {
                    $("#tb_01").html(arrHtml[0]);
                    $("#tb_02").html(arrHtml[1]);
                }
                else { $("#tb_01").html("<tr><td colspan='100'>无数据</td></tr>"); }
                $("[data-name='tablesorter']").trigger("update", [true]);
            }, function () { });
        }
    </script>
</asp:Content>
