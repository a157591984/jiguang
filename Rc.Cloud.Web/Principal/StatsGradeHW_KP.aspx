<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PrincipalAnalysis.Master" AutoEventWireup="true" CodeBehind="StatsGradeHW_KP.aspx.cs" Inherits="Rc.Cloud.Web.Principal.StatsGradeHW_KP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <ul class="nav nav-tabs bg_white pv mt">
        <li><a href="StatsGradeHW_TQ.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">双向细目表分析</a></li>
        <li class="active"><a href='##'>知识点得分分析</a></li>
        <li><a href="TQScoreContrast.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">小题得分分析</a></li>
    </ul>
    <div class="kg perform_con bg_white mt">
        <div class="perform_title">
            <ul class="clearfix">
                <li class="perform_left">
                    <ul class="clearfix">
                        <li class="l_1">知识点</li>
                        <li class="l_2">知识点分数</li>
                        <li class="l_3">题目序号</li>
                        <li class="l_4">年级平均得分率</li>
                    </ul>
                </li>
                <li class="perform_center">
                    <a href="##" class="btn_left" data-name="btn_left">&lsaquo;</a>
                    <div class="perform_div" data-name="perform_title">
                        <ul class="clearfix" id="ltlClassAvg"></ul>
                    </div>
                    <a href="##" class="btn_right" data-name="btn_right">&rsaquo;</a>
                </li>
                <li class="perform_right">操作</li>
            </ul>
        </div>
        <div class="perform_detail" id="ltlBody">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $("[data-name='sidebar'] li:eq(0)").addClass("active");

            num = 5;
            loadData();
            var objTitle = '[data-name="perform_title"]';
            var objCon = '[data-name="perform_div"]';
            var $_liNum = $(objTitle + ' li').length;
            var $_liWidth = 122;
            var $_allLiWidth = $_liNum * $_liWidth;
            var $_titWidth = $(objTitle).width();
            var $_titOffset = $(objTitle).children('ul').position().left;
            var btnInti = function (obj) {
                //2.设置按钮
                //2-1.左侧按钮
                (obj == 0) ? $('[data-name="btn_left"]').hide() : $('[data-name="btn_left"]').show();
                //2-2.右侧按钮
                (-obj == $_allLiWidth - ($_liWidth * 5)) ? $('[data-name="btn_right"]').hide() : $('[data-name="btn_right"]').show();
            }
            //1.设置宽度
            $(objTitle).children('ul').width($_allLiWidth);
            $(objCon).children('ul').width($_allLiWidth);
            //2.设置按钮
            //2-1.所有按钮
            if ($_allLiWidth < $_titWidth) $('[data-name^="btn_"]').hide();
            //2-2.左侧按钮
            if ($_allLiWidth > $_titWidth && $_titOffset == 0) $('[data-name="btn_left"]').hide();
            //2-3.右侧按钮
            if ($_allLiWidth > $_titWidth && $_titOffset == $_allLiWidth - ($_liWidth * 5)) $('[data-name="btn_right"]').hide();
            //3.滚动
            //3-1.点击左侧按钮
            $('[data-name="btn_left"]').on({
                click: function () {
                    $_offset = $(objTitle).children('ul').position().left + $_liWidth;
                    $('[data-name^="perform_"]').children('ul').animate({
                        left: $_offset
                    }, 100);
                    //3-1-1.设置按钮
                    btnInti($_offset);
                }
            })
            //3-2.右侧按钮
            $('[data-name="btn_right"]').on({
                click: function () {
                    $_offset = $(objTitle).children('ul').position().left - $_liWidth;
                    $('[data-name^="perform_"]').children('ul').animate({
                        left: $_offset
                    }, 100);
                    //3-2-1.设置按钮
                    btnInti($_offset);
                }
            })
        })
        var loadData = function () {
            var dto = {
                GradeId: "<%=GradeId%>",
                ResourceToResourceFolder_Id: "<%=ResourceToResourceFolder_Id%>",
                x: Math.random()
            };
            $.ajaxWebService("StatsGradeHW_KP.aspx/GetStatsGradeHW_KP", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "") {
                    //alert(json.thead);
                    $("#ltlClassAvg").html(json.thead);
                    $("#ltlBody").html(json.tbody);
                }
                else { $("#ltlBody").val("<tr><td colspan='100'>无数据</td></tr>"); }
            }, function () {
                //alert($('[data-name="perform_title"] li').width());
            }, false);
        }

        var ShowPic = function (kName, cName, data) {
            layer.open({
                type: 2,
                area: ["60%", "80%"],
                content: "StatsGradeHW_KP_Chart.aspx?kName=" + kName + "&cName=" + cName + "&data=" + data
            });
        }

    </script>
</asp:Content>
