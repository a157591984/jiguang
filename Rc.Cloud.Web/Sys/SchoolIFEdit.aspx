<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchoolIFEdit.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SchoolIFEdit" %>

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
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            var _SchoolIF_Id = "<%=SchoolIF_Id%>";
            $("#txtRemark").bind({
                blur: function () { if (this.value.length > 300) this.value = this.value.slice(0, 300) },
                keyup: function () { if (this.value.length > 300) this.value = this.value.slice(0, 300) }
            });

            $("#ddlSchoolIF_Code").blur(function () {
                if (!VerifySchoolIF_CodeIsExists($.trim($("#ddlSchoolIF_Code").val()), _SchoolIF_Id)) {
                    layer.msg("标识已添加", { icon: 2, time: 1000 }, function () { $("#ddlSchoolIF_Code").focus(); });
                    return false;
                }
            });

            $("#btnSubmit").click(function () {
                if ($.trim($("#ddlSchoolIF_Code").val()) == "") {
                    layer.msg("请选择标识", { icon: 2, time: 1000 }, function () { $("#ddlSchoolIF_Code").focus(); });
                    return false;
                }
                if ($.trim($("#txtSchoolIF_Name").val()) == "") {
                    layer.msg("配置名称不能为空", { icon: 2, time: 1000 }, function () { $("#txtSchoolIF_Name").focus(); });
                    return false;
                }
                if ($.trim($("#hidtxtSchool").val()) == "") {
                    layer.msg("学校不能为空", { icon: 2, time: 1000 }, function () { $("#txtSchool").focus(); });
                    return false;
                }
                
            });

        });
        function VerifySchoolIF_CodeIsExists(SchoolIF_Id, SchoolIF_Code) {
            var flag = false;
            var dto = {
                SchoolIF_Code: SchoolIF_Code,
                SchoolIF_Id: SchoolIF_Id,
                x: Math.random()
            };
            $.ajaxWebService("SchoolIFEdit.aspx/VerifySchoolIF_CodeIsExists", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err != "null") {
                    flag = false;
                }
                else if (json.err == "null") {
                    flag = true;
                }
            }, function () { flag = false; }, false);
            return flag;
        }

    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>配置标识&nbsp;<span class="text-danger">*</span></label>
                <asp:DropDownList runat="server" ID="ddlSchoolIF_Code" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group">
                <label>配置名称&nbsp;<span class="text-danger">*</span></label>
                <asp:TextBox runat="server" ID="txtSchoolIF_Name" CssClass="form-control" MaxLength="50"></asp:TextBox>
            </div>
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
                <label>说明</label>
                <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
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
