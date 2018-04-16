<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recordingComment.aspx.cs" Inherits="Rc.Cloud.Web.teacher.recordingComment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>语音批注</title>
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../js/common.js"></script>
    <script src="../js/json2.js"></script>
    <script src="../../plugin/layer/layer.js"></script>
    <script src="../plugin/jplayer/jquery.jplayer.min.js"></script>
    <link href="../plugin/jplayer/skin/blue.monday/jplayer.blue.monday.css" rel="stylesheet" />
    <script type="text/javascript">
        $.ajaxWebServiceN = function (url, dataMap, fnSuccess, fnError, asyncT) {
            layer.ready(function () {
                if (asyncT != false) {
                    asyncT = true;
                }
                var idx = '';
                $.ajax({
                    type: "POST",
                    async: asyncT,
                    contentType: "application/json; charset=utf-8",
                    url: url,
                    data: dataMap,
                    dataType: "json",
                    beforeSend: function () {
                        idx = layer.load();
                    },
                    success: fnSuccess,
                    complete: function () {
                        layer.close(idx);
                    },
                    error: fnError
                });
            });
        }

        //添加事件
        $(function () {

            setRecording();

            var min = 0;
            var sec = 0;
            var ms = 0;
            var timer = null;

            $("#btnStart").click(function () {
                var _recordingPath = "<%=recordingPath%>";
                var _tips = "确定要开始录音吗？";
                if (_recordingPath != "") {
                    _tips += "<br/>此操作会删除原录音，且不可恢复。请谨慎操作！";
                }
                var idx = layer.confirm(_tips, {
                    title: false,
                    closeBtn: false,
                    shade: [0.3, '#393D49']
                }, function () {
                    layer.close(idx);
                    $("#btnStart").attr("disabled", "disabled");
                    $("#btnStop").removeAttr("disabled");
                    clearInterval(timer);
                    timer = setInterval(show, 10)

                    var dto = {
                        hwCTime: "<%=hwCTime%>",
                        stuHwId: "<%=stuHwId%>",
                        tqId: "<%=tqId%>",
                        x: Math.random()
                    };

                    $.ajaxWebServiceN("recordingComment.aspx/StartRecording", JSON.stringify(dto), function (data) {
                        if (data.d == "0") {
                            layer.msg("正在录音", { time: 1000, icon: 1 });
                        }
                    }, function () {
                        layer.msg("异常，请重试", { time: 2000, icon: 2 }, function () {
                            $("#btnStart").removeAttr("disabled");
                            $("#btnStop").attr("disabled", "disabled");
                            clearInterval(timer);
                            resetTime();
                        });
                    });
                });

            });
            $("#btnStop").click(function () {
                clearInterval(timer);

                $.ajaxWebServiceN("recordingComment.aspx/StopRecording", "{x:'" + Math.random() + "'}", function (data) {
                    layer.msg("录音完成", { time: 1000, icon: 1 }, function () { window.location.reload(); });
                }, function () {
                    layer.msg("异常，请重试", { time: 2000, icon: 2 }, function () {
                        $("#btnStart").removeAttr("disabled");
                        $("#btnStop").attr("disabled", "disabled");
                        resetTime();
                    });
                });
            });
            $("#btnDel").click(function () {
                var idx = layer.confirm('确定要删除录音吗？', {
                }, function () {
                    layer.close(idx);
                    var dto = {
                        hwCTime: "<%=hwCTime%>",
                        stuHwId: "<%=stuHwId%>",
                        tqId: "<%=tqId%>",
                        x: Math.random()
                    };

                    $.ajaxWebServiceN("recordingComment.aspx/DelRecording", JSON.stringify(dto), function (data) {
                        if (data.d == "0") {
                            layer.msg("删除成功", { time: 1000, icon: 1 }, function () { window.location.reload(); });
                        }
                        else {
                            layer.msg("删除失败", { time: 2000, icon: 2});
                        }
                    }, function () {
                        layer.msg("删除异常", { time: 1000, icon: 2 }, function () { window.location.reload(); });
                    });
                });
            });

            //生成时间
            function show() {
                ms++;
                if (sec == 60) {
                    min++; sec = 0;
                }
                if (ms == 100) {
                    sec++; ms = 0;
                }
                var msStr = ms;
                if (ms < 10) {
                    msStr = "0" + ms;
                }
                var secStr = sec;
                if (sec < 10) {
                    secStr = "0" + sec;
                }
                var minStr = min;
                if (min < 10) {
                    minStr = "0" + min;
                }
                timeStr = minStr + ":" + secStr + "." + msStr;
                $('#showtime').html(timeStr);

                //$('#showtime span:eq(0)').html(minStr);
                //$('#showtime span:eq(2)').html(secStr);
                //$('#showtime span:eq(4)').html(msStr);
            };
            //重置时间
            function resetTime() {
                min = 0;
                sec = 0;
                ms = 0;
                $('#showtime').html('');

            }

            //播放器
            function setRecording() {
                var _recordingPath = "<%=recordingPath%>";
                if (_recordingPath != "") {
                    $("#btnDel").removeAttr("disabled");
                    if (isBrowser() == "IE") {
                        $("#objectPlayer").show();
                    }
                    else {
                        $("#jp_container_1").show();
                        $("#jquery_jplayer_1").jPlayer({
                            ready: function (event) {
                                $(this).jPlayer("setMedia", {
                                    title: "",
                                    wav: _recordingPath
                                });
                            },
                            swfPath: "../plugin/jplayer",
                            supplied: "wav",
                            wmode: "window",
                            useStateClassSkin: true,
                            autoBlur: false,
                            smoothPlayBar: true,
                            keyEnabled: true,
                            remainingDuration: true,
                            toggleDuration: true
                        });
                    }
                }
            }
            function isBrowser() {
                var Sys = {};
                var ua = navigator.userAgent.toLowerCase();
                var s;
                (s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :
                (s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :
                (s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :
                (s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :
                (s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;
                if (ua.indexOf('trident') > -1) {//Js判断为IE浏览器 
                    return "IE";
                }
                if (Sys.firefox) {//Js判断为火狐(firefox)浏览器 
                    return "Firefox";
                }
                if (Sys.chrome) {//Js判断为谷歌chrome浏览器 
                    return "Chrome";
                }
                if (Sys.opera) {//Js判断为opera浏览器 
                    return "Opera";
                }
                if (Sys.safari) {//Js判断为苹果safari浏览器
                    return "Safari";
                }
            }

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="button" id="btnStart" value="开始录音" />
            <input type="button" id="btnStop" value="停止录音" disabled="disabled" />
            <input type="button" id="btnDel" value="删除录音" disabled="disabled" />
            <div id="showtime">
            </div>
        </div>
        <br />
        <div id="jquery_jplayer_1" class="jp-jplayer"></div>
        <div id="jp_container_1" class="jp-audio" role="application" aria-label="media player" style="display: none;">
            <div class="jp-type-single">
                <div class="jp-gui jp-interface">
                    <div class="jp-controls">
                        <button class="jp-play" role="button" tabindex="0">play</button>
                        <button class="jp-stop" role="button" tabindex="0">stop</button>
                    </div>
                    <div class="jp-progress">
                        <div class="jp-seek-bar">
                            <div class="jp-play-bar"></div>
                        </div>
                    </div>
                    <div class="jp-volume-controls">
                        <button class="jp-mute" role="button" tabindex="0">mute</button>
                        <button class="jp-volume-max" role="button" tabindex="0">max volume</button>
                        <div class="jp-volume-bar">
                            <div class="jp-volume-bar-value"></div>
                        </div>
                    </div>
                    <div class="jp-time-holder">
                        <div class="jp-current-time" role="timer" aria-label="time">&nbsp;</div>
                        <div class="jp-duration" role="timer" aria-label="duration">&nbsp;</div>
                        <div class="jp-toggles">
                            <button class="jp-repeat" role="button" tabindex="0">repeat</button>
                        </div>
                    </div>
                </div>
                <div class="jp-details" style="display: none;">
                    <div class="jp-title" aria-label="title">&nbsp;</div>
                </div>
                <div class="jp-no-solution">
                    <span>Update Required</span>
                    To play the media you will need to either update your browser to a recent version or update your <a href="http://get.adobe.com/flashplayer/" target="_blank">Flash plugin</a>.
	
                </div>
            </div>
        </div>
        <object id="objectPlayer" height="100" width="260" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6" style="display: none;">
            <param name="AutoStart" value="0">
            <!--是否自动播放-->
            <param name="Balance" value="0">
            <!--调整左右声道平衡,同上面旧播放器代码-->
            <param name="enabled" value="-1">
            <!--播放器是否可人为控制-->
            <param name="EnableContextMenu" value="-1">
            <!--是否启用上下文菜单-->
            <param name="url" value="<%=recordingPath%>">
            <!--播放的文件地址-->
            <param name="PlayCount" value="1">
            <!--播放次数控制,为整数-->
            <param name="rate" value="1">
            <!--播放速率控制,1为正常,允许小数,1.0-2.0-->
            <param name="currentPosition" value="0">
            <!--控件设置:当前位置-->
            <param name="currentMarker" value="0">
            <!--控件设置:当前标记-->
            <param name="defaultFrame" value="">
            <!--显示默认框架-->
            <param name="invokeURLs" value="0">
            <!--脚本命令设置:是否调用URL-->
            <param name="baseURL" value="">
            <!--脚本命令设置:被调用的URL-->
            <param name="stretchToFit" value="0">
            <!--是否按比例伸展-->
            <param name="volume" value="100">
            <!--默认声音大小0%-100%,50则为50%-->
            <param name="mute" value="0">
            <!--是否静音-->
            <param name="uiMode" value="Full">
            <!--播放器显示模式:Full显示全部;mini最简化;None不显示播放控制,只显示视频窗口;invisible全部不显示-->
            <param name="windowlessVideo" value="0">
            <!--如果是0可以允许全屏,否则只能在窗口中查看-->
            <param name="fullScreen" value="0">
            <!--开始播放是否自动全屏-->
            <param name="enableErrorDialogs" value="-1">
            <!--是否启用错误提示报告-->
            <param name="SAMIStyle" value>
            <!--SAMI样式-->
            <param name="SAMILang" value>
            <!--SAMI语言-->
            <param name="SAMIFilename" value>
            <!--字幕ID-->
        </object>
    </form>
</body>
</html>
