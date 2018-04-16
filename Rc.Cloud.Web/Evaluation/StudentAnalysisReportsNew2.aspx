<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentAnalysisReportsNew2.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.StudentAnalysisReportsNew2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>答题分析</title>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/common.js"></script>
    <script src="../js/jquery-jtemplates.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../plugin/highcharts/js/highcharts.js"></script>
    <script src="../js/highcharts.config.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../js/function.js"></script>
    <script type="text/javascript">
        $(function () {
            Istrue = "<%=IsTrue%>";
            if (Istrue != "" && Istrue != undefined && Istrue != null) {
                layer.msg('数据不存在或数据已更新,请关闭页面重新进入.', { time: 2000, icon: 2 });
            }
            else {

            }
            GetTQ_Type();
            GetStatsStuHW_KPAll();
            //loadDataKP();
            loadDataTQ();
            var strComplexityText = "<%=strComplexityText%>"
            var data = [];
            $.each(strComplexityText.split('|'), function (e, v) {
                var v = v.split(",");
                data.push([v[0], parseFloat(v[1])]);
            })
            console.log(data);

            $('#chart1').highcharts({
                credits: {
                    enabled: false
                },
                title: {
                    text: '难度分布'
                },
                tooltip: {
                    headerFormat: '{series.name}<br>',
                    pointFormat: '{point.name}: <b>{point.percentage:.2f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: false
                        },
                        showInLegend: true
                    }
                },
                series: [{
                    type: 'pie',
                    name: '试题难度占比',
                    data: data,
                    dataLabels: {
                        enabled: true,
                        format: '{point.y:.2f}%'
                    },
                }]
            });



            $('#chart3').highcharts({
                creadits: {
                    enabled: false,
                },
                chart: {
                    type: 'bar'
                },
                title: {
                    text: null
                },
                xAxis: {
                    categories: [<%=strKPName%>],
                    title: {
                        text: null
                    }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '掌握程度',
                    }
                },
                tooltip: {
                    valueSuffix: ' %'
                },
                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true,
                            allowOverlap: true
                        }
                    }
                },
                legend: {
                    enabled: false
                },
                credits: {
                    enabled: false
                },
                series: [{
                    name: '掌握程度',
                    data: [<%=strKPScoreRate%>],
                    dataLabels: {
                        enabled: true,
                        format: '{point.y:.0f}%'
                    },
                }]
            });

            $('#chart4').highcharts({
                credits: {
                    enabled: false
                },
                title: {
                    text: null
                },
                tooltip: {
                    headerFormat: '',
                    pointFormat: '{point.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: false
                        },
                        showInLegend: true
                    }
                },
                series: [{
                    type: 'pie',
                    //name: '试题难度占比',
                    data: [
                        ['得分点比例', <%=TQTrueAvg%>],
                        ['失分点比例', <%=TQFalseAvg%>]
                    ],
                    dataLabels: {
                        enabled: true,
                        format: '{point.y:.1f}%'
                    },
                }]
            });
            var tqNum = "<%=strTQNum%>";
            var tqScore = "<%=strTQScore%>";
            var arr = new Array();
            for (var i = 0; i < tqScore.split(',').length; i++) {
                arr.push(parseFloat(tqScore.split(',')[i]));
            }
            console.log(tqNum.split(','));
            $('#chart5').highcharts({
                creadits: {
                    enabled: false
                },
                chart: {
                    type: 'bar',
                },
                title: {
                    text: null
                },
                xAxis: {
                    categories: tqNum.split(','),
                    title: {
                        text: '题号'
                    }
                },
                yAxis: {
                    title: {
                        text: '得分'
                    },
                    minTickInterval: 1
                },
                tooltip: {
                    enabled: false
                },
                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true,
                            allowOverlap: true
                        }
                    }
                },
                legend: {
                    enabled: false
                },
                credits: {
                    enabled: false
                },
                series: [{
                    data: arr
                }]
            });
        })

        var GetTQ_Type = function () {
            var dto = {
                HomeWork_Id: "<%=HomeWork_Id%>",
                StudentId: "<%=StudentId%>",
                x: Math.random()
            };
            $.ajaxWebService("StudentAnalysisReportsNew2.aspx/GetTQ_Type", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    var arrayObj = data.d.split('|');
                    //alert(arrayObj);
                    if (arrayObj.length == 2) {
                        GetTQ_TypePic(arrayObj[0], arrayObj[1]);
                    }

                }
                else { }
            }, function () { });
        }
        var GetTQ_TypePic = function (CountTQ_Type, TQ_Type) {
            var arr = new Array();
            for (var i = 0; i < CountTQ_Type.split(',').length; i++) {
                arr.push(parseFloat(CountTQ_Type.split(',')[i]));
            }
            var arr1 = TQ_Type.split(',');
            $('#chart2').highcharts({
                credits: {
                    enabled: false
                },
                chart: {
                    type: 'column'
                },
                title: {
                    text: '题型及试题分布'
                },
                xAxis: {
                    title: {
                        text: '题型'
                    },
                    categories: arr1,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '数量（个）'
                    }
                },
                legend: {
                    enabled: false
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px"><b>{point.key}</b>',
                    pointFormat: '：{point.y} 个</b>',
                    footerFormat: '</span>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        borderWidth: 0
                    }
                },
                series: [{
                    data: arr,
                    dataLabels: {
                        enabled: true,
                        format: '{point.y:.0f}'
                    },
                }]
            });
        }
        var GetStatsStuHW_KPAll = function () {
            var dto = {
                HomeWork_Id: "<%=HomeWork_Id%>",
                StudentId: "<%=StudentId%>",
                x: Math.random()
            };
            $.ajaxWebService("StudentAnalysisReportsNew2.aspx/GetStatsStuHW_KPAll", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template_1", null, { filter_data: false });
                    $("#tb1").processTemplate(json);

                }
                else {
                    $("#tb1").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");

                }
                if (json.list == null || json.list == "") {

                }
            }, function () { });
        }
        //按知识点分析
        var loadDataKP = function () {

            var $_objBox = $("#list");
            var objBox = "list";
            var $_objList = $("#tb2");
            var dto = {
                HomeWork_Id: "<%=HomeWork_Id%>",
                Student_Id: "<%=StudentId%>",
                x: Math.random()
            };
            $.ajaxWebService("StudentAnalysisReportsNew2.aspx/GetListKP", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $_objList.setTemplateElement(objBox, null, { filter_data: false });
                    $_objList.processTemplate(json);

                }
                else {
                    $_objList.html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");

                }

            }, function () { });
        }
        var loadDataTQ = function () {
            var $_objBox = $("#list3");
            var objBox = "list3";
            var $_objList = $("#tb3");
            var dto = {
                HomeWork_Id: "<%=HomeWork_Id%>",
                Student_Id: "<%=StudentId%>",
                x: Math.random()
            };
            $.ajaxWebService("StudentAnalysisReportsNew2.aspx/GetListTQ", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $_objList.setTemplateElement(objBox, null, { filter_data: false });
                    $_objList.processTemplate(json);
                }
                else {
                    $_objList.html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                }

            }, function () { });
        }
    </script>
