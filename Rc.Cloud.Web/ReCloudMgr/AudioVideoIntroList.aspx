<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="AudioVideoIntroList.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.AudioVideoIntroList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-heading">
                <div class="panel-title">
                    正在维护【<%=BookName %>】书本的音视频信息
                </div>
            </div>
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <input type="button" class="btn btn-primary btn-sm" id="btnAdd" value="新增视/音频" clientidmode="Static" />
                    <asp:DropDownList ID="ddlAudioVideoTypeEnum" CssClass="form-control input-sm" runat="server" ClientIDMode="Static">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtFileName" runat="server" CssClass="form-control input-sm" placeholder="文件名称" ClientIDMode="Static"></asp:TextBox>
                    <asp:TextBox ID="txtAudioVideoName" runat="server" CssClass="form-control input-sm" placeholder="视频/音频名称" ClientIDMode="Static"></asp:TextBox>
                    <input type="button" class="btn btn-default btn-sm" id="btnSearch" value="查询" clientidmode="Static" />
                    <input type="button" class="btn btn-default btn-sm" id="btnBack" value="返回" clientidmode="Static" />
                </div>
                <table class="table table-hover table-bordered">
                    <thead>
                        <tr>
                            <th>文件名称</th>
                            <th>类型</th>
                            <th>视频/音频名称</th>
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
    <textarea id="template_Res" class="hidden">
    {#foreach $T.list as record}
    <tr>
        <td>{$T.record.FileName}</td>
        <td>{$T.record.AudioVideoTypeEnumName}</td>
         <td>{$T.record.AudioVideoName}</td>
        <td>{$T.record.CreateTime}</td>
        <td class="opera">
            <a href="javascript:;" onclick="UpdateData('{$T.record.AudioVideoIntroId}','{$T.record.AudioVideoBookId}')">编辑</a>&nbsp;&nbsp;
            {#if $T.record.AudioVideoUrl!="False"}
            <a href="javascript:;" onclick="ShowData('{$T.record.AudioVideoUrl}')">播放</a>&nbsp;&nbsp;
            {#else}
            <a href="javascript:;" class="text-danger">文件缺失</a>&nbsp;&nbsp;
            {#/if}
            <a href="javascript:;" onclick="DeleteData('{$T.record.AudioVideoIntroId}')">删除</a>
        </td>
    </tr>
    {#/for}
    </textarea>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
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

        //编辑
        function UpdateData(AudioVideoIntroId, AudioVideoBookId) {
            layer.ready(function () {
                if (AudioVideoIntroId == "") {
                    title = "新增音/视频";
                } else {
                    title = "编辑音/视频"
                }
                layer.open({
                    type: 2,
                    title: title,
                    offset: '10px',
                    area: ["385px", "550px"],
                    content: "AudioVideoIntroEdit.aspx?AudioVideoBookId=" + AudioVideoBookId + "&AudioVideoIntroId=" + AudioVideoIntroId
                })
            });
        }
        function ShowData(url) {
            layer.ready(function () {
                title = "播放音/视频"
                layer.open({
                    type: 2,
                    title: title,
                    fix: false,
                    area: ["600px", "400px"],
                    content: "AudioVideoIntroShow.aspx?AudioVideoUrl=" + url
                })
            });
        }
        //删除
        function DeleteData(AudioVideoIntroId) {
            layer.ready(function () {
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
            });
        }

    </script>
</asp:Content>
