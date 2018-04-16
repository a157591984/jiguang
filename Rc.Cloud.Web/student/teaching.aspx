<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/student.Master" AutoEventWireup="true" CodeBehind="teaching.aspx.cs" Inherits="Rc.Cloud.Web.student.teaching" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--子导航-->
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="teachingPlan.aspx">已购买教案</a></li>
            <li><a href="allTeachingPlan.aspx">全部教案</a></li>
            <li><a href="microClass.aspx">已购买微课件</a></li>
            <li><a href="allMicroClass.aspx">全部微课件</a></li>
            <li><a href="teaching.aspx" class="active">已购买习题集</a></li>
            <li><a href="allTeaching.aspx">全部习题集</a></li>
        </ul>
    </div>
    <!--内容-->
    <div class="iframe-container">
        <div class="container pt">
            <div class="row goods_list_panel" id="tbRes"></div>
            <div class="pagination_container">
                <ul class="pagination"></ul>
            </div>
            <textarea id="template_Res" style="display: none">
            {#foreach $T.list as record}
                <div class="col-xs-2">
                    <div class="thumbnail purchase">
                        <a href='##' title="{$T.record.Book_Name}">
                            <img src="{$T.record.BookImg_Url}" onerror="this.src='../images/re_nopic.jpg'">
                            <div class="caption">
                                <p class="res_name">{$T.record.Book_Name}</p>
                                <p>￥{$T.record.BookPrice}</p>
                            </div>
                        </a>
                    </div>
                </div>
            {#/for}
            </textarea>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        var loadData = function () {
            var dto = {
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 12 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("teaching.aspx/GetResourceFolderListPage", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbRes").setTemplateElement("template_Res", null, { filter_data: false });
                    $("#tbRes").processTemplate(json);
                    $(".pagination_container").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbRes").html("<li>暂无数据</li>");
                    $(".pagination_container").find("ul").html("");
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
            }, function () { }, false);
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        $(function () {
            pageIndex = 1;
            loadData();
        });
    </script>
</asp:Content>
