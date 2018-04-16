<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mHomework_API.aspx.cs" Inherits="Rc.Cloud.Web.teacher.mHomework_API" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Rc.Common.ConfigHelper.GetConfigString("WebSiteName") %></title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugin/tree/tree.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../plugin/layer/css/layer.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/jquery.min-1.11.1.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../plugin/layer/js/layer.js"></script>
    <script type="text/javascript" src="../plugin/tree/tree.js"></script>
    <script type="text/javascript" src="../js/json2.js"></script>
    <script type="text/javascript" src="../js/jq.pagination.js"></script>
    <script type="text/javascript" src="../js/jquery-jtemplates.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <!--[if lt IE 9]>
      <script src="../js/html5shiv.min.js"></script>
      <script src="../js/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript">
        $(function () {
            $(document).on('click', ".layoutIframe", function () {
                var rtrId = $(this).attr("val");
                var classid = '<%=strUserGroup_IdActivity%>';
                layer.open({
                    type: 2,
                    title: '布置作业',
                    area: ['680px', '555px'],
                    content: 'layoutIframe.aspx?rtrId=' + rtrId + "&classId=" + classid //iframe的url
                });
            });

            $(document).on('click', ".AddlayoutIframe", function () {
                var rtrId = $(this).attr("val");
                var classid = '<%=strUserGroup_IdActivity%>';
                var HwId = $(this).attr("HomeWork_Id");
                layer.open({
                    type: 2,
                    title: '布置作业',
                    area: ['680px', '555px'],
                    content: 'AddlayoutIframe.aspx?rtrId=' + rtrId + "&classId=" + classid + "&HomeWork_Id=" + HwId //iframe的url
                });
            });


            $(document).on('click', ".DellayoutIframe", function () {
                var HwId = $(this).attr("HomeWork_Id");
                var rtrId = $(this).attr("val");
                classDisband(HwId, rtrId);
            })

            $(".left_tree dd a").click(function () {
                $(".left_tree dd a").removeClass("active");
                $(this).addClass("active");
            });

            //布置详情
            $(document).on({
                click: function () {
                    var rtrId = $(this).attr("val");

                    var classid = '<%=strUserGroup_IdActivity%>';
                    layer.open({
                        type: 2,
                        title: '【' + $.trim($(this).closest("tr").find("td:eq(0)").html()) + '】布置详情',
                        area: ['860px', '550px'],
                        content: 'ArrangeDetail.aspx?ResourceToResourceFolderId=' + rtrId + "&classId=" + classid //iframe的url
                    });
                }
            }, '[data-name="ArrangeDetail"]')
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="iframe-header">
            <div class="navbar navbar-primary" role="navigation">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <a class="navbar-brand" href='##'>我的空间</a>
                    </div>
                    <div class="collapse navbar-collapse">
                        <ul class="nav navbar-nav">
                            <li><a href="cHomework_API.aspx?ugid=<%=strUserGroup_IdActivity %>">习题集</a></li>
                            <li><a href="mHomework_API.aspx?ugid=<%=strUserGroup_IdActivity %>" class="active">自有习题集</a></li>
                            <li id="testPaper2" runat="server" visible="false"><a href="historyTestPaper_API.aspx?ugid=<%=strUserGroup_IdActivity %>">已组试卷</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField runat="server" ID="hidClassId" ClientIDMode="Static" />
        <asp:Literal runat="server" ID="litContent"></asp:Literal>


        <textarea id="template_Res" class="hidden">
        {#foreach $T.list as record}
            <tr>
                <td>{$T.record.docName}</td>
                <td>{$T.record.docTime}</td>
                <td class="text-center">
                    <a href="./HomeworkPreviewT.aspx?ResourceToResourceFolder_Id={$T.record.docId}&groupId=<%=strUserGroup_IdActivity %>&HomeWork_Id={$T.record.HomeWork_Id}" data-name="Preview">预览</a>
                    {#if $T.record.HomeWork_Id==""}
                    <a href='##' class="layoutIframe" val="{$T.record.docId}">布置</a>
                    {#else}
                    <a href='##' class="AddlayoutIframe" val="{$T.record.docId}" HomeWork_Id="{$T.record.HomeWork_Id}">继续布置</a>
            <a href='##' onclick="classDisband('{$T.record.HomeWork_Id}','{$T.record.docId}')" val="{$T.record.docId}" HomeWork_Id="{$T.record.HomeWork_Id}">撤销</a>
                    <a href='##' data-name="ArrangeDetail" val="{$T.record.docId}">布置详情</a>
                    {#/if}
                </td>
            </tr>
        {#/for}
    </textarea>
        <script type="text/javascript">

            var classDisband = function (hwid, rtrId) {
                var index = layer.confirm("确定要撤销作业吗？作业撤销后系统将清除学生已提交作业的所有相关数据！", { icon: 4, title: '提示' }, function () {
                    layer.close(index);//关闭确认弹窗
                    $.ajaxWebService("cHomework.aspx/DeleteHw", "{HomeWorkId:'" + hwid + "',x:'" + Math.random() + "'}", function (data) {
                        if (data.d == "1") {
                            loadData();
                        }
                        if (data.d == "2") {
                            layer.msg('重新布置作业失败', { icon: 2 });
                            return false;
                        }

                    }, function () {
                        layer.msg('重新布置作业失败！', { icon: 2 });
                        return false;
                    }, false);
                });
            }
            function Show(Classid, rom) {
                var id = $("#list div.active a").attr("id");
                window.location.href = "mHomework_API.aspx?" + Classid + "&" + rom + "&strResourceForder_IdDefault=" + id;
            }
            function ShowSubDocument(strDoctumentID, ResourceFolder_Name) {
                ResourceFolder_Id = strDoctumentID;
                ShowFolderIn = "0";
                pageIndex = 1;
                loadData();
            }
            var Delete = function (rtrId) {

                var classid = '<%=strUserGroup_IdActivity%>';
                layer.open({
                    type: 2,
                    title: '布置作业',
                    area: ['680px', '443px'],
                    content: 'layoutIframe.aspx?rtrId=' + rtrId + "&classId=" + classid //iframe的url
                });

            }
            var loadData = function () {
                var dto = {
                    ResourceFolder_Id: ResourceFolder_Id,
                    DocName: docName,
                    ShowFolderIn: ShowFolderIn,//0加载全部 1加载文件夹
                    UserGroupId: "<%=strUserGroup_IdActivity%>",
                    PageIndex: pageIndex,
                    PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                    x: Math.random()
                };
                $.ajaxWebService("mHomework_API.aspx/GetcHomework", JSON.stringify(dto), function (data) {
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
