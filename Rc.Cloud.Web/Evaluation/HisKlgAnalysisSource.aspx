<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HisKlgAnalysisSource.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.HisKlgAnalysisSource" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=KPName %></title>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/common.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../plugin/highcharts/js/highstock.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../js/highcharts.config.js"></script>
    <script src="../js/jq.pagination.js"></script>
    <script src="../js/jquery-jtemplates.js"></script>
    <script src="../js/json2.js"></script>
    <script src="../js/function.js"></script>
    <script type="text/javascript">
        $(function () {
            loadData();//初始化数据
        })
        var loadData = function () {
            var $_objBox = $("#listTpl");
            var objBox = "listTpl";
            var $_objList = $("#listBox");
            var $_pagination = $(".pagination_container");
            var dto = {
                TeacherID: "<%=UserId%>",
                SubjectID: "<%=SubjectID%>",
                ClassID: "<%=ClassID%>",
                KPName: "<%=KPNameEncode%>",
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
            if (!categories) return false;
            var arr = categories.split(',');
            var strDataArr = series.split(',');
            var arrData = new Array();
            for (var i = 0; i < strDataArr.length; i++) {
                arrData.push(parseFloat(strDataArr[i]));
            }
            $('#chart').highcharts({
                chart: {
                    type: 'column',
                    width: $('#tab1').width(),
                    height: 500
                },
                xAxis: {
                    categories: arr,
                    title: {
                        text: '知识点名称'
                    },
                    labels: {
                        rotation: -45
                    },
                    max: (arr.length > 5) ? 5 : null,
                    min: (arr.length > 5) ? 0 : null
                },
                scrollbar: {
                    enabled: (arr.length > 8) ? true : false
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
                    min: 0,
                    max: 100,
                    title: {
                        text: '平均得分率/%'
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}: {point.y:.1f}%</span>',
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
<body>
    <form id="form1" runat="server">
        <ul class="nav nav-tabs">
            <li role="presentation" class="active"><a href="#tab1" data-toggle="tab">列表查看</a></li>
            <li role="presentation"><a href="#tab2" data-toggle="tab">图表查看</a></li>
        </ul>
        <div class="panel">
            <div class="panel-body">
                <div class="tab-content">
                    <div role="tabpanel" class="tab-pane active" id="tab1">
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>作业名称</th>
                                    <th id="tdHWTime" runat="server">布置日期</th>
                                    <th>分值</th>
                                    <th>平均得分率</th>
                                    <th class="text-left">对应习题</th>
                                </tr>
                            </thead>
                            <tbody id="listBox">
                            </tbody>
                        </table>
                        <textarea id="listTpl" class="hidden">
                                {#foreach $T.list as record}
                                <tr>
                                    <td>{$T.record.HomeWork_Name}</td>
                                    {#if $T.record.ClassID!=''}<td>{$T.record.HomeWorkCreateTime}</td>{#/if}
                                    <td>{$T.record.KPScoreSum}</td>
                                    <td>{$T.record.KPScoreAvgRate}</td>
                                    <td>{$T.record.TestQuestionNums}</td>
                                </tr>
                                {#/for}
                                </textarea>
                    </div>

                    <div role="tabpanel" class="tab-pane" id="tab2">
                        <div id="chart"></div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
