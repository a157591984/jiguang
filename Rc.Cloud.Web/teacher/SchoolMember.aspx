<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="SchoolMember.aspx.cs" Inherits="Rc.Cloud.Web.teacher.SchoolMember" %>

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
                                    data-url="SchoolMember.aspx?ugroupId=<%#Eval("UserGroup_Id") %>">
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
                <li><a href="SchoolData.aspx?ugroupId=<%=ugroupId %>">资料</a></li>
                <li class="active"><a href="SchoolMember.aspx?ugroupId=<%=ugroupId %>">成员</a></li>
                <li><a href="SchoolVerifyNotice.aspx?ugroupId=<%=ugroupId %>">消息</a></li>
            </ul>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <td>身份</td>
                        <td class="align_left">名称</td>
                        <td>班数</td>
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
        <td class="align_left">{$T.record.UserGroup_Name}{#if $T.record.IType=='1'}({$T.record.UserGroup_Id}){#else}({$T.record.UserName}){#/if}</td>
        <td>{$T.record.ClassCount}</td>
        <td>{$T.record.TeacherCount}</td>
        <td>{$T.record.StudentCount}</td>
        <td>{$T.record.User_ApplicationPassTime}</td>
        <td>{#if $T.record.UserStatus=='0'}<span class="text-success">正常</span>{#elseif $T.record.UserStatus=='1'}<span class="text-danger">已退出</span>{#/if}</td>
        <td class="table_opera">
            {#if $T.record.IType=='1'}
            <a href='##' data-name="GradeNums" data-title="{$T.record.UserGroup_Name}({$T.record.UserGroup_Id})" val="{$T.record.UserGroup_Id}">年级详情</a>
            {#/if}
            {#if $T.record.IsGroupOwner==true}
                {#if $T.record.MembershipEnum=='principal'}
                    <span class="text-muted disabled">不可操作</span>
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
            //全部班级
            $(document).on({
                "click": function () {
                    var LayerTitle = $(this).attr('data-title');
                    layer.open({
                        type: 2,
                        title: LayerTitle,
                        area: ['1000px', '90%'],
                        content: 'GradeNums.aspx?ugroupId=' + $(this).attr("val")
                    })
                }
            }, '[data-name="GradeNums"]');

            _ugroupId = "<%=ugroupId%>";

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
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                x: Math.random()
            };
            $.ajaxWebService("SchoolMember.aspx/GetMember", JSON.stringify(dto), function (data) {
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
                $.ajaxWebService("SchoolMember.aspx/DelClassMember", JSON.stringify(dto), function (data) {
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
                $.ajaxWebService("SchoolMember.aspx/RecoveryClassMember", JSON.stringify(dto), function (data) {
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
