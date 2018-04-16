<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="classList.aspx.cs" Inherits="Homework.teacher.classList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container">
        <div class="container pt">
            <div class="page_title">我创建的班级</div>
            <div class="school_panel" id="ulClassList"></div>
            <div class="page_title">我加入的班级</div>
            <div class="school_panel" id="ulClassList2"></div>
        </div>
    </div>
    <textarea id="template_Class" class="hidden">
    {#foreach $T.listSelf as record}
        <div class="panel_item">
            <%--{#if $T.record.PGroup_User_ApplicationStatus=='applied'}disabled warning{#/if}
            <div class="item_label">审核中</div>--%>
            {#if $T.record.IsGroupOwner==true}
            <div class="item_control dropdown">
                <i class="material-icons" data-toggle="dropdown"></i>
                <ul class="dropdown-menu">
                    {#if $T.record.IsGroupOwner==true && $T.record.PGroup_User_ApplicationStatus=='passed' && $T.record.PGroupId!=''}
                    <li><a href="##" onclick="javascript:QuitGrade('{$T.record.PGroupId}','{$T.record.UserGroup_Id}','{$T.record.User_ID}','{$T.record.User_Name}');">退出年级</a></li>
                    <li><a href="##" onclick="classDisband('{$T.record.UserGroup_Id}','{$T.record.PGroupId}');">解散</a></li>
                    {#else}
                    <li><a href="##" onclick="classDisband('{$T.record.UserGroup_Id}','');">解散</a></li>
                    {#/if}
                </ul>
            </div>
            {#/if}
            <div class="item_heading">
                <div class="name"><a href="classInfo.aspx?ugroupId={$T.record.UserGroup_Id}" title="{$T.record.UserGroup_Name}">{$T.record.UserGroup_Name}</a></div>
                <ul class="info">
                    <li>创建人：{$T.record.User_Name}</li>
                    <li>班级号：{$T.record.UserGroup_Id}</li>
                    <li>年　级：
                        {#if $T.record.PGroup_User_ApplicationStatus==''}
                        <i class="material-icons add-hook" data-url="EntryGrade.aspx?reqGroupId={$T.record.UserGroup_Id}" val="{$T.record.UserGroup_Id}" data-title="加入新年级" title="加入">&#xE147;</i>
                        {#elseif $T.record.PGroup_User_ApplicationStatus=='applied'}
                        {$T.record.PGroupId}(审核中)
                        {#else}
                        {$T.record.PGroupName}({$T.record.PGroupId})
                        {#/if}
                    </li>
                </ul>
            </div>
            <div class="item_body">
                {$T.record.UserGroup_BriefIntroduction}
            </div>
            <div class="item_footer">
                <ul>
                    <li><a href="classMember.aspx?ugroupId={$T.record.UserGroup_Id}">{$T.record.MemberCount} 成员</a></li>
                    <li><a href="classNotice.aspx?ugroupId={$T.record.UserGroup_Id}">{$T.record.MesCount} 消息</a></li>
                </ul>
            </div>
        </div>
    {#/for}
    </textarea>
    <textarea id="template_Class2" class="hidden">
    {#foreach $T.listJoin as record}
            <div class="panel_item {#if $T.record.JoinApplicationStatus=='applied' && $T.record.JoinStatus=='1'}disabled warning{#/if} {#if $T.record.JoinApplicationStatus=='passed' && $T.record.JoinStatus=='1'}disabled danger{#/if}">
                {#if $T.record.JoinApplicationStatus=='applied' && $T.record.JoinStatus=='1'}
                <div class="item_label">审核中</div>
                {#/if}
                {#if $T.record.JoinApplicationStatus=='passed' && $T.record.JoinStatus=='1'}
                <div class="item_label">已退出</div>
                {#/if}
                <div class="item_heading">
                    <div class="name"><a href="classInfo.aspx?ugroupId={$T.record.UserGroup_Id}" title="{$T.record.UserGroup_Name}">{$T.record.UserGroup_Name}</a></div>
                    <ul class="info">
                        <li>创建人：{$T.record.User_Name}</li>
                        <li>班级号：{$T.record.UserGroup_Id}{#if $T.record.JoinApplicationStatus=='applied'}(正在等待审核){#elseif $T.record.JoinStatus=='1'}(已退出){#/if}</li>
                        <li>年　级：
                            {#if $T.record.PGroup_User_ApplicationStatus==''}
                            未加入年级
                            {#elseif $T.record.PGroup_User_ApplicationStatus=='applied'}
                            {$T.record.PGroupId}(审核中)
                            {#else}
                            {$T.record.PGroupName}({$T.record.PGroupId})
                            {#/if}
                        </li>
                    </ul>
                </div>
                <div class="item_body">
                    {$T.record.UserGroup_BriefIntroduction}
                </div>
                <div class="item_footer">
                    <ul>
                        <li><a href="classMember.aspx?ugroupId={$T.record.UserGroup_Id}">{$T.record.MemberCount} 成员</a></li>
                        <li><a href="classNotice.aspx?ugroupId={$T.record.UserGroup_Id}">{$T.record.MesCount} 消息</a></li>
                    </ul>
                </div>
            </div>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            loadData();

            //加入
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

            //创建
            $(document).on('click', '.create-hook', function (e) {
                var url = $(this).data('url');
                var title = $(this).data('title');
                layer.open({
                    type: 2,
                    title: title,
                    area: ['580px', '500px'],
                    content: url
                });
                e.preventDefault();
            })
        });

        var classDisband = function (classId, gradeId) {
            var index = layer.confirm("确认解散么？解散以后，班级无法恢复，请谨慎操作!", { icon: 4, title: '提示' }, function () {
                layer.close(index);//关闭确认弹窗
                $.ajaxWebService("classList.aspx/ClassDisband", "{ClassId:'" + classId + "',GradeId:'" + gradeId + "',x:'" + Math.random() + "'}", function (data) {
                    if (data.d == "1") {
                        layer.msg('解散成功', { icon: 1, time: 1000 }, function () {
                            window.location.reload();
                        })
                        return false;
                    }
                    if (data.d == "2") {
                        layer.msg('班有成员，解散失败！', { icon: 2 });
                        return false;
                    }
                    else {
                        layer.msg('解散失败！', { icon: 2 });
                        return false;
                    }
                }, function () {
                    layer.msg('解散失败！', { icon: 2 });
                    return false;
                }, false);
            });
        }
        var QuitGrade = function (pugroupId, ugroupId, userId, userName) {
            var index = layer.confirm("确定要退出年级吗？", { icon: 4, title: '提示' }, function () {
                layer.close(index);//关闭确认弹窗
                $.ajaxWebService("classList.aspx/QuitGrade", "{PugroupId:'" + pugroupId + "',UgroupId:'" + ugroupId + "',UserId:'" + userId + "',UserName:'" + userName + "',x:'" + Math.random() + "'}", function (data) {
                    //alert(data.d); return;
                    if (data.d == "1") {
                        layer.msg('退出年级成功', { icon: 1, time: 1000 }, function () {
                            window.location.reload();
                            //loadData();
                        })
                        return false;
                    }
                    else {
                        layer.msg('退出年级失败！', { icon: 2 });
                        return false;
                    }
                }, function () {
                    layer.msg('退出年级失败err！', { icon: 2 });
                    return false;
                }, false);
            });
        }
        var loadData = function () {
            var dto = {
                x: Math.random()
            };
            $("#ulClassList").html("");
            $("#ulClassList2").html("");
            $.ajaxWebService("classList.aspx/GetClassList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#ulClassList").setTemplateElement("template_Class", null, { filter_data: false });
                    $("#ulClassList").processTemplate(json);

                    $("#ulClassList2").setTemplateElement("template_Class2", null, { filter_data: false });
                    $("#ulClassList2").processTemplate(json);
                }
                $("#ulClassList").append('<div class="panel_item"><div class="item_add create-hook" data-url="CreateClass.aspx" data-title="创建新班级"><i class="material-icons">&#xE147;</i><p>创建新班级</p></div></div>');
                $("#ulClassList2").append('<div class="panel_item"><div class="item_add add-hook" data-url="JoinClass.aspx" data-title="加入新班级"><i class="material-icons">&#xE147;</i><p>加入新班级</p></div></div>');
            }, function () { }, false);
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
    </script>
</asp:Content>
