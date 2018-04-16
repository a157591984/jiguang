<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="BookProductionLogList.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.BookProductionLogList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../SysLib/plugin/datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="../SysLib/plugin/datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../SysLib/plugin/datetimepicker/js/locales/bootstrap-datetimepicker.zh-CN.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <asp:DropDownList ID="ddlYear" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlResource_Type" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtTime" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="生产日期"></asp:TextBox>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="文件名"></asp:TextBox>
                    <asp:TextBox ID="txtUser" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="生产员"></asp:TextBox>
                    <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" data-name="submit" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>年份</th>
                            <th>文档类型</th>
                            <th>名称</th>
                            <th>生产日期</th>
                            <th>上传时间</th>
                            <th>结束时间</th>
                            <th>数量（个）</th>
                            <th>生产员</th>
                        </tr>
                    </thead>
                    <tbody id="tbRes">
                    </tbody>
                </table>
                <hr />
                <div class="page"></div>
            </div>
        </div>
    </div>
    <textarea id="template_Res" class="hidden">
    {#foreach $T.list as record}
    <tr>
        <td>{$T.record.ParticularYear}</td>
        <td>{$T.record.Resource_TypeName}</td>
        <td>{$T.record.ResourceName}</td>
        <td>{$T.record.CreateTime}</td>
        <td>{$T.record.STime}</td>
        <td>{$T.record.ETime}</td>
        <td>{$T.record.ReCount}</td>
        <td>{$T.record.CreateUser}</td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">

        var loadData = function () {
            var dto = {
                ParticularYear: $.trim($("#ddlYear").val()),
                Resource_Type: $.trim($("#ddlResource_Type").val()),
                Time: $.trim($("#txtTime").val()),
                ReName: $.trim($("#txtName").val()),
                CreateUsesr: $.trim($("#txtUser").val()),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("BookProductionLogList.aspx/GetDataList", JSON.stringify(dto), function (data) {
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
                    $("#tbRes").html("<tr class='tr_con_002'><td colspan='100' align='center'>暂无数据</td></tr>");
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
            $("#txtTime").datetimepicker({
                format: 'yyyy-mm-dd',
                language: 'zh-CN',
                minView: 4,
                autoclose: true
            });

            pageIndex = 1;
            loadData();

            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();
            });

            $("#ddlYear").change(function () {
                pageIndex = 1;
                loadData();
            });
            $("#ddlResource_Type").change(function () {
                pageIndex = 1;
                loadData();
            });

        });
    </script>
</asp:Content>
