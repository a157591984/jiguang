<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigSchoolEdit.aspx.cs" Inherits="Rc.Cloud.Web.Sys.ConfigSchoolEdit" %>

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
            var _ConfigEnum = "<%=ConfigEnum%>";
            $("#txtRemark").bind({
                blur: function () { if (this.value.length > 300) this.value = this.value.slice(0, 300) },
                keyup: function () { if (this.value.length > 300) this.value = this.value.slice(0, 300) }
            });
            $("#txtSchoolIP").bind({
                blur: function () { if (this.value.length > 4000) this.value = this.value.slice(0, 4000) },
                keyup: function () { if (this.value.length > 4000) this.value = this.value.slice(0, 4000) }
            });

            $("#txtConfigEnum").blur(function () {
                if (_ConfigEnum == "") {
                    if (!VerifyConfigEnumIsExists($.trim($("#txtConfigEnum").val()))) {
                        layer.msg("标识已存在", { icon: 2, time: 1000 }, function () { $("#txtConfigEnum").focus(); });
                        return false;
                    }
                }
            });
            $("#ddlDType").blur(function () {
                if (!VerifyConfigEnumTypeIsExists($.trim($("#txtConfigEnum").val()), $.trim($("#hidtxtSchool").val()), $.trim($("#ddlDType").val()))) {
                    layer.msg("该学校已添加此类型数据", { icon: 2, time: 1000 });
                    return false;
                }
            });

            $("#btnSubmit").click(function () {
                if ($.trim($("#txtConfigEnum").val()) == "") {
                    layer.msg("标识不能为空", { icon: 2, time: 1000 }, function () { $("#txtConfigEnum").focus(); });
                    return false;
                }
                if (_ConfigEnum == "") {
                    if (!VerifyConfigEnumIsExists($.trim($("#txtConfigEnum").val()))) {
                        layer.msg("标识已存在", { icon: 2, time: 1000 }, function () { $("#txtConfigEnum").focus(); });
                        return false;
                    }
                }

                if ($.trim($("#hidtxtSchool").val()) == "") {
                    layer.msg("学校不能为空", { icon: 2, time: 1000 }, function () { $("#txtSchool").focus(); });
                    return false;
                }
                if ($.trim($("#txtDName").val()) == "") {
                    layer.msg("配置名称不能为空", { icon: 2, time: 1000 }, function () { $("#txtDName").focus(); });
                    return false;
                }
                if ($.trim($("#txtDValue").val()) == "") {
                    layer.msg("内网IP不能为空", { icon: 2, time: 1000 }, function () { $("#txtDValue").focus(); });
                    return false;
                }
                if ($("#txtDSort").val() == "") {
                    $("#txtDSort").val("1");
                }
                if ($.trim($("#ddlDType").val()) == "") {
                    layer.msg("请选择配置类型", { icon: 2, time: 1000 }, function () { $("#ddlDType").select(); });
                    return false;
                }
                if (!VerifyConfigEnumTypeIsExists($.trim($("#txtConfigEnum").val()), $.trim($("#hidtxtSchool").val()), $.trim($("#ddlDType").val()))) {
                    layer.msg("该学校已添加此类型数据", { icon: 2, time: 1000 });
                    return false;
                }

            });

        });
        function VerifyConfigEnumIsExists(strConfigEnum) {
            var flag = false;
            $.ajaxWebService("ConfigSchoolEdit.aspx/VerifyConfigEnumIsExists", "{strConfigEnum:'" + strConfigEnum + "',x:'" + new Date().getTime() + "'}", function (data) {
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
        function VerifyConfigEnumTypeIsExists(strConfigEnum, strSchool_ID, strD_Type) {
            var flag = false;
            $.ajaxWebService("ConfigSchoolEdit.aspx/VerifyConfigEnumTypeIsExists", "{strConfigEnum:'" + strConfigEnum + "',strSchool_ID:'" + strSchool_ID + "',strD_Type:'" + strD_Type + "',x:'" + new Date().getTime() + "'}", function (data) {
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
            <div class="row">
                <div class="col-xs-4">
                    <div class="form-group">
                        <label>标识&nbsp;<span class="text-danger">*</span></label>
                        <asp:TextBox runat="server" ID="txtConfigEnum" CssClass="form-control" MaxLength="20"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-4">
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
                </div>
                <div class="col-xs-4">
                    <div class="form-group">
                        <label>配置名称&nbsp;<span class="text-danger">*</span></label>
                        <asp:TextBox runat="server" ID="txtDName" CssClass="form-control" MaxLength="50"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>对内URL&nbsp;<span class="text-danger">*</span></label>
                        <asp:TextBox runat="server" ID="txtDValue" CssClass="form-control" MaxLength="50"></asp:TextBox>
                        <p class="helper-block">校内资源的对内URL地址，用于学校局域网获取资源</p>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>对外URL</label>
                        <asp:TextBox runat="server" ID="txtDPublicValue" CssClass="form-control" MaxLength="50"></asp:TextBox>
                        <p class="helper-block">校内资源的对外URL地址，用于向学校局域网同步资源</p>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label>学校的公网IP</label>
                <asp:TextBox runat="server" ID="txtSchoolIP" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                <p class="helper-block">
                    用于判断用户使用系统的场所，在学校使用系统时取学校的局域网资源，在学校外时取外网资源<br />
                    当前场所下的IP是：<asp:Literal ID="litIp" runat="server"></asp:Literal>
                </p>
            </div>
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>配置类型&nbsp;<span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlDType" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>配置排序</label>
                        <asp:TextBox runat="server" ID="txtDSort" CssClass="form-control" MaxLength="5">1</asp:TextBox>
                    </div>
                </div>
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
