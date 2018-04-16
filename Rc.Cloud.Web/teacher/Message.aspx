<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="Rc.Cloud.Web.teacher.Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container">
        <div class="container pt">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th width="5">
                            <input type="checkbox" data-name="CheckAll" data-mark="id" />
                        </th>
                        <th width="150">发送人</th>
                        <th>消息内容</th>
                        <th width="150">发送时间</th>
                        <th width="100">状态</th>
                        <%--<th width="100">操作</th>--%>
                    </tr>
                </thead>
                <tbody id="tbHW"></tbody>
            </table>
            <div class="pagination_container">
                <button type="button" class="btn btn-default pull-left" data-name="batchMark">标记已读</button>
                <ul class="pagination"></ul>
            </div>
        </div>
    </div>
    <textarea id="template_HW" class="hidden">
        {#foreach $T.list as record}
            <tr>
                <td>{#if $T.record.MsgStatus=='Unread'}<label><input type="checkbox" name="id[]" value="{$T.record.MsgId}" /></label>{#/if}</td>
                <td>{$T.record.MsgSenderName}</td>
                <td>{$T.record.MsgTitle}</td>
                <td>{$T.record.CreateTime}</td>
                <td>{$T.record.MsgStatusText}</td>
                <td class="hidden">
                    {#if $T.record.MsgStatus=='Unread'}
                    <a href='##' onclick="changeIsRead('{$T.record.MsgId}','1');">标记已读</a>
                    {#elseif $T.record.MsgStatus=='Read'}
                    <%--<a href='##' onclick="changeIsRead('{$T.record.MsgId}','0');">标记未读</a>--%>
                    <font color="red">不可操作</font>
                    {#/if}</td>
            </tr>
        {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            loadData();
            $('[data-name="batchMark"]').click(function () {
                var arrChk = getCheckedInputValue();
                if (arrChk.length == 0) {
                    layer.msg("没有选中任何数据，请重新选择", { time: 1000, icon: 4 });
                    return false;
                }
                changeIsRead(arrChk.join(","), "1");
            });
        });
        function getCheckedInputValue() {
            var arrChk = new Array();
            $('#tbHW input[name^="id"]').each(function () {
                if ($(this).is(":checked")) {
                    arrChk.push($(this).val());
                }
            });
            return arrChk;
        }
        function loadData() {
            var dto = {
                MsgAccepter: "<%=userId%>",
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("Message.aspx/GetMessageList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbHW").setTemplateElement("template_HW", null, { filter_data: false });
                    $("#tbHW").processTemplate(json);
                    $(".pagination_container").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                    $('[data-name="batchMark"]').show();
                }
                else {
                    $("#tbHW").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
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
        function changeIsRead(MsgId, IsRead) {
            //var _msg = "您确定要标记为已读吗？";
            //if (IsRead == "0") {
            //    _msg = "您确定要标记为未读吗？";
            //}
            //layer.confirm(_msg, {
            //    btn: ['确定', '取消'] //按钮
            //}, function () {
            var dto = {
                MsgId: MsgId,
                x: Math.random()
            };
            $.ajaxWebService("Message.aspx/ChangeIsRead", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    layer.msg('标记成功', { icon: 1, time: 1000 }, function () { window.location.reload(); });
                    //loadData();

                }
                else {
                    layer.msg('标记失败', { icon: 2, time: 2000 });
                }
            }, function () { layer.msg('标记失败', { icon: 2, time: 2000 }); });
            //});
        }
    </script>
</asp:Content>
