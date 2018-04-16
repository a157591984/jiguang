<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/student.Master" AutoEventWireup="true" CodeBehind="classMember.aspx.cs" Inherits="Homework.student.classMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            _ugroupId = "<%=ugroupId%>";
            $(".sub_nav ul li a[val='" + _ugroupId + "']").addClass("active");
        });
    </script>
    <link href="../Styles/style001/Tree.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container">
        <div class="iframe-sidebar">
            <div class="mtree mtree-class-hook">
                <ul>
                    <asp:Repeater runat="server" ID="rptClass">
                        <ItemTemplate>
                            <li>
                                <div class="mtree_link mtree-link-hook <%#GetStyle(Eval("UserGroup_Id").ToString()) %>" data-url="classMember.aspx?ugroupId=<%#Eval("UserGroup_Id") %>">
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
                <li><a href="classInfo.aspx?ugroupId=<%=ugroupId %>">资料</a></li>
                <li class="active"><a href="classMember.aspx?ugroupId=<%=ugroupId %>">成员</a></li>
            </ul>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <td width="5%">学号</td>
                        <td class="text-left">姓名</td>
                        <td>身份</td>
                        <td width="15%">邮箱</td>
                        <td width="15%">加入时间</td>
                        <td width="18%">状态</td>
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
    <textarea id="template_Member" style="display: none">
    {#foreach $T.list as record}
    <tr>
        <td>{$T.record.Row}</td>
        <td class="text-left">{$T.record.TrueName}</td>
        <td>{$T.record.MembershipEnumName}</td>
        <td>{$T.record.Email}</td>
        <td>{$T.record.User_ApplicationPassTime}</td>
        <td>{#if $T.record.UserStatus=='0'}<font color="green">正常</font>{#elseif $T.record.UserStatus=='1'}<font color="red">已退班</font>{#/if}</td>
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
            }, function () { }, false);
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }

    </script>
</asp:Content>
