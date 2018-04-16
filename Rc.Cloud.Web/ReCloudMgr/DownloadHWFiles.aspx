<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownloadHWFiles.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.DownloadHWFiles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>下载作业相关文件</title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/auto-complete/css/style.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/bootstrap-3.3.7/js/bootstrap.min.js"></script>
    <script src="../SysLib/plugin/datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../SysLib/plugin/datetimepicker/js/locales/bootstrap-datetimepicker.zh-CN.js"></script>
    <script src="../SysLib/plugin/auto-complete/js/AutoComplete.js"></script>
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
            <asp:Button ID="btn" runat="server" CssClass="btn btn-primary btn-sm" Text="执行下载" OnClick="btn_Click" />
        </div>
        <div class="form-inline search_bar mb hidden">
            <asp:DropDownList ID="ddlStatus" CssClass="form-control input-sm" runat="server">
                <asp:ListItem Text="--全部--" Value=""></asp:ListItem>
                <asp:ListItem Text="进行中" Value="0"></asp:ListItem>
                <asp:ListItem Text="成功" Value="1"></asp:ListItem>
                <asp:ListItem Text="失败" Value="2"></asp:ListItem>
            </asp:DropDownList>
            <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
        </div>
        <table class="table table-hover table-bordered">
            <thead>
                <tr>
                    <th>执行条件</th>
                    <th style="width: 15%;">开始时间</th>
                    <th style="width: 15%;">结束时间</th>
                    <th style="width: 10%;">执行时长</th>
                    <th style="width: 6%;">状态</th>
                    <th style="width: 20%;">备注</th>
                    <th style="width: 6%;">执行人</th>
                    <th style="width: 6%;">操作</th>
                </tr>
            </thead>
            <tbody id="tbRes">
            </tbody>
        </table>
        <hr />
        <div class="page"></div>

        <textarea id="template_Res" style="display: none">
            {#foreach $T.list as record}
                <tr class="tr_con_001">
                    <td>{$T.record.FileSyncExecRecord_Condition}</td>
                    <td>{$T.record.FileSyncExecRecord_TimeStart}</td>
                    <td>{$T.record.FileSyncExecRecord_TimeEnd}</td>
                    <td>{$T.record.FileSyncExecRecord_Long}</td>
                    <td>{$T.record.FileSyncExecRecord_Status}</td>
                    <td>{$T.record.FileSyncExecRecord_Remark}</td>
                    <td>{$T.record.SysUser_Name}</td>
                    <td class="align_center table_opera">
                        <a href="FileSyncDetail.aspx?FileSyncExecRecord_id={$T.record.FileSyncExecRecord_id}" target="_blank" data-name="Preview">执行明细</a>
                    </td>
                </tr>
            {#/for}
        </textarea>

        <script type="text/javascript">
            var loadData = function () {
                var dto = {
                    SchoolId: "<%=SchoolId%>",
                    strFileSyncExecRecord_Status: $.trim($("#ddlStatus").val()),
                    PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                    PageIndex: pageIndex,
                    x: Math.random()
                };

                $.ajaxWebService("DownloadHWFiles.aspx/GetDataList", JSON.stringify(dto), function (data) {
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
            });

        </script>
    </form>
</body>
</html>
