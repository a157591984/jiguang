<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="correctT.aspx.cs" Inherits="Rc.Cloud.Web.teacher.correctT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../js/function.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../js/common.js"></script>
</head>
<body class="body_bg">
    <div class="container">
        <div class="test_paper_panel">
            <div class="fixed_sidebar right mini bottom">
                <ul>
                    <li class="active" id="btnSubmit" runat="server">
                        <div class="link">
                            完成批改
                        </div>
                    </li>
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
                        <div class="panel_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                        <ul class="panel_info">
                            <li>姓名：<asp:Literal runat="server" ID="ltlStudentName"></asp:Literal></li>
                            <li>班：<asp:Literal runat="server" ID="ltlClassName"></asp:Literal></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="panel_body">
                <div class="overview overview-hook" id="divCorrectTQ" runat="server">
                    <div class="catalog legend-toggle-hook">未阅</div>
                    <div class="overview_panel overview-panel-hook">
                        <div class="panel_heading">
                            <div class="panel_title">未阅试题</div>
                        </div>
                        <div class="panel_body">
                            <asp:Repeater runat="server" ID="rptCorrectTQ">
                                <ItemTemplate>
                                    <a href="javascript:;" class="warning" data-name="question" data-value="<%#Eval("TestQuestions_Id") %>"><%#Eval("topicNumber").ToString().TrimEnd('.') %></a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div class="question_type_panel" id="choice" runat="server">
                    <div class="panel_heading">选择题</div>
                </div>
                <div class="question_score_panel">
                    <%
                        if (dtStuScore.Rows.Count > 0)
                        {
                            System.Data.DataRow[] drStuScore = dtStuScore.Select("TestType in('selection','clozeTest')");
                            if (drStuScore.Length == 1)
                            {
                                StringBuilder stbHtml = new StringBuilder();
                                stbHtml.AppendFormat("<span class=\"total\">总分：{0}</span>"
                                    , Convert.ToDecimal(drStuScore[0]["FullScore"]).ToString("#0.##"));
                                stbHtml.AppendFormat("<span class=\"score\">得分：{0}</span>"
                                    , Convert.ToDecimal(drStuScore[0]["StuScore"]).ToString("#0.##"));
                                stbHtml.AppendFormat("<span class=\"points\">扣分：{0}</span>"
                                    , (Convert.ToDecimal(drStuScore[0]["FullScore"]) - Convert.ToDecimal(drStuScore[0]["StuScore"])).ToString("#0.##"));
                                Response.Write(stbHtml.ToString());
                            }
                        }
                    %>
                </div>
                <div class="objective_question_panel clearfix" runat="server" id="choice2">
                    <div class="panel_dt">
                        <ul>
                            <li>
                                <span>题号</span>
                                <span>参考答案</span>
                                <span>学生答案</span>
                                <span>得分</span>
                            </li>
                        </ul>
                    </div>
                    <div class="panel_dd">
                        <ul>
                            <asp:Repeater runat="server" ID="rptStuHomeworkSelection">
                                <ItemTemplate>
                                    <li class="<%#Eval("TestQuestions_Num") %>  <%#Eval("Student_Answer_Status").ToString()=="right"?"":"error" %>">
                                        <span><%#Eval("topicNumber").ToString().TrimEnd('.') %><%#string.IsNullOrEmpty(Eval("testIndex").ToString())?"":("-"+Eval("testIndex").ToString()) %></span>
                                        <span><%#Eval("TestCorrect") %></span>
                                        <span><%#Eval("Student_Answer") %></span>
                                        <%--<div class="hidden question_tit"><i class="fen"><%#Eval("TestQuestions_Score") %></i></div>--%>
                                        <span><%#Eval("Student_Score") %><input type="hidden" name="fill_Score" value="<%#Eval("Student_Score") %>"></span>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
                <div class="question_type_panel" id="truefalse" runat="server">
                    <div class="panel_heading">判断题</div>
                </div>
                <div class="question_score_panel">
                    <%
                        if (dtStuScore.Rows.Count > 0)
                        {
                            System.Data.DataRow[] drStuScore = dtStuScore.Select("TestType in('truefalse')");
                            if (drStuScore.Length == 1)
                            {
                                StringBuilder stbHtml = new StringBuilder();
                                stbHtml.AppendFormat("<span class=\"total text-info\">总分：{0}</span>"
                                    , Convert.ToDecimal(drStuScore[0]["FullScore"]).ToString("#0.##"));
                                stbHtml.AppendFormat("<span class=\"score text-success\">得分：{0}</span>"
                                    , Convert.ToDecimal(drStuScore[0]["StuScore"]).ToString("#0.##"));
                                stbHtml.AppendFormat("<span class=\"lose text-danger\">扣分：{0}</span>"
                                    , (Convert.ToDecimal(drStuScore[0]["FullScore"]) - Convert.ToDecimal(drStuScore[0]["StuScore"])).ToString("#0.##"));
                                Response.Write(stbHtml.ToString());
                            }
                        }
                    %>
                </div>
                <div class="objective_question_panel clearfix" id="truefalse2" runat="server">
                    <div class="panel_dt">
                        <ul>
                            <li>
                                <span>题号</span>
                                <span>参考答案</span>
                                <span>学生答案</span>
                                <span>得分</span>
                            </li>
                        </ul>
                    </div>
                    <div class="panel_dd">
                        <ul>
                            <asp:Repeater runat="server" ID="rptStuHomeworkTruefalse">
                                <ItemTemplate>
                                    <li class="<%#Eval("Student_Answer_Status").ToString()=="right"?"":"error" %>">
                                        <span><%#Eval("topicNumber").ToString().TrimEnd('.') %></span>
                                        <span><%#Eval("TestCorrect") %></span>
                                        <span><%#Eval("Student_Answer") %></span>
                                        <%--<div class="hidden question_tit"><i class="fen"><%#Eval("TestQuestions_Score") %></i></div>--%>
                                        <span><%#Eval("Student_Score") %><input type="hidden" name="fill_Score" value="<%#Eval("Student_Score") %>"></span>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>

                <div id="testpaper_correct_web"></div>

                <%--<div class="pb text-right">
                    <input type="button" id="btnSubmit" class="btn btn-primary" value="完成批改" runat="server" />
                </div>--%>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            var overview = '.overview-hook';
            var overviewPanel = '.overview-panel-hook';
            var legendToggle = '.legend-toggle-hook';
            var $overview = $(overview);
            var $overviewPanel = $(overviewPanel);
            var $gobacktop = $('.gobacktop-hook');
            var $catalogToggle = $(legendToggle);
            //窗口缩放
            $(window).on('resize', function () {
                if ($overview.length) {
                    var overviewPanelWeight = $overviewPanel.outerWidth();
                    var scrollLeft = $(this).scrollLeft();
                    if ($(window).width() <= overviewPanelWeight) {
                        $overviewPanel.css({
                            left: -scrollLeft + 'px'
                        });
                    } else {
                        $overviewPanel.css({
                            left: ''
                        });
                    }
                }
            })

            //页面滚动
            $(document).on('scroll', function () {
                var scrollTop = $(this).scrollTop();
                var scrollLeft = $(this).scrollLeft();
                if ($overview.length) {
                    //图例固定顶部
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
                            left: -scrollLeft + 'px'
                        });
                    }
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

            //分数计算
            //$(['choice', 'truefalse', 'fill', 'short']).each(function (i, e) {
            //    //总分
            //    var total = 0,
            //        score = 0;
            //    $('.' + e + ' .fen').each(function () {
            //        total += +$(this).html();
            //    })
            //    $('#' + e).find('.total i').html(total);
            //    //得分
            //    $('.' + e + ' input[name$="_Score"]').each(function () {
            //        score += +$(this).val();
            //    })
            //    $('#' + e).find('.score i').html(score);
            //    //扣分
            //    $('#' + e).find('.lose i').html(total - score);
            //});

            //一键满分
            //$('.full_mark').on({
            //    click: function () {
            //        //满分
            //        $(this).closest('.question').find('input[name$="_Score"]').each(function () {
            //            //alert($(this).attr('actualscore'));
            //            $(this).val($(this).attr('actualscore'));
            //        });
            //        //已阅
            //        $(this).prev('label').find("input[name='isRead']:checkbox").attr('checked', true);
            //    }
            //});

            //设置分值
            //$(".score input[name$='_Score']:enabled").bind({
            //    blur: function () {
            //        var actualScore = parseFloat($(this).attr("actualscore"));
            //        var Score = parseFloat($(this).val());
            //        if (!js_validate.IsFloat(Score)) {
            //            $(this).val("");
            //        }
            //        else if (Score > actualScore) {
            //            $(this).val("");
            //            layer.msg('得分不能大于试题分值', { icon: 4 });
            //        }
            //    },
            //    keyup: function () {
            //        var actualScore = parseFloat($(this).attr("actualscore"));
            //        var Score = parseFloat($(this).val());
            //        if (!js_validate.IsFloat(Score)) {
            //            $(this).val("");
            //        }
            //        else if (Score > actualScore) {
            //            $(this).val("");
            //            layer.msg('得分不能大于试题分值', { icon: 4 });
            //        }
            //    }
            //});

            //目录
            $(document).on('click', '[data-name="switch"]', function () {
                overviewListTop = $('[data-name="overviewList"]').css('top').replace('px', '');
                overviewListHeight = $('[data-name="overviewList"]').outerHeight();
                if (overviewListTop < 0) {
                    $('[data-name="overviewList"]').css({ 'top': 0 });
                } else {
                    $('[data-name="overviewList"]').css({ 'top': -(overviewListHeight - 3) });
                }
            })

            $(document).on('blur keyup', ".score input[name$='_Score']:enabled", function () {
                //验证分值
                var actualScore = parseFloat($(this).attr("actualscore"));
                var Score = parseFloat($(this).val());
                if (!js_validate.IsFloat(Score)) {
                    $(this).val("0");
                    return false;
                }
                else if (Score > actualScore) {
                    $(this).val("0");
                    layer.msg('得分不能大于试题分值', { icon: 4 });
                    return false;
                }
                else {
                    $(this).val(Score);
                }

                //已阅
                var tqId = $(this).attr("tqnum");
                $("#divCorrectTQ a[data-value='" + tqId + "']").remove();
                if ($("#divCorrectTQ a").length == 0) $("#divCorrectTQ").remove();

                $(this).closest('.question_panel').find("input[name='isRead']:checkbox").attr('checked', true).attr('disabled', true).next('label').html('已阅').removeClass('btn-danger').addClass('btn-success');

            })


            $("#btnSubmit").click(function () {
                var noAnswerCount = 0;
                var arrCorrect = new Array();

                //判断试题是否都已批阅
                if ($('input[name="isRead"]:checkbox:checked').length < $('input[name="isRead"]:checkbox').length) {
                    layer.msg('还有未批阅的试题，请勿提交！', { icon: 4, time: 1000 }, function () {
                        var firstCheckbox = $('input[name="isRead"][type="checkbox"]').not(':checked').get(0);
                        var firstInputScorce = $(firstCheckbox).closest('.question-panel-hook').find('input[name="fill_Score"]').get(0);
                        //获得焦点
                        //alert(firstInputScorce);
                        $(firstInputScorce).focus();
                    });
                    return false;
                }

                var noScoreObj = null;
                $(".score input[name$='_Score']:enabled").each(function () {
                    if ($.trim($(this).val()) == "") {
                        if (noScoreObj == null) noScoreObj = $(this);
                        noAnswerCount++;
                    }
                    else {
                        var obj = {
                            testquestions_Id: $(this).attr("tqnum"),
                            tqonum: $(this).attr("tqonum"),
                            actualscore: $(this).attr("actualscore"),
                            score: $(this).val(),
                            comment: $.trim($(this).closest('.question-panel-hook').find('[data-name="remark"]').attr("data-content")),
                            isRead: $(this).closest('.question-panel-hook').find('input[name="isRead"]:checked').val()
                        }
                        arrCorrect.push(obj);
                    }
                });

                var testCount = $(".score input[name$='_Score']").size();
                if (testCount > 0 && noAnswerCount > 0) {
                    layer.msg('还有未给分的试题，请勿提交！', { icon: 4, time: 2000 }, function () {
                        //获得焦点
                        $(noScoreObj).focus();
                    });
                    return false;
                }
                if (testCount > 0 && arrCorrect.length == 0) {
                    layer.msg('没有批改试题的数据，请勿提交！');
                    return false;
                }

                layer.confirm("确定要完成批改吗？", {
                    icon:4,
                    btn: ['确定', '取消'] //按钮
                }, function () {
                    var index = layer.load();
                    var dto = {
                        key: "teacher_correct_web",
                        correctData: JSON.stringify(arrCorrect),
                        stuHwId: "<%=stuHomeWorkId%>",
                        tchId: "<%=tchId%>",
                        x: Math.random()
                    }
                    $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                        if (data == "1") {
                            $("#btnSubmit").hide();
                            layer.close(index);
                            layer.msg('批改完成！', { icon: 1, time: 2000 }, function () {
                                window.opener.modifyNoCorrectCount();
                                window.opener.loadStudentHomeWorkData();
                                window.close();
                            });
                        }
                        else {
                            layer.close(index);
                            layer.msg('批改失败，请重新提交！', { icon: 2, time: 2000 });
                        }
                    }, function () {
                        layer.close(index);
                        layer.msg('批改失败，请重新提交！', { icon: 2, time: 2000 });
                    });

                    });

            });

            //编辑批注
            $(document).on('click', '[data-name="remark"]', function () {
                var objId = $(this).attr("id");
                var _comment = $.trim($("#" + objId).attr("data-content"));
                var innerHtml = '';
                innerHtml += '<div class="pa">';
                innerHtml += '<textarea name="" class="form-control" id="txtComment" rows="5"';
                innerHtml += ' onblur="if ($(this).val().length > 100) $(this).val($(this).val().slice(0, 100)); "';
                innerHtml += ' onkeyup="if ($(this).val().length > 100) $(this).val($(this).val().slice(0, 100)); "';
                innerHtml += '>' + _comment + '</textarea>';
                innerHtml += '<div class="pt text-right">';
                innerHtml += "<input type=\"button\" value=\"确定\" class=\"btn btn-primary\" onclick=\"setComment('" + objId + "');\">";
                //innerHtml += '<input type="button" value="取消" class="btn btn-default">';
                innerHtml += '</div>';
                innerHtml += '</div>';
                layer.open({
                    type: 1,
                    title: '批注',
                    area: ['380px', '245px'],
                    content: innerHtml //html内容
                });
            })

            //分数为空时设置默认值
            $('input[name="fill_Score"]').each(function () {
                if ($(this).val() == '') {
                    $(this).val('0');
                }
            })

            //给分数input赋值 2017-05-23
            $(document).on('click', '.score-list-hook:not(.disabled) a', function () {
                $(this).addClass('active').siblings().removeClass('active');

                var scoreIpt = $(this).closest('div.student_answer').next('div.score').find('input[name="fill_Score"]');
                if (scoreIpt.length) {
                    scoreIpt.val($(this).data('val'));
                }
                else {
                    $(this).closest('.question').find('input[name="fill_Score"]').val($(this).data('val'));
                }

                //已阅
                var objRead = $(this).closest('.question_panel').find("input[name='isRead']:checkbox");
                var tqId = objRead.attr("id").replace("isRead_", "");
                $("#divCorrectTQ a[data-value='" + tqId + "']").remove();
                if ($("#divCorrectTQ a").length == 0) $("#divCorrectTQ").remove();
                objRead.prop('checked', true).prop('disabled', true).next('label').html('已阅').removeClass('btn-danger').addClass('btn-success');
            })

            //自动已阅
            $('.question-panel-hook').each(function () {
                var Score = 0;
                var TotalScore = parseFloat($(this).find('.panel-stem-hook').data('total-score'));
                $(this).find('input[name$="_Score"]').each(function () {
                    Score += parseFloat($(this).val());
                })
                if (TotalScore == Score) {
                    $(this).find("input[name='isRead']:checkbox").attr('checked', true).attr('disabled', true).next('label').html('已阅').removeClass('btn-danger').addClass('btn-success');
                }
            });

            //已阅
            $(document).on('click', "input[name='isRead']:checkbox", function () {
                if ($(this).is(':checked')) {
                    var tqId = $(this).attr("id").replace("isRead_", "");
                    $("#divCorrectTQ a[data-value='" + tqId + "']").remove();
                    if ($("#divCorrectTQ a").length == 0) $("#divCorrectTQ").remove();
                    $(this).attr('disabled', true).next('label').html('已阅').removeClass('btn-danger').addClass('btn-success');
                } else {
                    $(this).attr('disabled', true).next('label').html('待阅').removeClass('btn-success').addClass('btn-danger');
                }
            })

            //语音批注
            $(document).on('click', '[data-name="comment_recording"]', function (e) {
                var hwCTime = '<%=hwCreateTime%>';
                var stuHwId = '<%=stuHomeWorkId%>';
                var tqId = $(this).data('value');
                layer.open({
                    type: 2,
                    title: '录音',
                    area: ['540px', '280px'],
                    content: '<%=strTestpaperViewWebSiteUrl%>teacher/recordingComment.aspx?hwCTime=' + hwCTime + "&stuHwId=" + stuHwId + "&tqId=" + tqId //iframe的url
                });
                e.preventDefault();
            });

            getTestpaperCorrect();

        });
        var getTestpaperCorrect = function () {
            $("#btnSubmit").hide();
            var dto = {
                key: "testpaper_correct_web",
                rtrfId: "<%=ResourceToResourceFolder_Id%>",
                hwId: "<%=HomeWork_Id%>",
                stuHwId: "<%=stuHomeWorkId%>",
                hwCreateTime: "<%=hwCreateTime%>",
                x: Math.random()
            };
            $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                $("#testpaper_correct_web").html(data);
                $("#btnSubmit").show();
                $('[data-name="remark"]').popover({
                    container: 'body',
                    trigger: 'hover',
                    placement: 'left'
                })
            }, function () {
                $("#btnSubmit").remove();
                $("#testpaper_correct_web").html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
                });
        }
        var setComment = function (objId) {
            $("#" + objId).attr("data-content", $.trim($("#txtComment").val()));
            layer.closeAll();
        }
    </script>
</body>
</html>
