<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PrincipalAnalysis.Master" AutoEventWireup="true" CodeBehind="AverageContrast.aspx.cs" Inherits="Rc.Cloud.Web.Principal.AverageContrast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <ul class="nav nav-tabs bg_white pv mt">
        <li><a href="ResultsPreview.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">成绩概览</a></li>
        <li><a href="StatsGradeClassHW_TOPN.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">年级排名分布</a></li>
        <li class="active"><a href='##'>平均分对比</a></li>
        <li><a href="StatsGradeHW_ScoreLevel.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">等级分布对比</a></li>
        <li><a href="StatsGradeHW_Subsection.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">分段统计对比</a></li>
        <li><a href="StatsGradeClassHW_Hierarchy.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">四个层次对比</a></li>
        <li class="pull-right">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#tab1" data-toggle="tab">列表查看</a></li>
                <li><a href="#tab2" data-toggle="tab">图表查看</a></li>
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
                                <th>排名</th>
                                <th>班级</th>
                                <th>授课老师</th>
                                <th>平均分</th>
                            </tr>
                        </thead>
                        <tbody id="tb1">
                        </tbody>
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
                    1: { sorter: false },
                    2: { sorter: false },
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
            $.ajaxWebService("AverageContrast.aspx/GetStatsGradeHW_Score", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    var arrayObj = data.d.split('|');
                    //alert(arrayObj);
                    if (arrayObj.length == 3) {
                        $("#tb1").html(arrayObj[0]);
                        AverageGrade(arrayObj[1], arrayObj[2]);
                    }
                }
                else { $("#tb1").html("<tr><td colspan='100'>无数据</td></tr>"); }
                $("[data-name='tablesorter']").trigger("update", [true]);
            }, function () { });
        }
        var AverageGrade = function (ClassName, Avgscore) {
            var arrc = ClassName.split(',');
            var arr = new Array();
            for (var i = 0; i < Avgscore.split(',').length; i++) {
                arr.push(parseFloat(Avgscore.split(',')[i]));
            }
            //年级排名
            $('#chart').highcharts({
                chart: {
                    type: 'bar',
                    height: 500,
                    width: $('#tab1').width()
                },
                xAxis: {
                    categories: arrc,
                    title: {
                        text: null
                    },
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '平均分'
                    }
                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true
                        }
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size: 10px">{point.key}:<b>{point.y}</b>分</span><br/>',
                    pointFormat: '',
                    footerFormat: '',
                    shared: true,
                    useHTML: true,
                },
                series: [
                    {
                        data: arr,
                    }
                ]
            });
        }
    </script>
</asp:Content>
