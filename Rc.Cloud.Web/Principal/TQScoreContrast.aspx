<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PrincipalAnalysis.Master" AutoEventWireup="true" CodeBehind="TQScoreContrast.aspx.cs" Inherits="Rc.Cloud.Web.Principal.TQScoreContrast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <ul class="nav nav-tabs bg_white pv mt">
        <li><a href="StatsGradeHW_TQ.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">双向细目表分析</a></li>
        <li><a href="StatsGradeHW_KP.aspx?GradeId=<%=GradeId %>&ResourceToResourceFolder_Id=<%=ResourceToResourceFolder_Id %>">知识点得分分析</a></li>
        <li class="active"><a href='##'>小题得分分析</a></li>
    </ul>
    <div class="smquestion perform_con bg_white mt">
        <div class="perform_title">
            <ul class="clearfix">
                <li class="perform_left">
                    <ul class="clearfix">
                        <li class="l_1">题号</li>
                        <li class="l_2">小题分数</li>
                        <li class="l_3">年级得分率</li>
                    </ul>
                </li>
                <li class="perform_center">
                    <a href="##" class="btn_left" data-name="btn_left">&lsaquo;</a>
                    <div class="perform_div" data-name="perform_title">
                        <ul class="clearfix" id="ltlClassAvg"></ul>
                    </div>
                    <a href="##" class="btn_right" data-name="btn_right">&rsaquo;</a>
                </li>
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

            loadData();
            var objTitle = '[data-name="perform_title"]';
            var objCon = '[data-name="perform_div"]';
            var $_liNum = $(objTitle + ' li').length;
            var view = 7;
            //var $_liNum = num;
            //var $_liWidth = $(objTitle + ' li').width();
            var $_liWidth = 122;
            var $_allLiWidth = $_liNum * $_liWidth;
            var $_titWidth = $(objTitle).width();
            var $_titOffset = $(objTitle).children('ul').position().left;
            var btnInti = function (obj) {
                //2.设置按钮
                //2-1.左侧按钮
                (obj == 0) ? $('[data-name="btn_left"]').hide() : $('[data-name="btn_left"]').show();
                //2-2.右侧按钮
                (-obj == $_allLiWidth - ($_liWidth * view)) ? $('[data-name="btn_right"]').hide() : $('[data-name="btn_right"]').show();
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
            $.ajaxWebService("TQScoreContrast.aspx/GetStatsGradeHW_TQ", JSON.stringify(dto), function (data) {
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
    </script>
</asp:Content>
