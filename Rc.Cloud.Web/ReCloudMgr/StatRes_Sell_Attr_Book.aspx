<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="StatRes_Sell_Attr_Book.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.StatRes_Sell_Attr_Book" %>

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
        <div class="" id="userDocumentContent">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
                <thead>
                    <tr class="tr_title">
                        <td>年份</td>
                        <td>文档类型</td>
                        <td><%=SAttrTypeName %></td>
                        <%--<td>生产数量（册）</td>--%>
                        <%--<td>下载频次（个）</td>--%>
                        <td>销售数量（册）</td>
                    </tr>
                </thead>
                <tbody id="tbRes">
                </tbody>
            </table>
            <hr />
            <div class="page"></div>
        </div>
        <div style="text-align: center; margin-top: 30px;">
            <input type="button" class="btn" id="btnBack" value="返回" />
        </div>
    </div>

    <textarea id="template_tb1" style="display: none">
    {#foreach $T.list as record}
    <tr class="tr_con_001">
        <td>{$T.record.SYear}</td>
        <td>{$T.record.SDocTypeName}</td>
         <td>{$T.record.SData_Name}</td>
         <%--<td>{$T.record.SProductionCount}</td>--%>
         <%--<td>{$T.record.SDownloadCount}</td>--%>
        <td>{$T.record.SSaleCount}</td>
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
                    window.location.href = "Res_Sell.aspx";
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
            $.ajaxWebService("StatRes_Sell_Attr.aspx/GetStatRes_Doc_Attr", JSON.stringify(dto), function (data) {
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
