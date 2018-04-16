<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaperDetail.aspx.cs" Inherits="Rc.Cloud.Web.teacher.PaperDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../plugin/layer/css/layer.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/layer/js/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            //布置作业
            $('[data-name="assignment"]').on('click', function () {
                layer.open({
                    type: 2,
                    title: '布置作业',
                    area: ['415px', '600px'],
                    content: 'AssignExercise.aspx'
                });
            });
            //开始讲评
            $('[data-name="startCommenting"]').on('click', function () {
                layer.open({
                    type: 2,
                    title: '选择讲评数据',
                    area: ['550px', '500px'],
                    content: 'StartCommenting.aspx'
                });
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="elevator">
            <div class="item" data-name="assignment">布置作业</div>
            <div class="item" data-name="startCommenting">开始讲评</div>
        </div>
        <div class="container paper_preview_container">
            <h4 class="p_t">
                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 19px; vertical-align: middle;">上海市中等职业学校公共基础课学业水平考试</span>
                </p>
                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 19px; vertical-align: middle;">数学　模拟测试卷（一）</span>
                </p>
                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,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&#10;AAAASUVORK5CYII=" style="vertical-align: 0px" width="529px" height="396px">
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                </p>
            </h4>
            <h4 class="p_t">
                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">第</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">卷</span>
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">一、选择题（本大题满分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">66</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">.</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">共</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">22</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">题，每题</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，有且只有一个选项是正确的）</span>
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                </p>
            </h4>
            <a name="3"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">1. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">下列对象能构成集合的是</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">某班成绩优异的同学</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">中国著名的河流</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">绝对值很大的数</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">中国的四大发明</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">D</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="4"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">2.　</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">已知角</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">α</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">的终边经过点</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">P</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">（－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">），则</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">tan</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">α</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. 2	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAApCAYAAAA8hqkEAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAANZJREFU&#10;SEvtVe0NQyEItJs5E+uwCXMwhUtQP2pqn6JGY/KaPBL/ycnBHb7Eh9mJALATZic5Vn8QwAmBEQMk&#10;rvOKUgELGp8czhpAetIRPAD/3wPGZR0UQoqCAiFFjie9MGezG1VgrU3umzjhbo4bUZjreX3rFIVf&#10;JSLr9TUqCMsUJefEteYno4HUAEwX3X+2s4Iw1YPoynWAVME8hWu/HAl0/oYhBUZ9Fwz/xjCB3gj7&#10;AIwC5RryVKihhzYFn1y78quNsk0VQBZOBbA+xr7NhlMYufQBEHkDoThqNGK6PkIAAAAASUVORK5C&#10;YII=" style="vertical-align: -20px" width="16px" height="41px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAApCAYAAAA8hqkEAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAANZJREFU&#10;SEvtVe0NQyEItJs5E+uwCXMwhUtQP2pqn6JGY/KaPBL/ycnBHb7Eh9mJALATZic5Vn8QwAmBEQMk&#10;rvOKUgELGp8czhpAetIRPAD/3wPGZR0UQoqCAiFFjie9MGezG1VgrU3umzjhbo4bUZjreX3rFIVf&#10;JSLr9TUqCMsUJefEteYno4HUAEwX3X+2s4Iw1YPoynWAVME8hWu/HAl0/oYhBUZ9Fwz/xjCB3gj7&#10;AIwC5RryVKihhzYFn1y78quNsk0VQBZOBbA+xr7NhlMYufQBEHkDoThqNGK6PkIAAAAASUVORK5C&#10;YII=" style="vertical-align: -20px" width="16px" height="41px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">D</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="5"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">3. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">下列函数为偶函数的是</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=3</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">+1	</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=sin</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAAMCAYAAACulacQAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAF5JREFU&#10;KFO1jt0NwCAIhOnKrHObMIdTuMQVRKqm6WNJNMbvfrjoI18TkGyESIio1vMrTHF1M8clwnxP+Egj&#10;A8oyD+cBPaWCT9hAZP7qzDpQdrIW0rFpndK8Ovf+n+ANOcUdmd+XtxsAAAAASUVORK5CYII=" style="vertical-align: 5px" width="7px" height="12px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">5</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">D</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="6"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">4. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">篮球现在是多数年轻人的最喜欢的运动项目之一，某种品牌的篮球如图所示，则与篮球相类似的几何体为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAF0AAAByCAYAAAAxi90gAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAJa5JREFU&#10;eF7t3WWU3cjVhWF/v8LMzMzMzMzMzDRhZmYGh5kZHWZmh5kTh5mjrx/Zb0/5Trfd7WmPJ1nTa2np&#10;SldXqtq1a9epc06p/29a+tt0yN9BiwDQD/k7aBHYdNA+7pCnzcpycIfhn//85/TXv/51+tOf/jT9&#10;8Y9/nPe2P/zhD/Pxn//85+kvf/nL9I9//GP6z3/+c3Cvzly+gxXoQAPgtm3bpm9/+9vT5z//+elj&#10;H/vY9IEPfGB673vfO23ZsmWn7T3vec9ke9/73jd95CMfmT772c9OX/3qV6cf//jH0+9+97vpX//6&#10;18GyEfYp6EDG0F/84hfTl770penDH/7w9O53v3t685vfPL385S+fXvziF0/Pe97zpmc961nTM5/5&#10;zOlpT3vavD3jGc+YnvrUp87nnv70p8/Hrtm8efP0whe+cHrNa14zvf3tb5/e//73T5/73OemH/zg&#10;B3Nj/vvf/z5YNMI+AV3lf/Ob30xf+MIXpg996EPTW97ylumVr3zl9KIXvWgG+bnPfe707Gc/eyew&#10;n/SkJ01PfOITJ3vb4x//+Pn4CU94wrzXCBrEXkM85znPmV7wghdML3vZy6Y3vOENc4/49Kc/Pf3w&#10;hz/c51J0kIKO1br+Jz/5yZnNr33ta2dQMBrQmIq9WIu9wHzyk588gwzcxz72sdPjHve4eQ/0xzzm&#10;MfPeVqPUAH4PeI33/Oc/f36G3qMB9KbvfOc785iwL8aBvQ46VhvsMOyDH/zg9OpXv3pm9Ute8pIZ&#10;ZMwGNJCSD3tg2wMT0LZHP/rRM9CPfOQjp0c96lHLm3O+67qxsWK+Z2hY8gN821vf+tbpi1/84vSr&#10;X/3qIGX/XgX973//+/SjH/1oHuSwWkVJiMrbAJ1WAyfZSEIACdxYHdAPe9jDpoc//OHLjdB1Y2No&#10;AL3D5n6NAZ5ZYyvLS1/60ulNb3rTPGgH/t4W/r0COqtBBQyOr3/962ewMUw3T6+TEYAkIWk0wAAM&#10;RBsmP+IRj5g3YNs/5CEPmQLfufFz15IhDWIf+DVA8kP39TplfOc73zl9/etfn83RvSk7Gw46K+G7&#10;3/3uPHC94hWvmLVUxdJXkmGgsz3lKU9Z3gCePAAt1gLU54AF7kMf+tDlDfg23zvv+64P/HqIxqsB&#10;MN9m/EAGZcR80vfRj350+ulPfzpLzt742zDQMYNtrJsaJA2Q6XaAYxdW24COdRohGQBIcgLE2BzI&#10;I8APfOADpwc96EHTgx/84OV9n2uAsQfUYzSK5zTwerbGN+CSHWVW9ne9610z641HG836DQHdYElO&#10;TGRe9apXzewmJyqRnAA5dvUZ+LE7KcBOwAM6kAPX8f3vf/+Zzfe73/0mwD/gAQ+Y9wC3t9VIY+8Y&#10;e0DM9+zMUMCTPGXG+iwddv7vf//7DQX+QIMO8J/97GfLcgLs9BvD025AY7VzsXu0SgCRZgMNiHe/&#10;+92nm9/85tO1r33t6apXvep0pStdabrUpS41XfSiF50udKELTRe84AXnzeeLXexi0+Uud7npale7&#10;2nS9611vut3tbjfd+973nu8D/MYC4LclNxrclq0PeJKD9cakj3/843Mv3ijGHyjQFYLdbfbX5CZ2&#10;29dlYzY2qRiGs0gA3aB5z3vec7rZzW42A3fuc597Ov3pTz+d4QxnmM585jPPxxe4wAWmy172stNV&#10;rnKVuQFsV77yladrXvOa8/4yl7nM3ABnO9vZ5t+e9rSnnTefndNY173udad73OMec28gN40dypC9&#10;TwKVN/se8Hov2/63v/3thgC/x6Bj+E9+8pPpda973SwnBqEGJJKC0U3PVSANz7zD5tvc5jYziBe+&#10;8IWn8573vNOlL33p6QY3uMF0l7vcZQbnvve972x9tGWPBxbgup/9aN2Qnbvd7W7TbW9725n57n2O&#10;c5xjboSzn/3s0yUucYnppje96U69YbR0kCOpaWL1jne8Y55JH1h3wh6BziQkKW984xvnLsjWBTpZ&#10;AXaTkNFiwSTarNvf+MY3ntl5jWtcYwb+zne+88y+unv7TMZYCdTReiEdjgPbMd3vurFRki8NCuwr&#10;XOEK0/nPf/5ZmsgWGTNOIIMe2VyBPCITjcd4jrdf//rXBwr4dYMe4GxaQAe2wQfY2eJMsbQb2He6&#10;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&#10;jzyE6fsA9P8HcQUlzdsttWQAAAAASUVORK5CYII=" style="vertical-align: 0px" width="93px" height="114px">
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">圆锥</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">圆柱</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">球</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">棱锥</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">C</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="7"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">5. i</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">是虚数单位，复数</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">(1+i</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAAMCAYAAACulacQAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAF1JREFU&#10;KFOtjtEJwEAIQ+3KruMmzuEULpFqbXt60L8GBOHFxAMh+hJgEKI0gNXT+4pcNXAqTXLvxak7TRj9&#10;eMJI6cELmkAqf3VWnYB2kp2ufH36TPeMzhm6ffsfPAHUth2ZGB9YJQAAAABJRU5ErkJggg==" style="vertical-align: 4px" width="7px" height="12px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">的值为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. 0	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. 1+i</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. 1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">i	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. 2i</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">D</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="8"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">6. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">已知角</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">β</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=200°</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，那么</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">β</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">是</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">第一象限角</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">第二象限角</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">第三象限角</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">第四象限角</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">C</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="9"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">7. 2015</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">年</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">月</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">9</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">日上海地区气温</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">℃的变化范围可以用</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADUAAAAWCAYAAABg3tToAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAUdJREFU&#10;WEftVYkNwyAMDCuzjjdhDqZgCWoXzJfQJKpRQQ1SBQkNvvPZh/I4NhxKqS0u6XG5UeEnUpEYL5ec&#10;UYWEO63KlyuyekitotpESlkP2AsEiH5g2xSe7ef/T0LKeaPBMw9ndEOMCLX7+bmlPwcpa7xxJTQi&#10;iYqxXM75ahvpw6abb6ZTat+tFgpS7bYzXu/rM9v4ZUu3EOu9yBC/06bJ5LeWEpQ6xE2ETuLdLr93&#10;vWM0nr+Ff/h9Bzj3WjATyZ6igHiorptAlJuFfr9QICbXw3BbKTyyUxq15bI19+fjTBPgD+0Skxcw&#10;iJFyBjxgJpMzSWqEPVoBxaowu/sqBCQj6ZG/pxQHLWqeSIpUYjKifAF3++bELC6RSg2aUhPvEaHe&#10;qg2gIMXxWsLS7idZbaPOuqTUqOCjzn1Ijcqs9Ln/o5R05n553gvuWyOFjq1+xQAAAABJRU5ErkJg&#10;gg==" style="vertical-align: -6px" width="52px" height="22px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">≤</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">7</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">表示，那么下面气温中可能是当天测得的是</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. 7</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">℃</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. 31</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">℃</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. 18</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">℃</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. 0</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">℃</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">C</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="10"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">8. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">下列直线中倾斜角为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">45°</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">的是</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=1	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=1</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">A</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="11"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">9. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">某公司组织员工参加献血活动，并分配到</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">A</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">部门</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">4</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">个名额，分配到</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">B</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">部门</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">个名额，已知该公司</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">A</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">部门有</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">11</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">人报名参与，</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">B</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">部门有</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">10</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">人报名参与，则不同选法的种数是</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. C</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAkAAAAXCAYAAADZTWX7AAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAHZJREFU&#10;OE/NUtsNwCAIpCu7DpswB1N0iSuKWtsY6l8lMfH0wh2PAxb0FZlU4hQk1g7HCzlQMBEoIikzNMyk&#10;jJIgIkkymSzVzkSyeloyvkqaFu+Pt9xGJO+TddwHiCfuxvNHIzXiiEt1+5GU6wYkMXe2fi/8w1gu&#10;Gabauu7VvMgAAAAASUVORK5CYII=" style="vertical-align: -6px" width="10px" height="24px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">C</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAsAAAAXCAYAAADduLXGAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAKRJREFU&#10;OE/Nk10SgCAIhOnKXIebcA5O4SUIUsl+sGZ6iadG1/UL1kWt4G25WAsrAvghRS7b0l1BFRetElFC&#10;bt9XeRX3shtYUmMNsVDFAMrVR2cDYCSDmTEPe8IPzIFgGBOKnTn/rX3nxDw/8h+x99YHcuxvYbxk&#10;pTGfhuHBaj0UQu3ZuhW7a/RbKJKYiCncPL7UrL87b48hY45sDMHva+PL+ckEV7DChFykLoUoAAAA&#10;AElFTkSuQmCC" style="vertical-align: -6px" width="11px" height="24px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">　</span><span style="color: #000000; font-size: 16px; vertical-align: middle;"></span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. P</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAXCAYAAAAyet74AAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAH5JREFU&#10;OE/Vkt0NwCAIhOnKrsMmzsEULnFFK/7FaPvQNCXxAfzCHegBDboTESwRPBxLV7KEalXARKAdKMyQ&#10;bUdhpEZrMMA7lYyydibyjcdtx8dTT7dSir30gv0WtD3qyySPY34Zzx7jpYEGt/mPQOH8c5zXmYEx&#10;b6Zev/M74AnNLDJHi/1HrQAAAABJRU5ErkJggg==" style="vertical-align: -6px" width="10px" height="24px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">P</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAsAAAAXCAYAAADduLXGAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAJ5JREFU&#10;OE/Vk9ERwzAIQ+nKrMMmzMEULKFC7BC3jZ3c9afly+fIyjPID0TR3YIrmCgPgNXz7LQI7mgSg7D2&#10;9bmeajv+oLY0xiY2aRgka/XhHADKEjAr5uGb6QVzIQTGBUVjvlt/Ks7+5lBee+zKH3npF3wbSIar&#10;99GEsefrVJyu1XOTSuNELOWWEZZu/b3z9iBmzJWPIfz73vh6fmTcT0nihFzid95lAAAAAElFTkSu&#10;QmCC" style="vertical-align: -6px" width="11px" height="24px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">　</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. C</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAXCAYAAAAyet74AAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAH5JREFU&#10;OE/Vkt0NwCAIhOnKrsMmzsEULnFFK/7FaPvQNCXxAfzCHegBDboTESwRPBxLV7KEalXARKAdKMyQ&#10;bUdhpEZrMMA7lYyydibyjcdtx8dTT7dSir30gv0WtD3qyySPY34Zzx7jpYEGt/mPQOH8c5zXmYEx&#10;b6Zev/M74AnNLDJHi/1HrQAAAABJRU5ErkJggg==" style="vertical-align: -6px" width="10px" height="24px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">P</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAsAAAAXCAYAAADduLXGAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAJ5JREFU&#10;OE/Vk9ERwzAIQ+nKrMMmzMEULKFC7BC3jZ3c9afly+fIyjPID0TR3YIrmCgPgNXz7LQI7mgSg7D2&#10;9bmeajv+oLY0xiY2aRgka/XhHADKEjAr5uGb6QVzIQTGBUVjvlt/Ks7+5lBee+zKH3npF3wbSIar&#10;99GEsefrVJyu1XOTSuNELOWWEZZu/b3z9iBmzJWPIfz73vh6fmTcT0nihFzid95lAAAAAElFTkSu&#10;QmCC" style="vertical-align: -6px" width="11px" height="24px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">　</span><span style="color: #000000; font-size: 16px; vertical-align: middle;"></span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. P</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAXCAYAAAAyet74AAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAH5JREFU&#10;OE/Vkt0NwCAIhOnKrsMmzsEULnFFK/7FaPvQNCXxAfzCHegBDboTESwRPBxLV7KEalXARKAdKMyQ&#10;bUdhpEZrMMA7lYyydibyjcdtx8dTT7dSir30gv0WtD3qyySPY34Zzx7jpYEGt/mPQOH8c5zXmYEx&#10;b6Zev/M74AnNLDJHi/1HrQAAAABJRU5ErkJggg==" style="vertical-align: -6px" width="10px" height="24px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">C</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAsAAAAXCAYAAADduLXGAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAKRJREFU&#10;OE/Nk10SgCAIhOnKXIebcA5O4SUIUsl+sGZ6iadG1/UL1kWt4G25WAsrAvghRS7b0l1BFRetElFC&#10;bt9XeRX3shtYUmMNsVDFAMrVR2cDYCSDmTEPe8IPzIFgGBOKnTn/rX3nxDw/8h+x99YHcuxvYbxk&#10;pTGfhuHBaj0UQu3ZuhW7a/RbKJKYiCncPL7UrL87b48hY45sDMHva+PL+ckEV7DChFykLoUoAAAA&#10;AElFTkSuQmCC" style="vertical-align: -6px" width="11px" height="24px">
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">A</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="12"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">10. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">如图所示，小王从</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">O</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">点出发，沿着一块扇形绿地</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">OAB</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">的边缘匀速散步一周，设小王的运动时间为</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">t</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">，小王到</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">O</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">点的距离为</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">s</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">，则</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">s</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">关于</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">t</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">的函数图像大致为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,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&#10;AAAASUVORK5CYII=" style="vertical-align: 0px" width="134px" height="97px">
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,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" style="vertical-align: 0px" width="138px" height="122px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,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&#10;RK5CYII=" style="vertical-align: 0px" width="129px" height="124px">
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,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&#10;zM7O/gGNBWhOfSmJIAAAAABJRU5ErkJggg==" style="vertical-align: 0px" width="137px" height="113px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,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" style="vertical-align: 0px" width="142px" height="117px">
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">B</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="13"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">11.　</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">已知函数</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABUAAAAUCAYAAABiS3YzAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAS5JREFU&#10;OE+tlLENhDAMRXMDskxqRmACMgA9ZSRqWopbgBIqNvDlGxxBSMKBcINE4mf7286HnKm3DdC3TYXA&#10;aZqo67rLOG3b0rIs0XsHKIB1XV8C5UJVVQSf0DwUUYuiSEaPRUr5eCii3slSgsAHvntjKEpwA0DD&#10;MPxdulzs+5599zIwFKLjYBzH21D4wBcMMYaWZckHB/sa/qeUJjtvJ/JPW5Jf25wz4wBdnU/TxXdm&#10;q0mZr//GSgn9mZSDOhppF1D7dM/Y+1BXqNWKXLJJi0Kjmm6I2RoyZpUgZYCeNE123zWGy4YEW3MQ&#10;ZK9Esvsyp5g53xw0z2e3SoCMQm0x29E5BQib8WSjmqaJb5Ro9fruAwwZwj3OrRiyzL5S4oyXZ79y&#10;KSje3BiQ5z6XydOzH1Avm+mwcrtbAAAAAElFTkSuQmCC" style="vertical-align: -4px" width="21px" height="20px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">的图像是如图所示的折线段，那么该函数的定义域可能是</span><span style="color: #000000; font-size: 16px; vertical-align: middle;"> 	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAC8AAAAWCAYAAABQUsXJAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAS1JREFU&#10;WEfVltENwyAMROnKrMMmnoMpWMK1SUiAknC0qApI+WiU2s/HYfNiWWbVpfCrLrMqeHTM8vBiefV9&#10;8TylqBabvjuUTz+ugAPZozDnZ5fl2WXCIfFxeO8E3PHGvCVCEmAlBiabYjMnkXrxQXgNXsFqMZY4&#10;YHT3X3liKgJt+UyHHoMPxNbYMkHr3YxC9hjezYIvLJMIZ1snr7yx0w1hMOX/Da+7Cljye/hom5mH&#10;9pTWu8qiF3bE4P/oee00vS6TasHgpafU3Sa2M2Brh86w2NPmbUdEo5t5AsJra89b48Vh3a10zoMB&#10;9Hiu6gl/9v5WJBxe/t2fsGlKYp5NQHncooApfX5AQCmRyU0aXp28H8r/djFT5cdUR3XpXszQQE/7&#10;bv37/NMURXmWVv4NnT5GNsYK9tMAAAAASUVORK5CYII=" style="vertical-align: -6px" width="47px" height="22px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAD8AAAAWCAYAAAB3/EQhAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAATZJREFU&#10;WEftl+0RgyAMhu3KrJNNmIMpWCJN5Kh8WZOWoiflzh96GnjeJC/yQBrLrIPhZx3LrOBrxU8PTy3P&#10;fZ9ddxGlxcbPXpmPN78DdgiJuOB6zqSPPRDeozWAkddbs1ZYHwEYvIy93e9JPA7eWbQ+XQaLQS3W&#10;g957zEKTxLCYYr5agnHwDfkddIIvY3uLRiDqifAh84I16oyBwY0tKqEd4jx4xSKl9NFHgrNfqecL&#10;AgfHPSmFrqs+mKnJTaYKp8x8vp3s7Z3b87b6nKHu5Z6hhZbqDP9pLpLvHOSLovK3Xff7MBeb6ZHA&#10;ysx/CU/gdbUU1cFeIOzZ3dUI/WQYfG5GyS90lZ7YWgpPKEW9vNu/LSLqWZBtV9/UYpX58w82sr8z&#10;LfThwUYb8C7v/8/zd8mklmPqzD8BgbJsUkpC6iUAAAAASUVORK5CYII=" style="vertical-align: -6px" width="62px" height="22px">
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAD8AAAAWCAYAAAB3/EQhAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAATtJREFU&#10;WEftV+sRgyAMtiuzTjZhDqZgCRpQlCCacLX4QO76o+hFvkcefByuodflwfe6hl6BB8d3Dx5T3uc9&#10;+T2FlBI2vzcrH//8D7BxkJAL5tgvWa1m4SSxG4K3TitwEW88qOSQIooMIPAYfySZi90OvNFO2xSG&#10;JwNTjDuhCPkYi4TyZCjtyCezWO3AF0AYOAi81U4NipJb2rsO+IJaIpWLLCaWj89565+nvFeGsaWY&#10;C5LvNwBvILOpGKlQ+WD7/aJXqTxtVVu9c9lfqjspddiSDqlzMehtch4tqtKyjwfXP/f7df0IrfRS&#10;1T7kZj5FZu6Y7Lr0bGE+kNbGF7vpFttmwkunL0LAyv8xteprwoUnPKGC4TW0MewPKDXRtt5dFbzz&#10;LzZe+XrVOTLYiw0X4KnP3/v8U5XlcHWt/BctlV5FlgMrRAAAAABJRU5ErkJggg==" style="vertical-align: -6px" width="63px" height="22px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAC4AAAAWCAYAAAC/kK73AAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAASFJREFU&#10;WEfVltERhCAMRL2WaYdOqIMqaCJHoigokj1lTmHGDx1cXsIm8KE4phEHg484phGhxSVDg0eLs8+L&#10;5w0B1bj425rx9HIGG5xZg7K+d0iebJY0TR8H9zZCW5p550U0cTw01kvaRHOCtveaDggeyJkdKAdi&#10;HAWc7nxmCDsdDsSQa4hj4MGR2QvVvvUIgjVYW9lODLywSaLrbZdFl6GBnXwVeF78fTxey7hYpWeB&#10;bj5LAZiGybGM/9vjsVS5GdwHX4TyepGsAF68Wq/etncTy7i07rz9nRTmYh/Nn2owQIHi4NKltJMz&#10;nX7tHnwAlxrKrhrATv4ErmZKJkR/2k4HU2PBA/i9S5Z+4mHBl7PUS9YV0af/Gfs+/nT2rqw/bMa/&#10;QSP9vQJGBI4AAAAASUVORK5CYII=" style="vertical-align: -6px" width="46px" height="22px">
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">B</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="14"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">12. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">某学校会计专业开设有</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">8</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">门选修课程，每位学生必须任选其中</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">5</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">门课程进行学习，则该会计专业学生小赵可选</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">5</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">门不同选修课的种数为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. 28	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. 56</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. 168	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. 336</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">B</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="15"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">13. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">不等式</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">(</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1)(</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3)&lt;0</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">的解集为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. {1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3}	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. {</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">|1&lt;</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">&lt;3}</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. {</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">|</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">&lt;1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">或</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">&gt;3}	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. 1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">或</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">B</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="16"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">14. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">向量</span><i><b><span style="color: #000000; font-size: 16px; vertical-align: middle;">a</span></b></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=(</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，向量</span><i><b><span style="color: #000000; font-size: 16px; vertical-align: middle;">b</span></b></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=(3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">5)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，则</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><i><b><span style="color: #000000; font-size: 16px; vertical-align: middle;">a</span></b></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><i><b><span style="color: #000000; font-size: 16px; vertical-align: middle;">b</span></b></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">13	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">30</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. (</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">11</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">19)	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">11</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">或－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">19</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">C</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="17"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">15. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">已知复数</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">z</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=5</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">i(i</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">是虚数单位</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，那么</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">z</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">的共轭复数</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAwAAAAfCAYAAADTNyzSAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAHVJREFU&#10;OE/dlMENwCAIRenKrMMmzsEULEHVSytgW24NJJ78P/EB30N7QaaGIVOQEc/XlDIwjfaaQ8oGshb0&#10;xwkWgxZtaCeN2mTtxgUtrHy7ZAIluxe7POzEYYCexM7wJl4MTsydKZj+hJaGPgsRcblP4Dd5OAGS&#10;/mfcP82zFwAAAABJRU5ErkJggg==" style="vertical-align: -6px" width="12px" height="32px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">=　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. 5+i	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">5</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">i</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">5+i	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1+5i</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">A</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="18"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">16. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">已知集合</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">A</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">={</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">水果</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">}</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">B</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">={</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">苹果，梨子，葡萄</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">}</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，集合</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">C</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">={</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">茄子，南瓜，冬瓜</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">}</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，则下列关系正确的是</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">C</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">⊊</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">A</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">B</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">⊊</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">A</span></i>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">A</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">⊊</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">B</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">A</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">⊊</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">C</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;"> </span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">B</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="19"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">17. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">已知</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">(</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">的函数图像如图所示，那么下列关系正确的是</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,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" style="vertical-align: 0px" width="111px" height="118px">
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">(1)&lt; </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">(2)&lt; </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">(3)	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">(1)&lt; </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">(3)&lt; </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">(2)</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">(3)&lt; </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">(2)&lt; </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">(1)	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">(2)&lt; </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">(1)&lt; </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">(3)</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">A</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="20"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">18. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">过点（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">），倾斜角为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">45°</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">的直线方程为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">+</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">5=0	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">+</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">+1=0</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">5=0	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">+1=0</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">D</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="21"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">19. sin330°</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">等于</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAaCAYAAACtv5zzAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAN5JREFU&#10;SEvdlu0NxCAIhr2VWcdNnMMpWIKT3lk/6iloTZMj6Y82lQfel9C+KITZGQzYGUaTPDTK3YqumFcN&#10;0BR0yK85wNVrQ3xiJrmqg38EeLLZRFnfdkfkwVUeTm4p5kQHYXTTfY6aAyASFgUzEMiVD4835gC1&#10;GugIfmi0DuDk4KqOUgVDQG88P9rH1THpgWT+IwgaJix1kIRAcmBoI4DIW0Mtn4sOop7nqpUut47R&#10;JyDf80OAt+U3QTNFeRcSg0fr+2LydsD3J+CQ4I5oZqnNXgE9A1ipuD57j9Cdit4DQqKea+VXlQAA&#10;AABJRU5ErkJggg==" style="vertical-align: -6px" width="24px" height="27px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB8AAAAvCAYAAAAb1BGUAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAXFJREFU&#10;WEftWNERwiAMxc06E+uwCXMwBUtEYsUClvYB6aGn3Pmhh3nJS5q89EbhqFmHwWcd1QscyGLGoE8N&#10;Ywi81/H4v+8DZ7olTpeVP/hGvSOTVL5xWFKaaX+nnIENRTxvdXj8tu9HboyDe08+Q2BnNNn8x10f&#10;xsFLs96SBnmXBWdgbQsm6sQ3gR89YmuuY7u9IOfI8x2d0EDSxSLfyPVktaJJ4ETOKEJqrhp5zN9r&#10;AqH9vKHodsHTOX0K7kw+06WqPY0eKTasqW63DgtuKvhTXD5oveKcWi0LT9KJzwaXjLS09Yp8WRZI&#10;BqNyuXaPcYbVqwQjpzmXABFfGiSc+rbI+5TqHlONkfOsLpUqNj7HwZ0tVOkqHBQyvHfQGyN/t8DC&#10;YRL4Gnln4DQWeYNqGc95YcEZbDMRbzIskXvpHuvtQbdl0jjQb8HNNGWhPeelYHxsKdiGUh2pSK/O&#10;V6LkTVQn/+2RI16Cd/7gIFGy136X9jv2eu79coBU+QAAAABJRU5ErkJggg==" style="vertical-align: -20px" width="31px" height="48px">
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAApCAYAAAA8hqkEAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAANZJREFU&#10;SEvtle0NAyEIhu1mNxPrsAlzMIVLUD9qemoPjMbkmhyJ/+SRjxd8STC3YhGwYm7FOUW/CeCFwIkD&#10;Em+88CMCFnTBOZ45QH7SEzyA/68B47QOTkJKggIhRY67ZmF8xG4UwXEceQIHTrxb7EYpjNe9vrkj&#10;hVqJyHpsTQRxmaIUn7TWQlc0SA1ganT/2c4KwaxBmsp5QI5gPIW2Xp4EjL9BTYFR3wXq3xg7YLXw&#10;GsAocF5DIRW60EOfQnDuJ/KrjbZMFaAIpwPMt9EeMVNIFuIBiLwB6UZqNLajQioAAAAASUVORK5C&#10;YII=" style="vertical-align: -20px" width="16px" height="41px"><span style="color: #000000; font-size: 16px; vertical-align: middle;"> 	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAApCAYAAAA8hqkEAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAANZJREFU&#10;SEvtVe0NQyEItJs5E+uwCXMwhUtQP2pqn6JGY/KaPBL/ycnBHb7Eh9mJALATZic5Vn8QwAmBEQMk&#10;rvOKUgELGp8czhpAetIRPAD/3wPGZR0UQoqCAiFFjie9MGezG1VgrU3umzjhbo4bUZjreX3rFIVf&#10;JSLr9TUqCMsUJefEteYno4HUAEwX3X+2s4Iw1YPoynWAVME8hWu/HAl0/oYhBUZ9Fwz/xjCB3gj7&#10;AIwC5RryVKihhzYFn1y78quNsk0VQBZOBbA+xr7NhlMYufQBEHkDoThqNGK6PkIAAAAASUVORK5C&#10;YII=" style="vertical-align: -20px" width="16px" height="41px">
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">D</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="22"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">20. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">函数</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">（</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">）是奇函数，若</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">（－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">=4</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，则</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">=　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. 3</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">4	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. 4</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">C</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="23"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">21. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">过点</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADoAAAAUCAYAAADcHS5uAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAZxJREFU&#10;WEfll73NgzAQhv0NyDKuGYEJ8AD0lEjUtBQsQIkrNrjwmjjYxo5BMhJ8OSlFcti55/75o0XYLwhA&#10;f0GYCzlNE7Vt+0j2uq5pnmev7RYoIMuyfCSkNrooCgKHKx9QeCLLsqBHztNLajgjxhuS5w9HTgwk&#10;2HL3+yOG7fEQxwcUnkgXTcOQ5KC4W5Bmkw1fgLfvQAYHeExRoAg1vNP3fVLfKyNSg0rpZAjAOTVG&#10;2nRdp3jMFFagKGIoxnG8P6hroWyIm7m76MEBHnBpUaB5nitFarkkoqaRgAxkDHjAZYHqon4S6Fqb&#10;uiHZNQoOl0mF8YmgOigamJtFeh7UbuObB7fWbv9me/by1FW06xg7BPrYGn2HdRCMnH6ksnRXo1d1&#10;XRgQHC9oJKrG9vV1qld4GlKw6+o5ivmTRtyUt+fc+h/6GZ/uixWDMJqQ35HYB7xzFNdim0i3GR11&#10;11JfIv2KWFWVfzPSZqXddWOw+40mduKIPrrr4hKksLsjHrn8Ts8gml/fXrSx8Ii5Ot0JImYL3qN9&#10;kGpXiB3+L/oX1P4juRxii4sAAAAASUVORK5CYII=" style="vertical-align: -5px" width="58px" height="21px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">且平行于直线</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">+3=0</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">的直线方程为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. 2</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">+</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1=0	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. 2</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">+</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">5=0</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">+2</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">5=0	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">+7=0</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">D</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="24"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">22. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">如图，下列四个几何体中，它们的各自的三视图</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">(</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">主视图、左视图、俯视图</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">有且仅有两个相同的是</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAXcAAACBCAYAAAA7fPpOAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAJoVJREFU&#10;eF7tnQeQHMUVhhfbRVEuRFEU4HIVORUS2BZZJuecc0aILAkBAowAEUQQYIQBkTmCCCIIk/ORk8hC&#10;wBENCJuoI2NjG9vQnq/vepldbZjQM9s9817V1km7Mz3d7/X8/fqlnk0FVLFA//znPysXXXRR5amn&#10;nrLQmjtNzDbbbBXDIv49YsSIyrrrrutOBy315K9//Wvlm2++qXz++eeVjz/+WLf66quvVt55553q&#10;+M2j/vKXv1R6enoq888/f2XIkCGVn//85zW9mHPOOSsbbrih/m6uueaqLLTQQvqa3/zmN5Z6K83U&#10;c+A///lP5U9/+lNl8cUXr+ywww7CoBw58O6771b22GOPymeffVb55S9/meOTZ33U66+/rt/Ljz76&#10;qMKLm5r+/e9/q/Hjx6t5551XDRs2TN13332F+txxxx1qs802UwFAqTFjxqTmVycb+PHHH9W3336r&#10;PvjgA3XTTTepk046SW255Zbq97//vVpppZXUwIED1Wqrrab2339//bnwwgtjyfK6665ThxxyiL53&#10;++23VwsvvLBaZZVV1PLLL6+23nprFSyO6pJLLlHBoqG+/vpr9a9//auT7CjMs6dNm6aWXnppteqq&#10;q6q//e1vhRmX6wMJlCK1/vrrq5122kkFCo8KFJ+OfN5880116qmnqkCxUr/4xS8021KD+3fffadO&#10;O+00tcQSS6hrrrlGv7RFo7Fjx6qNNtpIrbjiit6Ce6BVqNtvv10vwiuvvLIKNDy18cYbq1GjRqkr&#10;rrhC3XvvvYprsqKXXnpJTZkyRR1zzDH6RVhhhRU0P3fddVc1adIk9dZbb2X16FK0O3z4cPWHP/xB&#10;bbHFFur8888vxZg7PUiAfO2111Y777yzuuuuu9T06dM71qXLL79cLbLIIhrgf/vb36YH9++//16d&#10;cMIJatCgQSowx6i///3vCrAvCqFVTpgwQe24444qMFGoTTbZxCtwRx7vvfeeBnA088CEojVngBww&#10;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&#10;QHVEfgAAAABJRU5ErkJggg==" style="vertical-align: 0px" width="374px" height="129px">
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">①</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">棱长为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">的正方体；</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">②</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">底面直径和高均为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">的圆柱；</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">③</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">底面直径和高均为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">的圆锥；</span><span style="color: #000000; font-size: 16px; vertical-align: middle;"> </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">④</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">长、宽、高分别为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">、</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">、</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">4</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">的长方体</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">.</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">①②</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">①③</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">②③</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">①④</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">C</div>
                        </div>
                    </div>
                </div>
            </div>
            <h4 class="p_t">
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">二、填空题（本大题满分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">18</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，共</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">6</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">题，每题</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">)</span>
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                </p>
            </h4>
            <a name="26"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">23. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">已知集合</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">A</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">={0</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1}</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">B</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">={0</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，</span><span style="color: #000000; font-size: 16px; vertical-align: middle;"> </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1}</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，则</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">A</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">∪</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">B</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">= </span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(　)</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAFEAAAAUCAYAAAD1GtHpAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAZBJREFU&#10;WEftmL2OgzAMx3Nvy8xrHG/A3JdAZWbswsLWmenewI2bRkeIwyWyXXqISFWlqrHNL/74ky+wy5yL&#10;RwAhnotHwFDbm6bhWT3g7i0mEcS+72EYhgNi4D0SMkE21IogVlXF8xbs/oGu/gZT32AWtOpNzd3F&#10;9nNr337aUdIBHXeKTQTRdlihaO7Qvh5QBeJ4tfCu4Ng5XzIg03Gn2ChCdGfxzBbxTHSZEkBDqIJ+&#10;qLiPBXG+QW0u0C17BPUbo6ZYEGV7olImBqXsSUmWNB13Vk/ECTRNE+P84q0q5bwTxJRyCXqihj58&#10;G8RnOUsNl3QFUYwCiJiJsUZcTCs/bTe//cRUHCw79USsUkpDK+tEpZ4I8XSWznjKXlZPxNyR04ku&#10;E8d2Q2y/SvBX7xW040DSJIYKwz4V9w4SZ90GVpJkIZLNWq5ksvz7jcXHQPlOOUnHvQPETBJYmq3O&#10;a6GLQM5+NkRpnbiNEk+9JEtyD2apHeXsZ/fE8xaHPqiiWxw0oaEXS3Po0/5fdJ/4acH/h3geP7P2&#10;SflsTIoAAAAASUVORK5CYII=" style="vertical-align: -5px" width="81px" height="21px">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="27"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">24. 240°=</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(　)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">弧度</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">.　</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAApCAYAAAA8hqkEAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAQNJREFU&#10;SEvFVssNhCAQZTuzJtqwBM4mWwOxBy8U4JmTHTwZV3cNysdB1kk8mDCPN4+ZBy+4ECVBACUhSpIX&#10;9nUBTA8hB9jILhEGI5Ro+QBG9dC6YwI46soAlgfgqKtxqZoFQNTd5kwAO0Bv2RwGRjnVSfmTjzQ5&#10;i2gjsTTY7/I8QM6cVB6mDAr3MWiad7AH/L6gtVvcxyCj3OudmANaq4TVztahCg1SwJUp2fOE3b9f&#10;1rEEO3kuTIAddMCa0xo4k5GrxV32A1Ay915YvODrTD9N0hp4KzYgqSduI03QskUBAEBmyzLVz+UQ&#10;F/J4jHQj722dewo5g/SHB0YGjXQrJ0CeB5gBsOuCVyII7+sAAAAASUVORK5CYII=" style="vertical-align: -20px" width="16px" height="41px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">π</span>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="28"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">25. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">请写出一个模等于</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">5</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">的复数：</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(　)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">.</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">（只需写出一个）</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">z</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=3+4i</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAF0AAAASCAYAAAA5f9J6AAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAtBJREFU&#10;WEftWMGx6yAM9GvZbfwSOKcJxj34QgE5c0oHPAkQBhsLge03/xBmMkk8IMRKuxL+cTCm7/hbBBD0&#10;fEzTv/2jm/+/nYI9Zv252W6HObu6eV6d7VgyMvUMy4mM4QQOcKtfbto5Wnsmcw6AV2/Z1EdmhcAr&#10;E76nB32pYVqAzp3PKHTy4/SMwVmccejwAuCFYOFHmTMLsE6tzmDgaK5a0m981p/55Mu2v9iGgb0T&#10;0GgHz/Pc2APvQW9KCtIRnDTq5XTkpP9twvPmwEPOL6dQUqKtsObjLMvxI7AUND7IpUd7RoYEKufg&#10;M3HQmgfmJVsAOmQ0ygoCN0XQATgdncYD9ThrIMNxqQciZn3+LbMVgsHO9f4GFsx63epI1HOdMZQC&#10;aO1zdSZP7AboFeqh0wQ+G/GolztgCYAxScFo4f6SrMx8z9iFGT2B1BHDMPjnstiZ0sz0DtCPVpK2&#10;o+MDPlm9QOdAGTiQWeKguyiHFHxgGGT/rN9Jw0u2XdN1H0ymKA+DXkhJx+EpNsGxrS5sh5YeGKUO&#10;ijfUBw1S4Is6V1O8xJBt6pgiA3wAoKawmV5na00Wi2cVn4ZA9wAdjFGh24CsJT+Bm2hcdA9SuoRu&#10;yUDx1R70sK6VYVSwtw5lkx1ppltoLUdYnZ+sG/RU2bPiVM/YumycFc1WdiSnPasoY0vQA/KxaEo6&#10;KR+0xenYAPx/mh6L1p2FxtcE7Cagi5G0m8cLWAV0BL4IDMeesjnwoIM/WJzb9w0pKwUtI0459uqx&#10;VYzrE40PbR4vLfn2iTEAkO/ZY4bWg3rWFqLMyPc8wlT2/qjrBkC/Kh9cOKqXI1rAXpIOOhycl7GA&#10;rt0UwVDEwsiKleh9yEXQxYwYy+r9KvY1QBv4WiVvdx1Bz3fzzgppcVs9P/S1zCwZfA+0dSvNF15P&#10;bl6jOH/9f9abJ2+eEs/TCy/J5O+cexD4gn4Pjl1WfgGU7KEOZW9IUQAAAABJRU5ErkJggg==" style="vertical-align: -4px" width="93px" height="18px">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="29"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">26.　</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">对于函数</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=sin</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">，当</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(　)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">时，函数有最大值为</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(　)</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">.</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【(1)&nbsp;&nbsp;分值】</div>
                            <div class="score">1</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAApCAYAAAA8hqkEAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAQpJREFU&#10;SEvtVcsRhCAMZTuzJtqwBM7ObA0Ze/CSAjxzsoMsAfxuAMU97ZgZDzLwEvLyHi9yoe4EA9wJdeew&#10;r/4B+HkPRjKqdXMhfHogK3R8xwKantBtstAT8G47kFZhLRUijWi6AIA9qUTmGVAAmAh0zFoFwIdi&#10;2Ra6qxVwdtdAM/oKtwAIJ5oYmtaSmbsW/5mVZe3QzUcLGz9omrc8wsJY897MIF2zmP9lYe8LqSlM&#10;uPJGjbMeLo0yDsELltgL7MjRKRbQrAqtAAgV1KuRJZ2xteIVFn9MDGgWgB0pR2H+cXXeqGFa87qr&#10;gODvcgXeWI+Pi/w+fAF4I5Vepmi0FTTm5V1koeQODwDRB1tYeffOAtLaAAAAAElFTkSuQmCC" style="vertical-align: -20px" width="16px" height="41px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">+2</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">k</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">π</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADAAAAAVCAYAAAAAY20CAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAgBJREFU&#10;WEfVVztuwkAQdQ7IPah9DfoUuEmD6CktKFKhVFBYiigigVLhJtxg2Le29zvjTxwCjISE5N3Z95kZ&#10;r19IRfLMAQLPHIkE/nQ603b7eXdum82ezucfEQdLAJv2+6+7g28AAI8kZkRgtfogbLBxpCyZqT6Z&#10;UZpf/pDUhfK0yhv/FpSX/lHL5TsBWxgegcPhmyaTVwYkSMRJvYXFWgBTA0x35GNCzjUVJoklJAkF&#10;bMDohkdgOn0jMI2i3FEaAXBWafAumD5GHamw6ImMAHIeYANGlgBqHlaGDLG4zBeUZEcRVZHNKHPB&#10;9MHvrelXpg1Gtz+NA/N5rgnEHV9ZqwHCCadmK6vxvKO8OghpgXTedhcxGbEOWJswBGANHrLlYxIr&#10;pSInrHp8Q9Y9IDrYT30DWGF0y8gQaA4PCZjy0epzCo1zoK/6LgFX6E4CqO9UlUibxb/vAat+2ENl&#10;yY/sUGhDACMqLqFqfGaFSqYdEGodE6SlyaUWMOpzI1bIB4xsCTVNjEYxAWAmeT2ndWL1P/enElwY&#10;1gOy+tp15qWJASM2MTeiwkS2XsdNHQjURZgjgBEPAuwYRVKUEfsiGzzXb7Oh9UWGI3Fh4q8StwE0&#10;NGvnVQIJwfIRXcBFrvMy1yiChY9EAjXv35Ctd60fNNKmodaPWY+y9iZjkEwkMObQ/9x7BdZ3usCm&#10;3A3dAAAAAElFTkSuQmCC" style="vertical-align: -5px" width="48px" height="21px">
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【(2)&nbsp;&nbsp;分值】</div>
                            <div class="score">2</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">1</span>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="30"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">27. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">如图，在边长为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">4</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">的正方形</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">ABCD</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">的</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">BC</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">边上有一动点</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">P</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">，从</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">B</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">点开始，沿线段</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">BC</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">向</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">C</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">点运动，设点</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">P</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">移动的路程为</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">，△</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">ABP</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">的面积为</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">S</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">.</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">则</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">S</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">关于</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">的解析式为</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(　)</span>
                        </p>
                        <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIMAAACdCAYAAACXQcuNAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAADolJREFU&#10;eF7tnXlsVNUXx+eXkJgY/jPhX+MaQQRaEFki1IaKhE2IFm1kUXY0lRaqQiJhi8Q2EKABWiY0VdSW&#10;HSzrmJYt0JhWWTJAwaIWUGBAhIKKC5zf+96ZN7xZOjNt5725791zkwl05r377jnn887dz/0facnF&#10;yfEa+Pnnn10nT550derUyfXwww+7/vrrL9cff/zhGjBggKtDhw5++QGDMf3333/077//hn/Nf9tc&#10;A01NTfTSSy9RRUUF/fDDD+T1eumTTz6h3NxcunbtmpAuAoaqqioqLy+3uehc/HANaJ6AevfuTd9+&#10;+23IT5mZmbR9+/ZIGEDPqFGjaNiwYaxNh2ngzp071K1bN/rxxx9DJJsyZQqtX78+EobS0lL6/PPP&#10;KSsri3755ReHqUNtcX766SfKyMigq1evhiji/fffp23btoXCcPDgQVq0aJFoL/Tr14+OHDmitvYc&#10;Jn1lZSVNmjSJ7t27F5QMVQdqgrNnzz6A4e+//6aCggJas2YN7dy5k55//vmg63CYTpQVp7CwkKZO&#10;nRoi/44dO2jjxo3B70QDEg2I5cuX0927d8VnyZIltGDBghCKlNWiAwS/fv26qPqLi4upoaFBfI4d&#10;O0Y1NTUER6An140bN+jDDz+k27dvB788fPgwDR06NORCB+hEWRF8Ph/BC9TW1oouJT7nz5+P0Idr&#10;1apVNHPmTLp06ZL4sbm5mTZv3kyvvPKKIAjjDuvWreOPA3QQrx3oQlcDn99++03A8Oeff5I2WiW+&#10;u3LlivgbY1NPP/00vffee+Lz7rvv8scmOtBt9vjjj0e0GcJdQ8SgU/gFOgyPPPIInThxQllXa3fB&#10;0WsIb0C2GYY+ffpQdnY23bx50+56UbL8SYUBAxMYzly5cqWSyrS70EmFYf/+/bRr1y5KT0+n48eP&#10;2103ypU/qTAcOHBAKPCDDz4Qcxfa9KdyCrWzwKbAgDmLvn37UklJiZ11o1zZTYEBWqyurhYzYHV1&#10;dcop1a4CmwYDFJKfn0+DBw/mIWub0GEqDBix1JZMickt40yYTXSjXDFNhQHaxLT3k08+yYNRNkDL&#10;dBj03gXmMTDbyUleDVgCA5bKYXQSk1mc5NWAJTBAfEyNorrg3gXDQP/88w/NmDGDRo8eLa82FC+Z&#10;ZZ4BesYUOJZdFxUViTUQnOTSgKUwQPS9e/fSE088ITZpcJJLA5bDgOpi7ty5NHLkyJBldHKpRc3S&#10;WA4D1IytWuhdYDnd/fv31dS8hFKnBAa9unj00Ucjdu9IqCNlipQyGLD8+qOPPqLXXnuNbt26pYzC&#10;ZRY0ZTBAKb///js999xz5Ha7ZdaRMmVLKQzQ8jfffEOoLrh3kXrmUg4D5iuwVHvs2LGEXcCcUqeB&#10;lMMA0bGRF9UFtu9xSp0GpIAB3Uvs0OrevTs1NjamThuKP1kKGHQbzJkzh0aMGMH7LlIEpVQw/Prr&#10;r9SzZ08qKytLkTrUfqxUMOiDUVhIy/surAdTOhgwm4kd3xiM4n0X1gIhHQwQH9UFIsPwYBTDIDSw&#10;Z88e6tGjB1cXFvIgpWfQ5Ud1MXz4cLFKipP5GpAaBiykRXXBAUjNBwFPkBoGFBCbebt06UL19fXW&#10;aEThp0gPA2wzbdo0sTKKk7kasAUMFy9eFAGuV69ezQtpTeTBFjBAfo/HI/Zd6JFKTdSJslnbBgbM&#10;bCIWJXoXCCjGKfkasA0MEP3ChQti7gKBzDklXwO2ggHiIzLtY489RtqJKcnXhuI52g4GRDnHNr2c&#10;nBwejEoyvLaDAfJjmx5iRiHoNQcBSR4RtoQB4uOYA2zTQ9hiTsnRgG1hwELavLw8euONN3ghbXJY&#10;kH84OpacCEuclpYmtulxar8GbOsZdNG3bt0qeheIbs+pfRqwPQwYjMI2PQjCAcwVhwHi46QcDFV/&#10;8cUX7dOG4nfb3jPo9tPnLnAyDqe2acAxMGBXNwajxo8fz0FA2saCvXsT4TJjVzd2ZXEA87bR4BjP&#10;APGxTW/Tpk1iqRxXF60HwlEw6OJjIS1CDPKu7tYB4UgYMETdq1cvnupuHQvOajMYZcfcBfZd8FR3&#10;4kQ40jPo7QecrYnqAgNTnOJrwLEwQHRUF2hMfvbZZxxiMD4Lzq0mdNkRkRb7LnC2M6fYGnC0Z9BF&#10;R3Xx6quvcnUR521QAgac2Y3qgucu2DMIDdTU1FDnzp15V3cMHpTwDJAfPYpJkyaJlVGcomtAGRgg&#10;Pg5fHThwoFgZxeddRAKhFAwQf/v27WIhLfZvcgrVgHIwYKob2/TQu+CYUYrDAPHhFdLT08XcBZ93&#10;8QAI5TyDcTAKC2nR7eTk14CyMGAnNwKYjxs3jnd1B94GZWGA/NimhwCkHGJQcc+gVw3oXaC6QLdT&#10;9aS0Z4Dxsat7+vTp9Pbbbyvfu1AeBgCB9sMzzzxDpaWlSjsHhiFg/vXr11PXrl3p8uXLygLBMBhM&#10;P2vWLLEyStXT9BgGAwyXLl2iZ599liorK5X0DgxDmNmxkBbVxZkzZ5QDgmEIMznmLqZMmUITJkxQ&#10;rnfBMER5/3HeBbbpYSGtSolhiGJtfZte79696fTp08rwwDDEMPXUqVPF8UiqnHfBMMSA4fz582Kq&#10;u6KiQgnvwDDEMfOuXbvEZJYK+y4YhgTe+YkTJ4rqwumJYUjAwlgAg4i0WBnl5Ii0DEMCMOASDEZh&#10;MuvcuXMJ3mG/yxiGBG2GpfXYppednU0YmHJiYhhaYdXGxkZx3oVT5y4YhlbAgEv37dsnqotTp061&#10;8k75L2cYWmkjrIxCeEHMXTgtMQxtsOjVq1dF7wKn6Tmpd8EwtAEG3LJx40Z66qmnHLUyimFoIwyo&#10;LhBi8M0333TMVDfD0EYYcBvWSw4ZMsQxIQYZhnbAgFsxd4Glcj6fr505pf52hqGdNsD0dmFhoSMC&#10;mDMM7YQBt2ObXr9+/eirr75KQm6py4JhSJLuDxw4IEYn7XyaHsOQJBjQu5gzZ44488KuZ3UzDEmC&#10;Adkgiv2AAQOorKwsiblalxXDkERdYyFtVVUVZWZmUlNTUxJztiYrhsEEPS9cuJDGjBlju/MuGAYT&#10;YEDMqMGDB9OWLVtMyN28LBkGk3S7f/9+EaLYTtv0GAaTYEBE2vnz59PkyZNts++CYTAJBmSLXd2Y&#10;6sYMpx0Sw2CildC7OHr0KGVkZNiiumAYTIRBzzo3N1dMdcsegJRhsAAGTHWPGDFCnLkpc2IYLLIO&#10;ehdpaWlSL6RlGCyCAWsl582bJ3oXsiaGwULLXLhwgV5++WUqLy+XciEtw2AhDHjUoUOHqH///rR4&#10;8WLCQe4ypaTAgOlbhNzFkcMQlD8t6+CFF16gjh070kMPPSRO5JVJVzjDa/bs2bR79276+OOP6a23&#10;3hKhEJcuXRpc1ueKRy/qw++//54/CeigtrZWRLLHFv8dO3ZIpbO6ujqaO3cubd68mZqbm4XZERMT&#10;xzjpazQiYMDiz6KiIurTpw9t2LCBpk2bRp9++qljlozHg789v2MDLyLCnDhxQgQAwQdtCRkSejyI&#10;RYF1GcZk3CgU1TPgRvSdsSMZ/Wgs+bLjHL7VRoBiYXzs5kZE+++++07s7sayuVQnDI7FC1kUFYa8&#10;vDzCvD2Ew1Qtukx2Xe5ltRFu375NgwYNEt4Badu2bZSTk2N1MSKeB0CxOCdWigoDKALRxcXFtGDB&#10;AqkHU1Ku5bACoG5GF/Pu3bvily+//FL0LlKd0JZZtmxZzG5vVBiGDh1KqCqQcPg4diXL1lVKtXJb&#10;ej423rz++uviZwQgxeGrMkSzr6+vF0v+v/76a1E2NAGOHTsWtDO+i4ABLcwXX3wxeJTwjRs3CF0m&#10;WRpCskKglwtdtYKCAtFOQLuhoaFBiiJjIq26upoQXR/dynfeeYdw9IIR1AgYcENWVlaw1Ykb1q5d&#10;G3R7UkgmaSGw6AUglJSUSFrC2MUKgQGuY8WKFeIYHzQct27dSgcPHuTjhBM07c2bN0O8aoK3SXNZ&#10;3EEnaUpqg4JgZA8jkMOGDRObdu2WGAa7WczE8jIMJirXblkzDHazmInlZRhMVK7dsmYY7GYxE8sr&#10;KQxecrvyyROMnuMjT77/b6/b+L2JmjEra5+H8t1es3JvV75ywuB1k8vl8n/yPeTT/nZ7AYRL/Bs3&#10;xJLx/hDF+/MI5h14Rou2MeaDawMX+jz5oeULM0HI77ocBnk87oBc7TJd8m+WEAbNK2hKh0Lz4Qr0&#10;Nwn/utwU950yvnniHpc/HySvx+BtxBeaB4qVp78sSCgP/ivK5XbHfrsFvAFjAagw2vS8km/O9uUo&#10;GQxRjKO5AXgGvM1Bo8aQ2actKDHGZhNvKbxLtHuiGCr0Mt2TaCC4PUEQxTNilckAg1cDxw1PoHs5&#10;A1jtM13y75YMBl1AQGFw53izNAUnAkOEimC0FmDwakaKV33jmnBDBj1DSzAFYBAeQHiRUE/CniFR&#10;kDXX7g66dUPbIZEqIsozgtVNJCVxqgjjDQE4Nai8HrdW1Wh/B9oyUQEV1ZGXPEIOXKtVLcbyG6uR&#10;RPViwXUSegZDI097bb0eo4vXftPcdeIhOgNGa2UVEdkAfNCu8Gnl0Urlb0u05K30tklIOyfU27XJ&#10;y5kMhHwwBBp94S1+/9/aG4beQDzfHmy7tdw4TKSK0I2twwEDChj0N7uFNzwIk6ieAnAby9zWKk85&#10;GIIC+xuT7nDPEBhviKeX2PVyvF6EP3fRXhBVw4PGY7B3IxqQ0cc8fKIqMfQmwhu/DEM884XX0f63&#10;OlhN6H3+lnoGIbeHNTbD3+C4vQjR5Pe3XcS/2geG14wc0hhtYQAp2sBYyNmZDENiMPhdrO7eW9tG&#10;8I8DRFYxodVF3CoCRo6ADmVBQ9Dfu2i5poo+sPWgTOhhBMZQElOJZVdJ1WaIbPnHaACaqKLwsYrI&#10;R4V1fRMcA/Hno4+kmihAG7OWCoZIGVrvGdqoB75N04DkMLCNrNQAw2CltiV/FsMguYGsLB7DYKW2&#10;JX8WwyC5gaws3v8Bg9AYCw/NXWQAAAAASUVORK5CYII=" style="vertical-align: 0px" width="131px" height="157px">
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">S</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=2</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">0</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">≤</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">≤</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">4</span>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="31"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">28. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">从</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">名同学中任选</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">名到不同的岗位顶岗实习，共有</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(　)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">种不同的分配方案</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">.(</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">结果用数值表示</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">)　</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">6</span>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <h4 class="p_t">
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">三、解答题（本大题满分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">16</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，共</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">题，每题需写出必要的步骤）</span>
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                </p>
            </h4>
            <h4 class="p_t">
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">29.(</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">本题满分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">6</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，第（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）小题</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，第（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）小题</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">)</span>
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">甲、乙两同学到文具店购买铅笔和橡皮，购买数量如表所示</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">.</span>
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                </p>
                <table style="border-left: 1px solid #000000; border-right: 1px solid #000000; border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-collapse: collapse;">
                    <tbody>
                        <tr>
                            <td width="176px" style="border-left: 1px solid #000000; border-right: 1px solid #000000; border-top: 1px solid #000000; border-bottom: 1px solid #000000; padding-left: 9px; padding-right: 9px; padding-top: 0px; padding-bottom: 0px;" valign="top">
                                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                </p>
                            </td>
                            <td width="191px" style="border-left: 1px solid #000000; border-right: 1px solid #000000; border-top: 1px solid #000000; border-bottom: 1px solid #000000; padding-left: 9px; padding-right: 9px; padding-top: 0px; padding-bottom: 0px;" valign="top">
                                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #000000; font-size: 14px; vertical-align: middle;">铅笔</span><span style="color: #000000; font-size: 14px; vertical-align: middle;">（单位：支）</span><span style="color: #000000; font-size: 14px; vertical-align: middle;">
                                    </span>
                                </p>
                            </td>
                            <td width="175px" style="border-left: 1px solid #000000; border-right: 1px solid #000000; border-top: 1px solid #000000; border-bottom: 1px solid #000000; padding-left: 9px; padding-right: 9px; padding-top: 0px; padding-bottom: 0px;" valign="top">
                                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #000000; font-size: 14px; vertical-align: middle;">橡皮</span><span style="color: #000000; font-size: 14px; vertical-align: middle;">（单位：块）</span><span style="color: #000000; font-size: 14px; vertical-align: middle;">
                                    </span>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td width="176px" style="border-left: 1px solid #000000; border-right: 1px solid #000000; border-top: 1px solid #000000; border-bottom: 1px solid #000000; padding-left: 9px; padding-right: 9px; padding-top: 0px; padding-bottom: 0px;" valign="top">
                                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #000000; font-size: 14px; vertical-align: middle;">甲</span><span style="color: #000000; font-size: 14px; vertical-align: middle;">
                                    </span>
                                </p>
                            </td>
                            <td width="191px" style="border-left: 1px solid #000000; border-right: 1px solid #000000; border-top: 1px solid #000000; border-bottom: 1px solid #000000; padding-left: 9px; padding-right: 9px; padding-top: 0px; padding-bottom: 0px;" valign="top">
                                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #000000; font-size: 14px; vertical-align: middle;">3
                                    </span>
                                </p>
                            </td>
                            <td width="175px" style="border-left: 1px solid #000000; border-right: 1px solid #000000; border-top: 1px solid #000000; border-bottom: 1px solid #000000; padding-left: 9px; padding-right: 9px; padding-top: 0px; padding-bottom: 0px;" valign="top">
                                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #000000; font-size: 14px; vertical-align: middle;">1
                                    </span>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td width="176px" style="border-left: 1px solid #000000; border-right: 1px solid #000000; border-top: 1px solid #000000; border-bottom: 1px solid #000000; padding-left: 9px; padding-right: 9px; padding-top: 0px; padding-bottom: 0px;" valign="top">
                                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #000000; font-size: 14px; vertical-align: middle;">乙</span><span style="color: #000000; font-size: 14px; vertical-align: middle;">
                                    </span>
                                </p>
                            </td>
                            <td width="191px" style="border-left: 1px solid #000000; border-right: 1px solid #000000; border-top: 1px solid #000000; border-bottom: 1px solid #000000; padding-left: 9px; padding-right: 9px; padding-top: 0px; padding-bottom: 0px;" valign="top">
                                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #000000; font-size: 14px; vertical-align: middle;">4
                                    </span>
                                </p>
                            </td>
                            <td width="175px" style="border-left: 1px solid #000000; border-right: 1px solid #000000; border-top: 1px solid #000000; border-bottom: 1px solid #000000; padding-left: 9px; padding-right: 9px; padding-top: 0px; padding-bottom: 0px;" valign="top">
                                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #000000; font-size: 14px; vertical-align: middle;">2
                                    </span>
                                </p>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <p></p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                </p>
            </h4>
            <a name="34"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">29.</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）</span><span style="color: #000000; font-size: 16px; vertical-align: middle;"> </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">用列矩阵</span><i><b><span style="color: #000000; font-size: 16px; vertical-align: middle;">A</span></b></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">表示两人购买铅笔的数量，用列矩阵</span><i><b><span style="color: #000000; font-size: 16px; vertical-align: middle;">B</span></b></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">表示两人购买橡皮的数量；</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">【解】　</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(1)</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">矩阵</span><i><b><span style="color: #00008B; font-size: 16px; vertical-align: middle;">A</span></b></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAvCAYAAAD5CArtAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAWNJREFU&#10;WEftV0sWgyAMtFf2Gh6BdS/B8w5uPIBrVt4gJeIHKDGESt9rn6z6STLOJODwALuamgsBaq6mZvFF&#10;HQqgaToWOysmroJJOYlbHhcfMEgXnkCtoPi/GtPEqIdiALB4D1tNo5+W3fHdh2IBkgFmBhM8MAI+&#10;QYc/7hGpGjuDLN3NAK2ayOZ/BoDF2yFiFGIVAzjt3XRRPXAnwvtoizbaBtTqmd0j+xj788xnzaDb&#10;DiiAYol84FHJ9oJ8ik4aLWcw9l5zbYNrTRHfGxchZ5BbeY37cwChGtnhsjFlyt49yNY9eNMVZQmS&#10;7iYHYv3ImOIRThzbFzBYXV4tgFH1oNFhlACwo22lQV+6OAvmzZbcaOfOzkqzOrozgOIeoDSBAS6R&#10;iGRgLaP2LPvlDNAHHbYx/BzfFYol8ptWzICyHfF0UQDsBeQrALkgMauzEU/ad+7m6Dty7mYkuh+w&#10;uz0RcAOwqr0AF2Aiqha1f6YAAAAASUVORK5CYII=" style="vertical-align: -18px" width="24px" height="47px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，矩阵</span><i><b><span style="color: #00008B; font-size: 16px; vertical-align: middle;">B</span></b></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABcAAAAvCAYAAAAIA1FgAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAUVJREFU&#10;WEftV8kRhCAQZFMmjQ2B9yZBmYMfAvDtywxYQPGAGQYQPxZ8rFJp2rbn+miz2FPLgj+12FPAThEM&#10;nLFv8lzqOQhuN+VsXP9V+t0L8xh00ZIbAD7qOfEdGJkE+KTFxuw2OHb6LH8kcy9R+HE78w5+loYM&#10;olzNITO9WHMl6CCqsOIpiFww/bRMhClkZVLz3KzZwUGlkrLkalvyXv+hYGkkE1eJxmixuAOCdhD+&#10;QW7FL2lFulvSbsE1v6ZeoWDVK7Ki7bgG7fFcPTW5HTqgHFyNQYHY2jsxRfSbJC5X9gDwqup/3bQy&#10;byNLSGceNUc63nLNA3Al8CJ9C9w6BbNhRWtxoq0GzeVy3DDyyMDvpFvAQDLAfjw5rof3MdbRTBSC&#10;+6CJwAMrZo0tKRYlqXZP46D5MyY6apKLZGldjV5QQ1tLYvH+TVBqX2BiYJ8AAAAASUVORK5CYII=" style="vertical-align: -18px" width="23px" height="47px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">；</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">　</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">3</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">分</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="35"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">29.</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）</span><span style="color: #000000; font-size: 16px; vertical-align: middle;"> </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">若铅笔的价格为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">元</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">/</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">支，橡皮的价格为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">元</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">/</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">块，利用矩阵求甲、乙各应付多少钱？</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">【解】</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(2)2</span><i><b><span style="color: #00008B; font-size: 16px; vertical-align: middle;">A</span></b></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">+</span><i><b><span style="color: #00008B; font-size: 16px; vertical-align: middle;">B</span></b></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=2×</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAuCAYAAAAyVNlIAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAV5JREFU&#10;WEftV8sRhCAMZVu2DUvgvE0w9uDFAjxzsoMsEV3llwDq7jgjJ8Ukj7xEfXmBWeLKhQBXLnFl8Jmd&#10;FIAQLYudZeNHQSfXcQS57OG+HFyP0N57vr8NT4TBO1hjavU24Ns97WufOhQFAHoC7RwIAd+g3E0b&#10;KEHpFyCHT9A9NHJM1iYWIx8Agze9l1FYj6Cm6waVgeXeFj9VgxRN+RmYCCtQo6YoTSRFbNPPBhOo&#10;poUUQCxGUQYYYJDhu0DRXAbAFLq8i4ZuV1xT4CNdlFeDcqsyipj45RQVHvgBYAl7KDpGEetdafC8&#10;aA5xN2hT/HwTn+yDGSwK7yqAQXagUF3UApBtbqhBTTqrCuavVqGLDDWLmuMAqmqA1Djit5aiqLIz&#10;KkLt5PrpGaAG2iSje+3PCedJx1qKKI2/dgZFETsf/AQgByQqcImBMTplcoPdXuxyk9H/5uTKP2Tg&#10;dv8MPjRfwvtbIFrFAAAAAElFTkSuQmCC" style="vertical-align: -18px" width="24px" height="47px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">+</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABcAAAAuCAYAAADDX4LFAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAVBJREFU&#10;WEftV8ERgyAQJC3bRkrgnSYYe/BDAb592QEBlCBwBwfK5AMfRwN7m+WWO15KD9ZrGPBeg/UCtopg&#10;4Iy9s3FLv4PgZpFfuCsx6fdpURsQKpybTgiYh2xWxc9AGLiDw/5FBvxYuokPypwMjkWngB/pnO7R&#10;j/kAv+ZM0URUzSFT/FdzyXETBRLUZcvFRNZMHyUgm54RsqnY4wArak4N2mSiAU5VAJw3NK+X5Zbg&#10;yOJhorYaSt2LxjwPj14u4XAN4KbjmpXDsyVPn+1QgHpwuUQF4mzv+JrQf6RY2LIHgDcV6HDRwfwZ&#10;WWI626KmTMcbT69yqOR4ka7f0AsVkylYGjY3ohZfzmoSuw+l5RFRvhezBex0NbC7Qfinz32Mtf2e&#10;65qcaRLwKBVJN4scC+yMyV28wC63dJFyJEo3umILTT0Vsw69A4JK1gPUYXaV5QstMA67aZHypgAA&#10;AABJRU5ErkJggg==" style="vertical-align: -18px" width="23px" height="47px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB8AAAAwCAYAAADpVKHaAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAY1JREFU&#10;WEftWEsSgyAMpVf2Gj2Cay/heAc3HMC1K29AET/lE5IgWJ2pzHTjBN6HhBJeSg9x1ZjBrxriKmDj&#10;OAYuxDuZW8ocEHxeIGURnyF3fgCeAjq2jSHp/hrVjgsdai0HnAp2FQ6qDoA1kapXKzZJ4Di4HJT0&#10;/ZadqtrJ+YoJ2sHTVMN5KOuv5XZEbO2C4HobPMs3AueDA5b/DDxmOZb1hWyPW84CTz7K7AmI5egJ&#10;Su0LhxRmOUv58VLDLT8ZnPbm/FJDOJDgNP/yEYVKDSdGKj+ecLQj9wan+ZePePacvG/lmH7vhPvf&#10;UnuUh1k9qbYKm4Etzu5a6uAij3cuRJdqdSXQtVhfn4To1uZhiY0RgEqVdcIZdQH44ogDNpMBSGbV&#10;OQg+9qoSXocCfUMaxuPKHcs3U2Hryyu/HbixPUy68spL7jl2v+ZmOxTH6s8xcFlHDhmntNKSzeD5&#10;xe8y9Z8+wuYfO+Go/4voa1TO5QFz0V43erxyn7Mg5yjFe3OaqzBn/n2fP3NUceZeqvwD7zxr8w8A&#10;HosAAAAASUVORK5CYII=" style="vertical-align: -18px" width="31px" height="47px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">　</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">分</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">即甲同学应付</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">7</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">元，乙同学应付</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">10</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">元</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">.　</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">1</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">分</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <h4 class="p_t">
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">30.(</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">本题满分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">10</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，第（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）小题</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，第（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）小题</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，第（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）小题</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">4</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">)</span>
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">某帐篷的三视图如图</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">(1)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">所示</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">.</span>
                </p>
                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAU0AAADPCAYAAABx/4OjAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAMCxJREFU&#10;eF7tnQvYVVMax5thHiaXwtOTa82Icb9kIiqVECKXdKWmG1/NdFMaRWIIUYYkmvoyKYRyl4YySsko&#10;NYoPUbkkJJckDGNm1pzfqnXaZ3/nsvfZl7P3Pu96nvP0dc7a6/Jf6/zPe1vv+plKlRpSBAFBQBAQ&#10;BJwhAGlKEQQEAUFAEHCGQA1n1aSWICAICAKCgNbMBQZBQBAQBAQB5wgIaTrHSmoKAoKAIFBekub/&#10;/vc/1atXL7XDDjuoRYsWhbb8t956q+ratWv61aVLF/WrX/0q4z3r5/y9adOm0MZHR5s3b1YNGjRQ&#10;TZs2VVu2bAm1b+lMEIgTAmUlac6ZM0ftvffe6sYbb1SHHXaY+uabb0qyVgsWLFB77bWX+vbbb0vS&#10;v71Tfkz69eun+vbtq9q1a6dGjRoViXHJIASBKCJQNqT5+eefq4MPPlg9+OCDeh3OPPNMddlllykI&#10;I+zyxz/+kTAvtXz58rC7ztofJI7ky4/IRx99pH7zm9+opUuXRmJsMghBIGoIlAVp/ve//1WXXHKJ&#10;6tixYxr/tWvXqjp16qiXXnop9DVp1KiRJs3x48eH3re9w6+++kodfvjh6rnnnkt/NH36dHXSSSep&#10;H3/8seTjkwEIAlFDoCxI829/+5vaf//9tRRlLZWVlerII49UX3/9dWjr8umnn6o99thDk2bz5s1L&#10;IulaJ4taXlFRUW3+559/vjZjSBEEBIFMBBJPmhs3btTqplHL7RvgjDPOUIMHDw5tXyxcuFD17t1b&#10;mwpQ0//zn/+E1re9I9Ty/fbbT33//ffVxvDBBx+ogw46SK1YsaJk45OOBYEoIpB40oSg2rdvnxP7&#10;1atXa+KAQMIqL7/8sjr11FPD6i5rP1988YVWy+fNm5dzHPfff79q2LBhSccpnQsCUUMg0aSJt7xe&#10;vXpq3bp1eXFHTT/66KPVl19+Gcr6RIE0BwwYoC699NKC8z3vvPNETS+IklQoJwQSS5obNmzQavlD&#10;Dz1UcD3//e9/qzZt2qghQ4YUrOtHhVKTJtIlMZn/+te/Ck7n/fffV7/+9a/V66+/XrCuVBAEygGB&#10;xJImUlSHDh0UnnMn5Z133tHk8Pe//91JdU91SkmaqOXHHnusevbZZx3P4b777lPHHXec+umnnxw/&#10;IxUFgaQikEjSfPrpp7Uk9eGHH7pat8mTJ2s1PejTOKUkzYEDB6o+ffq4wgVJnKD3G264wdVzUlkQ&#10;SCICiSNNvOXWIHY3i/bDDz+oc845R11++eVuHnNdt1SkOXfuXB1iRWym2/Lee+9pSXzlypVuH5X6&#10;gkCiEEgcaRJzSBA70lExBW86MZ3z588v5nFHz0A8bqU9Rw3nqcTZ8mOOOUbNnj276KYIej/hhBMk&#10;6L1oBOXBJCCQKNJ85plntLfcHsTudqEmTZqkDj300Kzxi27bikr9QYMGZQ1idzM+o6ZL0Lsb1KRu&#10;0hBIDGkiSXF+esaMGZ7XCHLgbDrB58WWzz77LOej7777ro6PNK8333yz2G4cPYfUjFqeb0yOGkpV&#10;4geJpCdBj9npeKSeIBA2AokhTdTdCy+80LcTNsR27r777gr7o9OCan/zzTdrT/Ndd92V87GbbrpJ&#10;nXvuufpFHGSQZ9D5AcDG60Utt0/k3nvvVccff7xvWDvFV+oJAlFAIBGkSbKJPffcU+EE8rOgpiO9&#10;Og1bWrVqlQ6QR0KdMGFCzqE8+uijfg4zb1t4y0lW4mchMxSEL2q6n6hKW3FBIPakiccbdTHX2XKv&#10;C3H66ae79qYXIs2JEyeqYcOG6STEv//979V3332nA81nzZqlRowYodX2Tp06qWuuuUYnBx46dKjq&#10;3LmzevXVV11Nh3PuBPj7oZbbO+YHarfddlPEt0oRBMoJgdiTJkHsqOVBFbIS7brrrmrx4sWOuyhE&#10;mpDZ+vXrdXv9+/dXLVq00H9j60Sy5RQOYUFHHXWUJlEKKnGTJk0cj4EfE0KE3ASxO258W0W86Y0b&#10;N3Z0ssht21JfEIgqArEmTdTy2rVrFxV36GZBUNMPOeQQx+RQiDStfZOzkjkY0iQsyJQePXqomTNn&#10;6v9CstgmnRa85ajlQSdZbtu2rajpThdF6iUCgdiSJvfYHHDAAb54ywutJOnbWrdurdVkJyUfaZK7&#10;8+OPP043g5qLp95P0iRjEyTvt40329xR/evWrStB7042htRJBAKxJU285RztC1qSMqtMfkmSBztR&#10;00lHd/3116c3CPkqW7ZsmT7WedFFF6kXXnhBe+YvvvhitWbNGt9Ik3uHsGOSeDmsMm3aNNWsWTO5&#10;kC0swKWfkiIQS9JELedisjAkKevq4MDhQrZsSXtNveeff1795S9/UaSbM+FKeN+feuqp9EkabJZ4&#10;1/Gim0xD/IsqjikASfG1115Tf/3rX9UDDzygsKtChrRbKKEI9x6hljv1+Pux++gLNZ1QKimCQNIR&#10;iB1pcvlX/fr1NZmEXSCH0047zVPQe5Bj5lpiHElh5QW1zgWTA+sSlcvigsRZ2i5vBGJFmqjiqOUX&#10;XHBBaGq5fXugpteqVUu98sorRe+cf/zjH4prNvwspVDL7ePHw8+9R1G5mthPfKUtQcAgECvSJEsP&#10;N0iirpayoCYfccQRRZOD31mO+DHBW44ttdSFoPfRo0eXehjSvyAQGAKxIU1UThwc3FsThYKa7tSb&#10;bh+v36TJNcREEhAIX+rC8VNCo0RNL/VKSP9BIRAL0sSW2LdvX+0tj0rhnDnOqCVLlrgekp+kCVEi&#10;9XIfUlTK1KlTVdOmTYtOzxeVecg4BIFsCMSCNPFI77vvvq4zsQe95HjTyfTuVsLzkzRRy3v16hX0&#10;VF23TzLnW265xfVz8oAgEHUEIk+a3GlDoDb31ESxcBWv2xRyfpEmavk+++xTtG01SDzJ9M6VI2+8&#10;8UaQ3UjbgkDoCESeNFHLzz///NCBcdohCSuQgiEwp8UP0jRn06Okltvnb1LIOcVF6gkCcUAg0qSJ&#10;t5yrJ9xekBY28KjpDRs2dHwhmx+kOXjwYNWzZ8+wp+q6v7PPPluNGTPG9XPygCAQVQQiS5qc9uHK&#10;iah4y/MtIIl+OZt+xRVXFFxnMp5z0qdVq1bq7rvvLiqRL0cwCWKPQzwkR0TJtiSZ3gtuDakQEwQi&#10;S5rkmSSIPczjgF7W7K233tJE9uKLL+ZtBhvfz3/+c1WjRg2d/dzt2XlCr8gMz31IcSl407mQjcQn&#10;SSlIz3IzZ1JW0908Ikma2OkOPPBAhTMhToUrLlDTyWSUrxx++OGaNPF8uy1DhgyJRBC7m3EjiXOt&#10;B1eBJKUwHzQFKeWHQORIE285anlUveX5tghJN0jzNnz48Lw7CW87pOnGeUSDJOuAcMEobgU1HUk8&#10;CWo6+JO1HhOLlPJDIHKk+Yc//EFnYic5bxwL9wThvCJ5Rq5C2rZddtlFkXzEaaEuUuyTTz7p9JHI&#10;1cOWS/b5Yu+kj8qEsCnzo8c1K0QxSCkvBCJFmkuXLlU77bST6tKli5bW4vrihM5JJ52UJgdCb6xz&#10;6datm5a68s3PTqhcYkY+z7hiwrgxLSChkTYvLgW7JclZeHEdCWXUqFGaNHnF+UcsLmsQtXFGijQ5&#10;WXPHHXck4mW9cZJcmm7nZfeMr1ixwnUbbvsMqz65QuNQWIM//elP6s4771TXXXddmuyvvvpqHRHA&#10;UdGHH344DlORMfqIQKRI08d5SVOCgGcEiNzghQ3THhWBJgGhSik/BIQ0y2/NZcYuECAk7Iknnqj2&#10;hJCmCxATVlVIM2ELKtPxDwHiSrlaxVxvYnVOCmn6h3PcWhLSjNuKyXhDQ+Cee+7RCZWx9V555ZUZ&#10;t4gKaYa2DJHrSEgzcksiA4oKAsSUvvrqq+mX9XSakGZUVin8cQhpho+59JgABIQ0E7CIRU4hEaRZ&#10;KMAY21TUMyUVuX6OHiO+sFAgfdyOrDqaeICVhDQDBDfiTSeCNEleQRxdroJaRQYi7gSnPP3004ok&#10;EoSR8OIM+OLFizMe5+5wMsabOtZ/8ab269dPbdiwIeLLu3V4bdq0UaSSy1eeffZZfT0xl9ZBoNxh&#10;buZMMLr1TnPO2BOwnw0b3uvevbtatmxZLLApdpBCmsUiF//nEkGaeDc5umhObHB64+23385YHcjg&#10;qquu0u8NGzZMG/g///xz/eK8uD1rEImFOS9NijqO/mHfQlrl+l2OQZ588smKs+ZRLz/88IM+smnN&#10;yEOwPZfCWbMO8Tc/LEjtnIlv2bJlGp/bbrtNkXXKFAj41ltv1Z8Tq8jn/M3ZeP7lkre1a9dGHRpP&#10;4xPS9ARfrB+OLWmSCYlTN7w4lcHRRPN/Tmx06tRJ55skzs68zxW3fPkhzfHjx+vL2iAKrp2dN29e&#10;xkJCmps2bVIkDO7cubNug7vKSaoLcUY5WcPrr7+enjPkyC2eBgP+feihh3RCFO5wpyBl8/6ECRN0&#10;QhDCbMgPCrkeddRR+iSMNSMTpMk1xhTuOZ85c6Ymz9q1a6v169er+vXr6x+bJBchzSSvbv65xZY0&#10;rdPCXle3bt28MyVdGwkvIEFDmlxI9uCDD+YkTU6CQMCEnEDSECbtRJ00rUAMHDjQ0cmVn376SfXp&#10;00er4UiakObjjz+uLrroopykCclCmPxocSWJydokpFm+hFIOM08kaWZzemCvg0AgBytpImHlkjTn&#10;z5+vj9FBBnvuuad+DpUd9TzKkqbZuARj2wmM+ZMNHynaWvhh+O1vf6s+++yzDNLs0aNHVtLEhoxU&#10;STuYLzBxLFiwIK2ei6RZDvRRnnOMLWliozMqJySAasj/cfA0atRI/2sKd5OTxquiokJnUDKk2aJF&#10;C/XnP/9ZnX766VnVc+xyjz32mHaiIKHiSDryyCPV2LFjY0GazA0iBJeRI0fqLEPTpk3T80BCNIVs&#10;8KeccopOFNysWTM1a9YsLWkyT+4hYv529XzcuHGaJMn4g7Q+ZcoUdcwxx2hbJ1K/kKa/hILNGZs8&#10;6yeltAjEljSdqudcQ4H9EkkT2x1kgafY3KW+44476vRepKWzFmyaZOPBJsqGHTBggP4/JQ7qOfZc&#10;kjmbhMVIktmSI2NuIO3cDTfcoD/nMjvmSe5LrhshlRs/MlVVVeqTTz7R84dEue7BtE00gtW7Luq5&#10;/19qIjbQdljT7777zv8OpEXHCCSeNI2qbkjTIIM9jgzqNWvW1G9xZM5OmqiekOntt9+ujFQaF9JE&#10;okYSNCUXaRp8kBytpLp69WotObZr107bc3EucTrGkCaOoI8//lj/CKGa41RDXacIaTr+/jmqyI/V&#10;QQcdpKMTyMPKv1JKh0AiSRMiwBa3bt26NLJW0ly+fLlq27atQi2FNJEkUUettlAkTciCDOy8MAHg&#10;PedvCCPKNs1JkyaphQsXZuwqK2mSt9R+ltqcr+YhPsexg0RNFn1wQL1HQjekiVfeYMPnXbt21bZP&#10;3sMUIuq5f19q9hrSPoWbDazhX/71Ii05RSCWpElIjDWE5pFHHlGTJ0/OCEGC2Ky/yJAmKjlSI7Y+&#10;k7mGmMI6derojPHWsCNIE2kLx4j9BUkQhhTFgrpNYDkqthUjSH/69On6PUKOwAdHlylG0iRe1Rq4&#10;z5cUe/EvfvELLZkb0sTemQ0b3iNMqdDlclHEzs2Ywgo5wo6JlmMK2gMJkLds2eJmuFLXRwRiSZrF&#10;zB+SxHNstwcRoM4GtGdKNzGe2fpCMo37PTf2eTEfvO3gY71WGMzAh5dJWEHAfL75xyHov5g9ZH0m&#10;DNLENNSgQYOM7EqMAece9xRJKQ0CZUOapYFXek0qAkGTJifYIEzrtSkGS06ziYpeup0lpFk67KXn&#10;GCMQJGkiyXOoAGK0HnU1cBERgmPIrh3FGM5YDV1IM1bLJYONCgJBkib25qOPPjpvZqrjjjtOOyel&#10;hI+AkGYAmGPTwxYoJbkIBEWaq1at0mFF77zzTl7wUNE5jCAlfAQckybZb7jP282Ls8xuCunGaJ9T&#10;N276iVrdAw88UB1yyCHi4XSz+DGrGwRp4og79thjdYRHoYKKTvYqCXQvhJT/nzsmTWwr5Fm0v+6+&#10;+241ceLErJ9hzHZTCAWqUaOGPspHards/UX5PdLHnXjiiTqtGvNIetiNm7VNWt0gSJMsXB07dsyI&#10;XsiHGwlkyFYlJVwEHJNmrmER9EzCXj8KpMnGIc6P1G5xCuvBeM/pGc5ok+BDSNOPHRHdNvwmTZLA&#10;YMfkwIXTwjn0c845x2l1qecTAp5Jk9MKhx12mC/DgTRJxYaEisrNr671MitfOgmgEcidc9pkUWK8&#10;nKQR0gwA6Ag16Sdpkhhmn3320Znw3RQIduedd04f1HDzrNQtHgFPpMmZWE7SkPRixYoVxY9i25OG&#10;NPkvJ0tIiNu+fftIEycOH1LLNW7cOK1WCWl63gqRb8Av0sQmyRFeNLZiCio6TiEp4SHgiTTJ7I1E&#10;xYvzzl6LlTRpi/PLRuKMoqrOmPBgktjCZPxh3EKaXndC9J/3izTJeE9bxRacp4QfSQkPAU+kCcnh&#10;JebF316LnTStEidnvfEuRqUYGyaEyb041iKkGZVVCm4cfpAmCVD22muvjB9ctyNGUiUvAKFKUsJB&#10;wBNpMsSLL75YZ7jxo2QjTdrFxkkYEqckIKtSF+IwMRtkI0yRNEu9OuH075U0yahFCj23dsxssyNR&#10;9M033xzOxKUXFQvSZJ3I3QhxQtKlJE5DmMTTYXfNVkTSTP43yytpkpqQxNbW5CjFokY2L5J4SAkH&#10;gdiQpiFOwpGQOEuhqkOYHTp00AHI+e48F9IMZ/OWshcvpEmmfNK9+XV2HCGCdHEEvEsJHoFYkaZV&#10;4iQzedjOIYL4CxGmqOfBb9oo9FAsaZL9niuVzdUpfs2FzPk4laQEj0DsSBNICHUiHAnnUFhxnFyH&#10;gZcyl0puXSqRNIPfuKXuoRjSJK6SmztJBu13QUUnoXYpTVd+zymq7cWSNAET9Zig+jAC4LlkDKeP&#10;0yschDSjut39G5db0sR2iTTIIYggCqp+vXr1Mu6FCqIfaVPFxxGUbbGQ+iBOJM6gsgpBmBxvs4cV&#10;5ds8QprJ/2q5JU3OiOMtN9esBIEQpMxVJVKCRSC2kqaBxUicQTiHUMlzhRUJaQa7MaPeuhvS5Ae3&#10;Vq1aOglNkAVHEGnlJPNRkCjHXNI00GDj5OSQn+FIU6ZMUQ0bNnSskotNM9iNGrXW3ZDm8ccfr++V&#10;D7pg30crsl7dHHSf5dh+7CVNs2jcuQ1xEmjv1RjOVbTEveULKxJJsxy/Ltvn7JQ0icXkqG22ayuC&#10;QBBy7t+/fxBNS5vbEEgMaTIfK3EWG8cJYTZq1EifQiq2iE2zWOTi85wT0uSqaY4Yk8UorIKK7vVo&#10;ZlhjjWs/iSJNQ5yEI6Gqu3UOoZIjYXohTMYgpBnXr4PzcRcizY8++kjH9HK+POxCvw8++GDY3ZZN&#10;f55Jc+zYserWW2/1BbBcZ8/dNs6RS7zqOIecxnHiJceG6ZUwhTTdrlY86+cjTQ5dcHKsVNfsoqIH&#10;FdoUz9Xyd9SOSJMYMxILzJw5U61cudKX87LZpuEXadI25HfooYfqcKRC53tJr4UB3UnguhP4RdJ0&#10;glK86+QjTQQJTDzFmoi8IoPQsPfee+e9zdJrH+X8vCPShMwmT56sRX6kMf4OovhJmowPRw5JPjhy&#10;mUtVhzCp4zRw3cm8hTSdoBTvOrlIk1skycL+wQcflHSCZ511lqjoAa2AI9J85pln0t0TzhBURhW/&#10;SZNBI2X26tVL2zjtZ9WnTp2qzwH7SZiinge0UyPWbDbSRLLkJlJs46UumJu4s0qK/wg4Ik1rtyQc&#10;QOWlzJ49W9sN33jjDS3Nde/eXSdDHTNmjOKXbsaMGa5GHARpMgDCPbj8rVu3bmmViU21++67q7lz&#10;57oao5PKImk6QSnedbKRJkd62WdO7ehBIoCpabfddlPvv/9+kN2UZduuSBNJDYntn//8ZxqsmjVr&#10;Kq6uRf2FOHmRQo07g+rWresqZjIo0mSwjP3SSy9Vv/vd7/SVw7Vr1w7Msymkmfzvkp00H374YZ1E&#10;xi+7uB8InnnmmVqAkeIvAo5J89FHH81KMpCmKbfddpsaMWJE+v/Ei7k5axskaTIopEvIkmtPkTrd&#10;hiQ5hV5I0ylS8a1nSBMBgXvur7vuushNZtOmTTpmc82aNZEbW5wH5Ig0IcwzzjhDqx7mZe5nLkSa&#10;bs7BBkmaOHxQx7lfGrI0ds4gVCkhzTh/JZyN3ZDm5ZdfrpDogthHzkaSv1ZFRYVIm34AaWnDEWk+&#10;9NBD6oEHHsh4kYoKmybvP/nkk+q9997TIUmoKeaSp0ceeURBuE5LUKRJrsFddtlFcULDFDZ5jx49&#10;tE3W7yKk6Tei0WsP0jzttNPUwQcf7Lsj0c/Zzps3T3H2XYp/CDgiTf+6y99SEKS5evVqfRVALoeP&#10;IU4/zwYLaYa1Y0rXDxIcV1cjOES5oFXtv//+clulj4uUaNJ89913VYMGDfJ6yAlJgjixcfp1fYaQ&#10;po87NIJNYcfkMMRVV10VWbXcChsO0MGDB0cQyXgOyTNpkivQTYLefDD5KWliIiAG08nZX6RMbJyE&#10;TPlxikNIM55fBiej5kcWhyfq+ddff+3kkZLXQcvab7/9YkHwJQfLwQA8k2YY9547mEdGFQiTs+dz&#10;5sxx/ChSZu/evbXU6ZU4hTQdwx67ikuWLFF77LGHOvfccxXZ2ONQcMbuu+++auHChXEYbuTHmDjS&#10;hDCJl7OeYnK6CpClIU4vqrqQplPE41Vvy5Ytiiukn3rqKS1pxun2x2uvvVbfsy7FOwKJIk3O/SJh&#10;FkOYBkrI0qjqxcZxCml635hRbAGtisMRlLiRZlVVlb4Kw2uC7iiuS9hjSgxp4vQh4asXwjTgQ5bG&#10;OVRM/J2QZtjbOPj+7rrrLn1fFKF2cSRN9jTOK1HRve+VRJAmJx7wkhO47lfBOYRUwfUZbouQplvE&#10;ol2fsDX2l/X48KxZsxT2zTgVVPT27dvHaciRHGvsSZOrBIjDtAau+4m0IU43cZxCmn6uQGnb4iji&#10;iSeeqCZMmFDagfjQO4l1OFbpV7SLD0OKZROxJk0jAQRFmKwoISYQJ+FITm2cQpqx/C5UGzRrP3z4&#10;cH1M0h5RQY4FpM04FeZD7lhO7UkpHoHYkiZOH+Iw/VTJc8FobJw9e/Z0FAAvpFn8hozSkxwPJlTH&#10;5Fmwji1ujiAzdnJ9iorubZfFkjT98JK7hQ2vOqSJZ71QHKeQplt0o1efFG8kFH755ZezDi6upMmF&#10;bwcccIDC7FCu5Z577lG81q1b51h7tGIVO9KEMAmdeOyxx0Jfc8jSEGe+OE4hzdCXxtcOsV+3bt1a&#10;XXHFFTnbhTRPPfVUddNNN6VfqL8Eklvf42/SIxKFYX2fLy2FRDfW9/HSU7jnx/o+p+UoSL329nk/&#10;W78c92Qu9vqNGzfWyXXItWn9jOthKOPHj0+/z+knU7jmxtQfPXq0fptoAnv7hDWhndnf37x5s37G&#10;3q/JQXrHHXdkPAOpkc7R3o7X/7dt21bnDeBFRAEOMjfJyGNFmoYwBw0apDdDKQrESTgS5JnLximk&#10;WYqV8a/Pa665RhMipJOtPPHEEzovK6TKzY/mZUjT+h5/G9K0vl9ZWZkmTev7xuEEaVrfNze+Qpr2&#10;9g152d83pGl/n7b4UYD4rJ8Z0oS8zPuGrOlj0qRJ6fdvvPFGPX4C/u3tG9K0v29I8+abb854xpDm&#10;uHHjMt6nb0jN3o7X/5NP15AmNl4OtDg5bm32QmxIE6dP/fr11cCBA0tGmAY0pEwcQ7yy3XQppOkf&#10;gYXd0tKlS3W8L3G/2Qo/3ORljfO94hDyrrvuqj788MOw4XXcH2klIbQgruu4/vrrFT9azN+pc9c6&#10;cE+kSSB58+bN9RUX5pfTMSpZKuZK2IEKgx2GY2DFBJt7GVOuZ5F0ieE0J0Ss9YQ0g0A8+DaReI44&#10;4oi8XnF+JDldE/eCpGmVIqM0H0xvnOwr9Y2euTDxRJp44hBzf/azn2kR2mvJRpoQJhla+vfv77V5&#10;35/nC2SI00rmQpq+Qx14g6wlJhdzaaC9Q/JmFnPQIfCBF9kB311sm1ErmD5I7IzDyq9CImYy7CPo&#10;cJsutzh4KZ5Ik2DZHXfcUZMm2dG9FjtpEriOhBlFwjRzNcRptXEKaXrdCeE/j70Ob3k2TYbQI651&#10;iVtcZiEUa9WqFSkVnVseCCPEfOBnWbZsmb7DCVsqp7o++eQTT817Ik165oQBV0n4UaykiTG5RYsW&#10;kSZMM2fsIqjpGJSxdwpp+rEbwmsDu1mdOnUUP9L2QhwwNswkBoSzX6OiovODhGkEj3kQBWmzU6dO&#10;vjTtmTT79Omjzj77bF8GY0gTr1+HDh0iZcMsNEHjHGIjkhQBs0VcktQWmluSPycaomHDhjqxcLay&#10;cuVKfQdWEgseY46IlroQ/kTKvSBtmOSn8Os+MM+kiUjt1UZgFg3SHDp0aJowSxVWVOwm4guIR/2E&#10;E04Q0iwWxJCf464f7Jh2LyqE8tZbb4U8mnC7w7REQuXFixeH27GlNwiT7FFBeMnphvjVr776Ssdh&#10;tmrVSp+737hxo6f5OiZN4rEwptpfffv2Veedd17Wz9x61CFNVH0WkljMbP1F/T08/IRziKTpaV+G&#10;8jDRH8cdd5xav359Rn84I7D3kWw46QXNiO9UKQoqOXGYQREmc0Itv/POO7WtmvhbP8wsjkkTVZMr&#10;S928rrzySldrAYhu2o96XZN70RUIUjkUBIj7JayFL5W1QJT86HFtdTkUJOp69eqFPlXiMLFhBqmS&#10;BzUpx6QZ1ACkXUEgbARQxTlKd9lll1XrumPHjonzkufDFyywJ+K8DKs8/vjj2kselNMn6HkIaQaN&#10;sLQfOQQIP7E7QHAUZDvdFbnBBzCgkSNHZv0BCaArhQ+EOEy/w4qCGGuuNoU0w0Rb+io5Ajh3ODf+&#10;xRdfpMeC5INKzqV85ViIEOCIcjFHCt3ghfkNk4ifgetu+verrpCmX0hKO5FHgMQZnC6bMWNGeqwc&#10;2YNEy8WGmW2RCJdDRQ8yeB8HDH1E+by70w0spOkUKakXewS4q7xfv37peeCoO/7448uaMA0YV199&#10;tW/B3/aNAmESVhRHp0+2TS+kGXsqkAk4QWDq1KmqSZMmOmaPYv4llE6KUqjonIry+/6goOMwS7F2&#10;kSJNErPmOplRCnCK7fO1117Ttxd+8803xTYhz/mIwPLly7W3ln8pxGFyGZ/Vruljd7FsCnsmx5YJ&#10;BfKroO4HfdLHr7G6aSdSpJkrNZybCVGXoGXUMF4rVqxw+7jn+nL23DOEvjWAJHnyySfrZA2Up59+&#10;Wv3yl7/UmculZCJA0hK/zmfjJcfpE9ewonx7I3GkiXeUtPwE47/wwgv6hJHJSB3Wl0RIMyyk8/dD&#10;CBE/nJxY429U0J122kmHvUipjgBOGq6SMaaLYjHCuUZYkf2kVbHtRe252JCm03PoZN62xtvxy2kS&#10;LnCUis9QRUx4Be/hPSw2Rs+0aV1YIc1obPP7779fq+FWuyWmEym5EeCUnZes9KjkZL6Pcxxmof0R&#10;WdIk0xFXC5DcGK+n/bhboYmZz0855RQtaS5atEi1bNlSTZ8+Xaebw+Y4ceJExUVW1OFua6fEbO37&#10;lVde0Qk6brnlFvXSSy8pUtoJaTpdneDqkYWdXKzkUkTyYX2kFEYAFb1p06ZF3ZCAl5wrKpKokluR&#10;ixxpXnDBBVqdYsOby4922GEHfdGVkxdeUlO4yY7TH6bgPTW3zrHAZK5GUkTqJHGDua6V3JhO+qIO&#10;hGvGyb9INo0aNZKEHYW/n4HV4MeP2yJHjRqlVXESwPiRqCGwAUeoYVR0Av3dqtbEuZJ8IwlxmIWW&#10;I3KkScYVyMvcCMgC7rzzzjozPO8XeplfOTzxXMJmzcQNaRr1DHunNQ8oaokhTbI7F+rHfI50uc8+&#10;++isOGwavqhkd5IsR4W2XnCfjxgxQp111lnqvvvu04RZzoHrxaDMd+H22293/GjS4jALTTxypEmQ&#10;rbXwy0XmGRL7Oi2kmuIZu7rtlDSd9kM9joSRKcb6yyzquRsE/a374osv6h8x7pbCpCMSpnt8iTBA&#10;RXdi5ycKAWEhKYHrTtCKPGk6mYS1Dt5zPHfWF/dTv/3223pxsWkSwIstk41hrmrl1xUJkaSlXouQ&#10;plcEi3ueuEvSjZG9yGui2eJGkIyncIxyxUchFd1cUVEOKrl1ZRNHmgSUs9jWFyEUEKV5D2I0f3/5&#10;5ZcaD/M5G8ZrEdL0imBxz7dv315LmYQVIS1JKR6Bbt265VXRca5xYCDuyTeKQShxpFkMCH4/I6Tp&#10;N6KF2xs7dqy2I3M7Kl9oKd4QmDNnjnZqZiucGoIwvd7q6G2EpXs6UqSJdJAEoz0qPxfD+aHql25r&#10;xKdnTC98wXH8BZmpJz6I+DPS3XbbTS1ZsiSjMWzEmEAKqe7+jCCarUSKNKMJkYwqyghw8ovzzW7v&#10;o4rynKIyth49eij8AaaYsKKkx2EWwl9IsxBC8nlkESC+lov9CEkL87qGyALi88AIzzMqOoR57LHH&#10;lkUcZiEYhTQLISSfRxYBwl0gTAkrCmaJCNlDFR8yZEii8mF6RUtI0yuC8nxJEOCKCrzknBbDYy6v&#10;YDAAY5w+5RZWlG9TC2mW5CsvnXpBgIMLSJhjxoxR9957r7wCxqAcw4oSS5pDhw5Nny0nUcawYcPS&#10;xyStk8b2xR3shFFQ1q5dm+H9e+6556qlwyKsItdFU5s3b0635eXLL8+6R4AEwkg/SJpSBIFSIBBr&#10;SZMkG2RB4pw5hHjJJZeoLl266L+bN2+ecYEWjgKOUVIg1969e+tkDry4J2batGkZ+O+777769BCf&#10;t27dWufoNPVHjx6dyIzUpdiAbvoEf/KjSj5MN6hJXb8RiC1pckzuoIMO0sTYtWtXfQJkypQpipRy&#10;ZDpCIvnxxx/1+VlDdiTTuOOOOzRpjh8/XpEGi3+zpZ6DNDdt2qTP1JIfkPg/zsWTgQkCbtWqld9r&#10;Ie3lQQDJn6QoEocp26TUCMSWNJH2kDQJhejevbtWv81JEEiRTDcEPZsCebZr106RLs6QZq9evXTC&#10;1XykOXz4cDVhwgRNoEik5M8U0gx320KYZCuSKyrCxV16y45ALEkTWyO5MNu0aaMlD/JfduzYUS1Y&#10;sEBLleTkhCAhO1MGDx6s6xv1HAmzEGk+//zz6tNPP1VvvPGGqlu3rpZg6VtIM7yvE4TJveRCmOFh&#10;Lj3lRyCWpMkX6c4770yTJtmLIDUkSyRCI0maqfMeYRMkIYAszeedO3fWBJhP0iS35oABA7T6z8kT&#10;yBo1XdTz4L9aaA5k2zGEuWbNmozzzmS0kiIIhI1ALEnzzTffVPPnz0+TJnZGSBOCa9asWQZpcswO&#10;FRuyI/EwBMitl9wfg8pHnB+pxHjWWrBpkvgYTztkfOGFFyruH+KLKpJm8NsUm3TNmjXTV8qyBtde&#10;e622Q1O+/fZb1bZt2+AHIj0IAjYEYkmazMGQZkVFhc7QDWmS1o2M01ZJky8bF2uRL3Px4sXp6ZM4&#10;GBWe61wpdo+scQRBlHjOUe/5myKkGfz3yB5WRAZ+ElGzFhTWHPu0FEEgbARiT5p4zrlEC9I0xa6e&#10;876VNMm5yUVqnK1FmqFAvtZiSNNINYQ10YaQZvBbFLU8Wxwmkr9JLkz4mCQaDn4tpIfqCMSeNLlT&#10;CBKENFHZ8KIjPdrPIxvSRA0nhdjy5cs1Gpws6dmzpw6YRiU0xRqnaUKW8LTzN557sWn6+3Xihw8S&#10;xF6NHTNbQdqnDhInP3pSBIFSIBBb0sSjPWjQIG1z/P7773WIEQWnz+zZs6vdb4InnLuDCHy3ZmfH&#10;446qhxfeegkb5EicZ7ZClnfsqlL8Q6BPnz7qqquu0p7yXLGYI0eO1BEROIaIuZUiCJQCgdiSZinA&#10;kj6DQYAbPTGTkHkd4uRHKVtBk+B6Ek6BrVq1KpjBSKuCQAEEhDRli5QUAdKPtWzZUhMmxInEuWHD&#10;hmpj4tI0U2655ZaSjlk6L28EhDTLe/1LPnvMIBAmxLlo0aKc4+HwAmYYoh7kGpGSL1tZD0BIs6yX&#10;v/ST5xpliNN+R719ZMuWLdNOIiHM0q9ZuY9ASLPcd4DMXxAQBFwhIKTpCi6pLAgIAuWOQHmS5upx&#10;qknF1oTEUgSByCBQ1L6coypqVKjid7PX5yODXmgDSSxprh7XRDsYsr6ajFPjKlKfpf5dnQdq2mgy&#10;LksNNneNJirbRzSX87nQllU6iioCfuzL7XNbrcY1yUOYcyq27/+8QkKKOAt8F6KKZynGlVjSVKkN&#10;k94nbB7bpmHz5ttHenNnq5DeiLlJk4WckyLlrIRbilWWPqODgMd9aZ3I6nEVOX+4U7/c27Up/SNf&#10;YD+m6lTkkgKig14kRlIWpDknda68AskSyXPbL2pe0sxCspmrhUqTnzQNcYoVIBL7PDqDsJCm632Z&#10;MYv80uHq1Mk3q46khYC80mQBqTU6CJZ8JIknTU2OKdJsol/bLT+5SZPNUyOvFJqSIx2Rpv61F7Wn&#10;5Js8UgPYRpru96VtFql2XGkyCAIOzFHyI194tySYNFN2y9Vz1DitcvCrnLJPWg3mVjUpU+fJrJcV&#10;Q4ek6ZRcC6+T1EgKAnOK3Je2+bu1mzup76ROUpbByzwSTpopaLQ9xxjLIbvtzqGsv9QOfpEdS5op&#10;Bamw1Opl+eTZ2CGgSbOIfemJNJ05eoQ0ne2mxJJm2kupVZKt5JXh2Mml3ghpOts5UqsoBIrel/be&#10;XKjn2E6dhCQVco4WNeEEPpRg0rR4Frd5vDMkSyHNBG7n6E8pw+PtZl/ap+YwptMNEc6pKOzcjD7C&#10;wY8wsaSZbQOQSzNdcpFmhjqfawHEphn81kxmD0XvyyxwFJQg7Xs8lx1ft53a0+IFcrTpEkqa29Tx&#10;XMHtqXChitSvanbvYwE75LaYNxM0n3efiffc0SYsn0pe9mU2lIyjs/pn2YPoc6vpq8eNc6TCl89a&#10;5Z5pQkmz0NIWIEZHds1CfWwNcJcf78I4SQ2DgHvHoRv1OyfOLuyjslZKlSlpFl76nCeCCj+6VdmR&#10;E0EOkZJqXhHwZIt0aBv1OsYkPS+kmW81i/wFltCNJH1FZC6CQCYCQpqyIwQBQUAQcIGAkKYLsKSq&#10;ICAICAJCmrIHBAFBQBBwgYCQpguwpKogIAgIAkKasgcEAUFAEHCBgJCmC7CkqiAgCAgCQpqyBwQB&#10;QUAQcIGAkKYLsKSqICAICAJCmrIHBAFBQBBwgYCQpguwpKogIAgIAkKasgcEAUFAEHCBgJCmC7Ck&#10;qiAgCAgCQpqyBwQBQUAQcIGAkKYLsKSqICAICAJCmrIHBAFBQBBwgYCQpguwpKogIAgIAkKasgcE&#10;AUFAEHCBgJCmC7CkqiAgCAgCQpqJ3wNVqrLGEDV3o5noRjV3yNb/V1Va348hEBvnqiGVVTEcuAw5&#10;zggIacZ59ZyMvapSmeuGawyZqzam/l9ZBXHW0P9uTJNplsasz1Yjp61t6LZpt9BYrG3xzLb2Ns4d&#10;kjk+WzsZn9uvZE71O7fSYf+FxiefCwIOERDSdAhUPKulpMwUOUE8QxAtjWTGvzUqVX4ZbeuzWwvS&#10;KiS7HYWqFFnpNlNFE1tBiW97e9Snuh5XZWV+aVGTvBlG6gfA1o9pK57rI6OOIwJCmnFcNUdjhuhs&#10;xJgSK5E0kQ4N4eVsirqWDyHJNF9VI90sfVVr2EimKcKsnJsm7I1VKVLPNyYLaValCLYSydIi3Qpp&#10;OtoMUslHBIQ0fQQzmk1tlRLTKjrMlyKigqRpmUw1SRKSy1DJjbqfHwGI1054aUmTNrNJq9tIU5Oj&#10;lkozJVMhzWjuuiSPSkgzyaubkggrjQcow6ZYSDU3oFjslhZCq66OOyNNq6oP6VbNrUw5pFKkvs3W&#10;mpXIq1J2y1SduXoe1E2p9FYJ2qq+J3ktZW6RQUBIMzJLEcRAMkmvaq7VYZP6LKUmF3TgbDVapogq&#10;nw0zN2lWd+RsJ+yNqfFUQYT5pF9NmmYM5tlM6dmN1BwEytJmeSEgpJnk9d5GdmnVPMP7nJLY8H4X&#10;dOBsBShDuixGPd9mEjAkCtFp0jSSYg6JMU262hyw7Ucg0yPlytSQ5OWWuYWDgJBmODiXuJetjppK&#10;u6S5LV7T0eCsNke7I8iBN17bM7VKvt0JlPbma9NB9pjRjVqF3zbCbA4jl/ZZR3OVSoJAHgSENBO/&#10;PbZ7ttPqubFvOomv3CpnpgPiDVz2kKO8KrKxrep/Uy8IMkWGGc6kHIHq2QLwq1Ie93QR0kz8Do7a&#10;BIU0o7YiPo5nq2pr7IAubJhGHU+r89mkwOxOomrDhwyrkTNjwaGz1Zue20Jg6cMe2K7/j0d9Wwyq&#10;j7hJU4JAPgSENBO6P9IB7dtFsq1e6pDnq+Mw8/ZpC4lyEkOabs+N1z7kiUt3iUVASDOxS2ufmDtJ&#10;s2xgkYkKAi4RENJ0CZhUFwQEgfJGQEizvNdfZi8ICAIuERDSdAmYVBcEBIHyRkBIs7zXX2YvCAgC&#10;LhEQ0nQJmFQXBASB8kbg//5+VXl6o7ypAAAAAElFTkSuQmCC" style="vertical-align: 0px" width="333px" height="207px">
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                </p>
            </h4>
            <a name="37"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">30.</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）</span><span style="color: #000000; font-size: 16px; vertical-align: middle;"> </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">在图</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">(2)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">中用斜二测法补画完该帐篷的直观图；</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">【解】　</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">1</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">）</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                    <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIwAAACSCAYAAACXHCLVAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAEF9JREFU&#10;eF7tnVeMHEUTgP0jQIh3BC+IF7CQkCyBEDnnnHOWMQiDkeAIIoPIYLLIByIaY9ky2OTzASZH+whL&#10;TibDkUzO1L9f3/bc3KaZnp2d7Zmplk6n3e3p6an+tqq6urr3f1It47SoBOJKAGDyUgYHB/PS1cL2&#10;c1xenuzXX3+VrbbaKi/dLWw/cwPMvHnzZKmllpL333+/sIORhwfzFpg777xTzjnnHDnrrLPkzz//&#10;lNNOOw1fS66//vo8yLWwffQSmP/++0++/fZb+fLLL+Wdd94xwj/66KNl2WWXNRBp6Z0EvAQGceCz&#10;vPTSS4Fkvv/+e1lhhRV6Jym9s5GAl8AAyzPPPCO//fabfPTRR6ajCowfxHoJzIcffigXX3yxTJ06&#10;VZ544gkFxg9W/NUwzeSjGsYParzUMAqMH3A064UC4+/YeNkzBcbLYfG3UwqMv2PjZc9KD8xff/0l&#10;jz32mPBfS7QESg/MtGnTZOmllzaR5N9//z1aYiWvUWpghoeHZaONNpJTTjlFJkyYINtuu6189913&#10;JUei/eOXFph///1XDj30ULnyyivl888/lw022MAECddff3157bXXFJoWEigtMLfeeqsceeSRgVh2&#10;3313WbBggRBl3n777WX27NkKTRMJlBKYV155RZZffnmZNWtWIJL7779fTj75ZPP666+/loMPPliu&#10;uuoq+fvvvxWckARKB8wff/wh6623nmy22Wbyzz//BKL4+eefTUafnS2Rg3PiiSfKCSecID/99JNC&#10;U5NAqYAhz+b444+XAw44QBYuXNgAwcSJE+XRRx8N3qf+NddcI7vttpt88803Ck1VAqUCBr9k3XXX&#10;lR9//LHp4D/44INyxBFHNHz25JNPGqf49ddfLz00pQHmk08+MVPoNdZYQz799NOmA49ZWmuttUzy&#10;Vn0hL4dp98yZM0sNTSmAIRFrzz33lJ133lnOOOOMtgN+0kknCQnnzQpxm0mTJskll1wyxv8pE0Gl&#10;AOaiiy6S8847z8Rc0CLtyvPPPy+HH354yypEg08//XQ59thjW5q2IgNUeGDwP4ixPPvss4ITG1WY&#10;JWGWSNhqVWjniiuuMBqLKXiZSqGB+eqrr8z0+ZFHHpEdd9wxthnBLLHNJarMnz9fVl99dXnrrbei&#10;qhbm88ICQ+gf03LPPffIXnvtZaCJW4j4opXilA8++EA23HDD0kSGCwvMTTfdJFOmTDE7D26++eZY&#10;5sgCgsnZZpttzN6oOOWLL76Qgw46SM4///zYWixOuz7WKSQwLB5usskmZuU5aeyEXQt33HFH7DED&#10;TIKCQFrkyHDhgCEoR5Dt7bffNprlzDPPjD3o4Yrs4d5yyy0F0xa3oJmYie29996FTZMoFDAMbl9f&#10;n9x1111mloPDmzSkT1toqffeey8uL0E9jiVhvSrJtc43y/iCQgFDFPbAAw80/gqDxQp0J+WGG24w&#10;GiNJITK88cYby3333Zfkcm+vKQwwDBAaBb+FiGyr8L/LSODMbrfddk5mKdw+TjPO8OWXX564DZf+&#10;ZlG3EMCQikDS0wsvvGC0yyGHHOI0jW4laNpiSv7uu+8mHgvWpUgBPe644+SXX35J3I4vFxYCmFNP&#10;PdXsw6a8+OKLst9++6U2vb399tuDxKqkg4Y/dO2115p+5T0ynHtgnnrqKdlnn33MoUMUoq+daIR6&#10;KMj3XWeddYL2k0LDdQQP8WsI9uW15BoYfJW1117bJHFTXn75ZXMIUdqFZPFmCVdJ7kPOMGtVDz/8&#10;cJLLe35NboFhkXCPPfaQhx56yAiRYBnwdEPlz507V1hfSquwYr7vvvuaI03iLIimdd802sklMAiZ&#10;KS+5LVbgl156qVx33XVpyKShDWAk35dobloFZ5j+2+WLtNrtdju5BAbHduuttx4z62B1uZuzEGZe&#10;VpulNSjAzpoX2iaNMEBa/WrXTu6AQZ0Tsn/zzTfNczEDIeel26r9gQcekMmTJ3dlTGibFe88pEnk&#10;DpijjjpKmOraQuxl11137fr+ITLtWCqIythLShSRafKNW6WHJm037etyBcySSy4phx12WBBjQbuw&#10;0MfGtCwKjq9LXo1rn3744QeTxeezM5wbYDBBbBFZvHhxMA44o3fffXfXzVFYm+HLdNP84YexeY5k&#10;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" style="vertical-align: 0px" width="140px" height="146px">
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">3</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">分</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="38"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">30.</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）</span><span style="color: #000000; font-size: 16px; vertical-align: middle;"> </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">求该帐篷内空间的体积</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">V</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">；</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">【解】</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">）</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">V</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA8AAAAqCAYAAABstwHPAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAOJJREFU&#10;SEvtVkEOhCAMZH/mm/jGPoGzyb6B+AcvPMAzJ37QtRCyQihgI9HDNuFgZDplOkVfsIfgBoK5IbhA&#10;X/EAsAMt3yDkCraSvcC8gRI7ENd5cKCyev6DC6qTfb5PMKNYfT6YxJtlBk3YbIS3+8blAczT9AmT&#10;1Fi4L8YDyu7TN911ddmpw5Sha8qYEbhA3O8n6/Ccp0nB1mW3JSbjetuuINVG1k0LhkDO1RvOGt32&#10;06B+5uxtTCK1K5be6HP47DDBAHgdUb2uMzdES8FmSUeSo3bvkFw9GL28Y34r+tjvO/MX/6Ao7sXv&#10;fSsAAAAASUVORK5CYII=" style="vertical-align: -20px" width="15px" height="41px"><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">S</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">·</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">h</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA8AAAAqCAYAAABstwHPAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAOJJREFU&#10;SEvtVkEOhCAMZH/mm/jGPoGzyb6B+AcvPMAzJ37QtRCyQihgI9HDNuFgZDplOkVfsIfgBoK5IbhA&#10;X/EAsAMt3yDkCraSvcC8gRI7ENd5cKCyev6DC6qTfb5PMKNYfT6YxJtlBk3YbIS3+8blAczT9AmT&#10;1Fi4L8YDyu7TN911ddmpw5Sha8qYEbhA3O8n6/Ccp0nB1mW3JSbjetuuINVG1k0LhkDO1RvOGt32&#10;06B+5uxtTCK1K5be6HP47DDBAHgdUb2uMzdES8FmSUeSo3bvkFw9GL28Y34r+tjvO/MX/6Ao7sXv&#10;fSsAAAAASUVORK5CYII=" style="vertical-align: -20px" width="15px" height="41px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">×2×2×1.5=2(m</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAANCAYAAABlyXS1AAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAGtJREFU&#10;KFOdj7EVwCAIRMnKrOEIbMLLDjYMYG3lBoQYQ6LRJlcJd/x7bmqClU5zJahGjooQjBAUuXi2mUVz&#10;XSUljO1tdR3SCCzPxk2hCwuUBqyPRRl3vY97rIWEh05HGpZmnbO/frDv0H/zAF6bPXFABehIAAAA&#10;AElFTkSuQmCC" style="vertical-align: 5px" width="6px" height="12px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">；</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">　</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">3</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">分</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="39"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">30.</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）</span><span style="color: #000000; font-size: 16px; vertical-align: middle;"> </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">求该帐篷的侧面积</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">S</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">.</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">4</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">【解】</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">3</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">）</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">h</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB8AAAAMCAYAAACa7GYMAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAS5JREFU&#10;OE/lk71txDAMhZ2VtUZGUO0lCO/ghgO4VuUNmMcf0ZLhQ5LikCICDpJ1FD/ykfwQrOWvlsJt8Sal&#10;bkKsH4fUesT9LtREGq0I8vPlr9CZrvLQdillFzy31fjIsyXdDRvtwmocAHXGCIbZ4dOCnQf5wBuD&#10;VPDoM5PzdwFHpgtA6hRBECIkhePcBjjXyBxOqZ8tWLyd4jjxf4AjgKdgHQ5YNdBm+0u42ggck5bk&#10;vo90ha8u+U360crgDLhn+UN4CQWsL3oQ3a2quCKB8wKn9LNCWfOEQ65e41n2kPLbzKNZkcgyZX4P&#10;cmg4A5kKgCOz2ouYNf8tXMvn3a59dJXrEj4yV8eQfIBqKXy8IKF1O2xMbnwTssodd8M4TWOao+RT&#10;dB/HlP15cN57+3/hX0aovac05gxAAAAAAElFTkSuQmCC" style="vertical-align: -6px" width="31px" height="12px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAD4AAAAcCAYAAAA5pQx5AAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAZ9JREFU&#10;WEftl1GSwiAMhtkr9xp7BJ69BOMdfOEAPvfJG2SB0jUGGgKtVK3MOOOMTvJ/yU+a/oA76ojHgx/x&#10;qBpopX6dO977M/NWg9cU6pX/+wUvdcdb/JOOuONl8CvoeP8Hc9uxRjIdm4GP5gI24PrE5/i9P79U&#10;x2bgGNHqE5ixPzTNyOkQgZdt/pjSuu6/ADdwOhrBb2AG9zwfMoD2DHryfOVhYuYijRcY8E6hr6T6&#10;vI4G8PvwSMAdtKICiOjRnCAdfkzMhfL5OHiZeii2QEcD+KQkJEYdZ4Ug8XnwfMxly7hCLRRYqmMz&#10;cKmvNwH3HY02b7tW7sWsJHhpsNGOl+LMv68Hj7OAu98CMW8IjqnuRajt/JPBM93Jvd3FWdHqIqvd&#10;E6YwVKkJngyeem691TM+FkzxjwVfZfV5Us7V4Ta2YK/cAlMYLFzHm2L6RaZBx7/V8TLAg6NlI9zX&#10;ur28uMAkMclGRze2BmjPl9xx3PXaHV3wFAmLz76vrZPK7uCS4vT4T3aq07veQ0jvHF9wXPHDdry3&#10;7fbIV9zc9hDVI+cfGxZrzgoIqv4AAAAASUVORK5CYII=" style="vertical-align: -6px" width="62px" height="28px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">＝</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACcAAAAwCAYAAACScGMWAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAbtJREFU&#10;WEftmMuNwyAQhtnOUpPbSAmcI20NKD34QgE5++QOZsEODk8zPMVGtpRDZGw+/hmG+f0D4iKjXhJu&#10;1IuUgBFyF6qX/0IMxXAli4s9+51wMpytr2zlLrjc0AyrnAu2AptESZlmWJzVvoBq5YZyvBxZOWfC&#10;aZM7cPLeExTPwh6iLn7+xzArwO1TbBPbcMtqKSlhH8Bceb2cbeHsKZcZJvqKCXbcT4YLbQSvcjqG&#10;BPPmZJi1C9yea+oMbphz2cqpvBSQE1tRoe2i3IdkLzmDwgFwegdsrTtVTuWJWvnZqSAn9RdhLYKJ&#10;myIIpzeR53DmCUD0OsafZjNae7fq6vU4T/WdEt0QQ8PJldi5h6oDFQZFlbvgAiqjlKsQoaxXHHC3&#10;228VD1rqYyXHUbqyltTpof8R1k5iJE3z7crlu6uYjIXKyf7Mdlf4lqgtHJ8tJ/X2rwkm5gywUDn3&#10;1VtfNybcrhy2020bVp8vTWwou4WVU7ybj6m2dUOYQZgx0pvWCmfds1V4BcPuCSPDEr4mhRZfrpxt&#10;YjZnj3f1zXLO/MygffIfs5RgshM/pjys+LmSR15wyZK9H7iUy1XuDwPtL3q83EevAAAAAElFTkSu&#10;QmCC" style="vertical-align: -20px" width="38px" height="48px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(m)</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">　</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">分</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">S</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=4×</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA8AAAApCAYAAADqI3NhAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAOZJREFU&#10;SEvtlcsNwyAMhulmmYk1OgLnSJ0BZQcuDJAzp2zg8ihKeBlqNWqk1hI3Pvxj/4Yb2GDUcDA1GBX0&#10;ik+AN5D8DowrMMjplcwrCGZBt96HQyoj5z9cqXqzz98rmBakPh9M4s0yg2zY7Axvj43LBTJP0yNM&#10;Ume5fTEuIHusvumuT8tOHSZ0W1OW2T18C8T9frJs9VsHpLBWmY9fr6hYq+m7d/bTRYND5jHZuTij&#10;gCNvNypbi/Yso3+VqzTWpjasF+By2y9h5ctKv0vZFiwna+/9sSwJHE1RwLRW4ePSNQmG/yL8BJ+5&#10;5GYqLXTDAAAAAElFTkSuQmCC" style="vertical-align: -20px" width="16px" height="41px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">×2×</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">h</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB8AAAAMCAYAAACa7GYMAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAS5JREFU&#10;OE/lk71txDAMhZ2VtUZGUO0lCO/ghgO4VuUNmMcf0ZLhQ5LikCICDpJ1FD/ykfwQrOWvlsJt8Sal&#10;bkKsH4fUesT9LtREGq0I8vPlr9CZrvLQdillFzy31fjIsyXdDRvtwmocAHXGCIbZ4dOCnQf5wBuD&#10;VPDoM5PzdwFHpgtA6hRBECIkhePcBjjXyBxOqZ8tWLyd4jjxf4AjgKdgHQ5YNdBm+0u42ggck5bk&#10;vo90ha8u+U360crgDLhn+UN4CQWsL3oQ3a2quCKB8wKn9LNCWfOEQ65e41n2kPLbzKNZkcgyZX4P&#10;cmg4A5kKgCOz2ouYNf8tXMvn3a59dJXrEj4yV8eQfIBqKXy8IKF1O2xMbnwTssodd8M4TWOao+RT&#10;dB/HlP15cN57+3/hX0aovac05gxAAAAAAElFTkSuQmCC" style="vertical-align: -6px" width="31px" height="12px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=2</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAbCAYAAABr/T8RAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAQdJREFU&#10;SEvtlksOgyAQhumVuYZHYO0liHdw4wFcs+IGU5Ciw6NUXqFJO4kLI/LN/PAzPEAFGREaPCJIDpSQ&#10;SalT91heNjgn0dTY7wdriVvG7Yr/4FrZb0kdyiyBU2UruoIIMtiBIcuxLZ5iARhNHID1twUsS/BZ&#10;+f56xykUgM3vx6Q+WEhPAZ3IDDyUBdqCfVXFCpTtUa0/gt/ZKFoxRmhodA+YQV3AZm3tmV64xsUV&#10;232gEqBcBnJ3qfiiGNsNAANsbIKYl52K7bqcPTPRGPSE8QMEqZrYYCcYN/g02D2ZCPbptrgXhZxd&#10;jatu3ZGSJ9cw8GHulwdrO1D21ef3wD0lLrretkzoCcG7V25vuhvCAAAAAElFTkSuQmCC" style="vertical-align: -6px" width="31px" height="27px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(m</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAANCAYAAABlyXS1AAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAGlJREFU&#10;KFNj/A8EDLgASBIXYPj//8H/2QzdQN3d/4t2fUBRx/Bq1/n/V8BCIEV7oGyIGqBOBLgye+n/Xa8Q&#10;fFRJoClIckg6r+z5PxtiPhxAdAIlGGY/wHA00EFLwS6FYWTdKHaia6WRJAC+Sj1RwffRSAAAAABJ&#10;RU5ErkJggg==" style="vertical-align: 5px" width="7px" height="12px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">).　</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">分</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <h4 class="p_t">
                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">第</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">卷</span>
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">一、选择题（本大题满分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">16</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">.</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">共</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">4</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">题，每题</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">4</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，有且只有一个选项是正确的）</span>
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                </p>
            </h4>
            <a name="41"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">31.　</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">已知幂函数</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAYAAAAMCAYAAABBV8wuAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAFtJREFU&#10;KFOVjsEVwEAERKVl7ehEHarQxITILofNIXPijfnjQohOgiuYKE2wet49IrijVoOwvnMaS5FU21sZ&#10;JoUiaacTAVGWAK6OTsN0dGxMoAZplI9kvfuh/8YNB/vxPqg3sAwAAAAASUVORK5CYII=" style="vertical-align: 5px" width="6px" height="12px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">，其定义域为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">[1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，则其值域为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;"> 	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">[1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">[1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">27)</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">(1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">27)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">(1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">4</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">B</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="42"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">32. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">张红、李芳、赵明都喜欢下象棋，年级组织象棋团体比赛，班级派他们</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">个人参加比赛，要确定与对手比赛的先后顺序，他们约定用“剪刀、石头、布”的方式确定，问在一个回合中三个人都出剪刀的可能性大小是</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA8AAAApCAYAAADqI3NhAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAANZJREFU&#10;SEvtVdsNwyAMdDdjJq/DJszBFF7CxaC0ITGPWEJNpFriA8mHj+NsXpwCrCFga4AVmBkvABMHBAYM&#10;TJ3TlcqRPSSgrOvgUooC/sGK6s13/p1g0ZveeWeSbBbk0LDZCm/PtcsNKjvnSicNluRtcQPac/rW&#10;WSto1y7zUefVmGGet/zcXfDd7485g4kOE1NY6P4e35kCY4N3HyzAzvjtT5KP22bvfBC2CAaMSlOP&#10;75zkk6/HCGaWkaRpNq7cEe0Mjr5uS4vaM40ypn3tf56pWXIeWvkNkGrZBO0C0RMAAAAASUVORK5C&#10;YII=" style="vertical-align: -20px" width="15px" height="41px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAApCAYAAAA8hqkEAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAANpJREFU&#10;SEvtVdENxCAI9TZzJtZxE+ZwCpfgFLz0qkW5Nia9pCT+6ePxeOCLcrgrUQCuhLvymNkvBEiE4MgB&#10;UhpkURhECi4/LuccgKRMCA/A/2sQw2kffBmJDQWEih1XzoJtzG7EwHsv02c45e4nblSCTfP+1toS&#10;eCOxqIGiQnGyE/WHgy7UOQhazj2VjgFP4IByW0kDINkBcVvrE7A9QAwsGuS/QAqon4tZRAZohKug&#10;miQHDFrlpSwbQCbeXU5I8MtGEvNsLLgrg5YeGklaWc/ED2tnwTKhDwOiNyIsXpogrMg9AAAAAElF&#10;TkSuQmCC" style="vertical-align: -20px" width="15px" height="41px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAApCAYAAAAvUenwAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAARRJREFU&#10;WEftltERhCAMRLnOrIl26CR1pAqayAEeKohZ9I6Z+4AZv4SseclGXhKWGbmiwMhlRgZPdP5EwAtZ&#10;I8aS+Jtf1JEBizMheHzGCKyf7MlOAb16ExHs7uGI2A3zwcFoyXBW6IadO5wM6akbpgDktyFalmWd&#10;mD94Yqy8Zg36awB3PtzQqEHpXMdl5DSTTo1w7e5KIP7cneSYOdguUo8N/K8uBZiqOfO5TWQF5k18&#10;y4udWGU4wTZNU7TmdKDGTh9+QGDN4Dp+QAauMrqAJ7FaAIAHXh1R+ui9KhA7SEEfzmI81wJ16gEV&#10;VX6QDjxtgXDwbKTdG7mBevCcBNoubbVpHx5Y5IfjpzgGjfatyBSABIcjegMYix/J1F9U7AAAAABJ&#10;RU5ErkJggg==" style="vertical-align: -20px" width="24px" height="41px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA8AAAApCAYAAADqI3NhAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAARFJREFU&#10;SEvtVcENwyAMpJtlJtZhE+ZgCpZwOFInGGHioHwqFamPVD5jn+/Mh8pxqwfg1eNWgbXil8GJgnPg&#10;of5C0tN3NwMYiONz9CXB9d2nkeCcKYsIJPMU5Z9nxLznHMlP6tbBAPrYVSILH4KPXpk0a88dI5zE&#10;K03fzDlT9I4WwUQp6LO+Z3tCmgSn0BBVCFth26r3t41hvfctS27bJslqbHmpzRHi+Px6z3aOr8hh&#10;z9DzSdKTZVCBjSzrt5Kgu3lgQehd0fgQ3AZjIdj9jN0FgdRSy/Y0W5KJPK2p7y/luUHfAPHL8WAB&#10;gt22x579Vg+D56bfWfIJmoCPUYnR2EeFvN8EbMnHbBuF/vezkSgO2wGDMb5+xuA8dgAAAABJRU5E&#10;rkJggg==" style="vertical-align: -20px" width="15px" height="41px">
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">4</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">C</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="43"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">33. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">在直角坐标系中，当</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">a</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">&gt;1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">时，函数</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">a</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABEAAAAMCAYAAACEJVa/AAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAGxJREFU&#10;OE/dkdEJADEIQ72Vs46bOIdTuETOlvuQoz/iXwOCFHzG9GFKplqQqWQK2Jd0IK5CUSddMwJldlst&#10;yBpwA7FARR8kaMgtcigYowyEKa0+9J14LgN/RhrnhBE7B6emYxQ77UxOH3EZ5AViNskIwPqANQAA&#10;AABJRU5ErkJggg==" style="vertical-align: 6px" width="17px" height="12px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">的图像是</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,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&#10;QmCC" style="vertical-align: 0px" width="97px" height="90px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAGEAAABaCAYAAACouzjNAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAB25JREFU&#10;eF7tXWtLVU0UPi/+Aj+EGBgoIkaEhOVdk8oPikVXIU0EKRJCRSjF0gKRLoQRVJbWlyxLUUNFy0op&#10;vJWFkV0sisqi0i5mGlqaul6fgX2gNPfpOLszc/Ys2Ki4z8yc55m118xaa9b+j6bEIom8ffvWUlpa&#10;atm5c6fFxcVFklHbMEyQIJJMTk7SyMgITUxMsGHh5+joKPu9sbGRVqxYQV++fBFpyHMei2XOLXBu&#10;4PXr17Rr1y6amvWs5fv379PU7Ge/p6SkQGvp+fPnnHt1bHPCkQCAMzMz6dmzZwyZI0eOUGVlJQ0P&#10;D9OyZcsYCadOnXIsapx7F44EfL/z589bNWHr1q0E7bh79y55eHgwEhITEznD4NjmhCMBM764uJj6&#10;+vqovLycduzYwexCbW0tpaenk4+PD+3du5d6e3sdixzH3oUjYWBggPbt20etra00tQqy2gMY66dP&#10;n9Ly5csZQfibh2Ah4GgRjoSxsTE6c+YMdXR0UFpaGg0NDVkxevHiBUVGRlJ/fz8X3D59+kRJSUn0&#10;6NEjLu3Z24hwJPz8+ZN9l3fv3lFRURGBFE14kwBtCwkJIWifI0UoEvCYqa+vZ3i0tLRMm6G8STh2&#10;7BitXr3akfizvoUiAcY2IyODLl26xDThd+FJwo8fPyguLo7Onj2rSPgbBHiS8O3bN/Lz82ObQUeL&#10;UJqgBwZPEm7cuEErV64kGGdHi2lJSE1Npfz8fEfjL55N0EOElya8efOGwsLCmPEXQUypCdh9h4aG&#10;Mn+UCGJKErZs2UL79+8XAX9zPo6+fv1KCxcuZDtyUcR0mnDgwAG2QePle+JBpKlIwI4cDsCGhgYe&#10;2HFrw1QkVFRUUHh4uDVcyg3FOTZkGhLgjUV8uqqqao6Q8f+4KUhAzODixYvMDY49gmhiChIQfwAB&#10;dXV1ouFvniVqWVkZRUdHE5anIorTawKSBFatWkUXLlwQEX/n14Tx8XHKysoiZGxoETsRmXBqTXj8&#10;+DF5enqypAGRxWlJQNwY+UmFhYUi4+/cj6NDhw4xWyCSe+JPs8EpNeH27dvk7e1NnZ2dwmsBBuh0&#10;JPT09NCaNWuooKCAYJhlEKcjYdOmTSx7WxYCnEoTvn//znJUExIS6MOHDzIogHWMTqEJSBg+fPgw&#10;BQcHS3l2QXoSNOcczi60t7dLpQHaYKUmAc/9trY2FiO4evWqlARIbxMAPBJ6ZdUAqTXh48ePdPPm&#10;TVq6dCkdP35cWg2QmgQkDAcGBlJzc7P0BEj5OJo6v8wSeUXJnuMxC6QxzDjLjHQVNzc3un79Oo/v&#10;LkwbUpCAs2pwxi1ZsoTrcSlRWBCaBByVQo4Q4sM5OTlsFcTzzJoiQQeBwcFBOnjwIHl5eVFJSQm7&#10;m1dWtijgC7s6QhgSy89169axdMWXL19anXGKhH8wfZANkZubS4sXL6bdu3dPc8QpEgwkAdlxV65c&#10;IX9/f4qPjyfEBGZyRSsSDCABhhdB+NjYWLb5QlrKbOFIRQJHEjDLUTAkOTmZFixYwAwwTlPqiSJB&#10;DyEb/g+gsdHavn07BQUFsWXnkydPrAWm9JpQJOghNMv/kQuKbOgNGzaQu7s7ZWdns1XP34oi4S8R&#10;g7F98OABKyCFgxnIBT19+jQ7qW9vNpwiwUYSAPKJEydo7dq1bKmJ0gWXL1/mcmhbkTADCYjtYjmJ&#10;zRX8+ljlLFq0iGJiYljhKATf7Z31M3FuehIQy4UnE+6E7u5uOnr0KFvTI7KF05DY3dbU1LBHkC0r&#10;HRsV65fbTE8CymCiAsu8efMoICCApZYgw+HWrVv/7AyY6UnAxgoVslAVBeUyHZHjaXoS7Hl88P6M&#10;IoE3ona0p0iwAzTeH1Ek8EbUjvYUCXaAxvsjigTeiNrRnswkIFkBJ0lnEgu8mLJc165dY3sUnMSR&#10;ZczaOPPy8liJadTdQ/FdbHi1UtMW1HuQ5ULqu6urK0VEREgzZmAL4H19fVmxde2aP38+4VwdxIK4&#10;riwXNoqoXffq1Stpxqxhi5g5oofQiKamJnr//r21/JuweUdwk8AXhUsrVCuzTcDY4Xeb0SbYYR8N&#10;/Qg8rydPnmTub4RBMZPwZhGo7sOHD6VI/kLCMjIGUeIN30N77PwJOKE0Aa5xuMSrq6t/ybZAZC4q&#10;KopQNEqGDDwYXJT2QfrOvXv3dL3KQpGAV7ls3rx52oSB8xCxCmTiyUACvgCWpAho2SJCkYDXuOBt&#10;Ib8LSMDZZK1wFK/3J9gCkD33IJB1584dWr9+PQtqoTj6bCIUCYjGIWbxu8BtjiQBxC5E1wRsyJDI&#10;Bvu1bds29n4gvXrcQpGAmY5cJBhnTaAFKK9/7tw5djxWdBJQWQYGGYsKnCTq6urSVSahSPj8+TNt&#10;3LiR9uzZYzVmeL0XDDVE5iWqNI8jDBQrJETwsMzDhbi29vIhRYKuUhl/gyLBeIx1e1Ak6EJk/A2K&#10;BOMx1u1BkaALkfE3KBKMx1i3B0WCLkTG36BIMB5j3R4UCboQGX+DIsF4jHV7UCToQmT8DYoE4zHW&#10;7UGRoAuR8TcoEozH2LQ9/A/NN/oh/StuMQAAAABJRU5ErkJggg==" style="vertical-align: 0px" width="97px" height="90px">
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,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" style="vertical-align: 0px" width="97px" height="90px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,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" style="vertical-align: 0px" width="97px" height="90px">
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">4</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">A</div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="44"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">34. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">在等比数列</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAWCAYAAADXYyzPAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAStJREFU&#10;SEvtlr0RgzAMhcm21KoZgQnwJrqjZgE3rOAlFAnzY+zYxkCOFNElDSHvk56eQ17EVT1RAn6iqk/Q&#10;tm0v95LTCMB939MwDJfBoiFasQrAdV1fhi4CKa0AzDm7DZzS+oMptheDIOed30CIiipAMpmlHN6x&#10;JHEcR0/OEAIDlZ6va1LcAGAOS1OqYydkt+Ou64IZtHKh8rFtZO0jM3XsPO/AwcSaLa0ULbNODIME&#10;/rUIXNw7NLF8v2maVUam3Vvq254e19Xy70wep73NdrcSMKU18Stb58/xZLWFWcsXuGP/an0YuvPg&#10;7Eyyc7YfkVDNjTmp+y6YXUAAm3J2yM1EETgViI8GODA9Tb1VUbgk/iWPxQ0273/+Rcvp/M4fgSN5&#10;uuOe+x6+hd28AfZRO3Ou4N+RAAAAAElFTkSuQmCC" style="vertical-align: -6px" width="30px" height="22px"><span style="color: #000000; font-size: 16px; vertical-align: middle;"> </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">中，若</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">a</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAAMCAYAAACulacQAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAFxJREFU&#10;KFOdjtENwCAIROnKrMMmzMEULEERe63V+OMlJCbvfHBFhnZpcBfqwEKImiFY/e0WdNXEKMnzznWz&#10;0oQDn1eYFoj/0CSk+ysfTEAjAXTluhSDzrJzPPAc3hYyHZnNUzLvAAAAAElFTkSuQmCC" style="vertical-align: -6px" width="7px" height="12px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">=2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">a</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAAMCAYAAACulacQAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAGBJREFU&#10;KFONj9ENwCAIRO3KrMMmzMEUt8Qplaoxtekl/PC4Ay42lZMCnlQGcI2EVkrP5oDuT2vmJHRquMSI&#10;ZUeHQG9G9DIwd6YDprS0bxA02Q6CSV4qw3X//+/Pl6lPZwWUwh/BLKzNUAAAAABJRU5ErkJggg==" style="vertical-align: -6px" width="7px" height="12px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">=</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAApCAYAAAA8hqkEAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAALpJREFU&#10;SEvtVdEJxSAMzNvMmVwnm2SOTOESeUYr9FGrQRF8pQH/zHkJd+dHYsFMKcBMwUxzYr8IIAh5EPAk&#10;ofNChQELQmzWMwaQnwzkX4D/3wHjsA5OQkqC8kINOa7ygt1iGzFwzmUHGo7eLbXRCPa9/95cPAJj&#10;N5UaDA5Jd3LxFoARhQyZUAeI1JFtwVqPde02JvMFQKnn9hEGgYRK9whASqEbLxxTXfTWFJLld9oc&#10;wGKwxWYyUHgAgy/6BnPQdOERWQAAAABJRU5ErkJggg==" style="vertical-align: -20px" width="16px" height="41px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">，则</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">q</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=　	</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">()</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">A. 2	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">B. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">　</span>
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">C. </span>
                                <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAApCAYAAAA8hqkEAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAANZJREFU&#10;SEvtle0NAyEIhu1mNxPrsAlzMIVLUD9qemoPjMbkmhyJ/+SRjxd8STC3YhGwYm7FOUW/CeCFwIkD&#10;Em+88CMCFnTBOZ45QH7SEzyA/68B47QOTkJKggIhRY67ZmF8xG4UwXEceQIHTrxb7EYpjNe9vrkj&#10;hVqJyHpsTQRxmaIUn7TWQlc0SA1ganT/2c4KwaxBmsp5QI5gPIW2Xp4EjL9BTYFR3wXq3xg7YLXw&#10;GsAocF5DIRW60EOfQnDuJ/KrjbZMFaAIpwPMt9EeMVNIFuIBiLwB6UZqNLajQioAAAAASUVORK5C&#10;YII=" style="vertical-align: -20px" width="16px" height="41px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">	</span>
                            </p>
                        </div>
                        <div class="col-xs-6">
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                <span style="color: #000000; font-size: 16px; vertical-align: middle;">D. ±</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAApCAYAAAA8hqkEAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAANRJREFU&#10;SEvtVe0NxSAI9G3mTKzDJszBFC7B86Omtn5GY9ImJfGfnBzc4U9sqJVwACuhVpJ99ZsAjBAoUUBi&#10;Oi8UKmBBZZPdmQMITxqCD+D9PWCc1kEiJC8oEGrIcZcXxi32oAq01sGBA8fdjfEgCuN9v97cReGq&#10;RuR6fYUK3EJFiTl+tdnJ1EByAKab9o8NXUEY6oF35jxAqGCcwr1fhgQa/0OXAuPCPnATaI2w/bky&#10;CqSryFKhgh7KFGxy7spTG2mbMoAonAxgfoxtm3Wn0HPpByDyB+pNajShDu4cAAAAAElFTkSuQmCC" style="vertical-align: -20px" width="16px" height="41px">
                            </p>
                            <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            </p>
                        </div>
                    </div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">4</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">C</div>
                        </div>
                    </div>
                </div>
            </div>
            <h4 class="p_t">
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">二、填空题（本大题满分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">12</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，共</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">题，每题</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">4</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">)</span>
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                </p>
            </h4>
            <a name="46"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">35. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">某商场试销一种服装，经试销发现，销售量</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">(</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">件</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">与销售单价</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">(</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">元</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">符合一次函数</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">kx</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">+</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">b</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">，且</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=65</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">时，</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=55</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">；</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=75</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">时，</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=45.</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">则一次函数</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">=</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">kx</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">+</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">b</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">的表达式为</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(　)</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">.</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">4</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">－</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">+120</span>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="47"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">36. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">如图是计算边长为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">5</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">、</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">6</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">、</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">7</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">的三角形面积的程序框图，那么计算的结果是</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(　)</span>
                        </p>
                        <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,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" style="vertical-align: 0px" width="122px" height="168px">
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">4</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">6</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABkAAAAaCAYAAABCfffNAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAOBJREFU&#10;SEvllkESgyAMRemVuUaPwNpLMN6BDQdwzao3SIVES2tAEBw7bWbYOXn+n0/0BnOJs8tDzi5RAxDi&#10;PqsuP0vvakjNS30nxNt0tIrt+g+I0wOlbgTLeFpkV9qqCVSINN+8Kl08hABq2s3DYSVW7StoVIIq&#10;pDZkVx64q4S1yo5h0FIaGvQDtEyDGiAfwyawYuLVDwJoYT8I19AZkGIA7bZhe1OyrPE1FZl9hRfw&#10;ZVlIWyLOKyT+TpRA/DMYYzqZ+7KZSaymZSnGpl0DwR8LtKBXsRH+HUgvm6oWZCv0CY5gEFmEHQtf&#10;AAAAAElFTkSuQmCC" style="vertical-align: -6px" width="24px" height="27px">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="48"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">37. </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">中国政府工作报告数据显示，</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2014</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">年</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">GDP</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">（国内生产总值）</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">是</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">63.6</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">万亿，</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2015</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">年</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">GDP</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">预期目标增长</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">7%</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">左右，那么</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2015</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">年</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">GDP</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">约为</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(　)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">万亿元</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">.(</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">结果保留三位有效数字</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">)　</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">4</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">68.1</span>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <h4 class="p_t">
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">38.(</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">本题满分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">8</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，第（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）小题</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">4</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，第</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">小题</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">4</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">)</span>
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">某商场进一批单价为</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">5</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">元的商品，经调查发现，该商品的销售量和定价具有如图所示的数量关系</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">.</span>
                </p>
                <p style="text-align: center; margin-top: 0px; margin-bottom: 0px; text-indent: 32px; margin-left: 0px; margin-right: 0px;">
                    <img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,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" style="vertical-align: 0px" width="160px" height="180px">
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                </p>
            </h4>
            <a name="50"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">38.(1) </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">求销售量与定价的关系；</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">4</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">【解】</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">　（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">1</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">）由图可得，设</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">kx</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">+</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">b</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，则</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEcAAAAvCAYAAACxF9coAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAA21JREFU&#10;aEPtmr9y3CAQxp0nzTvoNezJC6h25e5KzblMkepSqHHnGVe+JlekJ9oFxALLH4GU4Jib8cyNZNDy&#10;42PZ49MXsXzu+ocnAHD6hydw18GECXQ4EXVkwXl5eROn048lN91/KqFlwbGgXC9iWCDBNfwbXxWw&#10;VzGqa8N0q4R4E9Mg+6/vS4ZynR5VzGcxk+jM9Xsx0huwUeWMgsKhncF1u0MA9Cima7xX6CNn0POY&#10;7isdv540Gwq2m88LMH1d/h8dz0Y4SwerUpiwQFXDRSTY4Cym4UCwzIDSNMh/KDBszFKd1uQCLBL/&#10;NjhIWsrdleAq3Rg8FXYWHHhWRl8xVvMIsQYAY3pwlOlcy4IjAzB5wM835j5Cc/KSq5IcODCwcTZ5&#10;zMwoEwfNgfr7cBbfMGdd1lxogbKWlEZsL60sOP4uZQJcFYRAyPqNzHoajpsn/HyQXF1K5bDMZZ7V&#10;MasYj4MjQ0PZKgiYqOG7BSmgOHamSa7ylhSTH1J0uMErYDih3H2leD3hhcpRkZFBAKhh4LdLdxwp&#10;5cglRVvRXTBzWX2FWNx8QxS4b85hpmqBIwchgx/npb7hHuo0jcPxd6lVlSm1WPeZpWjF5qsRn1O8&#10;W9GH023b2gLVzOJyW75Pukg0jaNwnCUl66qyLd1tS9OAzAt0666pc9zKmBDGJUWqYlMo8kVczrLi&#10;d8RN0jF50avm7YkKlSd1OWd7rNgiBaew292b/RM4u4/ioA47nAjYDqcWzkGqbb7bqHJut994yPX8&#10;/LP5gRwRYBTO+/uvDucI6v9Dnz0h1ybkz3awrnl15XTllGXArpxa5ZRx//itipXjm2QNm3pBI1JO&#10;YMjYK4ATMcnUiWBbpp49eM9Wihh7G+HETDKYgtZMPdBFzIiMG3ub4ERNMi3PDCMu67BrB1MP10zM&#10;iEwcsm+AI1UTNMmUL3S8qQcjznQghu/iSb2QwB67JryrLDh0BoImWWumnrdZMkbkHnDw50PCJGvO&#10;1AtUEpYDkTD2spQThIO7kzTg/p6pt2VZMW980Fy2d86xnMi18xZNvZB0tBFpINMxUWMvXzlrsWQM&#10;tlWijZp6Hh6u1IgYe5vg4M6I77zYr7w1a+pFjEgKrqpChhcm4Rz54eG0yw+mrDpnlyfVdZKlnLpH&#10;fNzWHU4/sihTb1dOV06Zcv4AJPm1+Zdm/OMAAAAASUVORK5CYII=" style="vertical-align: -18px" width="71px" height="47px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，∴</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADwAAAAwCAYAAABNPhkJAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAohJREFU&#10;aEPtmjtywyAQhpMrZnISXSO+AbUv4FJjHyCVU6hx55lUdhMV6QnLQwK0Mg8DsiM044lHIizf7r+A&#10;WL9Sdr2s6QLgNV0va4Llaq7AiAdOp2+6232yXP94ev94RdgEPVPCwOFe0/YLOaCnbcPG0BzpFRnB&#10;td3y8cGHdGaDCGDoAKC3tMWsZXfB6HAUuNsz0D0VnKKtDh0HfD3SZsa72XmlAR7FyRhE5I2oggO0&#10;dlHA3Bg5l2JD7aDAEAhbedY9L2DTouZF3pnIFfi8v43f1b3J30TKQIENOatRm7L2AjYmLQ6p5chC&#10;kS4GPMjZAC+vbm9gqUKV18ER7ghbjho17atIA7BcKjSJo7IeJK3Ntq7/4c91W5QWymGxHJGOrb/Y&#10;BFEw0L6ztN0uLMLGFC8jynOYfW/LztqgtNl12FJR9DrM5aztrsYdTclNiJ0KU9sZdloFtZvYVJik&#10;ExtforsKjHn9P7wWKi6vCC8hvVw2bwL3/S9/8T8cvnLZL97vTeDL5WddwMXdX8CgVw6vbtKqwAWk&#10;l8tElXTdeDAPrC6Hc+XTEv0G5vB4jHNf1eF25WA4LoqoLLicGAgsuuvIPS/8jsqBrBbw8zAM2FFZ&#10;yAAMAzYP1FxGsOf4mdTY0vfMilqVBddYwiMMBhKcRUcBe1QWkgPDuRbpMFmGHdNGAXtUFpIAj50o&#10;0PnqnMugev7QwMM6PJEzUq3zJE4GbFUWXOaDcljIWe9SrxMXkHTZHJ7OzveUTaMiLMs5utNd/dgR&#10;94+wJWdx2B2/PM1WDuQIYysLySU9FMiilyZX5cD1XBbSZn7DkQQYfsUDB3mbzc7V38M/95L0w1ME&#10;DLACBzjrKZvWCD9l2AIG/QcWntINVc2GJAAAAABJRU5ErkJggg==" style="vertical-align: -18px" width="59px" height="48px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">， </span><span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">分）</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">那么销售量与定价的关系式为</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">－</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">10</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">+110.</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;"> </span><span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">分）</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="51"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">38.(2)　</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">将售价每个定为多少元时，才能使每天所赚的利润最大？最大利润是多少？</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">4</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">【解】</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">）设每件商品的售价为</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">元，由题意可得每天可获利润</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)=(</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">－</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">5)(</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">－</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">10</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">+110)=</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">－</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">10</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">x</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAAMCAYAAACulacQAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAG1JREFU&#10;KFNj/A8EDLgASPL//wf/ZzN0AxV1/y/a9QEiBNIEIl7tOv//ClzRHigbKglXCmRcmb30/65XSDpR&#10;JIGmQOUgxsLBlT3/Z0PMR9gJZgElGGY/QFELddBSsEthGKYb1VgUfVhciyxPvk4ASJIhbb1dZ94A&#10;AAAASUVORK5CYII=" style="vertical-align: 5px" width="7px" height="12px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">+160</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">－</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">550</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，∴－</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAApCAYAAAAvUenwAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAWVJREFU&#10;WEftVkEOwiAQxJ/5pv7Ac5/A2cSjZ9I/9MIDPPfkD1YWxJaldBdiE01KYtSk3YGZYWdP4JbacyHA&#10;nkvtWdyz8wMATzBd73TqoTPP6v2IT2D1FcxUXV9K0QO0GsDW1xcC2AGUfjSUF4psdQ/a4imCFqob&#10;QcqWQINYOFIU/mshXzxARk9w1dcAAj1L+vEEckcxJ8jdM5lrleDbAIQeX7zSrqwGSJF3Dn4arMoC&#10;NJl/8dIBwDL4oeh8vs1iRlEbv7FWXIcGcg3YJxsfKGiw6P0VrXltDysA2I7neAz9R96eKUgOYEcS&#10;7u+poqEPieci3/D2A1hPsEidwvAxbigo5DR/0aYRuuRlSlkwRGkoYwHowJXTtZ3RmwBIQ5LHmHA0&#10;0fCEGylXBnDFkmO7QvpCqeAdtg7gd7qISv97gHvipvky4lBmC3NSBjC7gwCgTRNgvIx0KMvvMity&#10;Yws68kBO3P9r8AIsHS/UhNxWUAAAAABJRU5ErkJggg==" style="vertical-align: -20px" width="23px" height="41px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=8</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">把</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=8</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">代入关系式得</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">f</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABMAAAAMCAYAAACA0IaCAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAK5JREFU&#10;OE/NUjsWgyAQJFfmGh5ha6vcgJc2tc0ewJrKG4wzoCFQpKLIvofrg3E+Kw+wwqwS2awKs4hKQj1y&#10;WmH2Ytylda8yOtN+CCtSPpAi3+OGZAtiOjovJNthAtsOOAnV8wYbgDqzIkC8yJy4oWrMG8he1Dqy&#10;S0yuL7fCj64+MX+ROeMXjluQQpHu3RS7t0Zn/RzqfLQqSZuZZvXGkzMzb998883/m7Oux/86OwHn&#10;M/8z6+bBgwAAAABJRU5ErkJggg==" style="vertical-align: -6px" width="19px" height="12px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=90.</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">即每件商品定价为</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">8</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">元时，获得最大利润</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">90</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">元</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">.</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <h4 class="p_t">
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">39.(</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">本题满分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">6</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，第</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">(1)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">小题</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，第</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">(2)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">小题</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">3</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">)</span>
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">某地区</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2015</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">年农村低保家庭最低年补助标准是</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2300</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">元</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">.</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">“十三五”期间</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">(2016</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">年—</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2020</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">年</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">)</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，当地政府计划每年都将农村低保家庭最低年补助标准提高</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">350</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">元</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">.</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">设</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2015</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">年之后的第</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">n</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">年该地区农村低保家庭最低年补助标准为</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">a</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAAMCAYAAACulacQAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAFZJREFU&#10;KFOlkNERgDAIQ+PKrMMmmYMpWCJi1bbnXf3p4y8hcHCowIrLXIENMymDK6ockDH7MChTJEV/Gjwm&#10;sxI0U9PCP8lJiJYeYAj3Thj1bt055ecLJx8WIe240Q2HAAAAAElFTkSuQmCC" style="vertical-align: -6px" width="7px" height="12px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">(0&lt;</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">n</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">≤</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">5</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">n</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">∈</span><b><span style="color: #000000; font-size: 16px; vertical-align: middle;">N</span></b><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAYAAAAMCAYAAABBV8wuAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAElJREFU&#10;KFPFjcEJACAMA+PKXSebdA6ncIlYLYKifwOFwvWaogheGUDNZaxzXUElZO4iKcLkLRFSsHhH7U4a&#10;cT3mMp4dR+Ne/hN07if3+RIokgsAAAAASUVORK5CYII=" style="vertical-align: 5px" width="6px" height="12px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">，单位：元</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">).</span>
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                </p>
            </h4>
            <a name="53"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">39.(1) </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">求数列</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAWCAYAAADXYyzPAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAASlJREFU&#10;SEvtlrsRgzAMhsm21K4ZgQnwJrqjZgE1rOAlFMnmYXBsDDhHiuhCw+P/pF8S5EUc1RMh4Cei+gRt&#10;2/Z2LkcaAbjvexqG4TZYNEQrFgG4ruvb0FkgpRWAec6KgVNafzDF+mJAyb7zoQhAU6WAzEFTsnss&#10;kziO407OECgGapzOI2lOQMERluxUxzZk0+Ou64IaUPtQuewSWfI4qDq2zxtwUDGypZWmuVbLMEBq&#10;fy4CF/eyKpbnm6ZZZKTaraV729Pl+lr7O5PrtLXZ9VYGTCMS/1wsDoS9v77H1moHc5bPcM9+wy4A&#10;EOjputf86+CsdxjysCk3bJyo35rvgj0Y2qrXOAVODcQnA1bY1AbvxXJquGT8S30WUzq/80cga54K&#10;3FTu43symTcSPjtzTiuE4AAAAABJRU5ErkJggg==" style="vertical-align: -6px" width="30px" height="22px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">的首项</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">a</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAYAAAAMCAYAAABBV8wuAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAADhJREFU&#10;KFNj/A8EDNjA//+v/u8qYgBKzv5/BaQKChggNEhygCSuzAa5CoiLdgGdAQFQVyG5k/oSAPpX+MTz&#10;n+p0AAAAAElFTkSuQmCC" style="vertical-align: -6px" width="5px" height="12px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">及通项公式</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">a</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAgAAAAMCAYAAABfnvydAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAFpJREFU&#10;KFOlT8ENgEAIw5VZh006B1OwREVOPWMMucTya9rSbkxIh0PQQdYEAaoYPc9EqIjbNxIiCICwU2T+&#10;EqQTqize7SPhQXqlTNSLSY4OouDVYnFFs/V/wg73n03BKkdOxgAAAABJRU5ErkJggg==" style="vertical-align: -6px" width="7px" height="12px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">；</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">【解】　</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(1)</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">数列</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">a</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAANCAYAAABlyXS1AAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAGJJREFU&#10;KFOlkLENwCAMBD8re42M4DpLWOxA4wGoqdjgQxIUUCSacOUfb8tsrGDGJWdgQeZIQaAzUbFTrLzD&#10;wFxoFmnaHmgaZG2YHFSvmYdPcwj8bnfQg2cnJDI3v3LK3x86AYYGQHMGO7a8AAAAAElFTkSuQmCC" style="vertical-align: -6px" width="7px" height="12px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">为等差数列，且</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">a</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAANCAYAAABhPKSIAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAD1JREFU&#10;KFNj/A8EDOgAJIgOGP7///B/V1E3UPWe/1egskBBEABJ0EHwymyQ7UBcdP7/K5C7cbgTq+OJFAQA&#10;qmrjfI1MK7EAAAAASUVORK5CYII=" style="vertical-align: -6px" width="5px" height="12px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=2300+350=2650</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">d</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=350</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">则</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">a</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAANCAYAAABlyXS1AAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAGVJREFU&#10;KFOlj7ENwCAMBJ2VvUZGcJ0lLHag8QDUVGzwMRAlKJIrvvz3n/UHXBSph5FoM6wZTAmGAqETrG0A&#10;J7Y2qGaoPAdSltAbyhfE3LP0ay6GjfbUwH7G/EmcUd+fwZ7dnQH2Bs8uQHMvxsomAAAAAElFTkSu&#10;QmCC" style="vertical-align: -6px" width="7px" height="12px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=2650+</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACkAAAAVCAYAAADb2McgAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAY1JREFU&#10;SEvVlq9uwzAQxrMH7HsUG/cNVjxQk5GoGg2MWjBUDWXA0mgLE7KA8Zu/c05zbCdgypzupKpS/+R+&#10;/u678z2QjeLeA5D3HkUI2PdfVFVv2bnnco4g2/aT9vsqO6AkRG4whDGC3G6fCUrmiZ5q9UiFaqgb&#10;Et5uLYFhEvJ4fKXDoc7DR1fShQXEy4NE8t3uJbIbKwn18IfL5SMTpEvT1WUEeT6/M4tfUYYEHL6A&#10;3DkjBQmGUDCGhGFDeuoaUkNJtGG9nYfsZ6ruFzlLClKq6jcwQ242T5w8DnjnRIY9VFJtHY4H/yUk&#10;GMACJgmGZAOnIM2JCn0lowEqajrYJSKlZIpnFtJoW1pVkis3nG4tMOpGr0ulW2ff5bDD4xKNMwmZ&#10;LrcD8Eu7ZKn5zDOQUbmlcUbTPqmaLbVpSC/UOKhUOCcnGyc1giLVpNuD4fs7b4Y2+fH55AgSegzS&#10;tUMEi4Y5wLCF4EpaO3A144r2I1owct86PgzUm10w8ON/sarJqdZaeqfWxGgzX9uTqfzf1Z30xJVL&#10;zkwAAAAASUVORK5CYII=" style="vertical-align: -5px" width="41px" height="21px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">×350=2300+350</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">n</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(0&lt;</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">n</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">≤</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">5</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">n</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">∈</span><b><span style="color: #00008B; font-size: 16px; vertical-align: middle;">N</span></b><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAYAAAAMCAYAAABBV8wuAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAE1JREFU&#10;KFNj/A8EDNgASOL/q/P/i2Y/ADNhgOHK7O7/RbvO/589e8//2QxL/+96BZFiAGvYtRRo3J7/VzB0&#10;AFUXYdOB1Q4UG5GNGkgJABXo+oN0j5GjAAAAAElFTkSuQmCC" style="vertical-align: 5px" width="6px" height="12px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">).　(3</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">分</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="54"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">39.(2) </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">问当地一个农村低保家庭在“十三五”期间至少可获得政府低保补助多少元？</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">3</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">【解】</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(2)</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">一个农村低保家庭在“十三五”期间至少可获得政府低保补助为</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">S</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAYAAAAMCAYAAABBV8wuAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAGZJREFU&#10;KFNj/A8EDNgASAIbYAALXtkD1NUNxHv+X4GqAktcufIAQxNQ4sH/2SDVRef/v0KSZvj/6gNEAGQc&#10;kiTEDih4tWvP/11QbUgSH/7vKkKy/NWupVAXLYWrBvsNvz+wyOLUAQD9fPbYHg2rNQAAAABJRU5E&#10;rkJggg==" style="vertical-align: -6px" width="7px" height="12px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=5×2650+</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAD4AAAAqCAYAAADrhujJAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAspJREFU&#10;aEPtWTtOw0AQDTfLBbgHdWpukJwgrlGoKFJaoaJAoomQLNFQJAVFUoUCiW7ZZ7xe73o/3o/XhHik&#10;SJHsfTNv3sz+fEWoTS7RQPwSbdIX6dPpm6zX733BK3Fd/PVC/HD4IovFS1LSzBn8wr/N1MSPWzKb&#10;zGnvV79sZ8MRnt/c5ASK1xaIp3Z+IvmMxjfbkmPjhf3+k8C/zZTEj/kdJ03JZ4UNhj9frd7Icvkq&#10;DAjBU3vekYyJIhHH+7e3T9Y2UxCnoI4Ks+CgMqrk+fmjEa8/ni3dZUIVxB8f92UcQtVJYG3ixaZW&#10;myuNstoQWXg4blYDCMMhyq02JZ6NUrfnOuLw3xZAxJSIV32j6u+SACevcoqJRcy0Aa8bN+NbOuKs&#10;8kwTrGFW50FzVdFbG9oKczLLT62grq8fSuLGychxzjAx1xHHGMSBeHRmXc4KSnJS97wqGRyarQKm&#10;YEW8MNltxPUi0MRYXaPEQbws9TuSV2tHSUCaWLoQL3HqRDZm52Z7af+L80zvxLOi2+RmLvUqxZS4&#10;y/I4TKlj46FYLnTBsMlNu3NyxLNVo6rqMMZ9cpN3WA6k4bC1nAXi6YnLLcJbEGM8ljNbjs3PWaax&#10;gRjSmABuG5jAiHFCwpZxSMOWGVtnk9lndQ8GOCQIuzcPDN8hUNn7kOLrlI0732NpKPNqvMvFQAyX&#10;8Gfq66aPutSn03vhKFqfxTttLBpn9z/8Pjgy66XHY6jXN8ZIvO8M/zX8UfE0iohbzViHFZ/YEyou&#10;nvDYBeRQ5NMRL7b1Wf5XoepSw/Ni00dl5ToeCuQzPuZtjKv/dIq3IvtV/P+Xukw88qXE2SheZOLl&#10;gWvgoe8PUuryh4hQEj7j0xOnl43CnTwt+dzh25wPSdWYtMQbn5P46a/9aSoWORNOMuLyF1PfT9Cx&#10;kpKMeKyAY+GMxGNl8lxwRsXPRalYcV6s4j/PIaY36gjBlQAAAABJRU5ErkJggg==" style="vertical-align: -20px" width="62px" height="42px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">×350=16750(</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">元</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">).</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：当地一个农村低保家庭在“十三五”期间至少可获得政府低保补助</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">16750</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">元</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">.　(3</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">分</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <h4 class="p_t">
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">40.(</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">本题满分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">8</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，第（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）小题</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">4</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分，第（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）小题</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">4</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">分</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">)</span>
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                    <span style="color: #000000; font-size: 16px; vertical-align: middle;">已知圆</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">C</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">:</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAAMCAYAAACulacQAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAFxJREFU&#10;KFOdjtENwCAIROnKrMMmzMEULEERe63V+OMlJCbvfHBFhnZpcBfqwEKImiFY/e0WdNXEKMnzznWz&#10;0oQDn1eYFoj/0CSk+ysfTEAjAXTluhSDzrJzPPAc3hYyHZnNUzLvAAAAAElFTkSuQmCC" style="vertical-align: 5px" width="7px" height="12px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">+</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAAMCAYAAACulacQAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAFxJREFU&#10;KFOdjtENwCAIROnKrMMmzMEULEERe63V+OMlJCbvfHBFhnZpcBfqwEKImiFY/e0WdNXEKMnzznWz&#10;0oQDn1eYFoj/0CSk+ysfTEAjAXTluhSDzrJzPPAc3hYyHZnNUzLvAAAAAElFTkSuQmCC" style="vertical-align: 5px" width="7px" height="12px"><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">4</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">6</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">+9=0</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">，直线</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">l</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">:</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">tx</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">－</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">+1=0.</span>
                </p>
                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                </p>
            </h4>
            <a name="56"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">40.</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">1</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）</span><span style="color: #000000; font-size: 16px; vertical-align: middle;"> </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">若直线</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">l</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">经过圆</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">C</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">的圆心，求</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">t</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">的值；</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">4</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">【解】　</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（１）圆</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">C</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">的方程</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">x</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAANCAYAAABlyXS1AAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAGVJREFU&#10;KFNj/A8EDLgASBIXYIBIPPg/m6EbaEL3/6JdH+BqwZKvdp3/fwWuaA+UDbQO3cgrs5f+3/UKIoop&#10;CTQFKocmeWXP/9kQ89F0AiUYZj9AsQXqoKVgl8IwTDeGnchaaSQJABQJPVHY240hAAAAAElFTkSu&#10;QmCC" style="vertical-align: 5px" width="7px" height="12px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">+</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">y</span></i><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAANCAYAAABlyXS1AAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAGVJREFU&#10;KFNj/A8EDLgASBIXYIBIPPg/m6EbaEL3/6JdH+BqwZKvdp3/fwWuaA+UDbQO3cgrs5f+3/UKIoop&#10;CTQFKocmeWXP/9kQ89F0AiUYZj9AsQXqoKVgl8IwTDeGnchaaSQJABQJPVHY240hAAAAAElFTkSu&#10;QmCC" style="vertical-align: 5px" width="7px" height="12px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">－</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">4</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">－</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">6</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">y</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">+9=0</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，可化为</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">(</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">x</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">－</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2)</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAANCAYAAABlyXS1AAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAGVJREFU&#10;KFNj/A8EDLgASBIXYIBIPPg/m6EbaEL3/6JdH+BqwZKvdp3/fwWuaA+UDbQO3cgrs5f+3/UKIoop&#10;CTQFKocmeWXP/9kQ89F0AiUYZj9AsQXqoKVgl8IwTDeGnchaaSQJABQJPVHY240hAAAAAElFTkSu&#10;QmCC" style="vertical-align: 5px" width="7px" height="12px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">+(</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">y'</span></i>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">－</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">3)</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAcAAAAMCAYAAACulacQAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAG1JREFU&#10;KFNj/A8EDLgASPL//wf/ZzN0AxV1/y/a9QEiBNIEIl7tOv//ClzRHigbKglXCmRcmb30/65XSDpR&#10;JIGmQOUgxsLBlT3/Z0PMR9gJZgElGGY/QFELddBSsEthGKYb1VgUfVhciyxPvk4ASJIhbb1dZ94A&#10;AAAASUVORK5CYII=" style="vertical-align: 5px" width="7px" height="12px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=4</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，所以圆</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">C</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">的圆心坐标为（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">3</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">），</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">　</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">分</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">因为直线</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">l</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">经过圆</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">C</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">的圆心，所以</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">t</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">－</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">3+1=0</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，即</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">t</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">=1</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">；</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">　</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">分</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <a name="57"></a>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #000000; font-size: 16px; vertical-align: middle;">40.</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">（</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">2</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">）</span><span style="color: #000000; font-size: 16px; vertical-align: middle;"> </span><span style="color: #000000; font-size: 16px; vertical-align: middle;">若直线</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">l</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">与圆</span><i><span style="color: #000000; font-size: 16px; vertical-align: middle;">C</span></i><span style="color: #000000; font-size: 16px; vertical-align: middle;">有公共点，求直线倾斜角的范围</span><span style="color: #000000; font-size: 16px; vertical-align: middle;">.</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                            <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                        </p>
                        <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                        </p>
                    </div>
                    <div class="row clearfix"></div>
                </div>
                <div class="panel-footer question_info">
                    <div class="row">
                        <div class="col-xs-2">
                            <div class="question_info_heading">【分值】</div>
                            <div class="score">4</div>
                        </div>
                        <div class="col-xs-6">
                            <div class="question_info_heading">【标准答案】</div>
                            <div class="anwser">
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">答：</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">【解】</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">）因为直线</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">l</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">与圆</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">C</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">有公共点，所以圆</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">C</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">的圆心到直线</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">l</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">的距离</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">d</span></i>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">小于等于圆</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">C</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">的半径</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">r</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，即</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADgAAAAzCAYAAADCQcvdAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAmpJREFU&#10;aEPtWktuwyAQ9dFyJl+jR2AdqWewcgdvOEDWXuUGFLBpYBjMH5OWSJWqGjPz5s0HXjoxx2eavlyP&#10;uvy7y99pAOySL9upwSCMyT+twScjvPkI8OKH0Jr5G2erQIq+2DI/mMK0LfeKIONt5QOkK1s2nTHh&#10;BGeSPE0at5WR5ZVHbagtzUo+QMRlSgBADm6e7iAQeVjV25YtsG0FgDuDqg6lA9Xq07SFhaw8QMHW&#10;vDI9awXIOTc9Me8RW6FdP/kkQwlMRdH1aqWnf9+iDIoOCkeE7KqAUcbMVq+nMP77u0srhjBbdVOU&#10;Psw05Omz0L1GiqcnagtvWmUY5AbtyPOoy+65R58Sm4WkPuqyFXn7Ca5BNdgtgGIOas6UON2c2qoF&#10;MImFhi+VSdGGDseaGgBDB2dsZFutHwz+eQZvt29kxpkHaP9JpI/1Aov6OOdgq9qpbWcADI3wVenr&#10;868Yg72qcAOgLwXE817Zk76FAPCt8QN8X3yL3xk9zjUBuC3roacKoIXui76oH8+bANR9sbWcQE8T&#10;l2UD9Ken6RnlbBr6cZDjh8hsaT7+l9sC5Df/sxu/uMnbNaoJV10BhBK+kDWgzA8IwAHui3DV7ioG&#10;gYQPNRYXix8BMEfC7w6gq8GkSvgfAjBUwj86o/aFDXpgPxrLJTWIMZjqiGokrpNO6r5ZY8IGmCfh&#10;95+imRL+GUDZvGrOQVUfv1oH9p9QmRK+d9DLevV/laZPx6AU1Yv/FKB/7p6uOGMwdesggDooVXex&#10;Z9BUB3PfGwBhBGEt5ka49vtRDEoJ4BjMtR0rtf8AWCqSV+0TzeBVjqba/QHMz8Uor98QjQAAAABJ&#10;RU5ErkJggg==" style="vertical-align: -28px" width="56px" height="51px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">≤</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">　</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">分</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;">解得</span><i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">t</span></i><span style="color: #00008B; font-size: 16px; vertical-align: middle;">≥</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">0</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">，所以直线倾斜角的范围是</span><img border="0px" hspace="0px" vspace="0px" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADcAAAArCAYAAADczxCmAAAAAXNSR0IArs4c6QAAAARnQU1BAACx&#10;jwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAedJREFU&#10;aEPtmUGygyAMhn0365m8xjsC6868MzDewQ0H6NpVb0AJAgVLS0LBQZ7MdFFFyMdPQqI/UrWh1wZw&#10;vbahVzC9I7uHG4Zf5Xfh7yjQMdvhmlPO/jkKUMrOEy61Qq3eP5XbX5mbZJHgpoPGOMsFaVCTygk2&#10;SaEAFj5JDiTLLMdhvUZpTcJZAMGuK5yYSIrZ5xuGu0s+GrX2gFv41R3yjLpHKPsJ+gKQ2Yp6XoKv&#10;0ZXzJpNydfh6gKCaCh7spu304QQvHlDWyQKYzK2CElAHEG8+8x98iLKgOJ/TgxvnttbFrqEs368T&#10;Di7Ykta42lvz+0U44fzI5dZz6xffL3TxEXDK7eRzl8vfSz35rk6D69D/U8PByddomXv2FJfnw4BI&#10;uG0K9CaYuJBNzwNrQOPh7GFqsvX4eWOz+c2xUcNyxJgkOMR4qovawgyfReDGzOtVGA6UK6VaWNNR&#10;MhN6bpm3eJlPeRWB5w5UwMLKZbJsHxPzWse5FibS2FnahItYL9izSugMLlKVIAiPoRycn1WLVcRK&#10;1eri3qUQJ2heOUjzqFGy8aPAmKfqyJHfn3qp7ckJ727aVU4XyNuvTrSctUk4/y1bAGheGGFdr0k4&#10;rPGpfv8LrtsvqymZj3q//w/+R1UmZXfXyj0AiUpZqzl+jLAAAAAASUVORK5CYII=" style="vertical-align: -18px" width="55px" height="43px"><span style="color: #00008B; font-size: 16px; vertical-align: middle;">.　</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">（</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">2</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">分</span><span style="color: #00008B; font-size: 16px; vertical-align: middle;">)</span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                    <span style="color: #00008B; font-size: 16px; vertical-align: middle;"></span>
                                </p>
                                <p style="text-align: justify; margin-top: 0px; margin-bottom: 0px; text-indent: 0px; margin-left: 0px; margin-right: 0px;">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
