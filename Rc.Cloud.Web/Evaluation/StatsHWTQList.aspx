<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TeacherStats.Master" AutoEventWireup="true" CodeBehind="StatsHWTQList.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.StatsHWTQList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../plugin/tablesorter/js/jquery.tablesorter.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[data-name='tablesorter']").tablesorter({
                //sortList: [[0, 0]],
                headers: {
                    1: { sorter: false },
                    10: { sorter: false },
                }
            });
            StatsClassHW_ScoreOverviewID = "-1";
            Class = getUrlVar("ClassCode");
            sd = getUrlVar("index");
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
                        //loadDataClassHW_ScoreOrder();
                        //loadStatsClassHW_ScoreOrderDistribution();
                    }
                }
            });
            loadData();

        })
        var loadData = function () {
            var dto = {
                Class: $("#Classes li a[tt].active").attr("cs"),
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                UserId: "<%=UserId%>",
                x: Math.random()
            };
            $.ajaxWebService("StatsHWTQList.aspx/GetStatsClassHW_TQ", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $("#tb1").html(data.d);
                }
                else { $("#tb1").html("<tr><td colspan='100'>无数据</td></tr>"); }
                $("[data-name='tablesorter']").trigger("update", [true]);
            }, function () { });
        }
        var ShowKnowledgeMasterList = function () {
            window.location.href = "KnowledgeMasterList.aspx?ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id%>&index=" + sd + "&HomeWork_ID=<%=HomeWork_ID%><%=StrTemp%>&ClassCode=" + Class;
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

    <ul class="nav nav-tabs pv bg_white mt">
        <li><a href="javascript: ShowKnowledgeMasterList();">知识点掌握情况</a></li>
        <li class="active"><a href='##'>双向细目表</a></li>
    </ul>

    <div class="panel mn">
        <div class="panel-body">
            <table class="table table-bordered text-center" data-name="tablesorter">
                <thead>
                    <tr>
                        <th width="70">题号</th>
                        <th width="70">题型</th>
                        <th width="200" class="text-left">测量目标</th>
                        <th class="text-left">考查内容</th>
                        <th width="90">难易度</th>
                        <th width="70">分值</th>
                        <th width="85">平均分</th>
                        <th width="85">标准差</th>
                        <th width="85">区分度</th>
                        <th width="110">班级错误率</th>
                        <th width="60">操作</th>
                    </tr>
                </thead>
                <tbody id="tb1"></tbody>
            </table>
        </div>
    </div>
</asp:Content>
