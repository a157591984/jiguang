<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrachPlanPreview.aspx.cs" Inherits="Rc.Cloud.Web.teacher.TrachPlanPreview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Styles/style001/Style.css" rel="stylesheet" />
    <style>
        .preview_container { width: 100%; margin: 0 auto; }
        .preview_container img { max-width: 100%; display: block; border-bottom: #ccc solid 1px; }
        .load_more { width: 100px; margin: 20px auto; display: block; float: none; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="preview_container">
            <iframe src="<%=strViewUrl %>" width="1000" height="500"></iframe>
        </div>
        <a href="##" class="load_more create_btn">加载更多</a>
    </form>
</body>
</html>
