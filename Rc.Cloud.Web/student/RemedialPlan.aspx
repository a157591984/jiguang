<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/student.Master" AutoEventWireup="true" CodeBehind="RemedialPlan.aspx.cs" Inherits="Rc.Cloud.Web.student.RemedialPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="oHomework.aspx">最新作业</a></li>
            <li><a href="St_oHistoryHomework.aspx">历次作业</a></li>
            <li><a href="oWrongHomework.aspx">错题集</a></li>
            <li><a href="AchievementTrack.aspx">成绩跟踪</a></li>
            <li><a href="CheckStuHWAnalysis.aspx" class="active">综合分析</a></li>
        </ul>
    </div>
    <div class="iframe-container pa">
            <div class="comprehensive_data_analysis">
                <div class="filter">
                    <div class="filter_section">
                        <div class="filter_row clearfix">
                            <span class="row_name">学科：</span>
                            <div class="row_item">
                                <ul ajax-name="Subject">
                                </ul>
                            </div>
                        </div>
                        <div class="filter_row clearfix">
                            <span class="row_name">课本：</span>
                            <div class="row_item">
                                <ul ajax-name="BookType">
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="content">
                    <div class="content_sidebar">
                        <div class="title">全部</div>
                        <div class="mtree mtree-knowledge-hook ph" ajax-name="divKPList">
                        </div>
                    </div>
                    <div class="content_main">
                        <ul class="nav nav-tabs mb">
                            <li><a href="##" onclick="pageJump(1);">综合分析</a></li>
                            <li><a href="##" onclick="pageJump(2);">错误分析</a></li>
                            <li class="active"><a href="##">提分补救方案</a></li>
                        </ul>
                        <h4>建议学习的知识点</h4>
                        <%--<div>
                        截止到     年   月     日，         同学共完成多少道作业题目，其中错误题目为16道，共涉及到知识点11个：具体为元素与集合的关系、元素的确定性、元素的无序性、容斥原理、图示法、常见数集及其记法、子集与真子集、集合相等、并集、交集、集合的混合运算。请认知将以上知识点进行补救学习或请老师指导学习。
                    </div>--%>
                        <div id="divAnalysisInfo"></div>
                        <h4 class="mt">错题重练</h4>
                        <div>
                            <p>建议对以上错误知识点对应的题目进行再次训练</p>
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>未掌握的知识点</th>
                                        <th width="200">以前答错题目</th>
                                    </tr>
                                </thead>
                                <tbody id="tbWrongReTest">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <textarea id="template_Res" class="hidden">
    {#foreach $T.list as record}
        <tr>
            <td>{$T.record.KPNameBasic}</td>
            <td>
                <a href="ErrorDetail.aspx?KPNameBasic={$T.record.KPNameBasic_En}&S_KnowledgePoint_Id={$T.record.S_KnowledgePoint_Id}&Student_Id={$T.record.Student_Id}" target="_blank" class="btn btn-primary btn-xs">{$T.record.TQCount_Wrong}</a>
            </td>
        </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;

            $(document).on('click', '[ajax-name="Subject"] a', function () {
                loadDict("2", $('[ajax-name="Subject"] a[class="active"]').attr("ajax-value"), "<%=BookType%>", $('[ajax-name="BookType"]'));
            });
            $(document).on('click', '[ajax-name="BookType"] a', function () {
                loadKPList();
            });

            loadDict("1", "", "<%=Subject%>", $('[ajax-name="Subject"]'));

        });

        //跳转
        function pageJump(type) {
            var _url = "";
            switch (type) {
                case 1:
                    _url = "aggregateAnalysis.aspx?1=1";
                    break;
                case 2:
                    _url = "DataAnalysisReportMultipleJobError.aspx?1=1";
                    break;

                case 3:
                    _url = "RemedialPlan.aspx?1=1";
                    break;
            }
            _url += "&Subject=" + $('[ajax-name="Subject"] a[class="active"]').attr("ajax-value");
            _url += "&BookType=" + $('[ajax-name="BookType"] a[class="active"]').attr("ajax-value");
            _url += "&ParentId=" + $('[ajax-name="divKPList"] div[class*="active"]').attr("ajax-value");
            window.location.href = _url;
        }

        //加载数据字典
        function loadDict(type, pId, chkId, objContainer) {
            var dto = {
                Type: type,//1学科 2课本
                ParentId: pId,
                CheckedDicId: chkId,
                StuId: "<%=StudentId%>"
            };
            $.ajaxWebService("aggregateAnalysis.aspx/GetDictList", JSON.stringify(dto), function (data) {
                $(objContainer).html(data.d);
                if (data.d == "") {
                    $("#tbRes").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $(".pagination_container").html("");
                }
                else {
                    if (type == "1") {
                        loadDict("2", $('[ajax-name="Subject"] a[class="active"]').attr("ajax-value"), "<%=BookType%>", $('[ajax-name="BookType"]'));
                    }
                    else {
                        loadKPList();
                    }
                }
            }, function () { }, false);
        }
        //加载知识点章/节
        function loadKPList() {
            var dto = {
                Subject: $('[ajax-name="Subject"] a[class="active"]').attr("ajax-value"),
                BookType: $('[ajax-name="BookType"] a[class="active"]').attr("ajax-value"),
                CheckedKPId: "<%=ParentId%>",
                StuId: "<%=StudentId%>"
            };
            $.ajaxWebService("aggregateAnalysis.aspx/GetKPList", JSON.stringify(dto), function (data) {
                $('[ajax-name="divKPList"]').html(data.d);

                if (data.d == "") {
                    $("#tbWrongReTest").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $(".pagination_container").html("");
                }
                else {
                    pageIndex = 1;
                    loadData();
                }
                $('.mtree-knowledge-hook').mtree({
                    display: 2,
                    url: true,
                    onClick: function (obj) {
                        pageIndex = 1;
                        loadData();
                    }
                });
            }, function () { }, false);
        }

        function loadData() {
            var dto = {
                StuId: "<%=StudentId%>",
                Subject: $('[ajax-name="Subject"]').find('a.active').attr('ajax-value'),
                BookType: $('[ajax-name="BookType"]').find('a.active').attr('ajax-value'),
                ParentId: $('[ajax-name="divKPList"]').find('div.active').attr('ajax-value'),
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 12 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("RemedialPlan.aspx/GetDataList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbWrongReTest").setTemplateElement("template_Res", null, { filter_data: false });
                    $("#tbWrongReTest").processTemplate(json);
                    //$(".pagination_container").pagination(json.TotalCount, {
                    //    current_page: json.PageIndex - 1,
                    //    callback: pageselectCallback,
                    //    items_per_page: json.PageSize
                    //});
                    $('#divAnalysisInfo').html(json.Info);
                }
                else {
                    $("#tbWrongReTest").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                    $(".pagination_container").find("ul").html("");
                }
                //if (json.list == null || json.list == "") {
                //    pageIndex--;
                //    if (pageIndex > 0) {
                //        loadData();
                //    }
                //    else {
                //        pageIndex = 1;
                //    }
                //}
            }, function () { }, false);
        }

        //var pageselectCallback = function (page_index, jq) {
        //    pageIndex = page_index + 1;
        //    loadData();
        //}

    </script>
</asp:Content>
