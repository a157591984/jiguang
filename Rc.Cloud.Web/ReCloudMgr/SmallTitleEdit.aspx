<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmallTitleEdit.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.SmallTitleEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            $("#btnSubmit").click(function () {
                if ($.trim($("#txtTestQuestions_NumStr").val()) == "") {
                    layer.ready(function () { layer.msg("请填写题号", { time: 2000, icon: 2 }, function () { $("#txtTestQuestions_NumStr").focus(); }) });
                    return false;
                }
                if ($.trim($("#txtSort").val()) == "") {
                    layer.ready(function () { layer.msg("请填写序号", { time: 2000, icon: 2 }, function () { $("#txtSort").focus(); }) });
                    return false;
                }
                //if ($.trim($("#txtTargetText").val()) == "") {
                //    layer.ready(function () { layer.msg("请填写测量目标", { time: 2000, icon: 2 }, function () { $("#txtTargetText").focus(); }) });
                //    return false;
                //}
                //if ($.trim($("#txtKnowledgePoint").val()) == "") {
                //    layer.ready(function () { layer.msg("请填写知识点", { time: 2000, icon: 2 }, function () { $("#txtKnowledgePoint").focus(); }) });
                //    return false;
                //} txtScore
                if ($.trim($("#txtScore").val()) == "") {
                    layer.ready(function () { layer.msg("请填写分值", { time: 2000, icon: 2 }, function () { $("#txtScore").focus(); }) });
                    return false;
                }
                if ($.trim($("#ddlTestQuestionType_Web").val()) == "-1") {
                    layer.ready(function () { layer.msg("请选择前端题型", { time: 2000, icon: 2 }, function () { $("#ddlTestQuestionType_Web").focus(); }) });
                    return false;
                }
            })
        })
        function clearNoNum(obj) {
            obj.value = obj.value.replace(/[^\d.]/g, "");  //清除“数字”和“.”以外的字符
            obj.value = obj.value.replace(/^\./g, "");  //验证第一个字符是数字而不是.
            obj.value = obj.value.replace(/\.{2,}/g, "."); //只保留第一个. 清除多余的.
            obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
        }
    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>题号</label>
                        <asp:TextBox runat="server" ID="txtTestQuestions_NumStr" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>序号</label>
                        <asp:TextBox runat="server" ID="txtSort" CssClass="form-control" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>题型</label>
                        <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlTestQuestions_Type" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>分值</label>
                        <asp:TextBox runat="server" ID="txtScore" onkeyup="clearNoNum(this)" CssClass="form-control" MaxLength="5"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>前端题型</label>
                        <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlTestQuestionType_Web" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
