<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="StatsActivity.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.StatsActivity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../SysLib/plugin/mfilter-1.0/mfilter.css" rel="stylesheet" />
    <script src="../SysLib/plugin/mfilter-1.0/mfilter.js"></script>
    <script src="../SysLib/plugin/highcharts/highstock.js"></script>
    <script src="../SysLib/js/highcharts.config.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="panel panel-default mfilter" data-name="mfilter">
                    <div class="mfilter_control" data-name="mfilterControl">
                        <div class="mfilter_header" data-name="mfilterHeader">产品类型</div>
                        <div class="mfilter_body" data-name="mfilterBody">
                            <div class="mfilter_item" data-name="mfilterItem" ajax-name="ProductType">
                                <a href="javascript:;" ajax-value="ALL" class="active">全部</a>
                                <a href="javascript:;" ajax-value="class">课件</a>
                                <a href="javascript:;" ajax-value="scienceword">作业</a>
                                <a href="javascript:;" ajax-value="skill">学生做作业</a>
                                <a href="javascript:;" ajax-value="marking">老师批改作业</a>
                            </div>
                        </div>
                    </div>
                    <div class="mfilter_control" data-name="mfilterControl">
                        <div class="mfilter_header" data-name="mfilterHeader">资源类型</div>
                        <div class="mfilter_body" data-name="mfilterBody">
                            <div class="mfilter_item" data-name="mfilterItem" ajax-name="ResourceClass">
                                <a href="javascript:;" ajax-value="ALL" class="active">所有</a>
                                <a href="javascript:;" ajax-value="CLOUD">云资源</a>
                                <a href="javascript:;" ajax-value="OWN">自有资源</a>
                            </div>
                        </div>
                    </div>
                    <div class="mfilter_control" data-name="mfilterControl">
                        <div class="mfilter_header" data-name="mfilterHeader">统计类型</div>
                        <div class="mfilter_body" data-name="mfilterBody">
                            <div class="mfilter_item" data-name="mfilterItem" ajax-name="DateType">
                                <a href="#day" data-toggle="tab" ajax-value="day" class="active">日统计</a>
                                <a href="#week" data-toggle="tab" ajax-value="week">周统计</a>
                                <a href="#month" data-toggle="tab" ajax-value="month">月统计</a>
                            </div>
                        </div>
                    </div>
                    <div class="mfilter_control" data-name="mfilterControl">
                        <div class="mfilter_header" data-name="mfilterHeader">时间区间</div>
                        <div class="mfilter_body tab-content" data-name="mfilterBody">
                            <div class="mfilter_item tab-pane active" data-name="mfilterItem" ajax-name="DateTime" id="day">
                                <a href="javascript:;" ajax-value="-1" class="active">全部</a>
                                <a href="javascript:;" ajax-value="week">近一周</a>
                                <a href="javascript:;" ajax-value="month">近一个月</a>
                                <a href="javascript:;" ajax-value="threemonth">近三个月</a>
                            </div>
                            <div class="mfilter_item tab-pane" data-name="mfilterItem" ajax-name="DateTime" id="week">
                                <a href="javascript:;" ajax-value="-1" class="active">全部</a>
                                <a href="javascript:;" ajax-value="month">近一个月</a>
                                <a href="javascript:;" ajax-value="threemonth">近三个月</a>
                                <a href="javascript:;" ajax-value="year">近一年</a>
                            </div>
                            <div class="mfilter_item tab-pane" data-name="mfilterItem" ajax-name="DateTime" id="month">
                                <a href="javascript:;" ajax-value="-1" class="active">全部</a>
                                <a href="javascript:;" ajax-value="year">近一年</a>
                                <a href="javascript:;" ajax-value="threeyear">近三年</a>
                                <a href="javascript:;" ajax-value="tenyear">近十年</a>
                            </div>
                        </div>
                    </div>
                    <div class="mfilter_control" data-name="mfilterControl">
                        <div class="mfilter_header" data-name="mfilterHeader">学校</div>
                        <div class="mfilter_body" data-name="mfilterBody">
                            <div class="mfilter_item" data-name="mfilterItem" ajax-name="SchoolId">
                                <a href="javascript:;" ajax-value="-1" class="active">全部</a>
                                <asp:Literal ID="ltlSchoolName" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="chart" style="width: 100%; height: 500px;"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $('[data-name="mfilter"]').mfilter({
                onClick: function (obj) {
                    loadData();
                }
            });

            loadData();
        })
        var loadData = function () {
            var dto = {
                ProductType: $('[ajax-name="ProductType"]').find('a.active').attr('ajax-value'),
                ResourceClass: $('[ajax-name="ResourceClass"]').find('a.active').attr('ajax-value'),
                DateType: $('[ajax-name="DateType"]').find('a.active').attr('ajax-value'),
                SchoolId: $('[ajax-name="SchoolId"]').find('a.active').attr('ajax-value'),
                datetime: $('[ajax-name="DateTime"]').find('a.active').attr('ajax-value'),
                x: Math.random()
            };
            $.ajaxWebService("StatsActivity.aspx/GetData", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                chart(json.strDate.split(","), json.strCount.split(","), json.strCount_N.split(","));
            }, function () { });
        }

        var chart = function (obj1, obj2, obj3) {
            var strCount = new Array();
            var strCount_N = new Array();
            for (var a = 0; a < obj2.length; a++) {
                strCount.push(parseFloat(obj2[a]));
            }
            for (var a = 0; a < obj3.length; a++) {
                strCount_N.push(parseFloat(obj3[a]));
            }
            $('#chart').highcharts({
                xAxis: {
                    categories: obj1,
                    max: (strCount.length > 10) ? 10 : null,
                    min: (strCount.length > 10) ? 0 : null
                },
                scrollbar: {
                    enabled: (strCount.length > 10) ? true : false
                },
                yAxis: [
                    {
                        min: 0,
                        title: {
                            text: '总数/人'
                        },
                        allowDecimals: false
                    },
                    {
                        min: 0,
                        title: {
                            text: '当前数量/人'
                        },
                        opposite: true,
                        allowDecimals: false
                    }
                ],
                tooltip: {
                    valueSuffix:'人'
                },
                series: [{
                    name: '总数',
                    data: strCount
                },
                {
                    name: '当前数量',
                    yAxis: 1,
                    data: strCount_N
                }
                ]
            });
        }
    </script>
</asp:Content>
