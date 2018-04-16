<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="AudioVideoIntroList.aspx.cs" Inherits="Rc.Cloud.Web.AuthApi.AudioVideoIntroList" %>

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
            <div class="form-inline">
                <asp:DropDownList ID="ddlAudioVideoTypeEnum" runat="server" ClientIDMode="Static" CssClass="form-control input-sm">
                </asp:DropDownList>
                <asp:TextBox ID="txtFileName" runat="server" placeholder="文件名称" ClientIDMode="Static" CssClass="form-control input-sm"></asp:TextBox>
                <asp:TextBox ID="txtAudioVideoName" runat="server" placeholder="视频/音频名称" ClientIDMode="Static" CssClass="form-control input-sm"></asp:TextBox>
                <input type="button" class="btn btn-primary btn-sm" id="btnSearch" value="查询" clientidmode="Static" />
                <input type="button" class="btn btn-default btn-sm" id="btnBack" value="返回" clientidmode="Static" />
                <%--<input type="button" class="btn btn-primary btn-sm" id="btnAdd" value="新增视/音频" clientidmode="Static" />--%>
            </div>
            <div class="page-header mh">
                正在查看【<%=BookName %>】书本的音视频信息
            </div>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <td>文件名称</td>
                        <td>类型</td>
                        <td>视频/音频名称</td>
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
        <td>{$T.record.FileName}</td>
        <td>{$T.record.AudioVideoTypeEnum}</td>
         <td>{$T.record.AudioVideoName}</td>
        <td>{$T.record.CreateTime}</td>
        <td>
            <%--<a href="javascript:;" onclick="UpdateData('{$T.record.AudioVideoIntroId}')">编辑</a>&nbsp;&nbsp;--%>
            {#if $T.record.AudioVideoUrl!="False"}
            <%--<a href="javascript:;" onclick="ShowData('{$T.record.AudioVideoUrl}')">播放</a>--%>
             <a href="{$T.record.AudioVideoUrl}" id="select_media">选择</a>&nbsp;&nbsp;
           {#else}
            <a href="javascript:;"style="color:red">文件缺失</a>
            {#/if}
            <%--<a href="javascript:;" onclick="DeleteData('{$T.record.AudioVideoIntroId}')">删除</a>--%>
        </td>
    </tr>
    {#/for}
    </textarea>
<script type="text/javascript">
    $(function () {
        pageIndex = 1;
        loadData();

        $("#btnAdd").click(function () {
            UpdateData("", "<%=AudioVideoBookId%>");
        })
        $("#btnSearch").click(function () {
            pageIndex = 1;
            loadData();
        });
        $("#ddlAudioVideoTypeEnum").change(function () {
            pageIndex = 1;
            loadData();
        });
        $("#btnBack").click(function () {
            var backurl = getUrlVar("backurl");
            if (backurl != "") {
                b = new Base64();
                window.location.href = b.decode(backurl);
            }
            else {
                window.location.href = "AudioVideoList.aspx";
            }
        });
    });
    var loadData = function () {
        var AudioVideoTypeEnum = $("#ddlAudioVideoTypeEnum").val();//
        var FileName = $("#txtFileName").val();//
        var AudioVideoName = $("#txtAudioVideoName").val();
        var dto = {
            AudioVideoBookId: "<%=AudioVideoBookId%>",
            AudioVideoTypeEnum: AudioVideoTypeEnum,
            FileName: FileName,
            AudioVideoName: AudioVideoName,
            PageIndex: pageIndex,
            PageSize: ($("[data-name='pagination_select']").val() == undefined ? 10 : $("[data-name='pagination_select']").val()),
            x: Math.random()
        };
        $.ajaxWebService("AudioVideoIntroList.aspx/GetAudioVideoIntroList", JSON.stringify(dto), function (data) {
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

    //编辑
    function UpdateData(AudioVideoIntroId, AudioVideoBookId) {
        if (AudioVideoIntroId == "") {
            title = "新增音/视频";
        } else {
            title = "修改音/视频"
        }
        layer.open({
            type: 2,
            title: title,
            fix: false,
            area: ["650px", "590px"],
            content: "AudioVideoIntroEdit.aspx?AudioVideoBookId=" + AudioVideoBookId + "&AudioVideoIntroId=" + AudioVideoIntroId
        })
    }
    function ShowData(url) {
        title = "播放音/视频"
        layer.open({
            type: 2,
            title: title,
            fix: false,
            area: ["600px", "400px"],
            content: "AudioVideoIntroShow.aspx?AudioVideoUrl=" + url
        })
    }
    //删除
    function DeleteData(AudioVideoIntroId) {
        layer.confirm("确定要删除吗？", { icon: 2, title: "删除提示" }, function () {
            $.ajaxWebService("AudioVideoIntroList.aspx/DeleteData", "{AudioVideoIntroId:'" + AudioVideoIntroId + "',x:" + Math.random() + "}", function (data) {
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
