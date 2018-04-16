<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sitemap.aspx.cs" Inherits="Rc.Cloud.Web.test.Sitemap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>网站地图</title>
</head>
<body>
    <ul>
        <h3>单次作业数据分析</h3>
        <li><a href="/Evaluation/StudentAnalysisReportsNew.aspx">答题分析(程序做完了，没有入口，没传参看不到数据)</a></li>
        <h3>多次作业数据分析</h3>
        <li><a href="/student/aggregateAnalysis.aspx" target="_blank">综合作业数据分析</a>
            <ul>
                <li><a href="/student/ErrorDetail.aspx" target="_blank">错题详情</a></li>
            </ul>
        </li>
        <li><a href="/student/DataAnalysisReportMultipleJobError.aspx" target="_blank">多次作业错误题目数据分析报告</a></li>
        <li><a href="/student/RemedialPlan.aspx" target="_blank">提分补救方案</a></li>
    </ul>
</body>
</html>
