<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/student.Master" AutoEventWireup="true" CodeBehind="allTeaching.aspx.cs" Inherits="Rc.Cloud.Web.student.allTeaching" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <%--<li><a href="teachingPlan.aspx">已购买教案</a></li>--%>
            <li><a href="allTeachingPlan.aspx">全部教案</a></li>
            <%--<li><a href="microClass.aspx">已购买微课件</a></li>--%>
            <li><a href="allMicroClass.aspx">全部微课件</a></li>
            <%--<li><a href="teaching.aspx">已购买习题集</a></li>--%>
            <li><a href="allTeaching.aspx" class="active">全部习题集</a></li>
        </ul>
    </div>

    <div class="iframe-container">
        <div class="container pt">
            <div class="filter">
                <div class="form-inline">
                    <div class="form-group">
                        <label>所在地区：</label>
                        <asp:DropDownList runat="server" ID="ddlProvince" CssClass="form-control input-sm" onchange="loadData();" Style="width: auto;" ClientIDMode="Static"></asp:DropDownList>
                        <select id="ddlCity" onchange="loadData();" class="form-control input-sm" style="width: auto;">
                            <option value="-1">市区</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label>教材名称：</label>
                        <input type="text" id="txtName" class="form-control input-sm" />
                    </div>
                    <input type="button" id="btnSearch" value="检索" class="btn btn-primary btn-sm" />
                </div>
                <div class="filter_section">
                    <div class="filter_row clearfix">
                        <span class="row_name">年份：</span>
                        <div class="row_item">
                            <ul ajax-name="year">
                                <asp:Literal ID="ddlYear" runat="server" ClientIDMode="Static"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">年级学期：</span>
                        <button type="button" class="btn btn-default btn-sm row_switch" data-name="rowSwitch">展开</button>
                        <div class="row_item" data-name="rowItem">
                            <ul ajax-name="gradeTerm">
                                <asp:Literal ID="ddlGradeTerm" runat="server" ClientIDMode="Static"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">学科：</span>
                        <div class="row_item">
                            <ul ajax-name="subject">
                                <asp:Literal ID="ddlSubject" runat="server" ClientIDMode="Static"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">教材版本：</span>
                        <button type="button" class="btn btn-default btn-sm row_switch" data-name="rowSwitch">展开</button>
                        <div class="row_item" data-name="rowItem">
                            <ul ajax-name="version">
                                <asp:Literal ID="ddlVersion" runat="server" ClientIDMode="Static"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row goods_list_panel" id="tbRes"></div>
            <textarea id="template_Res" style="display: none">
            {#foreach $T.list as record}
                <div class="col-xs-2">
                    {#if $T.record.isGouMai=="0"}
                    <div class="thumbnail no_purchase">
                        <a href="/teacher/TeachingplanShow.aspx?Type=1&ResourceFolder_ID={$T.record.ResourceFolder_ID}" title="{$T.record.Book_Name}" target="_blank">
                            <div class="thumb"><img src="{$T.record.BookImg_Url}" {#if $T.record.imgHeight !=0}height="{$T.record.imgHeight}"{#/if}  {#if $T.record.imgWidth !=0}width="{$T.record.imgWidth}"{#/if} onerror="this.src='../images/re_nopic.jpg'"></div>
                            <div class="caption">
                                <p class="res_name">{$T.record.Book_Name}</p>
                                <p>￥{$T.record.BookPrice}</p>
                            </div>
                        </a>
                    </div>
                    {#elseif  $T.record.isGouMai=="1"}
                    <div class="thumbnail purchase">
                        <a href="/teacher/TeachingplanShow.aspx?Type=1&ResourceFolder_ID={$T.record.ResourceFolder_ID}" target="_blank" title="{$T.record.Book_Name}">
                            <div class="thumb"><img src="{$T.record.BookImg_Url}" {#if $T.record.imgHeight !=0}height="{$T.record.imgHeight}"{#/if}  {#if $T.record.imgWidth !=0}width="{$T.record.imgWidth}"{#/if} onerror="this.src='../images/re_nopic.jpg'"></div>
                            <div class="caption">
                                <p class="res_name">{$T.record.Book_Name}</p>
                                <p>￥{$T.record.BookPrice}</p>
                            </div>
                        </a>
                    </div>
                    {#/if}
                </div>
            {#/for}
            </textarea>
            <div class="pagination_container">
                <ul class="pagination"></ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            //搜索
            $('[ajax-name] li').on({
                click: function () {
                    if (!$(this).children("a").hasClass("disabled")) {
                        $(this).closest('ul').find('a').removeClass('active');
                        $(this).children('a').addClass('active');
                        pageIndex = 1;
                        loadData();
                    }
                }
            });
        });

        var loadData = function () {
            var dto = {
                ParticularYear: $('[ajax-name="year"]').find('a.active').attr('ajax-value'),
                GradeTerm: $('[ajax-name="gradeTerm"]').find('a.active').attr('ajax-value'),
                Subject: $('[ajax-name="subject"]').find('a.active').attr('ajax-value'),
                Resource_Version: $('[ajax-name="version"]').find('a.active').attr('ajax-value'),
                Province: $("#ddlProvince").val(),
                City: $("#ddlCity").val(),
                Name: $.trim($("#txtName").val()),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 12 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("allTeaching.aspx/GetResourceFolderListPage", JSON.stringify(dto), function (data) {
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
            $("#ddlProvince").change(function () {
                LoadCity($(this).val());
            });
            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();
            });
        });
        var LoadCity = function (pid) {
            $.ajaxWebService("../Teacher/allTeaching.aspx/GetAreaCityInfo", "{pid:'" + pid + "',x:'" + Math.random() + "'}", function (data) { $("#ddlCity").html(data.d); }, function () { $("#ddlCity").html("<option value=\"-1\">市区</option>"); }, false);
        }
    </script>
</asp:Content>
