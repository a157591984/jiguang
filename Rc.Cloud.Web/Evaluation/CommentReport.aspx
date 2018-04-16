<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommentReport.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.CommentReport" %>

<%@ Import Namespace="Rc.Common.StrUtility" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../js/function.js"></script>
    <script src="../js/common.js"></script>
    <title>讲评报告</title>
</head>
<body class="body_bg">
    <form id="form1" runat="server">
        <div class="container container_comment_report">
            <div class="test_paper_panel">

                <div class="fixed_sidebar right mini bottom">
                    <ul>
                        <li class="close-page-hook">
                            <div class="link">
                                <i class="material-icons">&#xE14C;</i>
                            </div>
                            <div class="pop">关闭页面</div>
                        </li>
                        <li class="hidden gobacktop-hook">
                            <div class="link">
                                <i class="material-icons">&#xE5D8;</i>
                            </div>
                            <div class="pop">返回顶部</div>
                        </li>
                    </ul>
                </div>

                <div class="panel_heading">
                    <div class="test_paper_name_panel">
                        <div class="panel_heading">
                            <h3 class="panel_title">
                                <asp:Literal ID="ltlHomeWork_Name" runat="server"></asp:Literal></h3>
                            <ul class="panel_info">
                                <li>年级：<asp:Literal ID="ltlGrade" runat="server"></asp:Literal></li>
                                <li>班级：<asp:Literal ID="ltlClass" runat="server"></asp:Literal></li>
                                <li>学科：<asp:Literal ID="ltlSubjectName" runat="server"></asp:Literal></li>
                                <li>老师：<asp:Literal ID="ltlTeacherName" runat="server"></asp:Literal></li>
                                <li>满分<asp:Literal ID="ltlSumSore" runat="server"></asp:Literal>分</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="panel_body">
                    <!-- 得分概览 -->
                    <div class="overview overview-hook">
                        <div class="catalog legend-toggle-hook">目录</div>
                        <div class="overview_panel overview-panel-hook">
                            <div class="panel_heading">
                                <div class="panel_control">
                                    <i class="material-icons legend-toggle-hook">&#xE14C;</i>
                                </div>
                                <ul class="legend">
                                    <li class="desc">小题得分率区间划分图例</li>
                                    <li class="item"><i class="danger"></i>0-60%</li>
                                    <li class="item"><i class="warning"></i>60%-70%</li>
                                    <li class="item"><i class="info"></i>70%-85%</li>
                                    <li class="item"><i class="success"></i>85%-100%</li>
                                </ul>
                            </div>
                            <div class="panel_body">
                                <asp:Repeater runat="server" ID="rptTQ">
                                    <ItemTemplate>
                                        <a href="javascript:;" class="<%#Eval("StudentAvgScoreRateStyle") %>" data-name="question" data-value="<%#Eval("TestQuestions_Num") %>" tt="<%#Eval("StudentAvgScoreRate") %>"><%#string.IsNullOrEmpty(Eval("topicNumber").ToString()) ?"&nbsp;": Eval("topicNumber").ToString().TrimEnd('.') %></a>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>

                    <div id="testpaper_correct_comment"></div>

                </div>
            </div>
        </div>
        <div style="display: none;" id="idTbody"></div>
        <script type="text/javascript">
            $(function () {
                getTestpaperComment();

                $(document).on('click', '.attr-switch-hook', function (e) {
                    e.preventDefault();
                    var $attrEle = $(this).closest('.panel-opera-hook').next('.panel-other-hook');
                    if ($attrEle.hasClass('panel_other_mini')) {
                        $attrEle.removeClass('panel_other_mini');
                        $(this).html('收起&nbsp;<i class="material-icons">&#xE316;</i>');
                    } else {
                        $attrEle.addClass('panel_other_mini');
                        $(this).html('展开&nbsp;<i class="material-icons">&#xE313;</i>');
                    }
                })

                //答题情况对比
                $(document).on('click', '[data-name="contrast"]', function () {
                    var dataVal = $(this).data("value").split(",");
                    var arrStu = new Array();
                    $(this).siblings("li").find("input:checkbox:checked").each(function () {
                        arrStu.push($(this).val());
                    });
                    if (arrStu.length < 2) {
                        layer.msg("请选择两个及以上学生", { icon: 4 })
                        return false;
                    }

                    layer.open({
                        type: 2,
                        title: "答题情况对比",
                        fix: false,
                        area: ["90%", "90%"],//"745px", "450px"
                        scrollbar: false,
                        content: "CommentReportStudentAnswer_Multi.aspx?ResourceToResourceFolder_Id=" + dataVal[0]
                        + "&Homework_Id=" + dataVal[1] + "&TestQuestions_Id=" + dataVal[2] + "&Student_Id=" + arrStu.join(",")
                    });
                    return false;
                })


                var overview = '.overview-hook';
                var overviewPanel = '.overview-panel-hook';
                var legendToggle = '.legend-toggle-hook';
                var $overview = $(overview);
                var $overviewPanel = $(overviewPanel);
                var $gobacktop = $('.gobacktop-hook');
                var $catalogToggle = $(legendToggle);
                //窗口缩放
                $(window).on('resize', function () {
                    var overviewPanelWeight = $overviewPanel.outerWidth();
                    var scrollLeft = $(this).scrollLeft();
                    if ($(window).width() <= overviewPanelWeight) {
                        $overviewPanel.css({
                            left: - scrollLeft + 'px'
                        });
                    } else {
                        $overviewPanel.css({
                            left: ''
                        });
                    }
                })

                //页面滚动
                $(document).on('scroll', function () {
                    //图例固定顶部
                    var scrollTop = $(this).scrollTop();
                    var scrollLeft = $(this).scrollLeft();
                    var overviewOffsetTop = $overview.offset().top;
                    var overviewPanelHeight = $overviewPanel.height();
                    var overviewPanelWeight = $overviewPanel.outerWidth();
                    if (scrollTop > overviewOffsetTop) {
                        $overviewPanel.addClass('fiexd');
                        $overview.height(overviewPanelHeight);
                        $catalogToggle.show();
                    } else {
                        $overviewPanel.removeClass('fiexd').show();
                        $overview.height('');
                        $catalogToggle.hide();
                    }
                    //横向滚动时图例的位置
                    if ($(window).width() <= overviewPanelWeight) {
                        $overviewPanel.css({
                            left: - scrollLeft + 'px'
                        });
                    }

                    //返回顶部
                    if (scrollTop > 100) {
                        $gobacktop.removeClass('hidden');
                    } else {
                        $gobacktop.addClass('hidden');
                    }
                });

                //关闭页面
                $('.close-page-hook').on('click', function () {
                    window.close();
                });

                //返回顶部
                $gobacktop.on('click', function () {
                    $("html,body").animate({
                        scrollTop: 0
                    }, 150);
                })

                //显示/隐藏图例
                $overview.on('click', legendToggle, function (e) {
                    $overviewPanel.toggle();
                });

                //定位试题
                $('[data-name="question"]').on('click', function (e) {
                    e.stopPropagation();
                    e.preventDefault;
                    var overviewPanelHeight = $(overviewPanel).height();
                    var question_num = "question_" + $(this).attr("data-value");
                    $("html,body").animate({
                        scrollTop: $('[data-name="' + question_num + '"]').offset().top - overviewPanelHeight
                    }, 150);
                });

                setInterval(function () { UpdateVisitTime(); }, 1000 * 60);

            })

            function getTestpaperComment() {
                var dto = {
                    key: "testpaper_comment",
                    rtrfId: "<%=ResourceToResourceFolder_Id%>",
                    hwId: "<%=HomeWork_Id%>",
                    x: Math.random()
                };
                $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                    $("#testpaper_correct_comment").html(data);
                    $('[data-toggle="popover"]').popover({
                        trigger: 'hover'
                    });
                }, function () {
                    $("#testpaper_correct_comment").html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
                    })
            }

            //查看解析
            function PicPreview(url, title, width, height) {
                if (title == '' || title == undefined) {
                    title = '预览';
                }
                layer.open({
                    type: 2,
                    title: title,
                    area: ["90%", "90%"],
                    scrollbar: false,
                    content: url
                });
            }

            //答题情况
            function AnswerStatus(url, title) {
                layer.open({
                    type: 2,
                    title: title,
                    fix: false,
                    area: ["90%", "90%"],//"745px", "450px"
                    scrollbar: false,
                    content: url
                });
            }

            //放大
            function enlargeTBody(obj) {
                $("#idTbody").html("");
                var fontSize = 32;
                var _tbHtml = $.trim($(obj).html()).replace(/font-size:\s?\w+;?/g, 'font-size:' + fontSize + 'px;');
                $("#idTbody").html(_tbHtml);
                $("#idTbody img").each(function () {
                    $(this).prop("width", $(this).prop("width") * 2);
                    $(this).prop("height", $(this).prop("height") * 2);
                    var _align = parseInt($(this).css("vertical-align").toLowerCase().replace("px", ""));

                    $(this).css("vertical-align", _align * 2);
                });
                layer.open({
                    type: 1
                    , area: ["90%", "90%"]
                    , title: '题干'
                    , scrollbar: false
                    , content: '<div style="line-height:50px;padding:20px;font-size:' + fontSize + 'px;">' + $.trim($("#idTbody").html()) + '</div>'
                });
            }

            //更新浏览时间
            function UpdateVisitTime() {
                var dto = {
                    visit_web_id: "<%=visit_web_id%>",
                    x: Math.random()
                };
                $.ajaxWebService("CommentReport.aspx/UpdateRecordVisitCloseTime", JSON.stringify(dto), function () { }, function () { }, false);
            }
        </script>
    </form>
</body>
</html>
