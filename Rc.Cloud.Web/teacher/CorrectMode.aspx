<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="CorrectMode.aspx.cs" Inherits="Rc.Cloud.Web.teacher.CorrectMode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript" src="../Scripts/json2.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/jq.pagination.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/jquery-jtemplates.js"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            pageIndex = 1;
            _homeworkId = getUrlVar("HomeWorkId") == "" || getUrlVar("HomeWorkId") == undefined ? "" : getUrlVar("HomeWorkId");
            _CorrectMode = "";
            _PravitehomeworkId = "";
            _ClassId = getUrlVar("ClassId") == "" || getUrlVar("ClassId") == undefined ? "" : getUrlVar("ClassId");
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
                $("#tbSHW li a").each(function () {
                    $(this).closest('.name').removeClass('active');
                })
                $(this).closest('.name').addClass('active');
                _PravitehomeworkId = $(this).attr("val");
                _CorrectMode = $(this).attr("tt");
                _IsCorrect = $(this).attr("IsCorrect");
                $("#btnUpdate").hide();
                if (_IsCorrect == "yes") {
                    $("#btnUpdate").show();
                }
                else {
                    var val = $('input:radio[name="mode"]:checked').val();
                    if (val == "2") {
                        $("#btnUpdate").show();
                    }
                    else {
                        $("#btnUpdate").hide();
                        layer.alert("学生提交作业时间未到，还剩" + _IsCorrect + "分钟才能使用此模式批改.", {
                             closeBtn: 0
                        })
                    }
                }
                RedirectCorrectPage(_CorrectMode, _PravitehomeworkId, $("#hidClassId").val());
            });

            $("[data-name='Choice'] input[type='radio']").click(function () {
                if (_IsCorrect == "yes") {
                    $("#btnUpdate").show();
                }
                else {
                    var val = $('input:radio[name="mode"]:checked').val();
                    if (val == "2") {
                        $("#btnUpdate").show();
                    }
                    else {
                        $("#btnUpdate").hide();
                        layer.alert("学生提交作业时间未到，还剩" + _IsCorrect + "分钟才能使用此模式批改.", {
                             closeBtn: 0
                        })
                      
                    }
                }
            })

            $("#btnUpdate").click(function () {
                var _checkMode;
                $("[data-name='Choice'] input[type='radio']").each(function () {
                    //var chk = $(this).find("[checked]");
                    if (this.checked) {
                        _checkMode = $(this).val();
                        $("#hidCorrectMode").val($(this).val());
                    }
                });
                layer.confirm('您确定要按【' + (_checkMode == "1" ? "试题" : "学生") + '】模式批改吗？<br>选择后不可更改，仅对本次作业有效。', { icon: 4 }, function () {
                    UpdateHomeWork(_PravitehomeworkId, $("#hidCorrectMode").val(), $("#hidClassId").val());
                })


            })
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header_subnav"></div>
    <asp:HiddenField runat="server" ID="hidClassId" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hidCorrectMode" ClientIDMode="Static" />
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
            <div class="container_box rightmost_main create_mode" data-name='Choice'>
                <ul>
                    <h3 class="warning_tip"><i class="fa fa-exclamation-triangle"></i>批改模式选定后不可更改，请慎重选择</h3>
                    <li>
                        <label for="StudentCreate">
                            <div class="radio">
                                <input type="radio" value="2" name="mode" checked="checked" id="StudentCreate">
                            </div>
                            <div class="comments">
                                <h2>按学生批改</h2>
                                <p>按学生批改时，即单个学生批改，老师看某个学生整篇作业进行批改，知道所有的学生都批改完成</p>
                            </div>
                        </label>
                    </li>
                    <li>
                        <label for="ExercizeCreate">
                            <div class="radio">
                                <input type="radio" value="1" name="mode" id="ExercizeCreate">
                            </div>
                            <div class="comments">
                                <h2>按试题批改</h2>
                                <p>按试题批改时，即批量批改，老师可以看到某一道题所有学生的答案，进行批量批改，直到作业的所有试题全部批改完成</p>
                            </div>
                        </label>
                    </li>

                </ul>
                <div class="btn"><a href='##' id="btnUpdate" class="create_btn">确定</a></div>

            </div>
        </div>
    </div>
    <textarea id="template_SHW" style="display: none">
    {#foreach $T.list as record}
    <li>
        <div class="name">
            <a href='##' val="{$T.record.HomeWork_Id}" tt="{$T.record.CorrectModes}" IsCorrect="{$T.record.IsCorrect}">
                <span class="time">{$T.record.CreateTime}</span>
                <div class="hwname">{$T.record.HomeWork_Name}</div>
            </a>
        </div>
    </li>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">
        function ShowSubDocument(obj, _iid) {
            $(".left_homework_list li a").removeClass();
            $(obj).addClass("active");
            homeWork_Id = _iid;
            loadData();
        }
        var loadData = function () {
            var _classId = $("#hidClassId").val();
            var dto = {
                ClassId: _classId,
                x: Math.random()
            };
            $.ajaxWebService("CorrectMode.aspx/GetCorrectHomework", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbSHW").setTemplateElement("template_SHW", null, { filter_data: false });
                    $("#tbSHW").processTemplate(json);

                    if (json.list == null || json.list == "") {
                        $("#tbSHW").html("<li><div class='name'><a href='##'>暂无数据</a></div></li>");
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
                    $("#ulYJ").html("<tr><td colspan='100'>暂无数据</td></tr>");
                    $("#ulWJ").html("<tr><td colspan='100'>暂无数据</td></tr>");
                    $("#btnUpdate").hide();
                }
            }, function () { });
        }
        var RedirectCorrectPage = function (CorrectMode, HomeWorkID, ClassId) {
            if (CorrectMode == "1") {
                window.location.href = "ExerciseCorrect.aspx?ClassId=" + ClassId + "&HomeWorkId=" + HomeWorkID;
            }
            if (CorrectMode == "2") {
                window.location.href = "cCorrectHomework.aspx?ClassId=" + ClassId + "&HomeWorkId=" + HomeWorkID;
            }
        }
        var UpdateHomeWork = function (HomeWorkID, CorrectMode, ClassId) {
            var dto = {
                HomeWorkID: HomeWorkID,
                CorrectMode: CorrectMode,
                x: Math.random()
            };
            $.ajaxWebService("CorrectMode.aspx/UpdateHomeWork", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    RedirectCorrectPage(CorrectMode, HomeWorkID, ClassId);
                }
                else {
                    layer.msg('失败', { icon: 2, time: 2000 });
                }
            }, function () { alert("异常"); }, false);
        };
    </script>
</asp:Content>
