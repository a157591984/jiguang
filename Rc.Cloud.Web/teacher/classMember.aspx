<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="classMember.aspx.cs" Inherits="Homework.teacher.classMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            _ugroupId = "<%=ugroupId%>";
            $(".sub_nav ul li a[val='" + _ugroupId + "']").addClass("active");
        });
    </script>
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
                                    data-url="classMember.aspx?ugroupId=<%#Eval("UserGroup_Id") %>">
                                    <div class="mtree_indent mtree-indent-hook"></div>
                                    <div class="mtree_btn mtree-btn-hook"></div>
                                    <div class="mtree_name mtree-name-hook"><%#Rc.Cloud.Web.Common.pfunction.GetSubstring(Eval("UserGroup_Name").ToString(),16,false) %></div>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="iframe-main pa">
            <ul class="nav nav-tabs mb">
                <li><a href="classInfo.aspx?ugroupId=<%=ugroupId %>">资料</a></li>
                <li class="active"><a href="classMember.aspx?ugroupId=<%=ugroupId %>">成员</a></li>
                <li><a href="classNotice.aspx?ugroupId=<%=ugroupId %>">消息</a></li>
            </ul>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <td>姓名(登录名)</td>
                        <td>身份</td>
                        <td width="25%">邮箱</td>
                        <td width="15%">加入时间</td>
                        <td width="10%">状态</td>
                        <td width="18%">操作</td>
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
                <td>{$T.record.TrueName}({$T.record.UserName})</td>
                <td>{$T.record.MembershipEnumName}</td>
                <td>{$T.record.Email}</td>
                <td>{$T.record.User_ApplicationPassTime}</td>
                <td>
                    {#if $T.record.UserStatus=='0'}
                    <span class="text-success">正常</span>
                    {#elseif $T.record.UserStatus=='1'}
                    <span class="text-danger">已退班</span>
                    {#/if}
                </td>
                <td>
                    {#if $T.record.IsGroupOwner==true}
                        <a href='##' onclick="resettingPwd('{$T.record.User_Id}');">重置密码</a>
                        {#if $T.record.UserStatus=='0'}
                            {#if $T.record.MembershipEnum=='headmaster'}

                            {#elseif $T.record.MembershipEnum!='headmaster'}
                            <a href='##' onclick="delClassMember('{$T.record.UserGroup_Member_Id}','{$T.record.User_Id}');">退班</a>
                            {#/if}
                        {#/if}
                        {#if $T.record.UserStatus=='1'}
                            <a href='##' onclick="recoveryClassMember('{$T.record.UserGroup_Member_Id}');">恢复</a>
                        {#/if}
                        {#if $T.record.MembershipEnum=='teacher'}
                            <a href='##' onclick="setHeadmaster('{$T.record.UserGroup_Member_Id}','{$T.record.userGroupId}','{$T.record.User_Id}')">设为班主任</a>
                        {#/if}
                    {#else}
                    
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
            loadData();
            $('.mtree-class-hook').mtree({
                onClick: function (obj) {
                    window.location.href = obj.data('url');
                }
            });
        });
        var loadData = function () {
            var dto = {
                UserGroup_Id: _ugroupId,
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("classMember.aspx/GetClassMember", JSON.stringify(dto), function (data) {
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

        var resettingPwd = function (userId) {
            var _msg = "您确定要重置此成员的密吗？如果重置密码成功后,密码将更改为123456.";
            layer.confirm(_msg, {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var dto = {
                    UserId: userId,
                    x: Math.random()
                };
                $.ajaxWebService("classMember.aspx/resettingPwd", JSON.stringify(dto), function (data) {
                    if (data.d == "1") {
                        layer.msg('重置密码成功,密码更改为123456.', { icon: 1, time: 1000 });
                        loadData();
                    }
                    else if (data.d = "2") {
                        layer.msg('重置密码失败', { icon: 2, time: 2000 });
                    }
                    else {
                        layer.msg('重置密码失败', { icon: 2, time: 2000 });
                    }
                }, function () { layer.msg('重置密码失败err', { icon: 2, time: 2000 }); }, false);
            });
        }
        var delClassMember = function (ugmId, userId) {
            var _msg = "您确定要退班该成员吗？";
            layer.confirm(_msg, {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var dto = {
                    UserGroup_Member_Id: ugmId,
                    UserId: userId,
                    UserGroup_Id: _ugroupId,
                    x: Math.random()
                };
                $.ajaxWebService("classMember.aspx/DelClassMember", JSON.stringify(dto), function (data) {
                    if (data.d == "1") {
                        layer.msg('退班成功', { icon: 1, time: 1000 });
                        loadData();
                    }
                    else if (data.d = "2") {
                        layer.msg('成员已布置作业,退班失败', { icon: 2, time: 2000 });
                    }
                    else {
                        layer.msg('退班失败', { icon: 2, time: 2000 });
                    }
                }, function () { layer.msg('退班失败err', { icon: 2, time: 2000 }); }, false);
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
                $.ajaxWebService("classMember.aspx/RecoveryClassMember", JSON.stringify(dto), function (data) {
                    if (data.d == "1") {
                        layer.msg('恢复成功', { icon: 1, time: 1000 });
                        loadData();
                    }
                    else {
                        layer.msg('恢复失败', { icon: 2, time: 2000 });
                    }
                }, function () { layer.msg('恢复失败err', { icon: 2, time: 2000 }); }, false);
            });
        }

        var setHeadmaster = function (userGroupMemberId, userGroupId, userId) {
            layer.confirm('确定设为班主任？', { icon: 4 }, function () {
                var data = {
                    userGroupMemberId: userGroupMemberId,
                    userGroupId: userGroupId,
                    userId: userId,
                    x: Math.random()
                }
                $.ajaxWebService("classMember.aspx/setHeaderMaster", JSON.stringify(data), function (data) {
                    if (parseInt(data.d) > 0) {
                        layer.msg('设置成功', { icon: 1, time: 1000 });
                        loadData();
                    } else {
                        layer.msg('设置失败', { icon: 2, time: 2000 });
                    }
                });
            })
        }
    </script>
</asp:Content>
