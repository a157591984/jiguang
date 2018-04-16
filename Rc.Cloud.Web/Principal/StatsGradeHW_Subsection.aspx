<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PrincipalAnalysis.Master" AutoEventWireup="true" CodeBehind="StatsGradeHW_Subsection.aspx.cs" Inherits="Rc.Cloud.Web.Principal.StatsGradeHW_Subsection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <ul class="nav nav-tabs mt bg_white pv">
        <li><a href="ResultsPreview.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">成绩概览</a></li>
        <li><a href="StatsGradeClassHW_TOPN.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">年级排名分布</a></li>
        <li><a href="AverageContrast.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">平均分对比</a></li>
        <li><a href="StatsGradeHW_ScoreLevel.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">等级分布对比</a></li>
        <li class="active"><a href='##'>分段统计对比</a></li>
        <li><a href="StatsGradeClassHW_Hierarchy.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">四个层次对比</a></li>
    </ul>

    <div class="panel mn">
        <div class="panel-body">
            <table class="table table-bordered text-center">
                <thead id="tb1"></thead>
                <tbody id="tb2"></tbody>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $("[data-name='sidebar'] li:eq(1)").addClass("active");
            loadData();
        })
        var loadData = function () {
            var dto = {
                GradeId: "<%=GradeId%>",
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                x: Math.random()
            };
            $.ajaxWebService("StatsGradeHW_Subsection.aspx/GetStatsGradeHW_Subsection", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    var json = $.parseJSON(data.d);
                    $("#tb1").html(json.thead);
                    $("#tb2").html(json.tbody);

                }
                else { $("#tb1").html("<tr><td colspan='100'>无数据</td></tr>"); }
            }, function () { });
        }

    </script>
</asp:Content>
