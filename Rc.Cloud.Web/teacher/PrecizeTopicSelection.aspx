<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrecizeTopicSelection.aspx.cs" Inherits="Rc.Cloud.Web.teacher.PrecizeTopicSelection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>精准选题</title>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../js/common.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../js/function.js"></script>
    <script type="text/javascript">
        $(function () {
            _ChapterAssembly_TQ_Id = "<%=ChapterAssembly_TQ_Id%>";
            getTestPaper(_ChapterAssembly_TQ_Id);

            $(document).on('click', '.ischange-hook', function () {
                var Tq_id = $(this).attr("tt");
                UpdateTQ(Tq_id);
            });
        })
        var UpdateTQ = function (TQ_Id) {
            var dto = {
                ChapterAssembly_TQ_Id: _ChapterAssembly_TQ_Id,
                TestQuestions_id: TQ_Id,
                x: Math.random()
            };
            $.ajaxWebService("PrecizeTopicSelection.aspx/UpdateTQ", JSON.stringify(dto), function (data) {
                var _json = $.parseJSON(data.d);
                if (_json.err=="") {
                    window.parent.ChangeTQ(_ChapterAssembly_TQ_Id, "2", _json.RetrunValue, _json.ChangeType);
                    window.parent.layer.closeAll();
                }
                else {
                    layer.msg("选择试题失败，请重试。", { icon: 2, time: 2000 });
                }
            }, function () { });
        }

        // 加载试题列表
        var getTestPaper = function (ChapterAssembly_TQ_Id) {
            var dto = {
                key: "combination_testpaperchapter_tq_list",
                ChapterAssembly_TQ_Id: ChapterAssembly_TQ_Id,
                x: Math.random()
            };
            $.ajaxWeb("<%=strTestpaperViewWebSiteUrl%>AuthApi/getTestpaper.ashx", dto, function (data) {
                $("#divTQList").html(data);
            }, function () {
                $("#tbTQList").html("请求失败：请检查学校网络是否畅通,<%=strTestpaperViewWebSiteUrl%>");
            });

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pa">
            <div class="filter">
                <div class="form-inline">
                    <div class="form-group">
                        <label>关键字：</label>
                        <input type="text" name="name" value="" class="form-control input-sm" />
                    </div>
                    <button type="button" class="btn btn-primary btn-sm">检索</button>
                </div>
                <div class="filter_section">
                    <div class="filter_row clearfix">
                        <span class="row_name">知识点：</span>
                        <div class="row_item">
                            <ul>
                                <li><a href="javascript:;" class="active">全部</a></li>
                                <li><a href="javascript:;">函数</a></li>
                                <li><a href="javascript:;">变量</a></li>
                                <li><a href="javascript:;">勾股定理</a></li>
                                <li><a href="javascript:;">余弦定理</a></li>
                                <li><a href="javascript:;">三角定理</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">考点：</span>
                        <div class="row_item">
                            <ul>
                                <li><a href="javascript:;" class="active">全部</a></li>
                                <li><a href="javascript:;">判断集合</a></li>
                                <li><a href="javascript:;">判断费集合</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">来源：</span>
                        <div class="row_item">
                            <ul>
                                <li><a href="javascript:;" class="active">全部</a></li>
                                <li><a href="javascript:;">高考真题</a></li>
                                <li><a href="javascript:;">常考题</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">难易度：</span>
                        <div class="row_item">
                            <ul>
                                <li><a href="javascript:;" class="active">全部</a></li>
                                <li><a href="javascript:;">简单</a></li>
                                <li><a href="javascript:;">中等</a></li>
                                <li><a href="javascript:;">困难</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">测量目标：</span>
                        <div class="row_item">
                            <ul>
                                <li><a href="javascript:;" class="active">全部</a></li>
                                <li><a href="javascript:;">基本知识和基本技能</a></li>
                                <li><a href="javascript:;">空间想象能力</a></li>
                                <li><a href="javascript:;">逻辑思维能力</a></li>
                                <li><a href="javascript:;">运算能力</a></li>
                                <li><a href="javascript:;">分析问题与解决问题的能力</a></li>
                                <li><a href="javascript:;">数学探究与创新能力</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="test_paper_panel" id="divTQList">
            </div>
        </div>
    </form>
</body>
</html>
