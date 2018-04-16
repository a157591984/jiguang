<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentErrorAnalysis.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.StudentErrorAnalysis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>错题分析</title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/jquery.min-1.11.1.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../plugin/layer/layer.js"></script>
    <script type="text/javascript" src="../js/json2.js"></script>
    <script type="text/javascript" src="../js/jquery-jtemplates.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script src="../plugin/jqprint/jquery-migrate-1.2.1.min.js"></script>
    <script src="../plugin/jqprint/jquery.jqprint-0.3.js"></script>
    <script type="text/javascript">
        $(function () {
            HomeWork_Id_ = "<%=HomeWork_Id%>";
            Student_Id_ = "<%=StudentId%>";
            //layer.msg("正在初始化试题数据，请耐心等待。", { icon: 16, time: 0, shade: [0.1, '#fff'] });
            //InitializationData(HomeWork_Id_);
            GetGeneralData();

        })
        // 初始化数据
        var InitializationData = function (HomeWork_Id_) {
            var dto = {
                HomeWork_Id: HomeWork_Id_,
                x: Math.random()
            };
            $.ajaxWebService("StudentErrorAnalysis.aspx/InitializationData", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    layer.msg("", { icon: 1, time: 1 });

                }
                else {
                    layer.msg("初始化试题数据失败", { icon: 2, time: 2000 });
                }
            }, function () {
                layer.msg("初始化试题数据失败err", { icon: 2, time: 2000 });
            }, false);
        }
        //综述
        var GetGeneralData = function () {

            var dto = {
                HomeWork_Id: HomeWork_Id_,
                Student_Id: Student_Id_,
                x: Math.random()
            };
            $.ajaxWebService("StudentErrorAnalysis.aspx/GetGeneralData", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $("#GeneralData").html(data.d);
                    loadDataKP();
                }
                else {
                    layer.msg("错题综述异常", { icon: 2, time: 2000 });
                }
            }, function () {
                layer.msg("错题综述异常err", { icon: 2, time: 2000 });
            });
        }
        //按知识点分析
        var loadDataKP = function () {

            var $_objBox = $("#list");
            var objBox = "list";
            var $_objList = $("#tb1");
            var dto = {
                HomeWork_Id: HomeWork_Id_,
                Student_Id: Student_Id_,
                x: Math.random()
            };
            $.ajaxWebService("StudentErrorAnalysis.aspx/GetListKP", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $_objList.setTemplateElement(objBox, null, { filter_data: false });
                    $_objList.processTemplate(json);
                    loadDataTQ();
                }
                else {
                    $_objList.html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    loadDataTQ();
                }

            }, function () { });
        }
        //按题分析
        var loadDataTQ = function () {
            var $_objBox = $("#list2");
            var objBox = "list2";
            var $_objList = $("#tb2");
            var dto = {
                HomeWork_Id: HomeWork_Id_,
                Student_Id: Student_Id_,
                x: Math.random()
            };
            $.ajaxWebService("StudentErrorAnalysis.aspx/GetListTQ", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $_objList.setTemplateElement(objBox, null, { filter_data: false });
                    $_objList.processTemplate(json);
                }
                else {
                    $_objList.html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                }

            }, function () { });
        }

    </script>
    <script>
        $(function () {
            $('.jqprint-hook').on('click', function (e) {
                var dom = $(this).data('dom');
                $("#" + dom).jqprint();
            })
        });
    </script>
</head>
<body class="body_bg">
    <form id="form1" runat="server">
        <ul class="res_fixed_sidebar">
            <%=link %>
        </ul>
        <div class="container pb relative">
            <div class="res_info">
                <h2 class="res_title">
                    <asp:Literal ID="ltlHwName" runat="server"></asp:Literal></h2>
                <div class="res_desc text-center text-muted">
                    <span>年级：<asp:Literal ID="ltlGradeName" runat="server"></asp:Literal></span>
                    <span>班级：<asp:Literal ID="ltlClassName" runat="server"></asp:Literal></span>
                    <span>学生：<asp:Literal ID="ltlSundentName" runat="server"></asp:Literal></span>
                    <span>满分：<asp:Literal ID="ltlHwSorce" runat="server"></asp:Literal>分</span>
                    <span>得分：<asp:Literal ID="ltlStuScorce" runat="server"></asp:Literal>分</span>
                </div>
            </div>
            <p id="GeneralData"></p>
            <h4>按知识点分析
                <input type="button" value="打印" class="btn btn-default btn-sm jqprint-hook" data-dom="knowledgePointAnalysis" /></h4>
            <div id="knowledgePointAnalysis">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <td>重要程度</td>
                            <td>知识点</td>
                            <td>中考对应值</td>
                            <td>难度</td>
                            <td>题型</td>
                            <td>题号</td>
                        </tr>
                    </thead>
                    <tbody id="tb1">
                    </tbody>
                </table>
                <textarea id="list" class="hidden">
                {#foreach $T.list as record}
                    <tr>
                        <td>{$T.record.KPImportant}</td>
                        <td>{$T.record.KPNameBasic}</td>
                        <td>{$T.record.GKScore}</td>
                        <td>{$T.record.ComplexityText}</td>
                        <td>{$T.record.TestType}</td>
                        <td>{$T.record.topicNumber}</td>
                    </tr>
                {#/for}
            </textarea>
            </div>
            <h4 class="pt">按错题分析
                <input type="button" value="打印" class="btn btn-default btn-sm jqprint-hook" data-dom="errorAnalysis" /></h4>
            <div id="errorAnalysis">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <td>题号</td>
                            <td>知识点</td>
                            <td>分值</td>
                            <td>得分</td>
                            <td>重要程度</td>
                        </tr>
                    </thead>
                    <tbody id="tb2">
                    </tbody>
                </table>
                <textarea id="list2" class="hidden">
                {#foreach $T.list as record}
                    <tr>
                        <td>{$T.record.topicNumber}</td>
                        <td>{$T.record.KPNameBasic}</td>
                        <td>{$T.record.TQScore}</td>
                        <td>{$T.record.Score}</td>
                        <td>{$T.record.KPImportant}</td>
                    </tr>
                {#/for}
              </textarea>
            </div>
        </div>
    </form>
</body>
</html>
