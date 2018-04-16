<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="AudioVideoList.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.AudioVideoList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <input type="button" class="btn btn-primary btn-sm" id="btnAdd" value="新增书本" clientidmode="Static" />
                    <asp:DropDownList ID="ddlYear" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlGradeTerm" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlResource_Version" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSubject" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtBookName" runat="server" CssClass="form-control input-sm" placeholder="书本名称" ClientIDMode="Static"></asp:TextBox>
                    <asp:TextBox ID="txtFileName" runat="server" CssClass="form-control input-sm" placeholder="文件名称" ClientIDMode="Static"></asp:TextBox>
                    <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" clientidmode="Static" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>年份</th>
                            <th>年级学期</th>
                            <th>教材版本</th>
                            <th>学科</th>
                            <th>书本名称</th>
                            <th>生产日期</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody id="tbRes">
                    </tbody>
                </table>
                <hr />
                <div class="page"></div>
            </div>
        </div>
    </div>

    <textarea id="template_Res" class="display_none" style="display: none;">
    {#foreach $T.list as record}
    <tr>
        <td>{#if $T.record.ParticularYear!='-1'}{$T.record.ParticularYear}{#/if}</td>
        <td>{$T.record.GradeTermName}</td>
        <td>{#if $T.record.Resource_VersionName!='-1'}{$T.record.Resource_VersionName}{#/if}</td>
         <td>{$T.record.SubjectName}</td>
        <td>{$T.record.BookName}</td>
        <td>{$T.record.CreateTime}</td>
        <td class="opera">
            <a href="javascript:;" onclick="UpdateData('{$T.record.AudioVideoBookId}')">编辑</a>&nbsp;&nbsp;
            <a href="javascript:;" onclick="ShowInfo('{$T.record.AudioVideoBookId}','{$T.record.BookNameUrl}')">进入</a>&nbsp;&nbsp;
            <a href="javascript:;" onclick="DeleteData('{$T.record.AudioVideoBookId}')">删除</a>
        </td>
    </tr>
    {#/for}
    </textarea>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(function () {
            b = new Base64();
            pageIndex = 1;
            basicUrl = ""; //本页基础url(不包括页码参数)
            backurl = ""; //跳转所用bas64加页码url
            loadParaFromLink();
            loadData();

            $("#btnAdd").click(function () {
                UpdateData("");
            })
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
            $("#ddlYear").change(function () {
                pageIndex = 1;
                loadData();
            });
            $("#ddlSubject").change(function () {
                pageIndex = 1;
                loadData();
            });
        });
        var loadData = function () {
            var Year = $("#ddlYear").val();
            var strGradeTerm = $("#ddlGradeTerm").val();
            var strSubject = $("#ddlSubject").val();
            var strResource_Version = $("#ddlResource_Version").val();
            var BookName = $.trim($("#txtBookName").val());
            setBasicUrl();
            var dto = {
                BookName: BookName,
                FileName: $.trim($("#txtFileName").val()),
                Year: Year,
                strGradeTerm: strGradeTerm,
                strSubject: strSubject,
                strResource_Version: strResource_Version,
                PageIndex: pageIndex,
                PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
                x: Math.random()
            };
            $.ajaxWebService("AudioVideoList.aspx/GetAudioVideoBook", JSON.stringify(dto), function (data) {
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
            basicUrl = "AudioVideoList.aspx?";
            backurl = b.encode(basicUrl + "pageIndex=" + pageIndex + "&ddlYear=" + escape($.trim($("#ddlYear").val()))
                + "&ddlGradeTerm=" + escape($.trim($("#ddlGradeTerm").val()))
                + "&ddlSubject=" + escape($.trim($("#ddlSubject").val()))
                + "&ddlResource_Version=" + escape($.trim($("#ddlResource_Version").val()))
                + "&txtBookName=" + escape($.trim($("#txtBookName").val()))
                + "&FileName=" + escape($.trim($("#txtFileName").val())));
        }
        var loadParaFromLink = function () {
            pageIndex = getUrlVar("pageIndex") == "" ? 1 : getUrlVar("pageIndex");
            $("#ddlYear").val(unescape(getUrlVar("ddlYear")));
            $("#ddlGradeTerm").val(unescape(getUrlVar("ddlGradeTerm")));
            $("#ddlSubject").val(unescape(getUrlVar("ddlSubject")));
            $("#ddlResource_Version").val(unescape(getUrlVar("ddlResource_Version")));
            $("#txtBookName").val(unescape(getUrlVar("txtBookName")));
            $("#txtFileName").val(unescape(getUrlVar("FileName")));
        }

        //编辑
        function UpdateData(AudioVideoBookId) {
            layer.ready(function () {
                if (AudioVideoBookId == "") {
                    title = "新增";
                } else {
                    title = "编辑"
                }
                layer.open({
                    type: 2,
                    title: title,
                    fix: false,
                    area: ["385px", "475px"],
                    content: "AudioVideoEdit.aspx?AudioVideoBookId=" + AudioVideoBookId
                })
            });
        }

        function ShowInfo(AudioVideoBookId, BookName) {
            window.location.href = "AudioVideoIntroList.aspx?backurl=" + backurl + "&AudioVideoBookId=" + AudioVideoBookId + "&BookName=" + BookName;
        }
        //删除
        function DeleteData(AudioVideoBookId) {
            layer.ready(function () {
                layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
                    $.ajaxWebService("AudioVideoList.aspx/DeleteData", "{AudioVideoBookId:'" + AudioVideoBookId + "',x:" + Math.random() + "}", function (data) {
                        if (data.d == "1") {
                            layer.msg("删除成功", { icon: 1, time: 1000 }, function () { loadData(); });
                        }
                        else {
                            layer.msg("删除失败", { icon: 2 });
                        }
                    }, function () { })
                });
            });
        }

    </script>
</asp:Content>
