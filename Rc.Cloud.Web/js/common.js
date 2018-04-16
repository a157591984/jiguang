$(function () {
    //拓展toggle
    $.fn.toggler = function (fn, fn2) {
        var args = arguments, guid = fn.guid || $.guid++, i = 0,
            toggler = function (event) {
                var lastToggle = ($._data(this, "lastToggle" + fn.guid) || 0) % i;
                $._data(this, "lastToggle" + fn.guid, lastToggle + 1);
                event.preventDefault();
                return args[lastToggle].apply(this, arguments) || false;
            };
        toggler.guid = guid;
        while (i < args.length) {
            args[i++].guid = guid;
        }
        return this.click(toggler);
    };

    //配置layer
    if (isInclude('layer.js')) {
        layer.ready(function () {
            layer.config({
                extend: 'bootstrap/css/style.css',
                skin: 'layer-ext-bootstrap',
                shade: 0.75
            });
        });
    }


    //回车触发提交按钮
    $(document).keydown(function (e) {
        if ($('#btnSearch').length > 0 && e.keyCode == 13) {
            $('#btnSearch').click();
            return false;
        }
    });

    // 全选
    $('[data-name="CheckAll"]').on({
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

    $('[data-name="rowSwitch"]').toggler(function () {
        $(this).html('收起').next('[data-name="rowItem"]').children('ul').height('auto');
    }, function () {
        $(this).html('展开').next('[data-name="rowItem"]').children('ul').height('');
    });


    // 选项卡
    $('[data-tab-bar]').on({
        click: function () {
            var TabName = $(this).attr('data-tab-bar'); //选项卡标识
            var Index = $(this).index(); //当前选项卡标识
            var TabBox = $('[data-tab-box="' + TabName + '"]');
            var TabBar = $('[data-tab-bar="' + TabName + '"]');
            //设置按钮状态
            TabBar.each(function () {
                $(this).removeClass('active');
            })
            $(this).addClass('active');
            Eindex = $(this).index();
            //设置内容状态
            TabBox.each(function () {
                $(this).css({
                    display: 'none'
                });
            })
            TabBox.eq(Index).css({
                display: 'block'
            });
        }
    });
    //设置默认选项卡
    $('[data-tab-bar]').each(function (e) {
        var TabName = $(this).attr('data-tab-bar'); //选项卡标识
        var TabBox = $('[data-tab-box="' + TabName + '"]'); //选项卡内容
        var TabBar = $('[data-tab-bar="' + TabName + '"]'); //选项卡按钮
        TabBar.each(function (e) {
            if ($(this).hasClass('active')) {
                TabBox.eq(e).siblings('[data-tab-box="' + $(this).attr('data-tab-bar') + '"]').hide();
                TabBox.eq(e).show();
            }
        })
    })

});

//文件是否被引用
function isInclude(name) {
    var js = /js$/i.test(name);
    var es = document.getElementsByTagName(js ? 'script' : 'link');
    for (var i = 0; i < es.length; i++)
        if (es[i][js ? 'src' : 'href'].indexOf(name) != -1) return true;
    return false;
}

//获取最近一年月份
function getMonth() {
    var myDate = new Date();
    var change = function (obj) {
        return (obj < 10) ? "0" + obj : obj;
    }
    var cDate = myDate.getFullYear() + '-' + change(myDate.getMonth() + 1);
    var str = '<li><a href="javascript:;" ajax-value="" class="active">全部</a></li>';
    str += '<li><a href="javascript:;" ajax-value="' + cDate + '">' + cDate + '</a></li>';
    for (var i = 1; i < 12; i++) {
        myDate.setMonth(myDate.getMonth() - 1);
        var fDate = myDate.getFullYear() + "-" + change(myDate.getMonth() + 1);
        str += '<li><a href="javascript:;" ajax-value="' + fDate + '">' + fDate + '</a></li>';
    }
    $('[data-name="month"]').html(str);
}

//获取最近两年季度
function getQuarter() {
    var myDate = new Date();
    var change = function (mon) {
        if (mon > 0) name = "第1季度";
        if (mon > 3) name = "第2季度";
        if (mon > 6) name = "第3季度";
        if (mon > 9) name = "第4季度";
        return name;
    }
    var cDate = myDate.getFullYear() + '年' + change(myDate.getMonth() + 1);
    var str = '<li class="item"><a href="javascript:;" ajax-value="">全部</a></li>';
    str += '<li><a href="javascript:;" ajax-value="' + cDate + '">' + cDate + '</a></li>';
    for (var i = 1; i < 8; i++) {
        myDate.setMonth(myDate.getMonth() - 3);
        var fDate = myDate.getFullYear() + "年" + change(myDate.getMonth() + 1);
        str += '<li><a href="javascript:;" ajax-value="' + fDate + '">' + fDate + '</a></li>';
    }
    $('[data-name="quarter"]').html(str);
}

//获取最近三年年度
function getYear() {
    var myDate = new Date();
    var change = function (mon) {
        if (mon > 0) name = "上半年";
        if (mon > 6) name = "下半年";
        return name;
    }
    var cDate = myDate.getFullYear() + '年' + change(myDate.getMonth() + 1);
    var str = '<li><a href="javascript:;" ajax-value="">全部</a></li>';
    str += '<li><a href="javascript:;" ajax-value="' + cDate + '">' + cDate + '</a></li>';
    for (var i = 1; i < 6; i++) {
        myDate.setMonth(myDate.getMonth() - 6);
        var fDate = myDate.getFullYear() + '年' + change(myDate.getMonth() + 1);
        str += '<li><a href="javascript:;" ajax-value="' + fDate + '">' + fDate + '</a></li>';
    }
    $('[data-name="year"]').html(str);
}

//预览
function PicPreview(url, title, width, height) {
    if (title == '' || title == undefined) {
        title = '预览';
    }
    if (width == '' || width == undefined) {
        width = '1000px';
    }
    if (height == '' || height == undefined) {
        height = '90%';
    }
    layer.open({
        type: 2,
        title: title,
        area: [width, height],
        content: url //iframe的url
    });
}

//弹出新窗口
//是新窗口而不是新标签页
function OpenNew(url) {
    window.open(url, 'newwindow');
}

//异步请求
$.ajaxWeb = function (url, dataMap, fnSuccess, fnError, asyncT, typeT) {
    layer.ready(function () {
        if (asyncT != false) {
            asyncT = true;
        }
        if (typeT != "GET") {
            typeT = "POST";
        }
        var idx = '';
        $.ajax({
            type: typeT,
            async: asyncT,
            url: url,
            data: dataMap,
            dataType: "text",
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
