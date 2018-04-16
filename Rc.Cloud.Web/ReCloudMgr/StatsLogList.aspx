<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="StatsLogList.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.StatsLogList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/style001/user.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style001/pagination.css" rel="stylesheet" />
    <script src="../Scripts/PhhcCommon.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_right_title">
        <div class="div_right_title_icon"></div>
        <%=siteMap%>
    </div>
    <div class="clearDiv"></div>
    <div class="clear"></div>
    <div style="width: 100%">
        <div class="div_right_search">
            <table class="table_search_001">
                <tr>
                    <td>执行方式：
                            <select id="ddlType">
                                <option value="">全部</option>
                                <option value="1">按日期</option>
                                <option value="2">按试卷</option>
                            </select>
                    </td>
                    <td>状态：
                            <select id="ddlStatus">
                                <option value="">全部</option>
                                <option value="1">成功</option>
                                <option value="2">失败</option>
                            </select>
                    </td>
                    <td>名称：
                            <input type="text" id="txtName" class="txt_Search myTextBox" />
                    </td>
                    <td>
                        <input type="button" class="btn" id="btnSearch" value="查询" />
                        <input type="button" value="手动统计" class="btn" onclick="OperateStatsData();" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="" id="userDocumentContent">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
                <thead>
                    <tr class="tr_title">
                        <td>序号</td>
                        <td>执行方式</td>
                        <td>作业/试卷名称</td>
                        <td>状态</td>
                        <td>执行时间</td>
                        <td>操作</td>
                    </tr>
                </thead>
                <tbody id="tb1">
                </tbody>
            </table>
            <hr />
            <div class="page"></div>
        </div>
    </div>

    <textarea id="template_tb1" style="display: none">
    {#foreach $T.list as record}
    <tr class="tr_con_001">
        <td>{$T.record.Num}</td>
        <td>{$T.record.DataTypeName}</td>
        <td>{$T.record.DataName}</td>
        <td>{$T.record.LogStatus}</td>
        <td>{$T.record.CTime}</td>
        <td>
            <a title="重新执行"  href="javascript:;" onclick="ReExecuteStatsData('{$T.record.StatsLogId}')">重新执行</a>
        </td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(function () {
            pageIndex = 1; //页码
            loadData();

            $("#btnSearch").on({
                click: function () {
                    pageIndex = 1;
                    loadData();
                }
            });
            $("#ddlType,#ddlStatus").on({
                change: function () {
                    pageIndex = 1;
                    loadData();
                }
            });
        })
        var loadData = function () {
            var dto = {
                DataType: $("#ddlType").val(),
                DataName: $.trim($("#txtName").val()),
                LogStatus: $.trim($("#ddlStatus").val()),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };

            $.ajaxWebService("StatsLogList.aspx/GetDataList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template_tb1", null, { filter_data: false });
                    $("#tb1").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tb1").html("<tr class='tr_con_001'><td style=\"padding-right: 4px; text-align: center;color:red;\" colspan=\"100\">暂无数据</td></tr>");

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

        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }

        //编辑
        function OperateStatsData() {
            layer.open({
                type: 2,
                title: "执行统计",
                fix: false,
                area: ["600px", "470px"],
                content: "StatsLogOperate.aspx"
            })
        }

        //重新执行统计
        function ReExecuteStatsData(StatsLogId) {
            layer.confirm("确定要重新执行吗？", { icon: 3, title: "提示" }, function () {
                $.ajaxWebService("StatsLogList.aspx/ReExecuteStatsData", "{StatsLogId:'" + StatsLogId + "',x:" + Math.random() + "}", function (data) {
                    if (data.d == "1") {
                        layer.msg("重新执行成功", { icon: 1, time: 1000 }, function () { loadData(); });
                    }
                    else {
                        layer.msg("重新执行失败", { icon: 2, time: 2000 });
                    }
                }, function () { })
            });
        }
    </script>
</asp:Content>
