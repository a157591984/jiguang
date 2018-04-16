<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="HWSubmitMark.aspx.cs" Inherits="Rc.Cloud.Web.Evaluation.HWSubmitMark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="EachHWAnalysis.aspx">每次作业数据分析</a></li>
            <li><a href="HisKlgAnalysis.aspx" title="老师批改作业完成后，隔天才能生成对应作业的数据分析。">历次知识点数据分析</a></li>
            <li><a href="STGrowthTranjectory.aspx">学生成长轨迹</a></li>
            <li><a href="HWSubmitMark.aspx" title="老师批改作业完成后，隔天才能生成对应作业的数据分析。" class="active">作业提交及批改情况</a></li>
            <li><a href="visit_client.aspx">教案使用情况</a></li>
            <li><a href="visit_web.aspx">讲评报告使用情况</a></li>
        </ul>
    </div>

    <div class="iframe-container">
        <%--    <div class="iframe-sidebar">
            <div class="tree sidebar_menu">
                <ul data-level="0" data-name="subject">
                    <asp:Literal runat="server" ID="ltlSubject"></asp:Literal>
                </ul>
            </div>
        </div>--%>
        <div class="container pt">
            <div class="filter">
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
                                <asp:Literal ID="ltlClass" runat="server"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">统计方式：</span>
                        <div class="row_item">
                            <ul ajax-name="date">
                                <li><a href='##' ajax-value="day" class="active">日统计</a></li>
                                <li><a href='##' ajax-value="week">周统计</a></li>
                                <li><a href='##' ajax-value="month">月统计</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>日期</th>
                        <th>班级</th>
                        <%-- <th>布置作业次数</th>--%>
                        <th>布置人次</th>
                        <th>交作业人次</th>
                        <th>批改人次</th>
                        <th>交作业率</th>
                        <th>批改率</th>
                        <!-- <th width="80">查看</th> -->
                    </tr>
                </thead>
                <tbody id="list"></tbody>
            </table>
            <!-- 翻页 -->
            <div class="pagination_container">
                <ul class="pagination"></ul>
            </div>
        </div>
    </div>
    <textarea id="listBox" class="hidden">
        {#foreach $T.list as record}
            <tr>
                <td>{$T.record.DateData}</td>
                <td>{$T.record.ClassName}</td>
                <%-- <td>{$T.record.AssignedCount}</td>--%>
                <td>{$T.record.AssignedCount}</td>
                <td>{$T.record.CommittedCount}</td>
                <td>{$T.record.CorrectedCount}</td>
                <td>{$T.record.CommittedCountRate}</td>
                <td>{$T.record.CorrectedCountRate}</td>
                <!-- <td class="table_opera">
                    <a href="HWList.aspx?SubjectID={\$T.record.SubjectID}&ClassID={\$T.record.ClassID}&Date=">相关作业</a>
                </td> -->
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
                    if ($(this).closest('ul').attr('ajax-name') == 'Subject') {
                        loadClass();
                    }
                    if ($(this).closest('ul').attr('ajax-name') == 'Teacher') {
                        loadClass();
                    }
                    pageIndex = 1;
                    loadData();
                }
            });

            //检索
            $('input[name="search"]').on({
                click: function () {
                    pageIndex = 1;
                    loadData();
                }
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
            var $_objBox = $("#listBox");
            var objBox = "listBox";
            var $_objList = $("#list");
            var $_pagination = $(".pagination_container");
            var _subjectId = $('[ajax-name="Subject"]').find('a.active').attr('ajax-value');
            var _teacherId = $('[ajax-name="Teacher"]').find('a.active').attr('ajax-value');
            var _classId = $('[ajax-name="class"]').find('a.active').attr('ajax-value');
            var _dateType = $('[ajax-name="date"]').find('a.active').attr('ajax-value');
            var dto = {
                SubjectID: _subjectId,
                ClassID: _classId == undefined ? "" : _classId,
                DateType: _dateType == undefined ? "" : _dateType,
                TeacherID: _teacherId,//"<%=UserId%>",
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                x: Math.random()
            };
            $.ajaxWebService("HWSubmitMark.aspx/GetList", JSON.stringify(dto), function (data) {
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
