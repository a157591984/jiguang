<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TeacherStats.Master" AutoEventWireup="true" CodeBehind="KnowledgeMasterList.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.KnowledgeMasterList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../plugin/tablesorter/js/jquery.tablesorter.min.js"></script>
    <script src="../plugin/highcharts/js/highstock.js"></script>
    <script src="../js/highcharts.config.js"></script>
    <script type="text/javascript">
        $(function () {
            StatsClassHW_ScoreOverviewID = "-1";
            if (getUrlVar("ClassCode") == "" || getUrlVar("ClassCode") == null || getUrlVar("ClassCode") == undefined) {
                Class = "<%=ClassID%>";
            }
            else { Class = getUrlVar("ClassCode") }
            if (getUrlVar("index") == "" || getUrlVar("index") == null || getUrlVar("index") == undefined) {
                sd = 0;
            }
            else { sd = getUrlVar("index"); }

            //$("#Classes li a[tt]").removeClass("active");
            //$("#Classes li a[tt]").eq(sd).addClass("active");
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
            $("#btnSeacher").click(function () {
                loadData();
            })
            $(document).keydown(function (e) {
                if (e.keyCode == 13) {
                    $('#btnSeacher').click();
                    return false;
                }
            })
            loadData();
            $("[data-name='tablesorter']").tablesorter({
                sortList: [[2, 1]],
                headers: {
                    //0: { sorter: false },
                    3: { sorter: false },
                    4: { sorter: false }
                }
            });
        })
        var loadData = function () {
            var dto = {
                Class: $("#Classes li a[tt].active").attr("cs"),
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                UserId: "<%=UserId%>",
                KPName: $("#txtKPName").val(),
                KPScoreAvgRate: $("#txtKPScoreAvgRate").val(),
                x: Math.random()
            };
            $.ajaxWebService("KnowledgeMasterList.aspx/GetStatsClassHW_KP", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    var arrayObj = data.d.split('|');
                    //alert(arrayObj);
                    if (arrayObj.length == 4) {
                        $("#tb1").html(arrayObj[0]);
                        GetDatePic(arrayObj[1], arrayObj[2], arrayObj[3]);
                    }

                }
                else { $("#tb1").html("<tr><td colspan='100'>无数据</td></tr>"); }
                $("[data-name='tablesorter']").trigger("update", [true]);
            }, function () { });
        }
        var ShowStatsHWTQList = function () {
            window.location.href = "StatsHWTQList.aspx?ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id%>&index=" + sd + "&HomeWork_ID=<%=HomeWork_ID%><%=StrTemp%>&ClassCode=" + Class
        }

        var GetDatePic = function (ClassName, KPScoreAvgRates, KPNames) {
            var arr = new Array();
            for (var i = 0; i < KPScoreAvgRates.split(',').length; i++) {
                arr.push(parseFloat(KPScoreAvgRates.split(',')[i]));
            }
            $('#chart').highcharts({
                chart: {
                    type: 'column',
                    width: $('#tab1').width(),
                    height: 500
                },
                xAxis: {
                    categories: KPNames.split(','),
                    title: {
                        text: '知识点'
                    },
                    max: (arr.length > 9) ? 9 : null,
                    min: (arr.length > 9) ? 0 : null
                },
                scrollbar: {
                    enabled: (arr.length > 9) ? true : false
                },
                yAxis: {
                    min: 0,
                    max: 100,
                    title: {
                        text: '平均得分率/%'
                    },
                    labels: {
                        formatter: function () {
                            return this.value + '%'
                        }
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
                        name: ClassName,
                        data: arr
                    }
                ]
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Container" runat="server">
    <div class="filter analysis_filter">
        <div class="form-inline">
            <div class="form-group">
                <label>知识点：</label>
                <input type="text" name="" id="txtKPName" class="form-control input-sm" />
            </div>
            <div class="form-group">
                <label>平均得分率低于：</label>
                <input type="text" name="" id="txtKPScoreAvgRate" class="form-control input-sm">&nbsp;%
            </div>
            <input type="button" value="搜索" id="btnSeacher" class="btn btn-primary btn-sm">
        </div>
        <div class="filter_section">
            <div class="filter_row clearfix">
                <span class="row_name">班级：</span>
                <div class="row_item">
                    <ul id="Classes">
                        <asp:Literal ID="ltlClasses" runat="server"></asp:Literal>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <ul class="nav nav-tabs bg_white pv mt">
        <li class="active"><a href='##'>知识点掌握情况</a></li>
        <li><a href="javascript:ShowStatsHWTQList();">双向细目表</a></li>
        <li class="pull-right">
            <ul class="nav nav-tabs">
                <li role="presentation" class="active"><a href="#tab1" data-toggle="tab">列表查看</a></li>
                <li role="presentation"><a href="#tab2" data-toggle="tab">图表查看</a></li>
            </ul>
        </li>
    </ul>

    <div class="panel mn">
        <div class="panel-body">
            <div class="tab-content">
                <div class="tab-pane active" id="tab1">
                    <table class="table table-bordered" data-name="tablesorter">
                        <thead>
                            <tr>
                                <th>知识点</th>
                                <th width="80">分数</th>
                                <th width="135">班级平均得分率</th>
                                <th width="520">题目序号</th>
                                <th width="165">历次平均得分率走势图</th>
                            </tr>
                        </thead>
                        <tbody id="tb1">
                        </tbody>
                    </table>
                </div>
                <div class="tab-pane" id="tab2">
                    <div id="chart"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
