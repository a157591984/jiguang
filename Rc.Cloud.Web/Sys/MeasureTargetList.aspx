﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="MeasureTargetList.aspx.cs" Inherits="Rc.Cloud.Web.Sys.MeasureTargetList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../SysLib/plugin/mfilter-1.0/mfilter.css" rel="stylesheet" />
    <script src="../SysLib/plugin/mfilter-1.0/mfilter.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-body">
                    <div class="panel panel-default mfilter" data-name="mfilter">
                        <div class="mfilter_control" data-name="mfilterControl">
                            <div class="mfilter_header" data-name="mfilterHeader">学段</div>
                            <div class="mfilter_body" data-name="mfilterBody">
                                <div class="mfilter_item" data-name="mfilterItem" ajax-name="GradeTerm">
                                    <asp:Repeater runat="server" ID="rptGradeTerm">
                                        <ItemTemplate>
                                            <a href="##" ajax-value="<%#Eval("Parent_Id") %>"><%#Eval("D_Name") %></a>
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
                    </div>
                    <div class="form-inline search_bar mb">
                        <input type="button" id="btnAdd" value="新增" class="btn btn-primary btn-sm" />
                        <input type="text" id="txtName" class="form-control input-sm" placeholder="名称/编码" />
                        <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
                    </div>
                    <table class="table table-hover table-bordered">
                        <thead>
                            <tr>
                                <th>名称</th>
                                <th width="150">编码</th>
                                <th width="150">层级</th>
                                <th width="280">操作</th>
                            </tr>
                        </thead>
                        <tbody id="tb1">
                        </tbody>
                    </table>
                    <textarea id="template_MT" class="hidden">
                        {#foreach $T.list as record}
                            <tr>
                                <td onselectstart="return false" onclick="loadSubData(this,'{$T.record.S_MeasureTarget_Id}');" 
                                    mtId="{$T.record.S_MeasureTarget_Id}" pId="{$T.record.Parent_Id}" hasChildren="{$T.record.hasChildren}" data-parentidstr="{$T.record.parentIdStr}" data-toggle="0" {#if $T.record.hasChildren>0}style="cursor:pointer;"{#/if}>
                                    <span style="padding-left:{$T.record.paddingLeft}px"></span>
                                    {#if $T.record.hasChildren>0}<i class="fa fa-plus-square-o"></i>{#/if}
                                    {$T.record.MTName}
                                </td>
                                <td>{$T.record.MTCode}</td>
                                <td>{$T.record.MTLevelName}</td>
                                <td class="opera">
                                    <a href="##" onclick="AddSame(this,'{$T.record.S_MeasureTarget_Id}','{$T.record.Parent_Id}')" >新增同级</a>
                                    <a href="##" onclick="AddSub(this,'{$T.record.S_MeasureTarget_Id}')" >新增下级</a>
                                    <a href="##" onclick="UpdateData(this,'{$T.record.S_MeasureTarget_Id}','{$T.record.Parent_Id}')" >编辑</a>
                                    <a href="##" onclick="DeleteData(this,'{$T.record.S_MeasureTarget_Id}')" >删除</a>
                                </td>
                            </tr>
                        {#/for}
                    </textarea>
                    <hr />
                    <div class="page"></div>
                </div>
            </div>
        </div>
    </div>
    <div style="display: none;" id="divSubData"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            _gradeTerm = "";
            _objTD = null;
            _parentId = "";
            _parentIdStr = "";
            $('[data-name="mfilter"]').mfilter({});

            $('[ajax-name="GradeTerm"] a').click(function () {
                _gradeTerm = $(this).attr("ajax-value");
                // 年级学期722CE025-A876-4880-AAC1-5E416F3BDB1E，学科934A3541-116E-438C-B9BA-4176368FCD9B
                loadSubDict("722CE025-A876-4880-AAC1-5E416F3BDB1E", "934A3541-116E-438C-B9BA-4176368FCD9B", $(this).attr("ajax-value"), $('[ajax-name="Subject"]'));
                //$('[ajax-name="Subject"] a:eq(0)').click();
            });
            $(document).on('click', '[ajax-name="Subject"] a', function () {
                // 学科934A3541-116E-438C-B9BA-4176368FCD9B，教材版本74958B74-D2A4-4ACD-BB4E-F48C59329F40
                loadSubDict("934A3541-116E-438C-B9BA-4176368FCD9B", "74958B74-D2A4-4ACD-BB4E-F48C59329F40", $(this).attr("ajax-value"), $('[ajax-name="Resource_Version"]'));
                //$('[ajax-name="Resource_Version"] a:eq(0)').click();
            });

            $(document).on('click', '[ajax-name="Resource_Version"] a', function () {
                loadData();
            });

            $('[ajax-name="GradeTerm"] a:eq(0)').click();

            $(document).on("click", "#btnSearch", function () {
                pageIndex = 1;
                loadData();
            });
            //新增
            $("#btnAdd").click(function () {
                layer.ready(function () {
                    layer.open({
                        type: 2,
                        title: '增加',
                        fix: false,
                        area: ["450px", "450px"],
                        content: "MeasureTargetEdit.aspx?parentId=0&GradeTerm=" + $('[ajax-name="GradeTerm"] a[class="active"]').attr("ajax-value")
                            + "&Subject=" + $('[ajax-name="Subject"] a[class="active"]').attr("ajax-value")
                            + "&Resource_Version=" + $('[ajax-name="Resource_Version"] a[class="active"]').attr("ajax-value")
                    })
                });
            })
        });
        //加载子级数据字典
        var loadSubDict = function (hId, sId, pId, objContainer) {
            var dto = {
                HeadDict_Id: hId,
                SonDict_Id: sId,
                Parent_Id: pId
            };
            $.ajaxWebService("MeasureTargetList.aspx/GetSubDictList", JSON.stringify(dto), function (data) {
                $(objContainer).html(data.d);

                switch (hId) {
                    case '722CE025-A876-4880-AAC1-5E416F3BDB1E':
                        $('[ajax-name="Subject"] a:eq(0)').click();
                        break;
                    case '934A3541-116E-438C-B9BA-4176368FCD9B':
                        $('[ajax-name="Resource_Version"] a:eq(0)').click();
                        break;
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
                Name: $.trim($("#txtName").val()),
                GradeTerm: _gradeTerm,
                Subject: $('[ajax-name="Subject"] a[class="active"]').attr("ajax-value"),
                Resource_Version: $('[ajax-name="Resource_Version"] a[class="active"]').attr("ajax-value"),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("MeasureTargetList.aspx/GetDataList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template_MT", null, { filter_data: false });
                    $("#tb1").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tb1").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $(".page").html("");
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
        //加载子级列表数据
        var loadSubData = function (obj, parentId) {
            if (parseInt($(obj).attr("hasChildren")) > 0) {
                _objTD = obj;
                _parentId = parentId;
                _parentIdStr = $(obj).data("parentidstr") + "&" + parentId;
                loadSubData2($(_objTD).data("toggle"));
            }
        }

        var loadSubData2 = function ($toggle) {
            if ($(_objTD).html() != undefined) {
                $("#divSubData").html("");
                if ($toggle == "0") {//加载子级数据
                    $(_objTD).data("toggle", 1);
                    $(_objTD).find("i").removeClass().addClass("fa fa-minus-square-o");
                    $('#tb1 td[data-parentidstr*="' + _parentId + '"]').closest("tr").remove();

                    var dto = {
                        parentId: _parentId,
                        parentIdStr: _parentIdStr,
                        x: Math.random()
                    };
                    $.ajaxWebService("MeasureTargetList.aspx/GetSubDataList", JSON.stringify(dto), function (data) {
                        var json = $.parseJSON(data.d);
                        if (json.err == "null") {
                            $("#divSubData").setTemplateElement("template_MT", null, { filter_data: false });
                            $("#divSubData").processTemplate(json);
                            $(_objTD).closest("tr").after($("#divSubData").html());
                        }
                    }, function () { });
                }
                else {//隐藏子级数据
                    $(_objTD).data("toggle", 0);
                    $(_objTD).find("i").removeClass().addClass("fa fa-plus-square-o");
                    $('#tb1 td[data-parentidstr*="' + _parentId + '"]').closest("tr").remove();
                }
            }
            else {
                loadData();
            }
        }

        //新增同级
        function AddSame(obj, mtId, parentId) {
            //变量赋值
            _objTD = $(obj).closest("table").find("td[mtId='" + parentId + "']");
            if ($(_objTD).attr("hasChildren") == 0) {
                _objTD = $(obj).closest("table").find("td[mtId='" + $(_objTD).attr("pId") + "']");
                if ($(_objTD).html() != undefined) {
                    _parentId = $(_objTD).attr("mtId");
                    _parentIdStr = $(_objTD).data("parentidstr") + "&" + _parentId;
                }
            }
            else {
                _parentId = parentId;
                _parentIdStr = $(_objTD).data("parentidstr") + "&" + _parentId;
            }

            layer.ready(function () {
                layer.ready(function () {
                    layer.open({
                        type: 2,
                        title: '新增同级',
                        fix: false,
                        area: ["450px", "450px"],
                        content: "MeasureTargetEdit.aspx?GradeTerm=" + $('[ajax-name="GradeTerm"] a[class="active"]').attr("ajax-value")
                            + "&Subject=" + $('[ajax-name="Subject"] a[class="active"]').attr("ajax-value")
                            + "&Resource_Version=" + $('[ajax-name="Resource_Version"] a[class="active"]').attr("ajax-value")
                            + "&parentId=" + parentId
                            + "&mtId_Copy=" + mtId
                    })
                });
            });
        }
        //新增下级
        function AddSub(obj, mtId) {
            //变量赋值
            _objTD = $(obj).closest("tr").find("td").eq(0);
            if ($(_objTD).attr("hasChildren") == 0) {
                _objTD = $(obj).closest("table").find("td[mtId='" + $(_objTD).attr("pId") + "']");
                if ($(_objTD).html() != undefined) {
                    _parentId = $(_objTD).attr("mtId");
                    _parentIdStr = $(_objTD).data("parentidstr") + "&" + _parentId;
                }
            }
            else {
                _parentId = mtId;
                _parentIdStr = $(_objTD).data("parentidstr") + "&" + _parentId;
            }

            layer.ready(function () {
                layer.ready(function () {
                    layer.open({
                        type: 2,
                        title: '新增下级',
                        fix: false,
                        area: ["450px", "450px"],
                        content: "MeasureTargetEdit.aspx?GradeTerm=" + $('[ajax-name="GradeTerm"] a[class="active"]').attr("ajax-value")
                            + "&Subject=" + $('[ajax-name="Subject"] a[class="active"]').attr("ajax-value")
                            + "&Resource_Version=" + $('[ajax-name="Resource_Version"] a[class="active"]').attr("ajax-value")
                            + "&parentId=" + mtId
                            + "&mtId_Copy=" + mtId
                    })
                });
            });
        }
        //编辑
        function UpdateData(obj, mtId, parentId) {
            //变量赋值
            _objTD = $(obj).closest("table").find("td[mtId='" + parentId + "']");
            if ($(_objTD).attr("hasChildren") == 0) {
                _objTD = $(obj).closest("table").find("td[mtId='" + $(_objTD).attr("pId") + "']");
                if ($(_objTD).html() != undefined) {
                    _parentId = $(_objTD).attr("mtId");
                    _parentIdStr = $(_objTD).data("parentidstr") + "&" + _parentId;
                }
            }
            else {
                _parentId = parentId;
                _parentIdStr = $(_objTD).data("parentidstr") + "&" + _parentId;
            }

            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '编辑',
                    fix: false,
                    area: ["450px", "450px"],
                    content: "MeasureTargetEdit.aspx?mtId=" + mtId + "&parentId=" + parentId
                })
            });
        }

        //删除
        function DeleteData(obj, mtId) {
            layer.ready(function () {
                layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
                    $.ajaxWebService("MeasureTargetList.aspx/DeleteData", "{S_MeasureTarget_Id:'" + mtId + "',x:" + Math.random() + "}", function (data) {
                        if (data.d == "1") {
                            layer.msg("删除成功", { icon: 1, time: 2000 }, function () {
                                var _del_pid = $(obj).closest("tr").find("td:eq(0)").attr("pId");
                                var _del_obj = $(obj).closest("table").find("td[mtId='" + _del_pid + "']");
                                //移除本行
                                $(obj).closest("tr").remove();
                                //移除加减号按钮
                                if ($('#tb1 td[data-parentidstr*="' + _del_pid + '"]').length == 0 && $(_del_obj).html() != undefined) {
                                    $(_del_obj).find("i").remove();
                                    $(_del_obj).attr("hasChildren", "0");
                                    $(_del_obj).css("cursor", "default");
                                }
                            });
                        }
                        else if (data.d == "2") {
                            layer.msg("存在子级数据，删除失败", { icon: 2, time: 2000 });
                        }
                        else {
                            layer.msg("删除失败", { icon: 2, time: 2000 });
                        }
                    }, function () { })
                });
            })
        }

    </script>
</asp:Content>
