<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="FAQList.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.FAQList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-body">
                    <div class="form-inline search_bar mb">
                        <div class="form-group">
                            <input type="button" name="add" id="btnAdd" value="新增" class="btn btn-primary btn-sm" clientidmode="Static">
                            <asp:TextBox ID="txt_title" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                            <input type="submit" name="" id="btnSearch" value="查询" class="btn btn-default btn-sm">
                        </div>
                    </div>
                    <table class="table table-hover table-bordered">
                        <thead>
                            <tr>
                                <th>标题</th>
                                <th>更新时间</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody id="tb"></tbody>
                    </table>
                    <textarea id="template_list" class="hidden">
                    {#foreach $T.list as record}
                    <tr>
                                <td>{$T.record.help_title}</td>
                                <td>{$T.record.create_time}</td>
                                <td class="opera">
                                    <a href="javascript:edit('{$T.record.help_id}');">编辑</a>
                                    <a href="javascript:;del('{$T.record.help_id}')">删除</a>
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
            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();
            })
            $("#btnAdd").click(function () {
                Add();
            })
        })
        var loadData = function () {
            var dto = {
                key: $("#txt_title").val(),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("FAQList.aspx/GetHelpList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb").setTemplateElement("template_list", null, { filter_data: false });
                    $("#tb").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tb").html("<tr class='tr_con_002'><td colspan='100' align='center'>暂无数据</td></tr>");
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
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        //编辑
        function edit(help_id) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '编辑',
                    area: ['850px', '570px'],
                    content: 'FAQEdit.aspx?help_id=' + help_id
                });
            });
        }
        function Add() {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '新增',
                    area: ['850px', '570px'],
                    content: 'FAQEdit.aspx'
                });
            });
        }
        var del = function (help_id) {
            var index = layer.confirm('删除数据？', { icon: 3, title: '提示' }, function () {
                layer.close(index);//关闭确认弹窗
                $.ajaxWebService("FAQList.aspx/delete", "{help_id:'" + help_id + "',x:'" + Math.random() + "'}", function (data) {
                    if (data.d == "1") {
                        layer.msg('删除成功', { icon: 1, time: 1000 }, function () { loadData(); });
                    }
                    if (data.d == "2") {
                        layer.ready(function () {
                            layer.msg('操作失败', { icon: 2 });
                        });
                        return false;
                    }

                }, function () {
                    layer.ready(function () {
                        layer.msg('操作失败', { icon: 2 });
                    });
                    return false;
                }, false);
            });
        }
    </script>
</asp:Content>
