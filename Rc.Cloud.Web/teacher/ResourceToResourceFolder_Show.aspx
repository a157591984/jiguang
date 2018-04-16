<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResourceToResourceFolder_Show.aspx.cs" Inherits="Rc.Cloud.Web.teacher.ResourceToResourceFolder_Show" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../Scripts/plug-in/laydate/laydate.js"></script>
    <script src="../Scripts/jquery-jtemplates.js"></script>
    <script src="../Scripts/jq.pagination.js"></script>
    <script src="../Scripts/function.js"></script>
    <script src="../Scripts/base64.js"></script>
    <title><%=Rc.Common.ConfigHelper.GetConfigString("WebSiteName") %></title>
    <script type="text/javascript">
        $(function () {
            b = new Base64();
            pageIndex = 1;
            basicUrl = ""; //本页基础url(不包括页码参数)
            backurl = ""; //跳转所用bas64加页码url
            loadParaFromLink();
            loadData();
            $("#btnHisBack").click(function () {
                var backurl = getUrlVar("backurl");
                if (backurl != "") {
                    b = new Base64();
                    window.location.href = b.decode(backurl);
                }
                else {
                    // window.location.href = "ResourceToResourceFolder_Show.aspx?ResourceFolder_Id=<%=ResourceFolder_Id%>";
                }
            })
        })

        var loadData = function () {
            setBasicUrl();
            var dto = {
                ResourceFolder_Id: "<%=ResourceFolder_Id%>",
                resTitle: "<%=resTitle%>",
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                x: Math.random()
            };
            $.ajaxWebService("ResourceToResourceFolder_Show.aspx/GetResourceList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbRes").setTemplateElement("template_Res", null, { filter_data: false });
                    $("#tbRes").processTemplate(json);
                    $(".pagination_container").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbRes").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $(".pagination_container").html("");
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
        var setBasicUrl = function () {
            basicUrl = "ResourceToResourceFolder_Show.aspx?";
            backurl = b.encode(basicUrl + "pageIndex=" + pageIndex
                + "&ResourceFolder_Id=<%=ResourceFolder_Id%>"
                + "&backurl=" + getUrlVar("backurl")
                + "&resTitle=<%=resTitle%>");
        }
        var loadParaFromLink = function () {
            pageIndex = getUrlVar("pageIndex") == "" ? 1 : getUrlVar("pageIndex");
            ResourceFolder_Id = getUrlVar("ResourceFolder_Id");
            resTitle = getUrlVar("resTitle");
        }
        function GoToNext(ResourceFolder_Id, name) {
            window.location.href = "ResourceToResourceFolder_Show.aspx?ResourceFolder_Id=" + ResourceFolder_Id + "&backurl=" + backurl + "&resTitle=" + name;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pa">
            <h4 class="hidden">
                <asp:Literal ID="ltlBookName" runat="server"></asp:Literal></h4>
            <input type="button" id="btnHisBack" value="返回" class="btn btn-default btn-sm mb">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>名称</th>
                        <th width="90">大小/类型</th>
                        <th width="150">日期</th>
                        <th width="90">操作</th>
                    </tr>
                </thead>
                <tbody id="tbRes">
                </tbody>
            </table>
            <div class="pagination_container">
                <ul class="pagination">
                </ul>
            </div>
            <textarea id="template_Res" class="hidden">
                {#foreach $T.list as record}
                    <tr>
                        <td>{$T.record.docName}</td>
                        <td>{#if $T.record.RType=='1'}{$T.record.docSize}{#else}文件夹{#/if}</td>
                        <td>{$T.record.docTime}</td>
                        <td class="opera">
                            {#if $T.record.RType=='0'}
                                <a href="javascript:;" onclick="GoToNext('{$T.record.docId}','{$T.record.ResourceFolder_NameUrl}')" >资源列表</a>
                            {#else}
                                {#if $T.record.IType=='1'}
                                <a href="FileView.aspx?ResourceToResourceFolder_Id={$T.record.docId}" target="_blank" data-name="Preview">预览</a>
                                {#else}
                                <a href="HomeworkPreviewT.aspx?ResourceToResourceFolder_Id={$T.record.docId}" target="_blank" data-name="Preview">预览</a>
                                {#/if}
                            {#/if}
                            {$T.record.Operate}
                        </td>
                    </tr>
                {#/for}
            </textarea>
        </div>
    </form>
</body>
</html>
