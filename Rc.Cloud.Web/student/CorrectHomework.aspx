<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/student.Master" AutoEventWireup="true" CodeBehind="CorrectHomework.aspx.cs" Inherits="Rc.Cloud.Web.student.CorrectHomework" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container correct_homework">
        <div class="iframe-sidebar">
            <div class="mtree mtree-homework-hook homework_list">
                <ul id="tbSHW"></ul>
            </div>
        </div>
        <div class="iframe-main pa">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th width="200">姓名</th>
                        <th width="180">打开时间</th>
                        <th width="180">提交时间</th>
                        <th>状态</th>
                        <th width="120">得分</th>
                    </tr>
                </thead>
                <tbody id="ulYJ">
                </tbody>
            </table>
        </div>
    </div>
    <textarea id="template_SHW" class="hidden">
    {#foreach $T.list as record}
        <li>
            <div class="mtree_link mtree-link-hook" val="{$T.record.HomeWork_Id}" tt="2" IsCorrect="{$T.record.IsCorrect}" data-status ="{$T.record.HomeWork_Status}" CountYJWP="{$T.record.StudentCountMTJ}">
                <div class="mtree_indent mtree-indent-hook"></div>
                <div class="mtree_btn mtree-btn-hook"></div>
                <div class="mtree_name mtree-name-hook">
                    <div class="desc clearfix">
                        <div class="time">{$T.record.CreateTime}</div>
                        <div class="not_approved">{#if $T.record.StudentCountMTJ==0}{#else}{$T.record.StudentCountMTJ}&nbsp;人未批{#/if}</div>
                    </div>
                    <div class="name">
                        {#if $T.record.StudentCountMTJ!="0"}
                            <span class="badge badge-hook danger">未批</span>
                        {#else}
                            {#if $T.record.HomeWork_Status=="0" }
                                <span class="badge badge-hook"></span>
                            {#else}
                                <span class="badge badge-hook">已批</span>
                            {#/if}
                        {#/if}
                        {$T.record.HomeWork_Name}</div>
                </div>
            </div>
        </li>
    {#/for}

    <%--{#foreach $T.list as record}
    <li>
        <div class="name">
            {#if $T.record.StudentCountMTJ!="0"}
                <span class="point red"></span>
            {#else}
                {#if $T.record.HomeWork_Status=="0" }
                {#else}
                    <span class="point"></span>
                {#/if}
            {#/if}
            <a href='##' val="{$T.record.HomeWork_Id}" tt="2" IsCorrect="{$T.record.IsCorrect}" data-status ="{$T.record.HomeWork_Status}">
                <span class="time">{$T.record.CreateTime}</span>
                <div class="hwname">{$T.record.HomeWork_Name}{#if $T.record.StudentCountMTJ==0}{#else}<i>【未批{$T.record.StudentCountMTJ}人】</i>{#/if}&nbsp;</div>
            </a>
        </div>
    </li>
    {#/for}--%>
    </textarea>
    <textarea id="template_YJ" class="hidden">
        {#foreach $T.listYJ as record}
            <tr data-score="{$T.record.Student_Score}" data-status="{$T.record.Student_HomeWork_CorrectStatus}">
                <td>{$T.record.TrueName}</td>
                <td>{$T.record.OpenTime}</td>
                <td>{$T.record.Student_AnswerTime}</td>
                <td>
                    <a href="{$T.record.url}" target="_blank">
                    {#if $T.record.Student_HomeWork_CorrectStatus==1}<span class="text-success">已批改({$T.record.CorrectTime})</span>{#else}
                        <span class="text-danger">未批改</span>
                    {#/if}
                    </a>               
                </td>
                <td>{$T.record.Student_Score}分</td>
            </tr>
        {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            loadData();
            $(document).on("click", "#tbSHW li a", function () {
                var _PravitehomeworkId = $(this).attr("val");
                var _HomeworkStatus = parseInt($(this).attr("data-status"));
                var _PrevCorrectMode = $('#tbSHW li div.active a').attr('tt');
                $("#tbSHW li a").each(function () {
                    $(this).closest('.name').removeClass('active');
                })
                $(this).closest('.name').addClass('active');
                _HomeworkId = $(this).attr("val");
                _CorrectMode = $(this).attr("IsCorrect");
                loadStudentHomeWorkData();
            });

        })

        function ShowSubDocument(obj, _iid) {
            $(".left_homework_list li a").removeClass();
            $(obj).addClass("active");
            homeWork_Id = _iid;
            loadData();
        }
        function loadData() {
            var dto = {
                x: Math.random()
            };
            $.ajaxWebService("CorrectHomework.aspx/GetCorrectHomework", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbSHW").setTemplateElement("template_SHW", null, { filter_data: false });
                    $("#tbSHW").processTemplate(json);

                    if (json.list == null || json.list == "") {
                        $("#tbSHW").html("<li><div class='mtree_nodata text-center'>暂无数据</div></li>");
                    }
                    //$("#tbSHW li:eq(0) a").click();
                }
                else {
                    $("#tbSHW").html("<li><div class='mtree_nodata text-center'>暂无数据</div></li>");
                }
                homework();
            }, function () { });
        }
        function loadStudentHomeWorkData() {
            var dto = {
                HomeworkId: _HomeworkId,
                IsCorrect: _CorrectMode,
                x: Math.random()
            };
            $.ajaxWebService("CorrectHomework.aspx/GetStudentHomework", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#ulYJ").setTemplateElement("template_YJ", null, { filter_data: false });
                    $("#ulYJ").processTemplate(json);
                    if (json.listYJ == null || json.listYJ == "") {
                        $("#ulYJ").html("<tr><td colspan='100'>暂无数据</td></tr>");
                    }
                    else {
                        if ($('#ulYJ tr').size() == $('#ulYJ tr[data-status="1"]').size()) {
                            sortFilter("desc");
                        }
                    }
                }
                else {
                    $("#ulYJ").html("<tr><td colspan='100'>暂无数据</td></tr>");
                }
            }, function () { });
        }
        function sortFilter(sortType) {
            if ($("#ulYJ tr").length > 0) {
                var flt_list = $("#ulYJ tr").toArray().sort(function (a, b) {

                    if (sortType == "asc") {
                        return parseInt($(a).attr("data-score")) - parseInt($(b).attr("data-score"));
                    } else {
                        return parseInt($(b).attr("data-score")) - parseInt($(a).attr("data-score"));
                    }
                });
                $(flt_list).appendTo($("#ulYJ"));
            }
        }
        function modifyNoCorrectCount() {
            var countHtml = $.trim($("#tbSHW div[class*='active'] i").html());
            if (countHtml != "") {
                countHtml = countHtml.replace("【未批", "").replace("人】", "");
                var num = parseInt(countHtml) - 1;
                if (num <= 0) {
                    countHtml = "";
                    $("#tbSHW div[class*='active'] .point").remove();
                }
                else {
                    countHtml = "【未批" + num + "人】";
                }
                $("#tbSHW div[class*='active'] i").html(countHtml);
            }
        };

        function homework() {
            $('.mtree-homework-hook').mtree({
                onClick: function (obj) {
                    var _PravitehomeworkId = obj.attr("val");
                    var _HomeworkStatus = parseInt(obj.attr("data-status"));
                    var _PrevCorrectMode = obj.attr('tt');
                    _HomeworkId = obj.attr("val");
                    _CorrectMode = obj.attr("IsCorrect");
                    CountYJWP = obj.attr('CountYJWP');
                    loadStudentHomeWorkData();
                    //已完成
                    if (_HomeworkStatus == 1) {
                        $('.homework-state-hook').addClass('alert-success').removeClass('alert-default').html('班级作业状态：已完成 <button type="button" class="btn btn-default btn-sm" onclick=\'CancelFinish("' + _PravitehomeworkId + '")\'>标记为未批改</button> <a href="javascript:;" onclick=\'assignCorrect("' + _PravitehomeworkId + '")\' target="_blank" class="btn btn-default btn-sm assignCorrect">分配给学生批改</a>');
                    }
                    //未完成
                    if (_HomeworkStatus == 0) {
                        $('.homework-state-hook').addClass('alert-default').removeClass('alert-success').html('班级作业状态：进行中 <button type="button" class="btn btn-success btn-sm" onclick=\'ConfirmFinish("' + _PravitehomeworkId + '")\'>标记为已批改</button> <a href="javascript:;" onclick=\'assignCorrect("' + _PravitehomeworkId + '")\' target="_blank" class="btn btn-default btn-sm assignCorrect">分配给学生批改</a>');
                    }
                },
                onLoad: function (obj) {
                    obj.find('.mtree-link-hook:eq(0)').click();
                }
            });
        }
    </script>
</asp:Content>
