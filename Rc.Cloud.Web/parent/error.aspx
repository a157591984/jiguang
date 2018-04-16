<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/parent.Master" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="Rc.Cloud.Web.parent.error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/js001/jquery.min-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../scripts/jQuery_Dialog.js"></script>
    <script type="text/javascript" src="../scripts/jquery.easydrag.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".nav li:eq(0) a").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--子导航-->
    <div class="sub_nav">
        <ul>
            <li class="userinfo">我是家长：<asp:Literal runat="server" ID="ltlUserName"></asp:Literal></li>
            <li><a href="comperhensive.aspx">综合排名</a></li>
            <li><a href="subject.aspx">学科排名</a></li>
            <li><a href="examination.aspx">考试排名</a></li>
            <li><a href="homework.aspx">作业排名</a></li>
            <li><a href="error.aspx" class="active">错题情况</a></li>
        </ul>
    </div>
    <!--内容-->
    <div class="container">
        <!--左侧-->
        <div class="left_tree">
            <dl>
                <dd><a href='##' class="active">考试</a></dd>
                <dd><a href='##'>作业</a></dd>
            </dl>
        </div>
        <!--右侧-->
        <div class="data_list">
            <table>
                <thead>
                    <tr>
                        <td>名称</td>
                        <td>开始时间</td>
                        <td>结束时间</td>
                        <td>状态</td>
                        <td>错题详情</td>
                    </tr>
                </thead>
                <tbody id="tbRes">
                    <tr>
                        <td>全真综合模拟测试卷（一）</td>
                        <td>2016-01-04 00:00</td>
                        <td>2016-01-04 00:00</td>
                        <td>已交</td>
                        <td><a href="##" target="_blank">错题详情</a></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>

