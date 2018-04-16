<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="Res_BookArea.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.Res_BookArea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <asp:DropDownList ID="ddlGradeTerm" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSubject" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlResource_Version" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlResource_Type" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlParticularYear" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtKey" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="书名"></asp:TextBox>
                    <asp:DropDownList runat="server" ID="ddlProvince" CssClass="form-control input-sm" Style="width: auto;" ClientIDMode="Static"></asp:DropDownList>
                    <select id="ddlCity" class="form-control input-sm">
                        <option value="-1">市区</option>
                    </select>
                    <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>年级学期</th>
                            <th>学科</th>
                            <th>教材版本</th>
                            <th>文档类型</th>
                            <th>年份</th>
                            <th width="30%">名称</th>
                            <th>所属地域</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody id="tbRes">
                    </tbody>
                </table>
                <hr />
                <div class="page"></div>
            </div>
        </div>
    </div>

    <textarea id="template_Res" class="hidden">
    {#foreach $T.list as record}
    <tr>
        <td>{$T.record.GradeTerm}</td>
        <td>{$T.record.Subject}</td>
        <td>{$T.record.Resource_Version}</td>
        <td>{$T.record.Resource_Type}</td>
        <td>{$T.record.ParticularYear}</td>
        <td title="{$T.record.docName}">{$T.record.docName}</td>
        <td>{$T.record.AreaInfo}</td>
        <td class="opera">{$T.record.Operate}</td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">
        function showPop(bid, bname) {
            var _srcArea = "Res_BookAreaEdit.aspx?bid=" + bid + "&bname=" + escape(bname);
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '维护地域',
                    offset: '10px',
                    area: ['650px', '380px'],
                    content: _srcArea
                });
            });
        }
        //操作提示，一般直接复制即可
        function Handel(sign, strMessage) {
            layer.ready(function () {
                if (sign == "1") {
                    layer.msg('操作成功', { icon: 1, time: 1000 }, function () {
                        loadData();
                    });
                }
                else {
                    layer.msg('操作失败', { icon: 2 });
                }
            });
        }
        function CloseDialog() {
            layer.closeAll();
        }
        var loadData = function () {
            var strResource_Class = '<%=strResource_Class%>';//资源类别-云资源

            var strResource_Type = $("#ddlResource_Type").val();//文档类型
            var strGradeTerm = $("#ddlGradeTerm").val();//年级学期
            var strSubject = $("#ddlSubject").val();//学科
            var strResource_Version = $("#ddlResource_Version").val();//教材版本
            var strParticularYear = $("#ddlParticularYear").val();//年份
            var dto = {
                DocName: $.trim($("#txtKey").val()),
                strResource_Type: strResource_Type,
                strResource_Class: strResource_Class,
                strGradeTerm: strGradeTerm,
                strSubject: strSubject,
                strResource_Version: strResource_Version,
                strParticularYear: strParticularYear,
                Province: $("#ddlProvince").val(),
                City: $("#ddlCity").val(),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("Res_BookArea.aspx/GetCloudBooks", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbRes").setTemplateElement("template_Res", null, { filter_data: false });
                    $("#tbRes").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbRes").html("<tr class='tr_con_002'><td colspan='100' align='center'>暂无数据</td></tr>");
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

            $(function () {
                pageIndex = 1;
                loadData();

                $("#ddlProvince").change(function () {
                    LoadCity($(this).val());
                });
                $("#btnSearch").click(function () {
                    pageIndex = 1;
                    loadData();
                });

                $("#ddlGradeTerm,#ddlResource_Version,#ddlResource_Type,#ddlSubject,#ddlParticularYear,#ddlCity").change(function () {
                    pageIndex = 1;
                    loadData();
                });

            });
            var LoadCity = function (pid) {
                var dto = {
                    pid: pid,
                    x: Math.random()
                };
                $.ajaxWebService("Res_BookArea.aspx/GetAreaCityInfo", JSON.stringify(dto), function (data) {
                    $("#ddlCity").html(data.d);
                }, function () {
                    $("#ddlCity").html("<option value=\"-1\">市区</option>");
                }, false);

                pageIndex = 1;
                loadData();
            }
    </script>
</asp:Content>
