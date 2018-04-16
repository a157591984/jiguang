<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cHomework_API_Select.aspx.cs" Inherits="Rc.Cloud.Web.teacher.cHomework_API_Select" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Rc.Common.ConfigHelper.GetConfigString("WebSiteName") %></title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugin/tree/tree.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/jquery.min-1.11.1.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../plugin/layer/layer.js"></script>
    <script type="text/javascript" src="../plugin/tree/tree.js"></script>
    <script type="text/javascript" src="../js/json2.js"></script>
    <script type="text/javascript" src="../js/jq.pagination.js"></script>
    <script type="text/javascript" src="../js/jquery-jtemplates.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="iframe-header">
            <div class="navbar navbar-default" role="navigation">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <a class="navbar-brand" href='##'>我的空间</a>
                    </div>
                    <div class="collapse navbar-collapse">
                        <ul class="nav navbar-nav">
                            <li class="active"><a href="cHomework_API_Select.aspx">习题集</a></li>
                            <li><a href="mHomework_API_Select.aspx">自有习题集</a></li>
                            <li id="apHomework_API_Select" runat="server" visible="false"><a href="pHomework_API_Select.aspx">集体备课习题集</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>

        <asp:Literal runat="server" ID="litContent"></asp:Literal>
        <asp:HiddenField ID="hidreid" runat="server" />
        <textarea id="template_Res" class="hidden">
            {#foreach $T.list as record}
                <tr>
                    <td>{$T.record.docName}</td>
                    <td>{$T.record.docTime}</td>
                    <td class="text-center">
                        <a href="HomeworkPreviewT.aspx?ResourceToResourceFolder_Id={$T.record.docId}&strResourceForder_IdDefault={$T.record.ResourceFolder_Id}&isBack=1">预览</a>
                        <a href="##" testid="{$T.record.docId}" id="select_media">选择</a>
                    </td>
                </tr>
            {#/for}
        </textarea>
        <script type="text/javascript">

            function ShowSubDocument(strDoctumentID) {
                ResourceFolder_Id = strDoctumentID;
                ShowFolderIn = "0";
                pageIndex = 1;
                loadData();
            }

            var loadData = function () {
                var dto = {
                    ResourceFolder_Id: ResourceFolder_Id,
                    DocName: docName,
                    ShowFolderIn: ShowFolderIn,//0加载全部 1加载文件夹
                    PageIndex: pageIndex,
                    PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                    x: Math.random()
                };
                $.ajaxWebService("cHomework_API_Select.aspx/GetcHomework", JSON.stringify(dto), function (data) {
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
                        $(".pagination_container").find("ul").html("");
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
                ResourceFolder_Id = '<%=strResourceForder_IdDefault%>';
                docName = "";
                ShowFolderIn = "1";

                $("#list .name.active a").click();

                $(".left_tree dd a").click(function () {
                    $(".left_tree dd a").removeClass("active");
                    $(this).addClass("active");
                });

            });

        </script>
    </form>
</body>
</html>
