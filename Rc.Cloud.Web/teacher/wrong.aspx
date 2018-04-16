<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="wrong.aspx.cs" Inherits="Homework.teacher.wrong" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../plug-in/layer/layer.js" type="text/javascript"></script>
    <script>
        $(function () {
            $('.data_list tbody td').live('mouseover', function () {
                var dataVal = $(this).attr('data-val');
                if (dataVal) {
                    layer.tips(dataVal, $(this), {
                        tips: [4, '#F90']
                    });
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--子导航-->
    <div class="w_fluid sub_nav">
        <div class="w">
            <ul>
                <li class="userinfo">我是老师：张三</li>
                <li><a href="##">最新作业</a></li>
                <li><a href="##">历史作业</a></li>
                <li><a href="##" class="active">错题本</a></li>
            </ul>
        </div>
    </div>
    <div class="w clearfix w_main">
        <div class="layout_main">
            <div class="left_tree">
                <dl>
                    <dt><a href="##">高三一班<i class="fa fa-angle-right"></i></a></dt>
                </dl>
                <ul class="left_homework_list">
                    <li><a href="##" class="active"><b>07-21</b>集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷（A）.dsc</a></li>
                    <li><a href="##"><b>07-21</b>集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷（A）.dsc</a></li>
                    <li><a href="##"><b>07-21</b>集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷（A）.dsc</a></li>
                    <li><a href="##"><b>07-21</b>集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷（A）.dsc</a></li>
                    <li><a href="##"><b>07-21</b>集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷（A）.dsc</a></li>
                    <li><a href="##"><b>07-21</b>集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷（A）.dsc</a></li>
                    <li><a href="##"><b>07-21</b>集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷（A）.dsc</a></li>
                    <li><a href="##"><b>07-21</b>集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷（A）.dsc</a></li>
                    <li><a href="##"><b>07-21</b>集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷集合测试卷（A）.dsc</a></li>
                </ul>
            </div>
            <div class="data_list">
                <table>
                    <thead>
                        <tr>
                            <td width="40">题号</td>
                            <td width="60">题型</td>
                            <td>测量目标</td>
                            <td>考查内容</td>
                            <td width="60">难易度</td>
                            <td width="60">分值</td>
                            <td width="60">正确率</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>1</td>
                            <td>单选题</td>
                            <td>领会集合的基本数学思想</td>
                            <td>集合的概念</td>
                            <td>容易</td>
                            <td>95 分</td>
                            <td data-val="未做：12">0.02%</td>
                        </tr>
                        <tr>
                            <td>5</td>
                            <td>单选题</td>
                            <td>领会集合的基本数学思想</td>
                            <td>集合的概念</td>
                            <td>容易</td>
                            <td>95 分</td>
                            <td data-val="未做：12<br>错误：3">0.02%</td>
                        </tr>
                    </tbody>
                </table>
                <div class="page_num clearfix">
                    <a>共5页 50 条记录</a>
                    <a href="##">首页</a>
                    <a href="##">上一页</a>
                    <a href="##" class="active">1</a>
                    <a href="##">2</a>
                    <a href="##">下一页</a>
                    <a href="##">末页</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
