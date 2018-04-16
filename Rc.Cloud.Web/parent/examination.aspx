<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/parent.Master" AutoEventWireup="true" CodeBehind="examination.aspx.cs" Inherits="Rc.Cloud.Web.parent.examination" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../Scripts/js001/Tree.js"></script>
    <script type="text/javascript" src="../Scripts/js001/common.js"></script>
    <script type="text/javascript" src="../Scripts/json2.js"></script>
    <script type="text/javascript" src="../Scripts/jq.pagination.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-jtemplates.js"></script>
    <link href="../Styles/style001/pagination.css" rel="stylesheet" />
    <link href="../Styles/style001/Tree.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $(".nav li:eq(0) a").addClass("active");
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--子导航-->
    <div class="header_subnav">
        <ul>
            <li><a href="comperhensive.aspx?struserid=<%=struserid %>">综合排名</a></li>
            <li><a href="subject.aspx?struserid=<%=struserid %>">学科排名</a></li>
            <li><a href="examination.aspx?struserid=<%=struserid %>" class="active">历次作业及考试排名</a></li>
            <%--<li><a href="homework.aspx">历次作业排名</a></li>--%>
            <%--<li><a href="error.aspx">错题情况</a></li>--%>
        </ul>
    </div>
    <!--内容-->
    <div class="main_box">
        <!--左侧-->
        <div class="left_sidebar">
            <ul class="tree pt20" id="datetype1">
                <%=km_html %>
            </ul>
        </div>
        <!--右侧-->
        <div class="right_main_box" data-name="main-auto">
            <div class="container_box">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <td>学科</td>
                            <td>作业名称</td>
                            <td>作业日期</td>
                            <td>得分</td>
                            <td>排名</td>
                            <%--<td>错题情况</td>--%>
                        </tr>
                    </thead>
                    <tbody id="tbRes">
                    </tbody>
                </table>
                <div class="page pagination">
                    <ul>
                    </ul>
                </div>
            </div>
        </div>
        <textarea id="template_Res" class="display_none">
    {#foreach $T.list as record}
    <tr class="tr_con_001">
        <td>{$T.record.SSubjectName }</td>
        <td>{$T.record.HomeWork_Name }</td>
         <td>{$T.record.HomeWorkDate}</td>
         <td>{$T.record.SAverageScore}</td>
         <td>{$T.record.SRank}</td>
    </tr>
    {#/for}
    </textarea>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script lang="javascript" type="text/javascript">

        var ssubject = "";
        //function ShowSubDocument(str, strDoctumentID, strDoctumentName) {
        //    //ShowUpload(str, strDoctumentID)
        //    catalogID = strDoctumentID;
        //    tp = "0";
        //    loadData();
        //}
        var loadData = function () {
            var struserid = '<%=struserid%>';
            var dto = {
                ssubject: ssubject,
                struserid: struserid,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                x: Math.random()
            };
            $.ajaxWebService("examination.aspx/GetStatTest_TestRListPage", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbRes").setTemplateElement("template_Res", null, { filter_data: false });
                    $("#tbRes").processTemplate(json);
                    $(".pagination").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbRes").html("<tr class='tr_con_002'><td colspan='100' align='center'>暂无数据</td></tr>");
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
        };
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        };

        $(function () {
            pageIndex = 1;
            ssubject = $("#datetype1").find("a").eq(0).attr("value");
            tp = "1";
            $(".tree li:eq(0) .name").addClass("active");
            loadData();
            $("#datetype1").find("a").each(function () {
                $(this).click(function () {
                    ssubject = $.trim($(this).attr("value"));
                    SetAClass(this);
                    loadData();
                });
            });
        });
        function SetAClass(obj) {
            $(obj).parent().parent().find("a").each(function () {
                $(this).removeClass("active");
            });
            $(obj).addClass("active");
        }
    </script>
</asp:Content>
