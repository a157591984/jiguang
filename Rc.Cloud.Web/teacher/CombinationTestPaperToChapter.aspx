<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CombinationTestPaperToChapter.aspx.cs" Inherits="Rc.Cloud.Web.teacher.CombinationTestPaperToChapter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>开始组卷</title>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/common.js"></script>
    <script src="../js/function.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            _Identifier_Id = "<%=Identifier_Id%>";
            getTestPaper();

            //var $gobacktop = $('.gobacktop-hook');
            ////页面滚动
            //$(document).on('scroll', function () {
            //    var scrollTop = $(this).scrollTop();
            //    //返回顶部
            //    if (scrollTop > 100) {     
             
            //        $gobacktop.removeClass('hidden');
            //    } else {
            //        $gobacktop.addClass('hidden');
            //    }
            //});

            ////返回顶部
            //$gobacktop.on('click', function () {
            //    $("html,body").animate({
            //        scrollTop: 0
            //    }, 150);
            //})

            //换题
            $(document).on('click', ".ischange-hook", function () {
                var id = $(this).attr('tt');
                ChangeTQ(id, "1", "0", "0");
            })
            //手动换题
            $(document).on('click', ".changetq-hook", function () {
                var id = $(this).attr('tt');
                layer.open({
                    type: 2,
                    title: '精准选题',
                    area: ['80%', '80%'],
                    scrollbar: false,
                    content: 'PrecizeTopicSelection.aspx?ChapterAssembly_TQ_Id=' + id
                });
            })
            //确认
            $(document).on('click', "#btnConfirm", function () {
                if ($.trim($("#rename").val()) == "") {
                    layer.msg("请填写试卷名称", { icon: 2, time: 2000 }, function () { $("#rename").focus(); });
                    return false;
                }
                else {
                    var arrCked = new Array();
                    $(".question_type_panel input[type=text]").each(function () {
                        arrCked.push($(this).val())
                    })
                    layer.open({
                        type: 2,
                        title: '保存到',
                        area: ['500px', '500px'],
                        content: 'ChapterAssemblySelectSaveCatelog.aspx?Identifier_Id=' + _Identifier_Id + "&ugid=<%=ugid%>" + "&ReName=" + $.trim($("#rename").val()) + "&Title=" + arrCked.join("♧")
                    });
                    // ConfirmTestpaper(Two_WayChecklist_Id, RelationPaper_Id, $("#rename").val(), arrCked.join("♧"), CreateUser)
                }
            })

            //刷新试卷
            $(document).on('click', "#btnAgain", function () {
                window.location.reload();
            })
        })

        // 初始化试题数据
        var InitializationTestQuestions = function (Two_WayChecklist_Id, RelationPaper_Id, CreateUser) {
            var dto = {
                Two_WayChecklist_Id: Two_WayChecklist_Id,
                RelationPaper_Id: RelationPaper_Id,
                CreateUser: CreateUser,
                x: Math.random()
            };
            $.ajaxWebService("CombinationTestPaper.aspx/InitializationTestQuestions", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    //layer.close(index);
                    //组卷
                    getTestPaper(Two_WayChecklist_Id, RelationPaper_Id, Two_WayChecklist_Name);
                }
                else {
                    layer.msg("初始化试题数据失败", { icon: 2, time: 2000 });
                }
            }, function () {
                layer.msg("初始化试题数据失败err", { icon: 2, time: 2000 });
            }, false);
        }
        // 加载试卷
        var getTestPaper = function () {

            var dto = {
                key: "combination_testpapertochapter_view",
                Identifier_Id: _Identifier_Id,
                x: Math.random()
            };
            $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                $("#divTestPaper").html(data);
            }, function () {
                $("#divTestPaper").html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
            });

        }
        // type=1随机切换 type=2选择换题
        var ChangeTQ = function (ChapterAssembly_TQ_Id, Type, RCount, ChangeType) {
            var dto = {
                key: "combination_testpapertochapter_tq_change",
                ChapterAssembly_TQ_Id: ChapterAssembly_TQ_Id,
                Type: Type,
                Identifier_Id: _Identifier_Id,
                RCount: RCount,
                ChangeType: ChangeType,
                x: Math.random()
            };
            $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                var $json = $.parseJSON(data);
                $("#change_" + ChapterAssembly_TQ_Id).html($json.tbody);
                if ($json.ReturnValue == 1) {
                    $("#change_" + ChapterAssembly_TQ_Id).prepend($json.title);
                }
                else if ($json.ReturnValue == 2) {
                    $("#change_" + ChapterAssembly_TQ_Id).after($json.title);
                }
                //div样式转换
                if ($json.changeType == 1) {//1普通题换综合题，2综合题换普通题                 
                    $("#change_" + ChapterAssembly_TQ_Id).removeClass('question_panel question-panel-hook').addClass('multiple_question');
                }
                else if ($json.changeType == 2) {
                    $("#change_" + ChapterAssembly_TQ_Id).removeClass('multiple_question').addClass('question_panel question-panel-hook');
                }
            }, function () {
                $("#change_" + ChapterAssembly_TQ_Id).html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
            });

        }

        // 确认组卷
        var ConfirmTestpaper = function (Two_WayChecklist_Id, RelationPaper_Id, Name, Titles, CreateUser) {
            //var index = layer.load();
            var dto = {
                key: "combination_testpaper_confirm",
                Two_WayChecklist_Id: Two_WayChecklist_Id,
                RelationPaper_Id: RelationPaper_Id,
                Name: Name,
                Titles: Titles,
                CreateUser: CreateUser,
                x: Math.random()
            }
            $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                //layer.close(index);
                if (data == "ok") {
                    layer.msg("组卷成功", { icon: 1, time: 2000 }, function () {
                        window.opener.window.location.href = "ChapterTestPaper.aspx?ugid=<%=ugid%>";
                        window.close();
                    });
                }
                else {
                    layer.msg("组卷失败", { icon: 2, time: 2000 });
                }
            }, function () {
                //layer.close(index);
                layer.msg("组卷失败err", { icon: 2, time: 2000 });
            });

        }

    </script>
</head>
<body class="body_bg">
    <form id="form1" runat="server">
        <div class="container test_paper_panel" id="divTestPaper"></div>
        <asp:HiddenField ID="hidTitle" runat="server" />
    </form>
</body>
</html>
