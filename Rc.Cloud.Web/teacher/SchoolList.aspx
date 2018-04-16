<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="SchoolList.aspx.cs" Inherits="Rc.Cloud.Web.teacher.SchoolList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container">
        <div class="container pt">
            <div class="school_panel" id="ulSchoolList"></div>
        </div>
    </div>
    <textarea id="template_Class" class="hidden">
    {#foreach $T.list as record}
        <div class="panel_item {#if $T.record.JoinApplicationStatus=='applied' && $T.record.JoinStatus=='1'}disabled warning{#/if} {#if $T.record.JoinApplicationStatus=='passed' && $T.record.JoinStatus=='1'}disable danger{#/if}">
            {#if $T.record.User_ApplicationStatus=='applied' && $T.record.UserStatus=='1'}
            <div class="item_label">审核中</div>
            {#/if}
            {#if $T.record.User_ApplicationStatus=='passed' && $T.record.UserStatus=='1'}
            <div class="item_label">已退出</div>
            {#/if}
            {#if $T.record.IsGroupOwner==true}
            <div class="item_control">
                <i class="material-icons" data-toggle="dropdown"></i>
                <ul class="dropdown-menu">
                    <li><a href="##" onclick="classDisband('{$T.record.UserGroup_Id}');">解散</a></li>
                </ul>
            </div>
            {#/if}
            <div class="item_heading">
                <div class="name"><a href="SchoolData.aspx?ugroupId={$T.record.UserGroup_Id}" title="{$T.record.UserGroup_Name}">{$T.record.UserGroup_Name}</a></div>
                <ul class="info">
                    <li>创建人：{$T.record.User_Name}</li>
                    <li>学校号：{$T.record.UserGroup_Id}{#if $T.record.JoinApplicationStatus=='applied'}(审核中){#elseif $T.record.JoinStatus=='1'}(已退出){#/if}</li>
                </ul>
            </div>
            <div class="item_body">
                {$T.record.UserGroup_BriefIntroduction}
            </div>
            <div class="item_footer">
                <ul>
                    <li><a href="SchoolMember.aspx?ugroupId={$T.record.UserGroup_Id}">{$T.record.MemberCount} 成员</a></li>
                    <li><a href="SchoolVerifyNotice.aspx?ugroupId={$T.record.UserGroup_Id}">{$T.record.MesCount} 消息</a></li>
                </ul>
            </div>
        </div>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(function () {
            loadData();

            //加入新学校
            $(document).on('click', '.add-hook', function (e) {
                var url = $(this).data('url');
                var title = $(this).data('title');
                layer.open({
                    type: 2,
                    title: title,
                    area: ['340px', '350px'],
                    content: url
                });
                e.preventDefault();
            })

            //创建新学校
            $(document).on('click', '.create-hook', function (e) {
                var url = $(this).data('url');
                var title = $(this).data('title');
                layer.open({
                    type: 2,
                    title: title,
                    area: ['580px', '425px'],
                    content: url
                });
                e.preventDefault();
            })
        });

        var loadData = function () {
            var dto = {
                UserId: "<%=userId%>",
                x: Math.random()
            };
            $("#ulGradeList").html("");
            $.ajaxWebService("SchoolList.aspx/GetSchoolList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#ulSchoolList").setTemplateElement("template_Class", null, { filter_data: false });
                    $("#ulSchoolList").processTemplate(json);
                }
                if ("<%=IsCreateSchool%>" == "1") $("#ulSchoolList").append('<div class="panel_item"><div class="item_add create-hook" data-url="CreateSchool.aspx" data-title="创建新学校"><i class="material-icons">&#xE147;</i><p>创建新学校</p></div></div>');
                if ("<%=IsJoinSchool%>" == "1") $("#ulSchoolList").append('<div class="panel_item"><div class="item_add add-hook" data-url="TeacherJoinSchool.aspx" data-title="加入新学校"><i class="material-icons">&#xE147;</i><p>加入新学校</p></div></div>');
            }, function () { }, false);
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }

        var classDisband = function (classId) {
            var index = layer.confirm("确认解散么？解散以后，学校无法恢复，请谨慎操作！", { icon: 4, title: '提示' }, function (e) {
                layer.close(index);//关闭确认弹窗
                $.ajaxWebService("SchoolList.aspx/ClassDisband", "{ClassId:'" + classId + "',x:'" + Math.random() + "'}", function (data) {
                    //alert(data.d); return;
                    if (data.d == "1") {
                        layer.msg('解散成功', { icon: 1, time: 1000 }, function () {
                            window.location.reload();
                            //loadData();
                        })
                        return false;
                    }
                    if (data.d == "2") {
                        layer.msg('学校有成员，解散失败！', { icon: 2 });
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
                e.preventDefault();
            });
        }
    </script>
</asp:Content>
