function CustomPageDataValid() { }

CustomPageDataValid.prototype.ErrorBorderColor = "1px solid #cc0000";
CustomPageDataValid.prototype.FilterSqlChars = new Array('alter ', 'update ', 'delete ', 'drop ', 'select ', 'exec ', 'exec(', 'insert ', '\\n', '\\r', '\\t');
CustomPageDataValid.prototype.SpecialHTMLChars = new Array('>', '<', '\'', '"', '%');

//检查必填文本框
CustomPageDataValid.prototype.CheckIsRequired = function (obj, isShowErrorInline) {
	var spanRequired = $("#span_required_check_for_" + $(obj).attr("id"));
	var spanFormatError = $("#span_format_check_for_" + $(obj).attr("id"));
	var isRequired = $(obj).attr("IsRequired");
	spanFormatError.hide();
	spanRequired.hide();
	if (isRequired != undefined && isRequired.toLowerCase() == "true" && $(obj).val() == "") {
		if (isShowErrorInline) {
			spanRequired.show();
		}
		$(obj).css('border', customPageDataValid.ErrorBorderColor);
		return spanRequired.html();
	}
	$(obj).css('border', '');
	return "";
}

//检查最小长度
CustomPageDataValid.prototype.CheckMinLength = function (obj, isShowErrorInline) {
	var minLength = $(obj).attr("MinLength");
	if (minLength) {
		var spanFormatError = $("#span_format_check_for_" + $(obj).attr("id"));
		if ($(obj).val().length < parseInt(minLength)) {
			if (isShowErrorInline) {
				spanFormatError.show();
			}
			$(obj).css('border', customPageDataValid.ErrorBorderColor);
			return spanFormatError.html();
		}
	}
	$(obj).css('border', '');
	return "";
}

//检查最大长度,目前只针对textarea控件有效
CustomPageDataValid.prototype.CheckMaxLength = function (obj, isShowErrorInline) {
	var maxLength = $(obj).attr("TextAreaMaxLength");
	if (maxLength) {
		var spanFormatError = $("#span_format_check_for_" + $(obj).attr("id"));
		if ($(obj).val().length > parseInt(maxLength)) {
			if (isShowErrorInline) {
				spanFormatError.show();
			}
			$(obj).css('border', customPageDataValid.ErrorBorderColor);
			return spanFormatError.html();
		}
	}
	$(obj).css('border', '');
	return "";
}

//检查最大值最小值，支持整数和浮点数
CustomPageDataValid.prototype.CheckValueBetween = function (obj, isShowErrorInline) {
	var maxValue = $(obj).attr("MaxValue");
	var minValue = $(obj).attr("MinValue");
	var spanFormatError = $("#span_format_check_for_" + $(obj).attr("id"));
	if (maxValue) {
		if (Number($(obj).val()) > Number(maxValue)) {
			if (isShowErrorInline) {
				spanFormatError.show();
			}
			$(obj).css('border', customPageDataValid.ErrorBorderColor);
			return spanFormatError.html();
		}
	}
	if (minValue) {
		if (Number($(obj).val()) < Number(minValue)) {
			if (isShowErrorInline) {
				spanFormatError.show();
			}
			$(obj).css('border', customPageDataValid.ErrorBorderColor);
			return spanFormatError.html();
		}
	}
	$(obj).css('border', '');
	return "";
}

//自定义正则表达式检查
CustomPageDataValid.prototype.CheckByCustomRegex = function (obj, isShowErrorInline, customRegex) {
	var spanFormatError = $("#span_format_check_for_" + $(obj).attr("id"));
	var rx = new RegExp(customRegex);
	var matches = rx.test($(obj).val());
	if (!matches) {
		if (isShowErrorInline) {
			spanFormatError.show();
		}
		$(obj).css('border', customPageDataValid.ErrorBorderColor);
		return spanFormatError.html();
	}
	$(obj).css('border', '');
	return "";
}

