<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatsLogOperate.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.StatsLogOperate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Styles/style001/Style.css" rel="stylesheet" />
    <link rel="stylesheet" href="./../Styles/style001/LayerForm.css" />
    <script type="text/javascript" src="../Scripts/js001/jquery.min-1.8.2.js"></script>
    <script type="text/javascript" src="../Scripts/plug-in/layer/layer.js"></script>
    <script type="text/javascript" src="../Scripts/plug-in/laydate/laydate.js"></script>
    <script src="../SysLib/plugin/auto-complete/js/AutoComplete.js"></script>
    <script type="text/javascript" src="../Scripts/function.js"></script>
    <script type="text/javascript" src="../Scripts/plug-in/laydate/laydate.js"></script>
    <link href="../Styles/styles003/style01.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="default_form_box form_box">
            <dl class="clearfix">
                <dt>执行方式<span>*</span></dt>
                <dd>
                    <select id="ddlType" class="select wauto" runat="server">
                        <option value="1">按日期</option>
                        <option value="2">按试卷</option>
                    </select>
                </dd>
            </dl>
            <dl class="clearfix" data-name="rtrf" style="display: none;">
                <dt>试卷名称<span>*</span></dt>
                <dd>
                    <input type="hidden" id="hidtxtRTRFName" clientidmode="Static" class="txt" runat="server" />
                    <input type="text" id="txtRTRFName" clientidmode="Static" class="input_text" runat="server"
                        pautocomplete="True"
                        pautocompleteajax="AjaxAutoCompletePaged"
                        pautocompleteajaxkey="ResourceToResourceFolder"
                        pautocompletevectors="AutoCompleteVectors"
                        pautocompleteisjp="0"
                        pautocompletepagesize="10" autocomplete="off" />
                </dd>
            </dl>
            <dl class="clearfix" data-name="homework">
                <dt>开始时间<span>*</span></dt>
                <dd>
                    <asp:TextBox runat="server" ID="txtSTime" CssClass="input_text" MaxLength="50"></asp:TextBox>
                </dd>
            </dl>
            <dl class="clearfix" data-name="homework">
                <dt>结束时间<span>*</span></dt>
                <dd>
                    <asp:TextBox runat="server" ID="txtETime" CssClass="input_text" MaxLength="50"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt></dt>
                <dd>
                    <asp:Button ID="btnSubmit" runat="server" Text="执行" CssClass="input_btn" OnClick="btnSubmit_Click" />
                </dd>
            </dl>
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
        <script type="text/javascript">
            $(function () {
                index = parent.layer.getFrameIndex(window.name);

                $('#txtSTime').val(laydate.now(-1, 'YYYY-MM-DD'));
                $('#txtETime').val(laydate.now(-1, 'YYYY-MM-DD'));
                var starTime = {
                    elem: '#txtSTime',
                    format: 'YYYY-MM-DD',
                    max: laydate.now(0, 'YYYY-MM-DD'),
                    choose: function (datas) {
                        endTime.min = datas; //开始日选好后，重置结束日的最小日期
                        endTime.max = laydate.now(0, 'YYYY-MM-DD');
                        endTime.start = datas //将结束日的初始值设定为开始日
                    }
                };
                var endTime = {
                    elem: '#txtETime',
                    format: 'YYYY-MM-DD',
                    max: laydate.now(0, 'YYYY-MM-DD'),
                    choose: function (datas) {
                        starTime.max = datas; //结束日选好后，重置开始日的最大日期
                    }
                };
                laydate(starTime);
                laydate(endTime);

                $("#ddlType").change(function () {
                    if ($(this).val() == "1") {
                        $('[data-name="homework"]').show();
                        $('[data-name="rtrf"]').hide();
                    }
                    else {
                        $('[data-name="homework"]').hide();
                        $('[data-name="rtrf"]').show();
                    }
                });

                $("#btnSubmit").click(function () {
                    if ($.trim($("#ddlType").val()) == "") {
                        layer.msg("请选择执行方式", { icon: 2, time: 1000 }, function () { $("#ddlType").select(); });
                        return false;
                    }
                    if ($("#ddlType").val() == "1") {
                        if ($.trim($("#txtSTime").val()) == "") {
                            layer.msg("请选择开始时间", { icon: 2, time: 1000 }, function () { $("#txtSTime").focus(); });
                            return false;
                        }
                        if ($.trim($("#txtETime").val()) == "") {
                            layer.msg("请选择结束时间", { icon: 2, time: 1000 }, function () { $("#txtETime").focus(); });
                            return false;
                        }
                    }
                    else {
                        if ($.trim($("#hidtxtRTRFName").val()) == "") {
                            layer.msg("请选择试卷名称", { icon: 2, time: 1000 }, function () { $("#txtRTRFName").focus(); });
                            return false;
                        }
                    }
                    layer.load();
                });

            });

        </script>
    </form>
</body>
</html>
