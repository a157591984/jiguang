<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonDetail.aspx.cs" Inherits="Rc.Cloud.Web.teacher.PersonDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <title>负责人详情</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pa">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>姓名</th>
                        <th width="8%">状态</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptHW">
                        <ItemTemplate>
                            <tr>
                                <td><%#(string.IsNullOrEmpty(Eval("TrueName").ToString())?Eval("UserName").ToString():Eval("TrueName").ToString()) %>（<%#Eval("UserName") %>）</td>
                                <td width="20%"><%# Convert.ToInt32( Eval("fileCount"))>0?"<span class='text-success'>文件已上传</span>":"<span class='text-danger'>文件未上传</span>" %></td>

                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr style="display: <%#rptHW.Items.Count==0?"":"none"%>">
                                <td colspan="100">暂无数据</td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
