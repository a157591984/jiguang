$(function () {

    //缩进
    $('.catelog ul').each(function () {
        var level = $(this).attr('data-level');
        $(this).find('div').css({
            'padding-left': level * 15 + 'px'
        });
    });

    //当前选中的栏目
    $('.catelog ul li .catelog_name').live('click', function () {
        //取消重命名
        $('#rename_res_btn').click();
        //添加当前状态
        $('.catelog ul li .catelog_name').removeClass('active')
        $(this).addClass('active');        
    })

    //获取下级栏目
    $('.catelog ul li .catelog_name').live('click', function (e) {
        var plus = $(this).children('i.plus');//加减icon
        var folder = $(this).children('i.folder');//文件夹icon
        var ul = $(this).closest('div').next('ul');//下级目录box
        var level = parseInt(ul.attr('data-level'));//下级目录等级

        //移除"新建文件夹"
        $('#build').remove();

        //如果点击的是全部，则停止执行
        if ($(this).hasClass('all_catelog')) {
            return;
        }


        if (plus.hasClass('fa-minus-square')) {
            //关闭
            plus.removeClass('fa-minus-square').addClass('fa-plus-square');
            folder.removeClass('fa-folder-open').addClass('fa-folder');
            //清空下级
            plus.closest('div').next('ul').html('');
        } else if (plus.hasClass('fa-plus-square')) {
            //展开
            plus.removeClass('fa-plus-square').addClass('fa-minus-square');
            folder.removeClass('fa-folder').addClass('fa-folder-open');

            /*此处写处理过程*/

            var inner = '';
            inner += '<li>';
            inner += '<div class="catelog_name" style="padding-left:' + level * 15 + 'px">';
            inner += '<i class="fa fa-plus-square plus"></i>';
            inner += '<i class="fa fa-folder folder"></i>';
            inner += '<span>这里新增的目录</span>';
            inner += '</div>';
            inner += '<ul data-level="' + (level + 1) + '"></ul>'
            inner += '</li>';
            ul.html(inner);//输出内容

        }
    });

    //新建文件夹
    $('#buildBtn').live('click', function () {
        if (!$('#build').length) {
            var level = parseInt($('.active').next('ul').attr('data-level'));
            var inner = '';
            //更改当前选中的plus
            if (!$('.active').find('.plus').hasClass('fa-minus-square')) {
                $('.active').click();
                //$('.active').find('.plus').removeClass('fa-plus-square').addClass('fa-minus-square');
                //$('.active').find('.folder').removeClass('fa-folder').addClass('fa-folder-open');
            }
            inner += '<li id="build">';
            inner += '<div style="padding-left:' + level * 15 + 'px">';
            inner += '<i class="fa plus"></i>';
            inner += '<i class="fa fa-folder folder"></i>';
            inner += '<span>';
            inner += '<input type="text" id="cateNameTxt" placeholder="新建文件夹">';
            inner += '<i class="fa fa-check sub_btn" id="sub_btn"></i>';
            inner += '<i class="fa fa-close res_btn" id="res_btn"></i>';
            inner += '</span>';
            inner += '</div>';
            inner += '</li>';
            $('.active').next('ul').append(inner);
        }
    });

    //确定新建文件夹
    $('#sub_btn').live('click', function () {
        var catelogName = $('#cateNameTxt').val();//当前文件夹名称
        var level = parseInt($('#build').closest('ul').attr('data-level'));//等级
        var inner = '';
        if (!catelogName.length) {
            alert('名称不能为空');
            return;
        }
        //清除当前选中栏目
        $('.catelog ul li .catelog_name').removeClass('active');

        /*此处写处理过程*/

        inner += '<li>';
        inner += '<div class="catelog_name active" style="padding-left:' + level * 15 + 'px">';
        inner += '<i class="fa plus"></i>';
        inner += '<i class="fa fa-folder folder"></i>';
        inner += '<span>' + catelogName + '</span>';
        inner += '</div>';
        inner += '<ul data-level="' + (level + 1) + '"></ul>'
        inner += '</li>';
        //添加新增的栏目
        $('#build').after(inner);
        //删除build
        $('#build').remove();


    });

    //取消新建
    $('#res_btn').live('click', function () {
        if ($('#build').closest('ul').find('li').length <= 1) {
            $('.active').find('.plus').removeClass('fa-minus-square');
            $('.active').find('.folder').removeClass('fa-folder-open').addClass('fa-folder');
        }
        $('#build').remove();
    });

    //重命名
    $('#rename').live('click', function () {
        var currentName = $('.active').find('span').text();//当前名称
        var inner = '';
        //清空当前内容
        $('.active').find('span').html('');
        //添加input
        inner += '<input type="text" id="cateNameTxt" data-name="' + currentName + '" value="' + currentName + '">';
        inner += '<i class="fa fa-check sub_btn" id="rename_sub_btn"></i>';
        inner += '<i class="fa fa-close res_btn" id="rename_res_btn"></i>';
        $('.active').find('span').html(inner);
        //重置div的class
        $('.active').removeClass();
    });

    //提交重命名
    $('#rename_sub_btn').live('click', function () {
        var catelogName = $('#cateNameTxt').val();//当前文件夹名称
        var level = parseInt($('#build').closest('ul').attr('data-level'));//等级
        var inner = '';
        if (!catelogName.length) {
            alert('名称不能为空');
            return;
        }
        inner += catelogName;
        $(this).closest('div').addClass('catelog_name active');
        $('.active').find('span').html(inner);


    });

    //取消重命名
    $('#rename_res_btn').live('click', function () {
        var catelogName = $('#cateNameTxt').attr('data-name');//初始文件夹名称
        var level = parseInt($('#build').closest('ul').attr('data-level'));//等级
        var inner = '';
        if (!catelogName.length) {
            alert('名称不能为空');
            return;
        }
        inner += catelogName;
        $(this).closest('div').addClass('catelog_name active');
        $('.active').find('span').html(inner);
    });


    //删除
    $('#del').live('click', function () {
        if (confirm('此操作不可恢复！确认执行？')) {

            /*此处写处理过程*/

            $('.active').closest('li').remove();
        }
    });


});
