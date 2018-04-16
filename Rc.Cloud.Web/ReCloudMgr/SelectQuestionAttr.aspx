<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectQuestionAttr.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.SelectQuestionAttr" %>

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
            //默认教材版本 
            loadSubDict("934A3541-116E-438C-B9BA-4176368FCD9B", "74958B74-D2A4-4ACD-BB4E-F48C59329F40", "<%=Subject%>", $('[ajax-name="Resource_Version"]'), "<%=Resource_Version%>");
            //默认课本
            loadSubDict("74958B74-D2A4-4ACD-BB4E-F48C59329F40", "3EF9506E-4C4B-407E-AA5D-451E0B20F0DA", "<%=Resource_Version%>", $('[ajax-name="Book_Type"]'), "");

            $('[data-name="mfilter"]').mfilter({
                onClick: function (obj) {
                    var name = $(obj).closest('[data-name="mfilterItem"]').attr('ajax-name');
                    var val = $(obj).attr('ajax-value');
                    switch (name) {
                        case 'GradeTerm':
                            loadSubDict("722CE025-A876-4880-AAC1-5E416F3BDB1E", "934A3541-116E-438C-B9BA-4176368FCD9B", val, $('[ajax-name="Subject"]'), "");
                            break;
                        case 'Subject':
                            loadSubDict("934A3541-116E-438C-B9BA-4176368FCD9B", "74958B74-D2A4-4ACD-BB4E-F48C59329F40", val, $('[ajax-name="Resource_Version"]'), "");
                            break;
                        case 'Resource_Version':
                            loadSubDict("74958B74-D2A4-4ACD-BB4E-F48C59329F40", "3EF9506E-4C4B-407E-AA5D-451E0B20F0DA", val, $('[ajax-name="Book_Type"]'), "");
                            break;
                        case 'Book_Type':
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
            //    // 年级学期722CE025-A876-4880-AAC1-5E416F3BDB1E，学科934A3541-116E-438C-B9BA-4176368FCD9B
            //    loadSubDict("722CE025-A876-4880-AAC1-5E416F3BDB1E", "934A3541-116E-438C-B9BA-4176368FCD9B", $(this).attr("ajax-value"), $('[ajax-name="Subject"]'), "");
            //    //$('[ajax-name="Subject"] a:eq(0)').click();
            //});
            //$(document).on('click', '[ajax-name="Subject"] a', function () {
            //    // 学科934A3541-116E-438C-B9BA-4176368FCD9B，教材版本74958B74-D2A4-4ACD-BB4E-F48C59329F40
            //    loadSubDict("934A3541-116E-438C-B9BA-4176368FCD9B", "74958B74-D2A4-4ACD-BB4E-F48C59329F40", $(this).attr("ajax-value"), $('[ajax-name="Resource_Version"]'), "");
            //    //$('[ajax-name="Resource_Version"] a:eq(0)').click();
            //});
            //$(document).on('click', '[ajax-name="Resource_Version"] a', function () {
            //    // 教材版本74958B74-D2A4-4ACD-BB4E-F48C59329F40，课本3EF9506E-4C4B-407E-AA5D-451E0B20F0DA
            //    loadSubDict("74958B74-D2A4-4ACD-BB4E-F48C59329F40", "3EF9506E-4C4B-407E-AA5D-451E0B20F0DA", $(this).attr("ajax-value"), $('[ajax-name="Book_Type"]'), "");
            //    //$('[ajax-name="Book_Type"] a:eq(0)').click();
            //});
            //$('[ajax-name="Book_Type"] a:eq(0)').click();
            //loadData();
            //$(document).on('click', '[ajax-name="Book_Type"] a', function () {
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
                window.parent.GetQuestionAttr(scoreId);
                window.parent.layer.closeAll();
            })
        })
        //增加知识点
        var AddAtrr = function (ScoreId, AttrId, TestQuestions_Knowledge_ID) {
            var dto = {
                ScoreId: ScoreId,
                AttrId: AttrId,
                TestQuestions_Knowledge_ID: TestQuestions_Knowledge_ID,
                x: Math.random()
            };
            $.ajaxWebService("SelectQuestionAttr.aspx/AddAtrr", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                }
                else {
                    layer.msg("异常", { time: 2000, icon: 2 });
                }
            }, function () { });
        }
        //删除知识点
        var DeleteAtrr = function (TestQuestions_Knowledge_ID) {
            var dto = {
                TestQuestions_Knowledge_ID: TestQuestions_Knowledge_ID,
                x: Math.random()
            };
            $.ajaxWebService("SelectQuestionAttr.aspx/DeleteAtrr", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                }
                else {
                    layer.msg("异常", { time: 2000, icon: 2 });
                }
            }, function () { });
        }
        //加载子级数据字典
        var loadSubDict = function (hId, sId, pId, objContainer, defaultId) {
            //alert(hId);
            var dto = {
                HeadDict_Id: hId,
                SonDict_Id: sId,
                Parent_Id: pId,
                defaultId: defaultId
            };
            $.ajaxWebService("SelectQuestionAttr.aspx/GetSubDictList", JSON.stringify(dto), function (data) {
                $(objContainer).html(data.d);

                if (defaultId == "") {

                    switch (hId) {

                        case '722CE025-A876-4880-AAC1-5E416F3BDB1E'://学段
                            $('[ajax-name="Subject"] a:eq(0)').click();
                            break;
                        case '934A3541-116E-438C-B9BA-4176368FCD9B'://学科
                            $('[ajax-name="Resource_Version"] a:eq(0)').click();
                            break;
                        case '74958B74-D2A4-4ACD-BB4E-F48C59329F40'://教参版本
                            $('[ajax-name="Book_Type"] a:eq(0)').addClass('active');
                            loadData();
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
                Resource_Version: $('[ajax-name="Resource_Version"] a[class="active"]').attr("ajax-value"),
                Book_Type: $('[ajax-name="Book_Type"] a[class="active"]').attr("ajax-value"),
                x: Math.random()
            };
            $.ajaxWebService("SelectQuestionAttr.aspx/GetDataList", JSON.stringify(dto), function (data) {
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
                    $.ajaxWebService("SelectQuestionAttr.aspx/GetSubDataList", JSON.stringify(dto), function (data) {
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
<body>
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
                    <div class="mfilter_header" data-name="mfilterHeader">教材版本</div>
                    <div class="mfilter_body tab-content" data-name="mfilterBody">
                        <div class="mfilter_item tab-pane active" data-name="mfilterItem" ajax-name="Resource_Version">
                        </div>
                    </div>
                </div>
                <div class="mfilter_control" data-name="mfilterControl">
                    <div class="mfilter_header" data-name="mfilterHeader">课本</div>
                    <div class="mfilter_body" data-name="mfilterBody">
                        <div class="mfilter_item" data-name="mfilterItem" ajax-name="Book_Type">
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
                                    kpId="{$T.record.S_KnowledgePoint_Id}" pId="{$T.record.Parent_Id}" hasChildren="{$T.record.hasChildren}" data-parentidstr="{$T.record.parentIdStr}" data-toggle="0" {#if $T.record.hasChildren>0}style="cursor:pointer;"{#/if} data-scoreid="{$T.record.ScoreId}">
                                    <span style="padding-left:{$T.record.paddingLeft}px"></span>
                                    {#if $T.record.IsLast=="1"}<input type="checkbox" class='che' value="{$T.record.S_KnowledgePoint_Id}" {$T.record.IsChecked}  data-scoreid="{$T.record.ScoreId}" data-tkid="{$T.record.tkid}" data-attrname="{$T.record.KPName}">{#else}{#/if}
                                    {#if $T.record.hasChildren>0}<i class="fa fa-plus-square-o"  onclick="loadSubData(this,'{$T.record.S_KnowledgePoint_Id}');"></i>{#/if}
                                    {$T.record.KPName}
                                </td>
                            </tr>
                        {#/for}
                    </textarea>
            <input type="button" id="btnSave" value="确定" class="btn btn-primary" />
        </div>
    </div>
    <div style="display: none;" id="divSubData"></div>
</body>
</html>
