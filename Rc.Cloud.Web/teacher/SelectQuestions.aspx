<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectQuestions.aspx.cs" Inherits="Rc.Cloud.Web.teacher.SelectQuestions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>选题</title>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../js/common.js"></script>
    <script src="../js/function.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            RelationPaperTemp_id = "<%=RelationPaperTemp_id%>";
            getTestPaper(RelationPaperTemp_id);

            $(document).on('click', '[data-name="selectTQ"]', function () {
                var Tq_id = $(this).data("tq");
                UpdateTQ(Tq_id);
            });
        })
        var UpdateTQ = function (TQ_Id) {
            var dto = {
                RelationPaperTemp_id: RelationPaperTemp_id,
                TestQuestions_id: TQ_Id,
                x: Math.random()
            };
            $.ajaxWebService("SelectQuestions.aspx/UpdateTQ", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    window.parent.ChangeTQ(RelationPaperTemp_id, "2", "<%=CreateUser%>");
                    window.parent.layer.closeAll();
                }
                else {
                    layer.msg("选择试题失败，请重试。", { icon: 2, time: 2000 });
                }
            }, function () { });
        }

        // 加载试题列表
        var getTestPaper = function (RelationPaperTemp_id) {
            var dto = {
                key: "combination_testpaper_tq_list",
                RelationPaperTemp_id: RelationPaperTemp_id,
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
        <ul class="nav nav-tabs ma">
            <asp:Repeater runat="server" ID="rptTQ">
                <ItemTemplate>
                    <li class="<%#Convert.ToInt16(Eval("R_N"))==1?"active":"" %>""><a href="#<%#Eval("TestQuestions_Id") %>" data-toggle="tab">第<%#Eval("R_N") %>道题</a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <div class="test_paper_panel">
            <div class="panel_body">
                <div class="tab-content ma" id="divTQList"></div>
            </div>
        </div>
    </form>
</body>
</html>
