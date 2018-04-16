﻿/*****************************************************************************
修改：2013.11.11
*****************************************************************************/
var JqueryCustomDialog = {

	//边框尺寸(像素)
	"cBorderSize": 1,
	//边框颜色
	"cBorderColor": "#ffffff",	
	//右上角关闭显示文本
	"cCloseText": "&nbsp;&nbsp;X&nbsp;&nbsp;",
	//提交按钮文本
	"cSubmitText": "确 认",
	//取消按钮文本
	"cCancelText": "取 消",
	//拖拽效果
	"cDragTime": "100",
	//显示在层中的div的Id
	ShowPageDivID : null,

	//将页面中的一个Div控件（一般为默认隐藏的DIV）显示在弹出层中//////////////////////////////////////
	OpenDialogFromDiv: function (dialogTitle, divId, dialogWidth, dialogTop) {
		ShowPageDivID = divId;
		if (dialogWidth == undefined || dialogWidth == null) {
			dialogWidth = 400;
		}
		if (dialogTop == undefined || dialogTop == null) {
			dialogTop = 120;
		}
		dialogTop += document.documentElement.scrollTop;
		//获取客户端页面宽高
		var _client_width = document.body.clientWidth;
		var _client_height = document.documentElement.scrollHeight;

		//create shadow
		if (typeof ($("#jd_shadow")[0]) == "undefined") {
			//前置
			$("body").prepend("<div id='jd_shadow'>&nbsp;</div>");
			var _jd_shadow = $("#jd_shadow");
			_jd_shadow.css("width", _client_width + "px");
			_jd_shadow.css("height", _client_height + "px");
		}

		//create dialog
		if (typeof ($("#jd_dialog")[0]) != "undefined") {
			$("#jd_dialog").remove();
		}
		$("body").prepend("<div id='jd_dialog'></div>");

		//dialog location
		var _jd_dialog = $("#jd_dialog");
		var _left = (_client_width - (dialogWidth + JqueryCustomDialog.cBorderSize * 2 + 5)) / 2;
		_jd_dialog.css("left", (_left < 0 ? 0 : _left) + document.documentElement.scrollLeft + "px");
		_jd_dialog.css("top", dialogTop + "px");

		//create dialog shadow
		_jd_dialog.append("<div id='jd_dialog_s'>&nbsp;</div>");
		var _jd_dialog_s = $("#jd_dialog_s");
		//iframeWidth + double border
		_jd_dialog_s.css("width", dialogWidth + JqueryCustomDialog.cBorderSize * 2 + "px");

		//create dialog main
		_jd_dialog.append("<div id='jd_dialog_m'></div>");
		var _jd_dialog_m = $("#jd_dialog_m");
		_jd_dialog_m.css("border", JqueryCustomDialog.cBorderColor + " " + JqueryCustomDialog.cBorderSize + "px solid");
		_jd_dialog_m.css("width", dialogWidth + "px");

		//header
		_jd_dialog_m.append("<div id='jd_dialog_m_h'></div>");
		var _jd_dialog_m_h = $("#jd_dialog_m_h");

		//header left
		_jd_dialog_m_h.append("<span id='jd_dialog_m_h_l'>" + dialogTitle + "</span>");
		_jd_dialog_m_h.append("<span id='jd_dialog_m_h_r' title='关闭弹出层' onclick='JqueryCustomDialog.Close();'>" + JqueryCustomDialog.cCloseText + "</span>");

		//body
		_jd_dialog_m.append("<div id='jd_dialog_m_b'></div>");
		var _jd_dialog_m_b = $("#jd_dialog_m_b");
		_jd_dialog_m_b.css("width", dialogWidth - 20 + "px");
		$("#jd_dialog_m_b").height($("#" + divId).height());			//设置body的高度等于要显示内容的高度

		//将div层浮动起来
		$("#" + divId).css("width", dialogWidth - 20 + "px");
		$("#" + divId).css("position", "absolute");
		$("#" + divId).css("left", (_left < 0 ? 0 : _left) + document.documentElement.scrollLeft + 10 + "px");
		$("#" + divId).css("top", dialogTop + 40 + "px");
		$("#" + divId).css("z-index", 1000);
		$("#" + divId).show();		

		//bottom		
		if (isCancelButton) {
			_jd_dialog_m.append("<div id='jd_dialog_m_t'></div>");
			var _jd_dialog_m_t = $("#jd_dialog_m_t");
			_jd_dialog_m_t.append("<span class='jd_dialog_m_t_s'><input id='jd_btn_cancel' value=' 关 闭 ' type='button' onclick='JqueryCustomDialog.Close();' /></span>");
		}

		$("#jd_dialog_s").height($("#jd_dialog_m").height() + 4);

		//注册为可拖拽
		JqueryCustomDialogDragAndDrop.Register(_jd_dialog[0], _jd_dialog_m_h[0]);
	},

	//显示alert对话框/////////////////////////////////////////////////////////////////////////////////////////
	AlertFull: function (dialogTitle, message, isCancelButton, isOkButton, dialogWidth, dialogTop) {
		if (dialogWidth == undefined || dialogWidth == null) {
			dialogWidth = 400;
		}
		if (dialogTop == undefined || dialogTop == null) {
			dialogTop = 120;
		}
		dialogTop += document.documentElement.scrollTop;
		//获取客户端页面宽高
		var _client_width = document.body.clientWidth;
		var _client_height = document.documentElement.scrollHeight;

		//create shadow
		if (typeof ($("#jd_shadow")[0]) == "undefined") {
			//前置
			$("body").prepend("<div id='jd_shadow'>&nbsp;</div>");
			var _jd_shadow = $("#jd_shadow");
			_jd_shadow.css("width", _client_width + "px");
			_jd_shadow.css("height", _client_height + "px");
		}

		//create dialog
		if (typeof ($("#jd_dialog")[0]) != "undefined") {
			$("#jd_dialog").remove();
		}
		$("body").prepend("<div id='jd_dialog'></div>");

		//dialog location
		var _jd_dialog = $("#jd_dialog");
		var _left = (_client_width - (dialogWidth + JqueryCustomDialog.cBorderSize * 2 + 5)) / 2;
		_jd_dialog.css("left", (_left < 0 ? 0 : _left) + document.documentElement.scrollLeft + "px");		
		_jd_dialog.css("top", dialogTop + "px");

		//create dialog shadow
		_jd_dialog.append("<div id='jd_dialog_s'>&nbsp;</div>");
		var _jd_dialog_s = $("#jd_dialog_s");
		//iframeWidth + double border
		_jd_dialog_s.css("width", dialogWidth + JqueryCustomDialog.cBorderSize * 2 + "px");		

		//create dialog main
		_jd_dialog.append("<div id='jd_dialog_m'></div>");
		var _jd_dialog_m = $("#jd_dialog_m");
		_jd_dialog_m.css("border", JqueryCustomDialog.cBorderColor + " " + JqueryCustomDialog.cBorderSize + "px solid");
		_jd_dialog_m.css("width", dialogWidth + "px");

		//header
		_jd_dialog_m.append("<div id='jd_dialog_m_h'></div>");
		var _jd_dialog_m_h = $("#jd_dialog_m_h");	

		//header left
		_jd_dialog_m_h.append("<span id='jd_dialog_m_h_l'>" + dialogTitle + "</span>");
		_jd_dialog_m_h.append("<span id='jd_dialog_m_h_r' title='关闭弹出层' onclick='JqueryCustomDialog.Close();'>" + JqueryCustomDialog.cCloseText + "</span>");

		//body
		_jd_dialog_m.append("<div id='jd_dialog_m_b'></div>");
		var _jd_dialog_m_b = $("#jd_dialog_m_b");
		_jd_dialog_m_b.css("width", dialogWidth - 20 + "px");
		$("#jd_dialog_m_b").append(message);

		//bottom
		if (isOkButton || isCancelButton) {
			_jd_dialog_m.append("<div id='jd_dialog_m_t'></div>");
			var _jd_dialog_m_t = $("#jd_dialog_m_t");
			if (isOkButton) {
				_jd_dialog_m_t.append("<span class='jd_dialog_m_t_s'><input id='jd_dialog_m_btn_Ok' value=' 确 定 ' type='button' /></span>");
			}
			if (isCancelButton) {
				_jd_dialog_m_t.append("<span class='jd_dialog_m_t_s'><input id='jd_dialog_m_btn_cancel' value=' 关 闭 ' type='button' onclick='JqueryCustomDialog.Close();' /></span>");
			}
		}		

		$("#jd_dialog_s").height($("#jd_dialog_m").height() + 4);

		//注册为可拖拽
		JqueryCustomDialogDragAndDrop.Register(_jd_dialog[0], _jd_dialog_m_h[0]);
	},
	//显示一个带关闭按钮的提示框
	Alert: function (message) {
		this.AlertFull("提示信息", message, true, false);
	},
	//显示一个带关闭按钮的提示框，同时可以指定距离顶部的距离
	AlertAtPlace: function (message, dialogWidth, dialogTop) {
		this.AlertFull("提示信息", message, true, false, dialogWidth, dialogTop);
	},
	//待确认的提示框
	ShowConfirm: function (dialogTitle, message, okMethod) {
		this.AlertFull(dialogTitle, message, true, true);
		okMethod = okMethod.replace(/'/g, '"');
		$('#jd_dialog_m_btn_Ok').attr("onclick", "javascript:setTimeout('" + okMethod + "', 0);");
		return false;
	},
	//显示一个自动关闭的提示框
	AlertAutoClose: function (message, millisec, dialogTop) {
		this.AlertFull("提示信息", message, false, false, undefined, dialogTop);
		if (millisec == undefined) {
			millisec = 2000;
		}
		setTimeout("JqueryCustomDialog.Close();", millisec);
	},

	//在弹出层中显示一个iframe内容//////////////////////////////////////////////////////////////////////
	OpenFrame: function (dialogTitle, iframeSrc, iframeWidth, iframeHeight, dialogTop) {
		if (dialogTop == undefined || dialogTop == null) {
			dialogTop = 120;
		}
		iframeWidth += 20;		//添加左右padding的距离
		//获取客户端页面宽高
		var _client_width = document.body.clientWidth;
		var _client_height = document.documentElement.scrollHeight;

		//create shadow
		if (typeof ($("#jd_shadow")[0]) == "undefined") {
			//前置
			$("body").prepend("<div id='jd_shadow'>&nbsp;</div>");
			var _jd_shadow = $("#jd_shadow");
			_jd_shadow.css("width", _client_width + "px");
			_jd_shadow.css("height", _client_height + "px");
		}

		//create dialog
		if (typeof ($("#jd_dialog")[0]) != "undefined") {
			$("#jd_dialog").remove();
		}
		$("body").prepend("<div id='jd_dialog'></div>");

		//dialog location
		//left 边框*2 阴影5
		//top 边框*2 阴影5 header30 bottom50
		var _jd_dialog = $("#jd_dialog");
		var _left = (_client_width - (iframeWidth + JqueryCustomDialog.cBorderSize * 2 + 5)) / 2;
		_jd_dialog.css("left", (_left < 0 ? 0 : _left) + document.documentElement.scrollLeft + "px");
		_jd_dialog.css("top", dialogTop + "px");

		//create dialog shadow
		_jd_dialog.append("<div id='jd_dialog_s'>&nbsp;</div>");
		var _jd_dialog_s = $("#jd_dialog_s");
		//iframeWidth + double border
		_jd_dialog_s.css("width", iframeWidth + JqueryCustomDialog.cBorderSize * 2 + "px");
		//iframeWidth + double border + header + bottom
		_jd_dialog_s.css("height", iframeHeight + JqueryCustomDialog.cBorderSize * 2 + 50 + "px");

		//create dialog main
		_jd_dialog.append("<div id='jd_dialog_m'></div>");
		var _jd_dialog_m = $("#jd_dialog_m");
		_jd_dialog_m.css("border", JqueryCustomDialog.cBorderColor + " " + JqueryCustomDialog.cBorderSize + "px solid");
		_jd_dialog_m.css("width", iframeWidth + "px");

		//header
		_jd_dialog_m.append("<div id='jd_dialog_m_h'></div>");
		var _jd_dialog_m_h = $("#jd_dialog_m_h");
		_jd_dialog_m_h.css("background-color", JqueryCustomDialog.cHeaderBackgroundColor);

		//header left
		_jd_dialog_m_h.append("<span id='jd_dialog_m_h_l'>" + dialogTitle + "</span>");
		_jd_dialog_m_h.append("<span id='jd_dialog_m_h_r' title='关闭弹出层' onclick='JqueryCustomDialog.Close();'>" + JqueryCustomDialog.cCloseText + "</span>");

		//body
		_jd_dialog_m.append("<div id='jd_dialog_m_b'></div>");
		var _jd_dialog_m_b = $("#jd_dialog_m_b");
		_jd_dialog_m_b.css("width", iframeWidth - 20 + "px");
		_jd_dialog_m_b.css("height", iframeHeight + "px");

		//iframe 遮罩层
		_jd_dialog_m_b.append("<div id='jd_dialog_m_b_1'>&nbsp;</div>");
		var _jd_dialog_m_b_1 = $("#jd_dialog_m_b_1");
		_jd_dialog_m_b_1.css("top", "30px");
		_jd_dialog_m_b_1.css("width", iframeWidth - 20 + "px");
		_jd_dialog_m_b_1.css("height", iframeHeight + "px");
		_jd_dialog_m_b_1.css("display", "none");

		//iframe 容器
		_jd_dialog_m_b.append("<div id='jd_dialog_m_b_2'></div>");
		//iframe
		$("#jd_dialog_m_b_2").append("<iframe id='jd_iframe' src='" + iframeSrc + "' scrolling='auto' frameborder='0' width='" + (iframeWidth - 20) + "' height='" + iframeHeight + "' />");
		
		JqueryCustomDialogDragAndDrop.Register(_jd_dialog[0], _jd_dialog_m_h[0]);
	},

	//在当前窗口关闭浮动层
	Close: function () {
		$("#jd_shadow").remove();
		$("#jd_dialog").remove();
		if (ShowPageDivID != null && ShowPageDivID != undefined) {
			$("#" + ShowPageDivID).hide();
			ShowPageDivID = null;
		}
	},

	//在iframe中关闭浮动层
	CloseFromIframe: function (reloadPage) {
		if (reloadPage == true) {
			window.parent.location.href = window.parent.location.href;
		}
		$("#jd_shadow", window.parent.document).remove();
		$("#jd_dialog", window.parent.document).remove();
		if (ShowPageDivID != null && ShowPageDivID != undefined) {
			$("#" + ShowPageDivID, window.parent.document).hide();
			ShowPageDivID = null;
		}
	},

	/// <summary>提交</summary>
	/// <remark></remark>
	Ok: function () {
		var frm = $("#jd_iframe");
		if (frm[0].contentWindow.Ok()) {
			JqueryCustomDialog.Close();
		}
		else {
			frm[0].focus();
		}
	},

	/// <summary>提交完成</summary>
	/// <param name="alertMsg">弹出提示内容，值为空不弹出</param>
	/// <param name="isCloseDialog">是否关闭对话框</param>
	/// <param name="isRefreshPage">是否刷新页面(关闭对话框为true时有效)</param>
	SubmitCompleted: function (alertMsg, isCloseDialog, isRefreshPage) {
		if ($.trim(alertMsg).length > 0) {
			alert(alertMsg);
		}
		if (isCloseDialog) {
			JqueryCustomDialog.Close();
			if (isRefreshPage) {
				window.location.href = window.location.href;
			}
		}
	}
};

var JqueryCustomDialogDragAndDrop = function () {

	//客户端当前屏幕尺寸(忽略滚动条)
	var _clientWidth;
	var _clientHeight;
	//拖拽控制区
	var _controlObj;
	//拖拽对象
	var _dragObj;
	//拖动状态
	var _flag = false;
	//拖拽对象的当前位置
	var _dragObjCurrentLocation;
	//鼠标最后位置
	var _mouseLastLocation;
	//使用异步的Javascript使拖拽效果更为流畅
	var _timer;
	//定时移动，由_timer定时调用
	var intervalMove = function(){
		$(_dragObj).css("left", _dragObjCurrentLocation.x + "px");
		$(_dragObj).css("top", _dragObjCurrentLocation.y + "px");
	};
	var getElementDocument = function (element) {
		return element.ownerDocument || element.document;
	};
	//鼠标按下
	var dragMouseDownHandler = function (evt) {
		if (_dragObj) {
			evt = evt || window.event;
			//获取客户端屏幕尺寸
			_clientWidth = document.body.clientWidth;
			_clientHeight = document.documentElement.scrollHeight;
			//iframe遮罩
			$("#jd_dialog_m_b_1").css("display", "none");
			//标记
			_flag = true;
			//拖拽对象位置初始化
			_dragObjCurrentLocation = {
				x: $(_dragObj).offset().left,
				y: $(_dragObj).offset().top
			};
			//鼠标最后位置初始化
			_mouseLastLocation = {
				x: evt.screenX,
				y: evt.screenY
			};
			//注：mousemove与mouseup下件均针对document注册，以解决鼠标离开_controlObj时事件丢失问题
			//注册事件(鼠标移动)			
			$(document).bind("mousemove", dragMouseMoveHandler);
			//注册事件(鼠标松开)
			$(document).bind("mouseup", dragMouseUpHandler);
			//取消事件的默认动作
			if (evt.preventDefault) {
				evt.preventDefault();
			}
			else {
				evt.returnValue = false;
			}
			//开启异步移动
			_timer = setInterval(intervalMove, 10);
		}
	};

	//鼠标移动
	var dragMouseMoveHandler = function (evt) {
		if (_flag) {
			evt = evt || window.event;
			//当前鼠标的x,y座标
			var _mouseCurrentLocation = {
				x: evt.screenX,
				y: evt.screenY
			};
			//拖拽对象座标更新(变量)
			_dragObjCurrentLocation.x = _dragObjCurrentLocation.x + (_mouseCurrentLocation.x - _mouseLastLocation.x);
			_dragObjCurrentLocation.y = _dragObjCurrentLocation.y + (_mouseCurrentLocation.y - _mouseLastLocation.y);
			//将鼠标最后位置赋值为当前位置
			_mouseLastLocation = _mouseCurrentLocation;
			//拖拽对象座标更新(位置)
			$(_dragObj).css("left", _dragObjCurrentLocation.x + "px");
			$(_dragObj).css("top", _dragObjCurrentLocation.y + "px");
			//取消事件的默认动作
			if (evt.preventDefault) {
				evt.preventDefault();
			}
			else {
				evt.returnValue = false;
			}
		}
	};

	//鼠标松开
	var dragMouseUpHandler = function (evt) {
		if (_flag) {
			evt = evt || window.event;
			//取消iframe遮罩
			$("#jd_dialog_m_b_1").css("display", "none");
			//注销鼠标事件(mousemove mouseup)
			cleanMouseHandlers();
			//标记
			_flag = false;
			//清除异步移动
			if(_timer){
				clearInterval(_timer);
				_timer = null;
			}
		}
	};

	//注销鼠标事件(mousemove mouseup)
	var cleanMouseHandlers = function () {
		if (_controlObj) {
			$(_controlObj.document).unbind("mousemove");
			$(_controlObj.document).unbind("mouseup");
		}
	};

	return {
		//注册拖拽(参数为dom对象)
		Register: function (dragObj, controlObj) {
			//赋值
			_dragObj = dragObj;
			_controlObj = controlObj;
			//注册事件(鼠标按下)
			$(_controlObj).bind("mousedown", dragMouseDownHandler);
		}
	}
}();