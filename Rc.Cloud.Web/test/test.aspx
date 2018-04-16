<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="Rc.Cloud.Web.test.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="button" onclick="test1()" value="测试跳转1" />
            <input type="button" onclick="test2()" value="测试跳转2" />
            <input type="button" onclick="test3()" value="测试跳转3" />
            <br />
            <a href="testNewOpen.aspx" target="_blank">超链接-新窗口打开-模拟批改作业</a>
        </div>    
    </form>
</body>
<script>
    function test1() {
        window.open("http://baidu.com");        //客户端无效
    }
    function test2() {
        self.location = 'http://baidu.com';     //无法打开新标签页
    }
    function test3() {
        window.navigate("http://baidu.com");    //浏览器无效
    }
    function modifyNoCorrectCount() {           //子页面调用父级页面方法1
        alert("子页面调用父级页面方法1");
    }
    function loadStudentHomeWorkData() {           //子页面调用父级页面方法2
        alert("子页面调用父级页面方法2");
    }
</script>
</html>
