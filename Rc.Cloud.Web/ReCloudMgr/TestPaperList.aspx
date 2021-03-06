﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="TestPaperList.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.TestPaperList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <input type="text" id="txtReName" class="form-control input-sm" placeholder="名称" />
                    <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
                    <input type="button" class="btn btn-default btn-sm" id="btnBack" value="返回" />
                </div>
                <table class="table table-hover table-bordered ">
                    <thead>
                        <tr>
                            <th>名称</th>
                            <th>大小/类型</th>
                            <th>日期</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody id="tbRes">
                    </tbody>
                </table>
                <textarea id="template_Res" class="hidden">
                    {#foreach $T.list as record}
                        <tr>
                            <td>{$T.record.docName}</td>
                            <td>{#if $T.record.RType=='1'}{$T.record.docSize}{#else}文件夹{#/if}</td>
                            <td>{$T.record.docTime}</td>
                            <td class="opera">
                                {#if $T.record.RType=='0'}
                                    <a href="TestPaperList.aspx?resourceFolderId={$T.record.docId}&resTitle=<%=resTitle %>--{$T.record.docName}&type=<%=type %>" >资源列表</a>
                                {#else}
                                    {#if $T.record.IType=='1'}
                                    <a href="FileView.aspx?ResourceToResourceFolder_Id={$T.record.docId}" target="_blank" data-name="Preview">预览</a>
                                    {#else}
                                    <a href="TestPaperView.aspx?ResourceToResourceFolder_Id={$T.record.docId}" target="_blank" data-name="Preview">预览</a>
                                    <a href="MarkQuestionAttr.aspx?ResourceToResourceFolder_Id={$T.record.docId}" target="_blank" data-name="Preview">试题属性维护</a>
                                    {#/if}
                                {#/if}
                               
                            </td>
                        </tr>
                    {#/for}
                </textarea>
                <hr />
                <div class="page"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        var loadData = function () {
            var dto = {
                ResourceFolder_Id: "<%=resourceFolderId%>",
                DocName: $.trim($("#txtReName").val()),
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                Module_Id: "<%=Module_Id%>",
                x: Math.random()
            };
            $.ajaxWebService("TestPaperList.aspx/GetResourceList", JSON.stringify(dto), function (data) {
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
            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();
            });
            $("#btnBack").click(function () {
                var type = '<%=type%>';
                var b = new Base64();
                var backurl = getUrlVar("backurl");
                if (backurl != null && backurl != undefined && backurl != "") {
                    window.location.href = b.decode(backurl);
                }
                else {
                    window.location.href = "TestPaperAttr.aspx";
                }

            })
        });
    </script>
</asp:Content>
