<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileSyncView.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FileSyncView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
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
        <div class="container-fluid pv">
            <div class="form-inline search_bar mb">
                <input type="text" id="txtReName" class="form-control input-sm" />
                <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
                <input type="button" class="btn btn-default btn-sm" id="btnBack" value="返回" />
            </div>
            <table class="table table-hover table-bordered">
                <thead>
                    <tr>
                        <th>名称</th>
                        <th style="width: 10%;">大小/类型</th>
                        <th style="width: 10%;">日期</th>
                        <th style="width: 10%;">操作</th>
                    </tr>
                </thead>
                <tbody id="tbRes">
                </tbody>
            </table>
            <div class="page"></div>
        </div>

        <textarea id="template_Res" class="hidden">
        {#foreach $T.list as record}
            <tr>
                <td>{$T.record.docName}</td>
                <td>{#if $T.record.RType=='1'}{$T.record.docSize}{#else}文件夹{#/if}</td>
                <td>{$T.record.docTime}</td>
                <td class="opera">
                    {#if $T.record.RType=='0'}
                        <a  href="FileSyncView.aspx?resourceFolderId={$T.record.docId}&resTitle=<%=resTitle %>--{$T.record.docName}&FileSyncExecRecord_Type=<%=FileSyncExecRecord_Type %>" >资源列表</a>
                    {#else}
                        {#if $T.record.IType=='1'}
                        <a href="FileViewNew.aspx?ResourceToResourceFolder_Id={$T.record.docId}" target="_blank" data-name="Preview">预览</a>
                        {#else}
                        <a href="TestPaperView.aspx?ResourceToResourceFolder_Id={$T.record.docId}" target="_blank" data-name="Preview">预览</a>
                        {#/if}
                    {#/if}
                    {$T.record.Operate}
                </td>
            </tr>
        {#/for}
    </textarea>
        <script type="text/javascript">
            var loadData = function () {
                var dto = {
                    ResourceFolder_Id: "<%=resourceFolderId%>",
                    DocName: $.trim($("#txtReName").val()),
                    PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                    PageIndex: pageIndex,
                    x: Math.random()
                };
                $.ajaxWebService("FileSyncView.aspx/GetResourceList", JSON.stringify(dto), function (data) {
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
                $("#btnBack").click(function () {
                    var b = new Base64();
                    var backurl = getUrlVar("backurl");

                    if (backurl != null && backurl != undefined && backurl != "") {
                        window.location.href = b.decode(backurl);
                    }
                    else {
                        window.location.href = "FileSyncViewList.aspx?FileSyncExecRecord_Type=<%=FileSyncExecRecord_Type%>";
            }
        })
    });

        </script>
    </form>
</body>
</html>
