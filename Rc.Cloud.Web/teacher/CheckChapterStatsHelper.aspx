<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckChapterStatsHelper.aspx.cs" Inherits="Rc.Cloud.Web.teacher.CheckChapterStatsHelper" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="../js/jquery.min-1.11.1.js"></script>
    <script type="text/javascript" src="../scripts/function.js"></script>
    <script type="text/javascript" src="../Scripts/plug-in/layer/layer.js"></script>
    <script type="text/javascript">

        $(function () {
            _Identifier_Id = "<%=Identifier_Id%>";
            _strUserGroup_IdActivity = "<%=strUserGroup_IdActivity%>";
            layer.msg("正在组卷，请耐心等待。", { icon: 16, time: 0, shade: [0.1, '#fff'] });
            Submit();
        });
        var Submit = function (arryKP, arryAttr, arrTQType, totalCountTQ) {
            var dto = {
                arryKP: window.opener.getDataForSub("kpcked"),
                arryAttr: window.opener.getDataForSub("tqcount"),
                identifier: "<%=Identifier_Id%>",
                arrTQType: window.opener.getDataForSub("tqtype"),
                totalCountTQ: window.opener.getDataForSub("totalcounttq"),
                x: Math.random()
            };
            $.ajax({
                type: "POST",
                async: true,
                contentType: "application/json; charset=utf-8",
                url: "ChapterAssembly.aspx/Submit",
                data: JSON.stringify(dto),
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        window.location.href = "CombinationTestPaperToChapter.aspx?Identifier_Id=<%=Identifier_Id%>&ugid=<%=strUserGroup_IdActivity%>";
                    }
                    else if (data.d == "2") {
                        layer.msg('试题数量大于题库中的试题数量请重新填写', { icon: 2, time: 4000 }, function () { window.close(); });
                    }
                    else {
                        layer.msg('生成数据失败，请重试.....', { icon: 2, time: 4000 }, function () { window.close(); });
                    }
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
