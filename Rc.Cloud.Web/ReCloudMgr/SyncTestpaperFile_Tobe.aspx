﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyncTestpaperFile_Tobe.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.SyncTestpaperFile_Tobe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>待同步习题集数据-运营</title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/auto-complete/css/style.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/bootstrap-3.3.7/js/bootstrap.min.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script src="../SysLib/js/jq.pagination.js"></script>
    <script src="../SysLib/js/json2.js"></script>
    <script src="../SysLib/js/base64.js"></script>
    <script src="../SysLib/js/index.js"></script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="form-inline search_bar mb">
            <input runat="server" type="button" id="btnConfirm" value="执行作业同步" class="btn btn-primary btn-sm" />
            <asp:Button ID="btnTestpaper" runat="server" CssClass="btn btn-primary btn-sm hide" Text="执行作业同步" OnClick="btnTestpaper_Click" />
            <input type="button" class="btn btn-primary btn-sm" value="同步预览" onclick="javascript: window.open('FileSyncViewList.aspx?FileSyncExecRecord_Type=同步试卷new');" />
            <input type="button" value="执行记录" class="btn btn-primary btn-sm" onclick="javascript: window.open('SyncTobe_LogList.aspx?FileSyncExecRecord_Type=同步试卷new');" />
            <input runat="server" type="button" class="btn btn-default btn-sm" id="btnBack" value="返回" onclick="history.back();" />
            <asp:HiddenField runat="server" ID="hid_bookId" />
            <asp:HiddenField runat="server" ID="hid_rtrfId" />
            此处显示的全部为审核通过且进行过数据同步的书
            <div class="hidden">
                <asp:DropDownList ID="ddlStatus" CssClass="form-control input-sm" runat="server">
                    <asp:ListItem Text="--全部--" Value=""></asp:ListItem>
                    <asp:ListItem Text="进行中" Value="0"></asp:ListItem>
                    <asp:ListItem Text="成功" Value="1"></asp:ListItem>
                    <asp:ListItem Text="失败" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
            </div>
        </div>
        <table class="table table-hover table-bordered">
            <thead>
                <tr>
                    <th>书名</th>
                    <th>资源名称</th>
                    <th>时间</th>
                    <th width="12%">操作</th>
                </tr>
            </thead>
            <tbody id="tbRes">
            </tbody>
        </table>
        <hr />
        <div class="page"></div>
        <textarea id="template_Res" class="hidden">
            {#foreach $T.list as record}
                <tr>
                    <td>{$T.record.bookName}</td>
                    <td>{$T.record.rtrfName}</td>
                    <td>{$T.record.CreateTime}</td>
                    <td class="opera">
                        {#if $T.record.IsShowDetail=='1'}
                        <a href="SyncTestpaperFile_Tobe.aspx?bookId={$T.record.bookId}&SysUser_ID=<%=SysUser_ID %>">资源明细</a>
                        {#/if}
                        {#if $T.record.IsCanExecing=='1'}
                        <a href="javascript:;" data-name="sync_testpaper" data-bookid="{$T.record.bookId}" data-rtrfid="{$T.record.rtrfId}">执行同步</a>
                        {#/if}
                        <%--{#if $T.record.FileSyncExecRecord_Status=='失败' || ($T.record.ExecHours>3 && $T.record.FileSyncExecRecord_Status=='进行中')}
                        <a href="javascript:;" data-name="ReExec" data-value="{$T.record.FileSyncExecRecord_id}">重新执行</a>
                        {#/if}--%>
                    </td>
                </tr>
            {#/for}
        </textarea>

        <script type="text/javascript">
            var loadData = function () {
                var dto = {
                    isCanExecing: "<%=isCanExecing%>",
                    bookId: "<%=bookId%>",
                    PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                    PageIndex: pageIndex,
                    x: Math.random()
                };

                $.ajaxWebService("SyncTestPaperFile_Tobe.aspx/GetDataList", JSON.stringify(dto), function (data) {
                    var json = $.parseJSON(data.d);

                    if (json.err == "null") {
                        $("#tbRes").setTemplateElement("template_Res", null, { filter_data: false });
                        $("#tbRes").processTemplate(json);
                        $(".page").pagination(json.TotalCount, {
                            current_page: json.PageIndex - 1,
                            callback: pageselectCallback,
                            items_per_page: json.PageSize
                        });
                    }
                    else {
                        $("#btnConfirm").attr({
                            disabled: "disabled",
                            title: "暂无可执行数据"
                        });
                        $("#tbRes").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                        $(".page").html("");
                    }
                    if (json.list == null || json.list == "") {
                        pageIndex--;
                        if (pageIndex > 0) {
                            loadData();
                        }
                        else {
                            pageIndex = 1;
                        }
                    }
                    //$(window.parent.document).find("#iframDrugFiled").css({ height: $(document).height() });
                }, function () { });
            }
            var pageselectCallback = function (page_index, jq) {
                pageIndex = page_index + 1;
                loadData();
            }
            $(function () {
                pageIndex = 1;

                loadData();

                $("#btnSearch").click(function () {
                    pageIndex = 1;
                    loadData();
                });

                $(document).on('click', '[data-name="sync_testpaper"]', function () {
                    $('#hid_bookId').val($(this).data('bookid'));
                    $('#hid_rtrfId').val($(this).data('rtrfid'));
                    $('#btnConfirm').click();
                });

                $("#btnConfirm").click(function () {
                    layer.confirm("您确定要执行同步作业吗？", { icon: 2, title: "提示" }, function () {
                        $("#btnTestpaper").click();
                    });
                });

            });

        </script>
    </form>
</body>
</html>
