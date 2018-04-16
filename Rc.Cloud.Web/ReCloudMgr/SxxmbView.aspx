<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SxxmbView.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.SxxmbView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/json2.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <title>预览</title>
    <script type="text/javascript">
        $(function () {
            loadData();
        })
        var loadData = function () {
            var Two_WayChecklist_Id = "<%=Two_WayChecklist_Id%>";
            var dto = {
                Two_WayChecklist_Id: Two_WayChecklist_Id,
                x: Math.random()
            };
            $.ajaxWebService("SxxmbView.aspx/GetSxxmbList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template_1", null, { filter_data: false });
                    $("#tb1").processTemplate(json);
                }
                else {
                    $("#tb1").html("<tr class='tr_con_002'><td colspan='100' align='center'>暂无数据</td></tr>");
                }
                if (json.list == null || json.list == "") {

                }
            }, function () { });
        }
    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <h4>
                <asp:Literal ID="ltlName" runat="server"></asp:Literal></h4>
            <p>
                <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
            </p>
            <table class="table table-bordered table-hover">
                <thead>
                    <tr>
                        <th width="5%">题号</th>
                        <th width="7%">题型</th>
                        <th>测量目标</th>
                        <th>知识点</th>                        
                        <th width="5%">难易度</th>
                        <th width="5%">分值</th>
                    </tr>
                </thead>
                <tbody id="tb1">
                </tbody>
            </table>
        </div>
    </form>
    <textarea id="template_1" class="display_none" style="display: none;">
    {#foreach $T.list as record}
     <tr class="tr_con_001">
        <td>{$T.record.TestQuestions_NumStr}</td>
        <td>{$T.record.TestQuestions_Type}</td>
        <td class="text-left">{$T.record.TargetText}</td>
        <td class="text-left">{$T.record.KnowledgePoint}</td>
        <td>{$T.record.ComplexityText}</td>
        <td>{$T.record.Score}</td>
     </tr>
    {#/for}
    </textarea>
</body>
</html>
