<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectQuestionTest.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.SelectQuestionTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>选择知识点</title>
    <link rel="stylesheet" href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../SysLib/css/style.css" />
    <link href="../SysLib/plugin/mfilter-1.0/mfilter.css" rel="stylesheet" />
    <script type="text/javascript" src="../SysLib/js/jquery.min-1.11.1.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/function.js"></script>

    <script src="../SysLib/plugin/mfilter-1.0/mfilter.js"></script>
    <script src="../SysLib/js/jquery-jtemplates.js"></script>
    <script type="text/javascript">
        $(function () {
            _gradeTerm = "";
            _objTD = null;
            _parentId = "";
            _parentIdStr = "";

            //加载默认学科
            loadSubDict("722CE025-A876-4880-AAC1-5E416F3BDB1E", "934A3541-116E-438C-B9BA-4176368FCD9B", "<%=GradeTerm%>", $('[ajax-name="Subject"]'), "<%=Subject%>");
            //默认考纲 

            loadSubDict("934A3541-116E-438C-B9BA-4176368FCD9B", "3EF9506E-4C4B-407E-AA5D-451E0B20F0DB", "<%=Subject%>", $('[ajax-name="Syllabus"]'), "");

            //默认考试类别
            loadSubDict("3EF9506E-4C4B-407E-AA5D-451E0B20F0DB", "3EF9506E-4C4B-407E-AA5D-451E0B20F0DC", $('[ajax-name="Syllabus"]').find('a.active').attr('ajax-value'), $('[ajax-name="Test_Category"]'), "");

            $('[data-name="mfilter"]').mfilter({
                onClick: function (obj) {
                    var name = $(obj).closest('[data-name="mfilterItem"]').attr('ajax-name');
                    var val = $(obj).attr('ajax-value');
                    switch (name) {
                        case 'GradeTerm':
                            loadSubDict("722CE025-A876-4880-AAC1-5E416F3BDB1E", "934A3541-116E-438C-B9BA-4176368FCD9B", val, $('[ajax-name="Subject"]'), "");
                            break;
                        case 'Subject':
                            loadSubDict("934A3541-116E-438C-B9BA-4176368FCD9B", "3EF9506E-4C4B-407E-AA5D-451E0B20F0DB", val, $('[ajax-name="Syllabus"]'), "");
                            break;
                        case 'Syllabus':
                            loadSubDict("3EF9506E-4C4B-407E-AA5D-451E0B20F0DB", "3EF9506E-4C4B-407E-AA5D-451E0B20F0DC", val, $('[ajax-name="Test_Category"]'), "");
                            break;
                        case 'Test_Category':
                            loadData();
                            break;
                    }
                },
                onLoad: function (obj) {
                    //$('[ajax-name="Test_Category"] a:eq(0)').click();
                }
            });
            //$('[ajax-name="GradeTerm"] a').click(function () {
            //    _gradeTerm = $(this).attr("ajax-value");
            //    loadSubDict("722CE025-A876-4880-AAC1-5E416F3BDB1E", "934A3541-116E-438C-B9BA-4176368FCD9B", $(this).attr("ajax-value"), $('[ajax-name="Subject"]'), "");
            //});
            //$(document).on('click', '[ajax-name="Subject"] a', function () {
            //    loadSubDict("934A3541-116E-438C-B9BA-4176368FCD9B", "3EF9506E-4C4B-407E-AA5D-451E0B20F0DB", $(this).attr("ajax-value"), $('[ajax-name="Syllabus"]'), "");
            //});
            //$(document).on('click', '[ajax-name="Syllabus"] a', function () {
            //    loadSubDict("3EF9506E-4C4B-407E-AA5D-451E0B20F0DB", "3EF9506E-4C4B-407E-AA5D-451E0B20F0DC", $(this).attr("ajax-value"), $('[ajax-name="Test_Category"]'), "");
            //});
            //$('[ajax-name="Test_Category"] a:eq(0)').click();
            loadData();
            //$(document).on('click', '[ajax-name="Test_Category"] a', function () {
            //    loadData();
            //});

            $(document).on('click', '.che', function () {
                var _scoreid = $(this).data('scoreid');
                var _tkid = $(this).data('tkid');
                var _attrid = $(this).val();
                if (this.checked) {
                    //增加
                    AddAtrr(_scoreid, _attrid, _tkid);
                }
                else {
                    //删除
                    DeleteAtrr(_tkid);
                }
            });
            $("#btnSave").click(function () {
                var scoreId = "<%=ScoreId%>";
                window.parent.GetQuestionTest(scoreId);
                window.parent.layer.closeAll();
            })
        })
        //增加考点
        var AddAtrr = function (ScoreId, AttrId, TestQuestions_Knowledge_ID) {
            var dto = {
                ScoreId: ScoreId,
                AttrId: AttrId,
                TestQuestions_Knowledge_ID: TestQuestions_Knowledge_ID,
                x: Math.random()
            };
            $.ajaxWebService("SelectQuestionTest.aspx/AddAtrr", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                }
                else {
                    layer.msg("异常", { time: 2000, icon: 2 });
                }
            }, function () { });
        }
        //删除考点
        var DeleteAtrr = function (TestQuestions_Knowledge_ID) {
            var dto = {
                TestQuestions_Knowledge_ID: TestQuestions_Knowledge_ID,
                x: Math.random()
            };
            $.ajaxWebService("SelectQuestionTest.aspx/DeleteAtrr", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                }
                else {
                    layer.msg("异常", { time: 2000, icon: 2 });
                }
            }, function () { });
        }
        //加载子级数据字典
        var loadSubDict = function (hId, sId, pId, objContainer, defaultId) {
            var dto = {
                HeadDict_Id: hId,
                SonDict_Id: sId,
                Parent_Id: pId,
                defaultId: defaultId
            };
            $.ajaxWebService("SelectQuestionTest.aspx/GetSubDictList", JSON.stringify(dto), function (data) {
                $(objContainer).html(data.d);

                if (defaultId == "") {
                    switch (hId) {
                        case '722CE025-A876-4880-AAC1-5E416F3BDB1E':
                            $('[ajax-name="Subject"] a:eq(0)').click();
                            break;
                        case '934A3541-116E-438C-B9BA-4176368FCD9B':
                            $('[ajax-name="Syllabus"] a:eq(0)').click();
                            break;
                        case '3EF9506E-4C4B-407E-AA5D-451E0B20F0DB':
                            $('[ajax-name="Test_Category"] a:eq(0)').click();
                            break;
                    }
                }

                if (data.d == "") {
                    $("#tb1").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $(".page").html("");
                }
            }, function () { }, false);
        }


        //加载一级列表数据
        var loadData = function () {
            var dto = {
                ScoreId: "<%=ScoreId%>",
                GradeTerm: $('[ajax-name="GradeTerm"] a[class="active"]').attr("ajax-value"),
                Subject: $('[ajax-name="Subject"] a[class="active"]').attr("ajax-value"),
                Syllabus: $('[ajax-name="Syllabus"] a[class="active"]').attr("ajax-value"),
                Test_Category: $('[ajax-name="Test_Category"] a[class="active"]').attr("ajax-value"),
                x: Math.random()
            };
            $.ajaxWebService("SelectQuestionTest.aspx/GetDataList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template_KP", null, { filter_data: false });
                    $("#tb1").processTemplate(json);

                }
                else {
                    $("#tb1").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                }
            }, function () { });
        }
        //加载子级列表数据
        var loadSubData = function (obj, parentId) {
            if (parseInt($(obj).closest('td').attr("hasChildren")) > 0) {
                _objTD = obj;
                _parentId = parentId;
                _parentIdStr = $(obj).closest('td').data("parentidstr") + "&" + parentId;
                loadSubData2($(_objTD).closest('td').data("toggle"), $(_objTD).closest('td').data("scoreid"));
            }
        }

        var loadSubData2 = function ($toggle, scoreid) {
            if ($(_objTD).closest('td').html() != undefined) {
                $("#divSubData").html("");
                if ($toggle == "0") {//加载子级数据
                    $(_objTD).closest('td').data("toggle", 1);
                    $(_objTD).closest('td').find("i").removeClass().addClass("fa fa-minus-square-o");
                    $('#tb1 td[data-parentidstr*="' + _parentId + '"]').closest("tr").remove();

                    var dto = {
                        ScoreId: scoreid,
                        parentId: _parentId,
                        parentIdStr: _parentIdStr,
                        x: Math.random()
                    };
                    $.ajaxWebService("SelectQuestionTest.aspx/GetSubDataList", JSON.stringify(dto), function (data) {
                        var json = $.parseJSON(data.d);
                        if (json.err == "null") {
                            $("#divSubData").setTemplateElement("template_KP", null, { filter_data: false });
                            $("#divSubData").processTemplate(json);
                            $(_objTD).closest("tr").after($("#divSubData").html());
                        }
                    }, function () { });
                }
                else {//隐藏子级数据
                    $(_objTD).closest('td').data("toggle", 0);
                    $(_objTD).closest('td').find("i").removeClass().addClass("fa fa-plus-square-o");
                    $('#tb1 td[data-parentidstr*="' + _parentId + '"]').closest("tr").remove();
                }
            }
            else {
            }
        }
    </script>
