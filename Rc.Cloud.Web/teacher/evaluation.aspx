<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="Evaluation.aspx.cs" Inherits="Rc.Cloud.Web.teacher.Evaluation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hidClassId" ClientIDMode="Static" />
    <div class="header_subnav">
        <ul>
        </ul>
    </div>
    <div class="main_box">
        <div class="left_sidebar">
            <ul class="tree pt20">
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
        <div class="right_main_box" data-name="main-auto">
            <div class="center_sidebar">
                <h2 class="column_name">本班学生</h2>
                <div class="tree evluation_tree">
                    <ul data-level="0">
                        <li>
                            <!-- <div class="rank">
                                <a href='##'>成绩单</a>
                            </div> -->
                            <div class="name active">
                                <i class="tree_btn fa"></i>
                                <a href='##'>全部</a>
                            </div>
                            <ul data-level="1" id="ulStudent">
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="rightmost_main container_box">
                <h2 class="column_title">老师作业情况统计</h2>
                <div class="searchbar pt20 pb20 clearfix">
                    <span>时间范围</span>
                    <select name="" class="mr20">
                        <option value="">-请选择-</option>
                        <option value="">年度</option>
                        <option value="">上学期</option>
                        <option value="">下学期</option>
                        <option value="">月度</option>
                    </select>
                    <select name="" class="mr20">
                        <option value="">-请选择-</option>
                    </select>
                </div>
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>名称</th>
                            <th>布置次数</th>
                            <th>学生数</th>
                            <th>提交次数</th>
                            <th>提交率</th>
                            <th width="230">统计分析</th>
                        </tr>
                    </thead>
                    <tbody id="tbHW">
                    </tbody>
                </table>
                <div class="page pagination">
                    <ul>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <textarea id="template_Student" style="display: none">
    {#foreach $T.list as record}
        <li>
            <div class="name"><a href='##' val="{$T.record.UserId}">{$T.record.UserName}</a></div>
            <div class="rank">
                <a href="AllRankings.aspx?userId={$T.record.UserId}">各科排名</a>
                <a href="University.aspx?userId={$T.record.UserId}">综合排名</a>
            </div>
        </li>
    {#/for}
    </textarea>
    <textarea id="template_HW" style="display: none">
    {#foreach $T.list as record}
    <tr>
        <td>{$T.record.HomeWork_Name}</td>
        <td>{$T.record.HomeworkCount}</td>
        <td>{$T.record.StudentCount}</td>
        <td>{$T.record.SubmitCount}</td>
        <td>{$T.record.SubmitRate}%</td>
        <td>
            <a href="KnowledgePointAnalysis.aspx">知识点分析</a>
            <a href="ScoreAnalysis.aspx">成绩分析</a>
            <a href="ScoreRank.aspx">成绩排名</a>
        </td>
    </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript" src="../Scripts/js001/Tree.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/json2.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/jq.pagination.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/jquery-jtemplates.js"></script>
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            //导航默认状态
            $(".nav li:eq(4) a").addClass("active");

            $(".left_sidebar .tree .name a").click(function () {
                $(".left_sidebar .tree .name").removeClass("active");
                var _val = $(this).attr("val");
                $("#hidClassId").val(_val);
                $(this).closest('.name').addClass("active");
                loadData();
            });

            //载入页面左侧默认状态
            $(".left_sidebar .tree .name:eq(0) a").click();

        })
        function ShowSubDocument(obj, _iid) {
            $(".left_homework_list li a").removeClass();
            $(obj).addClass("active");
            homeWork_Id = _iid;
            loadData();
        }
        var loadData = function () {
            var dto = {
                GroupId: $("#hidClassId").val(),
                x: Math.random()
            };
            $.ajaxWebService("Evaluation.aspx/GetGroupMember", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#ulStudent").setTemplateElement("template_Student", null, { filter_data: false });
                    $("#ulStudent").processTemplate(json);

                    if (json.list == null || json.list == "") {
                        $("#ulStudent").html("<li><div class='name'><a href='##'>暂无数据</a></div></li>");
                    }
                    var _groupId = $("#tbSHW li:eq(0) a").attr("val");
                    $("#tbSHW li .name:eq(0)").addClass("active");
                    loadHomeWorkData(_groupId);
                }
                else {
                    $("#ulStudent").html("<li><div class='name'><a href='##'>暂无数据</a></div></li>");
                    //$("#ulYJ").html("<tr><td colspan='5'>暂无数据</td></tr>");
                    //$("#ulWJ").html("<tr><td>暂无数据</td></tr>");
                }
            }, function () { }, false);
        }
        var loadHomeWorkData = function (groupId) {
            var dto = {
                GroupId: groupId,
                StudentId: "",
                DateType: "",
                DateValue: "",
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                PageIndex: pageIndex,                
                x: Math.random()
            };
            $.ajaxWebService("Evaluation.aspx/GetHomework", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbHW").setTemplateElement("template_HW", null, { filter_data: false });
                    $("#tbHW").processTemplate(json);
                    $(".pagination").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbHW").html("<tr><td colspan='100'>暂无数据</td></tr>");
                    $(".pagination").find("ul").html("");
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
            }, function () { }, false);
        }
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
    </script>
</asp:Content>
