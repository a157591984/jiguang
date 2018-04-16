<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SxxmbDetailEdit.aspx.cs" Inherits="Rc.Cloud.Web.teacher.SxxmbDetailEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../js/common.js"></script>
    <script src="../plugin/layer/layer.js"></script>
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
            })
        })
    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="container-fluid ph assign_hw_cont">
            <div class="form-group">
                <label>题号</label>
                <asp:TextBox runat="server" ID="txtTestQuestions_NumStr" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>序号</label>
                <asp:TextBox runat="server" ID="txtSort" CssClass="form-control" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
            </div>
          <%--  <div class="form-group">
                <label>题型</label>
                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlTestQuestions_Type" CssClass="form-control"></asp:DropDownList>
            </div>--%>
            <div class="form-group">
                <label>分值</label>
                <asp:TextBox runat="server" ID="txtScore" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
