<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="GradeMember.aspx.cs" Inherits="Rc.Cloud.Web.teacher.GradeMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container">
        <div class="iframe-sidebar">
            <div class="mtree mtree-class-hook">
                <ul>
                    <asp:Repeater runat="server" ID="rptClass">
                        <ItemTemplate>
                            <li>
                                <div class="mtree_link mtree-link-hook <%#GetStyle(Eval("UserGroup_Id").ToString()) %>"
                                    data-url="GradeMember.aspx?ugroupId=<%#Eval("UserGroup_Id") %>">
                                    <div class="mtree_indent mtree-indent-hook"></div>
                                    <div class="mtree_btn mtree-btn-hook"></div>
                                    <div class="mtree_name mtree-name-hook"><%#Rc.Cloud.Web.Common.pfunction.GetSubstring(Eval("UserGroup_Name").ToString(),15,false) %></div>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="iframe-main pa">
            <ul class="nav nav-tabs mb">
                <li><a href="GradeData.aspx?ugroupId=<%=ugroupId %>">资料</a></li>
                <li class="active"><a href="GradeMember.aspx?ugroupId=<%=ugroupId %>">成员</a></li>
                <li><a href="GradeVerifyNotice.aspx?ugroupId=<%=ugroupId %>">消息</a></li>
            </ul>
            <table class="table table-bordered text-center">
                <thead>
                    <tr>
                        <td>身份</td>
                        <td class="text-left">名称</td>
                        <td>老师数</td>
                        <td>学生数</td>
                        <td>加入时间</td>
                        <td>状态</td>
                        <td>操作</td>
                    </tr>
                </thead>
                <tbody id="tbMember">
                </tbody>
            </table>
            <div class="pagination_container">
                <ul class="pagination">
                </ul>
            </div>
        </div>
    </div>
    <textarea id="template_Member" class="hidden">
    {#foreach $T.list as record}
    <tr>
        <td>{$T.record.PostName}</td>
        <td class="text-left">{$T.record.UserGroup_Name}{#if $T.record.IType=='1'}({$T.record.UserGroup_Id}){#else}({$T.record.UserName}){#/if}</td>
        <!-- <td data-name="TeacherNums" val="{\$T.record.UserGroup_Id}">{\$T.record.TeacherCount}</td>
        <td data-name="StudentNums" val="{\$T.record.UserGroup_Id}">{\$T.record.StudentCount}</td> -->
        <td>{$T.record.TeacherCount}</td>
        <td>{$T.record.StudentCount}</td>
        <td>{$T.record.User_ApplicationPassTime}</td>
        <td>{#if $T.record.UserStatus=='0'}<span class="text-success">正常</span>{#elseif $T.record.UserStatus=='1'}<span class="text-danger">已退出</span>{#/if}</td>
        <td>
            {#if $T.record.IType=='1'}
            <a href='##' data-name="TeacherNums" val="{$T.record.UserGroup_Id}">班的详情</a>
            {#/if}
            {#if $T.record.IsGroupOwner==true}
                {#if $T.record.MembershipEnum=='gradedirector'}
                    <span style="color:red;">不可操作</span>
                {#else}
                    {#if $T.record.UserStatus=='0'}<a href='##' onclick="delClassMember('{$T.record.UserGroup_Member_Id}','{$T.record.User_Id}');">移除</a>{#/if}
                    {#if $T.record.UserStatus=='1'}<a href='##' onclick="recoveryClassMember('{$T.record.UserGroup_Member_Id}');">恢复</a>{#/if}
                {#/if}
            {#/if}
        </td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            _ugroupId = "<%=ugroupId%>";

            //班的详情
            $(document).on({
                "click": function () {
                    layer.open({
                        type: 2,
                        title: '班的详情',
                        area: ['1000px', '90%'],
                        content: 'TeacherNums.aspx?tp=gradeView&ugroupId=' + $(this).attr("val"),
                    })
                }
            }, '[data-name="TeacherNums"]');

            pageIndex = 1;
            loadData();
            $('.mtree-class-hook').mtree({
                onClick: function (obj) {
                    window.location.href = obj.data('url');
                }
            });
        })

        var loadData = function () {
            var dto = {
                UserGroup_Id: _ugroupId,
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("GradeMember.aspx/GetMember", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbMember").setTemplateElement("template_Member", null, { filter_data: false });
                    $("#tbMember").processTemplate(json);
                    $(".pagination_container").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbMember").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
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

        var delClassMember = function (ugmId, userId) {
            var _msg = "您确定要移除该成员吗？";
            layer.confirm(_msg, {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var dto = {
                    UserGroup_Member_Id: ugmId,
                    UserId: userId,
                    x: Math.random()
                };
                $.ajaxWebService("GradeMember.aspx/DelClassMember", JSON.stringify(dto), function (data) {
                    if (data.d == "1") {
                        layer.msg('移除成功', { icon: 1, time: 1000 });
                        loadData();
                    }
                    else {
                        layer.msg('移除失败', { icon: 2, time: 2000 });
                    }
                }, function () { layer.msg('移除失败err', { icon: 2, time: 2000 }); });
            });
        }
        var recoveryClassMember = function (ugmId) {
            var _msg = "您确定要恢复该成员吗？";
            layer.confirm(_msg, {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var dto = {
                    UserGroup_Member_Id: ugmId,
                    x: Math.random()
                };
                $.ajaxWebService("GradeMember.aspx/RecoveryClassMember", JSON.stringify(dto), function (data) {
                    if (data.d == "1") {
                        layer.msg('恢复成功', { icon: 1, time: 1000 });
                        loadData();
                    }
                    else {
                        layer.msg('恢复失败', { icon: 2, time: 2000 });
                    }
                }, function () { layer.msg('恢复失败err', { icon: 2, time: 2000 }); });
            });
        }
    </script>
</asp:Content>
