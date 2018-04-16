<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="ExerciseCorrect.aspx.cs" Inherits="Rc.Cloud.Web.teacher.ExerciseCorrect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript" src="../Scripts/json2.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/jq.pagination.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/jquery-jtemplates.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hidClassId" ClientIDMode="Static" />
    <div class="header_subnav">
        <ul>
            <%-- <li><a href="cCorrectHomework.aspx">按学生批改</a></li>
            <li><a href="ExerciseCorrect.aspx" class="active">按试题批改</a></li>--%>
        </ul>
    </div>
    <div class="main_box">
        <div class="left_sidebar">
            <div class="tree pt20">
                <ul>
                    <asp:Repeater runat="server" ID="rptClass" ClientIDMode="Static">
                        <ItemTemplate>
                            <li>
                                <div class="name">
                                    <a href='##' val="<%#Eval("UserGroup_Id") %>" title="<%#Eval("UserGroup_Name") %>"><%#Rc.Cloud.Web.Common.pfunction.GetSubstring(Eval("UserGroup_Name").ToString(),10,true) %>
                                        <span>(<%#Eval("UserGroup_Id") %>)</span>
                                    </a>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="right_main_box" data-name="main-auto" style="min-height: 685px;">
            <div class="center_sidebar">
                <h2 class="column_name">作业列表</h2>
                <ul class="tree tree_homework_list" id="tbSHW">
                </ul>
            </div>
            <div class="container_box rightmost_main">
                <div class="homework_state" data-name="homework_state"></div>
                <!-- <div class="correct_complete"><a href="##" class="create_btn">完成批改</a></div> -->
                <table class="table_list align_center">
                    <thead>
                        <tr>
                            <td width="60">题型</td>
                            <td class="align_left">题干</td>
                            <td width="50">分值</td>
                            <%--<td width="80">已批/全部</td>--%>
                            <td width="70">操作</td>
                        </tr>
                    </thead>
                    <tbody id="tbTQ">
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <textarea id="template_SHW" style="display: none">
    {#foreach $T.list as record}
    <li>
        <div class="name">
            {#if $T.record.HomeWork_Status=="0" } {#else}<span class="point"></span>{#/if}
            <a href='##' val="{$T.record.HomeWork_Id}" tt="{$T.record.CorrectModes}" rtrf="{$T.record.ResourceToResourceFolder_Id}" data-status="{$T.record.HomeWork_Status}">
                <span class="time">{$T.record.CreateTime}</span>
                <div class="hwname">{$T.record.HomeWork_Name}</div>
            </a>
            <%--{#if $T.record.IsUpdate=='yes'}
                {#if $T.record.HomeWork_Status=="0" }<span class="status"  onclick="ConfirmFinish('{$T.record.HomeWork_Id}')" title="点击完成批改">完成批改</span> {#else}<span class="status" onclick="CancelFinish('{$T.record.HomeWork_Id}')" title="点击取消完成">取消完成</span>{#/if}
            {#/if}--%>
        </div>
    </li>
    {#/for}
    </textarea>
    <textarea id="template_TQ" style="display: none">
    {#foreach $T.list as record}
    <tr>
        <td>{$T.record.TestQuestions_TypeName}</td>
        <td class="align_left">{$T.record.TestQuestionBody}</td>
        <td>{$T.record.TestQuestions_SumScore}</td>
        <%--<td>{#if $T.record.ConfirmCount<$T.record.StudentCount}{$T.record.ConfirmCount}{#else}{$T.record.StudentCount}{#/if}/{$T.record.StudentCount}</td>--%>
        <td class="table_opera">
            {#if $T.record.ConfirmCount==0}
                {#if $T.record.IsCorrect=='no'}
                    <a href='##' class="disabled">已完成</a>
                {#else}
                    <a href="ExerciseCorrectView.aspx?HomeWork_Id={$T.record.HomeWork_Id}&ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&TestQuestions_Id={$T.record.TestQuestions_Id}" target="_blank">批改</a>
                {#/if}
            {#else}
            <a href="ExerciseCorrectView.aspx?HomeWork_Id={$T.record.HomeWork_Id}&ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&TestQuestions_Id={$T.record.TestQuestions_Id}&correctStatus=1" target="_blank" class="success">已批改</a>
            {#/if}
            
        </td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(function () {
            pageIndex = 1;
            _homeworkId = (getUrlVar("HomeWorkId") == "" || getUrlVar("HomeWorkId") == undefined) ? "" : getUrlVar("HomeWorkId");
            _CorrectMode = "";
            _ClassId = (getUrlVar("ClassId") == "" || getUrlVar("ClassId") == undefined) ? "" : getUrlVar("ClassId");
            rtrfId = "";
            $(".left_sidebar .tree .name a").click(function () {
                $(".left_sidebar .tree .name").removeClass("active");
                var _val = $(this).attr("val");
                $("#hidClassId").val(_val);
                $(this).closest('.name').addClass("active");
                loadData();
                if (_ClassId != _val) {
                    _homeworkId = "";
                }
            });

            //载入页面左侧默认状态
            if (_ClassId != "") {
                $(".left_sidebar .tree .name a[val='" + _ClassId + "']").click();
            }
            else {
                $(".left_sidebar .tree .name:eq(0) a").click();
            }

            $("#tbSHW li a").live("click", function () {
                homeworkId = $(this).attr("val");
                rtrfId = $(this).attr("rtrf");
                CorrectMode = $(this).attr("tt");
                var _HomeworkStatus = parseInt($(this).attr("data-status"));
                var _PrevCorrectMode = $('#tbSHW li div.active a').attr('tt');
                $("#tbSHW li a").each(function () {
                    $(this).closest('.name').removeClass('active');
                })
                $(this).closest('.name').addClass('active');
                var flagRedirect = RedirectCorrectPage(CorrectMode, homeworkId, $("#hidClassId").val());
                if (CorrectMode != "" && !flagRedirect) {
                    loadHomeWorkTQData(homeworkId);
                    //已完成
                    if (_HomeworkStatus == 1) {
                        $('div[data-name="homework_state"]').addClass('success').removeClass('conduct').html('当前作业状态：已完成 <span onclick=\'CancelFinish("' + homeworkId + '")\'>设置作业状态为进行中</span>');
                    }
                    //未完成
                    if (_HomeworkStatus == 0) {
                        $('div[data-name="homework_state"]').addClass('conduct').removeClass('success').html('当前作业状态：进行中 <span onclick=\'ConfirmFinish("' + homeworkId + '")\'>设置作业状态为已完成</span>');
                    }
                }

                
            });
        });

        function ShowSubDocument(obj, _iid) {
            $(".left_homework_list li a").removeClass();
            $(obj).addClass("active");
            homeWork_Id = _iid;
            loadData();
        }
        var RedirectCorrectPage = function (CorrectMode, HomeWorkID, ClassId) {
            var flag = false;
            if (CorrectMode == "" || CorrectMode == undefined) {
                window.location.href = "CorrectMode.aspx?ClassId=" + ClassId + "&HomeWorkId=" + HomeWorkID;
                flag = true;
            }
            if (CorrectMode == "1") {
                if (_homeworkId != HomeWorkID) {
                    window.location.href = "ExerciseCorrect.aspx?ClassId=" + ClassId + "&HomeWorkId=" + HomeWorkID;
                    flag = true;
                }
            }
            if (CorrectMode == "2") {
                window.location.href = "cCorrectHomework.aspx?ClassId=" + ClassId + "&HomeWorkId=" + HomeWorkID;
                flag = true;
            }
            return flag;
        }
        var loadData = function () {
            var _classId = $("#hidClassId").val();
            var dto = {
                ClassId: _classId,
                x: Math.random()
            };
            $.ajaxWebService("ExerciseCorrect.aspx/GetCorrectHomework", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbSHW").setTemplateElement("template_SHW", null, { filter_data: false });
                    $("#tbSHW").processTemplate(json);

                    if (json.list == null || json.list == "") {
                        $("#tbSHW").html("<li><div class='name'><a href='##'>暂无数据</a></div></li>");
                        $("#tbTQ").html("<tr><td colspan='100'>暂无数据</td></tr>");
                    }
                    if (_homeworkId == "") {
                        $("#tbSHW li:eq(0) a").click();
                    }
                    else {
                        $("#tbSHW li a[val='" + _homeworkId + "']").click();
                    }
                }
                else {
                    $("#tbSHW").html("<li><div class='name'><a href='##'>暂无数据</a></div></li>");
                    $("#tbTQ").html("<tr><td colspan='100'>暂无数据</td></tr>");
                }
            }, function () { });
        }

        var loadHomeWorkTQData = function (homeworkId) {
            var dto = {
                HomeWork_Id: homeworkId,
                ResourceToResourceFolder_Id: rtrfId,
                x: new Date().getTime()
            };
            $.ajaxWebService("ExerciseCorrect.aspx/GetHomeworkTestQuestions", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbTQ").setTemplateElement("template_TQ", null, { filter_data: false });
                    $("#tbTQ").processTemplate(json);
                    if (json.list == null || json.list == "") {
                        $("#tbTQ").html("<tr><td colspan='100'>暂无数据</td></tr>");
                    }

                }
                else {
                    $("#tbTQ").html("<tr><td colspan='100'>暂无数据</td></tr>");
                }
            }, function () { $("#tbTQ").html("<tr><td colspan='100'>暂无数据</td></tr>"); });
        }
        var ConfirmFinish = function (Id) {
            var index = layer.confirm("确定要完成修改此次作业吗", { icon: 4, title: '提示' }, function () {
                layer.close(index);//关闭确认弹窗
                $.ajaxWebService("cCorrectHomework.aspx/UpdateS", "{Id:'" + Id + "',x:'" + Math.random() + "'}", function (data) {
                    if (data.d == "1") {
                        layer.msg('更新成功', { icon: 1, time: 1000 }, function () {
                            window.location.reload();
                            //loadData();
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
                }, false);
            });
        }
        var CancelFinish = function (Id) {
            var index = layer.confirm("确定要取消完成修改此次作业吗", { icon: 4, title: '提示' }, function () {
                layer.close(index);//关闭确认弹窗
                $.ajaxWebService("cCorrectHomework.aspx/UpdateC", "{Id:'" + Id + "',x:'" + Math.random() + "'}", function (data) {
                    if (data.d == "1") {
                        layer.msg('更新成功', { icon: 1, time: 1000 }, function () {
                            window.location.reload();
                            //loadData();
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
                }, false);
            });
        }

        //打开新窗口
        function OpenNew(Student_HomeWork_Id, HomeWork_Id) {
            var _url = "correctT.aspx?stuHomeWorkId=" + Student_HomeWork_Id + "&HomeWork_Id=" + HomeWork_Id + "&ClassId=" + $("#hidClassId").val();
            window.open(_url, 'newswindow');
        }

    </script>
</asp:Content>
