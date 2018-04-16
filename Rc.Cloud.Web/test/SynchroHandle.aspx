<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SynchroHandle.aspx.cs" Inherits="Rc.Cloud.Web.test.SynchroHandle" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Styles/style001/Style.css" rel="stylesheet" />
    <script language="javascript" src="../Scripts/js001/jquery.min-1.8.2.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <br /><br /><br />
        <div style="margin:auto; text-align:center; width:500px;">
         <div>
            <asp:Button runat="server" ID="btnSynchronousData" ClientIDMode="Static"  Text="同步数据" OnClientClick="oo()" OnClick="btnSynchronousData_Click"  /> <%-- --%>
             (操作此步骤后，运营端可看到生产端新生产的资源数据（数据库数据），此时尚未同步资源文件。)
        </div>
        <br />
        <div>
            需要同步的日期（YYYY-MM-DD）<asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
            
            <asp:Button ID="Button1" runat="server"   ClientIDMode="Static" OnClick="Button1_Click" Text="同步数据文件"  OnClientClick="oo()" />
             (操作此步骤后，运营端可看到生产端新生产的资源数据（数据库数据），以及资源文件。)

        </div>
        <br />
       
        <div>
            <asp:Button runat="server" ID="btnStatisticsData"  ClientIDMode="Static" Text="统计生产数据" OnClick="btnStatisticsData_Click" />
        </div>
            </div>
        <script>
            function oo()
            {
                $("#btnSynchronousData").val("同步进行中....");
                $("#btnSynchronousData").ready = true;
                $("#Button1").val("同步数据文件进行中....");
                $("#Button1").ready = true;
                $("#btnStatisticsData").val("统计生产数据进行中....");
                $("#btnStatisticsData").ready = true;
            }

        </script>
    </form>
</body>
</html>
