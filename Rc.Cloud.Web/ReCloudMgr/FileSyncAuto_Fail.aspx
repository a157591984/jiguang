<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="FileSyncAuto_Fail.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FileSyncAuto_Fail1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server" ClientIDMode="Static">
                        <asp:ListItem Text="--全部--" Value=""></asp:ListItem>
                        <asp:ListItem Text="进行中" Value="0"></asp:ListItem>
                        <asp:ListItem Text="成功" Value="1"></asp:ListItem>
                        <asp:ListItem Text="失败" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <table class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>开始时间</th>
                            <th>结束时间</th>
                            <th>执行时长</th>
                            <th>状态</th>
                            <th>备注</th>
                            <th>执行人</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody id="tbRes">
                    </tbody>
                </table>
                <textarea id="template_Res" class="hidden">
                {#foreach $T.list as record}
                    <tr>
                        <td>{$T.record.FileSyncExecRecord_TimeStart}</td>
                        <td>{$T.record.FileSyncExecRecord_TimeEnd}</td>
                        <td>{$T.record.FileSyncExecRecord_Long}</td>
                        <td>{$T.record.FileSyncExecRecord_Status}</td>
                        <td>{$T.record.FileSyncExecRecord_Remark}</td>
                        <td>{$T.record.SysUser_Name}</td>
                        <td class="opera">
                            {#if $T.record.FileSyncExecRecord_Status=='失败'}
                             <a href="FileSyncAuto_Fail_Book.aspx?FileSyncExecRecord_id={$T.record.FileSyncExecRecord_id}" data-name="Preview">失败明细</a>
                            {#/if}
                        </td>
                    </tr>
                {#/for}
                </textarea>
                <hr />
                <div class="page"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        var loadData = function () {
            var dto = {
                strFileSyncExecRecord_Status: $.trim($("#ddlStatus").val()),
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                x: Math.random()
            };

            $.ajaxWebService("FileSyncAuto_Fail.aspx/GetDataList", JSON.stringify(dto), function (data) {
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
                    $("#tbRes").html("<tr class='tr_con_001'><td colspan='100' align='center'>暂无数据</td></tr>");
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
            $("#ddlStatus").change(function () {
                pageIndex = 1;
                loadData();
            });
        });

    </script>
</asp:Content>
