<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentAnalysisReportsNew.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.StudentAnalysisReportsNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>答题分析</title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/tablesorter/js/jquery.tablesorter.min.js"></script>
    <script src="../plugin/highcharts/js/highcharts.js"></script>
    <script src="../plugin/highcharts/js/highcharts-more.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            Istrue = "<%=IsTrue%>";
            if (Istrue != "" && Istrue != undefined && Istrue != null) {
                Handel('2', '数据不存在或数据已更新,请关闭页面重新进入.');
            }
            else {

            }


            $("[data-name='tablesorter']").tablesorter({
                headers: {
                    0: { sorter: false }
                }
            });

            $('#chart').highcharts({
                chart: {
                    polar: true,
                    type: 'line'
                },
                title: {
                    text: false
                },
                pane: {
                    size: '80%'
                },
                xAxis: {
                    categories: [<%=strKPName%>],
                    tickmarkPlacement: 'on',
                    lineWidth: 0
                },
                yAxis: {
                    gridLineInterpolation: 'polygon',
                    lineWidth: 0,
                    min: 0,
                    max: 100
                },
                tooltip: {
                    shared: true,
                    pointFormat: '<span style="color:{series.color}">{series.name}: <b>{point.y:,.2f}%</b><br/>'
                },
                legend: {
                    enabled: false,
                    align: 'right',
                    verticalAlign: 'top',
                    y: 70,
                    layout: 'vertical'
                },
                series: [{
                    name: '知识点掌握情况',
                    data: [<%=strKPScoreRate%>],
                    pointPlacement: 'on'
                }],
                credits: {
                    enabled: false     //不显示LOGO 
                }
            });
        })
    </script>
</head>
<body class="body_bg">
    <ul class="res_fixed_sidebar">
        <%=link %>
    </ul>
    <div class="container pb relative">
        <div class="res_info">
            <h2 class="res_title">
                <asp:Literal ID="ltlHomeWork_Name" runat="server"></asp:Literal></h2>
            <div class="res_desc text-center text-muted">
                <span>年级：<asp:Literal ID="ltlGradeName" runat="server"></asp:Literal></span>
                <span>班级：<asp:Literal ID="ltlClassName" runat="server"></asp:Literal></span>
                <span>学生：<asp:Literal ID="ltlStudentName" runat="server"></asp:Literal></span>
                <span>满分：<asp:Literal ID="ltlHWScore" runat="server"></asp:Literal>分</span>
                <span>得分：<asp:Literal ID="ltlStudentScore" runat="server"></asp:Literal>分</span>
            </div>
        </div>
        <h4>得分概况</h4>
        <table class="table table-bordered text-center">
            <thead>
                <tr>
                    <td>平均分</td>
                    <td>最低分</td>
                    <td>最高分</td>
                    <td>我的得分</td>
                    <td>班级排名</td>
                    <td>等级分布</td>
                    <td>层次分布</td>
                    <td>班级人数</td>
                </tr>
            </thead>
            <tbody>
                <asp:Literal ID="ltlHw_Score" runat="server"></asp:Literal>
            </tbody>
        </table>

        <h4 class="pt">本次作业知识点掌握情况分析</h4>
        <div class="row">
            <div class="col-xs-6">
                <table class="table table-bordered" data-name='tablesorter'>
                    <thead>
                        <tr>
                            <td>知识点</td>
                            <td>正确题号</td>
                            <td>错误题号</td>
                            <td>知识点掌握度</td>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal ID="ltlHW_KP" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>
            <div class="col-xs-6">
                <div id="chart" style="width: 100%; height: 400px;"></div>
            </div>
        </div>

    </div>
</body>
</html>

