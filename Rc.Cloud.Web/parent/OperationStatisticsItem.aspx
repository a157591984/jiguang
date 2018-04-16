<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperationStatisticsItem.aspx.cs" Inherits="Rc.Cloud.Web.parent.OperationStatisticsItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../Styles/style001/Style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="hw_info">
                <h2>第一周 【学科】作业情况</h2>
                <a href="javascript:window.close();" class="close_page"><i class="fa fa-close"></i>关闭页面</a>
            </div>
        </div>
        <div class="container mt10">
            <div class="p20">
                <table class="table_list">
                    <thead>
                        <tr>
                            <td>日期</td>
                            <td>作业名称</td>
                            <td>学科</td>
                            <td>任课老师</td>
                            <td>答题时间</td>
                            <td>平均每道题答题时间</td>
                            <td>IP</td>
                            <td>状态</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>2016-01-01</td>
                            <td></td>
                            <td>数学</td>
                            <td>张老师</td>
                            <td>
                                <p>开始 2016-05-17 11:05</p>
                                <p>结束 2016-05-17 11:05</p>
                            </td>
                            <td>3分钟</td>
                            <td>127.0.0.1</td>
                            <td>未批改</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
