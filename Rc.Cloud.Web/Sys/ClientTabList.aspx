<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="ClientTabList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.ClientTabList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-body">
                    <div class="form-inline search_bar mb">
                        <input type="text" name="txtName" id="txtName" class="form-control input-sm" placeholder="Tab标识/Tab名称" />
                        <asp:DropDownList runat="server" ID="ddlTabType" CssClass="form-control input-sm" ClientIDMode="Static"></asp:DropDownList>
                        <input type="button" value="查询" class="btn btn-default btn-sm" id="btnSearch" />
                        <input type="button" value="新增" class="btn btn-default btn-sm" onclick="edit('');" />
                        <input type="button" value="标签对应关系维护" class="btn btn-default" onclick="edit('');" />
                    </div>
                    <table class="table table-hover table-bordered">
                        <thead>
                            <tr class="tr_title">
                                <th>Tab标识</th>
                                <th>Tab名称</th>
                                <th>类型</th>
                                <th>时间</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody id="list"></tbody>
                    </table>
                    <textarea id="listBox" style="display: none;">
                        {#foreach $T.list as record}
                        <tr class="tr_con_002 cc">
                            <td>{$T.record.Tabindex}</td>
                            <td>{$T.record.TabName}</td>
                            <td>{$T.record.TabType}</td>
                            <td>{$T.record.CreateTime}</td>
                            <td>
                                <a href="javascript:edit('{$T.record.Tabindex}');">修改</a>&nbsp;&nbsp;
                                <a href="javascript:del('{$T.record.Tabindex}');">删除</a>
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

            $(document).on("click", "#btnSearch", function () {
                pageIndex = 1;
                loadData();
            });

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
                area: ["450px", "500px"],
                content: "ClientTabEdit.aspx?tId=" + id
            })
        }

        //删除
        function del(id) {
            layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
                var dto = {
                    Tabindex: id,
                    x: Math.random()
                }
                $.ajaxWebService("ClientTabList.aspx/DelData", JSON.stringify(dto), function (data) {
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
                TabName: $("#txtName").val(),
                TabType: $("#ddlTabType").val(),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            }
            $.ajaxWebService("ClientTabList.aspx/GetDataList", JSON.stringify(dto), function (data) {
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
