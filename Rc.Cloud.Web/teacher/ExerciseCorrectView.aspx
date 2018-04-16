<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExerciseCorrectView.aspx.cs" Inherits="Rc.Cloud.Web.teacher.ExerciseCorrectView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="renderer" content="webkit" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Styles/style001/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/js001/jquery.min-1.8.2.js"></script>
    <script type="text/javascript" src="../Scripts/plug-in/layer/layer.js"></script>
    <script type="text/javascript" src="../Scripts/plug-in/layer/extend/layer.ext.js"></script>
    <script type="text/javascript" src="../Scripts/function.js"></script>
    <script type="text/javascript" src="../Scripts/json2.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="hw_info">
                <h2>
                    <asp:Literal runat="server" ID="ltlHomeWorkName"></asp:Literal></h2>
                <div class="hw_desc">
                    <span>班级：<asp:Literal runat="server" ID="ltlClassName"></asp:Literal></span>
                </div>
                <a href="javascript:window.opener.loadHomeWorkTQData('<%=HomeWork_Id %>');window.close();" class="close_page"><i class="fa fa-close"></i>关闭页面</a>
            </div>
        </div>

        <div class="container clearfix correct mt10 exercise_correct_view">
            <div class="pl20 pr20 exercise_correct_view">
                <h2 class="column_name">试题详情</h2>
                <div class="qustion">
                    <div class="qustion_tit">
                        <asp:Literal runat="server" ID="ltlTestQuestionsBody"></asp:Literal>
                        (分值：<i class="fen"><asp:Literal runat="server" ID="ltlTQSumScore"></asp:Literal></i>)
                    </div>
                    <div class="answer clearfix">
                        <div class="tit clearfix">
                            <div class="correct"><span>正确答案</span></div>
                            <div class="scorce"><span>分值</span></div>
                        </div>
                        <%=stbTQAnswerScore.ToString() %>
                    </div>
                </div>
                <h2 class="column_name exercise_correct_column_name">答题详情
                    <a href='##' data-name="OnekeyFullMarks" class="create_btn">一键满分</a>
                    <a href='##' data-name="OnekeyZero" class="create_btn">一键0分</a>
                    <a href="javascript:window.location.reload();" class="create_btn">还原</a>
                </h2>
                <div class="student_answer_list">
                    <%=stbStuAnswer.ToString() %>
                </div>
                <div class="submit_btn">
                    <asp:Button runat="server" ID="btnSubmit" CssClass="create_btn" Text="保存并继续批改下题" OnClick="btnSubmit_Click" ClientIDMode="Static" />
                    <asp:HiddenField runat="server" ID="hidCorrect" ClientIDMode="Static" />
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        $(function () {
            //一键满分
            $('[data-name="OnekeyFullMarks"]').live({
                click: function () {
                    $('[data-name="ScoreTxt"]').each(function () {
                        var FullMarks = $(this).attr('data-actual-marks');
                        $(this).val(FullMarks);
                    })
                }
            });
            //一键零分
            $('[data-name="OnekeyZero"]').live({
                click: function () {
                    $('[data-name="ScoreTxt"]').each(function () {
                        $(this).val('0');
                    })
                }
            });

            //下拉分数列表
            $('[data-name="ScoreTxt"]').live({
                mouseover: function () {
                    //触发当前时移除其他
                    RemoveScoreList();

                    var ActualScore = $(this).attr('data-actual-marks');//当前习题实际分数
                    var CurrentScore = $(this).val();//当前分数
                    var StrHtml, Current, i;
                    StrHtml = '<ul data-name="ScoreList">';
                    for (i = 1; i <= ActualScore; i++) {
                        Current = (i == CurrentScore) && 'current';
                        //alert(Current);
                        StrHtml += '<li data-val=' + i + ' class="' + Current + '">' + i + '</li>'
                    }
                    StrHtml += '</ul>';
                    $(this).closest('div').append(StrHtml);
                    i = (i > 4) ? 5 : i;
                    $('[data-name="ScoreList"]').css({
                        'left': (-33 * (i - 1)) + 1 + 'px'
                    })
                }
            });
            //给分数input赋值，并移除当前分数列表
            $('[data-name="ScoreList"] li').live({
                click: function () {
                    var DataVal = $(this).attr('data-val');//当前选择的分数
                    $(this).closest('ul').prev('[data-name="ScoreTxt"]').val(DataVal);
                    //RemoveScoreList();
                }
            });
            //大图预览
            layer.photos({
                shift: 5,
                shade: ['0.3', '#fff'],
                photos: '.answer_score'
            });
            //批注
            $('[data-name="remark"]').live({
                click: popComment,
                mouseover: function () {
                    var content = $(this).attr('data-content');
                    if (content !== '') {
                        layer.tips(content, this, {
                            tips: [1, '#000'],
                            time: 0
                        });
                    }
                },
                mouseout: function () {
                    layer.closeAll('tips');
                }
            });

            $('[data-name="ScoreTxt"]').bind({
                blur: function () {
                    var actualScore = parseFloat($(this).attr("data-actual-marks"));
                    var Score = parseFloat($(this).val());
                    if (!js_validate.IsFloat(Score)) {
                        $(this).val("0");
                    }
                    else if (Score > actualScore) {
                        $(this).val("0");
                        layer.msg('得分不能大于试题分值', { icon: 4 });
                    }
                },
                keyup: function () {
                    var actualScore = parseFloat($(this).attr("data-actual-marks"));
                    var Score = parseFloat($(this).val());
                    if (!js_validate.IsFloat(Score)) {
                        $(this).val("0");
                    }
                    else if (Score > actualScore) {
                        $(this).val("0");
                        layer.msg('得分不能大于试题分值', { icon: 4 });
                    }
                }
            });

            $("#btnSubmit").click(function () {
                var arrAnswer = new Array();
                $('[data-name="ScoreTxt"]').each(function () {
                    var obj = {
                        Student_HomeWorkAnswer_Id: $(this).attr("id"),
                        TestQuestions_Score_ID: $(this).attr("tqsId"),
                        actualscore: $(this).attr("data-actual-marks"),
                        score: $(this).val(),
                        comment: $.trim($(this).closest('div.student_answer_box').find('[data-name="remark"]').attr("data-content"))
                    }
                    arrAnswer.push(obj);
                });
                if (arrAnswer.length == 0) {
                    layer.msg('没有批改试题的数据，请勿提交！', { time: 2000, icon: 2 });
                    return false;
                }
                $("#hidCorrect").val(JSON.stringify(arrAnswer));
                layer.load();
            });

        });
        //移除分数列表
        function RemoveScoreList() {
            $('[data-name="ScoreList"]').remove();
        }
        //批注
        function popComment() {
            var objId = $(this).attr("id");
            var content = $.trim($("#" + objId).attr("data-content"));
            var innerHtml = '';
            innerHtml += '<div class="remark_frame">';
            innerHtml += '<textarea name="" id="txtComment" cols="30" rows="5"';
            innerHtml += ' onblur="if ($(this).val().length > 100) $(this).val($(this).val().slice(0, 100)); "';
            innerHtml += ' onkeyup="if ($(this).val().length > 100) $(this).val($(this).val().slice(0, 100)); "';
            innerHtml += '>' + content + '</textarea>';
            innerHtml += '<div class="btn">';
            innerHtml += "<input type=\"button\" value=\"确定\" class=\"create_btn\" onclick=\"setComment('" + objId + "');\">";
            innerHtml += '<!-- <input type="button" value="取消" class="close_btn"> -->';
            innerHtml += '</div>';
            innerHtml += '</div>';
            layer.open({
                type: 1,
                title: '编辑批注',
                area: ['380px', '255px'],
                content: innerHtml //html内容
            });
        }
        //编辑批注
        function setComment(objId) {
            $("#" + objId).attr("data-content", $.trim($("#txtComment").val()));
            layer.closeAll();
        }
    </script>
</body>
</html>
