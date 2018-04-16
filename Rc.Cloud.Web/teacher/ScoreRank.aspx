<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="ScoreRank.aspx.cs" Inherits="Rc.Cloud.Web.teacher.ScoreRank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- 二级导航 -->
    <div class="header_subnav">
        <ul>
            <li class="userinfo">我是老师：laoshi1<asp:Literal runat="server" ID="ltlTrueName"></asp:Literal></li>
            <li><a href="KnowledgePointAnalysis.aspx">知识点分析</a></li>
            <li><a href="ScoreAnalysis.aspx">成绩分析</a></li>
            <li><a href="ScoreRank.aspx" class="active">成绩排名</a></li>
        </ul>
    </div>
    <!-- 内容 -->
    <div class="main_box">
        <div class="container" data-name="main-auto">
            <div class="container_box">
                <h2 class="column_title">成绩排名</h2>
                <table class="table_list align_center">
                    <thead>
                        <tr>
                            <td>班</td>
                            <td>姓名</td>
                            <td>总分</td>
                            <td>排名</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>高三一班</td>
                            <td>高斯</td>
                            <td>135</td>
                            <td>1</td>
                        </tr>
                        <tr>
                            <td>高三一班</td>
                            <td>王强</td>
                            <td>120</td>
                            <td>2</td>
                        </tr>
                        <tr>
                            <td>高三一班</td>
                            <td>徐晓燕</td>
                            <td>115</td>
                            <td>2</td>
                        </tr>
                        <tr>
                            <td>高三一班</td>
                            <td>李菲</td>
                            <td>109</td>
                            <td>3</td>
                        </tr>
                        <tr>
                            <td>高三一班</td>
                            <td>刘玉杰</td>
                            <td>102</td>
                            <td>4</td>
                        </tr>
                        <tr>
                            <td>高三一班</td>
                            <td>王倩</td>
                            <td>97</td>
                            <td>5</td>
                        </tr>
                        <tr>
                            <td>高三一班</td>
                            <td>张文婷</td>
                            <td>90</td>
                            <td>6</td>
                        </tr>
                        <tr>
                            <td>高三一班</td>
                            <td>李伟</td>
                            <td>85</td>
                            <td>7</td>
                        </tr>
                        <tr>
                            <td>高三一班</td>
                            <td>闫云冲</td>
                            <td>80</td>
                            <td>8</td>
                        </tr>
                        <tr>
                            <td>高三一班</td>
                            <td>刘瑶</td>
                            <td>73</td>
                            <td>9</td>
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
        });
    </script>
</asp:Content>
