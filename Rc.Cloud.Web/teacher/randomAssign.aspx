<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="randomAssign.aspx.cs" Inherits="Rc.Cloud.Web.teacher.randomAssign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugin/layer/css/layer.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/layer/js/layer.js"></script>
    <script>
        $(function () {
            $(document).on('click', '[data-name="selectCorrected"]', function (e) {
                e.preventDefault();
                e.stopPropagation();
                layer.open({
                    type: 2,
                    title: '选择被批改人',
                    area: ['350px', '480px'],
                    content: 'selectCorrected.aspx'
                })
            })
        })
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container container-970 ptb15 assign_correct">
            <ul class="nav nav-tabs">
                <li class="active"><a href="randomAssign.aspx">随机分配</a></li>
                <li><a href="appointAssign.aspx">指定分配</a></li>
            </ul>
            <div class="ptb15">
                <button type="button" class="btn btn-default">随机分配</button>
                <button type="button" class="btn btn-primary">完成分配</button>
                <div class="assign_status pull-right">已分配 20/20</div>
            </div>
            <div>
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>批改人</th>
                            <th>被批改人</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>学生1</td>
                            <td>学生8</td>
                            <td class="opera">
                                <a href="javascript:;" data-name="selectCorrected">选择</a>
                                <a href="javascript:;">删除</a>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
