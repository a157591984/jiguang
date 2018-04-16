<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TeacherStats.Master" AutoEventWireup="true" CodeBehind="AssessmentProfile.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.AssessmentProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../plugin/tablesorter/js/jquery.tablesorter.min.js"></script>
    <script src="../plugin/highcharts/js/highcharts.js"></script>
    <script src="../js/highcharts.config.js"></script>
    <script type="text/javascript">
        $(function () {
            StatsClassHW_ScoreOverviewID = "-1";
            Class = "<%=ClassID%>";
            sd = 0;
            $("#Classes li a[tt]").bind({
                click: function () {
                    if (!$(this).hasClass("disabled")) {
                        $("#Classes li a[tt]").removeClass("active").eq(sd).addClass("active");
                        StatsClassHW_ScoreOverviewID = $(this).attr("tt");
                        Class = $(this).attr("cs");
                        sd = $("#Classes li a[tt]").index(this);
                        $("#Classes li a[tt]").removeClass("active");
                        $(this).addClass("active");
                        loadData();
                        loadDataClassHW_ScoreOrder();
                        loadStatsClassHW_ScoreOrderDistribution();
                    }
                }
            });

            loadData();
            loadDataClassHW_ScoreOrder();
            loadStatsClassHW_ScoreOrderDistribution();
            $("[data-name='tablesorter']").tablesorter({
                headers: {
                    0: { sorter: false }
                }
            });
        })
        function closeWP() {
            var Browser = navigator.appName;
            var indexB = Browser.indexOf('Explorer');

            if (indexB > 0) {
                var indexV = navigator.userAgent.indexOf('MSIE') + 5;
                var Version = navigator.userAgent.substring(indexV, indexV + 1);

                if (Version >= 7) {
                    window.open('', '_self', '');
                    window.close();
                }
                else if (Version == 6) {
                    window.opener = null;
                    window.close();
                }
                else {
                    window.opener = '';
                    window.close();
                }

            }
            else {
                window.close();
            }
        }
        function loadData() {
            var dto = {
                ClassCode: $("#Classes li a[tt].active").attr("cs"),
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                UserId: "<%=UserId%>",
                x: Math.random()
            };
            $.ajaxWebService("AssessmentProfile.aspx/GetStatsClassHW_ScoreOverview", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $("#td1").html("<tr>" + data.d + "</tr>");
                }
                else { $("#td1").html("<tr><td colspan='100'>无数据</td></tr>"); }
            }, function () { });
        }
        function loadDataClassHW_ScoreOrder() {
            var dto = {
                ClassCode: $("#Classes li a[tt].active").attr("cs"),
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                UserId: "<%=UserId%>",
                x: Math.random()
            };
            $.ajaxWebService("AssessmentProfile.aspx/GetStatsClassHW_ScoreOrder", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    var arrayObj = data.d.split('|');
                    //alert(arrayObj);
                    if (arrayObj.length == 2) {
                        $("#tb2").html(arrayObj[0]);
                        if (arrayObj[1] != "") {
                            $("#bf").attr("colspan", arrayObj[1]);
                            $("#af").attr("colspan", arrayObj[1]);
                        }

                    }

                }
                else { $("#tb2").html("<td colspan='100'>无数据</td>"); }
            }, function () { });
        }
        function loadStatsClassHW_ScoreOrderDistribution() {
            var dto = {
                ClassCode: $("#Classes li a[tt].active").attr("cs"),
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                UserId: "<%=UserId%>",
                x: Math.random()
            };
            $.ajaxWebService("AssessmentProfile.aspx/GetStatsClassHW_ScoreOrderDistribution", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    //alert(data.d)
                    var arrayObj = data.d.split('|');
                    //alert(arrayObj);
                    if (arrayObj.length == 2) {
                        $("#tb3").html(arrayObj[0]);
                        GreadChax(arrayObj[1]);
                    }
                }
                else { $("#tb3").html("<tr><td colspan='100'>无数据</td></tr>"); }
                $("[data-name='tablesorter']").trigger("update", [true]);
            }, function () { });
        }
        function GreadChax(obj) {
            $('#chart').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: true,
                    height: 230,
                    spacingLeft: 10,
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                },
                tooltip: {
                    valueSuffix: '%',
                },
                series: [{
                    type: 'pie',
                    name: '占比',
                    data: $.parseJSON(obj)
                }]
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Container" runat="server">
    <div class="filter analysis_filter">
        <div class="filter_section">
            <div class="filter_row">
                <span class="row_name">班级：</span>
                <div class="row_item">
                    <ul id="Classes">
                        <asp:Literal ID="ltlClasses" runat="server"></asp:Literal>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="page_title">成绩概况</div>
    <div class="panel mn">
        <div class="panel-body">
            <table class="table table-bordered text-center">
                <thead>
                    <tr>
                        <th>班级</th>
                        <th>人数</th>
                        <th>最高分</th>
                        <th>最低分</th>
                        <th>平均分</th>
                        <th>中位数</th>
                        <th>众数</th>
                        <th>标准差</th>
                        <th>已发/未发</th>
                        <th>已交/未交</th>
                        <th>已批/未批</th>
                    </tr>
                </thead>
                <tbody id="td1">
                </tbody>
            </table>
        </div>
    </div>

    <div class="page_title">前五名后五名</div>
    <div class="panel mn">
        <div class="panel-body">
            <table class="table table-bordered text-center">
                <thead>
                    <tr>
                        <th id="bf" colspan="" class="top_five">前五名</th>
                        <th id="af" colspan="" class="last_five">后五名</th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="tb2">
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

    <div class="page_title">等级分布</div>
    <div class="panel mn">
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-8">
                    <table class="table table-bordered text-center" data-name="tablesorter">
                        <thead>
                            <tr>
                                <th>分档</th>
                                <th>分数区间</th>
                                <th>人数</th>
                                <th>占比</th>
                                <th>百分比</th>
                            </tr>
                        </thead>
                        <tbody id="tb3" data-name="grade-chart">
                        </tbody>
                    </table>
                </div>
                <div class="col-xs-4">
                    <div id="chart"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

