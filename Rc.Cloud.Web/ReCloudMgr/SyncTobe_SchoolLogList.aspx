﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyncTobe_SchoolLogList.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.SyncTobe_SchoolLogList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>执行记录</title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <link href="../SysLib/plugin/auto-complete/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/bootstrap-3.3.7/js/bootstrap.min.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script src="../SysLib/js/jq.pagination.js"></script>
    <script src="../SysLib/js/json2.js"></script>
    <script src="../SysLib/js/base64.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/auto-complete/js/AutoComplete.js"></script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="form-inline search_bar mb">
            执行记录
            <div class="hidden">
                <asp:DropDownList ID="ddlStatus" CssClass="user_ddl" runat="server">
                    <asp:ListItem Text="--全部--" Value=""></asp:ListItem>
                    <asp:ListItem Text="进行中" Value="0"></asp:ListItem>
                    <asp:ListItem Text="成功" Value="1"></asp:ListItem>
                    <asp:ListItem Text="失败" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="input-group">
                <input type="hidden" id="hidtxtSchool" runat="server" clientidmode="Static" />
                <input type="text" id="txtSchool" clientidmode="Static" runat="server" autocomplete="off" class="form-control input-sm" placeholder="学校名称"
                    pautocomplete="True"
                    pautocompleteajax="AjaxAutoCompletePaged"
                    pautocompleteajaxkey="SCHOOL"
                    pautocompletevectors="AutoCompleteVectors"
                    pautocompleteisjp="0"
                    pautocompletepagesize="10" />
                <div class="input-group-btn">
                    <input type="button" class="btn btn-primary btn-sm" id="btnSearch" value="查询" />
                </div>
            </div>

        </div>
        <!--智能匹配载体-->
        <div id="AutoCompleteVectors" class="AutoCompleteVectors" hidden>
            <div id="topAutoComplete" class="topAutoComplete">
                简拼/汉字或↑↓
            </div>
            <div id="divAutoComplete" class="divAutoComplete">
                <ul id="AutoCompleteDataList" class="AutoCompleteDataList">
                </ul>
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
                    strFileSyncExecRecord_Type: $("#hidtxtSchool").val(),
                    strFileSyncExecRecord_Status: $.trim($("#ddlStatus").val()),
                    PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                    PageIndex: pageIndex,
                    x: Math.random()
                };

                $.ajaxWebService("SyncTobe_SchoolLogList.aspx/GetDataList", JSON.stringify(dto), function (data) {
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

            });

        </script>
    </form>
</body>
</html>
