<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="Res_Sell.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.Res_Sell" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <asp:Button runat="server" ID="btnStatisticsData" ClientIDMode="Static" Text="统计生产数据" OnClick="btnStatisticsData_Click" CssClass="btn btn-primary btn-sm" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>年份</th>
                            <th>文档类型</th>
                            <%--<th>生产数量（册）</th>--%>
                            <%-- <th>下载频次（个）</th>--%>
                            <th>销售数量（册）</th>
                            <th>按教材版本统筹</th>
                            <th>按年级学期统筹</th>
                            <th>按学科统筹</th>
                            <th>按区域统筹</th>
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
    <textarea id="template_tb1" class="hidden">
    {#foreach $T.list as record}
    <tr>
        <td>{$T.record.SYear}</td>
        <td>{$T.record.SDocTypeName}</td>
        <td><a href="Res_SellDetailList.aspx?SYear={$T.record.SYear}&SDocType={$T.record.SDocType}&SDocTypeName={$T.record.SDocTypeName}">{$T.record.SSaleCount}</a></td>
        <td class="opera">{$T.record.textbookversion}</td>
        <td class="opera">{$T.record.gradeterm}</td>
        <td class="opera">{$T.record.subject}</td>
        <td class="opera">{$T.record.area}</td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(function () {
            b = new Base64();
            pageIndex = 1; //页码
            basicUrl = ""; //本页基础url(不包括页码参数)
            backurl = ""; //跳转所用bas64加页码url
            loadParaFromLink();
            loadData();
        })
        var loadData = function () {
            setBasicUrl();
            var dto = {
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("Res_Sell.aspx/GetStatRes_Doc", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbRes").setTemplateElement("template_tb1", null, { filter_data: false });
                    $("#tbRes").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbRes").html("<td style=\"padding-right: 4px; text-align: center;color:red;\" colspan=\"100\">无符合条件的数据</td>");
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
            setBasicUrl();
            loadData();
        }

        var setBasicUrl = function () {
            basicUrl = "Res_Sell.aspx?";
            backurl = b.encode(basicUrl + "pageIndex=" + pageIndex);
        }
        var loadParaFromLink = function () {
            pageIndex = getUrlVar("pageIndex") == "" ? 1 : getUrlVar("pageIndex");

        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        var Show = function (url) {
            window.location.href = url + "&backurl=" + backurl;
        }
    </script>
</asp:Content>
