<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/student.Master" AutoEventWireup="true" CodeBehind="AchievementTrack.aspx.cs" Inherits="Rc.Cloud.Web.student.AchievementTrack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../plugin/laydate/laydate.js"></script>
    <script src="../plugin/highcharts/js/highcharts.js"></script>
    <script src="../js/highcharts.config.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="oHomework.aspx">最新作业</a></li>
            <%if (Convert.ToBoolean(Rc.Common.ConfigHelper.GetConfigString("IsShowNewStuAnalysis")))
                { %>
            <li><a href="St_oHistoryHomework.aspx">历次作业</a></li>
            <%}
                else
                { %>
            <li><a href="oHistoryHomework.aspx">历次作业</a></li>
            <%} %>
            <li><a href="oWrongHomework.aspx">错题集</a></li>
            <li><a href="AchievementTrack.aspx" class="active">成绩跟踪</a></li>
            <%if (Convert.ToBoolean(Rc.Common.ConfigHelper.GetConfigString("IsShowNewStuAnalysis")))
                { %>
            <li><a href="CheckStuHWAnalysis.aspx">综合分析</a></li>
            <%} %>
        </ul>
    </div>
    <div class="iframe-container">
        <div class="container pt">
            <div class="filter">
                <div class="form-inline">
                    <div class="form-group">
                        <label>时间：</label>
                        <input class="form-control input-sm" id="StartTime" type="text" placeholder="开始时间" clientidmode="Static">
                        <input class="form-control input-sm" id="EndTime" type="text" placeholder="结束时间" clientidmode="Static">
                    </div>
                    <input class="btn btn-primary btn-sm" id="btnSearch" type="button" value="确定" clientidmode="Static">
                </div>
                <div class="filter_section">
                    <div class="filter_row clearfix">
                        <span class="row_name">学科：</span>
                        <div class="row_item">
                            <ul data-name="subject">
                                <asp:Literal ID="ltlSubjectName" runat="server"></asp:Literal>
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
                                <th width="90">班级</th>
                                <th>作业名称</th>
                                <th width="170">布置时间</th>
                                <th width="75">得分</th>
                                <th width="90">班级平均分</th>
                                <%--<td>年级平均分</td>--%>
                                <th width="75">班级排名</th>
                                <th width="75">年级排名</th>
                            </tr>
                        </thead>
                        <tbody id="tb1">
                        </tbody>
                    </table>
                    <div class="pagination_container">
                        <ul class="pagination">
                        </ul>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane" id="chart">
                    <div id="charts" style="width: 1170px; height: 550px;"></div>
                </div>
            </div>
        </div>
    </div>
    <textarea id="template_tb1" class="hidden">
    {#foreach $T.list as record}
     <tr>
            <td>{$T.record.ClassName}</td>
            <td class="text-left">{$T.record.HomeWork_Name}</td>
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
            $('[data-name="subject"] li a').removeClass("active");
            $('[data-name="subject"] li a').eq(0).addClass("active");
            $('[data-name="subject"] li a').bind({
                click: function () {
                    $('[data-name="subject"] li a').removeClass("active").eq(0).addClass("active");
                    SubjectId = $(this).attr("SubjectId");
                    $('[data-name="subject"] li a').removeClass("active");
                    $(this).addClass("active");
                    loadData();

                }
            });
            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();

            })
            loadData();

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

            $('#charts').highcharts({
                chart: {
                    type: 'line'
                },
                xAxis: {
                    categories: ['作业1', '作业2', '作业3', '作业4', '作业5', '作业6', '作业7', '作业8', '作业9', '作业10', '作业11', '作业12'],
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
                    '<td style="padding:0"><b>{point.y:.1f}%</b></td></tr>',
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
                series: [
                    {
                        name: '分数',
                        data: [49, 71, 61, 84, 34, 67, 75, 98, 54, 87]
                    }, {
                        name: '班级平均分',
                        data: [49, 71, 56, 79, 78, 68, 85, 98, 80, 67]
                    }, {
                        name: '年级平均分',
                        data: [49, 71, 56, 79, 94, 100, 85, 98, 100, 100]
                    }
                ]
            });
        });
        function loadData() {
            var dto = {
                SubjectId: $('[data-name="subject"]').find('a.active').attr('SubjectId'),
                HomeWorkCreateTime: $("#StartTime").val(),
                HomeWorkFinishTime: $("#EndTime").val(),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
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
        function pageselectCallback(page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        function Chart(HomeWork_NameArray, jsonStr) {
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
                        text: '得分率'
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.1f}%</b></td></tr>',
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
