<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SchoolIFList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SchoolIFList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-body">
                    <div class="form-inline search_bar mb">
                        <input type="button" value="新增" class="btn btn-primary btn-sm" onclick="UpdateData('');" />
                        <input type="text" id="txtKey" class="form-control input-sm" placeholder="配置标识" />
                        <input type="text" id="txtName" class="form-control input-sm" placeholder="名称" />
                        <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
                    </div>
                    <table class="table table-hover table-bordered">
                        <thead>
                            <tr>
                                <th width="8%">配置标识</th>
                                <th width="10%">配置名称</th>
                                <th>学校</th>
                                <th>备注</th>
                                <th width="12%">操作</th>
                            </tr>
                        </thead>
                        <tbody id="tbSchoolIF">
                        </tbody>
                    </table>
                    <textarea id="template_SchoolIF" class="hidden">
                        {#foreach $T.list as record}
                            <tr>
                                <td>{$T.record.SchoolIF_Code}</td>
                                <td>{$T.record.SchoolIF_Name}</td>
                                <td>{$T.record.SchoolName}（{$T.record.SchoolId}）</td>
                                <td>{$T.record.Remark}</td>
                                <td class="opera">
                                    <a href="javascript:;" onclick="UpdateData('{$T.record.SchoolIF_Id}')" >编辑</a>
                                    <a href="javascript:;" onclick="DeleteData('{$T.record.SchoolIF_Id}')" >删除</a>
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
        var loadData = function () {
            var dto = {
                Key: $("#txtKey").val(),
                Name: $.trim($("#txtName").val()),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("SchoolIFList.aspx/GetDataList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbSchoolIF").setTemplateElement("template_SchoolIF", null, { filter_data: false });
                    $("#tbSchoolIF").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbSchoolIF").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
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
        function UpdateData(SchoolIF_Id) {
            if (SchoolIF_Id == "") {
                title = "新增";
            } else {
                title = "编辑"
            }
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: title,
                    fix: false,
                    area: ["400px", "450px"],
                    content: "SchoolIFEdit.aspx?SchoolIF_Id=" + SchoolIF_Id
                })
            });
        }
        //增加联系人AddPerson

        function AddPerson(School_Id) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '增加联系人',
                    fix: false,
                    area: ["85%", "85%"],
                    content: "AddSchoolSMS_Person.aspx?School_Id=" + School_Id
                })
            });
        }
        //删除
        function DeleteData(SchoolIF_Id) {
            layer.ready(function () {
                layer.confirm("确定要删除吗？", { icon: 4, title: "删除提示" }, function () {
                    $.ajaxWebService("SchoolIFList.aspx/DeleteData", "{SchoolIF_Id:'" + SchoolIF_Id + "',x:" + Math.random() + "}", function (data) {
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

        //初始化用户数据
        function HandleUserData(SchoolCode, SchoolId) {
            var dto = {
                SchoolCode: SchoolCode,
                SchoolId: SchoolId,
                x: Math.random()
            };
            layer.ready(function () {
                layer.confirm("确定要初始化用户数据吗？", { icon: 4, title: "提示" }, function () {
                    $.ajaxWebService("SchoolIFList.aspx/HandleUserData", JSON.stringify(dto), function (data) {
                        if (data.d == "1") {
                            layer.msg("操作成功", { icon: 1, time: 2000 }, function () { loadData(); });
                        }
                        else {
                            layer.msg("操作失败", { icon: 2, time: 2000 });
                        }
                    }, function () { })
                });
            })
        }
    </script>
</asp:Content>
