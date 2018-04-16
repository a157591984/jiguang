<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/student.Master" AutoEventWireup="true" CodeBehind="allOrder.aspx.cs" Inherits="Rc.Cloud.Web.student.allOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container">
        <div class="container ph">
            <div class="personal_center_panel">
                <div class="panel_left">
                    <div class="sidebar_menu_panel">
                        <div class="panel_heading">
                            个人中心
                        </div>
                        <div class="panel_body">
                            <ul>
                                <li><a href="basicSetting.aspx">基本设置</a></li>
                                <li><a href="safeSetting.aspx">安全设置</a></li>
                                <li class="active"><a href="allOrder.aspx">我的订单</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="panel_right">
                    <div class="order_panel">
                        <div class="panel_heading">
                            <div class="panel_title">我的订单</div>
                            <ul class="panel_control">
                                <li class="active"><a href="allOrder.aspx">全部订单</a></li>
                                <li><a href="waitPay.aspx">待付款</a></li>
                                <li><a href="waitRate.aspx">已评价</a></li>
                            </ul>
                        </div>
                        <div class="div panel_body">
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>名称</th>
                                        <th width="80">资源类型</th>
                                        <th width="60">价格</th>
                                        <th width="60">实付款</th>
                                        <th width="100">交易状态</th>
                                        <th width="175">时间</th>
                                        <th width="90">操作</th>
                                    </tr>
                                </thead>
                                <tbody id="tb1">
                                </tbody>
                            </table>
                            <div class="pagination_container">
                                <ul class="pagination">
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <textarea id="template_1" class="hidden">
    {#foreach $T.list as record}
        <tr>
            <td><a href="/teacher/TeachingplanShow.aspx?ResourceFolder_ID={$T.record.Book_Id}&Type=1" target="_blank">{$T.record.BookName}</a></td>
             <td>{$T.record.Resource_Type}</td>
            <td>{$T.record.Book_Price}</td>
            <td>{$T.record.UserOrder_Amount}</td>
            <td>{$T.record.UserOrder_Status}</td>
            <td>{$T.record.UserOrder_FinishTime}</td>
            <td class="opera">
                {#if $T.record.Status==1}
                <a href="payment.aspx?orderType=2&rid={$T.record.Book_Id}">立即付款</a>
                <a href="javascirpt:;" onclick="CancelOrder('{$T.record.UserOrder_Id}')">取消订单</a>
                {#elseif $T.record.Status==3}
                    {#if $T.record.CommentCount>0}
                    <a href="javascirpt:;">已评价</a>
                    {#else}
                    <a href='##' data-name="rate" val="{$T.record.UserOrder_No}">去评价</a>
                    {#/if}
                {#elseif $T.record.Status==4}
                <a href="javascirpt:;"><span class="text-danger">已取消</span></a>
                 {#else}
               <a href="/teacher/TeachingplanShow.aspx?ResourceFolder_ID={$T.record.Book_Id}" target="_blank">查看</a>
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

            //评价
            $(document).on('click', '[data-name="rate"]', function () {
                var order_num = $(this).attr("val");
                layer.open({
                    type: 2,
                    title: '评价',
                    area: ['600px', '350px'],
                    content: 'rate.aspx?order_num=' + order_num
                });
            });
        })
        var loadData = function () {
            var dto = {
                userid: "<%=userid%>",
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("allOrder.aspx/GetOrderList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template_1", null, { filter_data: false });
                    $("#tb1").processTemplate(json);
                    $(".pagination_container").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tb1").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
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
        var CancelOrder = function (Order_id) {
            var index = layer.confirm("确定要取消此订单吗？", { icon: 4, title: '提示' }, function () {
                layer.close(index);//关闭确认弹窗
                layer.load();
                $.ajaxWebService("allOrder.aspx/CancelOrder", "{Order_id:'" + Order_id + "',x:'" + Math.random() + "'}", function (data) {
                    if (data.d == "1") {
                        layer.msg('此订单已取消', { icon: 1, time: 1000 }, function () {
                            loadData();
                        })
                        return false;
                    }
                    else {
                        layer.msg('失败！', { icon: 2 });
                        return false;
                    }
                }, function () {
                    layer.msg('失败！', { icon: 2 });
                    return false;
                });
            });
        }
    </script>
</asp:Content>
