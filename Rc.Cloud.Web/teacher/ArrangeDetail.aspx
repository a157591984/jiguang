<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArrangeDetail.aspx.cs" Inherits="Rc.Cloud.Web.teacher.ArrangeDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <title>布置详情</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid ph">
            <table class="table table-bordered text-center">
                <thead>
                    <tr>
                        <td class="text-left">作业名称</td>
                        <td width="12%">布置时间</td>
                        <td width="8%">人数</td>
                        <td width="8%">提交后是否显示正确答案</td>
                        <td width="8%">是否对学生隐藏作业</td>
                        <td width="12%">作业开始时间</td>
                        <td width="12%">作业结束时间</td>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptHW">
                        <ItemTemplate>
                            <tr>
                                <td class="text-left"><%#Eval("HomeWork_Name") %></td>
                                <td><%#Eval("CreateTime") %></td>
                                <td title="<%#Eval("StudentNames") %>"><%#Eval("StudentCount") %></td>
                                <td><%#Eval("IsShowAnswer").ToString()=="1"?"<span class='text-success'>是</span>":"<span class='text-danger'>否</span>" %></td>
                                <td><%#Eval("IsHide").ToString()=="1"?"<span class='text-success'>是</span>":"<span class='text-danger'>否</span>" %></td>
                                <td><%#Eval("BeginTime") %></td>
                                <td><%#Eval("StopTime") %></td>
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
