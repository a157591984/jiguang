<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CloudResList.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.CloudResList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>选择云资源</title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/mtree-2.0/mtree.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/bootstrap-3.3.7/js/bootstrap.min.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/json2.js"></script>
    <script src="../SysLib/js/jq.pagination.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script type="text/javascript">
        $(function () {
            arrCheckUser = new Array();

            //判断是否为多个人授权
            $("#ddlStatus").hide();
            var array = parent.$("#hidUserlist").val().split(",");
            if (array.length > 1) {
                $("#ddlStatus").hide();
            }
            else {
                $("#ddlStatus").show();
            }
            pageIndex = 1;
            loadDataRe();
            $("#ddlGradeTerm").change(function () {
                pageIndex = 1;
                loadDataRe();
            })
            $("#ddlResource_Type").change(function () {
                pageIndex = 1;
                loadDataRe();
            })
            $("#ddlSubjects").change(function () {
                pageIndex = 1;
                loadDataRe();
            })
            $("#ddlResource_Version").change(function () {
                pageIndex = 1;
                loadDataRe();
            })
            $("#btnSearchs").click(function () {
                pageIndex = 1;
                loadDataRe();
            })
            $("#ddlStatus").change(function () {
                pageIndex = 1;
                loadDataRe();
            })
            // 全选
            $('[data-name="check-all"]').on({
                click: function () {
                    if ($(this).prop("checked")) {
                        $('input[type="checkbox"][data-name="check-single"]:enabled').each(function () {
                            setChecked($(this), true);
                        });
                    } else {
                        $('input[type="checkbox"][data-name="check-single"]:enabled').each(function () {
                            setChecked($(this), false);
                        });
                    }
                }
            });
            // 单个选择
            $(document).on('click', '[data-name="check-single"]', function () {
                setChecked($(this));
            });

            // 单个删除
            $(document).on('click', '[data-name="del-single"]', function () {
                $(this).closest("tr").remove();
                $('[data-name="check-single"][value="' + $(this).data("rtrfid") + '"]').prop("checked", false);
                arrCheckUser.splice($.inArray($(this).data("rtrfid"), arrCheckUser), 1);
                $("#spanCheckCount").html(arrCheckUser.length);
                resetCheckAll();
            });

            // 清空已选
            $('[data-name="del-all"]').on({
                click: function () {
                    $('[data-name="del-single"]').click();
                }
            });
        });
        // 清除/显示已选择
        var setChecked = function (obj, flag) {
            var _rtrfId = $(obj).val();
            if (flag == undefined) {
                if ($(obj).prop("checked")) {
                    var _html = "<tr data-rtrfid=\"" + _rtrfId
                        + "\"><td>" + $(obj).data("resname")
                        + "</td><td class=\"opera\"><a href=\"##\" data-rtrfid=\"" + _rtrfId + "\" data-name=\"del-single\">删除</a></td></tr>";
                    $("#tbSelect").append(_html);
                    arrCheckUser.push(_rtrfId);
                }
                else {
                    $("#tbSelect tr[data-rtrfid='" + _rtrfId + "']").remove();
                    arrCheckUser.splice($.inArray(_rtrfId, arrCheckUser), 1);
                }
            }
            else {
                if (flag && !$(obj).prop("checked")) {
                    $(obj).prop("checked", true);
                    var _html = "<tr data-rtrfid=\"" + _rtrfId
                        + "\"><td>" + $(obj).data("resname")
                        + "</td><td class=\"opera\"><a href=\"##\" data-rtrfid=\"" + _rtrfId + "\" data-name=\"del-single\">删除</a></td></tr>";
                    $("#tbSelect").append(_html);
                    arrCheckUser.push(_rtrfId);
                }
                else if (!flag) {
                    $(obj).prop("checked", false);
                    $("#tbSelect tr[data-rtrfid='" + _rtrfId + "']").remove();
                    arrCheckUser.splice($.inArray(_rtrfId, arrCheckUser), 1);
                }
            }
            $("#spanCheckCount").html(arrCheckUser.length);
            resetCheckAll();
        }
        // 加载数据是，已选图书重置为选择状态
        var resetCheck = function () {
            if (arrCheckUser.length > 0) {
                $('input[type="checkbox"][data-name="check-single"]:enabled').each(function () {
                    if ($.inArray($(this).val(), arrCheckUser) > -1) {
                        $(this).prop("checked", true);
                    }
                });
                resetCheckAll();
            }
        }
        var resetCheckAll = function () {
            //当前页 全被选，则全选按钮选中
            if ($('input[type="checkbox"][data-name="check-single"]:checked').length == $('input[type="checkbox"][data-name="check-single"]').length) {
                $('[data-name="check-all"]').prop("checked", true);
            }
            else {
                $('[data-name="check-all"]').prop("checked", false);
            }
        }

        var loadDataRe = function () {
            //全选按钮重置为未选状态
            $('[data-name="check-all"]').prop("checked", false);

            var strResource_Type = $("#ddlResource_Type").val();
            var strGradeTerm = $("#ddlGradeTerm").val();
            var strSubject = $("#ddlSubjects").val();
            var strResource_Version = $("#ddlResource_Version").val();
            var userList = parent.$("#hidUserlist").val();
            var Status = $("#ddlStatus").val();
            var dto = {
                GradeTerm: strGradeTerm,
                Subjects: strSubject,
                Resource_Version: strResource_Version,
                Resource_Type: strResource_Type,
                ReName: $("#txtRe").val(),
                userid: userList,
                Status: Status,
                PageSize: ($("#pageS [data-name='pagination_select']").val() == undefined ? 10 : $("#pageS [data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                x: Math.random()
            };
            $.ajaxWebService("CloudResList.aspx/GetReList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbRes").setTemplateElement("template_Res", null, { filter_data: false });
                    $("#tbRes").processTemplate(json);
                    $("#pageS").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize,
                        callback_name: "pageselectCallbackT"
                    });

                    resetCheck();

                    if (json.list == null || json.list == "") {
                        pageIndex--;
                        if (pageIndex > 0) {
                            loadData();
                        }
                        else {
                            pageIndex = 1;
                        }
                    }
                }
                else {
                    $("#tbRes").html("<tr class='tr_con_002'><td colspan='7' align='center'>暂无数据</td></tr>");
                    $("#pageS").html("");
                }
            }, function () { });
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadDataRe();
        }

        function SelectAllChks() {
            var result = false;
            if (arrCheckUser.length > 0) {
                $("#hidrelist").val(arrCheckUser.join(","));
                result = true;
                Empower($("#hidrelist").val());
            }
            else {
                layer.msg("请选择资源", { icon: 2, time: 1000 })
            }
            return result;
        }

        function DeleteAllChks() {
            var result = false;
            if (arrCheckUser.length > 0) {
                $("#hidrelist").val(arrCheckUser.join(","));
                result = true;
                DeleteEmpower($("#hidrelist").val());
            }
            else {
                layer.msg("请选择资源", { icon: 2, time: 1000 })
            }
            return result;
        }

        var Empower = function (relist) {
            var userList = parent.$("#hidUserlist").val();
            var dto = {
                userlist: userList,
                relist: relist,
                x: Math.random()
            };
            $.ajaxWebService("CloudResList.aspx/Empowers", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    layer.msg('授权成功', { icon: 1 }, function () {
                        loadDataRe();
                        //清除已选                        
                        arrCheckUser = new Array();
                        $("#spanCheckCount").html("0");
                        $("#tbSelect").html("");
                    });
                }
                else if (data.d == "2") {
                    layer.msg('授权失败', { icon: 2 });
                    return false;
                }

            }, function () {
                layer.msg('授权失败1！', { icon: 2 });
                return false;
            });
        }
        var DeleteEmpower = function (relist) {
            var userList = parent.$("#hidUserlist").val();
            var dto = {
                userlist: userList,
                relist: relist,
                x: Math.random()
            };
            $.ajaxWebService("CloudResList.aspx/DeleteEmpowers", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    layer.msg('批量删除授权成功', { icon: 1 }, function () {
                        loadDataRe();
                        //清除已选
                        arrCheckUser = new Array();
                        $("#spanCheckCount").html("0");
                        $("#tbSelect").html("");
                    });
                }
                else if (data.d == "2") {
                    layer.msg('批量删除授权失败', { icon: 2 });
                    return false;
                }

            }, function () {
                layer.msg('批量删除授权失败', { icon: 2 });
                return false;
            });
        }
    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="container-fluid pv">
            <div class="row">
                <div class="col-xs-8">
                    <!-- 资源列表 -->
                    <div class="form-inline search_bar mb">
                        <asp:DropDownList ID="ddlGradeTerm" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                        </asp:DropDownList>

                        <asp:DropDownList ID="ddlSubjects" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                        </asp:DropDownList>

                        <asp:DropDownList ID="ddlResource_Version" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                        </asp:DropDownList>

                        <asp:DropDownList ID="ddlResource_Type" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlStatus" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                            <asp:ListItem Text="授权状态" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="已授权" Value="1"></asp:ListItem>
                            <asp:ListItem Text="未授权" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <input type="text" id="txtRe" class="form-control input-sm" placeholder="资源名称" clientidmode="Static">
                        <input type="button" class="btn btn-default btn-sm" id="btnSearchs" value="查询" clientidmode="Static">

                        <input type="button" value="批量授权" onclick="SelectAllChks();" class="btn btn-default btn-sm">

                        <input type="button" value="删除批量授权" onclick="DeleteAllChks();" class="btn btn-default btn-sm">
                    </div>
                    <table class="table table-hover table-bordered">
                        <thead>
                            <tr>
                                <th width="8%">
                                    <label>
                                        <input type="checkbox" data-name="check-all" name="checkAlls">全选</label>
                                </th>
                                <th width="10%">年级学期</th>
                                <th width="8%">学科</th>
                                <th width="10%">教材版本</th>
                                <th width="10%">文档类型</th>
                                <th>名称</th>
                            </tr>
                        </thead>
                        <tbody id="tbRes">
                        </tbody>
                    </table>
                    <div class="page" id="pageS">
                        <ul>
                        </ul>
                    </div>
                    <asp:HiddenField ID="hidrelist" runat="server" ClientIDMode="Static" />
                    <textarea id="template_Res" class="hidden">
                    {#foreach $T.list as record}
                    <tr>
                        <td>
                            <label><input type='checkbox' name='checkAlls' value='{$T.record.docId}' data-resname="{$T.record.docName}" data-name="check-single" /></label>{$T.record.ispower}
                        </td>
                        <td>{$T.record.GradeTerm}</td>
                        <td>{$T.record.Subject}</td>
                        <td>{$T.record.Resource_Version}</td>
                        <td>{$T.record.Resource_Type}</td>
                        <td>{$T.record.docName}</td>
                    </tr>
                    {#/for}
                    </textarea>
                </div>
                <div class="col-xs-4">
                    <div class="form-inline search_bar mb text-right">
                        <h4 class="pull-left">已选（<span id="spanCheckCount">0</span>）</h4>
                        <input type="button" data-name="del-all" class="btn btn-default btn-sm" value="清空已选" />
                    </div>
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>名称</th>
                                <th width="12%">操作</th>
                            </tr>
                        </thead>
                        <tbody id="tbSelect">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
