<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DictRelationDetail_Add.aspx.cs" Inherits="Rc.Cloud.Web.Sys.DictRelationDetail_Add" %>

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
    <link href="../SysLib/plugin/mfilter-1.0/mfilter.css" rel="stylesheet" />
    <script src="../SysLib/plugin/mfilter-1.0/mfilter.js"></script>
    <script src="../SysLib/js/function.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel">
            <div class="panel-body">
                <div class="panel panel-default mfilter" data-name="mfilter">
                    <div class="mfilter_control" data-name="mfilterControl">
                        <div class="mfilter_body" data-name="mfilterBody">
                            <div class="mfilter_item" data-name="mfilterItem" ajax-name="FirstCode">
                                <asp:Literal ID="ltlFilst" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                    <div ajax-name="SecondCode">
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="panel panel-default mfilter">
                    <div class="mfilter_control">
                        <div class="mfilter_body">
                            <div class="mfilter_item">
                                <a href="javascript:;" class="active">
                                    <label id="title"></label>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div ajax-name="LastCode">
                    </div>
                </div>
            </div>
            <div class="form-group" style="text-align: center">
                <input type="button" name="btnSave" value="保存" id="btnSave" class="btn btn-primary" />
            </div>
            <input type="hidden" id="hidLastCode" />
        </div>
    </form>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            loadDataSecond();
            loadDataLast();
            $('[data-name="mfilter"]').mfilter({
                onClick: function (obj) {
                    loadDataSecond();
                    loadDataLast();
                }
            });
            $("#btnSave").click(function () {
                var arrCked = new Array();
                $("[ajax-name='LastCode'] input[type='checkbox']").each(function () {
                    if (this.checked) {
                        arrCked.push($(this).val())
                    }
                })

                $("#hidLastCode").val(arrCked);
                if ($("#hidLastCode").val() == "" || $("#hidLastCode").val() == undefined) {
                    layer.msg("没有选择关联的数据字典项！", { icon: 2, time: 2000 });
                    return false;
                }
                else {
                    AddData();
                }
            })
        })
        var loadDataSecond = function () {
            var dto = {
                FirstCode: $('[ajax-name="FirstCode"]').find('a.active').attr('ajax-value'),
                x: Math.random()
            };
            $.ajaxWebService("DictRelationDetail_Add.aspx/GetSecondData", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $('[ajax-name="SecondCode"]').html(data.d);
                }
            }, function () { });
        }
        var loadDataLast = function () {
            var dto = {
                SonDict_Id: "<%=SonDict_Id%>",
                x: Math.random()
            };
            $.ajaxWebService("DictRelationDetail_Add.aspx/GetLastData", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "成功") {
                    $('[ajax-name="LastCode"]').html(json.StrCode);
                    $("#title").html(json.title);
                }
            }, function () { });
        }
        var AddData = function () {
            var dto = {
                FirstCode: $('[ajax-name="FirstCode"]').find('a.active').attr('ajax-value'),
                SecondCode: $("[ajax-name='SecondCode'] input[type='radio']:checked").val(),
                LastCode: $("#hidLastCode").val(),
                DictRelation_Id: "<%=DictRelation_Id%>",
                x: Math.random()
            };

            $.ajaxWebService("DictRelationDetail_Add.aspx/AddData", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    $(function () { layer.ready(function () { layer.msg('添加关系成功!', { time: 1000, icon: 1 }, function () { parent.location.reload(); parent.layer.close(index) }); }) })
                }
                else { layer.msg('添加关系失败', { icon: 2, tiem: 2000 }); }
            }, function () { });
        }
    </script>

</body>
</html>
