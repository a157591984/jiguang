<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/parent.Master" AutoEventWireup="true" CodeBehind="student.aspx.cs" Inherits="Rc.Cloud.Web.parent.student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--内容-->
    <div class="iframe-container">
        <div class="container ph">
            <a href='javascript:;' class="btn btn-info mb" id="btnShow">添加我家宝贝</a>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>姓名</th>
                        <th>邮箱</th>
                    </tr>
                </thead>
                <tbody id="tbRes">
                </tbody>
            </table>
            <div class="pagination_container">
                <ul class="pagination"></ul>
            </div>
        </div>
    </div>
    <textarea id="template_tb1" class="hidden">
    {#foreach $T.list as record}
      <tr>
            <td>{$T.record.BabyName}</td>
            <td>{$T.record.Email}</td>
        </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1; //页码
            loadParaFromLink();
            loadData();

            $("#btnShow").click(function (e) {
                layer.open({
                    type: 2,
                    title: '添加我家宝贝',
                    closeBtn: 1,
                    area: ['300px', '265px'],
                    content: ['addmybaby.aspx', 'no'] //iframe的url<a href="addmybaby.aspx">addmybaby.aspx</a>
                });
                e.preventDefault();
                e.stopPropagation();
            })
        });
        var loadData = function () {

            var dto = {
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("student.aspx/GetmyBaby", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbRes").setTemplateElement("template_tb1", null, { filter_data: false });
                    $("#tbRes").processTemplate(json);

                    $(".pagination_container").show();
                    $(".pagination_container").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbRes").html("<tr><td class='text-center' colspan=\"100\">暂无数据</td></tr>");

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

        var setBasicUrl = function () {
            basicUrl = "student.aspx?";
            backurl = b.encode(basicUrl + "pageIndex=" + pageIndex);
        }
        var loadParaFromLink = function () {
            pageIndex = getUrlVar("pageIndex") == "" ? 1 : getUrlVar("pageIndex");
        }
    </script>
</asp:Content>
