<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="visit_client.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.visit_client" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <select id="ddlDateType" class="form-control input-sm">
                        <option value="day" selected="selected">日统计</option>
                        <option value="week">周统计</option>
                        <option value="month">月统计</option>
                    </select>
                    <input id="txtTName" class="form-control input-sm" type="text" placeholder="老师" />
                    <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th rowspan="2">日期</th>
                            <th rowspan="2">老师</th>
                            <th colspan="3" class="text-center">访问频次</th>
                            <th colspan="3" class="text-center">访问文件数</th>
                            <th colspan="3" class="text-center">创建自有文件数</th>
                            <%--<td rowspan="2">总时长</td>
                        <td rowspan="2">平均时长</td>--%>
                            <th rowspan="2">操作</th>
                        </tr>
                        <tr>
                            <th>所有</th>
                            <th>云</th>
                            <th>自有</th>
                            <th>所有</th>
                            <th>云</th>
                            <th>自有</th>
                            <th>总数</th>
                            <th>教案个数</th>
                            <th>作业个数</th>
                        </tr>
                    </thead>
                    <tbody id="list"></tbody>
                </table>
                <textarea id="listBox" class="hidden">
                {#foreach $T.list as record}
                <tr>
                        <td>{$T.record.DateData}</td>
                        <%--<td> {#if $T.record.TeacherClass==0}-{#else}<a href="javascript:;" title="点击查看" onclick="GetClassName('{$T.record.TeacherId}')">（{$T.record.TeacherClass}）个</a>{#/if}</td>--%>
                        <td>{$T.record.TeacherName}{#if $T.record.TrueName==""}{#else}({$T.record.TrueName}){#/if}</td>
                        <td>{$T.record.VisitCount_All}</td>
                        <td>{$T.record.VisitCount_Cloud}</td>
                        <td>{$T.record.VisitCount_Own}</td>
                        <td>{$T.record.VisitFile_All}</td>
                        <td>{$T.record.VisitFile_Cloud}</td>
                        <td>{$T.record.VisitFile_Own}</td>
                        <td>{$T.record.CreateOwnCount_All}</td>
                        <td>{$T.record.CreateOwnCount_Plan}</td>
                        <td>{$T.record.CreateOwnCount_TestPaper}</td>
                        <td class="opera">
                            {#if $T.record.VisitCount_All!=0 || $T.record.CreateOwnCount_All!=0}
                            <a href="visit_client_detail.aspx?DateType={$T.record.DateType}&DateData={$T.record.DateData}&TeacherId={$T.record.TeacherId}" target="_blank">查看</a>
                            {#else}
                            <a href="javascript:;" class="disabled">查看</a>
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
        $(function () {

            $("#ddlDateType").change(function () {
                pageIndex = 1;
                loadData();
            });

            $('#btnSearch').click(function () {
                pageIndex = 1;
                loadData();
            })
            $(document).keydown(function (e) {
                if (e.keyCode == 13) {
                    $('#btnSearch').click();
                    return false;
                }
            })

            pageIndex = 1;//默认页码
            loadData();//初始化数据
        })
        var loadData = function () {
            var $_objBox = $("#listBox");
            var objBox = "listBox";
            var $_objList = $("#list");
            var $_pagination = $(".page");
            var dto = {
                DateType: $("#ddlDateType").val(),
                TeacherName: $.trim($("#txtTName").val()),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("visit_client.aspx/GetList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $_objList.setTemplateElement(objBox, null, { filter_data: false });
                    $_objList.processTemplate(json);
                    $_pagination.pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $_objList.html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $_pagination.html("");
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
    </script>
</asp:Content>
