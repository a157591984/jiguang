<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchoolSMSAdd.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SchoolSMSAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/auto-complete/css/style.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/auto-complete/js/AutoComplete.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script src="../SysLib/js/jq.pagination.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);

            $("#txtRemark").bind({
                blur: function () { if (this.value.length > 300) this.value = this.value.slice(0, 300) },
                keyup: function () { if (this.value.length > 300) this.value = this.value.slice(0, 300) }
            });
            $("#txtSMSCount").bind({
                blur: function () { this.value = this.value.replace(/\D/g, ''); },
                keyup: function () { this.value = this.value.replace(/\D/g, ''); }
            });

            $("#btnSubmit").click(function () {
                if ($.trim($("#hidtxtSchool").val()) == "") {
                    layer.msg("请先选择学校", { icon: 2, time: 1000 }, function () { $("#txtSchool").focus(); });
                    return false;
                }
                if ($.trim($("#txtSMSCount").val()) == "") {
                    layer.msg("短信条数不能为空", { icon: 2, time: 1000 }, function () { $("#txtSMSCount").focus(); });
                    return false;
                }

            });
        });
    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>学校&nbsp;<span class="text-danger">*</span></label>
                <input type="hidden" id="hidtxtSchool" clientidmode="Static" runat="server" />
                <input type="text" id="txtSchool" clientidmode="Static" class="form-control" runat="server"
                    pautocomplete="True"
                    pautocompleteajax="AjaxAutoCompletePaged"
                    pautocompleteajaxkey="SCHOOL"
                    pautocompletevectors="AutoCompleteVectors"
                    pautocompleteisjp="0"
                    pautocompletepagesize="10" autocomplete="off" />
            </div>
            <div class="form-group">
                <label>短信条数&nbsp;<span class="text-danger">*</span></label>
                <asp:TextBox runat="server" ID="txtSMSCount" CssClass="form-control" MaxLength="10"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>说明</label>
                <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Rows="6" CssClass="form-control"></asp:TextBox>
            </div>
            <div style="text-align: right">
                <asp:Button ID="btnSubmit" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            </div>
        </div>
        <!--智能匹配载体-->
        <div id="AutoCompleteVectors" class="AutoCompleteVectors">
            <div id="topAutoComplete" class="topAutoComplete">
                简拼/汉字或↑↓
            </div>
            <div id="divAutoComplete" class="divAutoComplete">
                <ul id="AutoCompleteDataList" class="AutoCompleteDataList">
                </ul>
            </div>
        </div>
    </form>
</body>
</html>
