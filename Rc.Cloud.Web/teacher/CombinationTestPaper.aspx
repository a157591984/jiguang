<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CombinationTestPaper.aspx.cs" Inherits="Rc.Cloud.Web.teacher.CombinationTestPaper" %>

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
            ugid = "<%=ugid%>";
            Two_WayChecklist_Id = "<%=Two_WayChecklist_Id%>";
            RelationPaper_Id = "<%=guid%>";
            CreateUser = "<%=CreateUser%>";
            Two_WayChecklist_Name = "<%=Two_WayChecklist_Name%>";
            //index = layer.msg("正在初始化试题数据，请耐心等待。", { icon: 16, time: 0, shade: [0.1, '#fff'] });
            InitializationTestQuestions(Two_WayChecklist_Id, RelationPaper_Id, CreateUser);
            //换题
            $(document).on('click', ".ischange-hook", function () {
                var id = $(this).attr('tt');
                ChangeTQ(id, "1", CreateUser);
            })
            //手动换题
            $(document).on('click', ".changetq-hook", function () {
                var id = $(this).attr('tt');
                layer.open({
                    type: 2,
                    title: '精准选题',
                    area: ['80%', '80%'],
                    scrollbar: false,
                    content: 'SelectQuestions.aspx?RelationPaperTemp_id=' + id
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
                    $(".question-type-input-hook").each(function () {
                        arrCked.push($(this).val())
                    })
                    ConfirmTestpaper(Two_WayChecklist_Id, RelationPaper_Id, $("#rename").val(), arrCked.join("♧"), CreateUser)
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
        var getTestPaper = function (Two_WayChecklist_Id, RelationPaper_Id, Two_WayChecklist_Name) {

            var dto = {
                key: "combination_testpaper_view",
                Two_WayChecklist_Id: Two_WayChecklist_Id,
                RelationPaper_Id: RelationPaper_Id,
                Two_WayChecklist_Name: Two_WayChecklist_Name,
                x: Math.random()
            };
            $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                $("#divTestPaper").html(data);
            }, function () {
                $("#divTestPaper").html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
                });

        }
        // type=1随机切换 type=2选择换题
        var ChangeTQ = function (RelationPaperTemp_id, Type, CreateUser) {
            var dto = {
                key: "combination_testpaper_tq_change",
                RelationPaperTemp_id: RelationPaperTemp_id,
                Type: Type,
                CreateUser: CreateUser,
                x: Math.random()
            };
            $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                $("#change_" + RelationPaperTemp_id).html(data);
            }, function () {
                $("#change_" + RelationPaperTemp_id).html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
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
                        window.opener.window.location.href = "historyTestPaper.aspx?ugid=" + ugid + "&Two_WayChecklist_Id=" + Two_WayChecklist_Id;
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
