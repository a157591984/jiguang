<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Res_BookAreaEdit.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.Res_BookAreaEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <title>选择资源所属区域</title>
    <script type="text/javascript">
        $(function () {
            var arrSelected = $("#hidAreaIds").val().split(",");
            $("#ulProvince input[type='checkbox']").each(function () {
                if ($.inArray($(this).val(), arrSelected) != -1) {
                    $(this).click();
                }
            });

            $("#ulProvince input[type='checkbox']").click(function () {
                var _chkPSize = $("#ulProvince input[type='checkbox']:checked").size();
                if (_chkPSize == 1) {
                    LoadCity($("#ulProvince input[type='checkbox']:checked").val());
                }
                else {
                    $("#ulCity").html("");
                }
            });
            var _chkPSize = $("#ulProvince input[type='checkbox']:checked").size();
            if (_chkPSize == 1 && $.trim($("#ulCity").html()) == "") {
                LoadCity($("#ulProvince input[type='checkbox']:checked").val());
            }
            $("#btnSave").click(function () {
                var arrProvince = new Array();
                var arrCity = new Array();
                $("#ulProvince input[type='checkbox']:checked").each(function () {
                    arrProvince.push($(this).val());
                });
                $("#ulCity input[type='checkbox']:checked").each(function () {
                    arrCity.push($(this).val());
                });
                if (arrProvince.length == 0 && arrCity.length == 0) {
                    layer.ready(function () {
                        layer.msg("请选择区域", { icon: 4 });
                    });
                    return false;
                }
                $("#hidAreaIds").val(arrProvince.join(","));
                if (arrCity.length != 0) $("#hidAreaIds").val(arrCity.join(","));

            });
        });
        var LoadCity = function (pid) {
            var index = parent.layer.getFrameIndex(window.name);
            $.ajaxWebService("Res_BookAreaEdit.aspx/GetAreaInfo", "{pid:'" + pid + "',selectedAreaId:'" + $("#hidAreaIds").val() + "'}", function (data) {
                $("#ulCity").html(data.d);
                parent.layer.iframeAuto(index);
            }, function () {
                $("#ulCity").html("");
                parent.layer.iframeAuto(index);
            }, false);
        }
        function Handel(isSuccess, mes) {
            layer.ready(function () {
                layer.msg(mes, {
                    icon: 1,
                    time: 1000
                }, function () {
                    if (isSuccess == 1) {
                        parent.loadData();
                        parent.CloseDialog();
                    }
                });
            });
        }
    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <h4>省份</h4>
            <div class="row checkbox" id="ulProvince">
                <asp:Repeater runat="server" ID="rptProvince">
                    <ItemTemplate>
                        <div class="col-xs-3">
                            <label>
                                <input type="checkbox" value="<%#Eval("Regional_Dict_Id") %>" /><%#Eval("D_Name") %></label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <h4>市区</h4>
            <div class="row checkbox" id="ulCity">
            </div>
            <asp:Button runat="server" ID="btnSave" Text="确定" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
        <input type="hidden" id="hidAreaIds" runat="server" />
    </form>
</body>
</html>
