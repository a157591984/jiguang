<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/student.Master" AutoEventWireup="true" CodeBehind="microClass.aspx.cs" Inherits="Rc.Cloud.Web.student.microClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript" src="../Scripts/json2.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/jq.pagination.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/jquery-jtemplates.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--二级菜单-->
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="teachingPlan.aspx">已购买教案</a></li>
            <li><a href="allTeachingPlan.aspx">全部教案</a></li>
            <li><a href="microClass.aspx" class="active">已购买微课件</a></li>
            <li><a href="allMicroClass.aspx">全部微课件</a></li>
            <li><a href="teaching.aspx">已购买习题集</a></li>
            <li><a href="allTeaching.aspx">全部习题集</a></li>
        </ul>
    </div>

    <!--内容-->
    <div class="iframe-container">
        <div class="iframe-sidebar">
            <asp:Literal ID="litTree" ClientIDMode="Static" runat="server"></asp:Literal>
        </div>

        <!-- 右侧内容 -->
        <div class="iframe-main">
            <div class="container-fluid section_container">
                <div class="panel panel-default table_list">
                    <table class="table table-bordered table-hover text-center">
                        <thead>
                            <tr>
                                <td class="text-left">名称</td>
                                <td>大小</td>
                                <td>日期</td>
                                <td>操作</td>
                            </tr>
                        </thead>
                        <tbody id="tbRes">
                        </tbody>
                    </table>
                </div>
                <!-- 翻页 -->
                <div class="pagination_container">
                    <ul class="pagination">
                    </ul>
                </div>

            </div>
        </div>
    </div>

    <!-- 右侧数据列表 -->
    <textarea id="template_Res" style="display: none">
        {#foreach $T.list as record}
            <tr>
                <td class="text-left">{$T.record.docName}</td>
                <td>{$T.record.docSize}</td>
                <td>{$T.record.docTime}</td>
                <td>
                    <a href="../teacher/FileView.aspx?ResourceToResourceFolder_Id={$T.record.docId}&Resource_Class=<%=strResource_Class %>" target="_blank" data-name="Preview">预览</a>
                    {#if $T.record.IsDownload==true}
                    <a href="<%= Rc.Cloud.Web.Common.pfunction.GetResourceHost("TeachingPlanWebSiteUrl")  %>/common/downLoadFile.aspx?iid={$T.record.docId}">下载</a>
                    {#/if}
                </td>
            </tr>
        {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">
        function ShowSubDocument(strResourceFolder_Id, strIType) {
            ResourceFolder_Id = strResourceFolder_Id;
            itype = strIType;
            pageIndex = 1;
            loadData();
        }
        var loadData = function () {
            var dto = {
                ResourceFolder_Id: ResourceFolder_Id,
                DocName: docName,
                IType: itype,// 1学科，2资源
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("microClass.aspx/GetCloudResource", JSON.stringify(dto), function (data) {
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
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        $(function () {
            pageIndex = 1;
            ResourceFolder_Id = "";
            docName = "";
            itype = "";//itype 1学科，2资源
            //loadData();
            $(".tree a:eq(0)").click();
        });

    </script>
</asp:Content>
