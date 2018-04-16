<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TeacherStats.Master" AutoEventWireup="true" CodeBehind="ComparisonBetweenClasses.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.ComparisonBetweenClasses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../plugin/tablesorter/js/jquery.tablesorter.min.js"></script>
    <script src="../plugin/highcharts/js/highcharts.js"></script>
    <script src="../js/highcharts.config.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[data-name='tablesorter_1']").tablesorter({
                headers: {
                    0: { sorter: false }
                }
            });
            $("[data-name='tablesorter_2']").tablesorter();
            $("[data-name='tablesorter_3']").tablesorter();
            //$("[data-name='tablesorter_4']").tablesorter();

            loadStatsClassHW_ScoreData();
            loadData();
            loadDataScoreLevel();
            loadDataHierarchy();
        })
        var loadStatsClassHW_ScoreData = function () {
            var dto = {
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                UserId: "<%=UserId%>",
                ClassID: "<%=ClassID%>",
                x: Math.random()
            };
            $.ajaxWebService("ComparisonBetweenClasses.aspx/GetStatsClassHW_Score", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $("#tb1").html(data.d);
                }
                else { $("#tb1").html("<tr><td colspan='100'>无数据</td></tr>"); }
                $("[data-name='tablesorter_1']").trigger("update", [true]);
            }, function () { });
        }
        var loadDataScoreLevel = function () {
            var dto = {
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                UserId: "<%=UserId%>",
                ClassID: "<%=ClassID%>",
                x: Math.random()
            };
            $.ajaxWebService("ComparisonBetweenClasses.aspx/StatsClassHW_ScoreLevel", JSON.stringify(dto), function (data) {

                if (data.d != "") {
                    var arrayObj = data.d.split('|');
                    if (arrayObj.length == 3) {
                        $("#tb_2_01").html(arrayObj[0]);
                        $("#tb_2_02").html(arrayObj[2]);
                        GetDatePic(arrayObj[1]);
                    }
                }
                else { $("#tb_2_01").html("<tr><td colspan='100'>无数据</td></tr>"); }
                $("[data-name='tablesorter_2']").trigger("update", [true]);
            }, function () { });
        }

        var loadDataHierarchy = function () {
            var dto = {
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                UserId: "<%=UserId%>",
                ClassID: "<%=ClassID%>",
                x: Math.random()
            };
            $.ajaxWebService("ComparisonBetweenClasses.aspx/StatsTeacherClassHW_Hierarchy", JSON.stringify(dto), function (data) {

                if (data.d != "") {
                    var arrayObj = data.d.split('|');
                    //alert(arrayObj);
                    if (arrayObj.length == 2) {
                        $("#tb3").html(arrayObj[0]);
                        Hierarchy(arrayObj[1]);
                    }
                }
                else { $("#tb3").html("<tr><td colspan='100'>无数据</td></tr>"); }
                $("[data-name='tablesorter_3']").trigger("update", [true]);
            }, function () { });
        }

        var loadData = function () {
            var dto = {
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                UserId: "<%=UserId%>",
                ClassID: "<%=ClassID%>",
                x: Math.random()
            };
            $.ajaxWebService("ComparisonBetweenClasses.aspx/StatsClassHW_Subsection", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    if (data.d != "") {
                        var json = $.parseJSON(data.d);
                        $("#tb4").html(json.thead);
                        $("#tb5").html(json.tbody);
                    }
                }
                else { $("#tb5").html("<tr><td colspan='100'>无数据</td></tr>"); }
            }, function () { });
        }


        var GetDatePic = function (DataS) {
            $('#LevelCharts').highcharts({
                chart: {
                    type: 'line',
                },
                xAxis: {
                    categories: ['优秀', '良好', '中等', '及格', '不及格'],
                    title: {
                        text: '等级分布'
                    },
                },
                yAxis: {
                    title: {
                        text: '数量/人'
                    }
                },
                plotOptions: {
                    column: {
                        dataLabels: {
                            enabled: true
                        }
                    }
                },
                tooltip: {
                    valueSuffix: '人'
                },
                series: $.parseJSON(DataS)
            });
        }
        var Hierarchy = function (DateSourse) {
            //层次分布对比
            $('#levelCharts1').highcharts({
                chart: {
                    type: 'column'
                },
                xAxis: {
                    categories: ['第一层次', '第二层次', '第三层次', '第四层次'],
                    title: {
                        text: '层次分布'
                    }
                },
                yAxis: {
                    min: 0,
                    max: 100,
                    title: {
                        text: '占比/%'
                    },
                    labels: {
                        formatter: function () {
                            return this.value + '%'
                        }
                    }
                },
                plotOptions: {
                    column: {
                        stacking: 'percent',
                        dataLabels: {
                            enabled: true,
                            formatter: function () {
                                return this.y + '人'
                            }
                        }
                    }
                },
                tooltip: {
                    valueSuffix: '人'
                },
                series: $.parseJSON(DateSourse)
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Container" runat="server">
    <ul class="nav nav-tabs mb">
        <li class="active"><a href="ComparisonBetweenClasses.aspx?ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>&HomeWork_ID=<%=HomeWork_ID %><%=StrTemp %>">整体成绩对比</a></li>
        <li><a href="TQScoreComparison.aspx?ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>&HomeWork_ID=<%=HomeWork_ID %><%=StrTemp %>">小题得分对比</a></li>
        <li><a href="KPScoreComparison.aspx?ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>&HomeWork_ID=<%=HomeWork_ID %><%=StrTemp %>">知识点得分对比</a></li>
    </ul>
    <h4>班级成绩概览</h4>
    <div class="panel panel-default table_list">
        <table class="table table-bordered text-center" data-name="tablesorter_1">
            <thead>
                <tr>
                    <td>班级</td>
                    <td>平均分</td>
                    <td>最高分</td>
                    <td>最低分</td>
                    <td>中位数</td>
                    <td>众数</td>
                    <td>已交/未交</td>
                    <td>已改/未改</td>
                </tr>
            </thead>
            <tbody id="tb1">
            </tbody>
        </table>
    </div>
    <h4>等级分布对比</h4>
    <div class="row">
        <div class="col-xs-8">
            <div class="panel panel-default table_list">
                <table class="table table-bordered test-center" data-name="tablesorter_2">
                    <thead>
                        <tr>
                            <th>班级</th>
                            <th>优秀</th>
                            <th>良好</th>
                            <th>中等</th>
                            <th>及格</th>
                            <th>不及格</th>
                            <th>合计</th>
                        </tr>
                    </thead>
                    <tbody id="tb_2_01"></tbody>
                    <tfoot id="tb_2_02"></tfoot>
                </table>
            </div>
        </div>
        <div class="col-xs-4">
            <div class="grade_chart" id="LevelCharts" style="width: 100%; height: 245px;"></div>
        </div>
    </div>
    <h4>层次分布对比</h4>
    <div class="row">
        <div class="col-xs-8">
            <table class="table table-bordered text-center" data-name="tablesorter_3">
                <thead>
                    <tr>
                        <td>班级</td>
                        <td>第一层次</td>
                        <td>第二层次</td>
                        <td>第三层次</td>
                        <td>第四层次</td>
                    </tr>
                </thead>
                <tbody id="tb3">
                </tbody>
            </table>
        </div>
        <div class="col-xs-4">
            <div class="grade_chart" id="levelCharts1" style="width: 100%; height: 245px;"></div>
        </div>
    </div>
    <h4>分段分布</h4>
    <table class="table table-bordered text-center">
        <thead id="tb4">
        </thead>
        <tbody id="tb5">
        </tbody>
    </table>
</asp:Content>
