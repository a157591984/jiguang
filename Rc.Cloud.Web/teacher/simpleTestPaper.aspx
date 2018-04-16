<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="simpleTestPaper.aspx.cs" Inherits="Rc.Cloud.Web.teacher.simpleTestPaper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-subnav">
        <ul class="subnav">
            <li><a href="cHomework.aspx?ugid=<%=strUserGroup_IdActivity %>">习题集</a></li>
            <li><a href="mHomework.aspx?ugid=<%=strUserGroup_IdActivity %>">自有习题集</a></li>
            <li id="apHomework" runat="server" visible="false"><a href="pHomework.aspx?ugid=<%=strUserGroup_IdActivity %>">集体备课习题集</a></li>
            <%--<li id="testPaper" runat="server" visible="false"><a href="javascript:;" onclick="btnZJ()" class="active">组卷</a></li>
            <li id="testPaper2" runat="server" visible="false"><a href="javascript:;" onclick="btnYZSJ()">已组试卷</a></li>--%>
            <li class="active dropdown" id="testPaper" runat="server" visible="false">
                <a href="javascript:;" data-toggle="dropdown">组卷<i class="material-icons">&#xE5C5;</i></a>
                <ul class="dropdown-menu">
                    <li id="cptestPaper" runat="server" visible="false"><a href="ChapterAssembly.aspx?ugid=<%=strUserGroup_IdActivity %>">章节组卷</a></li>
                    <li id="twtestPaper" runat="server" visible="false" class="active"><a href="javascript:;" onclick="btnZJ();">双向细目表组卷</a></li>
                </ul>
            </li>
            <li class="dropdown" id="testPaper2" runat="server" visible="false">
                <a href="javascript:;" data-toggle="dropdown">已组试卷<i class="material-icons">&#xE5C5;</i></a>
                <ul class="dropdown-menu">
                    <li id="cptestPaper2" runat="server" visible="false"><a href="ChapterTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>">章节组卷</a></li>
                    <li id="twtestPaper2" runat="server" visible="false"><a href="javascript:;" onclick="btnYZSJ();">双向细目表组卷</a></li>
                </ul>
            </li>
        </ul>
    </div>
    <div class="iframe-container">
        <div class="iframe-sidebar">
            <div class="mtree mtree-hook">
                <ul id="tbTwo">
                    <asp:Literal ID="ltlTwo_WayChecklist" runat="server"></asp:Literal>
                </ul>
            </div>
        </div>
        <div class="iframe-main">
            <div class="iframe-main-section pa">
                <%--<div class="sxxmbfx_panel">
                        <div class="sxxmbfx_heading">双向细目表分析</div>
                        <div class="sxxmbfx_body">
                            <img id="Analysis" onerror="this.src='../Images/sxsmbfx.png'" src="" />
                        </div>
                    </div>
                    <div class="sxxmb">--%>
                <%--<div class="mb" id="Info"></div>--%>
                <div class="text-right mb" id="btnTestpaper">
                    <a href='##' onclick="Custom()" id="AddCustom" class="btn btn-primary">自定义双向细目表</a>
                    <a href='##' onclick="UpdateCustom()" id="UpdateCustom" class="btn btn-primary">修改双向细目表</a>
                    <a href='##' onclick="RenewCustom()" id="RenewCustom" class="btn btn-primary">恢复默认双向细目表</a>
                    <a href="javascript:;" onclick="btnYZSJ()" class="btn btn-primary">已组试卷</a>
                    <a href='##' onclick="CombinationTestPaper()" class="btn btn-primary">开始组卷</a>
                </div>
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th width="10%">题号</th>
                            <th width="10%">题型</th>
                            <th>测量目标</th>
                            <th>知识点</th>
                            <th width="10%">难易度</th>
                            <th width="8%">分值</th>
                            <th width="8%">试题数</th>
                        </tr>
                    </thead>
                    <tbody id="tb1"></tbody>
                </table>
            </div>
            <%--</div>--%>
        </div>
    </div>
    <textarea id="template_1" class="hidden">
    {#foreach $T.list as record}
     <tr>
        <td>{$T.record.TestQuestions_NumStr}</td>
        <td>{$T.record.TestQuestions_Type}</td>
        <td>{$T.record.TargetText}</td>
        <td>{$T.record.KnowledgePoint}</td>
        <td>{$T.record.ComplexityText}</td>
        <td>{$T.record.Score}</td>
         <td>{$T.record.SumQuestions}</td>
     </tr>
    {#/for}
    </textarea>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#btnTestpaper").hide();
            $("#AddCustom").hide();
            $("#UpdateCustom").hide();
            $("#RenewCustom").hide();
            Two_WayChecklist_Id_ = "<%=Two_WayChecklist_Id%>";

            var IsAdd = $("#tbTwo div.active").attr("tt");
            if (IsAdd == "1") {
                $("#AddCustom").show();
                $("#UpdateCustom").hide();
                $("#RenewCustom").hide();
            }
            if (IsAdd == "2") {
                $("#AddCustom").hide();
                $("#UpdateCustom").show();
                $("#RenewCustom").show();
            }

            $('.mtree-hook').mtree({
                onClick: function (obj) {
                    Two_WayChecklist_Id_ = Two_WayChecklist_Id = obj.data('id');
                    //LoadInfo(Two_WayChecklist_Id);
                    loadData(Two_WayChecklist_Id);
                    var IsAdd = obj.attr("tt");
                    if (IsAdd == "1") {
                        $("#AddCustom").show();
                        $("#UpdateCustom").hide();
                        $("#RenewCustom").hide();
                    }
                    if (IsAdd == "2") {
                        $("#AddCustom").hide();
                        $("#UpdateCustom").show();
                        $("#RenewCustom").show();
                    }
                },
                onLoad: function (obj) {
                    if (Two_WayChecklist_Id_ == "") {
                        obj.find('.mtree-link-hook eq(0)').click();
                    } else {
                        //LoadInfo(Two_WayChecklist_Id_);
                        loadData(Two_WayChecklist_Id_);
                    }
                }
            });

        })
        //function ShowList(obj, Two_WayChecklist_Id) {
        //    Two_WayChecklist_Id_ = Two_WayChecklist_Id;
        //    //LoadInfo(Two_WayChecklist_Id);
        //    loadData(Two_WayChecklist_Id);
        //    var IsAdd = $(obj).attr("tt");
        //    if (IsAdd == "1") {
        //        $("#AddCustom").show();
        //        $("#UpdateCustom").hide();
        //        $("#RenewCustom").hide();
        //    }
        //    if (IsAdd == "2") {
        //        $("#AddCustom").hide();
        //        $("#UpdateCustom").show();
        //        $("#RenewCustom").show();
        //    }
        //}
        function LoadInfo(Two_WayChecklist_Id) {
            var dto = {
                Two_WayChecklist_Id: Two_WayChecklist_Id,
                x: Math.random()
            };
            $.ajaxWebService("simpleTestPaper.aspx/LoadInfo", JSON.stringify(dto), function (data) {
                if (data.d != "") {
                    $("#Info").html(data.d);
                }
                else { $("#Info").html(""); }
            }, function () { });
        }
        function loadData(Two_WayChecklist_Id) {
            var dto = {
                Two_WayChecklist_Id: Two_WayChecklist_Id,
                x: Math.random()
            };
            $.ajaxWebService("simpleTestPaper.aspx/GetTwo_WayChecklist", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#btnTestpaper").show();
                    $("#tb1").setTemplateElement("template_1", null, { filter_data: false });
                    $("#tb1").processTemplate(json);
                    $("#Analysis").attr('src', json.Analysis);
                }
                else {
                    $("#tb1").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                }
                if (json.list == null || json.list == "") {

                }
            }, function () { });
        }
        function CombinationTestPaper() {
            window.open("CombinationTestPaper.aspx?ugid=<%=strUserGroup_IdActivity%>&Two_WayChecklist_Id=" + Two_WayChecklist_Id_);
        }
        function Custom() {
            var TestPaper_Frame_Id = $("#tbTwo div.active").attr("tf_id");
            var IsAdd = $("#tbTwo div.active").attr("tt");
            var Two_WayChecklist_Name = $("#tbTwo div.active .mtree-name-hook").html();
            window.location.href = "CustomTwo_WayChecklist.aspx?Two_WayChecklist_Id=" + Two_WayChecklist_Id_ + "&OperationType=Add&TestPaper_Frame_Id=" + TestPaper_Frame_Id + "&Two_WayChecklist_Name=" + Two_WayChecklist_Name + "&strUserGroup_IdActivity=<%=strUserGroup_IdActivity %>";
        }
        function UpdateCustom() {
            var TestPaper_Frame_Id = $("#tbTwo div.active").attr("tf_id");
            var IsAdd = $("#tbTwo div.active").attr("tt");
            var Two_WayChecklist_Name = $("#tbTwo div.active .mtree-name-hook").html();
            window.location.href = "CustomTwo_WayChecklist.aspx?Two_WayChecklist_Id=" + Two_WayChecklist_Id_ + "&OperationType=Update&TestPaper_Frame_Id=" + TestPaper_Frame_Id + "&Two_WayChecklist_Name=" + Two_WayChecklist_Name + "&strUserGroup_IdActivity=<%=strUserGroup_IdActivity %>";
        }
        function RenewCustom() {
            layer.confirm("确定要恢复系统默认双向细目表？恢复后自定义的将会被删除，请慎重操作。", { icon: 4, title: "提示" }, function () {
                var dto = {
                    Two_NewWayChecklist_Id: Two_WayChecklist_Id_,
                    x: Math.random()
                };
                $.ajaxWebService("simpleTestPaper.aspx/RenewCustom", JSON.stringify(dto), function (data) {
                    if (data.d != "") {
                        layer.msg("恢复系统默认双向细目表成功。", { time: 2000, icon: 1 }, function () {
                            window.location.href = "simpleTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>&Two_WayChecklist_Id=" + data.d;
                        });
                    }
                    else {
                        layer.msg("恢复系统默认双向细目表失败，请重试。", { time: 2000, icon: 2 });
                        return false;
                    }
                }, function () { });
            })
        }
        function btnZJ() {
            window.location.href = "simpleTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>&Two_WayChecklist_Id=" + Two_WayChecklist_Id_;
        }
        function btnYZSJ() {
            window.location.href = "historyTestPaper.aspx?ugid=<%=strUserGroup_IdActivity %>&Two_WayChecklist_Id=" + Two_WayChecklist_Id_;
        }
    </script>
</asp:Content>
