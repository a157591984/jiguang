<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SendSMSList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SendSMSList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/mtree-2.0/mtree.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />

    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/bootstrap-3.3.7/js/bootstrap.min.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/plugin/mtree-2.0/mtree.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script src="../SysLib/js/jq.pagination.js"></script>
    <script src="../SysLib/js/json2.js"></script>
    <script src="../SysLib/js/base64.js"></script>
    <script src="../SysLib/js/index.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_right_title">
        <div class="div_right_title_icon"></div>
        <%=siteMap%>
    </div>
    <div class="clearDiv"></div>
    <div class="div_right_search">
        <table class="table_search_001">
            <tr>
                <td>手机号：
                </td>
                <td>
                    <input type="text" id="txtMobile" class="txt w100">
                </td>
                <td>
                    <input type="button" value="查询" class="btn" id="btnSearch">
                </td>
            </tr>
        </table>
    </div>
    <div class="clearDiv"></div>
    <!--主数据-->
    <table class="table_list" cellpadding="0" cellspacing="0">
        <thead>
            <tr class="tr_title">
                <td>手机号</td>
                <td>内容</td>
                <td>状态</td>
                <td>时间</td>
            </tr>
        </thead>
        <tbody id="list">
        </tbody>
    </table>
    <hr />
    <div class="page"></div>
    <textarea id="listBox" style="display: none;">
        {#foreach $T.list as record}
        <tr class="tr_con_002 cc">
            <td>{$T.record.Mobile}</td>
            <td>{$T.record.Content}</td>
            <td>{$T.record.Status}</td>
            <td>{$T.record.CTime}</td>
        </tr>
        {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            loadData();

            $(document).on("click", "#btnSearch", function () {
                pageIndex = 1;
                loadData();
            });
        })

        function loadData() {
            var dto = {
                Mobile: $("#txtMobile").val(),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            }
            $.ajaxWebService("SendSMSList.aspx/GetDataList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#list").setTemplateElement("listBox", null, { filter_data: false });
                    $("#list").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });

                }
                else {
                    $("#list").html("<tr class='tr_con_002'><td colspan='100' align='center'>暂无数据</td></tr>");
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
        function pageselectCallback(page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
    </script>
</asp:Content>
