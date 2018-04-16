<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="cCorrectHomework.aspx.cs" Inherits="Homework.teacher.cCorrectHomework" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hidClassId" ClientIDMode="Static" />
    <div class="iframe-container correct_homework">
        <div class="iframe-sidebar" id="sidebar">
            <div class="mtree mtree-class-hook">
                <ul>
                    <asp:Repeater runat="server" ID="rptClass" ClientIDMode="Static">
                        <ItemTemplate>
                            <li>
                                <div class="mtree_link mtree-link-hook" val="<%#Eval("UserGroup_Id") %>" title="<%#Eval("UserGroup_Name") %>">
                                    <div class="mtree_indent mtree-indent-hook"></div>
                                    <div class="mtree_btn mtree-btn-hook"></div>
                                    <div class="mtree_name mtree-name-hook">
                                        <%#Rc.Cloud.Web.Common.pfunction.GetSubstring(Eval("UserGroup_Name").ToString(),10,true) %>(<%#Eval("UserGroup_Id") %>)
                                    </div>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="iframe-main">
            <div class="iframe-main-sidebar">
                <div class="page_title pv">作业列表</div>
                <div class="mtree mtree-homework-hook homework_list">
                    <ul data-level="1" id="tbSHW"></ul>
                </div>
            </div>
            <div class="iframe-main-section pa">
                <div class="alert homework_state homework-state-hook hidden"></div>
                <div class="row">
                    <div class="col-xs-10">
                        <div class="page_title">已交学生&nbsp;&nbsp;<span data-name="Statistics"></span></div>
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>姓名</th>
                                    <th>打开时间</th>
                                    <th>提交时间</th>
                                    <th>批改人</th>
                                    <th>状态</th>
                                    <th>得分</th>
                                </tr>
                            </thead>
                            <tbody id="ulYJ">
                            </tbody>
                        </table>
                    </div>
                    <div class="col-xs-2">
                        <div class="page_title">未交学生</div>
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>姓名</th>
                                </tr>
                            </thead>
                            <tbody id="ulWJ"></tbody>
                        </table>
                    </div>
                </div>
            </div>
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
                        <div class="not_approved not-approved-hook">{#if $T.record.StudentCountMTJ==0}{#else}<i class="num-hook">{$T.record.StudentCountMTJ}</i>&nbsp;人未批{#/if}</div>
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
    </textarea>
    <textarea id="template_YJ" class="hidden">
        {#foreach $T.listYJ as record}
            <tr data-score="{$T.record.Student_Score}" data-status="{$T.record.Student_HomeWork_CorrectStatus}">
                <td>{$T.record.TrueName}</td>
                <td>{$T.record.OpenTime}</td>
                <td>{$T.record.Student_AnswerTime}</td>
                 <td>{$T.record.CourrectName}</td>
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
    <textarea id="template_WJ" class="hidden">
        {#foreach $T.listWJ as record}
            <tr>
        	    <td>{$T.record.TrueName}</td>
            </tr>
        {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            CountYJWP = 0;
            pageIndex = 1;
            _HomeworkId = (getUrlVar("HomeWorkId") == "" || getUrlVar("HomeWorkId") == undefined) ? "" : getUrlVar("HomeWorkId");
            _CorrectMode = "";
            _ClassId = (getUrlVar("ClassId") == "" || getUrlVar("ClassId") == undefined) ? "" : getUrlVar("ClassId");

            $('.mtree-class-hook').mtree({
                onClick: function (obj) {
                    var _val = obj.attr("val");
                    $("#hidClassId").val(_val);
                    loadData();
                },
                onLoad: function (obj) {
                    obj.find('.mtree-link-hook:eq(0)').click();
                }
            });

        });
        function ShowSubDocument(obj, _iid) {
            $(".left_homework_list li a").removeClass();
            $(obj).addClass("active");
            homeWork_Id = _iid;
            loadData();
        }
        function loadData() {
            var _classId = $("#hidClassId").val();
            var dto = {
                ClassId: _classId,
                x: Math.random()
            };
            $.ajaxWebService("cCorrectHomework.aspx/GetCorrectHomework", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbSHW").setTemplateElement("template_SHW", null, { filter_data: false });
                    $("#tbSHW").processTemplate(json);

                    if (json.list == null || json.list == "") {
                        $("#tbSHW").html("<li><div class='mtree_nodata text-center'>暂无数据</div></li>");
                    }
                    $('.homework-state-hook').removeClass('hidden');
                }
                else {
                    $("#tbSHW").html("<li><div class='mtree_nodata text-center'>暂无数据</div></li>");
                    $("#ulYJ").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $("#ulWJ").html("<tr><td colspan='100'>暂无数据</td></tr>");
                }
                homework();
            }, function () { });
        }

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
                        $('.homework-state-hook').addClass('alert-success').removeClass('alert-default').html('班级作业状态：已完成 <button type="button" class="btn btn-default btn-sm" onclick=\'CancelFinish("' + _PravitehomeworkId + '")\'>标记为未批改</button> <a href="javascript:void(0);" onclick=\'assignCorrect("' + _PravitehomeworkId + '")\' target="_blank" class="btn btn-default btn-sm assignCorrect">分配给学生批改</a>');
                    }
                    //未完成
                    if (_HomeworkStatus == 0) {
                        $('.homework-state-hook').addClass('alert-default').removeClass('alert-success').html('班级作业状态：进行中 <button type="button" class="btn btn-success btn-sm" onclick=\'ConfirmFinish("' + _PravitehomeworkId + '")\'>标记为已批改</button> <a href="javascript:void(0);" onclick=\'assignCorrect("' + _PravitehomeworkId + '")\' target="_blank" class="btn btn-default btn-sm assignCorrect">分配给学生批改</a>');
                    }
                },
                onLoad: function (obj) {
                    obj.find('.mtree-link-hook:eq(0)').click();
                }
            });
        }

        function loadStudentHomeWorkData() {
            var _classId = $("#hidClassId").val();
            var dto = {
                HomeworkId: _HomeworkId,
                IsCorrect: _CorrectMode,
                ClassId: _classId,
                x: Math.random()
            };

            $.ajaxWebService("cCorrectHomework.aspx/GetStudentHomework", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#ulYJ").setTemplateElement("template_YJ", null, { filter_data: false });
                    $("#ulYJ").processTemplate(json);
                    if (json.listYJ == null || json.listYJ == "") {
                        $("#ulYJ").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    }
                    else {
                        if ($('#ulYJ tr').size() == $('#ulYJ tr[data-status="1"]').size()) {
                            sortFilter("desc");
                        }
                    }

                    $("#ulWJ").setTemplateElement("template_WJ", null, { filter_data: false });
                    $("#ulWJ").processTemplate(json);
                    $('[data-name="Statistics"]').html("(" + json.listYJ.length + "/" + (json.listYJ.length + json.listWJ.length) + ")");
                    if (json.listWJ == null || json.listWJ == "") {
                        $("#ulWJ").html("<tr><td colspan='100'>暂无数据</td></tr>");
                    }

                }
                else {
                    $("#ulYJ").html("<tr><td colspan='100'>暂无数据</td></tr>");
                    $("#ulWJ").html("<tr><td colspan='100'>暂无数据</td></tr>");
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
        function ConfirmFinish(Id) {
            var index = layer.confirm("确定要标记为已批改吗？", { icon: 4, title: '提示' }, function () {
                layer.close(index);
                layer.load();
                $.ajaxWebService("cCorrectHomework.aspx/UpdateS", "{Id:'" + Id + "',x:'" + Math.random() + "'}", function (data) {
                    if (data.d == "1") {
                        layer.ready(function () {
                            layer.msg('更新成功', { icon: 1, time: 1000 }, function () {
                                $('.mtree-homework-hook .mtree-link-hook.active .badge-hook').removeClass('danger hidden').html('已批');
                                $('.homework-state-hook').addClass('alert-success').removeClass('alert-default').html('班级作业状态：已完成 <button type="button" class="btn btn-default btn-sm" onclick=\'CancelFinish("' + Id + '")\'>标记为未批改</button> <a href="javascript:void(0);" onclick=\'assignCorrect("' + Id + '")\' target="_blank" class="btn btn-default btn-sm assignCorrect">分配给学生批改</a>');
                                var aObj = $('.mtree-homework-hook .mtree-link-hook.active');
                                $(aObj).attr({
                                    "IsCorrect": "no",
                                    "data-status": "1"
                                });
                                _HomeworkId = Id;
                                _CorrectMode = $(aObj).attr("IsCorrect");
                                loadStudentHomeWorkData();
                            })
                        })
                        return false;
                    }
                    else {
                        layer.msg('失败！', { icon: 2 });
                        return false;
                    }
                }, function () {
                    layer.msg('失败！', { icon: 2 });
                    return false;
                });
            });
        }
        function CancelFinish(Id) {
            var index = layer.confirm("确定要标记为未批改吗？", { icon: 4, title: '提示' }, function () {
                layer.close(index);//关闭确认弹窗
                layer.load();
                $.ajaxWebService("cCorrectHomework.aspx/UpdateC", "{Id:'" + Id + "',x:'" + Math.random() + "'}", function (data) {
                    if (data.d == "1") {
                        layer.msg('更新成功', { icon: 1, time: 1000 }, function () {
                            $('.mtree-homework-hook .mtree-link-hook.active .badge-hook').addClass('hidden');
                            $('.homework-state-hook').addClass('alert-default').removeClass('alert-success').html('班级作业状态：进行中 <button type="button" class="btn btn-success btn-sm" onclick=\'ConfirmFinish("' + Id + '")\'>标记为已批改</button> <a href="javascript:void(0);" onclick=\'assignCorrect("' + Id + '")\' target="_blank" class="btn btn-default btn-sm assignCorrect">分配给学生批改</a>');
                            var aObj = $('.mtree-homework-hook .mtree-link-hook.active');
                            $(aObj).attr({
                                "IsCorrect": "yes",
                                "data-status": "0"
                            });
                            _HomeworkId = Id;
                            _CorrectMode = $(aObj).attr("IsCorrect");
                            loadStudentHomeWorkData();
                        })
                        return false;
                    }
                    else {
                        layer.msg('失败3！', { icon: 2 });
                        return false;
                    }
                }, function () {
                    layer.msg('失败2！', { icon: 2 });
                    return false;
                });
            });
        }
        //分配
        function assignCorrect(Id) {
            $(".assignCorrect").attr("href", "javascript:void(0);");
            if (CountYJWP > 1) {
                $(".assignCorrect").attr("href", "assignCorrect.aspx?HomeWork_Id=" + Id);
                return true;
            }
            else {
                layer.msg('未批改的学生小于2人无法分配给学生批改', { icon: 2, time: 2000 });
                return false;
            }
        }
        $('#test').on('click', function () {
            modifyNoCorrectCount();
        })
        function modifyNoCorrectCount() {
            var $active = $('#tbSHW').find('.mtree-link-hook.active');
            var activeLen = $active.length;
            if (activeLen > 0) {
                var numLen = $active.find('.num-hook').length;
                if (numLen > 0) {
                    var num = parseInt($active.find('.num-hook').text()) - 1;
                    if (num <= 0) {
                        $active.find('.not-approved-hook').remove();
                        $active.find('.badge-hook').remove();
                    } else {
                        $active.find('.num-hook').text(num);
                    }
                }
            }
            //var countHtml = $.trim($("#tbSHW div[class*='active'] i").html());
            //if (countHtml != "") {
            //    countHtml = countHtml.replace("【未批", "").replace("人】", "");
            //    var num = parseInt(countHtml) - 1;
            //    if (num <= 0) {
            //        countHtml = "";
            //        $("#tbSHW div[class*='active'] .point").remove();
            //    }
            //    else {
            //        countHtml = "【未批" + num + "人】";
            //    }
            //    $("#tbSHW div[class*='active'] i").html(countHtml);
            //}
        };

        //打开新窗口
        //function OpenNew(Student_HomeWork_Id, HomeWork_Id, CorrectMode) {
        //    var _url = "";
        //    if (CorrectMode == "1") {//客户端批改
        //        _url = "correct_client.aspx?stuHomeWorkId=" + Student_HomeWork_Id + "&HomeWork_Id=" + HomeWork_Id + "&ClassId=" + $("#hidClassId").val();
        //    }
        //    else {//web端批改
        //        _url = "correctT.aspx?stuHomeWorkId=" + Student_HomeWork_Id + "&HomeWork_Id=" + HomeWork_Id + "&ClassId=" + $("#hidClassId").val();
        //    }

        //    window.open(_url, 'newswindow');
        //}
    </script>
</asp:Content>
