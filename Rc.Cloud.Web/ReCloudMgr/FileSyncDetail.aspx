<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileSyncDetail.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FileSyncDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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
        <div class="container-fluid pb">
            <h3>执行记录明细</h3>
            <div class="form-inline search_bar mb">
                <asp:DropDownList ID="ddlStatus" CssClass="form-control input-sm" runat="server">
                    <asp:ListItem Text="--全部状态--" Value=""></asp:ListItem>
                    <asp:ListItem Text="进行中" Value="0"></asp:ListItem>
                    <asp:ListItem Text="成功" Value="1"></asp:ListItem>
                    <asp:ListItem Text="失败" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <input type="button" class="btn btn-primary btn-sm" id="btnSearch" value="查询" />
            </div>
            <table class="table table-hover table-bordered">
                <thead>
                    <tr>
                        <th style="width: 15%;">书名</th>
                        <th style="width: 15%;">开始时间</th>
                        <th style="width: 15%;">结束时间</th>
                        <th style="width: 10%;">执行时长</th>
                        <th style="width: 6%;">状态</th>
                        <th style="width: 20%;">地址</th>
                        <th style="width: 20%;">备注</th>
                    </tr>
                </thead>
                <tbody id="tbRes">
                </tbody>
            </table>
            <div class="page"></div>
        </div>

        <textarea id="template_Res" style="display: none">
            {#foreach $T.list as record}
                <tr class="tr_con_001">
                    <td>{$T.record.ResourceFolder_Name}</td>
                    <td>{$T.record.Detail_TimeStart}</td>
                    <td>{$T.record.Detail_TimeEnd}</td>
                    <td>{$T.record.Detail_Long}</td>
                    <td>{$T.record.Detail_Status}</td>
                    <td>{$T.record.FileUrl}</td>
                    <td>{$T.record.Detail_Remark}</td>
                </tr>
            {#/for}
        </textarea>

        <script type="text/javascript">
            var loadData = function () {
                var dto = {
                    strFileSyncExecRecord_id: '<%=strFileSyncExecRecord_id%>',
                    strDetail_Status: $.trim($("#ddlStatus").val()),
                    PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                    PageIndex: pageIndex,
                    x: Math.random()
                };

                $.ajaxWebService("FileSyncDetail.aspx/GetDataList", JSON.stringify(dto), function (data) {
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

            });

        </script>
    </form>
</body>
</html>


