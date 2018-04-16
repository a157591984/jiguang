<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="ScoreAnalysis.aspx.cs" Inherits="Rc.Cloud.Web.teacher.ScoreAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- 二级导航 -->
    <div class="header_subnav">
        <ul>
            <li class="userinfo">我是老师：laoshi1<asp:Literal runat="server" ID="ltlTrueName"></asp:Literal></li>
            <li><a href="KnowledgePointAnalysis.aspx">知识点分析</a></li>
            <li><a href="ScoreAnalysis.aspx" class="active">成绩分析</a></li>
            <li><a href="ScoreRank.aspx">成绩排名</a></li>
        </ul>
    </div>
    <!-- 内容 -->
    <div class="main_box">
        <div class="container" data-name="main-auto">
            <div class="container_box">
                <h2 class="column_title">班的得分情况</h2>
                <table class="table_list align_center">
                    <thead>
                        <tr>
                            <td>班的人数</td>
                            <td>提交作业人数</td>
                            <td>平均分</td>
                            <td>标准差</td>
                            <td>最低分</td>
                            <td>最高分</td>
                            <td>中数</td>
                            <td>众数</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>50</td>
                            <td>50</td>
                            <td>75</td>
                            <td>7.8</td>
                            <td>25</td>
                            <td>135</td>
                            <td>75</td>
                            <td>75、65</td>
                        </tr>
                    </tbody>
                </table>
                <h2 class="column_title mt20">分数段分布情况</h2>
                <table class="table_list align_center">
                    <thead>
                        <tr>
                            <td>分数段</td>
                            <td>频数</td>
                            <td>累计频数</td>
                            <td>相对频数（%）</td>
                            <td>累计相对频（%）</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>≥131</td>
                            <td>1</td>
                            <td>1</td>
                            <td>0.02</td>
                            <td>0.02</td>
                        </tr>
                        <tr>
                            <td>121-130</td>
                            <td>1</td>
                            <td>2</td>
                            <td>0.02</td>
                            <td>0.04</td>
                        </tr>
                        <tr>
                            <td>111-120</td>
                            <td>2</td>
                            <td>4</td>
                            <td>0.04</td>
                            <td>0.08</td>
                        </tr>
                        <tr>
                            <td>101-110</td>
                            <td>5</td>
                            <td>9</td>
                            <td>0.1</td>
                            <td>0.18</td>
                        </tr>
                        <tr>
                            <td>91- 100</td>
                            <td>3</td>
                            <td>12</td>
                            <td>0.06</td>
                            <td>0.24</td>
                        </tr>
                        <tr>
                            <td>81-90</td>
                            <td>8</td>
                            <td>20</td>
                            <td>0.16</td>
                            <td>0.4</td>
                        </tr>
                        <tr>
                            <td>71-80</td>
                            <td>10</td>
                            <td>30</td>
                            <td>0.2</td>
                            <td>0.6</td>
                        </tr>
                        <tr>
                            <td>61-70</td>
                            <td>10</td>
                            <td>40</td>
                            <td>0.2</td>
                            <td>0.8</td>
                        </tr>
                        <tr>
                            <td>51-60</td>
                            <td>2</td>
                            <td>42</td>
                            <td>0.04</td>
                            <td>0.88</td>
                        </tr>
                        <tr>
                            <td>41-50</td>
                            <td>2</td>
                            <td>44</td>
                            <td>0.04</td>
                            <td>0.88</td>
                        </tr>
                        <tr>
                            <td>31-40</td>
                            <td>5</td>
                            <td>49</td>
                            <td>0.1</td>
                            <td>0.98</td>
                        </tr>
                        <tr>
                            <td>≤30</td>
                            <td>1</td>
                            <td>50</td>
                            <td>0.02</td>
                            <td>1</td>
                        </tr>
                    </tbody>
                </table>
                <div id="chart_1" class="mt20"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript" src="./../Scripts/Highcharts/highcharts.js"></script>
    <script type="text/javascript">
        $(function () {
            //导航默认状态
            $(".nav li:eq(4) a").addClass("active");
            //图表
            $('#chart_1').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false
                },
                title: {
                    text: '分数段人数分布频率'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                legend: {},
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            color: '#000000',
                            connectorColor: '#000000',
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    name: '累计相对频数',
                    data: [
                        ['≥131', 1],
                        ['121-130', 1],
                        ['111-120', 2],
                        ['101-110', 5],
                        ['91-100', 3],
                        ['81-90', 8],
                        ['71-80', 10],
                        ['61-70', 10],
                        ['51-60', 2],
                        ['41-50', 2],
                        ['31-40', 5],
                        ['≤30', 1]
                    ]
                }]
            });
        });
    </script>
</asp:Content>
