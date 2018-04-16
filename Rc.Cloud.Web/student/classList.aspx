<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/student.Master" AutoEventWireup="true" CodeBehind="classList.aspx.cs" Inherits="Homework.student.classList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container">
        <div class="container pt">
            <div class="school_panel" id="ulClassList"></div>
        </div>
    </div>
    <textarea id="template_Class" class="hidden">
    {#foreach $T.list as record}
        <div class="panel_item {#if $T.record.User_ApplicationStatus=='applied' && $T.record.UserStatus=='1'}disabled warning{#/if} {#if $T.record.User_ApplicationStatus=='passed' && $T.record.UserStatus=='1'}disabled danger{#/if}">
            {#if $T.record.User_ApplicationStatus=='applied' && $T.record.UserStatus=='1'}
            <div class="item_label">审核中</div>
            {#/if}
            {#if $T.record.User_ApplicationStatus=='passed' && $T.record.UserStatus=='1'}
            <div class="item_label">已退出</div>
            {#/if}
            <div class="item_heading">
                <div class="name"><a href="classInfo.aspx?ugroupId={$T.record.UserGroup_Id}" title="{$T.record.UserGroup_Name}">{$T.record.UserGroup_Name}</a></div>
                <ul class="info">
                    <li>创建人：{$T.record.User_Name}</li>
                    <li>班级号：{$T.record.UserGroup_Id}</li>
                </ul>
            </div>
            <div class="item_body">
                {$T.record.UserGroup_BriefIntroduction}
            </div>
            <div class="item_footer">
                <ul>
                    <li><a href="classMember.aspx?ugroupId={$T.record.UserGroup_Id}">{$T.record.MemberCount} 成员</a></li>
                </ul>
            </div>
        </div>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script lang="javascript" type="text/javascript">
        var loadData = function () {
            var dto = {
                userId: "<%=userId%>",
                x: Math.random()
            };
            $("#ulClassList").html("");
            $.ajaxWebService("classList.aspx/GetClassList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#ulClassList").setTemplateElement("template_Class", null, { filter_data: false });
                    $("#ulClassList").processTemplate(json);
                }
                $("#ulClassList").append('<div class="panel_item"><div class="item_add add-hook" data-url="addClass.aspx" data-title="加入新班级"><i class="material-icons">&#xE147;</i><p>加入新班级</p></div></div>');
            }, function () { }, false);
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        $(function () {
            loadData();

            $(document).on('click', '.add-hook', function (e) {
                var url = $(this).data('url');
                var title = $(this).data('title');
                layer.open({
                    type: 2,
                    title: title,
                    area: ['340px', '340px'],
                    content: url
                });
                e.preventDefault();
            })
        });

    </script>
</asp:Content>
