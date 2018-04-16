<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyncFileSchool_TobeNew.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.SyncFileSchool_TobeNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>待同步文件-学校</title>
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
            <label>
                <input type="checkbox" id="cbIsNeed" />只显示需要同步资源</label>
            <input type="button" class="btn btn-primary btn-sm" id="btnSearch" value="查询" />
            <input runat="server" type="button" id="btnConfirm" value="执行所有待同步图书（文件）" class="btn btn-primary btn-sm" />
            <asp:Button ID="btnExec" runat="server" CssClass="btn btn-primary btn-sm hide" OnClick="btnExec_Click" />
            <input type="button" class="btn btn-primary btn-sm" value="同步预览" onclick="javascript: window.open('FileSyncViewList.aspx?FileSyncExecRecord_Type=同步学校资源<%=SchoolId%>    ');" />
            <input type="button" value="执行记录" class="btn btn-primary btn-sm" onclick="javascript: window.open('SyncTobe_LogList.aspx?FileSyncExecRecord_Type=同步学校资源<%=SchoolId%>    ');" />
            <input runat="server" type="button" class="btn btn-default btn-sm" id="btnBack" value="返回" onclick="history.back();" />
            <asp:HiddenField runat="server" ID="hid_bookId" />
            <asp:HiddenField runat="server" ID="hid_rtrfId" />
            <asp:HiddenField runat="server" ID="hid_rType" />
        </div>
        <table class="table table-hover table-bordered">
            <thead>
                <tr>
                    <th width="10%">类型</th>
                    <th>书目</th>
                    <th data-name="resname" class="hide">资源名称</th>
                    <th width="10%">同步状态</th>
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
                    <td>{$T.record.Resource_TypeName}</td>
                    <td>{$T.record.BookName}<br />{$T.record.ParentName}</td>
                    <td data-name="resname" class="hide">{$T.record.rtrfName}</td>
                    <td>{$T.record.SyncStatusName}</td>
                    <td>{$T.record.CreateTime}</td>
                    <td class="opera">
                        {#if $T.record.IsBook==true}
                        <a href="SyncFileSchool_TobeNew.aspx?SchoolId=<%=SchoolId %>&SysUser_ID=<%=SysUser_ID %>&BookId={$T.record.BookId}&BookName={$T.record.BookName}&Resource_TypeName={$T.record.Resource_TypeName}">资源明细</a>
                        {#/if}
                        {#if $T.record.IsCanExecing==true&&$T.record.SyncStatus=='0'}
                        <a href="javascript:;" data-name="sync_school" data-bookid="{$T.record.BookId}" data-bookname="{$T.record.BookName}" data-rtrfid="{$T.record.rtrfId}" data-rtrfname="{$T.record.rtrfName}" data-rtype="{$T.record.Resource_Type}">执行同步</a>
                        {#/if}
                    </td>
                </tr>
            {#/for}
        </textarea>

        <script type="text/javascript">
            var loadData = function () {
                var dto = {
                    SchoolId: "<%=SchoolId%>",
                    BookId: "<%=BookId%>",
                    BookName:"<%=BookName%>",
                    Resource_TypeName:"<%=Resource_TypeName%>",
                    IsNeed: $("#cbIsNeed").prop("checked") ? "1" : "0",
                    PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                    PageIndex: pageIndex,
                    x: Math.random()
                };

                $.ajaxWebService("SyncFileSchool_TobeNew.aspx/GetDataList", JSON.stringify(dto), function (data) {
                    var json = $.parseJSON(data.d);

                    if (json.err == "null") {
                        $("#tbRes").setTemplateElement("template_Res", null, { filter_data: false });
                        $("#tbRes").processTemplate(json);
                        $(".page").pagination(json.TotalCount, {
                            current_page: json.PageIndex - 1,
                            callback: pageselectCallback,
                            items_per_page: json.PageSize
                        });
                        if (json.IsBook) {
                            $("[data-name='resname']").addClass("hide");
                        }else {
                            $("[data-name='resname']").removeClass("hide");
                        }
                    }
                    else {
                        $("#btnExec").attr({
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

                $("#btnConfirm").click(function () {
                    layer.confirm("确认要同步所有待同步的资源吗？", { icon: 2, title: "提示" }, function () {
                        $("#btnExec").click();
                    });
                });

                $(document).on('click', '[data-name="sync_school"]', function () {
                    var _this = $(this);
                    var _tips;
                    if ($(_this).data('rtrfname')=='') {
                        _tips='确认要同步【' + $(_this).data('bookname') + '】图书吗？';
                    }
                    else {
                        _tips='确认要同步【' + $(_this).data('rtrfname') + '】资源吗？';
                    }
                    layer.confirm(_tips, { icon: 2, title: "提示" }, function () {
                        $('#hid_bookId').val($(_this).data('bookid'));
                        $('#hid_rtrfId').val($(_this).data('rtrfid'));
                        $('#hid_rType').val($(_this).data('rtype'));

                        $('#btnExec').click();
                    });
                });

            });

        </script>
    </form>
</body>
</html>
