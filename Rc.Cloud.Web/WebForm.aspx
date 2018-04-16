<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm.aspx.cs" Inherits="Rc.Cloud.Web.WebForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="LoginRedirect.aspx?code=00002&uri=<%=PHHC.Share.StrUtility.clsUtility.Encrypt("InProgress.aspx?mid=20102000") %>">跳转</a>
        </div>
    </form>
</body>
</html>