//特殊HTML字符检查
CustomPageDataValid.prototype.CheckForSpecialHTMLChars = function (obj, isShowErrorInline) {
	var spanFormatError = $("#span_format_check_for_" + $(obj).attr("id"));
	var inputValue = $(obj).val().toString();	
	for (var i = 0; i < customPageDataValid.SpecialHTMLChars.length; i++) {
		if (inputValue.indexOf(customPageDataValid.SpecialHTMLChars[i]) != -1) {
			if (isShowErrorInline) {
				spanFormatError.show();
			}
			$(obj).css('border', customPageDataValid.ErrorBorderColor);
			return spanFormatError.html();
		}
	}
	$(obj).css('border', '');
	return "";
}

//sql特殊字符检查
CustomPageDataValid.prototype.CheckForSqlKeywords = function (obj, isShowErrorInline) {
	var spanFormatError = $("#span_format_check_for_" + $(obj).attr("id"));
	var inputValue = $(obj).val().toString();	
	for (var i = 0; i < customPageDataValid.FilterSqlChars.length; i++) {
		if (inputValue.toLowerCase().indexOf(customPageDataValid.FilterSqlChars[i]) != -1) {
			if (isShowErrorInline) {
				spanFormatError.show();
			}
			$(obj).css('border', customPageDataValid.ErrorBorderColor);
			return spanFormatError.html();
		}
	}
	$(obj).css('border', '');
	return "";
}

//检查checkBoxList必填
CustomPageDataValid.prototype.CheckForCheckBoxList = function (obj, isShowErrorInline) {
	var spanRequired = $("#span_required_check_for_" + $(obj).attr("id"));
	spanRequired.hide();
	var isRequired = $(obj).attr("IsRequired");
	if (isRequired != undefined && isRequired.toLowerCase() == "true") {
		var isFind = false;
		$(obj).find("input[type='checkbox']").each(function () {
			if (document.getElementById($(this).attr('id')).checked ||	//如果有选中的项目
				$(this).attr("disabled")) {								//或者控件的 enabled的值为false，那么
				isFind = true;
				return "";
			}
		});
		if (isFind) {
			spanRequired.hide();
			return "";
		}
		else {
			if (isShowErrorInline) {
				spanRequired.show();
			}
			return spanRequired.html();
		}
	}
	return "";
}

//检查RadioButtonList必填
CustomPageDataValid.prototype.CheckForRadionButtonList = function (obj, isShowErrorInline) {
	var spanRequired = $("#span_required_check_for_" + $(obj).attr("id"));
	spanRequired.hide();
	var isRequired = $(obj).attr("IsRequired");
	if (isRequired != undefined && isRequired.toLowerCase() == "true") {
		var isFind = false;
		$(obj).find("input[type='radio']").each(function () {
			if (document.getElementById($(this).attr('id')).checked ||	//如果有选中的项目
				$(this).attr("disabled")) {								//或者控件的 enabled的值为false，那么
				isFind = true;
				return "";
			}
		});
		if (isFind) {
			spanRequired.hide();
			return "";
		}
		else {
			if (isShowErrorInline) {
				spanRequired.show();
			}
			return spanRequired.html();
		}
	}
	return "";
}

//检查DropDownList必填
CustomPageDataValid.prototype.CheckForDropDownList = function (obj, isShowErrorInline) {
	var spanRequired = $("#span_required_check_for_" + $(obj).attr("id"));
	spanRequired.hide();
	if (!$(obj).attr("disabled")) {
		var isRequired = $(obj).attr("IsRequired");
		if (isRequired != undefined && isRequired.toLowerCase() == "true") {
			if ($(obj).val() != "") {
				return "";
			}
			if (isShowErrorInline) {
				spanRequired.show();
			}
			return spanRequired.html();
		}
	}
	return "";
}

var customPageDataValid = new CustomPageDataValid();

