<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="ExeccuteDataAnalysis.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.ExeccuteDataAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/style001/user.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style001/pagination.css" rel="stylesheet" />
    <script language="javascript" src="../Scripts/json2.js"></script>
    <script src="../Scripts/jq.pagination.js"></script>
    <script src="../Scripts/jquery-jtemplates.js"></script>
    <script src="../Scripts/PhhcCommon.js" type="text/javascript"></script>
    <script src="../Scripts/base64.js"></script>
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
                    <td>
                        <asp:Button runat="server" ID="btnDataAnalysis" ClientIDMode="Static" OnClick="btnDataAnalysis_Click" Text="统计数据分析" CssClass="btn" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="" id="userDocumentContent">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
                <thead>
                    <tr class="tr_title">
                        <td>执行时间</td>
                        <td>执行时长</td>
                        <td>执行人</td>
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
        <td style="text-align:center">{$T.record.ExeccuteTime}</td>
        <td style="text-align:center">{$T.record.ExeccuteLenght}</td>
        <td style="text-align:center">{$T.record.ExeccuteUserName}</td>
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

            $("#btnDataAnalysis").click(function () {
                layer.load();
            });

        })
        var loadData = function () {
            setBasicUrl();
            var dto = {
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("ExeccuteDataAnalysis.aspx/GetExeccuteDataAnalysis", JSON.stringify(dto), function (data) {
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
            setBasicUrl();
            loadData();
        }

        var setBasicUrl = function () {
            basicUrl = "ExeccuteDataAnalysis.aspx?";
            backurl = b.encode(basicUrl + "pageIndex=" + pageIndex);
        }
        var loadParaFromLink = function () {
            pageIndex = getUrlVar("pageIndex") == "" ? 1 : getUrlVar("pageIndex");

        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
    </script>
</asp:Content>
