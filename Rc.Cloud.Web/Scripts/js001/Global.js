$(function () {
    ////设置右侧区域最小高度
    //var ScreenHeight = $(window).height();
    //$('[data-name="main-auto"]').css({
    //    'min-height': ScreenHeight - 90 + 'px'
    //});

    //设置高亮
    $('[data-name="Preview"]').on({
        click: function () {
            var $_this = $('[data-name="Preview"]')
            $_this.each(function () {
                //alert(1);
                $_this.removeClass('active');
            })
            $(this).addClass('active');
        }
    })


    //回车触发提交按钮
    $(document).keydown(function (e) {
        if (e.keyCode == 13) {
            //alert(1);
            //$('[data-name="submit"]').click();
            $('#btnSearch').click();
            //$('[data-name="submit"]').trigger('click');
            //document.getElementById("btnSearch").click();
            return false;
        }
    })

    ////气泡
    //$('[data-name="MessageNums"]').each(function () {
    //    //设置导航信息气泡提示
    //    //var BubbleBox = $('[data-name="MessageNums"]');
    //    var MessageNums = $(this).text();
    //    //信息数为空或者等于零的时候隐藏气泡
    //    if (MessageNums == 0 || MessageNums == '') {
    //        $(this).css({
    //            display: 'none'
    //        });
    //    }
    //    //信息数大于99的时候，显示99+
    //    if (MessageNums > 99) {
    //        $(this).text('99+');
    //    }
    //});


    // 全选
    $('[data-name="CheckAll"]').on({
        click: function () {
            var Mark = $(this).attr('data-mark');
            var IsChecked = $(this).is(':checked');
            var Chackbox = $('input[type="checkbox"][name^="' + Mark + '"]:enabled');
            if (IsChecked) {
                Chackbox.prop('checked', true);
            } else {
                Chackbox.prop('checked', false);
            }
        }
    });


    ////问好
    //now = new Date();
    //var hour = now.getHours();
    ////alert(hour);
    //var Greeting;
    //if (hour <= 8) {
    //    Greeting = '，早上好';
    //} else if (hour <= 11) {
    //    Greeting = '，上午好';
    //} else if (hour <= 13) {
    //    Greeting = '，中午好';
    //} else if (hour <= 18) {
    //    Greeting = '，下午好';
    //} else {
    //    Greeting = '，晚上好';
    //}
    //$('[data-name="Greeting"]').append(Greeting);


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

})


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

//Highcharts.theme
Highcharts.theme = {
    colors: ['#1BB674', '#28A9E3', '#FF7D63', '#FFCA51', '#EF4349', '#2B908F', '#7798BF'],
    title: {
        text: '',
        style: {
            fontFamily: '"Microsoft YaHei",arial'
        }
    },
    subtitle: {
        text: '',
        style: {
            fontFamily: '"Microsoft YaHei",arial'
        }
    },
    credits: {
        enabled: false
    },
    legend: {
        verticalAlign: 'top',
        itemStyle: {
            fontWeight: "normal",
            fontFamily: '"Microsoft YaHei",arial'
        },
    },
    tooltip: {
        style: {
            fontFamily: '"Microsoft YaHei",arial',
            fontWeight: "normal",
            textShadow: "none"
        },
        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
            '<td style="padding:0"><b>{point.y}</b></td></tr>',
        footerFormat: '</table>',
        shared: true,
        useHTML: true
    },
    plotOptions: {
        column: {
            dataLabels: {
                style: {
                    fontFamily: '"Microsoft YaHei",arial',
                    fontWeight: "normal",
                    textShadow: "none"
                }
            }
        },
        line: {
            dataLabels: {
                style: {
                    fontFamily: '"Microsoft YaHei",arial',
                    fontWeight: "normal",
                    textShadow: "none"
                }
            }
        },
        spline: {
            dataLabels: {
                style: {
                    fontFamily: '"Microsoft YaHei",arial',
                    fontWeight: "normal",
                    textShadow: "none"
                }
            }
        },
        bar: {
            dataLabels: {
                style: {
                    fontFamily: '"Microsoft YaHei",arial',
                    fontWeight: "normal",
                    textShadow: "none"
                }
            }
        }
    },
    s: {
        style: {
            fontFamily: '"Microsoft YaHei",arial'
        }
    },
    xAxis: {
        title: {
            style: {
                fontFamily: '"Microsoft YaHei",arial'
            }
        },
        labels: {
            style: {
                fontFamily: '"Microsoft YaHei",arial'
            }
        }
    },
    yAxis: {
        title: {
            style: {
                fontFamily: '"Microsoft YaHei",arial'
            }
        },
        labels: {
            style: {
                fontFamily: '"Microsoft YaHei",arial'
            }
        },
        allowDecimals: false
    }
}
var highchartsOptions = Highcharts.setOptions(Highcharts.theme);