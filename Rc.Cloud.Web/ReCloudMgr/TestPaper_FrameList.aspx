<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="TestPaper_FrameList.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.TestPaper_FrameList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar pb">
                    <input type="button" class="btn btn-primary btn-sm" id="btnAdd" value="新增" onclick="EditData('');" />
                    <asp:DropDownList ID="ddlGradeTerm" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSubject" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlResource_Version" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtkey" runat="server" CssClass="form-control input-sm" placeholder="名称" ClientIDMode="Static"></asp:TextBox>
                    <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>年份</th>
                            <th>年级学期</th>
                            <th>教材版本</th>
                            <th>学科</th>
                            <th>双向细目表名称</th>
                            <th>添加人</th>
                            <th>关联试卷数</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody id="tbRes"></tbody>
                </table>
                <textarea id="template_Res" class="hidden">
                {#foreach $T.list as record}
                <tr class="tr_con_001">
                    <td>{$T.record.Year}</td>
                    <td>{$T.record.GradeTermName}</td>
                    <td>{$T.record.Resource_VersionName}</td>
                    <td>{$T.record.SubjectName}</td>
                    <td>{$T.record.TestPaper_Frame_Name}</td>
                    <td>{$T.record.SysUser_Name}</td>
                    <td>({$T.record.CountTestpaper})份</td>
                    <td class="opera">
                        <%--{#if $T.record.CountAuth=="0" }--%>
                        <a href="javascript:EditDetail('{$T.record.TestPaper_Frame_Id}','{$T.record.TestPaper_Frame_NameUrl}');">编辑明细</a>
                                {#if $T.record.CountQuestions=="0"}
                                <a href="javascript:;" class="disabled">预览</a>
                                <a href="javascript:;" class="disabled">自定义双向细目表</a>
                                <a href="javascript:;" class="disabled">关联试卷</a>
                                {#else}
                                <a href="javascript:View('{$T.record.TestPaper_Frame_Id}','{$T.record.TestPaper_Frame_NameUrl}');">预览</a>
                                <a href="javascript:Customize('{$T.record.TestPaper_Frame_Id}');">自定义双向细目表</a>
                                <a href="javascript:RelationPaper('{$T.record.TestPaper_Frame_Id}','{$T.record.TestPaper_Frame_NameUrl}','{$T.record.Year}','{$T.record.GradeTerm}','{$T.record.Resource_Version}','{$T.record.Subject}');">关联试卷</a>
                                {#/if}
                        <a href="javascript:EditData('{$T.record.TestPaper_Frame_Id}');" >修改</a>
                        <a href="javascript:DeleteData('{$T.record.TestPaper_Frame_Id}');">删除</a>
                        <%--{#else}
                        <a href="javascript:View('{$T.record.TestPaper_Frame_Id}','{$T.record.TestPaper_Frame_NameUrl}');">预览</a>
                        <a href="javascript:RelationPaper('{$T.record.TestPaper_Frame_Id}','{$T.record.TestPaper_Frame_NameUrl}','{$T.record.Year}','{$T.record.GradeTerm}','{$T.record.Resource_Version}','{$T.record.Subject}');">关联试卷</a>
                        {#/if}--%>
            
                    </td>
                </tr>
                {#/for}
                </textarea>
                <hr />
                <div class="page"></div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script>
        //编辑
        function EditData(Id) {
            layer.ready(function () {
                if (Id == "") {
                    title = "新增";
                } else {
                    title = "编辑"
                }
                layer.open({
                    type: 2,
                    title: title,
                    fix: false,
                    area: ["40%", "95%"],
                    content: "TestPaper_FrameEdit.aspx?TestPaper_Frame_Id=" + Id
                })
            })
        }
        function EditDetail(Id, Name) {
            window.location.href = "TestPaper_FrameDetail.aspx?TestPaper_Frame_Id=" + Id + "&TestPaper_Frame_Name=" + escape(Name) + "&backurl=" + backurl;
        }
        //预览
        function View(Id, Name) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '预览',
                    area: ["80%", "80%"],
                    content: "TestPaper_FrameView.aspx?TestPaper_Frame_Id=" + Id + "&TestPaper_Frame_Name=" + escape(Name)
                })
            })
        }
        //自定义双向细目表

        function Customize(Id) {
            window.location.href = "Sxxmb.aspx?TestPaper_Frame_Id=" + Id + "&backurl=" + backurl;
        }
        //关联试卷
        function RelationPaper(Id, Name, Year, GradeTerm, Resource_Version, Subject) {
            layer.ready(function () {
                layer.open({
                    type: 2,
                    title: '关联试卷',
                    area: ["950px", "90%"],
                    content: "RelationPaper.aspx?TestPaper_Frame_Id=" + Id + "&TestPaper_Frame_Name=" + escape(Name) + "&Year=" + Year + "&GradeTerm=" + GradeTerm + "&Resource_Version=" + Resource_Version + "&Subject=" + Subject,
                    cancel: function () { loadData(); }
                })
            })
        }

    </script>
    <script type="text/javascript">
        $(function () {
            b = new Base64();
            pageIndex = 1;
            basicUrl = ""; //本页基础url(不包括页码参数)
            backurl = ""; //跳转所用bas64加页码url
            loadParaFromLink();
            loadData();
            $("#btnSearch").click(function () {
                pageIndex = 1;
                loadData();
            });
            $("#ddlGradeTerm").change(function () {
                pageIndex = 1;
                loadData();
            });
            $("#ddlResource_Version").change(function () {
                pageIndex = 1;
                loadData();
            });
            $("#ddlSubject").change(function () {
                pageIndex = 1;
                loadData();
            });
        });
        var loadData = function () {
            var strGradeTerm = $("#ddlGradeTerm").val();//
            var strSubject = $("#ddlSubject").val();//
            var strResource_Version = $("#ddlResource_Version").val();//
            var TestPaper_Frame_Name = $("#txtkey").val();
            setBasicUrl();
            var dto = {
                TestPaper_Frame_Name: TestPaper_Frame_Name,
                strGradeTerm: strGradeTerm,
                strSubject: strSubject,
                strResource_Version: strResource_Version,
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("TestPaper_FrameList.aspx/GetSxxmbList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tbRes").setTemplateElement("template_Res", null, { filter_data: false });
                    $("#tbRes").processTemplate(json);
                    $(".page").pagination(json.TotalCount, {
                        current_page: json.PageIndex - 1,
                        callback: pageselectCallback,
                        items_per_page: json.PageSize
                    });
                }
                else {
                    $("#tbRes").html("<tr class='tr_con_002'><td colspan='100' align='center'>暂无数据</td></tr>");
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
        var setBasicUrl = function () {
            basicUrl = "TestPaper_FrameList.aspx?";
            backurl = b.encode(basicUrl + "pageIndex=" + pageIndex
                + "&strGradeTerm=" + escape($.trim($("#ddlGradeTerm").val()))
                + "&strSubject=" + escape($.trim($("#ddlSubject").val()))
                + "&strResource_Version=" + escape($.trim($("#ddlResource_Version").val()))
                + "&TestPaper_Frame_Name=" + escape($.trim($("#txtkey").val())));
        }
        var loadParaFromLink = function () {
            pageIndex = getUrlVar("pageIndex") == "" ? 1 : getUrlVar("pageIndex");
            $("#ddlGradeTerm").val(unescape(getUrlVar("strGradeTerm")));
            $("#ddlSubject").val(unescape(getUrlVar("strSubject")));
            $("#ddlResource_Version").val(unescape(getUrlVar("strResource_Version")));
            $("#txtkey").val(unescape(getUrlVar("TestPaper_Frame_Name")));
        }

        //删除
        function DeleteData(TestPaper_Frame_Id) {
            layer.ready(function () {
                layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
                    $.ajaxWebService("TestPaper_FrameList.aspx/DeleteData", "{TestPaper_Frame_Id:'" + TestPaper_Frame_Id + "',x:" + Math.random() + "}", function (data) {
                        if (data.d == "1") {
                            layer.msg("删除成功", { icon: 1, time: 2000 }, function () { loadData(); });
                        }
                        else if (data.d == "2") {
                            layer.msg("删除失败,此试卷结构中有明细。", { icon: 2, time: 2000 });
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
