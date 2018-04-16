<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentAnalysisReports.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.StudentAnalysisReports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>分析报告</title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            Istrue = "<%=IsTrue%>";
            if (Istrue != "" && Istrue != undefined && Istrue != null) {
                Handel('2', '数据不存在或数据已更新,请关闭页面重新进入.');
            }
            else {

            }
        })
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
                <div class="panel_title">
                    <asp:Literal ID="ltlHomeWork_Name" runat="server"></asp:Literal>
                </div>
                <ul class="panel_info">
                    <li>年级：<asp:Literal ID="ltlGradeName" runat="server"></asp:Literal></li>
                    <li>班级：<asp:Literal ID="ltlClassName" runat="server"></asp:Literal></li>
                    <li>学生：<asp:Literal ID="ltlStudentName" runat="server"></asp:Literal></li>
                    <li>满分：<asp:Literal ID="ltlHWScore" runat="server"></asp:Literal>分</li>
                    <li>得分：<asp:Literal ID="ltlStudentScore" runat="server"></asp:Literal>分</li>
                </ul>
            </div>
        </div>

        <div class="page_title">得分概况</div>
        <div class="panel mn">
            <div class="panel-body">
                <table class="table table-bordered text-center">
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

        <div class="page_title">小题得分</div>
        <div class="panel mn">
            <div class="panel-body">
                <table class="table table-bordered text-center">
                    <thead>
                        <tr>
                            <th>题号</th>
                            <th>题目分数</th>
                            <th>题目难度</th>
                            <th>小题得分</th>
                            <th>班级平均分</th>
                            <th>班级平均得分率</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal ID="ltlHW_TQ" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>
        </div>

        <div class="page_title">知识内容诊断</div>
        <div class="panel mn">
            <div class="panel-body">
                <table class="table table-bordered text-center">
                    <thead>
                        <tr>
                            <th class="text-left" width="280">知识内容</th>
                            <th class="text-left">题号</th>
                            <th width="80">分数</th>
                            <th width="80">得分</th>
                            <th width="90">班级平均分</th>
                            <th width="130">班级平均得分率</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal ID="ltlHW_KP" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>
        </div>

        <div class="page_title">测量目标诊断</div>
        <div class="panel mn">
            <div class="panel-body">
                <table class="table table-bordered text-center">
                    <thead>
                        <tr>
                            <th class="text-left">测量目标</th>
                            <th width="100">题号</th>
                            <th width="80">分数</th>
                            <th width="80">得分</th>
                            <th width="90">班级平均分</th>
                            <th width="130">班级平均得分率</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal ID="ltlHW_TQ_Target" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</body>
</html>

