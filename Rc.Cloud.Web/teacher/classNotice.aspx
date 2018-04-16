<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="classNotice.aspx.cs" Inherits="Homework.teacher.classNotice" %>

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
                                    data-url="classNotice.aspx?ugroupId=<%#Eval("UserGroup_Id") %>">
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
                <li><a href="classMember.aspx?ugroupId=<%=ugroupId %>">成员</a></li>
                <li class="active"><a href="classNotice.aspx?ugroupId=<%=ugroupId %>">消息</a></li>
            </ul>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <td width="5%" class="text-center">
                            <label>
                                <input type="checkbox" data-name="CheckAll" data-mark="id" /></label></td>
                        <td width="10%">姓名</td>
                        <td>验证消息</td>
                        <td class="text-center" width="15%">申请时间</td>
                        <td width="10%" class="text-center">操作</td>
                    </tr>
                </thead>
                <tbody id="tbNotice">
                </tbody>
            </table>
            <div class="pagination_container">
                <div class="mt pull-left">
                    <button type="button" class="btn btn-primary" style="display: none;" data-name="batchAgree">批量接受</button>
                    <button type="button" class="btn btn-primary" style="display: none;" data-name="batchRefuse">批量拒绝</button>
                </div>
                <ul class="pagination">
                </ul>
            </div>
        </div>
    </div>
    <textarea id="template_Mes" class="hidden">
        {#foreach $T.list as record}
            <tr>
                <td><label><input type="checkbox" name="id[]" value="{$T.record.UserGroup_Member_Id}" /></label></td>
                <td>{$T.record.UserName}</td>
                <td>{$T.record.User_ApplicationReason}</td>
                <td>{$T.record.User_ApplicationTime}</td>
                <td>
                    {#if $T.record.IsGroupOwner==true}
                        {#if $T.record.User_ApplicationStatus=='applied'}<a href='##' onclick="auditApplyData('1','{$T.record.UserGroup_Member_Id}');">接受</a>
                        <a href='##' onclick="auditApplyData('2','{$T.record.UserGroup_Member_Id}');">拒绝</a>{#/if}
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
            $.ajaxWebService("classNotice.aspx/GetClassNotice", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbNotice").setTemplateElement("template_Mes", null, { filter_data: false });
                    $("#tbNotice").processTemplate(json);
                    $(".pagination_container").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                    if ("<%=bool_IsGroupOwner%>" != "False") {
                        $('[data-name="batchAgree"]').show();
                        $('[data-name="batchRefuse"]').show();
                    }
                }
                else {
                    $("#tbNotice").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
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
        $(function () {
            pageIndex = 1;
            loadData();

            $('[data-name="batchAgree"]').click(function () {
                var arrChk = getCheckedInputValue();
                if (arrChk.length == 0) {
                    layer.msg("没有选中任何数据，请重新选择", { time: 1000, icon: 2 });
                    return false;
                }
                auditApplyData("1", arrChk.join(","));
            });
            $('[data-name="batchRefuse"]').click(function () {
                var arrChk = getCheckedInputValue();
                if (arrChk.length == 0) {
                    layer.msg("没有选中任何数据，请重新选择", { time: 1000, icon: 2 });
                    return false;
                }
                auditApplyData("2", arrChk.join(","));
            });

        });

        var getCheckedInputValue = function () {
            var arrChk = new Array();
            $('#tbNotice input[name^="id"]').each(function () {
                if ($(this).is(":checked")) {
                    arrChk.push($(this).val());
                }
            });
            return arrChk;
        }

        var auditApplyData = function (tp, ugmId) {
            var _msg = "您确定要接受吗？";
            if (tp == "2") _msg = "您确定要拒绝吗？";
            layer.confirm(_msg, {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var dto = {
                    UserGroup_Member_Id: ugmId,
                    AType: tp,
                    x: Math.random()
                };
                $.ajaxWebService("classNotice.aspx/AuditApplyData", JSON.stringify(dto), function (data) {
                    if (data.d == "1") {
                        layer.msg('操作成功！', { icon: 1, time: 1000 }, function () { window.location.reload(); });
                        //loadData();
                    }
                    else {
                        layer.msg('操作失败！', { icon: 2, time: 2000 });
                    }
                }, function () { layer.msg('操作失败！', { icon: 2, time: 2000 }); });
            });
        }

    </script>
</asp:Content>
