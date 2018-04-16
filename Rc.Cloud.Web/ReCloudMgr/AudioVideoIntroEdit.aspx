<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AudioVideoIntroEdit.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.AudioVideoIntroEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script type="text/javascript" src="../SysLib/plugin/jquery.fileupload/jquery-ui-1.9.2.js"></script>
    <script type="text/javascript" src="../SysLib/plugin/jquery.fileupload/jquery.fileupload.js"></script>
    <script type="text/javascript">
        $(function () {
            _id = "<%=AudioVideoIntroId%>";
            index = parent.layer.getFrameIndex(window.name);
            $("#txtAudioVideoUrl").bind({
                blur: function () { if (this.value.length > 30) this.value = this.value.slice(0, 30) },
                keyup: function () { if (this.value.length > 30) this.value = this.value.slice(0, 30) }
            });
            $("#txtFileName").bind({
                blur: function () { if (this.value.length > 30) this.value = this.value.slice(0, 30) },
                keyup: function () { if (this.value.length > 30) this.value = this.value.slice(0, 30) }
            });
            $("#btnSubmit").click(function () {
                if ($.trim($("#ddlAudioVideoTypeEnum").val()) == "-1") {
                    layer.ready(function () {
                        layer.msg("请填写类型", { icon: 4 }, function () { $("#ddlAudioVideoTypeEnum").focus(); });
                    });
                    return false;
                }
                if ($.trim($("#txtAudioVideoUrl").val()) == "") {
                    layer.ready(function () {
                        layer.msg("请填写音/视频名称", { icon: 4 }, function () { $("#txtAudioVideoUrl").focus(); });
                    });
                    return false;
                }
                if (_id == "") {
                    if ($("#hidFileUrl").val() == "") {
                        layer.ready(function () {
                            layer.msg("请上传音/视频文件", { icon: 4 });
                        });
                        return false;
                    }
                }
            });
        });

    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>类型 <span class="text-danger">*</span></label>
                <asp:DropDownList ID="ddlAudioVideoTypeEnum" runat="server" CssClass="form-control" ClientIDMode="Static">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>文件名称：</label>
                <asp:TextBox runat="server" ID="txtFileName" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label style="width: 110px">音/视频名称 <span class="text-danger">*</span></label>
                <asp:TextBox runat="server" ID="txtAudioVideoUrl" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>上传文件</label>
                <input id="fileupload" type="file" class="form-control mb" />
                <span class="proportion mt"></span>
                <!-- 上传进度条及状态： -->
                <div class="progress">
                    <div class="progress-bar" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>
                </div>
                <!-- 预览框： -->
                <div class="preview">
                    <asp:Literal runat="server" ID="ltlPriview"></asp:Literal>
                </div>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            <asp:HiddenField ID="hidFileUrl" runat="server" />
            <asp:HiddenField runat="server" ID="hidFileID" />
        </div>

    </form>
    <script type="text/javascript">
        $('#fileupload').fileupload(
            {
                url: '/Ajax/UploadAPI.ashx',
                dataType: "json",
                multipart: true,
                add: function (e, data) {
                    $(".upstatus").html("");
                    var arrExt = ["flv", "mp3", "mp4"]
                    var fileName = data.files[0].name;
                    var fileExt = fileName.substring(fileName.lastIndexOf(".") + 1);
                    if ($.inArray(fileExt, arrExt) == -1) {
                        $(".upstatus").html("<div style='color:red;'>文件格式不支持，支持格式：" + arrExt.toString() + "</div>");
                        return false;
                    }
                    if (_id == "") {
                        data.submit();
                    }
                    else {
                        layer.ready(function () {
                            layer.confirm('您确定要重新上传文件吗？<br/>此操作会删除原视频，且不可恢复。请谨慎操作！', {
                                btn: ['确定', '取消'], //按钮
                                icon: 3
                            }, function (index) {
                                layer.close(index);
                                data.submit();
                            }, function () {

                            });
                        });
                    }
                },
                done: function (e, data) {
                    if (data.result.sta) {
                        var index = parent.layer.getFrameIndex(window.name);
                        $("#txtAudioVideoUrl").val(data.result.FileName);
                        // 上传成功：
                        $(".preview").html("");
                        $(".preview").append("<video src=\"" + data.result.previewSrc + "\" controls=\"controls\" style=\"width:100%;\">your browser does not support the video tag</video>");
                        $(".preview").append("<div>" + data.result.msg + "</div>");
                        $("#hidFileUrl").val(data.result.previewSrc);
                        parent.layer.iframeAuto(index);
                    } else {
                        // 上传失败：
                        $(".upstatus").html("<div style='color:red;'>" + data.result.msg + "</div>");
                    }

                },
                progressall: function (e, data) {//上传进度
                    var progress = parseInt(data.loaded / data.total * 100, 10);
                    $(".progress .progress-bar").css("width", progress + "%").html(progress + "%");
                }
            }
        );
    </script>
</body>
</html>
