<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamPointEdit.aspx.cs" Inherits="Rc.Cloud.Web.Sys.ExamPointEdit" %>

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
    <script src="../SysLib/plugin/select2/dist/js/select2.min.js"></script>
    <script src="../SysLib/plugin/select2/dist/js/i18n/zh-CN.js"></script>
    <link href="../SysLib/plugin/select2/dist/css/select2.min.css" rel="stylesheet" />
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>层级 <span class="text-danger">*</span></label>
                <asp:DropDownList ID="ddlKPLevel" runat="server" ClientIDMode="Static" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="form-group hide" id="divCognitive_Level">
                <label>认知水平 <span class="text-danger">*</span></label>
                <asp:DropDownList ID="ddlCognitive_Level" runat="server" ClientIDMode="Static" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>是否最后一级 <span class="text-danger">*</span></label>
                <div class="radio radio_1">
                    <asp:RadioButton ID="rbtIsLast1" GroupName="IsLast" ClientIDMode="Static" value="1"
                        Text="是" runat="server" />
                    <asp:RadioButton ID="rbtIsLast0" GroupName="IsLast"
                        ClientIDMode="Static" value="0" Text="否" runat="server" Checked="true" />
                </div>
            </div>
            <div class="form-group">
                <label>名称 <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtTPName" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="100" autocomplete="off"></asp:TextBox>
                <select id="ddlTPName" class="form-control js-data-example-ajax" multiple="multiple">
                </select>
                <asp:HiddenField runat="server" ID="hidTPNameBasic_Id" />
                <asp:HiddenField runat="server" ID="hidTPNameBasic" />
            </div>
            <div class="form-group">
                <label>编码 <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtTPCode" runat="server" CssClass="form-control" autocomplete="off">01</asp:TextBox>
            </div>
            <div class="form-group">
                <label>重要程度 <span class="text-danger">*</span></label>
                <div class="radio radio_1">
                    <asp:RadioButton ID="rbt0" GroupName="Importance" ClientIDMode="Static" value="1"
                        Text="重要" runat="server" />
                    <asp:RadioButton ID="rbt1" GroupName="Importance"
                        ClientIDMode="Static" value="0" Text="次要" runat="server" Checked="true" />
                </div>
            </div>
            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        index = parent.layer.getFrameIndex(window.name);

        //异步加载知识点
        $("#ddlTPName").select2({
            language: "zh-CN",
            tags: true,
            maximumSelectionLength: 1,  //最多能够选择的个数
            placeholder: "选择/输入",
            allowClear: true,
            ajax: {
                url: "/Ajax/getDataForSelect2.ashx",
                dataType: 'json',
                delay: 250,
                data: function (params) {
                    return {
                        key: "tp",
                        GradeTerm: "<%=GradeTerm%>",
                        Subject: "<%=Subject%>",
                        name: params.term, // search term
                        pageIndex: params.page || 1,
                        pageSize: 10
                    };
                },
                processResults: function (data, params) {
                    params.pageIndex = params.pageIndex || 1;

                    return {
                        results: data.items,
                        pagination: {
                            more: (params.pageIndex * 10) < data.total_count
                        }
                    };
                },
                cache: true
            },
            escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
            minimumInputLength: 0,
            templateResult: formatRepoProvince, // omitted for brevity, see the source of this page
            templateSelection: formatRepoProvince // omitted for brevity, see the source of this page
        });
        $("span.select2").hide();

        $("#rbtIsLast1").click(function () {
            $("#txtTPName").hide();
            $("span.select2").show();
        });
        $("#rbtIsLast0").click(function () {
            $("#txtTPName").show();
            $("span.select2").hide();
        });
        //初始化select2值
        if ($("#hidTPNameBasic").val() != "") {
            $("#ddlTPName").append(new Option($("#hidTPNameBasic").val(), $("#hidTPNameBasic_Id").val(), false, true));
        }
        //层级项为考点，显示认知水平
        if ($("#ddlKPLevel").val() == "59c01827-b54d-4760-a8fa-bd05315163a6") $("#divCognitive_Level").removeClass("hide");
        $("#ddlKPLevel").change(function () {
            if ($(this).val() == "59c01827-b54d-4760-a8fa-bd05315163a6") {//层级项为考点，显示认知水平
                $("#divCognitive_Level").removeClass("hide");
            }
            else {
                $("#divCognitive_Level").addClass("hide");
                $("#ddlCognitive_Level").val("-1");
            }
        });
        //是最后一级，显示选择知识点
        if ($("#rbtIsLast1").prop("checked")) {
            $("#txtTPName").hide();
            $("span.select2").show();
        }


        $("#btnSave").click(function () {
            if ($.trim($("#ddlKPLevel").val()) == "") {
                layer.ready(function () {
                    layer.msg('请选择层级', { icon: 4, time: 2000 }, function () { $("#ddlKPLevel").focus(); })
                });
                return false;
            }
            if ($("#ddlKPLevel").val() == "59c01827-b54d-4760-a8fa-bd05315163a6") {//层级项为考点，显示认知水平
                if ($("#ddlCognitive_Level").val() == "-1") {
                    layer.ready(function () {
                        layer.msg('请选择认知水平', { icon: 4, time: 2000 }, function () { $("#ddlCognitive_Level").focus(); })
                    });
                    return false;
                }
            }
            if ($("#rbtIsLast0").prop("checked")) {
                if ($.trim($("#txtTPName").val()) == "") {
                    layer.ready(function () {
                        layer.msg('请填写名称', { icon: 4, time: 2000 }, function () { $("#txtTPName").focus(); })
                    });
                    return false;
                }
            }
            if ($("#rbtIsLast1").prop("checked")) {
                var _val = $("#ddlTPName").val();
                var _obj = $("#ddlTPName option[value='" + _val + "']");
                if (_val == null) {
                    layer.ready(function () {
                        layer.msg('请填写/选择名称', { icon: 4, time: 2000 }, function () { $("#ddlTPName").focus(); })
                    });
                    return false;
                }
                $("#hidTPNameBasic_Id").val(_val);
                $("#hidTPNameBasic").val($(_obj).text());
                if ($(_obj).attr("data-select2-tag")) {
                    $("#hidTPNameBasic_Id").val("");
                }
            }

            if ($.trim($("#txtTPCode").val()) == "") {
                layer.ready(function () {
                    layer.msg('请填写编码', { icon: 4, time: 2000 }, function () { $("#txtTPCode").focus(); })
                });
                return false;
            }

            //$("#hidImportance").val($("input[name='Importance']:checked").val());
            return true;

        })

    })
    function formatRepoProvince(repo) {
        if (repo.loading) return repo.text;
        var markup = "<div>" + repo.text + "</div>";
        return markup;
    }
</script>
