<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectPersonCharge.aspx.cs" Inherits="Rc.Cloud.Web.teacher.SelectPersonCharge" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/common.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../Scripts/jquery-jtemplates.js"></script>
    <script src="../Scripts/function.js"></script>
    <title>选择负责人</title>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            pageIndex = 1;
            $('#btnSearch').click(function () {
                pageIndex = 1;
                loadData();
            })
            $(document).keydown(function (e) {
                if (e.keyCode == 13) {
                    $('#btnSearch').click();
                    return false;
                }
            })
            //筛选
            $('[ajax-name] li').on({
                click: function () {
                    if (!$(this).children("a").hasClass("disabled")) {
                        $(this).closest('ul').find('a').removeClass('active');
                        $(this).children('a').addClass('active');
                        pageIndex = 1;
                        loadData();
                    }
                }
            });
            //全选
            $('[data-name="check-all"]').on({
                click: function () {
                    var Mark = $(this).data('mark');
                    var IsChecked = $(this).is(':checked');
                    var Chackbox = $('input[type="checkbox"][name^="' + Mark + '"]:enabled');
                    if (IsChecked) {
                        Chackbox.prop('checked', true);
                    } else {
                        Chackbox.prop('checked', false);
                    }
                }
            });
            //添加负责人
            $("#btnSelect").click(function () {
                var arrChkVal = new Array();
                $("input[name^='tchid']:checked").each(function () {
                    arrChkVal.push($(this).val());
                });
                if (arrChkVal.length == 0) {
                    layer.msg("请选择老师", { icon: 4 });
                    return false;
                }
                arrChkVal.join(",");
                $("#hidTeacherId").val(arrChkVal);
                var dto = {
                    TeacherList: $("#hidTeacherId").val(),
                    ResourceFolder_Id: "<%=ResourceFolder_Id%>",
                    x: Math.random()
                };
                $.ajaxWebService("SelectPersonCharge.aspx/Empowers", JSON.stringify(dto), function (data) {
                    if (data.d == "1") {
                        layer.msg('添加成功', { icon: 1, time: 2000 }, function () {
                            parent.loadData(); parent.layer.close(index)
                        });
                    }
                    else if (data.d == "2") {
                        layer.msg('添加失败', { icon: 2 });
                        return false;
                    }

                }, function () {
                    layer.msg('添加失败1！', { icon: 2 });
                    return false;
                });
            })

            //移除负责人
            $("#btnRemove").click(function () {
                var arrChkVal = new Array();
                $("input[name^='tchid']:checked").each(function () {
                    arrChkVal.push($(this).val());
                });
                if (arrChkVal.length == 0) {
                    layer.msg("请选择老师", { icon: 4 });
                    return false;
                }
                arrChkVal.join(",");
                $("#hidTeacherId").val(arrChkVal);
                var dto = {
                    TeacherList: $("#hidTeacherId").val(),
                    ResourceFolder_Id: "<%=ResourceFolder_Id%>",
                    x: Math.random()
                };
                $.ajaxWebService("SelectPersonCharge.aspx/DeleteEmpowers", JSON.stringify(dto), function (data) {
                    if (data.d == "1") {
                        layer.msg('移除成功', { icon: 1, time: 2000 }, function () {
                            parent.loadData(); parent.layer.close(index)
                        });
                    }
                    else if (data.d == "2") {
                        layer.msg('移除失败', { icon: 2 });
                        return false;
                    }

                }, function () {
                    layer.msg('移除失败1！', { icon: 2 });
                    return false;
                });
            })
            loadData();
        })
        var loadData = function () {
            var _subjectId = $('[ajax-name="Subject"]').find('a.active').attr('ajax-value');
            var _gradeId = $('[ajax-name="Grade"]').find('a.active').attr('ajax-value');
            var dto = {
                KeyName: $("#txtName").val(),
                SubjectID: _subjectId == undefined ? "" : _subjectId,
                GradeID: _gradeId == undefined ? "" : _gradeId,
                ResourceFolder_Id: "<%=ResourceFolder_Id%>",
                x: Math.random()
            };
            $.ajaxWebService("SelectPersonCharge.aspx/GetList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template_1", null, { filter_data: false });
                    $("#tb1").processTemplate(json);

                }
                else {
                    $("#tb1").html("<tr><td colspan='100' align='center'>暂无数据</td></tr>");
                }
            }, function () { });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pa select-person-charge-hook">
            <div class="filter">
                <div class="form-inline">
                    <div class="form-group">
                        <label>老师名称：</label>
                        <input type="text" id="txtName" class="form-control input-sm">
                    </div>
                    <input type="button" id="btnSearch" value="查询" class="btn btn-primary btn-sm">
                    <input type="button" id="btnSelect" value="选择已选老师" class="btn btn-primary btn-sm">
                    <input type="button" id="btnRemove" value="移除已选老师" class="btn btn-primary btn-sm">
                </div>
                <div class="filter_section">
                    <div class="filter_row clearfix">
                        <span class="row_name">年级：</span>
                        <div class="row_item" data-name="rowItem">
                            <ul ajax-name="Grade">
                                <asp:Literal ID="ltlGrade" runat="server" ClientIDMode="Static"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <div class="filter_row clearfix">
                        <span class="row_name">学科：</span>
                        <div class="row_item">
                            <ul ajax-name="Subject">
                                <asp:Literal ID="ltlSubject" runat="server" ClientIDMode="Static"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th width="15">
                            <input type="checkbox" data-name="check-all" data-mark="tchid">
                        </th>
                        <th>姓名</th>
                        <th>年级</th>
                        <th>学科</th>
                        <th>状态</th>
                    </tr>
                </thead>
                <tbody id="tb1">
                </tbody>
            </table>
            <asp:HiddenField ID="hidTeacherId" runat="server" ClientIDMode="Static" />
        </div>
        <textarea id="template_1" class="hidden">
    {#foreach $T.list as record}
    <tr>
        <td>
         <label><input type='checkbox' name='tchid' value='{$T.record.UserId}'  /></label>
        </td>
        <td>{$T.record.UserName}{#if $T.record.TrueName!=''}({$T.record.TrueName}){#else}{#/if}</td>
        <td>{$T.record.GradeName}</td>
        <td>{$T.record.Subject}</td>
        <td>{#if $T.record.PrpeLesson_Person_Id>0} <span class="text-primary">已选</span>{#else}  <span class="text-muted">未选</span>{#/if}</td>
      
    </tr>
    {#/for}
    </textarea>
    </form>
</body>
</html>
