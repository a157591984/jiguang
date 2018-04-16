<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="EachHWAnalysis.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.EachHWAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="EachHWAnalysis.aspx" class="active">每次作业数据分析</a></li>
            <li><a href="HisKlgAnalysis.aspx" title="老师批改作业完成后，隔天才能生成对应作业的数据分析。">历次知识点数据分析</a></li>
            <li><a href="STGrowthTranjectory.aspx">学生成长轨迹</a></li>
            <li><a href="HWSubmitMark.aspx" title="老师批改作业完成后，隔天才能生成对应作业的数据分析。">作业提交及批改情况</a></li>
            <li><a href="visit_client.aspx">教案使用情况</a></li>
            <li><a href="visit_web.aspx">讲评报告使用情况</a></li>
        </ul>
    </div>

    <div class="iframe-container">
        <%--<div class="iframe-sidebar">
            <div class="tree sidebar_menu">
                <ul data-level="0" data-name="subject">
                    <asp:Literal runat="server" ID="ltlSubject"></asp:Literal>
                </ul>
            </div>
        </div>--%>
        <div class="container pt">
            <div class="filter">
                <div class="form-inline">
                    <div class="form-group">
                        <label>作业名称：</label>
                        <input type="text" class="form-control input-sm" id="txtHomeWork_Name" />
                    </div>
                    <div class="form-group">
                        <label>布置时间：</label>
                        <input type="text" class="form-control input-sm" id="StartTime" placeholder="开始时间" />
                        <input type="text" class="form-control input-sm" id="EndTime" placeholder="结束时间" />
                    </div>
                    <button type="button" id="btnSearch" class="btn btn-primary btn-sm">检索</button>
                </div>
                <div class="filter_section">
                    <div id="Ishid">
                        <div class="filter_row clearfix">
                            <span class="row_name">学科：</span>
                            <div class="row_item">
                                <ul ajax-name="Subject">
                                    <asp:Literal ID="ltlSubject" runat="server" ClientIDMode="Static"></asp:Literal>
                                </ul>
                            </div>
                        </div>
                        <div class="filter_row clearfix">
                            <span class="row_name">老师：</span>
                            <div class="row_item">
                                <ul ajax-name="Teacher">
                                    <asp:Literal ID="ltlTeacher" runat="server" ClientIDMode="Static"></asp:Literal>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th width="120">日期</th>
                        <th>作业名称</th>
                        <th width="150">班级</th>
                        <th width="80">学科</th>
                        <th width="100">已交/未交</th>
                        <th width="100">已批/未批</th>
                        <th width="150">操作</th>
                    </tr>
                </thead>
                <tbody id="tb1"></tbody>
            </table>
            <!-- 翻页 -->
            <div class="pagination_container">
                <ul class="pagination">
                </ul>
            </div>
        </div>
    </div>
    <textarea id="template_1" class="hidden">
        {#foreach $T.list as record}
            <tr>
                <td>{$T.record.CreateTime}</td>
                <td>{$T.record.HomeWork_Name}</td>
                <td>{$T.record.ClassName}</td>
                <td>{$T.record.SubjectName}</td>
                <td>{$T.record.TJ}</td>
                <td>{$T.record.PG}</td>
                <td>
                {#if $T.record.HaveTJ==0}
                    <a href='##' class="disabled">数据分析</a>
                {#else}
                    {#if $T.record.IsTeacherDate==1}
                        <%--<a href='##' data-name="dataAnalysis" data-hwid="{$T.record.HomeWork_ID}" data-hwname="{$T.record.HomeWork_Name}" data-href="AssessmentProfile.aspx?ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&HomeWork_ID={$T.record.HomeWork_ID}&ClassID={$T.record.ClassID}&TeacherID={$T.record.TeacherID}&ClassName={$T.record.ClassNameEncode}">数据分析</a>--%>
                    <a href="CheckClassStatsHelper.aspx?HomeWork_ID={$T.record.HomeWork_ID}&HomeWork_Name={$T.record.HomeWork_Name}&ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&ClassID={$T.record.ClassID}&TeacherID={$T.record.TeacherID}&ClassName={$T.record.ClassName}&IsTeacherData={$T.record.IsTeacherData}" target="_blank">数据分析</a>
                    {#else}
                        <%--<a href='##' data-name="dataAnalysis" data-hwid="{$T.record.HomeWork_ID}" data-hwname="{$T.record.HomeWork_Name}" data-href="AssessmentProfile.aspx?ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&HomeWork_ID={$T.record.HomeWork_ID}">数据分析</a>--%>
                        <a href="CheckClassStatsHelper.aspx?HomeWork_ID={$T.record.HomeWork_ID}&HomeWork_Name={$T.record.HomeWork_Name}&ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&ClassID={$T.record.ClassID}&TeacherID={$T.record.TeacherID}&ClassName={$T.record.ClassName}&IsTeacherData={$T.record.IsTeacherData}" target="_blank">数据分析</a>
                    {#/if}
                    <a href='##' onclick="ReCalculation('{$T.record.HomeWork_ID}','{$T.record.HomeWork_Name}');">重新计算</a>
                {#/if}
                </td>
            </tr>
        {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript" src="../plugin/laydate/laydate.js"></script>
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            Subject = -1;
            $("#Ishid").hide();
            Isheadmaster = "<%=Isheadmaster%>";
            //是否为班主任 是就隐藏学科和老师
            if (Isheadmaster == "True") {
                $("#Ishid").show();
            }
            else {
                $("#Ishid").hide();
            }
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
            $('#btnSearch').click(function () {
                pageIndex = 1;
                loadData();
            })
            $(document).keydown(function (e) {
                if (e.keyCode == 13) {
                    $('#btnSearch').click();
                    return false;
                }
            })
            //筛选
            $(document).on('click', '[ajax-name] li', function () {
                if (!$(this).children("a").hasClass("disabled")) {
                    $(this).closest('ul').find('a').removeClass('active');
                    $(this).children('a').addClass('active');
                    pageIndex = 1;
                    loadData();
                }
            });
            loadData();
        })
        function loadData() {
            var _subjectId = $('[ajax-name="Subject"]').find('a.active').attr('ajax-value');
            var _teacherId = $('[ajax-name="Teacher"]').find('a.active').attr('ajax-value');
            var dto = {
                HWName: $("#txtHomeWork_Name").val(),
                HomeWorkCreateTime: $("#StartTime").val(),
                HomeWorkFinishTime: $("#EndTime").val(),
                SubjectID: _subjectId == undefined ? "" : _subjectId,
                TeacherID: _teacherId == undefined ? "" : _teacherId,
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("EachHWAnalysis.aspx/GetEachHWAnalysis", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template_1", null, { filter_data: false });
                    $("#tb1").processTemplate(json);
                    $(".pagination_container").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tb1").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
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
        //重新计算
        function ReCalculation(HomeWork_ID, HomeWork_Name) {
            layer.confirm("确定要重新计算吗？", { icon: 3, title: "提示" }, function () {
                $.ajaxWebService("EachHWAnalysis.aspx/ReCalculation", "{hwId:'" + HomeWork_ID + "',hwName:'" + HomeWork_Name + "',x:" + Math.random() + "}", function (data) {
                    if (data.d == "1") {
                        layer.msg("重新计算成功", { icon: 1, time: 1000 }, function () { loadData(); });
                    }
                    else {
                        layer.msg("重新计算失败", { icon: 2, time: 2000 });
                    }
                }, function () { })
            });
        }
    </script>
</asp:Content>
