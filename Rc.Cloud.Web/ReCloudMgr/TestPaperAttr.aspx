<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="TestPaperAttr.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.TestPaperAttr" %>

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
                    <asp:TextBox ID="txtKey" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="书本名称"></asp:TextBox>
                    <input type="button" class="btn btn-sm btn-default" id="btnSearch" value="查询" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>年级学期</th>
                            <th>学科</th>
                            <th>教材版本</th>
                            <th>文档类型</th>
                            <th width="30%">名称</th>
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
                        <td class="opera">
                        <a title=""  href="javascript:;" onclick="ShowRelist('TestPaperList.aspx?resourceFolderId={$T.record.docId}&resTitle={$T.record.GradeTerm}--{$T.record.Subject}--{$T.record.Resource_Version}--{$T.record.Resource_Type}--{$T.record.docName}')" >资源列表</a>
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
        var ShowRelist = function (url) {
            window.location.href = url + "&backurl=" + backurl;
        }
        function ShowSubDocument(str, strDoctumentID, strDoctumentName) {
            //ShowUpload(str, strDoctumentID)
            catalogID = strDoctumentID;
            tp = "0";
            loadData();
        }
        var loadData = function () {
            setBasicUrl();

            var strResource_Class = '<%=strResource_Class%>';//

            var strGradeTerm = $("#ddlGradeTerm").val();//
            var strSubject = $("#ddlSubject").val();//
            var strResource_Version = $("#ddlResource_Version").val();//
            var docName = $.trim($("#txtKey").val());
            var dto = {
                DocName: docName,
                strResource_Class: strResource_Class,
                strGradeTerm: strGradeTerm,
                strSubject: strSubject,
                strResource_Version: strResource_Version,
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("TestPaperAttr.aspx/GetCloudBooks", JSON.stringify(dto), function (data) {
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

                $('#tbRes img').popover({
                    trigger: 'hover',
                    html: true
                });
            }, function () { });
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        var setBasicUrl = function () {
            basicUrl = "TestPaperAttr.aspx?";
            backurl = b.encode(basicUrl + "pageIndex=" + pageIndex + "&DocName=" + escape($.trim($("#txtKey").val()))
                + "&strGradeTerm=" + $("#ddlGradeTerm").val()
                + "&strSubject=" + $("#ddlSubject").val()
                + "&strResource_Version=" + $("#ddlResource_Version").val());
        }
        var loadParaFromLink = function () {
            pageIndex = getUrlVar("pageIndex") == "" ? 1 : getUrlVar("pageIndex");
            $("#txtKey").val(unescape(getUrlVar("DocName")));
            $("#ddlGradeTerm").val(getUrlVar("strGradeTerm"));
            $("#ddlSubject").val(getUrlVar("strSubject"));
            $("#ddlResource_Version").val(getUrlVar("strResource_Version"));
        }
        $(function () {
            pageIndex = 1;
            catalogID = "";
            tp = "1";
            b = new Base64();
            loadParaFromLink();
            loadData();
            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();
            });
            $("#ddlGradeTerm").change(function () {
                pageIndex = 1;
                loadData();
            });
            $("#ddlResource_Version").change(function () {
                pageIndex = 1;
                loadData();
            });
            $("#ddlSubject").change(function () {
                pageIndex = 1;
                loadData();
            });
        });
    </script>
</asp:Content>
