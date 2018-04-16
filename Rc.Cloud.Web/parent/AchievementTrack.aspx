<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/parent.Master" AutoEventWireup="true" CodeBehind="AchievementTrack.aspx.cs" Inherits="Rc.Cloud.Web.parent.AchievementTrack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../plugin/laydate/laydate.js"></script>
    <script src="../plugin/highcharts/js/highcharts.js"></script>
    <script src="../js/highcharts.config.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container">
        <div class="container pt">
            <div class="filter">
                <div class="form-inline">
                    <div class="form-group">
                        <label>时间：</label>
                        <input class="form-control input-sm" id="StartTime" placeholder="开始时间" clientidmode="Static" type="text">
                        <input class="form-control input-sm" id="EndTime" placeholder="结束时间" clientidmode="Static" type="text">
                    </div>
                    <%--<div class="form-group">
                        <label>书目名称：</label>
                        <input class="form-control input-sm" id="txtBookName" type="text" placeholder="书目名称" clientidmode="Static">
                    </div>--%>
                    <input class="btn btn-primary btn-sm" id="btnSearch" value="确定" clientidmode="Static" type="button">
                </div>
                <div class="filter_section">
                    <div class="filter_row clearfix">
                        <span class="row_name">我家宝贝：</span>
                        <div class="row_item">
                            <ul ajax-name="student">
                                <asp:Literal ID="ltlBabyName" runat="server" ClientIDMode="Static"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">学科：</span>
                        <div class="row_item">
                            <ul ajax-name="subject">
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

            <ul class="nav nav-tabs mb">
                <li class="active"><a href="#list" data-toggle="tab">列表查看</a></li>
                <li><a href="#chart" data-toggle="tab">图表查看</a></li>
            </ul>

            <div class="tab-content">
                <div role="tabpanel" class="tab-pane active" id="list">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <td width="8%">班级</td>
                                <td width="8%">学科</td>
                                <td>作业名称</td>
                                <td width="15%">布置时间</td>
                                <td width="5%">分数</td>
                                <td width="8%">班级平均分</td>
                                <%--<td>年级平均分</td>--%>
                                <td width="8%">班级排名</td>
                                <td width="8%">年级排名</td>
                            </tr>
                        </thead>
                        <tbody id="tb1">
                        </tbody>
                    </table>
                    <div class="pagination_container">
                        <ul class="pagination"></ul>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane" id="chart">
                    <div id="charts" style="width: 1170px; height: 550px;"></div>
                </div>
            </div>
        </div>
    </div>
    <textarea id="template_tb1" style="display: none">
    {#foreach $T.list as record}
     <tr>
            <td>{$T.record.ClassName}</td>
          <td>{$T.record.SubjectName}</td>
            <td>{$T.record.HomeWork_Name}</td>
            <td>{$T.record.HomeWorkCreateTime}</td>
            <td>{$T.record.StudentScore}</td>
            <td>{$T.record.classavg}</td>
            <%--<td>{$T.record.gradeavg}</td>--%>
            <td>{$T.record.StudentScoreOrder}</td>
            <td>{$T.record.GradeStudentScoreOrder}</td>
     </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            $('[ajax-name="student"] li a').removeClass("active");
            $('[ajax-name="student"] li a').eq(0).addClass("active");
            $('[ajax-name="student"] li a').on({
                click: function () {
                    $('[ajax-name="student"] li a').removeClass("active").eq(0).addClass("active");
                    //SubjectId = $(this).attr("SubjectId");
                    $('[ajax-name="student"] li a').removeClass("active");
                    $(this).addClass("active");
                    pageIndex = 1;
                    loadSubject();
                }
            });
            loadSubject();
            $('[ajax-name="subject"] li a').removeClass("active");
            $('[ajax-name="subject"] li a').eq(0).addClass("active");
            $(document).on({
                click: function () {
                    $('[ajax-name="subject"] li a').removeClass("active").eq(0).addClass("active");
                    //SubjectId = $(this).attr("SubjectId");
                    $('[ajax-name="subject"] li a').removeClass("active");
                    $(this).addClass("active");
                    pageIndex = 1;
                    loadData();
                }
            }, '[ajax-name="subject"] li a');
            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();

            })

            // 日期
            var StarTime = {
                elem: '#StartTime',
                format: 'YYYY-MM-DD',
                choose: function (datas) {
                    EndTime.min = datas; //开始日选好后，重置结束日的最小日期
                    EndTime.start = datas //将结束日的初始值设定为开始日
                }
            }
            var EndTime = {
                elem: '#EndTime',
                format: 'YYYY-MM-DD',
                choose: function (datas) {
                    StarTime.max = datas; //结束日选好后，重置开始日的最大日期
                }
            }

            laydate(StarTime);
            laydate(EndTime);

        });
        var loadData = function () {
            var dto = {
                BabyId: $('[ajax-name="student"]').find('a.active').attr('BabyId'),
                SubjectId: $('[ajax-name="subject"]').find('a.active').attr('SubjectId'),
                HomeWorkCreateTime: $("#StartTime").val(),
                HomeWorkFinishTime: $("#EndTime").val(),
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                x: Math.random()
            };
            $.ajaxWebService("AchievementTrack.aspx/GetStatsClassStudentHW_Score", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template_tb1", null, { filter_data: false });
                    $("#tb1").processTemplate(json);
                    $(".pagination_container").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                    Chart(json.HomeWork_NameArray, json.jsonStr);
                }
                else {
                    $("#tb1").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $(".pagination_container").find("ul").html("");
                    Chart("[0]", "[{}]");
                }
                if (json.list == null || json.list == "") {
                    pageIndex--;
                    if (pageIndex > 0) {
                        loadData();
                    }
                    else {
                        pageIndex = 1;
                    }
                }
            }, function () { });
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        var loadSubject = function () {
            var dto = {
                BabyId: $('[ajax-name="student"]').find('a.active').attr('BabyId'),
                x: Math.random()
            };
            $.ajaxWebService("AchievementTrack.aspx/GetSubject", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $('[ajax-name="subject"]').html(data.d);
                    loadData();
                }
            }, function () { });

        }
        var Chart = function (HomeWork_NameArray, jsonStr) {
            var arr = HomeWork_NameArray.split(',').reverse();
            $('#charts').highcharts({
                chart: {
                    type: 'line'
                },
                xAxis: {
                    categories: arr,
                    title: {
                        text: '作业名称'
                    }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '分数'
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        dataLabels: {
                            enabled: true,
                            formatter: function () {
                                return this.y + '%';
                            }
                        }
                    },
                    spline: {
                        dataLabels: {
                            enabled: true,
                            formatter: function () {
                                return this.y + '%';
                            },
                            style: {
                                color: Highcharts.getOptions().colors[3]
                            }
                        }
                    },
                },
                series: $.parseJSON(jsonStr)
            });
        }
    </script>
</asp:Content>
