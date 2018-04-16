<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="GradeList.aspx.cs" Inherits="Rc.Cloud.Web.teacher.GradeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var classDisband = function (classId, schoolId) {
            var index = layer.confirm("确认解散么？解散以后，年级无法恢复，请谨慎操作!", { icon: 4, title: '提示' }, function () {
                layer.close(index);//关闭确认弹窗
                $.ajaxWebService("GradeList.aspx/ClassDisband", "{ClassId:'" + classId + "',SchoolId:'" + schoolId + "',x:'" + Math.random() + "'}", function (data) {
                    //alert(data.d); return;
                    if (data.d == "1") {
                        layer.msg('解散成功', { icon: 1, time: 1000 }, function () {
                            window.location.reload();
                            //loadData();
                        })
                        return false;
                    }
                    if (data.d == "2") {
                        layer.msg('年级有成员，解散失败！', { icon: 2 });
                        return false;
                    }
                    else {
                        layer.msg('解散失败！', { icon: 2 });
                        return false;
                    }
                }, function () {
                    layer.msg('解散失败！', { icon: 2 });
                    return false;
                });
            });
        }
        var QuitGrade = function (pugroupId, ugroupId, userId, userName) {
            var index = layer.confirm("确定要退出学校吗", { icon: 4, title: '提示' }, function () {
                layer.close(index);//关闭确认弹窗
                $.ajaxWebService("GradeList.aspx/QuitSchool", "{PugroupId:'" + pugroupId + "',UgroupId:'" + ugroupId + "',UserId:'" + userId + "',UserName:'" + userName + "',x:'" + Math.random() + "'}", function (data) {
                    //alert(data.d); return;
                    if (data.d == "1") {
                        layer.msg('退出学校成功', { icon: 1, time: 1000 }, function () {
                            window.location.reload();
                            //loadData();
                        })
                        return false;
                    }
                    else {
                        layer.msg('退出学校失败！', { icon: 2 });
                        return false;
                    }
                }, function () {
                    layer.msg('退出学校失败err！', { icon: 2 });
                    return false;
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container">
        <div class="container ph">
            <div class="school_panel" id="ulGradeList">
            </div>
        </div>
    </div>
    <textarea id="template_Class" class="hidden">
    {#foreach $T.list as record}
            <div class="panel_item {#if $T.record.PGroup_User_ApplicationStatus=='applied'}disabled warning{#elseif $T.record.PGroup_UserStatus=='1'}disabled danger{#/if}">
                {#if $T.record.PGroup_User_ApplicationStatus=='applied'}
                <div class="item_label">审核中</div>
                {#elseif $T.record.PGroup_UserStatus=='1'}
                <div class="item_label">已退出</div>
                {#/if}
                {#if $T.record.IsGroupOwner==true}
                <div class="item_control dropdown">
                    <i class="material-icons" data-toggle="dropdown"></i>
                    <ul class="dropdown-menu">
                            {#if $T.record.PGroup_User_ApplicationStatus=='passed'&&$T.record.PGroup_UserStatus=='0'&&$T.record.PGroupId!=''}
                            <li><a href="##" onclick="QuitGrade('{$T.record.PGroupId}','{$T.record.UserGroup_Id}','{$T.record.User_ID}','{$T.record.User_Name}');">退出学校</a></li>
                            <li><a href="##" onclick="classDisband('{$T.record.UserGroup_Id}','{$T.record.PGroupId}');">解散</a></li>
                        {#else}
                            <li><a href="##" onclick="classDisband('{$T.record.UserGroup_Id}','');">解散</a></li>
                        {#/if}
                    </ul>
                </div>
                {#/if}
                <div class="item_heading">
                    <div class="name"><a href="GradeData.aspx?ugroupId={$T.record.UserGroup_Id}" title="{$T.record.UserGroup_Name}">{$T.record.UserGroup_Name}</a></div>
                    <ul class="info">
                        <li>创建人：{$T.record.User_Name}</li>
                        <li>年级号：{$T.record.UserGroup_Id}{#if $T.record.JoinApplicationStatus=='applied'}(正在等待审核)
                            {#elseif $T.record.JoinStatus=='1'}(已退出){#/if}</li>
                        <li>学　校：
                            {#if $T.record.IsGroupOwner==true&&$T.record.PGroupId==''}
                            <i class="material-icons add-hook" data-url="JoinSchool.aspx?reqGroupId={$T.record.UserGroup_Id}" data-title="加入新学校">&#xE147;</i>
                            {#elseif $T.record.PGroup_User_ApplicationStatus==''}
                            还未加入学校
                            {#elseif $T.record.PGroup_User_ApplicationStatus=='applied'}
                            {$T.record.PGroupId}(审核中)
                            {#elseif $T.record.PGroup_UserStatus=='1'}
                            {$T.record.PGroupId}(已退出)
                            {#else}
                            <a href="##" title="{$T.record.PGroupName}({$T.record.PGroupId})">{$T.record.PGroupName}({$T.record.PGroupId})</a>
                            {#/if}
                        </li>
                    </ul>
                </div>
                <div class="item_body">
                    {$T.record.UserGroup_BriefIntroduction}
                </div>
                <div class="item_footer">
                    <ul>
                        <li><a href="GradeVerifyNotice.aspx?ugroupId={$T.record.UserGroup_Id}">{$T.record.MesCount} 消息</a></li>
                        <li><a href="GradeMember.aspx?ugroupId={$T.record.UserGroup_Id}">{$T.record.MemberCount} 成员</a></li>
                    </ul>
                </div>
            </div>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">

        var loadData = function () {
            var dto = {
                x: Math.random()
            };
            $("#ulGradeList").html("");
            $.ajaxWebService("GradeList.aspx/GetGradeList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#ulGradeList").setTemplateElement("template_Class", null, { filter_data: false });
                    $("#ulGradeList").processTemplate(json);
                }
                <%--if ("<%=IsCreateGrade%>" == "1") $("#ulGradeList").append("<div class='col-xs-3'><div class='unit class_unit create_unit text-center'><a href='createGrade.aspx' class='text-muted'><h1><i class='glyphicon glyphicon-plus'></i></h1><p>创建年级</p></a></div></div>");
                if ("<%=IsJoinGrade%>" == "1") $("#ulGradeList").append("<div class='col-xs-3'><div class='unit class_unit join_unit text-center'><a href=\"##\" data-name=\"TeacherJoinGrade\" class='text-muted'><h1><i class='glyphicon glyphicon-plus'></i></h1><p>加入年级</p></a></div></div>");--%>
                if ("<%=IsCreateGrade%>" == "1") $("#ulGradeList").append('<div class="panel_item"><div class="item_add create-hook" data-url="CreateGrade.aspx" data-title="创建新年级"><i class="material-icons">&#xE147;</i><p>创建新年级</p></div></div>');
                if ("<%=IsJoinGrade%>" == "1") $("#ulGradeList").append('<div class="panel_item"><div class="item_add add-hook" data-url="TeacherJoinGrade.aspx" data-title="加入新年级"><i class="material-icons">&#xE147;</i><p>加入新年级</p></div></div>');
            }, function () { }, false);
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        $(function () {
            loadData();

            //加入新年级
            $(document).on('click', '.add-hook', function (e) {
                var title = $(this).data('title');
                var url = $(this).data('url');
                layer.open({
                    type: 2,
                    title: title,
                    area: ['340px', '340px'],
                    content: url
                });
                e.preventDefault();
            });

            //创建新年级
            $(document).on('click', '.create-hook', function (e) {
                var title = $(this).data('title');
                var url = $(this).data('url');
                layer.open({
                    type: 2,
                    title: title,
                    area: ['580px', '500px'],
                    content: url
                });
                e.preventDefault();
            });
        });

    </script>
</asp:Content>
