<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="AudioVideoList.aspx.cs" Inherits="Rc.Cloud.Web.AuthApi.AudioVideoList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../js/jquery.min-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../plugin/layer/layer.js"></script>
    <script type="text/javascript" src="../../js/function.js"></script>
    <script type="text/javascript" src="../../js/json2.js"></script>
    <script type="text/javascript" src="../../js/jq.pagination.js"></script>
    <script type="text/javascript" src="../../js/jquery-jtemplates.js"></script>
    <script type="text/javascript" src="../../js/base64.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid ph">
            <div class="form-inline mb">
                <asp:DropDownList ID="ddlYear" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlGradeTerm" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlResource_Version" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlSubject" CssClass="form-control input-sm" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
                <asp:TextBox ID="txtBookName" runat="server" CssClass="form-control input-sm" placeholder="书本名称" ClientIDMode="Static"></asp:TextBox>
                <input type="button" class="btn btn-primary btn-sm" id="btnSearch" value="查询" clientidmode="Static" />
                <%-- <input type="button" class="btn btn-primary btn-sm" id="btnAdd" value="新增书本" clientidmode="Static" />--%>
            </div>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <td>年份</td>
                        <td>年级学期</td>
                        <td>教材版本</td>
                        <td>学科</td>
                        <td>书本名称</td>
                        <td>生产日期</td>
                        <td width="180">操作</td>
                    </tr>
                </thead>
                <tbody id="tbRes">
                </tbody>
            </table>
            <div class="pagination_container">
                <ul class="pagination"></ul>
            </div>
        </div>
    </form>
</body>
<textarea id="template_Res" class="hidden">
    {#foreach $T.list as record}
    <tr>
        <td>{$T.record.ParticularYear}</td>
        <td>{$T.record.GradeTermName}</td>
         <td>{$T.record.Resource_VersionName}</td>
         <td>{$T.record.SubjectName}</td>
        <td>{$T.record.BookName}</td>
        <td>{$T.record.CreateTime}</td>
        <td>

        <a href="AudioVideoIntroList.aspx?AudioVideoBookId={$T.record.AudioVideoBookId}&BookName={$T.record.BookNameUrl}">进入</a>
            </td>
        <%--<td align="center">
            <a href="javascript:;" onclick="UpdateData('{$T.record.AudioVideoBookId}')">编辑</a>&nbsp;&nbsp;
            
            <a href="javascript:;" onclick="DeleteData('{$T.record.AudioVideoBookId}')">删除</a>
        </td>--%>
    </tr>
    {#/for}
    </textarea>

<script type="text/javascript">
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
        var Year = $("#ddlYear").val();//
        var strGradeTerm = $("#ddlGradeTerm").val();//
        var strSubject = $("#ddlSubject").val();//
        var strResource_Version = $("#ddlResource_Version").val();//
        var BookName = $("#txtBookName").val();
        setBasicUrl();
        var dto = {
            BookName: BookName,
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
                $(".pagination_container").pagination(json.TotalCount, {
                    current_page: json.PageIndex - 1,
                    callback: pageselectCallback,
                    items_per_page: json.PageSize
                });
            }
            else {
                $("#tbRes").html("<tr class='tr_con_002'><td colspan='100' align='center'>暂无数据</td></tr>");
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
        if (AudioVideoBookId == "") {
            title = "新增书本";
        } else {
            title = "修改书本"
        }
        layer.open({
            type: 2,
            title: title,
            fix: false,
            area: ["650px", "590px"],
            content: "AudioVideoEdit.aspx?AudioVideoBookId=" + AudioVideoBookId
        })
    }

    function ShowInfo(AudioVideoBookId, BookName) {
        window.location.href = "AudioVideoIntroList.aspx?AudioVideoBookId=" + AudioVideoBookId + "&BookName=" + BookName;
    }
    //删除
    function DeleteData(AudioVideoBookId) {
        layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
            $.ajaxWebService("AudioVideoList.aspx/DeleteData", "{AudioVideoBookId:'" + AudioVideoBookId + "',x:" + Math.random() + "}", function (data) {
                if (data.d == "1") {
                    layer.msg("删除成功", { icon: 1, time: 2000 }, function () { loadData(); });
                }
                else {
                    layer.msg("删除失败", { icon: 2, time: 2000 });
                }
            }, function () { })
        });
    }

</script>
</html>
