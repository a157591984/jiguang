<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="ConfigSchoolList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.ConfigSchoolList" %>

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
                        <input type="text" id="txtKey" class="form-control input-sm" placeholder="标识" />
                        <input type="text" id="txtName" class="form-control input-sm" placeholder="名称" />
                        <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
                    </div>
                    <table class="table table-hover table-bordered">
                        <thead>
                            <tr>
                                <th width="8%">配置标识</th>
                                <th width="10%">学校</th>
                                <th>名称</th>
                                <th width="8%">对内URL</th>
                                <th width="8%">对外URL</th>
                                <%--<th width="8%">学校的公网IP</th>
                                <th>类型</th>--%>
                                <th width="5%">排序</th>
                                <th>说明</th>
                                <th width="25%">操作</th>
                            </tr>
                        </thead>
                        <tbody id="tbConfigSchool">
                        </tbody>
                    </table>
                    <textarea id="template_ConfigSchool" class="hidden">
                        {#foreach $T.list as record}
                            <tr>
                                <td>{$T.record.ConfigEnum}</td>
                                <td>{$T.record.School_Name}</td>
                                <td>{$T.record.D_Name}</td>
                                <td>{$T.record.D_Value}</td>
                                <td>{$T.record.D_PublicValue}</td>
                                <%--<td>{$T.record.SchoolIP}</td>
                                <td>{$T.record.D_TypeName}</td>--%>
                                <td>{$T.record.D_Order}</td>
                                <td>{$T.record.D_Remark}</td>
                                <td class="opera">
                                    <a href="javascript:;" onclick="UpdateData('{$T.record.ConfigEnum}')" >编辑</a>
                                    <a href="javascript:;" onclick="DeleteData('{$T.record.ConfigEnum}')" >删除</a>
                                    <a href="javascript:;" onclick="AddPerson('{$T.record.School_Id}')" >添加联系人</a>
                                    <a href="javascript:;" onclick="BookAttr('{$T.record.School_Id}')" >自有资源权限</a>
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
            $.ajaxWebService("ConfigSchoolList.aspx/GetDataList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbConfigSchool").setTemplateElement("template_ConfigSchool", null, { filter_data: false });
                    $("#tbConfigSchool").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbConfigSchool").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
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
        function UpdateData(ConfigEnum) {
            if (ConfigEnum == "") {
                title = "新增";
            } else {
                title = "编辑"
            }
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: title,
                    fix: false,
                    area: ["650px", "600px"],
                    content: "ConfigSchoolEdit.aspx?ConfigEnum=" + ConfigEnum
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
        function DeleteData(ConfigEnum) {
            layer.ready(function () {
                layer.confirm("确定要删除吗？", { icon: 4, title: "删除提示" }, function () {
                    $.ajaxWebService("ConfigSchoolList.aspx/DeleteData", "{ConfigEnum:'" + ConfigEnum + "',x:" + Math.random() + "}", function (data) {
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
        // 学校自有资源权限控制
        function BookAttr(School_Id) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '自有资源权限控制',
                    fix: false,
                    area: ["400px", "400px"],
                    content: "ConfigSchool_BookAttr.aspx?SchoolId=" + School_Id
                })
            });
        }
    </script>
</asp:Content>
