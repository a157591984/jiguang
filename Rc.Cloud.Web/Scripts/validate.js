
//网站公用js验证
//字符串验证前，可根据情况过滤两边空格
var js_validate = new Object();
js_validate.IsEmpty = function (str_input) {
    if (str_input == null) {
        return 0;
    }
    return str_input.length == 0;
};
//整数
js_validate.IsInt = function (str_input) {
    var pattern=/^-?\d+$/;
    return js_validate.IsMatch(str_input, pattern);
};
//非负整数
js_validate.IsPositiveInt = function (str_input) {
    var pattern = /^\d+$/;
    return js_validate.IsMatch(str_input, pattern);
};
//浮点数
js_validate.IsFloat = function (str_input) {
    var pattern = /^\d+(.\d+)?$/;
    return js_validate.IsMatch(str_input, pattern);
};
//时间[时:分:秒]
js_validate.IsTime = function (str_input) {
    var pattern = /^([0-1]\d|2[0-3]):[0-5]\d(:[0-5]\d)?$/;
    return js_validate.IsMatch(str_input, pattern); ;
};
//时间[时:分]
js_validate.IsHourMinute = function (str_input) {
    var pattern = /^([0-1]?\d|2?[0-3])(:|：)[0-5]?\d$/;
    return js_validate.IsMatch(str_input, pattern); ;
};
//日期 从1900年开始
js_validate.IsDateTime = function (str_input) {
    //年份-（大月-日）|（非2小月-日）|（2月-日）
    var pattern = /^19\d\d-((0?1|3|5|7|8)|(10|12)-(1\d|2\d|3[0-1]))|((0?2|4|6|9)|11-(1\d|2\d|30))|(0?2-(1\d|2\d))$/;
    return js_validate.IsMatch(str_input, pattern);
};
//手机号码8-20
js_validate.IsMobileNum = function (str_input) {
    var pattern = /^\d{8,20}$/;
    return js_validate.IsMatch(str_input, pattern);
};
js_validate.IsMobileNum2 = function (str_input) {
    var regexMobile = /^13[0-9]{1}[0-9]{8}$|^15[9]{1}[0-9]{8}$|^1[3,5]{1}[0-9]{1}[0-9]{8}$|^147[0-9]{8}$|^18[0-9][0-9]{8}$/; //手机
    return js_validate.IsMatch(str_input, regexMobile);
}
//电话号码6-30
js_validate.IsTelNum = function (str_input) {
    var pattern = /^(\d{2,4}-)?(\d{2,6}-)?\d{7,12}(-\d{1,6})?$/;
    return js_validate.IsMatch(str_input, pattern);
};

//Email 2011-12-27 modbyli
js_validate.IsEmail = function (str_input) {
    //var pattern = /^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/;
    var pattern = /^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return js_validate.IsMatch(str_input, pattern);
};
//邮编
js_validate.IsZipCode = function (str_input) {
    var pattern = /^\d{6}$/;
    return js_validate.IsMatch(str_input, pattern);
};

//身份证号15 
js_validate.is15Number = function (str_input) {
    var pattern = /^d{8}((0[1-9])|(1[012]))(0[1-9]|[12][0-9]|3[01])d{3}$/;
    return js_validate.IsMatch(str_input, pattern);
}
//身份证号18
js_validate.is18Number = function (str_input) {
    var pattern = /^(\d{6})(18|19|20)?(\d{2})([01]\d)([0123]\d)(\d{3})(\d|X)?$/;
    return js_validate.IsMatch(str_input, pattern);
}

//字符长度
js_validate.IsLength = function (str_input, min_length, max_length) {
    return js_validate.LengthBetween(str_input, min_length, max_length);
};
//数字取值范围
js_validate.IsNumBetween = function (str_input, min_length, max_length) {
    var pattern = /^\d+$/;
    if (js_validate.IsMatch(str_input, pattern)) {
        return js_validate.LengthBetween(str_input, min_length, max_length);
    }
    else {
        return false;
    }
};


//长度范围 范围为空则匹配任何长度
js_validate.LengthBetween = function (str_input,min_length, max_length) {
    var leng = str_input.length;
    if (min_length != null) {
        if (leng < min_length) {
            return false;
        }
    }
    if (max_length != null) {
        if (leng > max_length) {
            return false;
        }
    }
    return true;
}
//自定义正则
js_validate.IsMatch = function (str_input, pattern) {
    return pattern.test(str_input);
}


