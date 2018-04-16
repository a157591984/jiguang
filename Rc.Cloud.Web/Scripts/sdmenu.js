
$(function () {

    var menu = $("#my_menu");


    menu.children("span").unbind('click').click(function () {
        var _this = $(this);
        _this.next("div:eq(0)").toggle();
        SetCookie(this.id, _this.next("div:eq(0)").css('display'));
        if (_this.next("div")[0].style.display == 'block') {
            //            _this.style.backgroundImage = 'url(/images/images003/expand1.gif)';
            $(this).css("backgroundImage", "url(/images/images003/expand1.gif)");
        }
        else {
            //            _this.style.backgroundImage = 'url(/images/images003/expand2.gif)';
            $(this).css("backgroundImage", "url(/images/images003/expand2.gif)");
        }
    });

    var menuDiv = $("#my_menu");
    var menulist = menuDiv.children("span");
    var str = "";
    for (var i = 0; i < menulist.length; i++) {
        var strcookie = GetCookie(menulist[i].id);
        if (strcookie != null && strcookie != "") {
            if (strcookie.indexOf("none") >= 0) {
                $(menulist[i]).next("div:eq(0)").hide();
                $(menulist[i]).css("backgroundImage", "url(/images/images003/expand2.gif)");
            }
        }
    }

});
