<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="BookAuditList.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.BookAuditList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../SysLib/plugin/datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="../SysLib/plugin/datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../SysLib/plugin/datetimepicker/js/locales/bootstrap-datetimepicker.zh-CN.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
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
                    <asp:TextBox ID="txtTime" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="审核日期"></asp:TextBox>
                    <asp:TextBox ID="txtKey" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="名称"></asp:TextBox>
                    <asp:DropDownList ID="ddlBookShelvesState" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="未审核" Value="3"></asp:ListItem>
                        <asp:ListItem Text="已审核" Value="1"></asp:ListItem>
                        <asp:ListItem Text="驳回" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>年级学期</th>
                            <th>学科</th>
                            <th>教材版本</th>
                            <th>文档类型</th>
                            <th width="30%">名称</th>
                            <th>状态</th>
                            <th>审核日期</th>
                            <th>审核人</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody id="tbRes">
                    </tbody>
                </table>
                <textarea id="template_Res" class="hidden">
                {#foreach $T.list as record}
                <tr>
                    <td>{$T.record.GradeTerm}</td>
                    <td>{$T.record.Subject}</td>
                    <td>{$T.record.Resource_Version}</td>
                    <td>{$T.record.Resource_Type}</td>
                    <td>{$T.record.docName}</td>       
                    <td>{$T.record.AuditStateStr}</td>
                    <td>{$T.record.AuditTime}</td>
                    <td>{$T.record.UserName}</td>
                    <td class="opera">
                    {#if $T.record.AuditState==""}
                    <a href="javascript:;" onclick="BookAudit('{$T.record.docId}','{$T.record.docName}',1)">审核</a>
                    <a href="javascript:;" onclick="BookAudit('{$T.record.docId}','{$T.record.docName}',2)">驳回</a>
                    {#elseif  $T.record.AuditState=="0"}
                    <a href="javascript:;" onclick="BookAudit('{$T.record.docId}','{$T.record.docName}',1)">审核</a>
                    {#else }
                    <a href="javascript:;" onclick="BookAudit('{$T.record.docId}','{$T.record.docName}',2)" >驳回</a>
                    {#/if}
                    <a title=""  href="ReList.aspx?resourceFolderId={$T.record.docId}&resTitle={$T.record.GradeTerm}--{$T.record.Subject}--{$T.record.Resource_Version}--{$T.record.Resource_Type}--{$T.record.docName}&type=1" >资源列表</a>
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
        var BookAudit = function (reid, rename, type) {
            layer.ready(function () {
                var index = layer.confirm((type == 1) ? '审核通过？' : '驳回？', { icon: 3 }, function () {
                    layer.close(index);//关闭确认弹窗
                    $.ajaxWebService("BookAuditList.aspx/BookAudit", "{reid:'" + reid + "',type:" + type + ",rename:'" + rename + "',x:'" + Math.random() + "'}", function (data) {
                        if (data.d == "1") {
                            loadData();
                        }
                        if (data.d == "2") {
                            layer.msg('操作失败', { icon: 2 });
                            return false;
                        }

                    }, function () {
                        layer.msg('操作失败！', { icon: 2 });
                        return false;
                    }, false);
                });
            });
        }

        var loadData = function () {
            var strResource_Type = $("#ddlResource_Type").val();//
            var strResource_Class = '<%=strResource_Class%>';//

            var strGradeTerm = $("#ddlGradeTerm").val();//
            var strSubject = $("#ddlSubject").val();//
            var strResource_Version = $("#ddlResource_Version").val();//
            var AuditState = $("#ddlBookShelvesState").find("option:selected").val();//
            var dto = {
                DocName: docName,
                strResource_Type: strResource_Type,
                strResource_Class: strResource_Class,
                strGradeTerm: strGradeTerm,
                strSubject: strSubject,
                strResource_Version: strResource_Version,
                AuditState: AuditState,
                ATime: $.trim($("#txtTime").val()),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("BookAuditList.aspx/GetCloudBooks", JSON.stringify(dto), function (data) {
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
            catalogID = "";
            docName = "";
            tp = "1";
            loadData();
            $('#txtTime').datetimepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                minView: 4,
                language: 'zh-CN'
            });
            $("#btnSearch").click(function () {
                pageIndex = 1;
                docName = $.trim($("#txtKey").val());
                loadData();
            });
            $("#ddlGradeTerm").change(function () {
                pageIndex = 1;
                docName = $.trim($("#txtKey").val());
                loadData();
            });
            $("#ddlResource_Version").change(function () {
                pageIndex = 1;
                docName = $.trim($("#txtKey").val());
                loadData();
            });
            $("#ddlResource_Type").change(function () {
                pageIndex = 1;
                docName = $.trim($("#txtKey").val());
                loadData();
            });
            $("#ddlSubject").change(function () {
                pageIndex = 1;
                docName = $.trim($("#txtKey").val());
                loadData();
            });
            $("#ddlBookShelvesState").change(function () {
                pageIndex = 1;
                docName = $.trim($("#txtKey").val());
                loadData();
            });
        });
        var DelData = function (dataId) {
            layer.ready(function () {
                var index = layer.confirm("确认要删除吗？<br>请谨慎操作！<br>确认后与之相关的所有数据即将清除！", { icon: 4, title: '提示' }, function () {
                    layer.close(index);//关闭确认弹窗
                    $.ajaxWebService("BookAuditList.aspx/DelData", "{dataId:'" + dataId + "',userId:'<%=userId%>',x:'" + Math.random() + "'}", function (data) {
                        if (data.d == "1") {
                            layer.msg('删除成功', { icon: 1, time: 1000 }, function () { loadData(); });
                        }
                        else if (data.d == "2") {
                            layer.msg('存在子级，删除失败！', { icon: 2 });
                            return false;
                        }

                    }, function () {
                        layer.msg('删除失败！', { icon: 2 });
                        return false;
                    });
                });
            })
        }
        //编辑
        function EditData(iid, rtype) {//rtype 0文件夹 1文件
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: "编辑",
                    fix: false,
                    area: ["385px", "250px"],
                    content: "ResEdit.aspx?iid=" + iid + "&rtype=" + rtype
                })
            });
        }
    </script>
</asp:Content>
