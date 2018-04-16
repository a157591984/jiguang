<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="BookshelvesLogList.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.BookshelvesLogList" %>

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
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="名称"></asp:TextBox>
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="用户"></asp:TextBox>
                    <asp:TextBox ID="txtTime" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="时间"></asp:TextBox>
                    <select id="ddlType" class="form-control input-sm">
                        <option value="">全部</option>
                        <option value="1">上架</option>
                        <option value="2">下架</option>
                    </select>
                    <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" data-name="submit" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>序号</th>
                            <th>年份</th>
                            <th>文档类型</th>
                            <th>名称</th>
                            <th>用户</th>
                            <th>时间</th>
                            <th>类型</th>
                        </tr>
                    </thead>
                    <tbody id="tbRes">
                    </tbody>
                </table>
                <textarea id="template_Res" class="hidden">
                {#foreach $T.list as record}
                <tr>
                    <td>{$T.record.inum}</td>
                    <td>{$T.record.ParticularYear}</td>
                    <td>{$T.record.Resource_TypeName}</td>
                    <td>{$T.record.Book_Name}</td>
                    <td>{$T.record.UserName}</td>
                    <td>{$T.record.CreateTime}</td>
                    <td>{$T.record.LogTypeEnumName}</td>
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
                BookName: $.trim($("#txtName").val()),
                UserName: $.trim($("#txtUserName").val()),
                STime: $.trim($("#txtTime").val()),
                LogType: $.trim($("#ddlType").val()),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("BookshelvesLogList.aspx/GetBookshelvesLog", JSON.stringify(dto), function (data) {
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
            $('#txtTime').datetimepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                minView: 4,
                language: 'zh-CN'
            });

            pageIndex = 1;

            loadData();
            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();
            });
            $("#ddlType").change(function () {
                pageIndex = 1;
                loadData();
            });
        });
    </script>
</asp:Content>
