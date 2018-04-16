<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PrincipalAnalysis.Master" AutoEventWireup="true" CodeBehind="StatsGradeHW_TQ.aspx.cs" Inherits="Rc.Cloud.Web.Principal.StatsGradeHW_TQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <ul class="nav nav-tabs bg_white pv mt">
        <li class="active"><a href='##'>双向细目表分析</a></li>
        <li><a href="StatsGradeHW_KP.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">知识点得分分析</a></li>
        <li><a href="TQScoreContrast.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">小题得分分析</a></li>
    </ul>
    <div class="panel mn">
        <div class="panel-body">
            <table class="table table-bordered text-center" data-name='tablesorter'>
                <thead>
                    <tr>
                        <th width="8%">题号</th>
                        <th width="8%">题型</th>
                        <th width="10%" class="text-left">测量目标</th>
                        <th class="text-left">考查内容</th>
                        <th width="8%">难易度</th>
                        <th width="8%">分值</th>
                        <th width="8%">平均分</th>
                        <th width="8%">标准差</th>
                        <th width="8%">区分度</th>
                        <th width="10%">年级错误率</th>
                        <th width="8%">操作</th>
                    </tr>
                </thead>
                <tbody id="tb1">
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <script src="../plugin/tablesorter/js/jquery.tablesorter.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[data-name='sidebar'] li:eq(0)").addClass("active");

            $("[data-name='tablesorter']").tablesorter({
                //sortList: [[0, 0]],
                headers: {
                    1: { sorter: false },
                    10: { sorter: false },
                }
            });
            loadData();
        })
        var loadData = function () {
            var dto = {
                GradeId: "<%=GradeId%>",
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                x: Math.random()
            };
            $.ajaxWebService("StatsGradeHW_TQ.aspx/GetStatsGradeHW_TQ", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $("#tb1").html(data.d);
                }
                else { $("#tb1").html("<tr><td colspan='100'>无数据</td></tr>"); }
                $("[data-name='tablesorter']").trigger("update", [true]);
            }, function () { });
        }
    </script>
</asp:Content>
