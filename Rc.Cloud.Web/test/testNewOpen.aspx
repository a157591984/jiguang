<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testNewOpen.aspx.cs" Inherits="Rc.Cloud.Web.test.testNewOpen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <title></title>
    <script>
        $(function () {
            $("#btnClose").click(function () {
                window.opener.modifyNoCorrectCount();
                window.opener.loadStudentHomeWorkData();
                window.close();
            });
            
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="button" value="关闭窗口，调用父页面方法" id="btnClose" />
        </div>
    </form>
</body>
</html>
