/**
 * Created by Admin on 2017/4/11.
 * mtree v.2.0 by MengXianghan
 */
+(function ($) {
    var defaults = {
        openIcon: '<i class="material-icons">&#xE146;</i>',
        closeIcon: '<i class="material-icons">&#xE909;</i>',
        checkbox: false,//复选框：true=显示，false=隐藏
        checkboxLinkage: true,//复选框上下级联动
        checkboxName: 'mtree',
        opera: false,//操作：true=显示，false=隐藏
        data: [],//数据
        html: true,//是否直接解析html，data不需传参
        indent: 15,//缩进
        display: 1,//默认显示几级：true=全部显示，1=显示1级，2/3/4/5依次类推
        activeId: '',//选中菜单id
        activeClass: 'active',//选中菜单样式
        speed: 100,//动画速度
        url: false,//启用url：true=启用(点击不执行收缩)，false=禁用（点击执行收缩）
        onLoad: function (obj) { },//加载完成执行
        onClick: function (obj, url) { },
        onAdd: function (obj, name) { },
        onDel: function (obj, data) { },
        onDone: function (obj, name, id) { },
    };
    var mtree = {
        init: function () {
            this.$ele = $(this);
            mtree.unsubscribeEvents.call(this);
            mtree.initData.call(this);
        },
        unsubscribeEvents: function () {
            this.$ele.off('click');
        },
        //初始化数据
        initData: function () {
            if (!this.ops.html) {
                this.$ele.html('');
            }
            mtree.genData.call(this, this.$ele, 0, 1);
            mtree.initIdent.call(this);
            mtree.inintShow.call(this);
            mtree.initCurrent.call(this);
            mtree.initBtnStatus.call(this);
            mtree.clearRedundancy.call(this);
        },
        //赋值
        genData: function ($parent, pid, level) {
            var self = this;
            var data = self.ops.data;
            var $ul = $(mtree.template.ul).attr('data-level', level++);
            $.each(data, function (i, item) {
                if (item.pid == pid) {
                    var $li = $(mtree.template.li);
                    var $link = $(mtree.template.link.replace(/\[expand\]/, item.expand)).attr('title', item.name).attr('data-url', item.url).attr('data-id', item.id);
                    var $indent = $(mtree.template.indent);
                    var $btn = (item.btn == 'false') ? '' : $(mtree.template.btn).html(self.ops.openIcon);
                    var $checkbox = self.ops.checkbox ? $(mtree.template.checkbox.replace(/\{value\}/, item.value).replace(/\{checkboxName\}/, mtree.ops.checkboxName)) : '';
                    var $icon = (item.icon) ? $(mtree.template.icon).html(item.icon) : '';
                    var $name = $(mtree.template.name).html(item.name);
                    var $add = $(mtree.template.add);
                    var $edit = $(mtree.template.edit);
                    var $del = $(mtree.template.del);
                    var $opera = $(mtree.template.opera).append($add).append($edit).append($del);
                    var $str = $li.append($link.append($indent).append($btn).append($checkbox).append($icon).append($name).append($opera));
                    $ul.append($str);
                    mtree.genData.call(self, $str, item.id, level);
                }
            });
            if ($ul.html().length > 0) {
                $parent.append($ul);
            }
        },
        //初始化缩进
        initIdent: function () {
            var self = this;
            $('.mtree-link-hook', self.$ele).each(function (e) {
                var $closestUl = $(this).closest('ul');
                var $prev = $closestUl.prev('.mtree-link-hook');
                var indent = $prev.length ? parseInt($prev.find('.mtree-indent-hook').css('width').replace('px', '')) + self.ops.indent : 0;
                var level = indent / self.ops.indent + 1;
                if (!$closestUl.data('level')) {
                    $closestUl.attr('data-level', level);
                }
                $('.mtree-indent-hook', $(this)).css({
                    width: indent
                });
            })
        },
        //清理冗余
        clearRedundancy: function () {
            $('.mtree-link-hook', self.$ele).each(function (e) {
                //移除href
                $(this).removeAttr('href');
                //清理冗余ul
                if ($(this).next('ul').length > 0 && $(this).next('ul').children('li').length == 0) {
                    $(this).next('ul').remove();
                }
                //清理冗余按钮
                if ($(this).next('ul').length == 0) {
                    $(this).find('.mtree-btn-hook').html('');
                }
            })
        },
        //初始化按钮状态
        initBtnStatus: function () {
            var self = this;
            $('.mtree-link-hook', self.$ele).each(function () {
                if ($(this).next('ul').is(':hidden')) {
                    $(this).find('>.mtree-btn-hook')
                        .html(self.ops.openIcon);
                } else {
                    $(this).find('>.mtree-btn-hook')
                        .html(self.ops.closeIcon);
                }
            })
        },
        //初始化激活状态
        initCurrent: function () {
            if (this.ops.activeId) {
                this.$ele.find('.mtree-link-hook[data-id="' + this.ops.activeId + '"]')
                    .addClass(this.ops.activeClass)
                    //.parents('li')
                    //.addClass(this.ops.activeClass)
                    .parents('ul')
                    .show();
            }
        },
        //初始化显示
        inintShow: function () {
            var self = this;
            $('ul', self.$ele).each(function () {
                var level = $(this).data('level');
                if (self.ops.display === true || level <= self.ops.display) {
                    $(this).show();
                } else if (level > self.ops.display) {
                    $(this).hide();
                }
            });
            $('.mtree-link-hook', self.$ele).each(function () {
                if ($(this).hasClass(self.ops.activeClass)) {
                    $(this).parents('ul').show();
                }
            })
        },
        //mtree-link事件
        linkClick: function () {
            var self = this;
            var openIcon = this.ops.openIcon;
            var closeIcon = this.ops.closeIcon;
            self.$ele.on('click', '.mtree-link-hook', function (e) {
                var $child = $(this).next('ul');
                var $btn = $(this).find(' > .mtree-btn-hook');
                var url = $(this).data('url');
                self.find('.mtree-link-hook')
                    .removeClass(self.ops.activeClass)
                $(this)
                    .addClass(self.ops.activeClass)
                if (!self.ops.url) {
                    if ($child.size()) {
                        if ($child.is(':hidden')) {
                            //显示子集
                            $child.slideDown(self.ops.speed);
                            //改变按钮状态
                            $btn.html(closeIcon);
                        } else {
                            //隐藏子集
                            $child.slideUp(self.ops.speed);
                            //改变按钮状态
                            $btn.html(openIcon);
                        }
                    }
                    self.ops.onClick.call(self, $(this));
                } else {
                    self.ops.onClick.call(self, $(this));
                }
                e.preventDefault();
                e.stopPropagation();
            })
        },
        //展开/关闭按钮事件
        btnClick: function () {
            var self = this;
            var openIcon = this.ops.openIcon;
            var closeIcon = this.ops.closeIcon;
            self.$ele.on('click', '.mtree-btn-hook', function (e) {
                var $child = $(this).closest('.mtree-link-hook').next('ul');
                if ($child.size()) {
                    if ($child.is(':hidden')) {
                        //显示子集
                        $child.slideDown(self.ops.speed);
                        //改变按钮状态
                        $(this).html(closeIcon);
                    } else {
                        //隐藏子集
                        $child.slideUp(self.ops.speed);
                        //改变按钮状态
                        $(this).html(openIcon);
                    }
                }
                e.preventDefault();
                e.stopPropagation();
            })
        },
        //复选框
        checkbox: function () {
            var self = this;
            if (self.ops.checkbox) $('.mtree-checkbox-hook').show();
            self.$ele.on('click change', 'input:checkbox', function (e) {

                if (self.ops.checkboxLinkage) {
                    //关联上级
                    mtree._checkedPrev.call(self, $(this));
                    //关联下级
                    mtree._checkedNext.call(self, $(this));
                }

                //e.preventDefault();
                e.stopPropagation();
            })
        },
        //复选框关联下级
        _checkedNext: function (obj) {
            var self = this;
            obj.closest('.mtree-link-hook')
                .next('ul')
                .find('input:checkbox')
                .prop('checked', obj.is(':checked'));
        },
        //复选框关联上级
        _checkedPrev: function (obj) {
            var self = this;
            var checkedLen = obj.closest('ul').find('input:checkbox:checked').length;
            var checkbox = obj.closest('ul').prev('.mtree-link-hook').find('input:checkbox');
            var checkboxLen = checkbox.length;
            if (checkboxLen > 0) {
                if (checkedLen > 0) {
                    checkbox.prop('checked', true);
                } else {
                    checkbox.prop('checked', false);
                }
                mtree._checkedPrev.call(self, checkbox);
            }
        },
        //操作
        opera: function () {
            var self = this;
            if (this.ops.opera) $('.mtree-opera-hook').show();
            self.$ele.on('click', '.mtree-form-hook', function (e) {
                e.stopPropagation();
            })
        },
        //添加
        add: function () {
            var self = this;
            self.$ele.on('click', '.mtree-add-hook', function (e) {
                //var name = '新建文件夹';
                var $mtreeLink = $(this).closest('.mtree-link-hook');
                var hasNext = $mtreeLink.next('ul').length;
                var currectLevel = parseInt($(this).closest('ul').data('level'));
                var $li = $(mtree.template.li);
                var $link = $(mtree.template.link.replace(/\{expand\}/, '')).attr('data-url', '').attr('data-id', '');
                var $indent = $(mtree.template.indent);
                var $btn = $(mtree.template.btn);
                var $checkbox = self.ops.checkbox ? $(mtree.template.checkbox.replace(/\{value\}/, '').replace(/\{checkboxName\}/, self.ops.checkboxName)) : '';
                var $name = $(mtree.template.name).html(mtree.template.form.replace(/\{value\}/, ''));
                var $add = $(mtree.template.add);
                var $edit = $(mtree.template.edit);
                var $del = $(mtree.template.del);
                var $opera = self.ops.opera ? $(mtree.template.opera).append($add).append($edit).append($del).show() : '';
                var $str = $li.append($link.addClass('active').append($indent).append($btn).append($checkbox).append($name).append($opera));
                var mtreeEditFormLen = self.$ele.find('.mtree-form-hook').length;
                if (mtreeEditFormLen) {
                    $('.mtree-form-hook').find('.mtree-input-name-hook').focus();
                } else {
                    self.$ele.find('.mtree-link-hook').removeClass('active')
                    if (!hasNext) {
                        var $ul = $(mtree.template.ul).attr('data-level', currectLevel + 1).show();
                        $mtreeLink.after($ul);
                    }
                    $mtreeLink.next('ul').show().append($str).find('.mtree-input-name-hook').focus();
                    mtree.initIdent.call(self);
                    mtree.initCurrent.call(self);
                    mtree.initBtnStatus.call(self);
                    mtree.clearRedundancy.call(self);
                    self.ops.onAdd.call(self, $mtreeLink, name);
                }
                e.stopPropagation();
            })
        },
        //编辑
        edit: function () {
            var self = this;
            self.$ele.on('click', '.mtree-edit-hook', function (e) {
                var $mtreeLink = $(this).closest('.mtree-link-hook');
                var disabled = $(this).data('disabled');
                var $mtreeName = $mtreeLink.find('.mtree-name-hook');
                var mtreeName = $mtreeName.text();
                var form = mtree.template.form.replace(/\{value\}/, mtreeName);
                var $mtreeEditForm = self.$ele.find('.mtree-form-hook');
                var mtreeEditFormLen = $mtreeEditForm.length;
                if (mtreeEditFormLen) {
                    $mtreeEditForm.find('.mtree-input-name-hook').focus();
                } else {
                    if (!disabled) {
                        //禁用编辑按钮
                        $(this).data('disabled', true);
                        //保存旧名称
                        $mtreeName.data('name', mtreeName);
                        //插入编辑表单
                        $mtreeName.html(form).find('.mtree-input-name-hook').focus();
                    }
                }
                e.stopPropagation();
            })
        },
        //完成编辑
        doneEdit: function () {
            var self = this;
            self.$ele.on('click', '.mtree-done-hook', function (e) {
                var $mtreeLink = $(this).closest('.mtree-link-hook');
                var mtreeName = $mtreeLink.find('.mtree-input-name-hook').val();
                var $mtreeName = $(this).closest('.mtree-name-hook');
                var $mtreeEdit = $mtreeLink.find('.mtree-edit-hook');
                var id = $mtreeLink.data('id');
                var pid = $(this).closest('ul').prev('.mtree-link-hook').data('id');
                if (mtreeName) {
                    $mtreeName.html(mtreeName);
                    $mtreeEdit.data('disabled', false);
                    mtree.opera.call(self);
                    self.ops.onDone.call(self, $mtreeLink, mtreeName, id, pid);
                } else {
                    $mtreeLink.find('.mtree-input-name-hook').focus();
                }
                e.stopPropagation();
            })
        },
        //取消编辑
        cancleEdit: function () {
            var self = this;
            self.$ele.on('click', '.mtree-cancle-hook', function (e) {
                var $mtreeLink = $(this).closest('.mtree-link-hook');
                var $mtreeName = $(this).closest('.mtree-name-hook');
                var $mtreeEdit = $mtreeLink.find('.mtree-edit-hook');
                var mtreeName = $mtreeName.data('name');
                if (mtreeName) {
                    $mtreeName.html(mtreeName);
                    $mtreeName.data('name', '');
                    $mtreeEdit.data('disabled', false);
                } else {
                    var $closestUl = $(this).closest('ul');
                    $(this).closest('li').remove();
                    if ($closestUl.find('li').length == 0) $closestUl.remove();
                    mtree.clearRedundancy.call(self);
                }
                e.stopPropagation();
            })
        },
        //删除
        del: function () {
            var self = this;
            self.$ele.on('click', '.mtree-del-hook', function (e) {
                var $closestUl = $(this).closest('ul');
                var data = [];
                $(this).closest('li').find('.mtree-link-hook').each(function () {
                    data.push($(this).data('id'));
                })
                $(this).closest('li').remove();
                if ($closestUl.find('li').length == 0) $closestUl.remove();
                mtree.clearRedundancy.call(self);
                self.ops.onDel.call(self, $(this), data);
                e.stopPropagation();
            })
        },
        //模板
        template: {
            ul: '<ul></ul>',
            li: '<li></li>',
            link: '<div class="mtree_link mtree-link-hook" {expand}></div>',
            indent: '<div class="mtree_indent mtree-indent-hook"></div>',
            btn: '<div class="mtree_btn mtree-btn-hook"></div>',
            checkbox: '<div class="mtree_checkbox mtree-checkbox-hook"><input type="checkbox" name="{checkboxName}" value="{value}"></div>',
            icon: '<div class="mtree_icon mtree-icon-hook"></div>',
            name: '<div class="mtree_name mtree-name-hook"></div>',
            opera: '<div class="mtree_opera mtree-opera-hook"></div>',
            add: '<div class="mtree_add mtree-add-hook"><i class="material-icons">&#xE145;</i></div>',
            edit: '<div class="mtree_edit mtree-edit-hook"><i class="material-icons">&#xE150;</i></div>',
            del: '<div class="mtree_del mtree-del-hook"><i class="material-icons">&#xE872;</i></div>',
            form: '<div class="mtree_form mtree-form-hook"><input class="form-control mtree_input_name mtree-input-name-hook" type="text" value="{value}"><span class="mtree_done mtree-done-hook"><i class="material-icons">&#xE876;</i></span><span class="mtree_cancle mtree-cancle-hook"><i class="material-icons">&#xE5CD;</i></span></div>'
        }
    }
    $.fn.mtree = function (ops) {
        this.ops = $.extend({}, defaults, ops);
        mtree.init.call(this);
        mtree.linkClick.call(this);
        mtree.btnClick.call(this);
        if (this.ops.checkbox) mtree.checkbox.call(this);
        if (this.ops.opera) {
            mtree.opera.call(this);
            mtree.add.call(this);
            mtree.edit.call(this);
            mtree.doneEdit.call(this);
            mtree.cancleEdit.call(this);
            mtree.del.call(this);
        }
        this.ops.onLoad.call(this, this.$ele);
    }
})(jQuery);
