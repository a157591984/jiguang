<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="Rc.Web.teacher.UploadFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Styles/style001/uploadify.css" rel="stylesheet" />
    <script src="../scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../scripts/swfobject.js" type="text/javascript"></script>
    <script src="../scripts/jquery.uploadify.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $("#file_upload").uploadify({
                //开启调试
                'debug': false,
                //是否自动上传
                'auto': false,
                'buttonText': '浏览',
                //flash
                'swf': "../scripts/uploadify.swf",
                //文件选择后的容器ID
                'queueID': 'uploadfileQueue',
                //fpath为要上传的文件存放的目录，如果没有目录不存在，则新创建目录;默认在：/uploads目录下
                'uploader': '/Ajax/upload.ashx<%=strUrlPara%>',
                'width': '75',
                'height': '24',
                'multi': true,
                'fileTypeDesc': '支持的格式：',
                'fileTypeExts': '*.class;*.dsc;*.xpt;*.doc;*.docx;*.xls;*.xlsx;',
                'fileSizeLimit': '5MB',
                'removeTimeout': 1,
                'fileDataName ': 'a',
                //返回一个错误，选择文件的时候触发
                'onSelectError': function (file, errorCode, errorMsg) {
                    switch (errorCode) {
                        case -100:
                            alert("上传的文件数量已经超出系统限制的" + $('#file_upload').uploadify('settings', 'queueSizeLimit') + "个文件！");
                            break;
                        case -110:
                            alert("文件 [" + file.name + "] 大小超出系统限制的" + $('#file_upload').uploadify('settings', 'fileSizeLimit') + "大小！");
                            break;
                        case -120:
                            alert("文件 [" + file.name + "] 大小异常！");
                            break;
                        case -130:
                            alert("文件 [" + file.name + "] 类型不正确！");
                            break;
                    }
                },
                //检测FLASH失败调用
                'onFallback': function () {
                    alert("您未安装FLASH控件，无法上传文件！请安装FLASH控件后再试。");
                },
                //上传到服务器，服务器返回相应信息到data里
                'onUploadSuccess': function (file, data, response) {
                    //            alert(data);
                    if (data != "-1") {
                        parent.Handel('1', '上传成功')

                    }
                    else {
                        parent.Handel('1', '上传失败:' + data)

                    }
                }
            });
        });
        function doUplaod() {
            $('#file_upload').uploadify('upload', '*');
        }
        function closeLoad() {
            $('#file_upload').uploadify('cancel', '*');
        }
    </script>

</head>
<body>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" id="__01">
        <tr>
            <td id="file_upload"></td>
            <td>
                <img alt="" src="../images/images003/BeginUpload.gif" onclick="doUplaod()" style="cursor: hand; float: left;" /></            </td>
            <td>
                <img alt="" src="../images/images003/CancelUpload.gif" onclick="closeLoad()" style="cursor: hand; float: left;" /></            </td>
        </tr>
        <tr>
            <td colspan="3">
                <div id="uploadfileQueue" style="padding: 3px;"></div>
            </td>
        </tr>
    </table>
</body>
</html>
