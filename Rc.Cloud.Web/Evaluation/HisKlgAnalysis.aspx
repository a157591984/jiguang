<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="HisKlgAnalysis.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.HisKlgAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="EachHWAnalysis.aspx">每次作业数据分析</a></li>
            <li><a href="HisKlgAnalysis.aspx" class="active" title="老师批改作业完成后，隔天才能生成对应作业的数据分析。">历次知识点数据分析</a></li>
            <li><a href="STGrowthTranjectory.aspx">学生成长轨迹</a></li>
            <li><a href="HWSubmitMark.aspx" title="老师批改作业完成后，隔天才能生成对应作业的数据分析。">作业提交及批改情况</a></li>
            <li><a href="visit_client.aspx">教案使用情况</a></li>
            <li><a href="visit_web.aspx">讲评报告使用情况</a></li>
        </ul>
    </div>

    <div class="iframe-container">
        <div class="container pt">
            <div class="filter">
                <div class="form-inline">
                    <div class="form-group">
                        <label>知识点名称：</label>
                        <input type="text" name="key" class="form-control input-sm" />
                    </div>
                    <div class="form-group">
                        <label>平均得分率低于：</label>
                        <input type="text" name="KPScoreAvgRate" class="form-control input-sm" />&nbsp;%
                    </div>
                    <input type="button" name="search" id="btnSearch" value="检索" class="btn btn-primary btn-sm" />
                </div>
                <div class="filter_section">
                    <div class="filter_row clearfix is-hid-hook">
                        <span class="row_name">学科：</span>
                        <div class="row_item">
                            <ul ajax-name="Subject">
                                <asp:Literal ID="ltlSubject" runat="server" ClientIDMode="Static"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix is-hid-hook">
                        <span class="row_name">老师：</span>
                        <div class="row_item">
                            <ul ajax-name="Teacher">
                                <asp:Literal ID="ltlTeacher" runat="server" ClientIDMode="Static"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">班级：</span>
                        <div class="row_item">
                            <ul ajax-name="class">
                                <asp:Literal ID="ltlClass" runat="server" ClientIDMode="Static"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">日期范围：</span>
                        <div class="row_item">
                            <ul data-name="date_range" ajax-name="datetype">
                                <li data-tab-bar="date" class="active"><a href='##' class="active" ajax-value="month">月度</a></li>
                                <li data-tab-bar="date"><a href='##' ajax-value="quarter">季度</a></li>
                                <li data-tab-bar="date"><a href='##' ajax-value="halfyear">年度</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">日期：</span>
                        <div class="row_item">
                            <ul data-tab-box="date" data-name="month" ajax-name="datedata"></ul>
                            <ul data-tab-box="date" data-name="quarter" ajax-name="datedata"></ul>
                            <ul data-tab-box="date" data-name="year" ajax-name="datedata"></ul>
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th width="12%">日期</th>
                        <th width="10%">学科</th>
                        <th width="15%">班级</th>
                        <th>知识点名称</th>
                        <th width="10%">平均得分率</th>
                        <th width="10%">来源</th>
                    </tr>
                </thead>
                <tbody id="knowledgeList"></tbody>
            </table>
            <!-- 翻页 -->
            <div class="pagination_container">
                <ul class="pagination"></ul>
            </div>
        </div>
    </div>
    <textarea id="knowledgeBox" class="hidden">
        {#foreach $T.list as record}
            <tr>
                <td>{$T.record.DateData}</td>
                <td>{$T.record.SubjectName}</td>
                <td>{$T.record.ClassName}</td>
                <td>{$T.record.KPName}</td>
                <td>{$T.record.KPScoreAvgRate}</td>
                <td>
                    {#if $T.record.IsTeacherData=='1'}
                    <a href="javascript:;" class="scurce-hook" data-title="{$T.record.KPName}" data-url="HisKlgAnalysisSource.aspx?SubjectID={$T.record.SubjectID}&ClassID={$T.record.ClassID}&KPName={$T.record.KPNameEncode}&DateData={$T.record.DateData}&DateType={$T.record.DateType}&TeacherId={$T.record.TeacherId}">来源</a>
                    {#else}
                    <a href="javascript:;" class="scurce-hook" data-title="{$T.record.KPName}" data-url="HisKlgAnalysisSource.aspx?SubjectID={$T.record.SubjectID}&ClassID={$T.record.ClassID}&KPName={$T.record.KPNameEncode}&DateData={$T.record.DateData}&DateType={$T.record.DateType}">来源</a>
                    {#/if}
                </td>
            </tr>
        {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;//默认页码
            loadClass();
            loadData();//初始化数据
            getMonth();
            getQuarter();
            getYear();
            Isheadmaster = "<%=Isheadmaster%>";
            //是否为班主任 是就隐藏学科和老师
            if (Isheadmaster == "True") {
                $(".is-hid-hook").show();
            }
            else {
                $(".is-hid-hook").hide();
            }
            //筛选
            $(document).on('click', '[ajax-name] li', function () {
                if (!$(this).children("a").hasClass("disabled")) {
                    $(this).closest('ul').find('a').removeClass('active');
                    $(this).children('a').addClass('active');
                    if ($(this).closest('ul').attr('ajax-name') == 'datetype') {
                        var index = $(this).index();
                        $('[data-tab-box="date"]').find('a').removeClass('active');
                        $('[data-tab-box="date"]').eq(index).find('li:eq(0) a').addClass('active');
                    }
                    if ($(this).closest('ul').attr('ajax-name') == 'Subject') {
                        loadClass();
                    }
                    if ($(this).closest('ul').attr('ajax-name') == 'Teacher') {
                        loadClass();
                    }
                    pageIndex = 1;
                    loadData();
                }
            })
            //检索
            $('input[name="search"]').on({
                click: function () {
                    pageIndex = 1;
                    loadData();
                }
            })
            //切换日期范围时默认当前范围下的第一个条件选中
            //$('[data-name="date_range"] li a').on({
            //    click: function () {
            //        var index = $(this).closest('li').index();
            //        $('[data-tab-box="date"]').find('a').removeClass('active');
            //        $('[data-tab-box="date"]').eq(index).find('li:eq(0) a').click();
            //    }
            //});

            //来源
            $(document).on('click', '.scurce-hook', function (e) {
                var url = $(this).data('url');
                var title = $(this).data('title');
                layer.open({
                    type: 2,
                    title: title,
                    area: ['800px', '90%'],
                    content: url
                })
                e.preventDefault();
            })
        })
        function loadClass() {
            var _subjectId = $('[ajax-name="Subject"]').find('a.active').attr('ajax-value');
            var _teacherId = $('[ajax-name="Teacher"]').find('a.active').attr('ajax-value');
            var dto = {
                SubjectID: _subjectId,
                TeacherID: _teacherId,//"<%=UserId%>",
                x: Math.random()
            };
            $.ajaxWebService("HisKlgAnalysis.aspx/GetClass", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $('[ajax-name="class"]').html(data.d);
                }
            }, function () { }, false);

        }
        function loadData() {
            var $_objBox = $("#knowledgeBox");
            var objBox = "knowledgeBox";
            var $_objList = $("#knowledgeList");
            var $_pagination = $(".pagination_container");
            var _subjectId = $('[ajax-name="Subject"]').find('a.active').attr('ajax-value');
            var _teacherId = $('[ajax-name="Teacher"]').find('a.active').attr('ajax-value');
            var _classId = $('[ajax-name="class"]').find('a.active').attr('ajax-value');
            var _dateType = $('[ajax-name="datetype"]').find('a.active').attr('ajax-value');
            var _dateData = $('[ajax-name="datedata"]').find('a.active').attr('ajax-value');
            var dto = {
                SubjectID: _subjectId,
                ClassID: _classId == undefined ? "" : _classId,
                key: $('input[name="key"]').val(),
                KPScoreAvgRate: $('input[name="KPScoreAvgRate"]').val(),
                DateType: _dateType == undefined ? "" : _dateType,
                DateData: _dateData == undefined ? "" : _dateData,
                TeacherID: _teacherId,//"<%=UserId%>",
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                x: Math.random()
            };
            $.ajaxWebService("HisKlgAnalysis.aspx/GetKnowledge", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $_objList.setTemplateElement(objBox, null, { filter_data: false });
                    $_objList.processTemplate(json);
                    $_pagination.pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $_objList.html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $_pagination.find("ul").html("");
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
    </script>
</asp:Content>
