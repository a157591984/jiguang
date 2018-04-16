<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/parent.Master" AutoEventWireup="true" CodeBehind="OperationStatistics.aspx.cs" Inherits="Rc.Cloud.Web.parent.OperationStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main_box">
        <div class="container" data-name="main-auto">
            <div class="container_box">
                <div class="filter_box clearfix">
                    <dl class="filter_item">
                        <dt>我家宝贝</dt>
                        <dd>
                            <ul ajax-name="student">
                                <li><a href='##' class="active">测试学生01</a></li>
                            </ul>
                        </dd>
                    </dl>
                    <dl class="filter_item clearfix">
                        <dt>学科</dt>
                        <dd id="dd_Year">
                            <ul>
                                <li><a href='##' class="active">全部</a></li>
                                <li><a href='##'>数学</a></li>
                                <li><a href='##'>语文</a></li>
                                <li><a href='##'>英语</a></li>
                                <li><a href='##'>化学</a></li>
                                <li><a href='##'>物理</a></li>
                            </ul>
                        </dd>
                    </dl>
                    <dl class="filter_item clearfix">
                        <dt>时间</dt>
                        <dd id="dd_Year">
                            <ul>
                                <li><a href='##' class="active">周统计</a></li>
                                <li><a href='##'>月统计</a></li>
                            </ul>
                        </dd>
                    </dl>
                </div>
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>日期</th>
                            <th>作业布置次数</th>
                            <th>作业完成次数</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>第1周</td>
                            <td></td>
                            <td></td>
                            <td class="table_opera">
                                <a href="OperationStatisticsItem.aspx" target="_blank">详细</a>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>
