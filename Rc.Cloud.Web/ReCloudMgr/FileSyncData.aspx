<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileSyncData.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FileSyncData" %>

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
        <div class="form-inline search_bar mb">
            <asp:Button ID="btnData" runat="server" CssClass="btn btn-primary btn-sm" Text="执行数据同步" OnClick="btnData_Click" />
            <asp:Button runat="server" ID="btnDataReExec" Text="重新执行数据同步" CssClass="btn btn-primary btn-sm" OnClick="btnDataReExec_Click" Style="display: none;" />
            <asp:HiddenField runat="server" ID="hidId" />
            <div class="hidden">
                <asp:DropDownList ID="ddlStatus" CssClass="user_ddl" runat="server">
                    <asp:ListItem Text="--全部--" Value=""></asp:ListItem>
                    <asp:ListItem Text="进行中" Value="0"></asp:ListItem>
                    <asp:ListItem Text="成功" Value="1"></asp:ListItem>
                    <asp:ListItem Text="失败" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <input type="button" class="btn" id="btnSearch" value="查询" />
            </div>
        </div>
        <table class="table table-hover table-bordered">
            <thead>
                <tr>
                    <%--<td >执行条件</td>--%>
                    <td>开始时间</td>
                    <td>结束时间</td>
                    <td>执行时长</td>
                    <td>状态</td>
                    <td>备注</td>
                    <td>执行人</td>
                    <td>操作</td>
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
        <%--<td>{$T.record.FileSyncExecRecord_Condition}</td>--%>
        <td>{$T.record.FileSyncExecRecord_TimeStart}</td> 
        <td>{$T.record.FileSyncExecRecord_TimeEnd}</td>
        <td>{$T.record.FileSyncExecRecord_Long}</td>
        <td>{$T.record.FileSyncExecRecord_Status}</td>
        <td>{$T.record.FileSyncExecRecord_Remark}</td>
        <td>{$T.record.SysUser_Name}</td>
        <td class="opera">
            <a href="FileSyncDetail.aspx?FileSyncExecRecord_id={$T.record.FileSyncExecRecord_id}" target="_blank" data-name="Preview">执行明细</a>
            {#if $T.record.FileSyncExecRecord_Status=='失败' || ($T.record.ExecHours>3 && $T.record.FileSyncExecRecord_Status=='进行中')}
            <a href="javascript:;" data-name="ReExec" data-value="{$T.record.FileSyncExecRecord_id}">重新执行</a>
            {#/if}
        </td>
        </tr>
        {#/for}
        </textarea>
        <script type="text/javascript">
            var loadData = function () {
                var dto = {
                    strFileSyncExecRecord_Status: $.trim($("#ddlStatus").val()),
                    PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                    PageIndex: pageIndex,
                    x: Math.random()
                };

                $.ajaxWebService("FileSyncData.aspx/GetDataList", JSON.stringify(dto), function (data) {
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

                $(document).on('click', '[data-name="ReExec"]', function () {
                    $("#hidId").val($(this).data('value'));
                    $("#btnDataReExec").click();
                });

            });

        </script>
    </form>
</body>
</html>
