$(function () {

    ////树状菜单
    //$('[data-type="tree"] ul').each(function () {
    //    //得到level
    //    var level = $(this).attr('data-level');
    //    $(this).find('div').css({
    //        'padding-left': level * 10 + 'px'
    //    });
    //    $(this).find('div').each(function () {
    //        if ($(this).next('ul').length <= 0) {
    //            $(this).find('i.plus').removeClass('fa-caret-down');
    //        }
    //    });
    //});

    ////展开/折叠
    //$('[data-type="tree"] i.plus').on('click', function () {
    //    if ($(this).hasClass('fa-caret-down')) {
    //        $(this).closest('div').next('ul').slideUp(100);
    //        $(this).removeClass('fa-caret-down').addClass('fa-caret-right')
    //    } else if ($(this).hasClass('fa-caret-right')) {
    //        $(this).closest('div').next('ul').slideDown(100);
    //        $(this).removeClass('fa-caret-right').addClass('fa-caret-down')
    //    }
    //});

    ////左侧菜单默认点击后的样式
    //$('[data-type="tree"] a').on({
    //    click: function () {
    //        $('[data-type="tree"] a').each(function () {
    //            $(this).closest('div').css({
    //                'background': ''
    //            });
    //        })
    //        $(this).closest('div').css({
    //            'background': '#C8E3CE'
    //        });
    //    }
    //});

    ////默认第一个选中
    ////$('[data-type="tree"] a:eq(0)').closest('div').css({ 'background': '#fff' });

    ////左侧菜单划过切换
    //$('.left_tree > dl dt').hover(function () {
    //    $(this).children('ul').stop().show(150);
    //    $(this).children('a').addClass('active');
    //}, function () {
    //    $(this).children('ul').stop().hide(150);
    //    $(this).children('a').removeClass('active');
    //});

});


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
