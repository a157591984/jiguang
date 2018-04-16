$(function () {
    var $_TreeName = $('.tree');//盒子class
    var $_TreeBtn = $('.tree_btn');//按钮class
    var ShowLevel = $_TreeName.attr('show-level');//设置显示几级：0=全部显示，1=显示一级，2=显示两级...
    var ShowIcon = 'fa-angle-right';//隐藏时显示的icon图标
    var HideIcon = 'fa-angle-down';//显示时显示的icon图标

    /**
	 * [设置显示几级]
	 * @return {[type]}                                    [description]
	 */
    $_TreeName.find('ul').each(function () {
        if ($(this).attr('data-level') >= ShowLevel) {
            $(this).hide();
        }
    })

    /**
	 * [设置ICON图标]
	 * @return {[type]}           [description]
	 */
    $_TreeBtn.each(function (e) {
        var $_NameDiv = $(this).closest('div');
        var $_IsNull = $_NameDiv.next('ul').text();//是否存在下级
        var $_IsHidden = $_NameDiv.next('ul').is(':hidden');//下级是否隐藏
        if ($_IsNull) {
            if (!$_IsHidden) {
                $(this).addClass(HideIcon);
            } else {
                $(this).addClass(ShowIcon);
            }
        }
    })


    /**
	 * [状态切换]
	 * @return {[type]}           [description]
	 */
    $_TreeName.find('i').on({
        "click": function () {
            var $_This = $(this);
            var $_ClassName = $_This.hasClass(ShowIcon);//当前状态：true=隐藏，false=显示
            var $_CurrentLevel = $_This.closest('div').next('ul');
            if ($_ClassName) {
                //1.显示下级
                $_CurrentLevel.slideDown(150);
                //2.改变icon
                $_This.removeClass(ShowIcon).addClass(HideIcon);
            } else {
                //1.隐藏下级
                $_CurrentLevel.slideUp(150);
                //2.改变icon
                $_This.removeClass(HideIcon).addClass(ShowIcon);
            }
        }
    })


    /**
	 * [点击设置默认状态]
	 * @return {[type]}           [description]
	 */
    $_TreeName.find('.name a').on({
        'click': function () {
            var $_ThisParent = $(this).closest('.tree');
            var disabled = $(this).data('disabled');
            if (!disabled) {
                $_ThisParent.find('.name').each(function () {
                    //$(this).removeClass('active');
                    $(this).closest('div').removeClass('active');
                });
                //$(this).addClass('active');
                $(this).closest('div').addClass('active');
            } else {
                $(this).closest('div').find($_TreeBtn).click();
            }
        }
    })



})