//页面加载时绑定数据检查事件
$(document).ready(function () {
	//input控件
	$("body").find("input[type='text']").each(function () {
		var myInputType = $(this).attr("MyInputType");
		var TextBoxValidType = $(this).attr("TextBoxValidType");
		var maxValue = $(this).attr("MaxValue");
		var minLength = $(this).attr("MinLength");
		var minValue = $(this).attr("MinValue");
		var validationExpression = $(this).attr("ValidationExpression");
		var isFilterSqlChars = $(this).attr("IsFilterSqlChars");
		var isFilterSpecialChars = $(this).attr("IsFilterSpecialChars");
		var isRequired = $(this).attr("IsRequired");
		var showErrorType = $(this).attr("ShowErrorType");
		if (showErrorType != undefined) {
			showErrorType = showErrorType.toLowerCase();
		}
		var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";

		$(this).blur(function () {
			//公共检查部分
			var result = customPageDataValid.CheckIsRequired(this, isShowErrorInline); 	//必填
			if (result == "" && $(this).val() != "") {
				result = customPageDataValid.CheckMinLength(this, isShowErrorInline); 	//最小长度
				//检查值的范围
				if (result == "") {
					result = customPageDataValid.CheckValueBetween(this, isShowErrorInline);
				}
				//自定义正则表达式验证
				if (result == "" && validationExpression) {
					result = customPageDataValid.CheckByCustomRegex(this, isShowErrorInline, validationExpression);
				}
				//过滤特殊字符				
				if (result == "" && isFilterSpecialChars != undefined && isFilterSpecialChars.toLowerCase() == "true") {
					result = customPageDataValid.CheckForSpecialHTMLChars(this, isShowErrorInline);
				}
				//过滤sql
				if (result == "" && isFilterSqlChars != undefined && isFilterSqlChars.toLowerCase() == "true") {
					result = customPageDataValid.CheckForSqlKeywords(this, isShowErrorInline);
				}
			}
		});
	});

	//密码控件
	$("body").find("input[type='password']").each(function () {
		var myInputType = $(this).attr("MyInputType");
		var TextBoxValidType = $(this).attr("TextBoxValidType");
		var maxValue = $(this).attr("MaxValue");
		var minLength = $(this).attr("MinLength");
		var minValue = $(this).attr("MinValue");
		var validationExpression = $(this).attr("ValidationExpression");
		var isFilterSqlChars = $(this).attr("IsFilterSqlChars");
		var isFilterSpecialChars = $(this).attr("IsFilterSpecialChars");
		var isRequired = $(this).attr("IsRequired");
		var showErrorType = $(this).attr("ShowErrorType");
		if (showErrorType != undefined) {
			showErrorType = showErrorType.toLowerCase();
		}
		var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";

		$(this).blur(function () {
			//公共检查部分
			var result = customPageDataValid.CheckIsRequired(this, isShowErrorInline); 	//必填
			if (result == "" && $(this).val() != "") {
				result = customPageDataValid.CheckMinLength(this, isShowErrorInline); 	//最小长度				
				//过滤特殊字符				
				if (result == "" && isFilterSpecialChars != undefined && isFilterSpecialChars.toLowerCase() == "true") {
					result = customPageDataValid.CheckForSpecialHTMLChars(this, isShowErrorInline);
				}
				//过滤sql
				if (result == "" && isFilterSqlChars != undefined && isFilterSqlChars.toLowerCase() == "true") {
					result = customPageDataValid.CheckForSqlKeywords(this, isShowErrorInline);
				}
			}
		});
	});

	//textarea控件
	$("body").find("textarea").each(function () {
		var isFilterSqlChars = $(this).attr("IsFilterSqlChars");
		var isFilterSpecialChars = $(this).attr("IsFilterSpecialChars");
		var showErrorType = $(this).attr("ShowErrorType");
		var validationExpression = $(this).attr("ValidationExpression");
		if (showErrorType != undefined) {
			showErrorType = showErrorType.toLowerCase();
		}
		var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";

		$(this).blur(function () {
			var result = customPageDataValid.CheckIsRequired(this, isShowErrorInline); 	//必填

			if (result == "" && $(this).val() != "") {
				//最小长度
				result = customPageDataValid.CheckMinLength(this, isShowErrorInline); 
				//最大长度
				if (result == "") {
					retuls = customPageDataValid.CheckMaxLength(this, isShowErrorInline);
				}
				//自定义正则表达式验证
				if (result == "" && validationExpression) {
					result = customPageDataValid.CheckByCustomRegex(this, isShowErrorInline, validationExpression);
				}
				//特殊字符
				if (result == "" && isFilterSpecialChars != undefined && isFilterSpecialChars.toLowerCase() == "true") {
					result = customPageDataValid.CheckForSpecialHTMLChars(this, isShowErrorInline);
				}
				//sql关键字
				if (result == "" && isFilterSqlChars != undefined && isFilterSqlChars.toLowerCase() == "true") {
					result = customPageDataValid.CheckForSqlKeywords(this, isShowErrorInline);
				}
			}
		});
	});

	//检查列表控件
	$("body").find("span").each(function () {
		var myInputType = $(this).attr("MyInputType");
		var isRequired = $(this).attr("IsRequired");
		var showErrorType = $(this).attr("ShowErrorType");
		if (showErrorType != undefined) {
			showErrorType = showErrorType.toLowerCase();
		}
		var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";

		if (isRequired != undefined && isRequired.toLowerCase() == "true") {
			switch (myInputType) {
				case "RadioButtonList":
					$(this).click(function () {
						customPageDataValid.CheckForRadionButtonList(this, isShowErrorInline);
					});
					break;
				case "CheckBoxList":
					$(this).click(function () {
						customPageDataValid.CheckForCheckBoxList(this, isShowErrorInline);
					});
					break;
			}
		}
	});
	$("body").find("table").each(function () {
		var myInputType = $(this).attr("MyInputType");
		var isRequired = $(this).attr("IsRequired");
		var showErrorType = $(this).attr("ShowErrorType");
		if (showErrorType != undefined) {
			showErrorType = showErrorType.toLowerCase();
		}
		var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";

		if (isRequired != undefined && isRequired.toLowerCase() == "true") {
			switch (myInputType) {
				case "RadioButtonList":
					$(this).click(function () {
						customPageDataValid.CheckForRadionButtonList(this, isShowErrorInline);
					});
					break;
				case "CheckBoxList":
					$(this).click(function () {
						customPageDataValid.CheckForCheckBoxList(this, isShowErrorInline);
					});
					break;
			}
		}
	});
	$("body").find("ol").each(function () {
		var myInputType = $(this).attr("MyInputType");
		var isRequired = $(this).attr("IsRequired");
		var showErrorType = $(this).attr("ShowErrorType");
		if (showErrorType != undefined) {
			showErrorType = showErrorType.toLowerCase();
		}
		var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";

		if (isRequired != undefined && isRequired.toLowerCase() == "true") {
			switch (myInputType) {
				case "RadioButtonList":
					$(this).click(function () {
						customPageDataValid.CheckForRadionButtonList(this, isShowErrorInline);
					});
					break;
				case "CheckBoxList":
					$(this).click(function () {
						customPageDataValid.CheckForCheckBoxList(this, isShowErrorInline);
					});
					break;
			}
		}
	});
	$("body").find("ul").each(function () {
		var myInputType = $(this).attr("MyInputType");
		var isRequired = $(this).attr("IsRequired");
		var showErrorType = $(this).attr("ShowErrorType");
		if (showErrorType != undefined) {
			showErrorType = showErrorType.toLowerCase();
		}
		var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";

		if (isRequired != undefined && isRequired.toLowerCase() == "true") {
			switch (myInputType) {
				case "RadioButtonList":
					$(this).click(function () {
						customPageDataValid.CheckForRadionButtonList(this, isShowErrorInline);
					});
					break;
				case "CheckBoxList":
					$(this).click(function () {
						customPageDataValid.CheckForCheckBoxList(this, isShowErrorInline);
					});
					break;
			}
		}
	});

	$("body").find("select").each(function () {
		var isRequired = $(this).attr("IsRequired");
		var showErrorType = $(this).attr("ShowErrorType");
		if (showErrorType != undefined) {
			showErrorType = showErrorType.toLowerCase();
		}
		var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";
		if (isRequired != undefined && isRequired.toLowerCase() == "true") {
			$(this).change(function () {
				customPageDataValid.CheckForDropDownList(this, isShowErrorInline);
			});
		}
	});
});

