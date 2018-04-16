<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="cTeachPlan.aspx.cs" Inherits="Homework.teacher.cloudTeachPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <asp:Literal ID="litSubNaiv" ClientIDMode="Static" runat="server"></asp:Literal>
        </ul>
    </div>
    <div class="iframe-container teaching_plan_panel">
        <div class="iframe-sidebar">
            <asp:Literal ID="litTree" ClientIDMode="Static" runat="server"></asp:Literal>
        </div>
        <div class="iframe-main pa">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>名称</th>
                        <th width="100">大小</th>
                        <th width="170">日期</th>
                        <th width="80">操作</th>
                    </tr>
                </thead>
                <tbody id="tbRes">
                </tbody>
            </table>
            <div class="pagination_container">
                <ul class="pagination"></ul>
            </div>
        </div>
    </div>

    <textarea id="template_Res" class="hidden">
        {#foreach $T.list as record}
            <tr>
                <td class="text-left">{$T.record.docName}</td>
                <td>{$T.record.docSize}</td>
                <td>{$T.record.docTime}</td>
                <td>
                    <a href="FileView.aspx?ResourceToResourceFolder_Id={$T.record.docId}&Resource_Class=<%=strResource_Class %>" target="_blank" data-name="Preview">预览</a>
                    <%--{#if $T.record.IsDownload==true}
                    <a href="<%= Rc.Cloud.Web.Common.pfunction.GetResourceHost("TeachingPlanWebSiteUrl")  %>/common/downLoadFile.aspx?iid={$T.record.docId}">下载</a>
                    {#/if}--%>
                    {$T.record.shareUrl}
                    <%--{#if $T.record.Resource_Class==true}
                    {#if $T.record.IsShare !=""}
                    <a href='##' onclick="cancel('{$T.record.docId}')">取消分享</a>
                    {#else}
                    <a href='##' onclick="share('{$T.record.docId}')">分享</a>
                    {#/if}
                    {#/if}--%>
                </td>
            </tr>
        {#/for}
    </textarea>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            ResourceFolder_Id = "";
            docName = "";
            ShowFolderIn = "1";
            //loadData();
            //$(".tree a:eq(0)").click();

            $('.mtree-hook').mtree({
                display: 2,
                url: true,
                onClick: function (obj) {
                    ResourceFolder_Id = obj.data('id');
                    ShowFolderIn = "0";
                    pageIndex = 1;
                    loadData();
                },
                onLoad: function () {
                    $('.mtree-hook a').eq(0).click()
                }
            });
        });

        //function ShowSubDocument(strResourceFolder_Id) {
        //    ResourceFolder_Id = strResourceFolder_Id;
        //    ShowFolderIn = "0";
        //    pageIndex = 1;
        //    loadData();
        //}
        function loadData() {
            var strResource_Class = '<%=strResource_Class%>';//资源类别
            var dto = {
                ResourceFolder_Id: ResourceFolder_Id,
                DocName: docName,
                ShowFolderIn: ShowFolderIn,//0加载全部 1加载文件夹
                strResource_Class: strResource_Class,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                x: Math.random()
            };
            $.ajaxWebService("cTeachPlan.aspx/GetCloudResource", JSON.stringify(dto), function (data) {
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
                    $("#tbRes").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
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
            }, function () { });
        }
        function pageselectCallback(page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }

        //function share(ResourceToResourceFolder_Id) {
        //    var dto = {
        //        ResourceToResourceFolder_Id: ResourceToResourceFolder_Id,
        //        x: Math.random()
        //    };
        //    $.ajaxWebService("cTeachPlan.aspx/share", JSON.stringify(dto), function (data) {
        //        if (data.d != "") {
        //            layer.msg("分享资源成功", { time: 1000, icon: 1 }, function () { loadData(); });
        //        }
        //        else { layer.msg("分享资源失败", { time: 2000, icon: 2 }); }
        //    }, function () { }, false);
        //}

        //function cancel(ResourceToResourceFolder_Id) {
        //    var dto = {
        //        ResourceToResourceFolder_Id: ResourceToResourceFolder_Id,
        //        x: Math.random()
        //    };
        //    $.ajaxWebService("cTeachPlan.aspx/cancel", JSON.stringify(dto), function (data) {
        //        if (data.d != "") {
        //            layer.msg("取消分享资源成功", { time: 1000, icon: 1 }, function () { loadData(); });
        //        }
        //        else { layer.msg("取消分享资源失败", { time: 2000, icon: 2 }); }
        //    }, function () { }, false);
        //}
    </script>
</asp:Content>
