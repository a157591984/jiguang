<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyncData_Tobe.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.SyncData_Tobe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>待同步数据</title>
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
            <input runat="server" type="button" id="btnConfirm" value="执行数据同步" class="btn btn-primary btn-sm" />
            <asp:Button ID="btnData" runat="server" CssClass="btn btn-primary btn-sm hide" Text="执行数据同步" OnClick="btnData_Click" />
            <input type="button" value="执行记录" class="btn btn-primary btn-sm" onclick="javascript: window.open('SyncTobe_LogList.aspx?FileSyncExecRecord_Type=同步数据new');" />

            <div class="hidden">
                <asp:DropDownList ID="ddlStatus" CssClass="user_ddl" runat="server">
                    <asp:ListItem Text="--状态--" Value=""></asp:ListItem>
                    <asp:ListItem Text="未同步" Value="0"></asp:ListItem>
                    <asp:ListItem Text="同步失败" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <input type="button" class="btn" id="btnSearch" value="查询" />
            </div>
        </div>
        <table class="table table-hover table-bordered">
            <thead>
                <tr>
                    <td>TableName</td>
                    <td>操作类型</td>
                    <td>时间</td>
                    <td>书名</td>
                    <td>资源名</td>
                    <td>状态</td>
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
            <td>{$T.record.TableName}</td> 
            <td>{$T.record.OperateType}</td>
            <td>{$T.record.CreateTime}</td>
            <td>{$T.record.bookName}</td>
            <td>{$T.record.rtrfName}</td>
            <td>{$T.record.Status}</td>
        </tr>
        {#/for}
        </textarea>
        <script type="text/javascript">
            var loadData = function () {
                var dto = {
                    Status: $.trim($("#ddlStatus").val()),
                    PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                    PageIndex: pageIndex,
                    x: Math.random()
                };

                $.ajaxWebService("SyncData_Tobe.aspx/GetDataList", JSON.stringify(dto), function (data) {
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

                $("#btnConfirm").click(function () {
                    layer.confirm("您确定要执行同步数据吗？", { icon: 2, title: "提示" }, function () {
                        $("#btnData").click();
                    });
                });

            });

        </script>
    </form>
</body>
</html>
