<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/parent.Master" AutoEventWireup="true" CodeBehind="HistoryHomework.aspx.cs" Inherits="Rc.Cloud.Web.parent.HistoryHomework" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../plugin/laydate/laydate.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="LatestHomework.aspx">最新作业</a></li>
            <li><a href="HistoryHomework.aspx" class="active">历次作业</a></li>
            <li><a href="WrongQuestion.aspx">错题集</a></li>
        </ul>
    </div>
    <div class="iframe-container">
        <div class="container pt">
            <div class="filter">
                <div class="form-inline">
                    <div class="form-group">
                        <label>时间：</label>
                        <input class="form-control input-sm" id="StartTime" placeholder="开始时间" clientidmode="Static" type="text">
                        <input class="form-control input-sm" id="EndTime" placeholder="结束时间" clientidmode="Static" type="text">
                    </div>
                    <div class="form-group">
                        <label>书目名称：</label>
                        <input class="form-control input-sm" id="txtBookName" type="text" placeholder="书目名称" clientidmode="Static">
                    </div>
                    <input class="btn btn-primary btn-sm" id="btnSearch" value="确定" clientidmode="Static" type="button">
                </div>
                <div class="filter_section">
                    <div class="filter_row clearfix">
                        <span class="row_name">我家宝贝：</span>
                        <div class="row_item">
                            <ul ajax-name="student">
                                <asp:Literal ID="ltlBabyName" runat="server" ClientIDMode="Static"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">学科：</span>
                        <div class="row_item">
                            <ul ajax-name="subject">
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-bordered text-center">
                <thead>
                    <tr>
                        <td width="10%">日期</td>
                        <td width="23%" class="text-left">作业名称</td>
                        <td width="23%">作业来源</td>
                        <td width="8%">学科</td>
                        <td width="8%">试卷总分</td>
                        <td width="8%">我的得分</td>
                        <td width="8%">班级排名</td>
                        <!-- <td>布置作业时间</td> -->
                        <%--<td width="200">答题时间</td>--%>
                        <!-- <td>状态</td> -->
                        <td class="align_center">操作</td>
                    </tr>
                </thead>
                <tbody id="tbHW">
                </tbody>
            </table>
            <div class="pagination_container">
                <ul class="pagination"></ul>
            </div>
        </div>
    </div>
    <textarea id="template_HW" style="display: none">
    {#foreach $T.list as record}
    
         <tr>
        <td>{$T.record.MakeTime}</td>
        <td class="text-left">{$T.record.HomeWork_Name}</td>
        <td>{$T.record.BookName}</td>
        <td>{$T.record.SubjectName}</td>
        <td>{$T.record.HWScore}</td>
             <td>{$T.record.StudentScore}</td>
        <!-- <td><p>{\$T.record.BeginTime}</p></td> -->
        <td>{$T.record.StudentScoreOrder}</td>
        <%--<td><p>开始 {$T.record.OpenTime}</p>
            <p>结束 {$T.record.Student_AnswerTime}</p>
        </td>--%>
        <!-- <td><font color="green">已交</font></td> -->
        <td class="align_center table_opera"> 
            {#if $T.record.TJ=="0"}
            <a href='##' class="disabled" title="学生未提交">批改详情</a>  
            <a href='##' class="disabled" title="学生未提交">分析报告</a> 
            {#else}
            {#if $T.record.PG=="0"}
                {#if $T.record.CorrectMode=='1'}
                <a href="../student/ohomeworkview_client.aspx?ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&HomeWork_Id={$T.record.HomeWork_Id}&StudentId={$T.record.Student_Id}&Student_HomeWork_Id={$T.record.Student_HomeWork_Id}" class="danger" target="_blank" title="老师未批改">批改详情</a> 
                {#else}
                <a href="../student/oHomeWorkViewTT.aspx?ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&HomeWork_Id={$T.record.HomeWork_Id}&StudentId={$T.record.Student_Id}" class="danger" target="_blank" title="老师未批改">批改详情</a> 
                {#/if}
                <a href='##' class="disabled" title="老师未批改">分析报告</a> 
            {#else}
            <a href="../student/CheckStudentStatsHelper.aspx?ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&HomeWork_Id={$T.record.HomeWork_Id}&StudentId={$T.record.Student_Id}&HomeWork_Name={$T.record.HomeWork_Name}&type=1&Student_HomeWork_Id={$T.record.Student_HomeWork_Id}&CorrectMode={$T.record.CorrectMode}" target="_blank">批改详情</a>
             <a href="../student/CheckStudentStatsHelper.aspx?ResourceToResourceFolder_Id={$T.record.ResourceToResourceFolder_Id}&HomeWork_Id={$T.record.HomeWork_Id}&StudentId={$T.record.Student_Id}&HomeWork_Name={$T.record.HomeWork_Name}&type=2&Student_HomeWork_Id={$T.record.Student_HomeWork_Id}&CorrectMode={$T.record.CorrectMode}" target="_blank">分析报告</a> 
            {#/if}
            {#/if}     
        </td>
    </tr>
    
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            pageIndex = 1;
            $('[ajax-name="student"] li a').removeClass("active");
            $('[ajax-name="student"] li a').eq(0).addClass("active");
            $('[ajax-name="student"] li a').on({
                click: function () {
                    $('[ajax-name="student"] li a').removeClass("active").eq(0).addClass("active");
                    //SubjectId = $(this).attr("SubjectId");
                    $('[ajax-name="student"] li a').removeClass("active");
                    $(this).addClass("active");
                    pageIndex = 1;
                    loadSchoolUrl();
                    loadSubject();
                    //loadBookName();
                    loadData();
                }
            });
            loadSubject();
            loadSchoolUrl();
            //loadBookName();
            $('[ajax-name="subject"] li a').removeClass("active");
            $('[ajax-name="subject"] li a').eq(0).addClass("active");
            $(document).on({
                click: function () {
                    $('[ajax-name="subject"] li a').removeClass("active").eq(0).addClass("active");
                    //SubjectId = $(this).attr("SubjectId");
                    $('[ajax-name="subject"] li a').removeClass("active");
                    $(this).addClass("active");
                    pageIndex = 1;
                    loadData();
                }
            }, '[ajax-name="subject"] li a');

            $("#ddlBookName").change(function () {
                pageIndex = 1;
                loadData();
            })

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
            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();

            })
            laydate(StarTime);
            laydate(EndTime);
            loadData();
        })
        var loadData = function () {
            var dto = {
                BabyId: $('[ajax-name="student"]').find('a.active').attr('BabyId'),
                SubjectId: $('[ajax-name="subject"]').find('a.active').attr('SubjectId'),
                ReName: $("#txtBookName").val(),
                HomeWorkCreateTime: $("#StartTime").val(),
                HomeWorkFinishTime: $("#EndTime").val(),
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                PageIndex: pageIndex,
                x: Math.random()
            };

            $.ajaxWebService("HistoryHomework.aspx/GetoHomework", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbHW").setTemplateElement("template_HW", null, { filter_data: false });
                    $("#tbHW").processTemplate(json);
                    $(".pagination_container").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbHW").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
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
        var pageselectCallback = function (page_index, jq) {
            pageIndex = page_index + 1;
            loadData();
        }
        var loadSubject = function () {
            var dto = {
                BabyId: $('[ajax-name="student"]').find('a.active').attr('BabyId'),
                x: Math.random()
            };
            $.ajaxWebService("HistoryHomework.aspx/GetSubject", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $('[ajax-name="subject"]').html(data.d);
                }
            }, function () { }, false);

        }
        var loadSchoolUrl = function () {
            var _stuId = $('[ajax-name="student"]').find('a.active').attr('BabyId');
            var dto = {
                BabyId: _stuId,
                x: Math.random()
            };
            if (GetCookie("UserPublicUrl_Cookie" + _stuId) == null) {
                $.ajaxWebService("HistoryHomework.aspx/loadSchoolUrl", JSON.stringify(dto), function (data) {
                    var json = $.parseJSON(data.d);
                    if (json.err == "") {
                        onlinecheck(_stuId, json.local_url, json.local_url_en);
                    }
                }, function () { }, false);
            }
        }
        var onlinecheck = function (stuId, local_url, local_url_en) {
            var idx = "";
            var xhr = $.ajax({
                async: true,
                timeout: 2000,
                type: "get",
                url: local_url + "AuthApi/onlinecheck.ashx",
                data: "",
                dataType: "text",
                beforeSend: function () {
                    idx = layer.load();
                },
                complete: function (XMLHttpRequest, status) {
                    if (status == 'timeout') {//超时,status还有success,error等值的情况
                        layer.close(idx);
                    }
                },
                success: function (data) {
                    if (data == "ok") {
                        // 局域网成功连接，局域网地址保存cookie
                        SetCookie("UserPublicUrl_Cookie" + stuId, local_url_en);
                    }
                    layer.close(idx);
                },
                error: function () {
                    // 局域网地址请求异常（不保存局域网cookie）
                    layer.close(idx);
                }
            });
            setTimeout(function () {
                if (xhr && xhr.readystate != 4) {
                    xhr.abort();
                }
            }, 2000);
        }
    </script>
</asp:Content>
