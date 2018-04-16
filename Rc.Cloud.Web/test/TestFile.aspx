<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestFile.aspx.cs" Inherits="Rc.Cloud.Web.test.TestFile" %>

<!DOCTYPE html>
<html>
<head>
    <title>jquery.fileupload.js使用测试</title>
    <script src="../Scripts/js001/jquery.min-1.8.2.js"></script>
    <script src="/Scripts/plug-in/jquery.fileupload/jquery-ui-1.9.2.js"></script>
    <script src="/Scripts/plug-in/jquery.fileupload/jquery.fileupload.js"></script>
    <style type="text/css">
        .bar {
            margin-top: 10px;
            height: 10px;
            max-width: 370px;
            background: green;
        }
    </style>
</head>
<body>
    <input id="fileupload" type="file">
    <span class="proportion"></span>
    <!-- 上传进度条及状态： -->
    <div class="progress">
        <div class="bar" style="width: 0%;"></div>
        <div class="upstatus" style="margin-top: 10px;"></div>
    </div>

    <!-- 预览框： -->
    <div class="preview" style="margin-top: 20px;"></div>
</body>
</html>
<script type="text/javascript">
    $('#fileupload').fileupload(
        {
            url: '/Ajax/UploadAPI.ashx',
            dataType: "json",
            multipart: true,
            done: function (e, data) {
                //done方法就是上传完毕的回调函数，其他回调函数可以自行查看api
                //注意data要和jquery的ajax的data参数区分，这个对象包含了整个请求信息
                //返回的数据在data.result中，这里dataType中设置的返回的数据类型为json
                if (data.result.sta) {
                    // 上传成功：
                    //$(".preview").append("<div style='margin-top:10px;'><embed src=" +
                    //    data.result.previewSrc + " allowscriptaccess='always' " +
                    //    "allowfullscreen='true' wmode='opaque' width='280'>" +
                    //    "</embed></div>");
                    $(".preview").append("<video src=\"" + data.result.previewSrc + "\" controls=\"controls\" autoplay=\"autoplay\" style=\"width:400px;\">your browser does not support the video tag</video>");
                    $(".preview").append("<div>" + data.result.msg + "</div>");
                } else {
                    // 上传失败：
                    $(".upstatus").append("<div style='color:red;'>" + data.result.msg + "</div>");
                }

            },
            progressall: function (e, data) {//上传进度
                var progress = parseInt(data.loaded / data.total * 100, 10);
                $(".progress .bar").css("width", progress + "%");
                $(".proportion").html("上传总进度：" + progress + "%");
            }
        }
    );
</script>
