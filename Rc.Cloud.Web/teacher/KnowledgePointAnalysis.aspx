<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="KnowledgePointAnalysis.aspx.cs" Inherits="Rc.Cloud.Web.teacher.KnowledgePointAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- 二级导航 -->
    <div class="header_subnav">
        <ul>
            <li class="userinfo">我是老师：laoshi1<asp:Literal runat="server" ID="ltlTrueName"></asp:Literal></li>
            <li><a href="KnowledgePointAnalysis.aspx" class="active">知识点分析</a></li>
            <li><a href="ScoreAnalysis.aspx">成绩分析</a></li>
            <li><a href="ScoreRank.aspx">成绩排名</a></li>
        </ul>
    </div>
    <!-- 内容 -->
    <div class="main_box">
        <div class="container" data-name="main-auto">
            <div class="container_box">
                <h2 class="column_title">知识点分析</h2>
                <table class="table_list align_center">
                    <thead>
                        <tr>
                            <td>题号</td>
                            <td>测量目标</td>
                            <td class="align_left">考察内容</td>
                            <td>难易度</td>
                            <td>分值</td>
                            <td>平均分</td>
                            <td>班级错误率</td>
                            <td>知识点掌握情况</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>1</td>
                            <td>A标记</td>
                            <td class="align_left">现代汉语普通话常用字字音的标记</td>
                            <td>一般</td>
                            <td>3.00</td>
                            <td>3</td>
                            <td>20%</td>
                            <td>需努力</td>
                        </tr>
                        <tr>
                            <td>2</td>
                            <td>A标记</td>
                            <td class="align_left">现代汉语普通话常用字字音的标记</td>
                            <td>一般</td>
                            <td>3.00</td>
                            <td>3</td>
                            <td>20%</td>
                            <td>需努力</td>
                        </tr>
                        <tr>
                            <td>3</td>
                            <td>A标记</td>
                            <td class="align_left">现代汉语普通话常用字字音的标记</td>
                            <td>一般</td>
                            <td>3.00</td>
                            <td>3</td>
                            <td>20%</td>
                            <td>需努力</td>
                        </tr>
                        <tr>
                            <td>4</td>
                            <td>A标记</td>
                            <td class="align_left">现代汉语普通话常用字字音的标记</td>
                            <td>一般</td>
                            <td>3.00</td>
                            <td>3</td>
                            <td>20%</td>
                            <td>需努力</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            //导航默认状态
            $(".nav li:eq(4) a").addClass("active");
        })
    </script>
</asp:Content>
