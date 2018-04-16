<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/parent.Master" AutoEventWireup="true" CodeBehind="homework.aspx.cs" Inherits="Rc.Cloud.Web.parent.homework" %>

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
            <li><a href="examination.aspx">历次作业及考试排名</a></li>
            <%--<li><a href="homework.aspx" class="active">历次作业排名</a></li>--%>
            <%--<li><a href="error.aspx">错题情况</a></li>--%>
        </ul>
    </div>
    <!--内容-->
    <div class="container">
        <!--左侧-->
        <div class="left_tree">
            <dl>
                <dd><a href='##' class="active">数学</a></dd>
                <dd><a href='##'>语文</a></dd>
                <dd><a href='##'>英语</a></dd>
            </dl>
        </div>
        <!--右侧-->
        <div class="data_list">
            <table>
                <thead>
                    <tr>
                        <td>学科</td>
                        <td>作业名称</td>
                        <td>得分</td>
                        <td>排名</td>
                        <td>错题情况</td>
                    </tr>
                </thead>
                <tbody id="tbRes">
                    <tr>
                        <td>数学</td>
                        <td>三角形综合训练</td>
                        <td>120</td>
                        <td>10</td>
                        <td>查看</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>
