<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PrincipalAnalysis.Master" AutoEventWireup="true" CodeBehind="StatsGradeClassHW_Hierarchy.aspx.cs" Inherits="Rc.Cloud.Web.Principal.StatsGradeClassHW_Hierarchy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <ul class="nav nav-tabs mt bg_white pv">
        <li><a href="ResultsPreview.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">成绩概览</a></li>
        <li><a href="StatsGradeClassHW_TOPN.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">年级排名分布</a></li>
        <li><a href="AverageContrast.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">平均分对比</a></li>
        <li><a href="StatsGradeHW_ScoreLevel.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">等级分布对比</a></li>
        <li><a href="StatsGradeHW_Subsection.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">分段统计对比</a></li>
        <li class="active"><a href='##'>四个层次对比</a></li>
        <li class="pull-right">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#tab1" data-toggle="tab">列表查看</a></li>
                <li><a href="#tab2" data-toggle="tab">图标查看</a></li>
            </ul>
        </li>
    </ul>

    <div class="panel mn">
        <div class="panel-body">
            <div class="tab-content">
                <div class="tab-pane active" id="tab1">
                    <table class="table table-bordered text-center" data-name="tablesorter">
                        <thead>
                            <tr>
                                <th>班级</th>
                                <th>第一层次</th>
                                <th>第二层次</th>
                                <th>第三层次</th>
                                <th>第四层次</th>
                                <!-- <td>直观图</td> -->
                            </tr>
                        </thead>
                        <tbody id="tb_01"></tbody>
                        <tbody id="tb_02"></tbody>
                    </table>
                </div>
                <div class="tab-pane" id="tab2">
                    <div id="chart"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <script src="../plugin/tablesorter/js/jquery.tablesorter.min.js"></script>
    <script src="../plugin/highcharts/js/highcharts.js"></script>
    <script src="../js/highcharts.config.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[data-name='sidebar'] li:eq(1)").addClass("active");

            $("[data-name='tablesorter']").tablesorter({
                headers: {
                    0: { sorter: false },
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
            $.ajaxWebService("StatsGradeClassHW_Hierarchy.aspx/GetStatsGradeClassHW_Hierarchy", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    var json = $.parseJSON(data.d);
                    //$("#tb_01").html(json.tbGrade);
                    $("#tb_02").html(json.tb);
                    APic(json.TempClassName, json.TempDate);

                }
                else { $("#tb1").html("<tr><td colspan='100'>无数据</td></tr>"); }
                $("[data-name='tablesorter']").trigger("update", [true]);
            }, function () { });
        }
        var APic = function (TempClassName, TempDate) {

            var arr = TempClassName.split('|');
            $('#chart').highcharts({
                chart: {
                    type: 'column',
                    height: 500,
                    width: $('#tab1').width()
                },
                xAxis: {
                    categories: arr,
                    title: {
                        text: null
                    },
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '人数'
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y}</b></td></tr>',
                    footerFormat: '</table>',
                    valueSuffix: '人',
                    shared: true,
                    useHTML: true
                },
                series: $.parseJSON(TempDate)
            });
        }
    </script>
</asp:Content>
