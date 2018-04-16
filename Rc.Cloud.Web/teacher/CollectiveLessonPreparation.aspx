<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="CollectiveLessonPreparation.aspx.cs" Inherits="Rc.Cloud.Web.teacher.CollectiveLessonPreparation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container pa">
        <div class="filter">
            <div class="form-inline">
                <div class="form-group">
                    <label>备课名称：</label>
                    <input type="text" id="txtName" maxlength="200" class="form-control input-sm">
                </div>
                <input type="button" id="btnSearch" value="查询" class="btn btn-primary btn-sm">
                <a href="javascript:;" onclick="Add()" class="btn btn-info btn-sm">发起备课</a>
            </div>
            <div class="filter_section">
                <div class="filter_row clearfix">
                    <span class="row_name">年级：</span>
                    <div class="row_item" data-name="rowItem">
                        <ul ajax-name="Grade">
                            <asp:Literal ID="ltlGrade" runat="server" ClientIDMode="Static"></asp:Literal>
                        </ul>
                    </div>
                </div>
                <div class="filter_row clearfix">
                    <span class="row_name">学科：</span>
                    <div class="row_item">
                        <ul ajax-name="Subject">
                            <asp:Literal ID="ltlSubject" runat="server" ClientIDMode="Static"></asp:Literal>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <table class="table table-bordered">
            <thead>
                <tr>
                    <th>备课名称</th>
                    <th width="100">年级</th>
                    <th width="100">学科</th>
                    <th width="100">负责人数</th>
                    <th width="120">已上传文件数</th>
                    <th width="120">计划开始时间</th>
                    <th width="120">计划结束时间</th>
                    <th width="125">创建人</th>
                    <th width="150">修改阶段状态</th>
                    <th width="125">操作</th>
                </tr>
            </thead>
            <tbody id="tb1">
            </tbody>
        </table>
        <div class="pagination_container">
            <ul class="pagination">
            </ul>
        </div>
    </div>
    <textarea id="template_1" class="hidden">
    {#foreach $T.list as record}
    <tr>
        <td>{$T.record.ResourceFolder_Name}</td>
        <td>{$T.record.GradeName}</td>
        <td>{$T.record.Subject}</td>
        <td>{#if $T.record.PersonCount>0 } <a href="javascript:;" class="btn btn-primary btn-xs" title="点击查看" onclick="show('{$T.record.ResourceFolder_Id}')">{$T.record.PersonCount}</a>{#else}0{#/if}</td>
        <td>{#if $T.record.FilesCount>0 } <a href="javascript:;"  class="btn btn-primary btn-xs" title="点击查看" onclick="showFiles('{$T.record.ResourceFolder_Id}','{$T.record.ResourceFolder_Name}')">{$T.record.FilesCount}</a>{#else}0{#/if}</td>
        <td>{$T.record.StartTime}</td>
        <td>{$T.record.EndTime}</td>
        <td>{$T.record.UserName}</td>
        <td>
            <div class="form-inline">
                <select class="form-control input-sm" {#if $T.record.flag=='true'}{#else}disabled title="您不是创建人无法进行修改"{#/if} onchange="OperateReAuth('{$T.record.ResourceFolder_Id}',this);">
                    <option value="准备阶段" {#if $T.record.stage=='准备阶段'}selected{#/if}>准备阶段</option>
                    <option value="分析阶段" {#if $T.record.stage=='分析阶段'}selected{#/if}>分析阶段</option>
                    <option value="创造阶段" {#if $T.record.stage=='创造阶段'}selected{#/if}>创造阶段</option>
                    <option value="提高阶段" {#if $T.record.stage=='提高阶段'}selected{#/if}>提高阶段</option>
                    <option value="总结阶段" {#if $T.record.stage=='总结阶段'}selected{#/if}>总结阶段</option>
                     <option value="完成备课" {#if $T.record.stage=='完成备课'}selected{#/if}>完成备课</option>
                </select>
            </div>
        </td>
        <td>
            {#if $T.record.flag=="true"}
            <a href="PrepareLessons.aspx?ResourceFolder_Id={$T.record.ResourceFolder_Id}"  target="_blank">编辑</a>
            <a href="javascript:;" onclick="Delete('{$T.record.ResourceFolder_Id}')">删除</a>
            {#else}
            <%--<a href="javascript:;" class="disabled" title="您不是创建人无法进行编辑" >编辑</a>
            <a href="javascript:;" class="disabled" title="您不是创建人无法进行删除">删除</a>--%>
            <a href="PrepareLessons_view.aspx?ResourceFolder_Id={$T.record.ResourceFolder_Id}"  target="_blank">查看</a>
            {#/if}
        </td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            $('#btnSearch').click(function () {
                pageIndex = 1;
                loadData();
            })
            $(document).keydown(function (e) {
                if (e.keyCode == 13) {
                    $('#btnSearch').click();
                    return false;
                }
            })
            //筛选
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
            loadData();
        })
        function loadData() {
            var _subjectId = $('[ajax-name="Subject"]').find('a.active').attr('ajax-value');
            var _gradeId = $('[ajax-name="Grade"]').find('a.active').attr('ajax-value');
            var dto = {
                KeyName: $("#txtName").val(),
                SubjectID: _subjectId == undefined ? "" : _subjectId,
                GradeID: _gradeId == undefined ? "" : _gradeId,
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("CollectiveLessonPreparation.aspx/GetList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template_1", null, { filter_data: false });
                    $("#tb1").processTemplate(json);
                    $(".pagination_container").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tb1").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
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
        function Delete(ResourceFolder_Id) {
            layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
                $.ajaxWebService("CollectiveLessonPreparation.aspx/Delete", "{ResourceFolder_Id:'" + ResourceFolder_Id + "',x:" + Math.random() + "}", function (data) {
                    if (data.d == "1") {
                        layer.msg("删除成功", { icon: 1, time: 2000 }, function () { loadData(); });
                    }
                    else if (data.d == "2") {
                        layer.msg("删除失败:已上传资源", { icon: 2, time: 2000 });
                    }
                    else {
                        layer.msg("删除失败", { icon: 2, time: 2000 });
                    }
                }, function () { })
            });
        }
        function show(ResourceFolder_Id) {
            layer.open({
                type: 2,
                title: '负责人详情',
                area: ['800px', '90%'],
                content: "PersonDetail.aspx?ResourceFolder_Id=" + ResourceFolder_Id
            });
        }
        function showFiles(ResourceFolder_Id, resTitle) {
            layer.open({
                type: 2,
                title: '已上传文件数',
                area: ['850px', '90%'],
                content: "ResourceToResourceFolder_Show.aspx?ResourceFolder_Id=" + ResourceFolder_Id + "&resTitle=" + resTitle
            });
        }
        function Add() {
            window.open("PrepareLessons.aspx");
        }
        function OperateReAuth(reId, obj) {
            var dto = {
                ReId: reId,
                stage: $(obj).val(),
                x: Math.random()
            }
            $.ajaxWebService("CollectiveLessonPreparation.aspx/OperateResourceAuth", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    layer.ready(function () {
                        layer.msg('操作成功', { icon: 1, time: 1000 })
                    });
                }
            }, function () { }, false);
        }
    </script>
</asp:Content>
