<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fourLevelsOfContrast.aspx.cs" Inherits="Rc.Cloud.Web.Principal.fourLevelsOfContrast" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>教学平台</title>
    <link href="../../Styles/style001/Style.css" rel="stylesheet" />
</head>
<body>

    <ul class="ev_sidebar">
        <li><a href="成绩概览.html" class="active">班级成绩对比</a></li>
        <li><a href="知识点掌握情况列表.html">双向细目表</a></li>
        <li><a href="讲评报告.html">知识点得分对比</a></li>
        <li><a href="学生成绩跟踪表.html">小班得分对比</a></li>
        <li><a href="班级间成绩对比.html">讲评报告</a></li>
        <li><a href="班级间成绩对比.html">学生成绩跟踪与评价</a></li>
    </ul>

    <div class="container">
        <div class="hw_info">
            <h2>
                <asp:Literal ID="ltlHomeWork_Name" runat="server"></asp:Literal></h2>
            <div class="hw_desc">
                年级：<asp:Literal ID="ltlGrade" runat="server"></asp:Literal>年级      学科：<asp:Literal ID="ltlSubjectName" runat="server"></asp:Literal>
                满分：
                <asp:Literal ID="ltlSumSore" runat="server"></asp:Literal>分
            </div>
            <a href="javascript:closeWP();" class="close_page"><i class="fa fa-close"></i>关闭页面</a>
        </div>
    </div>

    <div class="container mt10">
        <div class="p20">
            <div class="submenu clearfix pb10">
                <ul class="submenu_bar">
                    <li><a href="成绩概览.html">成绩概览</a></li>
                    <li><a href="年级排名.html">年级排名</a></li>
                    <li><a href="平均分对比.html">平均分对比</a></li>
                    <li><a href="等级分布对比.html">等级分布对比</a></li>
                    <li><a href="分段统计对比.html">分段统计对比</a></li>
                    <li><a href="四个层次对比.html" class="active">四个层次对比</a></li>
                </ul>
            </div>
            <!-- 四个层次对比 -->
            <div class="pt10 pb20">
                <h2 class="column_title">四个层次对比
                    <ul class="list_chart clearfix">
                        <li data-tab-bar="list_to_chart" class="active"><a href='##'>列表查看</a></li>
                        <li data-tab-bar="list_to_chart"><a href='##'>图表查看</a></li>
                    </ul>
                </h2>
                <div data-tab-box="list_to_chart">
                    <table class="table table-bordered">
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
                        <tbody>
                            <tr>
                                <td>年级（120人）</td>
                                <td>20%（8人）</td>
                                <td>35%（14人）</td>
                                <td>30%（12人）</td>
                                <td>15%（6人）</td>
                                <!-- <td class="table_opera">
                                <a href="##">查看</a>
                            </td> -->
                            </tr>
                            <tr>
                                <td>1班（40人）</td>
                                <td>25%（10人）</td>
                                <td>30%（12人）</td>
                                <td>35%（14人）</td>
                                <td>10%（4人）</td>
                                <!-- <td class="table_opera">
                                <a href="##">查看</a>
                            </td> -->
                            </tr>
                            <tr>
                                <td>2班（40人）</td>
                                <td>20%（8人）</td>
                                <td>35%（14人）</td>
                                <td>30%（12人）</td>
                                <td>15%（6人）</td>
                                <!-- <td class="table_opera">
                                <a href="##">查看</a>
                            </td> -->
                            </tr>
                            <tr>
                                <td>3班（40人）</td>
                                <td>20%（8人）</td>
                                <td>35%（14人）</td>
                                <td>30%（12人）</td>
                                <td>15%（6人）</td>
                                <!-- <td class="table_opera">
                                <a href="##">查看</a>
                            </td> -->
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div data-tab-box="list_to_chart" id="charts" style="width: 1160px; height: 500px;"></div>
            </div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript" src="./../../Scripts/js001/jquery.min-1.8.2.js"></script>
<script type="text/javascript" src="../../Scripts/Highcharts/js/highcharts.js"></script>
<script type="text/javascript">
    //等级分布对比
    $('#charts').highcharts({
        chart: {
            type: 'column',
        },
        xAxis: {
            categories: ['1班', '2班', '3班', '4班', '5班', '6班', '7班', '8班', '9班', '10班', '11班', '12班'],
            title: {
                text: '班级名称'
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
        series: [
            {
                name: '第一层次',
                data: [49, 71, 76, 99, 100, 76, 35, 48, 62, 94, 95, 54]
            },
            {
                name: '第二层次',
                data: [83, 78, 98, 93, 56, 84, 55, 54, 91, 83, 56, 92]
            },
            {
                name: '第三层次',
                data: [83, 78, 98, 93, 56, 84, 55, 54, 91, 83, 56, 92]
            },
            {
                name: '第四层次',
                data: [83, 78, 98, 93, 56, 84, 55, 54, 91, 83, 56, 92]
            }
        ]
    });
</script>
