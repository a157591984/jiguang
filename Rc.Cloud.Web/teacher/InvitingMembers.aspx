<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvitingMembers.aspx.cs" Inherits="Rc.Cloud.Web.teacher.InvitingMembers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>邀请成员</title>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script>
        $(function () {
            var index = parent.layer.getFrameIndex(window.name);

        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pa">
            <table class="table table-bordered table_list">
                <thead>
                    <tr>
                        <th>姓名</th>
                        <th width="80">操作</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td></td>
                        <td>
                            <a href="##">邀请</a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
