<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="University.aspx.cs" Inherits="Rc.Cloud.Web.teacher.University" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- 二级导航 -->
    <div class="header_subnav">
        <ul>
            <li class="userinfo">我是老师：laoshi1<asp:Literal runat="server" ID="ltlTrueName"></asp:Literal></li>
            <li><a href="AllRankings.aspx">各科排名</a></li>
            <li><a href="University.aspx" class="active">综合排名</a></li>
        </ul>
    </div>
    <!-- 内容 -->
    <div class="main_box">
        <div class="container" data-name="main-auto">
            <div class="container_box">
                <h2 class="column_title">各科排名</h2>
                <div class="searchbar pb20 pt20 clearfix">
                    <span>时间范围</span>
                    <select name="" class="mr20">
                        <option value="">-请选择-</option>
                        <option value="">年度</option>
                        <option value="">上学期</option>
                        <option value="">下学期</option>
                        <option value="">月度</option>
                    </select>
                    <select name="" class="mr20">
                        <option value="">-请选择-</option>
                    </select>
                    <input type="button" value="搜索" class="input_btn" />
                </div>
                <table class="table_list align_center">
                    <thead>
                        <tr>
                            <td>科目</td>
                            <td>姓名</td>
                            <td>分数</td>
                            <td>排名</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>英语</td>
                            <td>刘玉杰</td>
                            <td>135</td>
                            <td>1</td>
                        </tr>
                        <tr>
                            <td>数学</td>
                            <td>刘玉杰</td>
                            <td>120</td>
                            <td>2</td>
                        </tr>
                        <tr>
                            <td>语文</td>
                            <td>刘玉杰</td>
                            <td>115</td>
                            <td>2</td>
                        </tr>
                        <tr>
                            <td>政治</td>
                            <td>刘玉杰</td>
                            <td>109</td>
                            <td>3</td>
                        </tr>
                        <tr>
                            <td>物理</td>
                            <td>刘玉杰</td>
                            <td>102</td>
                            <td>4</td>
                        </tr>
                        <tr>
                            <td>化学</td>
                            <td>刘玉杰</td>
                            <td>97</td>
                            <td>5</td>
                        </tr>
                        <tr>
                            <td>生物</td>
                            <td>张文婷</td>
                            <td>90</td>
                            <td>6</td>
                        </tr>
                        <tr>
                            <td>历史</td>
                            <td>刘玉杰</td>
                            <td>85</td>
                            <td>7</td>
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
