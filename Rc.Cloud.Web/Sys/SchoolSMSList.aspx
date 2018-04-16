<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SchoolSMSList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SchoolSMSList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-body">
                    <div class="form-inline search_bar mb">
                        <input type="button" id="btnAdd" value="新增" class="btn btn-primary btn-sm" />
                        <input type="text" id="txtName" class="form-control input-sm" placeholder="学校标识/学校名称" />
                        <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
                    </div>
                    <table class="table table-hover table-bordered">
                        <thead>
                            <tr>
                                <th width="8%">学校标识</th>
                                <th>学校名称</th>
                                <th width="8%">短信条数</th>
                                <th width="25%">说明</th>
                                <th width="15%">操作</th>
                            </tr>
                        </thead>
                        <tbody id="tb1">
                        </tbody>
                    </table>
                    <textarea id="template_ConfigSchool" class="hidden">
                        {#foreach $T.list as record}
                            <tr>
                                <td>{$T.record.School_Id}</td>
                                <td>{$T.record.School_Name}</td>
                                <td>{$T.record.SMSCount}</td>
                                <td>{$T.record.Remark}</td>
                                <td class="opera">
                                    <a href="javascript:;" onclick="UpdateData('{$T.record.School_Id}')" >新增收件人</a>
                                    <a href="javascript:;" onclick="UpdateData('{$T.record.School_Id}')" >编辑</a>
                                    <a href="javascript:;" onclick="DeleteData('{$T.record.School_Id}')" >删除</a>
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
            $("#btnAdd").click(function () {
                layer.ready(function () {
                    layer.open({
                        type: 2,
                        title: '增加',
                        fix: false,
                        area: ["450px", "450px"],
                        content: "SchoolSMSAdd.aspx"
                    })
                });
            })
        })
        var loadData = function () {
            var dto = {
                Name: $.trim($("#txtName").val()),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("SchoolSMSList.aspx/GetDataList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template_ConfigSchool", null, { filter_data: false });
                    $("#tb1").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tb1").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
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
        function UpdateData(School_Id) {

            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '编辑',
                    fix: false,
                    area: ["85%", "85%"],
                    content: "SchoolSMSEdit.aspx?School_Id=" + School_Id
                })
            });
        }
       
            //删除
            function DeleteData(School_Id) {
                layer.ready(function () {
                    layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
                        $.ajaxWebService("SchoolSMSList.aspx/DeleteData", "{School_Id:'" + School_Id + "',x:" + Math.random() + "}", function (data) {
                            if (data.d == "1") {
                                layer.msg("删除成功", { icon: 1, time: 2000 }, function () { loadData(); });
                            }
                            else {
                                layer.msg("删除失败", { icon: 2, time: 2000 });
                            }
                        }, function () { })
                    });
                })
            }

    </script>
</asp:Content>
