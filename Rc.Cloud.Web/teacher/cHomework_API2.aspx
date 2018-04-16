<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cHomework_API2.aspx.cs" Inherits="Rc.Cloud.Web.teacher.cHomework_API2" %>

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
    <script type="text/javascript">
        $(function () {
            $(document).on('click', ".DellayoutIframe", function () {
                var HwId = $(this).attr("HomeWork_Id");
                var rtrId = $(this).attr("val");
                classDisband(HwId, rtrId);
            })

            $(".left_tree dd a").click(function () {
                $(".left_tree dd a").removeClass("active");
                $(this).addClass("active");
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal runat="server" ID="litContent"></asp:Literal>
        <asp:HiddenField ID="hidreid" runat="server" />
        <textarea id="template_Res" class="hidden">
            {#foreach $T.list as record}
                <tr>
                    <td>{$T.record.docName}</td>
                    <td class="text-center">
                        <a href="<%=Rc.Cloud.Web.Common.pfunction.getHostPath() %>/teacher/TestpaperView.aspx?id={$T.record.docId}" testid="{$T.record.docId}" id="select_media">选择</a>
                    </td>
                </tr>
            {#/for}
        </textarea>
        <script type="text/javascript">
            function ShowSubDocument(strDoctumentID, ResourceFolder_Name) {
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
                $.ajaxWebService("cHomework_API2.aspx/GetcHomework", JSON.stringify(dto), function (data) {
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
                        $("#tbRes").html("<tr><td colspan='4' align='center'>暂无数据</td></tr>");
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
                //loadData();

                $("#list .name.active a").click();
            });

        </script>
    </form>
</body>
</html>
