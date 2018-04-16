<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testIframe.aspx.cs" Inherits="Rc.Cloud.Web.test.test_ifarme" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script>
        $(function () {
            $('#btn1').on('click', function () {
                $('body').append('<div style="width:500px; height:500px; border:#ccc solid 1px; margin:auto; position:fixed; top:0; bottom:0; left:0; right:0;"><iframe src="http://localhost:1528/teacher/layoutIframe.aspx?rtrId=af96d8cc-902d-4b85-9ade-ef5130880aed&classId=72577" width="500px" height="500px" frameborder="no"></iframe></div>');
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <button type="button" id="btn1">新增一个iframe到页面</button>
        </div>
    </form>
</body>
</html>
