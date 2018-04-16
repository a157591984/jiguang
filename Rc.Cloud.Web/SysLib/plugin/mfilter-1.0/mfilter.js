/**
 * Created by Admin on 2017/4/11.
 * filter-v.1.0 by MengXianghan
 */
+(function ($) {
    var defaults = {
        height: 32,//默认高度
        openText: '更多&nbsp;<i class="fa fa-caret-right"></i>',
        closeText: '更多&nbsp;<i class="fa fa-caret-down"></i>',
        currentClass: 'active',
        onClick: function (obj) {
        },
        onLoad: function (obj) {
        }
    };
    var mfilter = {
        init: function () {
            this._$ele = $(this);
            mfilter.onLoad.call(this);
        },
        onLoad: function () {
            var _this = this;
            $('[data-name="mfilterControl"]', this._$ele).each(function () {
                var body = $(this).children('[data-name="mfilterBody"]');
                var itemHeigth = body.children('[data-name="mfilterItem"]').height();
                var $footer = $('[data-name="mfilterFooter"]', $(this));
                if (itemHeigth > _this.ops.height) {
                    body.height(_this.ops.height);
                    if ($footer.size() == 0) {
                        $(this).prepend('<div class="mfilter_footer" data-name="mfilterFooter"><a href="javascript:;" class="btn btn-default btn-sm" data-name="mfilterMore">' + _this.ops.openText + '</a></div>');
                    }
                } else {
                    if ($footer.size() > 0) {
                        $footer.remove();
                    }
                }
            });
        },
        onResize: function () {
            var _this = this;
            $(window).on('resize', function () {
                mfilter.onLoad.call(_this);
            })
        },
        onMoreClick: function () {
            var _this = this;
            this._$ele.off('click').on('click', '[data-name="mfilterMore"]', function () {
                var mfilterBody = $(this).closest('[data-name="mfilterControl"]').find('>[data-name="mfilterBody"]');
                var mfilterBodyHeight = mfilterBody.height();
                if (mfilterBodyHeight == _this.ops.height) {
                    mfilterBody.height('');
                    $(this).html(_this.ops.closeText);
                } else {
                    mfilterBody.height(_this.ops.height);
                    $(this).html(_this.ops.openText);
                }
            })
        },
        onItemClick: function () {
            var _this = this;
            $(_this._$ele.find('[data-name="mfilterItem"]'))
                .off('click')
                .on('click', 'a', function () {
                    if (!$(this).hasClass("disabled")) {
                        $(this).addClass(_this.ops.currentClass).siblings().removeClass(_this.ops.currentClass);
                        _this.ops.onClick.call(_this, $(this));
                    }
                })
        }
    };
    $.fn.mfilter = function (ops) {
        this.ops = $.extend({}, defaults, ops);
        mfilter.init.call(this);
        mfilter.onResize.call(this);
        mfilter.onMoreClick.call(this);
        mfilter.onItemClick.call(this);
        this.ops.onLoad.call(this);
    }
})(jQuery);
