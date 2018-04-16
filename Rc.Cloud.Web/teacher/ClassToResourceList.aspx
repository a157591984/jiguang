<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassToResourceList.aspx.cs" Inherits="Rc.Cloud.Web.teacher.ClassToResourceList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/plugin/mtree-2.0/mtree.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />

    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/bootstrap-3.3.7/js/bootstrap.min.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/plugin/mtree-2.0/mtree.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script src="../SysLib/js/jq.pagination.js"></script>
    <script src="../SysLib/js/json2.js"></script>
    <script src="../SysLib/js/base64.js"></script>
    <script src="../SysLib/js/index.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="panel">
                <div class="panel-body">
                    <div class="form-inline search_bar mb">
                        <asp:DropDownList ID="ddlResource_Class" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlGradeTerm" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlResource_Version" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSubject" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtKey" runat="server" CssClass="form-control input-sm" ClientIDMode="Static" placeholder="文件名"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hiduserId" />
                        <asp:HiddenField runat="server" ID="hiduserIdentity" />
                        <asp:HiddenField runat="server" ID="hidfolderId" />
                        <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" data-name="submit" />
                        <input type="button" class="btn btn-default btn-sm" id="btnBack" value="返回" runat="server" />
                    </div>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>名称</th>
                                <th width="140">日期</th>
                                <th width="100">操作</th>
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
            <td>{$T.record.docName}</td>
            <td>{$T.record.docTime}</td>
            <td class="opera">
                {#if $T.record.RType=='0'}
                    <a href="##" onclick="loadLink('{$T.record.docId}');">资源列表</a>
                {#else}
                    <a href="/ReCloudMgr/TestPaperView.aspx?ResourceToResourceFolder_Id={$T.record.docId}&isBack=1">预览</a>
                    <a href="TestpaperView.aspx?id={$T.record.docId}" testid="{$T.record.docId}" id="select_media">选择</a>
                {#/if}
            </td>
        </tr>
        {#/for}
        </textarea>
    </form>
    <script type="text/javascript">
        var loadData = function () {
            var strResource_Class = $("#ddlResource_Class").val();
            var strGradeTerm = $("#ddlGradeTerm").val();
            var strResource_Version = $("#ddlResource_Version").val();
            var strSubject = $("#ddlSubject").val();

            //setBasicUrl();
            var dto = {
                DocName: docName,
                strResource_Class: strResource_Class,
                strGradeTerm: strGradeTerm,
                strSubject: strSubject,
                strResource_Version: strResource_Version,
                userId: "<%=userId%>",
                userIdentity: "<%=userIdentity%>",
                folderId: "<%=folderId%>",
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("ClassToResourceList.aspx/GetCloudBooks", JSON.stringify(dto), function (data) {
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
        //var setBasicUrl = function () {
        //    basicUrl = "ClassToResourceList.aspx?";
        //    backurl = b.encode(basicUrl + "pageIndex=" + pageIndex
        //        + "&ddlResource_Class=" + $.trim($("#ddlResource_Class").val())
        //        + "&ddlGradeTerm=" + $.trim($("#ddlGradeTerm").val())
        //        + "&ddlResource_Version=" + $.trim($("#ddlResource_Version").val())
        //        + "&ddlSubject=" + $.trim($("#ddlSubject").val())
        //        + "&txtKey=" + escape($.trim($("#txtKey").val()))
        //        + "&userId=" + $.trim($("#hiduserId").val())
        //        + "&userIdentity=" + $.trim($("#hiduserIdentity").val())
        //        + "&folderId=" + $.trim($("#hidfolderId").val())
        //        + "&backurl=" + $.trim($("#hidbackurl").val()));

        //}
        //var loadParaFromLink = function () {
        //    pageIndex = getUrlVar("pageIndex") == "" ? 1 : getUrlVar("pageIndex");
        //    $("#ddlResource_Class").val(getUrlVar("ddlResource_Class"));
        //    $("#ddlGradeTerm").val(getUrlVar("ddlGradeTerm"));
        //    $("#ddlResource_Version").val(getUrlVar("ddlResource_Version"));
        //    $("#ddlSubject").val(getUrlVar("ddlSubject"));
        //    $("#txtKey").val(unescape(getUrlVar("txtKey")));
        //    $("#hiduserId").val(getUrlVar("userId"));
        //    $("#hiduserIdentity").val(getUrlVar("userIdentity"));
        //    $("#hidfolderId").val(getUrlVar("folderId"));
        //    $("#hidbackurl").val(getUrlVar("backurl"));
        //}
        var loadLink = function (rId) {
            window.location.href = "ClassToResourceList.aspx?userId=<%=userId%>&userIdentity=<%=userIdentity%>&folderId=" + rId
                + "&Resource_Class=" + $.trim($("#ddlResource_Class").val())
                + "&GradeTerm=" + $.trim($("#ddlGradeTerm").val())
                + "&Resource_Version=" + $.trim($("#ddlResource_Version").val())
                + "&Subject=" + $.trim($("#ddlSubject").val())
                + "&isBack=1";
        }

        $(function () {
            catalogID = "";
            docName = "";

            b = new Base64();
            pageIndex = 1;
            basicUrl = ""; //本页基础url(不包括页码参数)
            backurl = ""; //跳转所用bas64加页码url
            //loadParaFromLink();
            loadData();


            $("#btnSearch").click(function () {
                pageIndex = 1;
                docName = $.trim($("#txtKey").val());
                loadData();
            });

            $("#btnBack").click(function () {
                history.back();
            });

            $("#ddlResource_Class").change(function () {
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

        });
    </script>
</body>
</html>
