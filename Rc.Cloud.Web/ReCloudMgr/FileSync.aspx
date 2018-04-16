<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="FileSync.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FileSync" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/style001/pagination.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/plug-in/laydate/laydate.js"></script>
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
                        <input type="button" class="btn" id="btnLoad" value="查询" /></td>
                    <td>日期：<asp:TextBox ID="txtBeginTime" runat="server" ClientIDMode="Static" CssClass="txt_Search myTextBox laydate-icon" placeholder="同步日期"></asp:TextBox>
                        类型：<asp:DropDownList runat="server" ID="ddlType">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="1">教案</asp:ListItem>
                            <asp:ListItem Value="2">习题集</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button runat="server" ID="btnFileSync" ClientIDMode="Static" OnClick="btnDataAnalysis_Click" Text="手动同步资源" CssClass="btn" />
                    </td>

                </tr>
                <tr>
                    <td colspan="2"><span style="color: red; margin: 0;">注意：同步文件过多时会有延时，请注意同步频率（建议同步时间间隔在10分钟以上）！</span></td>
                </tr>
            </table>
        </div>
        <div class="" id="userDocumentContent">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
                <thead>
                    <tr class="tr_title">
                        <td>同步时间</td>
                        <td>同步时长</td>
                        <td>同步用户</td>
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
        <td style="text-align:center">{$T.record.SyncTime}</td>
        <td style="text-align:center">{$T.record.SyncLong}</td>
        <td style="text-align:center">{$T.record.SyncUserName}</td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(function () {
            pageIndex = 1; //页码
            basicUrl = ""; //本页基础url(不包括页码参数)
            backurl = ""; //跳转所用bas64加页码url
            loadData();

            $("#btnLoad").click(function () { loadData(); });

            $("#btnFileSync").click(function () {
                if ($.trim($("#txtBeginTime").val()) == "") {
                    layer.msg("请选择日期", { icon: 2, time: 2000 });
                    return false;
                }
                layer.load();
            });

            //$('#txtBeginTime').val(laydate.now(0, 'YYYY-MM-DD'));
            var starTime = {
                elem: '#txtBeginTime',
                format: 'YYYY-MM-DD',
                istime: false, //是否开启时间选择
                max: laydate.now()
            };
            laydate(starTime);

        });

        var loadData = function () {
            var dto = {
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };

            $.ajaxWebService("FileSync.aspx/GetDataList", JSON.stringify(dto), function (data) {
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
            loadData();
        }

        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
    </script>
</asp:Content>