</head>
<body class="bg_white">
    <div class="panel">
        <div class="panel-body">
            <div class="panel panel-default mfilter" data-name="mfilter">
                <div class="mfilter_control" data-name="mfilterControl">
                    <div class="mfilter_header" data-name="mfilterHeader">学段</div>
                    <div class="mfilter_body" data-name="mfilterBody">
                        <div class="mfilter_item" data-name="mfilterItem" ajax-name="GradeTerm">
                            <asp:Repeater runat="server" ID="rptGradeTerm">
                                <ItemTemplate>
                                    <a href="##" ajax-value="<%#Eval("Parent_Id") %>" class="<%#Eval("Parent_Id").ToString()==GradeTerm ?"active":"" %>"><%#Eval("D_Name") %></a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div class="mfilter_control" data-name="mfilterControl">
                    <div class="mfilter_header" data-name="mfilterHeader">学科</div>
                    <div class="mfilter_body" data-name="mfilterBody">
                        <div class="mfilter_item" data-name="mfilterItem" ajax-name="Subject">
                        </div>
                    </div>
                </div>
                <div class="mfilter_control" data-name="mfilterControl">
                    <div class="mfilter_header" data-name="mfilterHeader">考纲</div>
                    <div class="mfilter_body" data-name="mfilterBody">
                        <div class="mfilter_item" data-name="mfilterItem" ajax-name="Syllabus">
                        </div>
                    </div>
                </div>
                <div class="mfilter_control" data-name="mfilterControl">
                    <div class="mfilter_header" data-name="mfilterHeader">考试类别</div>
                    <div class="mfilter_body" data-name="mfilterBody">
                        <div class="mfilter_item" data-name="mfilterItem" ajax-name="Test_Category">
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-hover table-bordered">
                <thead>
                    <tr>
                        <th>名称</th>
                    </tr>
                </thead>
                <tbody id="tb1">
                </tbody>
            </table>
            <textarea id="template_KP" class="hidden">
                        {#foreach $T.list as record}
                            <tr>
                                <td onselectstart="return false" 
                                    kpId="{$T.record.S_TestingPoint_Id}" pId="{$T.record.Parent_Id}" hasChildren="{$T.record.hasChildren}" data-parentidstr="{$T.record.parentIdStr}" data-toggle="0" {#if $T.record.hasChildren>0}style="cursor:pointer;"{#/if} data-scoreid="{$T.record.ScoreId}">
                                    
                                    <span style="padding-left:{$T.record.paddingLeft}px"></span>
                                    {#if $T.record.IsLast=="1"}<input type="checkbox" class='che' value="{$T.record.S_TestingPoint_Id}" {$T.record.IsChecked}  data-scoreid="{$T.record.ScoreId}" data-tkid="{$T.record.tkid}" data-attrname="{$T.record.TPName}">{#else}{#/if}
                                    {#if $T.record.hasChildren>0}<i class="fa fa-plus-square-o"  onclick="loadSubData(this,'{$T.record.S_TestingPoint_Id}');"></i>{#/if}
                                    {$T.record.TPName}
                                </td>
                            </tr>
                        {#/for}
                    </textarea>
            <input type="button" id="btnSave" value="完成" class="btn btn-primary" />
        </div>
    </div>
    <div style="display: none;" id="divSubData"></div>
</body>
</html>
