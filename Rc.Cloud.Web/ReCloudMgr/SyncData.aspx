<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyncData.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.SyncData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>数据同步</title>
    <link href="../styles/styles003/layOut.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/styles003/sdmenu.css" />
    <link href="../Styles/styles003/style01.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style001/pagination.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/json2.js"></script>

    <script src="../Scripts/js001/jquery.min-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../scripts/function.js"></script>
    <link href="../Styles/Dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/sdmenu.js"></script>
    <script src="../Scripts/plug-in/layer/layer.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-jtemplates.js"></script>
    <script type="text/javascript" src="../Scripts/PhhcCommon.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_right_search">
            <asp:Button ID="btnData" runat="server" CssClass="btn" Text="执行数据同步" OnClick="btnData_Click" />
            <asp:Button runat="server" ID="btnDataReExec" Text="重新执行数据同步" CssClass="btn" OnClick="btnDataReExec_Click" Style="display: none;" />
            <asp:HiddenField runat="server" ID="hidId" />
        </div>
        <div class="clear"></div>
        <div style="width: 100%" class="div_c_title">
            执行记录
        </div>
        <div style="width: 100%">
            <div class="div_right_search">
                <table class="table_search_001" style="display: none;">
                    <tbody>
                        <tr>
                            <td>状态：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" CssClass="user_ddl" runat="server">
                                    <asp:ListItem Text="--全部--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="进行中" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="成功" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="失败" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <input type="button" class="btn" id="btnSearch" value="查询" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="" id="userDocumentContent">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
                    <thead>
                        <tr class="tr_title">
                            <%--<td >执行条件</td>--%>
                            <td style="width: 15%;">开始时间</td>
                            <td style="width: 15%;">结束时间</td>
                            <td style="width: 10%;">执行时长</td>
                            <td style="width: 6%;">状态</td>
                            <td style="width: 20%;">备注</td>
                            <td style="width: 6%;">执行人</td>
                            <td style="width: 8%;">操作</td>
                        </tr>
                    </thead>
                    <tbody id="tbRes">
                    </tbody>
                </table>
                <hr />
                <div class="page"></div>
            </div>
            <textarea id="template_Res" style="display: none">
        {#foreach $T.list as record}
        <tr class="tr_con_001">
        <%--<td>{$T.record.FileSyncExecRecord_Condition}</td>--%>
        <td>{$T.record.FileSyncExecRecord_TimeStart}</td> 
        <td>{$T.record.FileSyncExecRecord_TimeEnd}</td>
        <td>{$T.record.FileSyncExecRecord_Long}</td>
        <td>{$T.record.FileSyncExecRecord_Status}</td>
        <td>{$T.record.FileSyncExecRecord_Remark}</td>
        <td>{$T.record.SysUser_Name}</td>
        <td class="align_center table_opera">
            <a href="FileSyncDetail.aspx?FileSyncExecRecord_id={$T.record.FileSyncExecRecord_id}" target="_blank" data-name="Preview">执行明细</a>
            {#if $T.record.FileSyncExecRecord_Status=='失败'}
            <a href="javascript:;" data-name="ReExec" data-value="{$T.record.FileSyncExecRecord_id}">重新执行</a>
            {#/if}
        </td>
        </tr>
        {#/for}
        </textarea>
        </div>
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
