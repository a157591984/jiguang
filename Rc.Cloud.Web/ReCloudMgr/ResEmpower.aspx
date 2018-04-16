<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="ResEmpower.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.ResEmpower" %>

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
                    <asp:TextBox ID="txtKey" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="文件名"></asp:TextBox>
                    <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" data-name="submit" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>年级学期</th>
                            <th>学科</th>
                            <th>教材版本</th>
                            <th>文档类型</th>
                            <th width="30%">名称</th>
                            <th>修改日期</th>
                            <th>操作授权</th>
                            <%-- <td>共享资源</td>--%>
                            <%--<td>授权</td>--%>
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
        <td>{$T.record.docName}</td>
        <td>{$T.record.docTime}</td>
        <td>
            <div class="form-inline">
                <select class="form-control input-sm" onchange="OperateReAuth('Print','{$T.record.docId}',this);">
            	    <option value="0" {#if $T.record.IsPrint=='0'}selected{#/if}>禁止打印</option>
            	    <option value="1" {#if $T.record.IsPrint=='1'}selected{#/if}>允许打印</option>
                </select>
                <select class="form-control input-sm" onchange="OperateReAuth('Save','{$T.record.docId}',this);">
            	    <option value="0" {#if $T.record.IsSave=='0'}selected{#/if}>禁止存盘</option>
            	    <option value="1" {#if $T.record.IsSave=='1'}selected{#/if}>允许存盘</option>
                </select>
                 <select class="form-control input-sm" onchange="OperateReAuth('Copy','{$T.record.docId}',this);">
            	    <option value="0" {#if $T.record.IsCopy=='0'}selected{#/if}>禁止复制</option>
            	    <option value="1" {#if $T.record.IsCopy=='1'}selected{#/if}>允许复制</option>
                </select>
            </div>
        </td>
        <%--<td style="text-align:center;" onclick="shareResouce('{$T.record.docId}','{$T.record.docName}','{$T.record.docShared}')" >
            <a title="资源共享后所有老师均可见。"  >{$T.record.docSharedName}</a>
        </td>--%>
        <%--<td  style="text-align:center;">
            <a title="授权。"  href="javascript:;" onclick="showPop('{$T.record.docId}','{$T.record.docName}')" >授权</a>
           </td>--%>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        //授权
        function showPop(bid, bname) {
            src = "<%=_t %>" == 1 ? "CloudResEmpowerDo.aspx" : "CloudResEmpowerDoS.aspx";
            src += "?bid=" + bid + "&dType=<%=_t %>";
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '授权',
                    area: ['850px', '620px'],
                    content: src
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
                    layer.msg(strMessage, { icon: 4 });
                }
            });
        }
        function ShowSubDocument(str, strDoctumentID, strDoctumentName) {
            //ShowUpload(str, strDoctumentID)
            catalogID = strDoctumentID;
            tp = "0";
            loadData();
        }
        var loadData = function () {
            var strResource_Type = $("#ddlResource_Type").val();//
            var strResource_Class = '<%=strResource_Class%>';//

            var strGradeTerm = $("#ddlGradeTerm").val();//
            var strSubject = $("#ddlSubject").val();//
            var strResource_Version = $("#ddlResource_Version").val();//
            var dto = {
                DocName: docName,
                strResource_Type: strResource_Type,
                strResource_Class: strResource_Class,
                strGradeTerm: strGradeTerm,
                strSubject: strSubject,
                strResource_Version: strResource_Version,
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("ResEmpower.aspx/GetCloudBooks", JSON.stringify(dto), function (data) {
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
        function OperateReAuth(attrEnum, reId, obj) {
            var dto = {
                AttrEnum: attrEnum,
                ReId: reId,
                AttrValue: $(obj).val(),
                x: Math.random()
            }
            $.ajaxWebService("ResEmpower.aspx/OperateResourceAuth", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    layer.ready(function () {
                        layer.msg('操作成功', { icon: 1, time: 1000 })
                    });
                }
            }, function () { }, false);
        }


        $(function () {
            pageIndex = 1;
            catalogID = "";
            docName = "";
            tp = "1";

            loadData();

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
        });
    </script>
</asp:Content>
