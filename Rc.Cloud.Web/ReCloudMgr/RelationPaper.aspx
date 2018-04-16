<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelationPaper.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.RelationPaper" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/mtree-2.0/mtree.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/mtree-2.0/mtree.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script src="../SysLib/js/json2.js"></script>
    <script type="text/javascript">
        $(function () {
            query = 1;
            TestPaper_Frame_Id = "<%=TestPaper_Frame_Id%>";
            $("#btnShow").show();
            $("#btnALL").hide();
            loadData("");

            $("#ddlYear").change(function () {
                if (query == 1) {
                    loadData("");
                }
            })
            $("#ddlGradeTerm").change(function () {
                if (query == 1) {
                    loadData("");
                }
            })
            $("#ddlResource_Version").change(function () {
                if (query == 1) {
                    if ($(this).val() == "-1") {
                        layer.msg("请选择教材版本", { time: 1000, icon: 4 });
                        return false;
                    }
                    loadData("");
                }
            })
            $("#ddlSubject").change(function () {
                if (query == 1) {
                    if ($(this).val() == "-1") {
                        layer.msg("请选择学科", { time: 1000, icon: 4 });
                        return false;
                    }
                    loadData("");
                }
            })

            $("#btnShow").click(function () {
                $("#btnShow").hide();
                $("#btnALL").show();
                loadData("1");
                query = 0;
            })
            $("#btnALL").click(function () {
                $("#btnALL").hide();
                $("#btnShow").show();
                loadData("");
                query = 1;
            })
            $("#btnSearch").click(function () {
                if (query == 1) {
                    loadData("");
                }
            })
            $("#btnRelation").click(function () {
                var arrCked = new Array();
                $("#ReTree input[type='checkbox']").each(function () {
                    if (this.checked) {
                        arrCked.push($(this).val())
                    }
                })
                $("#hidChecked").val(arrCked);
                if ($("#hidChecked").val() == "" || $("#hidChecked").val() == undefined) {
                    layer.msg("暂无选中的试卷", { icon: 2, time: 2000 });
                    return false;
                }
                else {
                    var index = layer.confirm('确定要关联选中的试卷么吗？', { icon: 4, title: '提示' }, function () {
                        layer.close(index);//关闭确认弹窗
                        $.ajaxWebService("RelationPaper.aspx/GetRelationPaper", "{TestPaper_Frame_Id:'" + TestPaper_Frame_Id + "',TestPaper:'" + $("#hidChecked").val() + "',x:'" + Math.random() + "'}", function (data) {
                            if (data.d == "1") {
                                layer.msg('关联成功', { icon: 1, time: 1000 }, function () {
                                    if (query == 1) {
                                        loadData("");
                                    }
                                    else { loadData("1"); }
                                });
                            }
                            else {
                                layer.alert(data.d, { icon: 2 });
                                return false;
                            }

                        }, function () {
                            layer.msg('操作失败！', { icon: 2 });
                            return false;
                        });
                    });
                }
            })
            $("#btnCancel").click(function () {
                var arrCked = new Array();
                $("#ReTree input[type='checkbox']").each(function () {
                    if (this.checked) {
                        arrCked.push($(this).val())
                    }
                })
                $("#hidChecked").val(arrCked);
                if ($("#hidChecked").val() == "" || $("#hidChecked").val() == undefined) {
                    layer.msg("暂无选中的试卷", { icon: 2, time: 2000 });
                    return false;
                }
                else {
                    var index = layer.confirm('确定要取消关联选中的试卷么吗？', { icon: 4, title: '提示' }, function () {
                        layer.close(index);//关闭确认弹窗
                        $.ajaxWebService("RelationPaper.aspx/CancelRelationPaper", "{TestPaper_Frame_Id:'" + TestPaper_Frame_Id + "',TestPaper:'" + $("#hidChecked").val() + "',x:'" + Math.random() + "'}", function (data) {
                            if (data.d == "1") {
                                layer.msg('取消关联成功', { icon: 1, time: 1000 }, function () {
                                    if (query == 1) {
                                        loadData("");
                                    }
                                    else { loadData("1"); }
                                });
                            }
                            else if (data.d == "2") {
                                layer.msg('此试卷结构中已存在双向细目表，取消关联失败。', { icon: 2, time: 4000 });
                                return false;
                            }
                            else {
                                layer.msg('取消关联失败', { icon: 2 });
                                return false;
                            }

                        }, function () {
                            layer.msg('操作失败！', { icon: 2 });
                            return false;
                        }, false);
                    });
                }
            })
        })


        var loadData = function (type) {
            if (query == 1) {
                if ($("#ddlSubject").val() == "-1") {
                    layer.msg("请选择学科", { time: 1000, icon: 4 });
                    return false;
                }
                if ($("#ddlResource_Version").val() == "-1") {
                    layer.msg("请选择教材版本", { time: 1000, icon: 4 });
                    return false;
                }
            }
            var Year = $("#ddlYear").val();
            var GradeTerm = $("#ddlGradeTerm").val();
            var Subject = $("#ddlSubject").val();
            var Resource_Version = $("#ddlResource_Version").val();
            var ReName = $("#txtReName").val();
            var dto = {
                TestPaper_Frame_Id: "<%=TestPaper_Frame_Id%>",
                Year: Year,
                Resource_Version: Resource_Version,
                GradeTerm: GradeTerm,
                Subject: Subject,
                Name: ReName,
                type: type,
                x: Math.random()
            };
            $.ajaxWebService("RelationPaper.aspx/GetTree", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $("#ReTree").html(data.d);
                }
                else { $("#ReTree").html("暂无数据"); }
                $("#ReTree").mtree({
                    html: true,
                    url: true,
                    onClick: function (obj, url) {
                        if (url != "" && url != undefined) {
                            window.open(url);
                        }
                    }
                });
            }, function () { });
        }
    </script>
    <style>
        .tree ul {
            padding-left: 0;
        }
    </style>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <h4>
                <asp:Literal ID="ltlName" runat="server"></asp:Literal></h4>
            <div class="form-inline search_bar mb">
                <asp:DropDownList ID="ddlYear" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlGradeTerm" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlSubject" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlResource_Version" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
                <asp:HiddenField ID="hidChecked" runat="server" ClientIDMode="Static" />
                <input type="text" id="txtReName" class="form-control input-sm" placeholder="试卷名称">
                <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询">
                <input type="button" class="btn btn-primary btn-sm" id="btnRelation" value="关联试卷">
                <input type="button" class="btn btn-danger btn-sm" id="btnCancel" value="取消关联">
                <input type="button" class="btn btn-success btn-sm" id="btnShow" value="只显示已关联">
                <input type="button" class="btn btn-info btn-sm" id="btnALL" value="显示全部">
            </div>
            <div class="mtree" id="ReTree"></div>
        </div>
    </form>
</body>
</html>