//点击提交按钮时出发的事件
function Phhc_data_check_for_page_on_submit(isOnlyShowFirstError, targetControlId) {
	var summaryResult = "";
	var alertResult = "";
	if (targetControlId == "" || targetControlId == undefined) {
		targetControlId = "body";
	}
	else {
		targetControlId = "#" + targetControlId;
	}

	$(targetControlId).find("input[type='text']").each(function () {
		var myInputType = $(this).attr("MyInputType");
		var maxValue = $(this).attr("MaxValue");
		var minValue = $(this).attr("MinValue");
		var validationExpression = $(this).attr("ValidationExpression");
		var isFilterSpecialChars = $(this).attr("IsFilterSpecialChars");
		var isFilterSqlChars = $(this).attr("IsFilterSqlChars");
		var requiredErrorMessage = $(this).attr("RequiredErrorMessage");
		var FormatErrorMessage = $(this).attr("FormatErrorMessage");
		var isRequired = $(this).attr("IsRequired");
		var showErrorType = $(this).attr("ShowErrorType");
		var minLength = $(this).attr("MinLength");		

		//如果控件禁用那么跳过检查
		var disabled = $(this).attr("disabled");
		if (!disabled) {
			if (showErrorType != undefined) {
				showErrorType = showErrorType.toLowerCase();
			}
			var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";
			var result = "";

			//必填
			if (isRequired != undefined && isRequired.toLowerCase() == "true") {
				result = customPageDataValid.CheckIsRequired(this, isShowErrorInline);
			}
			if (result == "" && $(this).val() != "") {
				//最小长度
				if (result == "") {
					result = customPageDataValid.CheckMinLength(this, isShowErrorInline); 	//最小长度			
				}
				//检查值得范围
				if (result == "") {
					result = customPageDataValid.CheckValueBetween(this, isShowErrorInline);
				}
				//检查正则表达式
				if (result == "" && validationExpression) {
					result = customPageDataValid.CheckByCustomRegex(this, isShowErrorInline, validationExpression)
				}
				//检查特殊字符
				if (result == "" && isFilterSpecialChars != undefined && isFilterSpecialChars.toLowerCase() == "true") {
					result = customPageDataValid.CheckForSpecialHTMLChars(this, isShowErrorInline);
				}
				//检查sql字符
				if (result == "" && isFilterSqlChars != undefined && isFilterSqlChars.toLowerCase() == "true") {
					result = customPageDataValid.CheckForSqlKeywords(this, isShowErrorInline);
				}
			}

			if (result != "") {
				summaryResult += result + "\r\n";
				if (showErrorType != "inline") {
					alertResult += result + "\r\n";
				}
			}
		}
	});

	$(targetControlId).find("input[type='password']").each(function () {
		var myInputType = $(this).attr("MyInputType");
		var maxValue = $(this).attr("MaxValue");
		var minValue = $(this).attr("MinValue");
		var validationExpression = $(this).attr("ValidationExpression");
		var isFilterSpecialChars = $(this).attr("IsFilterSpecialChars");
		var isFilterSqlChars = $(this).attr("IsFilterSqlChars");
		var requiredErrorMessage = $(this).attr("RequiredErrorMessage");
		var FormatErrorMessage = $(this).attr("FormatErrorMessage");
		var isRequired = $(this).attr("IsRequired");
		var showErrorType = $(this).attr("ShowErrorType");
		var minLength = $(this).attr("MinLength");

		//如果控件禁用那么跳过检查
		var disabled = $(this).attr("disabled");
		if (!disabled) {
			if (showErrorType != undefined) {
				showErrorType = showErrorType.toLowerCase();
			}
			var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";
			var result = "";

			//必填
			if (isRequired != undefined && isRequired.toLowerCase() == "true") {
				result = customPageDataValid.CheckIsRequired(this, isShowErrorInline);
			}
			if (result == "" && $(this).val() != "") {
				//最小长度
				if (result == "") {
					result = customPageDataValid.CheckMinLength(this, isShowErrorInline); 	//最小长度			
				}				
				//检查特殊字符
				if (result == "" && isFilterSpecialChars != undefined && isFilterSpecialChars.toLowerCase() == "true") {
					result = customPageDataValid.CheckForSpecialHTMLChars(this, isShowErrorInline);
				}
				//检查sql字符
				if (result == "" && isFilterSqlChars != undefined && isFilterSqlChars.toLowerCase() == "true") {
					result = customPageDataValid.CheckForSqlKeywords(this, isShowErrorInline);
				}
			}

			if (result != "") {
				summaryResult += result + "\r\n";
				if (showErrorType != "inline") {
					alertResult += result + "\r\n";
				}
			}
		}
	});

	$(targetControlId).find("textarea").each(function () {
		var isRequired = $(this).attr("IsRequired");
		var showErrorType = $(this).attr("ShowErrorType");
		var minLength = $(this).attr("MinLength");
		var validationExpression = $(this).attr("ValidationExpression");
		var isFilterSqlChars = $(this).attr("IsFilterSqlChars");
		var isFilterSpecialChars = $(this).attr("IsFilterSpecialChars");
		if (showErrorType != undefined) {
			showErrorType = showErrorType.toLowerCase();
		}
		var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";

		var result = "";
		var disabled = $(this).attr("disabled");
		if (!disabled) {
			if (isRequired != undefined && isRequired.toLowerCase() == "true") {
				result = customPageDataValid.CheckIsRequired(this, isShowErrorInline); 	//必填
			}
			if (result == "" && $(this).val() != "") {
				//最小长度
				if (result == "") {
					result = customPageDataValid.CheckMinLength(this, isShowErrorInline); 	//最小长度			
				}
				//最大长度
				if (result == "") {
					result = customPageDataValid.CheckMaxLength(this, isShowErrorInline); 	//最小长度			
				}
				//检查正则表达式
				if (result == "" && validationExpression) {
					result = customPageDataValid.CheckByCustomRegex(this, isShowErrorInline, validationExpression)
				}
				//检查特殊字符
				if (result == "" && isFilterSpecialChars != undefined && isFilterSpecialChars.toLowerCase() == "true") {
					result = customPageDataValid.CheckForSpecialHTMLChars(this, isShowErrorInline);
				}
				//检查sql字符
				if (result == "" && isFilterSqlChars != undefined && isFilterSqlChars.toLowerCase() == "true") {
					result = customPageDataValid.CheckForSqlKeywords(this, isShowErrorInline);
				}
			}
		}
		if (result != "") {
			summaryResult += result + "\r\n";
			if (showErrorType != "inline") {
				alertResult += result + "\r\n";
			}
		}
	});

	//检查列表控件/////////////////////////////////////////////////////////
	$(targetControlId).find("span").each(function () {
		var myInputType = $(this).attr("MyInputType");
		var isRequired = $(this).attr("IsRequired");
		var showErrorType = $(this).attr("ShowErrorType");
		if (showErrorType != undefined) {
			showErrorType = showErrorType.toLowerCase();
		}
		var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";
		var result = "";
		if (isRequired != undefined && isRequired.toLowerCase() == "true" && myInputType != undefined) {
			switch (myInputType.toLowerCase()) {
				case "radiobuttonlist":
					result = customPageDataValid.CheckForRadionButtonList(this, isShowErrorInline);
					break;
				case "checkboxlist":
					result = customPageDataValid.CheckForCheckBoxList(this, isShowErrorInline);
					break;
			}
		}
		if (result != "") {
			summaryResult += result + "\r\n";
			if (showErrorType != "inline") {
				alertResult += result + "\r\n";
			}
		}
	});
	$(targetControlId).find("table").each(function () {
		var myInputType = $(this).attr("MyInputType");
		var isRequired = $(this).attr("IsRequired");
		var showErrorType = $(this).attr("ShowErrorType");
		if (showErrorType != undefined) {
			showErrorType = showErrorType.toLowerCase();
		}
		var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";
		var result = "";
		if (isRequired != undefined && isRequired.toLowerCase() == "true" && myInputType != undefined) {
			switch (myInputType.toLowerCase()) {
				case "radiobuttonlist":
					result = customPageDataValid.CheckForRadionButtonList(this, isShowErrorInline);
					break;
				case "checkboxlist":
					result = customPageDataValid.CheckForCheckBoxList(this, isShowErrorInline);
					break;
			}
		}
		if (result != "") {
			summaryResult += result + "\r\n";
			if (showErrorType != "inline") {
				alertResult += result + "\r\n";
			}
		}
	});
	$(targetControlId).find("ol").each(function () {
		var myInputType = $(this).attr("MyInputType");
		var isRequired = $(this).attr("IsRequired");
		var showErrorType = $(this).attr("ShowErrorType");
		if (showErrorType != undefined) {
			showErrorType = showErrorType.toLowerCase();
		}
		var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";
		var result = "";
		if (isRequired != undefined && isRequired.toLowerCase() == "true" && myInputType != undefined) {
			switch (myInputType.toLowerCase()) {
				case "radiobuttonlist":
					result = customPageDataValid.CheckForRadionButtonList(this, isShowErrorInline);
					break;
				case "checkboxlist":
					result = customPageDataValid.CheckForCheckBoxList(this, isShowErrorInline);
					break;
			}
		}
		if (result != "") {
			summaryResult += result + "\r\n";
			if (showErrorType != "inline") {
				alertResult += result + "\r\n";
			}
		}
	});
	$(targetControlId).find("ul").each(function () {
		var myInputType = $(this).attr("MyInputType");
		var isRequired = $(this).attr("IsRequired");
		var showErrorType = $(this).attr("ShowErrorType");
		if (showErrorType != undefined) {
			showErrorType = showErrorType.toLowerCase();
		}
		var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";
		var result = "";
		if (isRequired != undefined && isRequired.toLowerCase() == "true" && myInputType != undefined) {
			switch (myInputType.toLowerCase()) {
				case "radiobuttonlist":
					result = customPageDataValid.CheckForRadionButtonList(this, isShowErrorInline);
					break;
				case "checkboxlist":
					result = customPageDataValid.CheckForCheckBoxList(this, isShowErrorInline);
					break;
			}
		}
		if (result != "") {
			summaryResult += result + "\r\n";
			if (showErrorType != "inline") {
				alertResult += result + "\r\n";
			}
		}
	});

	//检查下拉列表框///////////////////////////////////////////////////////////////////////////////
	$(targetControlId).find("select").each(function () {
		var isRequired = $(this).attr("IsRequired");
		var showErrorType = $(this).attr("ShowErrorType");
		if (showErrorType != undefined) {
			showErrorType = showErrorType.toLowerCase();
		}
		var isShowErrorInline = showErrorType == "inline" || showErrorType == "all";
		if (isRequired != undefined && isRequired.toLowerCase() == "true") {
			var result = customPageDataValid.CheckForDropDownList(this, isShowErrorInline);
			if (result != "") {
				summaryResult += result + "\r\n";
				if (showErrorType != "inline") {
					alertResult += result + "\r\n";
				}
			}
		}
	});
	if (alertResult != undefined && alertResult != "") {
		var firstLineEndPlace = alertResult.indexOf('\r');
		if (isOnlyShowFirstError && firstLineEndPlace != -1) {
			alertResult = alertResult.substring(0, firstLineEndPlace);
		}
		alert(alertResult);
	}
	return (summaryResult.replace('\r\n', '') != "") ? false : true;
}