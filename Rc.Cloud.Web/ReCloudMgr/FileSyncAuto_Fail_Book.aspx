<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="FileSyncAuto_Fail_Book.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FileSyncAuto_Fail_Book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/style001/user.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style001/pagination.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_right_title">
        <div class="div_right_title_icon"><%--<a href="javascript:history.back(-1);">返回上一级</a>--%></div>
        <%=siteMap%>
        <div class="div_right_title_002"></div>
        <div class="div_right_title_001" id="div_right_title_1">失败明细</div>
    </div>
    <div class="clearDiv"></div>
    <div class="div_right_search">
        <table class="table_search_001">
            <tbody>
                <tr>
                    <td>
                        <input type="button" class="btn" id="btnBack" value="返回" clientidmode="Static" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div style="width: 100%">
        <div class="" id="userDocumentContent">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
                <thead>
                    <tr class="tr_title">
                        <td>书名</td>
                        <td style="width: 12%;">操作</td>
                    </tr>
                </thead>
                <tbody id="tbRes">
                </tbody>
            </table>
            <hr />
            <div class="page"></div>
        </div>
        <textarea id="template_Res" style="display: none">
        {#foreach $T.list as record}
            <tr class="tr_con_001">
                <td>{$T.record.ResourceFolder_Name}</td>
                <td class="align_center table_opera">
                   <a href="FileSyncAuto_FailDetail.aspx?Book_id={$T.record.Book_Id}&FileSyncExecRecord_id={$T.record.FileSyncExecRecord_id}&FileName={$T.record.ResourceFolder_NameE}">失败明细</a>
                </td>
            </tr>
        {#/for}
    </textarea>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        var loadData = function () {
            var dto = {
                FileSyncExecRecord_id: "<%=FileSyncExecRecord_id%>",
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                x: Math.random()
            };
            $.ajaxWebService("FileSyncAuto_Fail_Book.aspx/GetDataList", JSON.stringify(dto), function (data) {
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

            $("#btnBack").click(function () {
                window.location.href = "FileSyncAuto_Fail.aspx";
            });
        });

    </script>
</asp:Content>
