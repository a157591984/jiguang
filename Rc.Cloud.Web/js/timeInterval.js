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