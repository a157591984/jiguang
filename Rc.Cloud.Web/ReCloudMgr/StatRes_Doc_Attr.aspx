<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="StatRes_Doc_Attr.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.Res_Doc_Attr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_sectin pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <input type="button" class="btn btn-default btn-sm" id="btnBack" value="返回" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>年份</th>
                            <th>文档类型</th>
                            <th><%=SAttrTypeName %></th>
                            <th>生产数量（册）</th>
                            <%--<th>下载频次（个）</th>--%>
                            <%--<th>销售数量（册）</th>--%>
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
         <td>{$T.record.SData_Name}</td>
         <td>{$T.record.SProductionCount}</td>
         <%--<td>{$T.record.SDownloadCount}</td>--%>
        <%--<td>{$T.record.SSaleCount}</td>--%>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(function () {
            pageIndex = 1; //页码
            SAttrType = getUrlVar("SAttrType");
            SYear = getUrlVar("SYear");
            SDocType = getUrlVar("SDocType");
            loadData();
            $("#btnBack").click(function () {
                var b = new Base64();
                var backurl = getUrlVar("backurl");
                if (backurl != "") {
                    window.location.href = b.decode(backurl);
                }
                else {
                    window.location.href = "Res_Doc.aspx";
                }
            });
        })
        var loadData = function () {
            var dto = {
                SAttrType: SAttrType,
                SDocType: SDocType,
                SYear: SYear,
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("StatRes_Doc_Attr.aspx/GetStatRes_Doc_Attr", JSON.stringify(dto), function (data) {
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
            loadData();
        }

    </script>
</asp:Content>
