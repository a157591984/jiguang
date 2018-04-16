<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="appointAssign.aspx.cs" Inherits="Rc.Cloud.Web.teacher.appointAssign" %>

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
            //选择批改人
            $(document).on('click', '[data-name="selectCorrection"]', function (e) {
                e.preventDefault();
                e.stopPropagation();
                layer.open({
                    type: 2,
                    title: '选择批改人',
                    area: ['350px', '480px'],
                    content: 'selectCorrection.aspx'
                })
            })
            //选择被批改人
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
            //删除批改人
            $(document).on('click', '[data-name="delCorrectionPerson"]', function (e) {
                e.preventDefault;
                e.stopPropagation;
                //移除dmo
                $(this).closest('li').remove();
                //do something

            })
            //删除被批改人
            $(document).on('click', '[data-name="delCorrectedPerson"]', function (e) {
                e.stopPropagation;
                e.preventDefault;
                //移除dom
                $(this).closest('tr').remove();
                //do something
            })
        })
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container container-970 ptb15 assign_correct">
            <%--<ul class="nav nav-tabs">
                <li><a href="randomAssign.aspx">随机分配</a></li>
                <li class="active"><a href="appointAssign.aspx">指定分配</a></li>
            </ul>--%>
            <div class="pb15">
                <button type="button" class="btn btn-default" data-name="selectCorrection">选择批改人</button>
                <button type="button" class="btn btn-default" data-name="selectCorrected">选择被批改人</button>
                <button type="button" class="btn btn-default">随机分配</button>
                <button type="button" class="btn btn-default">平均分配</button>
                <button type="button" class="btn btn-default">开始分配</button>
                <button type="button" class="btn btn-primary">完成分配</button>
                <div class="assign_status pull-right">已分配 20/20</div>
            </div>
            <div class="row">
                <div class="col-xs-3">
                    <ul class="list-group appoint_student_list" id="correctionPerson">
                        <li class="list-item active">
                            <span class="name">学生1</span>
                            <i class="fa fa-trash" data-name="delCorrectionPerson"></i>
                            <input type="text" name="name" value="" class="form-control input-sm student_num" />
                        </li>
                        <li class="list-item">
                            <span class="name">学生1</span>
                            <i class="fa fa-trash" data-name="delCorrectionPerson"></i>
                            <input type="text" name="name" value="" class="form-control input-sm student_num" />
                        </li>
                    </ul>
                </div>
                <div class="col-xs-9">
                    <table class="table table-bordered">
                        <tbody id="correctedPerson">
                            <tr>
                                <td>学生8</td>
                                <td class="opera">
                                    <a href="javascript:;" data-name="delCorrectedPerson">删除</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
