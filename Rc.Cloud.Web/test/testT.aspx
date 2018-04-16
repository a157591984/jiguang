<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testT.aspx.cs" Inherits="Rc.Cloud.Web.test.testT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%--    <script type="text/javascript" src="../Scripts/js001/jquery.min-1.8.2.js"></script>
    <script type="text/javascript" src="../Scripts/plug-in/layer/layer.js"></script>
    <script src="/Scripts/Scripts/jquery-1.8.3.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>select2</p>
            <select id="sel_menu3" class="js-data-example-ajax form-control" multiple="multiple">
            </select>
            <input type="button" value="取值" id="btnGetSelect2Value" />
        </div>
        <div>
            <asp:Literal runat="server" ID="ltlCommonDict"></asp:Literal>
        </div>
        <div class="row fileupload-buttonbar">
            <div class="span7">
                <div class="well">
                    <i class="icon-plus"></i><span>&nbsp;&nbsp;Add files...</span>
                    <input type="file" id="fileupload" name="fileupload" accept="*/*" multiple="multiple" />
                    <button id="btnUploadAll" class="btn btn-success pull-right" type="button">
                        Upload All</button>
                    <asp:Button runat="server" ID="btnDDD" Text="测试测试" OnClick="btnDDD_Click" />
                    <div class="clearfix">
                    </div>
                    <div class="progress">
                        <div class="bar" id="overallbar" style="width: 0%">
                        </div>
                    </div>
                </div>
            </div>
            <div class="span7">
                <div class="well hide" id="filelistholder">
                </div>
            </div>
            <div class="span7">
            </div>
        </div>
        <div>
            音频：<audio src="/images/1.mp3" controls="controls" preload="auto">
                您的浏览器不支持 audio 标签。
            </audio>
            <hr />
            视频：<video src="/images/1_0.mp4" controls="controls" preload="auto">
                your browser does not support the video tag
            </video>
        </div>
        <p>
            <img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAABV4AAACXCAYAAADtYkBQAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAB42SURBVHhe7d1tltu2kgDQ/MwS3lq8IK8nq/FmshhPwzGcSg0+SUpsyfeeUyMRVQDYVL9z2DW08sd3AAAAAOAp/ve//33/448/xJOjXPdn03gFAAAAgCcpTUCe747r7pMGAAAAgCfReL2Hxusnd8UHdHaN3flX/lLtrHXlvsXV6408c69HuOP8X/2aXWH3Gjzrms32WTmPK9Z4Nb2faednvWKN6BHX+RFrXimf35lrGms+48/9Gc8JAIDHcO93jzuuu086mH0AZ/PFmQ85zl1dZ2e/XDs7bok1O3vPXLHW6s9TxnNUo1zRyue4Wl5zd494bivREsdzfY4r5LWuWDvPn603q9+d/yhXnceorpcr40fjrLzO7rq92mevER2dl5V1cjxKa69RtOTxVl1vbrSyTkupW40zzs4HAOB1uPe7xx3X3Sed5A+hHPeipTdezfI9cd7OGrv75foj+x6ZM3N2nTy/t97s3I+sU9Tj8tqKM/L83fWeuf/ZvaqyTlwrH+/Kc1fXyudQrczP9bM4Kq8Rj3vRsjvek+t3568q68a18/HIqG51jaJXO1v/aLSs1j3Kzn692jw+O+7pzSuvMbIzYzvOzgcA4HW497vHHdf9DT7pb9+/fly4cvFKfP32c/ig0Yew8gHVmno+s1gR61bnVK360Ro5l/fO0RNzo7pVZ9bIc1fXatWtrjWqW13jX+V3/OvH/23bX++/duuznf3bufHP17Oz78iRdUrNavQczf1//eu3ss7eXufl/db2f97vSKlpRTXKZa3cqH7myNwz+11hZ/9H/nyxbjSnlTszVpTxVmStsbs9+5w+4zUAAHgE9z33uOO6v/gn/ff3v778+8f43399+XERzzZfe674gHbXiPVH9m/NOfpz1Hmr82Pdzp6ldjVW5Nre3Drei1oT5eMqzqtRxfdFPv6Pb18/8uOm60601PFc24osjtX3+bVojf3H3399//LHl+9//f3zeCKv0113QZkb5+fjFav1de0cWWusaeH3oyXunSNr1fRiRa5bnfes35FWXR0b5apy3IuVfNXK92Jkln+0un8831bUmijXrEZLHO/VFK3c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" />
        </p>
        <div>
            <p>关联试卷</p>
            <p>
                <asp:TextBox runat="server" ID="txtTwo" placeholder="双向细目表标识"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtRTRF" placeholder="资源标识 多个,分割"></asp:TextBox>
                <asp:Button runat="server" ID="btnVerify" Text="关联试卷验证" OnClick="btnVerify_Click" />
                结果：<asp:Literal runat="server" ID="ltlTips"></asp:Literal>
            </p>
        </div>
        <div>
            <p>下载文件重命名</p>
            <p>
                <asp:Button runat="server" ID="btnDown" OnClick="btnDown_Click" />
            </p>
        </div>
        <div>
            作业标识<input type="text" id="txthwId" />
            资源标识<input type="text" id="txtrtrfId" />
            <input type="button" id="txtDelHW" value="撤销作业" />
        </div>
        <div>
            <input type="button" id="btnCheckline" value="检测内网是否畅通" />
        </div>
        <br />
        <div>
            加密/解密字符串<br />
            <asp:TextBox runat="server" ID="txtKey" placeholder="字符串" Width="80%"></asp:TextBox><br />
            <asp:TextBox runat="server" ID="txtDeKey" placeholder="加密串" Width="80%"></asp:TextBox><br />
            <asp:Button runat="server" ID="btnEncrypt" Text="加密" OnClick="btnEncrypt_Click" />
            <asp:Button runat="server" ID="btnDeEncrypt" Text="解密" OnClick="btnDeEncrypt_Click" />
        </div>
        <%--<div>word转pdf<asp:Button runat="server" ID="btnWordToPdf" Text="word转pdf" OnClick="btnWordToPdf_Click"/></div>--%>
        <br />
        <br />
        <div>
            <asp:TextBox runat="server" ID="txtRTRFId" placeholder="资源标识"></asp:TextBox><br />
            <asp:Button runat="server" ID="btnDelRes" Text="删除文件" OnClick="btnDelRes_Click" /><br />
            <asp:TextBox runat="server" ID="txtResult" TextMode="MultiLine" Rows="5"></asp:TextBox><br />
        </div>

    </form>
    <script src="../SysLib/js/jquery.min-1.11.1.js"></script>
    <script src="../SysLib/plugin/jquery.fileupload/jquery-ui-1.9.2.js"></script>
    <script src="../SysLib/plugin/jquery.fileupload/jquery.fileupload.js"></script>
    <script src="../SysLib/plugin/jquery.fileupload/jquery.iframe-transport.js"></script>
    <script src="../SysLib/plugin/jquery.fileupload/bootstrap.js"></script>
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />

    <script src="../SysLib/js/function.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>

    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.css" rel="stylesheet" />
    <script src="../SysLib/plugin/select2/dist/js/select2.full.min.js"></script>
    <script src="../SysLib/plugin/select2/dist/js/i18n/zh-CN.js"></script>
    <link href="../SysLib/plugin/select2/dist/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $('#fileupload').fileupload({
                dataType: "json",
                url: "/Ajax/UploadAPI.ashx",
                //limitConcurrentUploads: 1,
                //sequentialUploads: true,
                //progressInterval: 100,
                //maxChunkSize: 100000,
                add: function (e, data) {
                    $('#filelistholder').removeClass('hide');
                    data.context = $('<div />').text(data.files[0].name).appendTo('#filelistholder');
                    $('</div><div class="progress"><div class="bar" style="width:0%"></div></div>').appendTo(data.context);
                    $('#btnUploadAll').click(function () {
                        data.submit();
                    });
                },
                done: function (e, data) {
                    data.context.text(data.files[0].name + '... Completed');
                    $('</div><div class="progress"><div class="bar" style="width:100%"></div></div>').appendTo(data.context);
                },
                progressall: function (e, data) {
                    var progress = parseInt(data.loaded / data.total * 100, 10);
                    $('#overallbar').css('width', progress + '%');
                },
                progress: function (e, data) {
                    var progress = parseInt(data.loaded / data.total * 100, 10);
                    data.context.find('.bar').css('width', progress + '%');
                }
            });
            $("#txtDelHW").click(function () {
                classDisband($("#txthwId").val(), $("#txtrtrfId").val());
            });

            $("#btnCheckline").click(function () {
                checklocalhost1();
            })

        });
        //检测内网是否通
        var checklocalhost = function () {
            $.ajaxWebService("testT.aspx/checklocalhost", "{x:'" + Math.random() + "'}", function (data) {
                if (data.d == "1") {
                    layer.msg('内网通', { icon: 1 });
                    return false;
                }
                else if (data.d == "2") {
                    layer.msg('外网通', { icon: 3 });
                    return false;
                }
                else {
                    layer.msg('异常错误', { icon: 4 });
                    return false;
                }

            }, function () {
                layer.msg('异常错误', { icon: 4 });
                return false;
            });
        }

        var checklocalhost1 = function () {
            $.ajax({
                type: "post",
                async: true,
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                url: "http://192.168.0.104:801/AuthApi/?key=onlinecheck",
                dataType: "text",
                success: function (data) {
                    if (data == "ok") {
                        layer.msg('内网通', { icon: 1 });
                        return false;
                    }
                    else {
                        layer.msg("内网不通", { icon: 2, time: 2000 });
                    }
                }
            });
        }
        var cc = function () {
            //步骤一:创建异步对象
            var ajax = new XMLHttpRequest();
            //步骤二:设置请求的url参数,参数一是请求的类型,参数二是请求的url,可以带参数,动态的传递参数starName到服务端
            ajax.open('get', 'http://192.168.0.104:801/AuthApi/?key=onlinecheck', true);
            //步骤三:发送请求
            ajax.send();
            ajax.getResponseHeader
            //步骤四:注册事件 onreadystatechange 状态改变就会调用
            ajax.onreadystatechange = function () {
                if (ajax.readyState == 4 && ajax.status == 200) {
                    //步骤五 如果能够进到这个判断 说明 数据 完美的回来了,并且请求的页面是存在的
                    console.log(xml.responseText);//输入相应的内容
                    alert(xml.responseText);
                }
            }
        }

        var classDisband = function (hwid, rtrId) {
            var index = layer.confirm("确定要撤销作业吗？作业撤销后系统将清除学生已提交作业的所有相关数据！", { icon: 4, title: '提示' }, function () {
                layer.close(index);//关闭确认弹窗
                $.ajaxWebService("../teacher/cHomework.aspx/DeleteHw", "{HomeWorkId:'" + hwid + "',x:'" + Math.random() + "'}", function (data) {
                    if (data.d == "1") {
                        //loadData();
                        layer.msg('撤销布置作业成功', { icon: 1 });
                    }
                    if (data.d == "2") {
                        layer.msg('撤销布置作业失败', { icon: 2 });
                        return false;
                    }

                }, function () {
                    layer.msg('撤销布置作业失败！', { icon: 2 });
                    return false;
                });
            });
        }
    </script>
    <script type="text/javascript">
        $(function () {

            $("#btnGetSelect2Value").click(function () {
                var _val = $("#sel_menu3").val();
                console.log(_val);
                var _text;
                if (_val != null) {
                    _text = $("#sel_menu3 option[value='" + _val + "']").text();
                    console.log($("#sel_menu3 option[value='" + _val + "']").attr("data-select2-tag"));
                    console.log(_text);
                }
            });

            //远程筛选
            $("#sel_menu3").select2({
                language: "zh-CN",
                tags: true,
                maximumSelectionLength: 1,  //最多能够选择的个数
                placeholder: "选择/输入",
                allowClear: true,
                ajax: {
                    url: "/Ajax/getDataForSelect2.ashx",
                    dataType: 'json',
                    delay: 250,
                    data: function (params) {
                        return {
                            key: params.term, // search term
                            pageIndex: params.page || 1,
                            pageSize: 10
                        };
                    },
                    processResults: function (data, params) {
                        params.pageIndex = params.pageIndex || 1;

                        return {
                            results: data.items,
                            pagination: {
                                more: (params.pageIndex * 10) < data.total_count
                            }
                        };
                    },
                    cache: true
                },
                escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
                minimumInputLength: 0,
                templateResult: formatRepoProvince, // omitted for brevity, see the source of this page
                templateSelection: formatRepoProvince // omitted for brevity, see the source of this page
            });
        });
        function formatRepoProvince(repo) {
            if (repo.loading) return repo.text;
            var markup = "<div>" + repo.text + "</div>";
            return markup;
        }
    </script>
</body>
</html>
