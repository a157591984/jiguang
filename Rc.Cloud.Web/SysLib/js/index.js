/**
 * Created by Admin on 2017/4/11.
 */
$(function () {
    //配置layer
    if (isInclude('layer.js')) {
        layer.ready(function () {
            layer.config({
                extend: 'bootcss/css/style.css',
                skin: 'layer-ext-bootcss',
                shade: 0.75
            });
        });
    }
});

//文件是否被引用
function isInclude(name) {
    var js = /js$/i.test(name);
    var es = document.getElementsByTagName(js ? 'script' : 'link');
    for (var i = 0; i < es.length; i++)
        if (es[i][js ? 'src' : 'href'].indexOf(name) != -1) return true;
    return false;
}