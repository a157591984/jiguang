<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="MessageTemplateList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.MessageTemplateList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-body">
                    <div class="form-inline search_bar mb">
                        <input type="text" name="txtUserName" id="txtUserName" class="form-control input-sm" placeholder="用户名" />
                        <input type="button" value="查询" class="btn btn-default btn-sm" id="btnSearch" />
                        <input type="button" value="新 增" class="btn btn-default btn-sm" onclick="edit('');" />
                    </div>
                    <table class="table table-hover table-bordered">
                        <thead>
                            <tr class="tr_title">
                                <th>类型</th>
                                <th>用户名</th>
                                <th>密码</th>
                                <th width="30%">URL/KEY</th>
                                <th>是否启用</th>
                                <th>时间</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody id="list"></tbody>
                    </table>
                    <textarea id="listBox" style="display: none;">
                        {#foreach $T.list as record}
                        <tr class="tr_con_002 cc">
                            <td>{$T.record.STypeName}</td>
                            <td>{$T.record.UserName}</td>
                            <td>{$T.record.PassWord}</td>
                            <td>{$T.record.MsgUrl}</td>
                            <td>{$T.record.IsStart}</td>
                            <td>{$T.record.CTime}</td>
                            <td>
                                <a href="javascript:edit('{$T.record.SendSMSTemplateId}');">修改</a>&nbsp;&nbsp;
                                <a href="javascript:del('{$T.record.SendSMSTemplateId}');">删除</a>
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

            $("#btnSearch").on({
                click: function () {
                    pageIndex = 1;
                    loadData();
                }
            })
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
                area: ["750px", "520px"],
                content: "MessageTemplateEdit.aspx?tId=" + id
            })
        }

        //删除
        function del(id) {
            layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
                var dto = {
                    SendSMSTemplateId: id,
                    x: Math.random()
                }
                $.ajaxWebService("MessageTemplateList.aspx/DelData", JSON.stringify(dto), function (data) {
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
                UserName: $("#txtUserName").val(),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            }
            $.ajaxWebService("MessageTemplateList.aspx/GetDataList", JSON.stringify(dto), function (data) {
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
