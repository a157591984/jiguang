<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/student.Master" AutoEventWireup="true" CodeBehind="oWrongHomework.aspx.cs" Inherits="Homework.student.oWrongHomework" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../plugin/laydate/laydate.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="oHomework.aspx">最新作业</a></li>
            <%if (Convert.ToBoolean(Rc.Common.ConfigHelper.GetConfigString("IsShowNewStuAnalysis")))
                { %>
            <li><a href="St_oHistoryHomework.aspx">历次作业</a></li>
            <%}
                else
                { %>
            <li><a href="oHistoryHomework.aspx">历次作业</a></li>
            <%} %>
            <li><a href="oWrongHomework.aspx" class="active">错题集</a></li>
            <li><a href="AchievementTrack.aspx">成绩跟踪</a></li>
            <%if (Convert.ToBoolean(Rc.Common.ConfigHelper.GetConfigString("IsShowNewStuAnalysis")))
                { %>
            <li><a href="CheckStuHWAnalysis.aspx">综合分析</a></li>
            <%} %>
        </ul>
    </div>
    <div class="iframe-container">
        <div class="container pt">
            <div class="filter">
                <div class="form-inline">
                    <div class="form-group">
                        <label>时间：</label>
                        <input class="form-control input-sm" id="StartTime" type="text" placeholder="开始时间" clientidmode="Static">
                        <input class="form-control input-sm" id="EndTime" type="text" placeholder="结束时间" clientidmode="Static">
                    </div>
                    <div class="form-group">
                        <input class="form-control input-sm" id="txtBookName" type="text" placeholder="书目名称" clientidmode="Static">
                        <input class="form-control input-sm" id="txtContentText" type="text" placeholder="知识点" clientidmode="Static">
                        <input class="form-control input-sm" id="txtTargetText" type="text" placeholder="测量目标" clientidmode="Static">
                    </div>
                    <input class="btn btn-primary btn-sm" id="btnSearch" type="button" value="确定" clientidmode="Static">
                </div>
                <div class="filter_section">
                    <div class="filter_row clearfix">
                        <span class="row_name">学科：</span>
                        <div class="row_item">
                            <ul data-name="subject">
                                <asp:Literal ID="ltlSubjectName" runat="server"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th width="250">作业名称</th>
                        <th width="250">作业来源</th>
                        <th>错题序号</th>
                        <th width="90">操作</th>
                    </tr>
                </thead>
                <tbody id="tbHW">
                </tbody>
            </table>
            <div class="pagination_container">
                <ul class="pagination">
                </ul>
            </div>
        </div>
    </div>
    <textarea id="template_HW" class="hidden">
    {#foreach $T.list as record}
    <tr>
        <td>{$T.record.HomeWork_Name}</td>
        <td>{$T.record.BookName}</td>
        <td>{$T.record.TestQuestionsNum}</td>
        <td>
            {#if $T.record.CorrectMode=='1'}
            <a href="ohomeworkview_client.aspx?wrong=w&HomeWork_Id={$T.record.HomeWork_Id}&StudentId={$T.record.Student_Id}&ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&Student_HomeWork_Id={$T.record.Student_HomeWork_Id}" target="_blank">错题详情</a>
            {#else}
            <a href="oHomeWorkViewTT.aspx?wrong=w&HomeWork_Id={$T.record.HomeWork_Id}&StudentId={$T.record.Student_Id}&ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}" target="_blank">错题详情</a>
            {#/if}
        </td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        function ShowSubDocument(strDoctumentID, obj) {
            homeWork_AssignTeacher = strDoctumentID;
            $(".left_tree_list div").removeClass();
            $(obj).parent().addClass("active");
            pageIndex = 1;
            loadData();

        }
        var loadData = function () {
            var dto = {
                isWrong: "true",
                SubjectId: $('[data-name="subject"]').find('a.active').attr('SubjectId'),
                BookName: $("#txtBookName").val(),
                HomeWorkCreateTime: $("#StartTime").val(),
                HomeWorkFinishTime: $("#EndTime").val(),
                ContentText: $("#txtContentText").val(),
                TargetText: $("#txtTargetText").val(),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("oWrongHomework.aspx/GetoHomework", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbHW").setTemplateElement("template_HW", null, { filter_data: false });
                    $("#tbHW").processTemplate(json);
                    $(".pagination_container").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbHW").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
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
        //var loadContentText = function () {
        //    var dto = {

        //        x: Math.random()
        //    };
        //    $.ajaxWebService("oWrongHomework.aspx/GetContentText", JSON.stringify(dto), function (data) {
        //        if (data.d != "") {
        //            $('[ajax-name="ContentText"]').html(data.d);
        //        }
        //    }, function () { }, false);

        //}
        //var loadTargetText = function () {
        //    var dto = {

        //        x: Math.random()
        //    };
        //    $.ajaxWebService("oWrongHomework.aspx/GetTargetText", JSON.stringify(dto), function (data) {
        //        if (data.d != "") {
        //            $('[ajax-name="TargetText"]').html(data.d);
        //        }
        //    }, function () { }, false);

        //}
        $(function () {
            pageIndex = 1;
            homeWork_AssignTeacher = $(".left_tree_list a:eq(0)").attr("tt");
            docName = "";
            ShowFolderIn = "1";

            $('[data-name="subject"] li a').removeClass("active");
            $('[data-name="subject"] li a').eq(0).addClass("active");
            $('[data-name="subject"] li a').bind({
                click: function () {
                    $('[data-name="subject"] li a').removeClass("active").eq(0).addClass("active");
                    SubjectId = $(this).attr("SubjectId");
                    $('[data-name="subject"] li a').removeClass("active");
                    $(this).addClass("active");
                    loadData();

                }
            });
            //$("#ddlBookName").change(function () {
            //    loadData();
            //})
            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();

            })
            //$("#ddlContentText").change(function () {
            //    pageIndex = 1;
            //    loadData();
            //})
            //$("#ddlTargetText").change(function () {
            //    pageIndex = 1;
            //    loadData();
            //})
            //loadContentText();
            //loadTargetText();
            loadData();
        })
        $(function () {
            // 日期
            var StarTime = {
                elem: '#StartTime',
                format: 'YYYY-MM-DD',
                choose: function (datas) {
                    EndTime.min = datas; //开始日选好后，重置结束日的最小日期
                    EndTime.start = datas //将结束日的初始值设定为开始日
                }
            }
            var EndTime = {
                elem: '#EndTime',
                format: 'YYYY-MM-DD',
                choose: function (datas) {
                    StarTime.max = datas; //结束日选好后，重置开始日的最大日期
                }
            }

            laydate(StarTime);
            laydate(EndTime);

        })


    </script>
</asp:Content>
