﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="Rc.Cloud.Web.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>系统提示</title>
    <link href="Styles/style001/Global.css" rel="stylesheet" />
    <style type="text/css">
        body {
            background:#fff;
            font-family: '微软雅黑';
            color: #333;
            font-size: 16px;
        }

        .system-message {
            padding: 24px 48px;
        }

        .system-message h1 {
            font-size: 100px;
            font-weight:normal;
            line-height: 120px;
            margin-bottom: 12px;
        }

        .system-message .jump {
            padding-top: 10px;
        }

        .system-message .jump a {
            color: #333;
        }

        .system-message .success, .system-message .error {
            line-height: 1.8em;
            font-size: 36px;
            font-weight:300;
        }

        .system-message .detail {
            font-size: 12px;
            line-height: 20px;
            margin-top: 12px;
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="system-message">
            <h1>:(</h1>
            <asp:Literal ID="ltErrorInfo" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
