<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="FUserList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.FUserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pa">
        <%=siteMap %>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <input type="button" runat="server" clientidmode="static" visible="false" value="新增" class="btn btn-primary btn-sm" id="btnAdd" onclick="edit('');" />
                    <input type="text" id="txtUserName" class="form-control input-sm" placeholder="登录名/用户名" />
                    <input type="button" runat="server" clientidmode="static" visible="false" value="查询" class="btn btn-default btn-sm" id="btnSearch" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>登录名</th>
                            <th>用户名</th>
                            <th>职务</th>
                            <th>性别</th>
                            <th>E-mail</th>
                            <th>手机</th>
                            <th>学科</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody id="list">
                    </tbody>
                </table>
                <textarea id="listBox" class="hidden">
                    {#foreach $T.list as record}
                    <tr>
                        <td>{$T.record.UserName}</td>
                        <td>{$T.record.TrueName}</td>
                        <td>{$T.record.UserPostName}</td>
                        <td>{$T.record.Sex}</td>
                        <td>{$T.record.Email}</td>
                        <td>{$T.record.Mobile}</td>
                        <td>{$T.record.SubjectName}</td>
                        <td class="opera">
                            <%--<a href="javascript:changepwd('{$T.record.UserId}');">修改密码</a>
                            <a href="javascript:Update('{$T.record.UserId}');">修改账号</a>
                            <a href="javascript:Show('{$T.record.UserId}');">查看</a>
                            <a href="javascript:del('{$T.record.UserId}');">删除</a>--%>
                            {$T.record.Operate}
                        </td>
                    </tr>
                    {#/for}
                </textarea>
                <hr />
                <div class="page"></div>
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
            $(document).keydown(function (e) {
                if (e.keyCode == 13) {
                    $('#btnSearch').click();
                    return false;
                }
            })
        })
        //查看
        function Show(id) {
            layer.open({
                type: 2,
                title: '查看',
                fix: false,
                area: ["80%", "50%"],
                content: "FUserShow.aspx?UserId=" + id
            })
        }
        //编辑
        function edit(id) {
            if (id == "") {
                title = "新增用户";
            } else {
                title = "修改用户"
            }
            layer.open({
                type: 2,
                title: title,
                fix: false,
                area: ["650px", "505px"],
                content: "FUserListEdit.aspx?UserId=" + id
            })
        }
        function changepwd(id) {
            layer.open({
                type: 2,
                title: '修改密码',
                fix: false,
                area: ["385px", "250px"],
                content: "changepwd.aspx?UserId=" + id
            })
        }
        function Update(id) {
            layer.open({
                type: 2,
                title: '修改帐号',
                fix: false,
                area: ["400px", "320px"],
                content: "FUser_Edit.aspx?UserId=" + id
            })
        }
        //删除
        function del(id) {
            layer.confirm("确定要删除吗？", { icon: 4, title: "删除提示" }, function () {
                $.get("../Ajax/SysAjax.aspx", { key: "DelFUser", UserId: id, x: Math.random() }, function (data) {
                    if (data) {
                        layer.msg("删除成功!", { icon: 1, time: 2000 }, function () {
                            loadData();
                        });
                    } else {
                        layer.msg("删除失败!", { icon: 2, time: 2000 }, function () {
                            loadData();
                        });
                    }
                })
            })
        }

        function loadData() {
            var dto = {
                UserName: $("#txtUserName").val(),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            }
            $.ajaxWebService("FUserList.aspx/GetFUserList", JSON.stringify(dto), function (data) {
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
            }, function () { })
        }
        function pageselectCallback(page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
    </script>
</asp:Content>
