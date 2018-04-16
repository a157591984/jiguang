<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyncFileSchoolList_Tobe.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.SyncFileSchoolList_Tobe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>待同步文件-学校列表</title>
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
            <div>
                <input type="text" class="form-control input-sm" id="txtName" placeholder="学校名称" autocomplete="off" />
                <input type="button" class="btn btn-primary btn-sm" id="btnSearch" value="查询" />
                <a href="SyncTobe_SchoolLogList.aspx" target="_blank" class="btn btn-primary btn-sm">执行记录</a>
                此处显示的全部为审核通过且进行过数据同步的书
            </div>
        </div>
        <table class="table table-hover table-bordered">
            <thead>
                <tr>
                    <th>学校名称（标识）</th>
                    <th>公网地址</th>
                    <th>图书数量</th>
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
                    <td>{$T.record.SchoolName}（{$T.record.SchoolId}）</td>
                    <td>{$T.record.D_PublicValue}</td>
                    <td>{$T.record.icount}</td>
                    <td class="opera">
                        <a href="{$T.record.D_PublicValue}ReCloudMgr/SyncFileSchool_Tobe.aspx?SchoolId={$T.record.SchoolId}&SysUser_ID=<%=SysUser_ID%>" target="_blank">明细</a>
                    </td>
                </tr>
            {#/for}
        </textarea>

        <script type="text/javascript">
            var loadData = function () {
                var dto = {
                    Name: $("#txtName").val(),
                    PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                    PageIndex: pageIndex,
                    x: Math.random()
                };

                $.ajaxWebService("SyncFileSchoolList_Tobe.aspx/GetDataList", JSON.stringify(dto), function (data) {
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
                //$("#btnSync").click(function () {
                //    window.open("SyncTobe_SchoolLogList.aspx")
                //})
            });

        </script>
    </form>
</body>
</html>
