<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="cShareTeachPlan.aspx.cs" Inherits="Rc.Cloud.Web.teacher.cShareTeachPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript" src="../Scripts/json2.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/jq.pagination.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/jquery-jtemplates.js"></script>
    <script type="text/javascript" src="../scripts/jQuery_Dialog.js"></script>
    <script type="text/javascript" src="../scripts/jquery.easydrag.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidResourceFolder_ParentId" runat="server" ClientIDMode="Static" Value="-1" />
    <asp:HiddenField ID="hidResourceFolder_Id" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hidResourceFolder_Name" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hidResourceFolder_isLast" runat="server" ClientIDMode="Static" />

    <!--二级菜单-->
    <div class="header_subnav">
        <ul>
            <asp:Literal ID="litSubNaiv" ClientIDMode="Static" runat="server"></asp:Literal>
        </ul>
    </div>

    <!--内容-->
    <div class="main_box clearfix">
        <div class="left_sidebar">
            <asp:Literal ID="litTree" ClientIDMode="Static" runat="server"></asp:Literal>
        </div>

        <!-- 右侧内容 -->
        <div class="right_main_box" data-name="main-auto">
            <div class="filter_box clearfix">
                <dl class="filter_item">
                    <dt>资源类型</dt>
                    <dd>
                        <div class="customize_box">
                            <select id="ddltype" class="select">
                                <option value="1">全部</option>
                                <option value="b8ea8767-4ae6-4b90-883f-93415a14e461">Class类型文件</option>
                                <option value="e3a3e85e-2903-4ae6-ba17-f7f2ad9d7e0d">ScienceWord类型文件</option>
                            </select>
                        </div>
                    </dd>
                </dl>
            </div>
            <div class="container_box table_list">
                <table>
                    <thead>
                        <tr>
                            <td>名称</td>
                            <td class="align_center">大小</td>
                            <td class="align_center">日期</td>
                             <td class="align_center">姓名</td>
                            <td class="align_center">操作</td>
                        </tr>
                    </thead>
                    <tbody id="tbRes">
                    </tbody>
                </table>
                <!-- 翻页 -->
                <div class="page pagination">
                    <ul>
                    </ul>
                </div>

            </div>
        </div>
    </div>

    <!-- 右侧数据列表 -->
    <textarea id="template_Res" style="display: none">
        {#foreach $T.list as record}
            <tr>
                <td>{$T.record.docName}</td>
                <td class="align_center">{$T.record.docSize}</td>
                <td class="align_center">{$T.record.docTime}</td>
                <td class="align_center">{$T.record.TeacherName}</td>
                <td class="align_center table_opera">
                    <a href="FileView.aspx?ResourceToResourceFolder_Id={$T.record.docId}" target="_blank" data-name="Preview">预览</a>
                    {#if $T.record.IsDownload==true}
                    <a href="<%= Rc.Cloud.Web.Common.pfunction.GetResourceHost("TeachingPlanWebSiteUrl")  %>/common/downLoadFile.aspx?iid={$T.record.docId}">下载</a>
                    {#/if}
                    {$T.record.shareUrl}
                </td>
            </tr>
        {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            loadData();
            $("#ddltype").change(function () {
                pageIndex = 1;
                loadData();
            })
        })
        var loadData = function () {
            var Resource_Type = $("#ddltype").val();//资源类别
            var dto = {
                Resource_Type: Resource_Type,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                x: Math.random()
            };
            //console.log(dto);
            $.ajaxWebService("cShareTeachPlan.aspx/GetCloudResource", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbRes").setTemplateElement("template_Res", null, { filter_data: false });
                    $("#tbRes").processTemplate(json);
                    $(".pagination").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbRes").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $(".pagination").find("ul").html("");
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
