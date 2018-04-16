<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="DictRelation_List.aspx.cs" Inherits="Rc.Cloud.Web.Sys.DictRelation_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ScriptManager>
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-body">
                    <div class="form-inline search_bar mb">
                        <input type="button" value="新增字典关系" class="btn btn-primary btn-sm" onclick="Dict_Add();" runat="server" id="btnAdd" />
                    </div>
                    <table class="table table-hover table-bordered">
                        <thead>
                            <tr>
                                <th>数据字典名称</th>
                                <th>对应关系名称</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody id="tb1"></tbody>
                    </table>
                    <hr />
                    <div class="page"></div>
                </div>
            </div>
        </div>
    </div>
    <textarea id="template1" class="hidden">
        {#foreach $T.list as record}
        <tr class="tr_con_001">
            <td>{$T.record.HeadName}</td>
            <td>{$T.record.SonName}</td>
                <td class="opera">
                    {#if $T.record.CountDict>0}
                    <a href="javascript:;" class="disabled">删除</a>
                    <a href="javascript:;" class="disabled">编辑</a>
                    {#else}
                    <a href="javascript:DeleteItem('{$T.record.DictRelation_Id}');">删除</a>
                    <a href="javascript:;Edit('{$T.record.DictRelation_Id}')">编辑</a>
                    {#/if}
                    <a href="javascript:;AddDetail('{$T.record.HeadDict_Id}','{$T.record.SonDict_Id}','{$T.record.DictRelation_Id}')">添加关系明细</a>
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
            $("#ddlD_Type").change(function () {
                pageIndex = 1;
                loadData();
            })
        })
        function DeleteItem(DictRelation_Id) {
            layer.ready(function () {
                layer.confirm('确定要删除关系？', { icon: 3 }, function () {
                    var dto = {
                        DictRelation_Id: DictRelation_Id,
                        x: Math.random()
                    };
                    $.ajaxWebService("DictRelation_List.aspx/DeleteItem", JSON.stringify(dto), function (data) {

                        if (data.d == "1") {
                            layer.msg('删除成功', { icon: 1, time: 1000 }, function () {
                                loadData();
                            })
                        }
                        else {
                            layer.msg('删除失败', { icon: 2 });
                        }
                    }, function () { });
                });
            });
        }
        function Dict_Add() {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '新增字典关系',
                    area: ['450px', '300px'],
                    content: 'DictRelation_Add.aspx'
                });
            });
        }
        function Edit(Id) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '编辑字典关系',
                    area: ['450px', '300px'],
                    content: 'DictRelation_Edit.aspx?Id=' + Id
                });
            });
        }
        function AddDetail(HeadDict_Id, SonDict_Id, DictRelation_Id) {
            window.location.href = "DictRelationDetail_List.aspx?HeadDict_Id=" + HeadDict_Id + "&SonDict_Id=" + SonDict_Id + "&DictRelation_Id=" + DictRelation_Id;
        }
        var loadData = function () {
            var dto = {
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("DictRelation_List.aspx/GetList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template1", null, { filter_data: false });
                    $("#tb1").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tb1").html("<tr class='tr_con_002'><td colspan='100' align='center'>暂无数据</td></tr>");
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
    </script>
</asp:Content>
