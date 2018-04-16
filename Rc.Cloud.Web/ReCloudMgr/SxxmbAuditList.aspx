<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SxxmbAuditList.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.SxxmbAuditList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/style001/user.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style001/pagination.css" rel="stylesheet" />
    <script src="../Scripts/PhhcCommon.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_right_title">
        <div class="div_right_title_icon"><%--<a href="javascript:history.back(-1);">返回上一级</a>--%></div>
        <%=siteMap%>
    </div>
    <div class="clearDiv"></div>
    <div class="div_right_search">
        <table class="table_search_001">
            <tr>
                <td>年级学期：
                </td>
                <td>
                    <asp:DropDownList ID="ddlGradeTerm" CssClass="user_ddl" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                </td>
                <td>学科：
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubject" CssClass="user_ddl" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                </td>
                <td>教材版本：
                </td>
                <td>
                    <asp:DropDownList ID="ddlResource_Version" CssClass="user_ddl" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                </td>
                <td>双向细目表名称：
                </td>
                <td>
                    <asp:TextBox ID="txtKey" runat="server" CssClass="txt_Search myTextBox" ClientIDMode="Static"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>审核状态：
                </td>
                <td colspan="8">
                    <asp:DropDownList ID="ddlBookShelvesState" CssClass="user_ddl" ClientIDMode="Static" runat="server">
                        <asp:ListItem Text="--全部--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="未审核" Value="3"></asp:ListItem>
                        <asp:ListItem Text="已审核" Value="1"></asp:ListItem>
                        <asp:ListItem Text="驳回" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <input type="button" class="btn" id="btnSearch" value="查询" />
                </td>
            </tr>
        </table>
    </div>

    <div class="clear"></div>
    <div style="width: 100%">
        <%--       <div class="left_tree">
            <asp:Literal ID="litTree" ClientIDMode="Static" runat="server"></asp:Literal>
        </div>--%>

        <div class="" id="userDocumentContent">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
                <thead>
                    <tr class="tr_title">
                        <td>年级学期</td>
                        <td>学科</td>
                        <td>教材版本</td>
                        <td>名称</td>
                        <td>审核状态</td>
                        <td>审核日期</td>
                        <td>审核人</td>
                        <td>操作</td>
                    </tr>
                </thead>
                <tbody id="tbRes">
                </tbody>
            </table>
            <div class="page pagination">
                <ul>
                </ul>
            </div>
        </div>
    </div>

    <textarea id="template_Res" class="display_none" style="display: none;">
    {#foreach $T.list as record}
    <tr class="tr_con_001">
        <td>{$T.record.GradeTerm}</td>
        <td>{$T.record.Subject}</td>
         <td>{$T.record.Resource_Version}</td>
        <td>{$T.record.docName}</td>       
        <td>{$T.record.AuditStateStr}</td>
         <td>{$T.record.AuditTime}</td>
         <td>{$T.record.UserName}</td>
        <td  style="text-align:center;">
            {#if $T.record.Status==""}
            <a   href="javascript:;" onclick="SxxmbAudit('{$T.record.docId}','{$T.record.docName}',1)" >审核</a>
          &nbsp; <a   href="javascript:;" onclick="SxxmbAudit('{$T.record.docId}','{$T.record.docName}',2)" >驳回</a>
            {#elseif  $T.record.Status=="0"}
             <a   href="javascript:;" onclick="SxxmbAudit('{$T.record.docId}','{$T.record.docName}',1)"  >审核</a>
            {#else }
            {#/if}
            &nbsp;<a title=""  href="javascript:;" onclick="View('{$T.record.docId}','{$T.record.docName}')">查看</a>
            {$T.record.Operate}
           </td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">
        //预览
        function View(Id, Name) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '预览',
                    area: ["750px", "650px"],
                    content: "SxxmbView.aspx?Two_WayChecklist_Id=" + Id + "&Two_WayChecklist_Name=" + Name
                })
            })
        }

        var SxxmbAudit = function (reid, rename, type) {
            layer.ready(function () {
                var _title = "";
                if (type == 1) {
                    _title = "确定审核通过?";
                }
                else {
                    _title = "确定要驳回?";
                }
                var index = layer.confirm(_title, { icon: 4, title: '提示' }, function () {
                    layer.close(index);//关闭确认弹窗
                    $.ajaxWebService("SxxmbAuditList.aspx/SxxmbAudit", "{reid:'" + reid + "',type:" + type + ",rename:'" + rename + "',x:'" + Math.random() + "'}", function (data) {
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
            })
        }



        var loadData = function () {
            var strGradeTerm = $("#ddlGradeTerm").val();//
            var strSubject = $("#ddlSubject").val();//
            var strResource_Version = $("#ddlResource_Version").val();//
            var AuditState = $("#ddlBookShelvesState").find("option:selected").val();//
            var dto = {
                DocName: docName,
                strGradeTerm: strGradeTerm,
                strSubject: strSubject,
                strResource_Version: strResource_Version,
                AuditState: AuditState,
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("SxxmbAuditList.aspx/GetTwo_WayChecklist", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbRes").setTemplateElement("template_Res", null, { filter_data: false });
                    $("#tbRes").processTemplate(json);
                    $(".pagination").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbRes").html("<tr class='tr_con_002'><td colspan='100' align='center'>暂无数据</td></tr>");
                    $(".pagination").find("ul").html("");
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
                    title: "修改名称",
                    fix: false,
                    area: ["600px", "300px"],
                    content: "ResEdit.aspx?iid=" + iid + "&rtype=" + rtype
                })
            })
        }
    </script>
</asp:Content>
