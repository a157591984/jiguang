<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="Res_SellDetailList.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.Res_SellDetailList" %>

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
                    <select class="form-control input-sm" id="ddlBuyType">
                        <option value="">购买方式</option>
                        <option value="NBSQ">内部授权</option>
                        <option value="ALIPAY">网上购买</option>
                    </select>
                    <asp:TextBox ID="txtTime" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="购买日期"></asp:TextBox>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="文件名"></asp:TextBox>
                    <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" data-name="submit" />
                    <a href="javascript:history.back(-1);" class="btn btn-default btn-sm">返回</a>
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>年份</th>
                            <th>文档类型</th>
                            <th>名称</th>
                            <th>购买日期</th>
                            <th>购买人</th>
                            <th>购买方式</th>
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
        <td><%=SYear%></td>
        <td><%=SDocTypeName%></td>
        <td>{$T.record.ResourceName}</td>
        <td>{$T.record.BuyTime}</td>
        <td>{$T.record.BuyUser}</td>
        <td>{$T.record.BuyType}</td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">

        var loadData = function () {
            var dto = {
                ParticularYear: "<%=SYear%>",
                Resource_Type: "<%=SDocType%>",
                BuyType: $.trim($("#ddlBuyType").val()),
                Time: $.trim($("#txtTime").val()),
                ReName: $.trim($("#txtName").val()),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("Res_SellDetailList.aspx/GetDataList", JSON.stringify(dto), function (data) {
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

            $("#ddlBuyType").change(function () {
                pageIndex = 1;
                loadData();
            });

        });
    </script>
</asp:Content>
