// JavaScript Document
//调用方式
//<img src="02.jpg" width="800" height="500" onload="javascript:zoomimg(this,800,500)" />
/*
onload="javascript:zoomimg(this,800,500)"
onload="javascript:zoomimg(this)"
*/

function zoomimg(imgz, widthz, heightz) {
    //参数(图片,允许的宽度,允许的高度)
    var widthz = imgz.width;
    var heightz = imgz.height;
    var image = new Image();
    image.src = imgz.src;
    if (image.width > 0 && image.height > 0) {
        if (image.width / image.height >= widthz / heightz) {
            if (image.width > widthz) {
                imgz.width = widthz;
                imgz.height = (image.height * widthz) / image.width;
                imgz.style.marginTop = (heightz - imgz.height) / 2 + "px";
            } else {
                imgz.width = image.width;
                imgz.height = image.height;
                imgz.style.marginTop = (heightz - imgz.height) / 2 + "px";
                //imgz.style.marginLeft=(widthz-imgz.width)/2 + "px";
            }
        } else {
            if (image.height > heightz) {
                imgz.height = heightz;
                imgz.width = (image.width * heightz) / image.height;
                //imgz.style.marginLeft=(widthz-imgz.width)/2 + "px";
            } else {
                imgz.width = image.width;
                imgz.height = image.height;
                imgz.style.marginTop = (heightz - imgz.height) / 2 + "px";
                //imgz.style.marginLeft=(widthz-imgz.width)/2 + "px";
            }
        }
    }
}
