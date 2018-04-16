<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectCorrection.aspx.cs" Inherits="Rc.Cloud.Web.teacher.selectCorrection" %>

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
            var index = parent.layer.getFrameIndex(window.name);
            //关闭弹窗
            $('[data-name="close"]').on('click', function () {
                parent.layer.close(index);
            });

            //确定
            $('[data-name="save"]').on('click', function () {
                //do something

                //定义dom
                strHtml = '';
                strHtml += '<tr>';
                strHtml += '<td>学生1</td>';
                strHtml += '<td><span class="tag_add" data-name="selectCorrected">+</span></td>';
                strHtml += '<td>0</td>';
                strHtml += '<td class="opera">';
                strHtml += '<a href="javascript:;" data-name="delCorrectionPerson">删除</a>';
                strHtml += '</td>';
                strHtml += '</tr>';
                //向dom中插入数据
                parent.$('#correctList').append(strHtml);
                //关闭弹窗
                parent.layer.close(index);
            })

        })
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="assign_correct">
            <div class="form-group p15 mn fiexd_top">
                <div class="input-group">
                    <input type="text" value="" class="form-control" id="" />
                    <div class="input-group-btn">
                        <button type="button" class="btn btn-default"><i class="fa fa-search"></i></button>
                    </div>
                </div>
            </div>
            <div class="student_list">
                <ul class="list-group">
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                    <li class="list-item">
                        <label>
                            学生1
                            <input type="checkbox" name="name" value="" /></label></li>
                </ul>
            </div>
            <div class="text-right p15 fiexd_bottom">
                <button type="button" class="btn btn-primary" data-name="save">确定</button>
                <button type="button" class="btn btn-default" data-name="close">取消</button>
            </div>
        </div>
    </form>
</body>
</html>