</head>
<body class="body_bg">
    <div class="container">
        <div class="fixed_sidebar">
            <ul>
                <%=link %>
            </ul>
        </div>
        <div class="test_paper_name_panel">
            <div class="panel_heading">
                <h3 class="panel_title">
                    <asp:Literal ID="ltlHwName" runat="server"></asp:Literal>
                </h3>
                <ul class="panel_info">
                    <li>年级：<asp:Literal ID="ltlGradeName" runat="server"></asp:Literal></li>
                    <li>班级：<asp:Literal ID="ltlClassName" runat="server"></asp:Literal></li>
                    <li>学科：<asp:Literal ID="ltlSubjectName" runat="server"></asp:Literal></li>
                    <li>共
                        <asp:Literal ID="ltlCountTQ" runat="server"></asp:Literal>
                        道题</li>
                    <li>满分
                        <asp:Literal ID="ltlHwScore" runat="server"></asp:Literal>
                        分</li>
                    <li>得分
                        <asp:Literal ID="ltlStuScore" runat="server"></asp:Literal>
                        分</li>
                </ul>
            </div>
        </div>
        <div class="page_title">试题结构</div>
        <div class="panel mn">
            <div class="panel-body">
                <div class="row">
                    <div class="col-xs-6">
                        <div id="chart1"></div>
                    </div>
                    <div class="col-xs-6">
                        <div id="chart2"></div>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <table class="table table-bordered mn">
                    <thead>
                        <tr>
                            <th>考察知识点</th>
                            <th>题型</th>
                            <th>题号</th>
                            <th>分值</th>
                        </tr>
                    </thead>
                    <tbody id="tb1">
                    </tbody>
                </table>

            </div>
        </div>
        <textarea id="template_1" class="hidden">
    {#foreach $T.list as record}
        <tr>
            <td>{$T.record.KPNameBasic}</td>
            <td>{$T.record.TestType}</td>
            <td>{$T.record.topicNumber}</td>
            <td>{$T.record.SumScore} 分</td>
        </tr>
    {#/for}
    </textarea>
        <div class="page_title">得分情况</div>
        <div class="panel mn">
            <div class="panel-body">
                <table class="table table-bordered text-center mn">
                    <thead>
                        <tr>
                            <th>平均分</th>
                            <th>最低分</th>
                            <th>最高分</th>
                            <th>我的得分</th>
                            <th>班级排名</th>
                            <th>等级分布</th>
                            <th>层次分布</th>
                            <th>班级人数</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal ID="ltlHw_Score" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>
        </div>

        <div class="page_title">知识点掌握情况</div>
        <div class="panel mn">
            <div class="panel-body">
                <div id="chart3"></div>
            </div>
            <div class="panel-body pn pv">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>知识点</th>
                            <th>正确题号</th>
                            <th>错误题号</th>
                            <th>知识点掌握程度</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal ID="ltlHW_KP" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>
        </div>

        <div class="page_title">错题分析</div>
        <div class="panel mn">
            <div class="panel-body">
                <div class="row">
                    <div class="col-xs-4">
                        <div class="pt clearfix">
                            <asp:Literal ID="ltlTQS" runat="server"></asp:Literal>
                        </div>
                        <div>
                            <asp:Literal ID="ltlTQSView" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="col-xs-8">
                        <div id="chart4"></div>
                    </div>
                </div>
            </div>
            <%--<div class="panel-heading">
                <div class="panel-title">薄弱知识点分析</div>
            </div>
            <div class="panel-body pn pv">
                <table class="table table-bordered mn">
                    <thead>
                        <tr>
                            <th>知识点</th>
                            <th>重要程度</th>
                            <th>中考对应分值</th>
                            <th>错题难度</th>
                            <th>题型</th>
                            <th>错题号</th>
                        </tr>
                    </thead>
                    <tbody id="tb2">
                    </tbody>
                </table>
                <textarea id="list" class="hidden">
                {#foreach $T.list as record}
                    <tr>
                        <td>{$T.record.KPNameBasic}</td>
                        <td>{$T.record.KPImportant}</td>
                        <td>{$T.record.GKScore}</td>
                        <td>{$T.record.ComplexityText}</td>
                        <td>{$T.record.TestType}</td>
                        <td>{$T.record.topicNumber}</td>
                    </tr>
                {#/for}
            </textarea>
            </div>--%>
            <div class="panel-heading">
                <div class="panel-title">错题数据分析</div>
            </div>
            <div class="panel-body pn pv">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>错题号</th>
                            <th>知识点</th>
                            <th>重要程度</th>
                            <th>分值</th>
                            <th>得分</th>
                            <th>难度</th>
                            <th>题型</th>
                        </tr>
                    </thead>
                    <tbody id="tb3">
                    </tbody>
                </table>
                <textarea id="list3" class="hidden">
                {#foreach $T.list as record}
                    <tr>
                        <td>{$T.record.topicNumber}</td>
                        <td>{$T.record.KPNameBasic}</td>
                        <td>{$T.record.KPImportant}</td>
                        <td>{$T.record.TQScore}</td>
                        <td>{$T.record.Score}</td>
                         <td>{$T.record.ComplexityText}</td>
                         <td>{$T.record.TestType}</td>
                    </tr>
                {#/for}
              </textarea>
            </div>
        </div>

        <div class="page_title">小题得分情况</div>
        <div class="panel mn">
            <div class="panel-body">
                <div id="chart5"></div>
            </div>
            <div class="panel-body pn pv">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>题号</th>
                            <th>题型</th>
                            <th>知识点</th>
                            <th>难度</th>
                            <th>题目分值</th>
                            <th>得分</th>
                            <th>班级平均分</th>
                            <th>班级平均分得分率</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal ID="ltlHW_TQScore" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>
        </div>

        <div class="page_title">补救提高建议</div>
        <div class="panel mn">
            <div class="panel-heading">
                <div class="panel-title">建议学习的薄弱知识点</div>
            </div>
            <div class="panel-body pn pv">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>章节</th>
                            <th>知识点</th>
                            <th>重要程度</th>
                            <th>升学对应分值</th>
                            <th>未掌握程度</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal ID="ltlImprovingSuggestions" runat="server"></asp:Literal>
                    </tbody>
                </table>
                <p>请同学对以上知识进行补救学习或者请老师进行指导学习，并对该知识点所涉及的典型例题进行复习。</p>
            </div>
            <div class="panel-heading">
                <div class="panel-title">错题重练</div>
            </div>
            <div class="panel-body pn pv pb">

                <asp:Literal ID="ltlWroghtTQ" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
</body>
</html>

