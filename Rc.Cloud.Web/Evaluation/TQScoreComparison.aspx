<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TeacherStats.Master" AutoEventWireup="true" CodeBehind="TQScoreComparison.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.TQScoreComparison" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../plugin/tablesorter/js/jquery.tablesorter.min.js"></script>
    <script src="../plugin/highcharts/js/highstock.js"></script>
    <script src="../js/highcharts.config.js"></script>
    <script type="text/javascript">
        $(function () {
            loadData();
        })
        var loadData = function () {
            var dto = {
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                UserId: "<%=UserId%>",
                ClassID: "<%=ClassID%>",
                x: Math.random()
            };
            $.ajaxWebService("TQScoreComparison.aspx/StatsClassHW_TQ", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    if (data.d != "") {
                        var json = $.parseJSON(data.d);
                        $("#tb1").html(json.thead);
                        $("#tb2").html(json.tbody);
                        DataSour(json.StrTQNum, json.DateSour);
                    }
                }
                else { $("#tb2").html("<tr><td colspan='100'>无数据</td></tr>"); }
            }, function () { });
        }
        var DataSour = function (TQNum, ClassNameV) {
            var arr = TQNum.split(',');
            //小题得分对比
            $('#chart').highcharts({
                chart: {
                    type: 'column',
                },
                xAxis: {
                    categories: arr,
                    title: {
                        text: '题号'
                    },
                    max: (arr.length > 9) ? 9 : null,
                    min: (arr.length > 9) ? 0 : null
                },
                scrollbar: {
                    enabled: (arr.length > 9) ? true : false
                },
                yAxis: {
                    min: 0,
                    max: 100,
                    labels: {
                        formatter: function () {
                            return this.value + '%'
                        }
                    },
                    title: {
                        text: '得分率'
                    }
                },
                plotOptions: {
                    column: {
                        dataLabels: {
                            enabled: true,
                            formatter: function () {
                                return this.y + '%';
                            }
                        }
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.1f}%</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                series: $.parseJSON(ClassNameV)
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Container" runat="server">
    <ul class="nav nav-tabs mb">
        <li><a href="ComparisonBetweenClasses.aspx?ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>&HomeWork_ID=<%=HomeWork_ID %><%=StrTemp %>">整体成绩对比</a></li>
        <li class="active"><a href='##'>小题得分对比</a></li>
        <li><a href="KPScoreComparison.aspx?ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>&HomeWork_ID=<%=HomeWork_ID %><%=StrTemp %>">知识点得分对比</a></li>
        <li class="pull-right">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#list" data-toggle="tab">列表查看</a></li>
                <li><a href="#charts" data-toggle="tab">图表查看</a></li>
            </ul>
        </li>
    </ul>

    <div class="tab-content">
        <div class="tab-pane active" id="list">
            <table class="table table-bordered text-center">
                <thead id="tb1">
                </thead>
                <tbody id="tb2">
                </tbody>
            </table>
        </div>
        <div class="tab-pane" id="charts">
            <div id="chart" style="width: 1170px; height: 500px;"></div>
        </div>
    </div>
</asp:Content>
