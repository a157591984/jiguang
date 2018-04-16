<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="historyTestPaper.aspx.cs" Inherits="Rc.Cloud.Web.teacher.historyTestPaper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="cHomework.aspx?ugid=<%=strUserGroup_IdActivity %>">习题集</a></li>
            <li><a href="mHomework.aspx?ugid=<%=strUserGroup_IdActivity %>">自有习题集</a></li>
            <li id="apHomework" runat="server" visible="false"><a href="pHomework.aspx?ugid=<%=strUserGroup_IdActivity %>">集体备课习题集</a></li>
            <%-- <li id="testPaper" runat="server" visible="false"><a href="javascript:;" onclick="btnZJ();">组卷</a></li>
            <li id="testPaper2" runat="server" visible="false"><a href="javascript:;" onclick="btnYZSJ();" class="active">已组试卷</a></li>--%>
            <li class="dropdown" id="testPaper" runat="server" visible="false">
                <a href="javascript:;" data-toggle="dropdown">组卷<i class="material-icons">&#xE5C5;</i></a>
                <ul class="dropdown-menu">
                    <li id="cptestPaper" runat="server" visible="false"><a href="ChapterAssembly.aspx?ugid=<%=strUserGroup_IdActivity %>">章节组卷</a></li>
                    <li id="twtestPaper" runat="server" visible="false"><a href="javascript:;" onclick="btnZJ();">双向细目表组卷</a></li>
                </ul>
            </li>
            <li class="active dropdown" id="testPaper2" runat="server" visible="false">
                <a href="javascript:;" data-toggle="dropdown">已组试卷<i class="material-icons">&#xE5C5;</i></a>
                <ul class="dropdown-menu">
                    <li id="cptestPaper2" runat="server" visible="false"><a href="ChapterTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>">章节组卷</a></li>
                    <li id="twtestPaper2" runat="server" visible="false" class="active"><a href="javascript:;" onclick="btnYZSJ();">双向细目表组卷</a></li>
                </ul>
            </li>
        </ul>
    </div>
    <div class="iframe-container">
        <asp:Literal ID="ltlClass" runat="server"></asp:Literal>
        <div class="iframe-main">
            <div class="iframe-main-sidebar">
                <div class="page_title pv">双向细目表</div>
                <div class="mtree mtree-checklist-hook" data-name="exerciseBook">
                    <ul id="tbTwo">
                        <asp:Literal ID="ltlTwo_WayChecklist" runat="server"></asp:Literal>
                    </ul>
                </div>
            </div>
            <div class="iframe-main-section pa">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>名称</th>
                            <th width="150">时间</th>
                            <th width="250">操作</th>
                        </tr>
                    </thead>
                    <tbody id="tbRes">
                    </tbody>
                </table>
                <div class='pagination_container'>
                    <ul class='pagination'>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <textarea id="template_Res" class="hidden">
        {#foreach $T.list as record}
            <tr>
                <td>{$T.record.docName}</td>
                <td>{$T.record.docTime}</td>
                <td>
                    <a href="./HomeworkPreviewT.aspx?ResourceToResourceFolder_Id={$T.record.docId}&groupId=<%=strUserGroup_IdActivity %>&HomeWork_Id={$T.record.HomeWork_Id}" target="_blank" data-name="Preview">预览</a>
                    {#if $T.record.HomeWork_Id==""}
                        <a href='##' class="assign-hook" val="{$T.record.docId}">布置</a>
                    {#else}
                        <a href='##' class="continue-assign-hook" val="{$T.record.docId}" HomeWork_Id="{$T.record.HomeWork_Id}">继续布置</a>
                        <a href='##' onclick="classDisband('{$T.record.HomeWork_Id}','{$T.record.docId}')" val="{$T.record.docId}" HomeWork_Id="{$T.record.HomeWork_Id}">撤销</a>
                        <a href='##' data-name="ArrangeDetail" val="{$T.record.docId}">布置详情</a>
                    {#/if}
                </td>
            </tr>
        {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            loadData();

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
            $(document).on('click', ".assign-hook", function (e) {
                var rtrId = $(this).attr("val");
                var classid = '<%=strUserGroup_IdActivity%>';
                layer.open({
                    type: 2,
                    title: '布置作业',
                    area: ['680px', '555px'],
                    content: 'layoutIframe.aspx?rtrId=' + rtrId + "&classId=" + classid //iframe的url
                });
                e.preventDefault();
            });

            $(document).on('click', ".continue-assign-hook", function (e) {
                var rtrId = $(this).attr("val");
                var classid = '<%=strUserGroup_IdActivity%>';
                var HwId = $(this).attr("HomeWork_Id");
                layer.open({
                    type: 2,
                    title: '布置作业',
                    area: ['680px', '555px'],
                    content: 'AddlayoutIframe.aspx?rtrId=' + rtrId + "&classId=" + classid + "&HomeWork_Id=" + HwId //iframe的url
                });
                e.preventDefault();
            });

            $('.mtree-class-hook').mtree({
                url: true,
                onClick: function (obj) {
                    var id = $(".mtree-checklist-hook .mtree-link-hook.active:eq(0)").attr("id");
                    window.location.href = "historyTestPaper.aspx?" + obj.data('ugid') + "&Two_WayChecklist_Id=" + id;
                }
            });

            $('.mtree-checklist-hook').mtree({
                onClick: function () {
                    loadData();
                }

            });

        })

        function loadData() {
            var Two_WayChecklist_Id = $(".mtree-checklist-hook .mtree-link-hook.active").attr("id");
            if (Two_WayChecklist_Id == "") {
                Two_WayChecklist_Id = $(".mtree-checklist-hook .mtree-link-hook:eq(0)").attr("id");
            }
            var dto = {
                Two_WayChecklist_Id: $(".mtree-checklist-hook .mtree-link-hook.active").attr("two_id"),
                UserGroupId: "<%=strUserGroup_IdActivity%>",
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };

            $.ajaxWebService("historyTestPaper.aspx/GetTestpaper", JSON.stringify(dto), function (data) {
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

        function pageselectCallback(page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }

        function classDisband(hwid, rtrId) {
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

        function btnZJ() {
            var Two_WayChecklist_Id_ = $(".mtree-checklist-hook .mtree-link-hook.active").attr("id");
            window.location.href = "simpleTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>&Two_WayChecklist_Id=" + Two_WayChecklist_Id_;
        }

        function btnYZSJ() {
            var Two_WayChecklist_Id_ = $(".mtree-checklist-hook .mtree-link-hook.active").attr("id");
            window.location.href = "historyTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>&Two_WayChecklist_Id=" + Two_WayChecklist_Id_;
        }
    </script>
</asp:Content>
