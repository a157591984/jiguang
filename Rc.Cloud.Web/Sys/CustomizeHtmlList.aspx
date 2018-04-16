﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="CustomizeHtmlList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.CustomizeHtmlList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-body">
                    <div class="form-inline search_bar mb">
                        <input type="button" value="新 增" class="btn btn-primary btn-sm" onclick="edit('');" />
                    </div>
                    <table class="table table-hover table-bordered">
                        <thead>
                            <tr class="tr_title">
                                <th width="80">类型</th>
                                <th>内容</th>
                                <th width="150">时间</th>
                                <th width="100">操作</th>
                            </tr>
                        </thead>
                        <tbody id="list"></tbody>
                    </table>
                    <textarea id="listBox" style="display: none;">
                        {#foreach $T.list as record}
                        <tr class="tr_con_002 cc">
                            <td>{$T.record.HtmlType}</td>
                            <td>{$T.record.HtmlContent}</td>
                            <td>{$T.record.CreateTime}</td>
                            <td>
                                <a href="javascript:edit('{$T.record.CustomizeHtml_Id}');">修改</a>&nbsp;&nbsp;
                                <a href="javascript:del('{$T.record.CustomizeHtml_Id}');">删除</a>
                            </td>
                        </tr>
                        {#/for}
                    </textarea>
                    <hr />
                    <div class="page"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            loadData();

        })

        //编辑
        function edit(id) {
            if (id == "") {
                title = "新增";
            } else {
                title = "修改"
            }
            layer.open({
                type: 2,
                title: title,
                fix: false,
                area: ["75%", "90%"],
                content: "CustomizeHtmlEdit.aspx?tId=" + id
            })
        }

        //删除
        function del(id) {
            layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
                var dto = {
                    CustomizeHtml_Id: id,
                    x: Math.random()
                }
                $.ajaxWebService("CustomizeHtmlList.aspx/DelData", JSON.stringify(dto), function (data) {
                    if (data.d == "1") {
                        layer.msg("删除成功!", { icon: 1, time: 2000 }, function () {
                            loadData();
                        });
                    }
                    else {
                        layer.msg("删除失败!", { icon: 2, time: 2000 }, function () {
                            loadData();
                        });
                    }
                }, function () {
                    layer.msg("删除失败!", { icon: 2, time: 2000 }, function () {
                        loadData();
                    });
                });
            })
        }

        function loadData() {
            var dto = {
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            }
            $.ajaxWebService("CustomizeHtmlList.aspx/GetDataList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#list").setTemplateElement("listBox", null, { filter_data: false });
                    $("#list").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });

                }
                else {
                    $("#list").html("<tr class='tr_con_002'><td colspan='100' align='center'>暂无数据</td></tr>");
                    $(".page").html("");
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
    </script>
</asp:Content>
