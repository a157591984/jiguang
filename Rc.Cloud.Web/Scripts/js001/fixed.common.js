/**
 * Created by 孟祥涵 on 2015-07-17.
 */
;(function($,window,document){

//    悬浮
    $.fn.fixedBox = function(options){
        //定义属性
        var _this = this,
            _default = {
                'fixed':_this.attr('data-location'), //固定位置
                'scorllTop':$(window).scrollTop(),  //滚动条滚动高度
                'warpOffsetTop':_this.parent('div').offset().top,    //当前元素的父元素距离页面顶部高度
                'windowHeight':$(window).height()   //窗口高度
            },
            _options = $.extend({},_default,options);
    //    定义函数
        var _fixedBox = {
            //固定顶部
            fixedTop : function(){
                //滚动页面高度大于当前元素的父元素距离页面顶部高度时，则将当前元素固定
                if(_options.scorllTop > _options.warpOffsetTop){
                    _this.addClass('fixed_header');
                }else{
                    _this.removeClass('fixed_header');
                }
            },
            //固定底部
            fixedBottom : function(){
                //当前元素的父元素距离页面顶部高度大于窗口高度时，则将当前元素固定到底部
                if(_options.warpOffsetTop > _options.windowHeight){
                    _this.addClass('fixed_footer');
                }
                //滚动页面高度大于等于当前元素的父元素距离页面顶部高度时，则取消当前元素固定
                if(_options.scorllTop + _options.windowHeight >= _options.warpOffsetTop){
                    _this.removeClass('fixed_footer');
                }else{
                    _this.addClass('fixed_footer');
                }
            }
        };
        if(_options.fixed == 'top'){
            return _fixedBox.fixedTop();
        }else if(_options.fixed == 'bottom'){
            return _fixedBox.fixedBottom();
        };
    };

})(jQuery,window,document);
