<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="assignCorrect.aspx.cs" Inherits="Rc.Cloud.Web.teacher.assignCorrect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>批改分配给学生</title>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/common.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script src="../Scripts/function.js"></script>

    <script>
        $(function () {
            Correct_Guid_ = "<%=Correct_Guid%>";
            Students = "";//被批改学生
            Students1 = "";//批改学生
            loadData();
            loadDataCount();
            ///完成分配
            $("#btnComplete").click(function () {
                var count1 = $("#CountDistributionStudent").html();
                var count2 = $("#CountStudent").html();
                if (count1 > count2) {
                    layer.msg("被批改的学生中存在已批改的人，请检查。", { icon: 4, time: 2000 });
                    return false;
                }
                else if (count1 < count2) {
                    layer.msg("还有批改人没分配被批改人，请检查。", { icon: 4, time: 2000 });
                    return false;
                }
                else {
                    CompleteAllocation();

                }

            })
            //判断批改人是否大于被批改人
            $(document).on('change', '.select_correction_list input[type=checkbox]', function () {
                var i = (parseInt($("#CountStudent").html()) - parseInt($("#CountDistributionStudent").html()));
                if ($("input[name='test']:checked").length >= i + 1) {
                    $(this).removeAttr("checked");
                    layer.msg("无法继续选择：您选择的批改人数大于被批改人数.", { time: 2000, icon: 2 });
                }
            });
            //选择批改人
            $(document).on('click', '[data-name="selectCorrection"]', function (e) {
                var count1 = $("#CountDistributionStudent").html();
                var count2 = $("#CountStudent").html();
                if (count1 == count2) {
                    layer.msg("操作失败，所有待被批改的学生都已分配完成。", { icon: 4, time: 3000 });
                    return false;
                }
                e.preventDefault();
                e.stopPropagation();
                var _this = $(this);
                var dto = {
                    HomeWork_Id: "<%=HomeWork_Id%>",
                    Correct_Guid: Correct_Guid_,
                    x: Math.random()
                };
                $.ajaxWebService("assignCorrect.aspx/SelectCorrected", JSON.stringify(dto), function (data) {
                    if (data.d != "") {
                        Students1 = data.d;
                    }
                }, function () { }, false)
                layer.open({
                    type: 1,
                    title: '选择批改人',
                    btn: ['确定', '取消'],
                    area: ['350px', '480px'],
                    content: '<div class="select_correction_list">' + Students1 + '</div>',
                    yes: function (index, layero) {
                        //定义dom
                        //strHtml = '';
                        //strHtml += '<tr>';
                        //strHtml += '<td>学生1</td>';
                        //strHtml += '<td><span class="tag_add" data-name="selectCorrected">+</span></td>';
                        //strHtml += '<td>0</td>';
                        //strHtml += '<td class="opera">';
                        //strHtml += '<a href="javascript:;" data-name="delCorrectionPerson">删除</a>';
                        //strHtml += '</td>';
                        //strHtml += '</tr>';
                        ////插入dom
                        //$('#correctList').append(strHtml);
                        ////do something
                        var arrStuId = [];//学生Id
                        $('.select_correction_list input:checkbox:checked').each(function () {
                            var StuId = $(this).val();
                            arrStuId.push(StuId);
                        });
                        layer.msg("正在分配，请耐心等待。", { icon: 16, time: 0, shade: [0.1, '#fff'] });
                        AllocationAvg(arrStuId.join(","));
                        layer.close(index);
                    }
                })
            })
            //选择被批改人
            $(document).on('click', '[data-name="selectCorrected"]', function (e) {
                e.preventDefault();
                e.stopPropagation();
                var _this = $(this);
                var Student_Id = $(this).data('stuid');//批改人
                var dto = {
                    HomeWork_Id: "<%=HomeWork_Id%>",
                    Correct_Guid: Correct_Guid_,
                    StudentId: Student_Id,
                    x: Math.random()
                };
                $.ajaxWebService("assignCorrect.aspx/SelectBeCorrected", JSON.stringify(dto), function (data) {
                    if (data.d != "") {
                        Students = data.d;
                    }
                }, function () { }, false)
                layer.open({
                    type: 1,
                    title: '选择被批改人',
                    btn: ['确定', '取消'],
                    area: ['350px', '480px'],
                    content: '<div class="select_corrected_list">' + Students + '</div>',
                    yes: function (index, layero) {
                        //定义dom
                        var arrShwId = [];//学生作业Id
                        var arrStuId = [];//学生Id
                        var arrId = [];//插入表唯一标识
                        strHtml = "";
                        $('.select_corrected_list input:checkbox:checked').each(function () {
                            var StuId = $(this).val();
                            var Sname = $(this).attr("sname");
                            //push选中数据
                            arrStuId.push(StuId);
                            var id = $(this).attr("tt");
                            arrId.push(id);
                            var ShwId = $(this).attr("shwid");
                            arrShwId.push(ShwId);
                            //拼接字符串
                            strHtml += '<span class="tag" data-name="tag">' + Sname + '<i data-name="delCorrectedPerson" data-value="' + id + '">×</i></span>';
                        });
                        //插入dom
                        _this.before(strHtml);
                        //更新批改人数
                        _this.closest('td').next('td').html(_this.closest('td').find('[data-name="tag"]').length);
                        //do something
                        if (arrStuId.length > 0) {
                            AddBeCorrected(Student_Id, arrStuId.join(","), arrShwId.join(","), arrId.join(","));
                        }
                        layer.close(index);
                    }
                })
            })
            //删除批改人
            $(document).on('click', '[data-name="delCorrectionPerson"]', function (e) {
                e.preventDefault;
                e.stopPropagation;
                $(this).closest('tr').remove();
                //do something
                var id = $(this).data('value');
                DeleteCorrect(id)
            })
            //删除被批改人
            $(document).on('click', '[data-name="delCorrectedPerson"]', function (e) {
                e.preventDefault;
                e.stopPropagation;
                //更新批改人数
                $(this).closest('td').next('td').html($(this).closest('td').find('[data-name="tag"]').length - 1);
                //do something
                var Student_Mutual_CorrectSub_Temp_Id_ = $(this).data('value');
                DeleteBeCorrect(Student_Mutual_CorrectSub_Temp_Id_);
                //移除dom
                $(this).closest('span').remove();
            })
            ///随机分配
            $("#btnRandom").click(function () {
                layer.msg("正在分配，请耐心等待。", { icon: 16, time: 0, shade: [0.1, '#fff'] });
                RandomData();
            })
        })

        ///随机分配
        function RandomData() {
            var dto = {
                Correct_Guid: Correct_Guid_,
                HomeWork_Id: "<%=HomeWork_Id%>",
                x: Math.random()
            };
            $.ajaxWebService("assignCorrect.aspx/RandomData", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    layer.msg("随机分配成功", { icon: 1, time: 1 });
                    loadData();
                    loadDataCount();
                }
                else {
                    layer.ready(function () {
                        layer.msg("随机分配失败", { icon: 4, time: 2000 })
                    });
                }
            }, function () { })
        }
        //加载数据
        function loadData() {
            $.ajaxWebService("assignCorrect.aspx/GetData", "{Correct_Guid:'" + Correct_Guid_ + "',x:" + Math.random() + "}", function (data) {
                if (data.d == "") {
                    layer.msg("异常", { icon: 4, time: 2000 });
                }
                else {
                    $("#correctList").html(data.d);
                }
            }, function () { })
        }
        //加载学生总数和已分配总数
        function loadDataCount() {
            var dto = {
                HomeWork_Id: "<%=HomeWork_Id%>",
            Correct_Guid: Correct_Guid_,
            x: Math.random()
        };
        $.ajaxWebService("assignCorrect.aspx/GetProgressData", JSON.stringify(dto), function (data) {
            var json = $.parseJSON(data.d);
            if (json.err = "null") {
                $("#CountDistributionStudent").html(json.CountDistributionStudent);
                $("#CountStudent").html(json.CountStudent);
            }
            else {
                layer.msg("加载失败", { icon: 4, time: 2000 });
            }
        }, function () { })
    }
    //删除被批改人
    function DeleteBeCorrect(Student_Mutual_CorrectSub_Temp_Id) {
        var dto = {
            Student_Mutual_CorrectSub_Temp_Id: Student_Mutual_CorrectSub_Temp_Id,
            x: Math.random()
        };
        $.ajaxWebService("assignCorrect.aspx/DeleteBeCorrect", JSON.stringify(dto), function (data) {
            if (data.d == "1") {
                loadDataCount();
            }
            else {
                layer.msg("异常", { icon: 4, time: 2000 });
            }
        }, function () { })
    }
    //增加被批改人
    function AddBeCorrected(StudentId, arrStuId, arrShwId, arrId) {
        var dto = {
            HomeWork_Id: "<%=HomeWork_Id%>",
                StudentId: StudentId,
                Correct_Guid: Correct_Guid_,
                ArrStudenId: arrStuId,
                ArrShwId: arrShwId,
                ArrId: arrId,
                x: Math.random()
            };
            $.ajaxWebService("assignCorrect.aspx/AddBeCorrected", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    loadDataCount();
                }
                else {
                    layer.msg("异常", { icon: 4, time: 2000 });
                }
            }, function () { })
        }
        //删除批改人
        function DeleteCorrect(Student_Mutual_Correct_Temp_Id) {
            var dto = {
                Student_Mutual_Correct_Temp_Id: Student_Mutual_Correct_Temp_Id,
                x: Math.random()
            };
            $.ajaxWebService("assignCorrect.aspx/DeleteCorrect", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    loadDataCount();
                }
                else {
                    layer.msg("异常", { icon: 4, time: 2000 });
                }
            }, function () { })
        }
        ///完成分配
        function CompleteAllocation() {
            //验证是否有被批改人
            var bool = false;
            $('[data-name="correctedPersonTd"]').each(function () {
                var correctedPersonLen = $(this).find('[data-name="tag"]').length;
                if (correctedPersonLen == 0) {
                    bool = true;
                }
            })

            if (bool) {
                layer.msg('还有批改人没分配被批改人!', { icon: 4, time: 2000 });
                bool = false;
                return false;
            }

            var dto = {
                HomeWork_Id: "<%=HomeWork_Id%>",
                    Correct_Guid: Correct_Guid_,
                    x: Math.random()
                };
                $.ajaxWebService("assignCorrect.aspx/CompleteAllocation", JSON.stringify(dto), function (data) {
                    if (data.d == "1") {
                        layer.msg("分配成功", { icon: 1, time: 2000 }, function () {
                            window.opener = null;
                            //window.opener=top;    
                            window.open("", "_self");
                            window.close();
                        });
                    }
                    else {
                        layer.msg("异常", { icon: 4, time: 2000 });
                    }
                }, function () { })
            }
            ///平均分配
            function AllocationAvg(arrStuId) {
                var dto = {
                    HomeWork_Id: "<%=HomeWork_Id%>",
                Correct_Guid: Correct_Guid_,
                arrStuId: arrStuId,
                x: Math.random()
            };
            $.ajaxWebService("assignCorrect.aspx/AllocationAvg", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    loadData();
                    loadDataCount();
                    layer.closeAll();
                }
                else if (data.d == "2") {
                    layer.msg("分配错误:被批改的学生数大于当然分配批改的人数。", { icon: 4, time: 2000 });
                }
                else if (data.d == "3") {
                    layer.msg("分配错误:被批改的学生与批改学生不能是同一个人", { icon: 4, time: 2000 });
                }
                else {
                    layer.msg("异常", { icon: 4, time: 2000 });
                }
            }, function () { })
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container ph assign_correct">
            <div class="pb">
                <button type="button" id="btnRandom" class="btn btn-default">一键随机分配</button>
                <button type="button" id="btnComplete" class="btn btn-primary">完成分配</button>
                <div class="assign_status pull-right">已分配 <span id="CountDistributionStudent"></span>/<span id="CountStudent"></span></div>
            </div>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>
                            <button type="button" class="btn btn-default btn-sm" data-name="selectCorrection">选择批改人</button></th>
                        <th>被批改人</th>
                        <th>批改人数</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody id="correctList"></tbody>
            </table>
        </div>
    </form>
</body>
</html>
