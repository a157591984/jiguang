<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HisKlgAnalysisSource.aspx.cs" Inherits="Rc.Cloud.Web.Principal.HisKlgAnalysisSource" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=KPNameEncode %></title>
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../plugin/highcharts/js/highcharts.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../js/common.js"></script>
    <script src="../js/function.js"></script>
    <script src="../js/highcharts.config.js"></script>
    <script src="../js/jquery-jtemplates.js"></script>
    <script src="../js/json2.js"></script>
    <script type="text/javascript">
        $(function () {
            loadData();//初始化数据
        })
        var loadData = function () {
            var $_objBox = $("#listBox");
            var objBox = "listBox";
            var $_objList = $("#lists");
            var dto = {
                GradeId: "<%=GradeId%>",
                SubjectID: "<%=SubjectID%>",
                ClassID: "<%=ClassID%>",
                KPName: "<%=KPName%>",
                DateData: "<%=DateData%>",
                DateType: "<%=DateType%>",
                x: Math.random()
            };
            $.ajaxWebService("HisKlgAnalysisSource.aspx/GetHWList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $_objList.setTemplateElement(objBox, null, { filter_data: false });
                    $_objList.processTemplate(json);
                    if (json.list == null || json.list == "") {
                        $_objBox.html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    }
                }
                else {
                    $_objList.html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                }
                Chart(json.strxAxisCategories, json.strSeriesData, json.DataCount);
            }, function () { });
        }

        //图表
        var Chart = function (categories, series, count) {
            var arr = categories.split(',');
            var strDataArr = series.split(',');
            var arrData = new Array();
            for (var i = 0; i < strDataArr.length; i++) {
                arrData.push(parseFloat(strDataArr[i]));
            }
            $('#chart').highcharts({
                chart: {
                    type: 'column',
                    height: 500,
                    width: $('#tab1').width()
                },
                xAxis: {
                    categories: arr,
                    title: {
                        text: '知识点名称'
                    },
                    labels: {
                        step: Math.round(count / 8)
                    }
                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    column: {
                        dataLabels: {
                            enabled: true,
                            format: '{y}%'
                        }
                    }
                },
                yAxis: {
                    title: {
                        text: '平均得分率/%'
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}: {point.y:.2f}%</span>',
                    pointFormat: '',
                    footerFormat: '',
                    shared: true,
                    useHTML: true
                },
                series: [
                    {
                        data: arrData
                    }
                ]
            });
        }
    </script>
</head>
<body class="body_bg">
    <form id="form1" runat="server">
        <div class="container">
            <div class="test_paper_name_panel">
                <div class="panel_heading">
                    <div class="panel_title"><%=KPNameEncode %></div>
                </div>
            </div>
            <ul class="nav nav-tabs bg_white pv mt">
                <li class="active"><a href="#tab1" data-toggle="tab">列表查看</a></li>
                <li><a href="#tab2" data-toggle="tab">图表查看</a></li>
            </ul>

            <div class="panel">
                <div class="panel-body">
                    <div class="tab-content">
                        <div class="tab-pane active" id="tab1">
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th width="400">作业名称</th>
                                        <th width="148">布置日期</th>
                                        <th width="60">分值</th>
                                        <th width="100">平均得分率</th>
                                        <th>对应习题</th>
                                    </tr>
                                </thead>
                                <tbody id="lists">
                                </tbody>
                            </table>
                            <textarea id="listBox" class="hidden">
                            {#foreach $T.list as record}
                            <tr>
                                <td>{$T.record.HomeWork_Name}</td>
                                <td>{$T.record.HomeWorkCreateTime}</td>
                                <td>{$T.record.KPScoreSum}</td>
                                <td>{$T.record.KPScoreAvgRate}</td>
                                <td>{$T.record.TestQuestionNums}</td>
                            </tr>
                            {#/for}
                            </textarea>
                        </div>
                        <div class="tab-pane" id="tab2">
                            <div id="chart"></div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
