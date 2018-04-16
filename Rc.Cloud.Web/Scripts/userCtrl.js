 $("#div_ShowUserList").easydrag();
    $("#div_ShowUserList").setHandler("div_ShowUserList_Title");
    function ClearUserList_Search() {
        $("#txt_userlist_name").val("");
        $("#txt_UserOrgName_userlist").val("");
        $("#hdUserOrigId_userlist").val("-1");
        $("#txt_UserOrgName_userlist").attr("title", "");
    }

    var tempModuleId = "";
    var userListArr = new Array();
    var userListNameArr = new Array();

    function ShowUserList(tempPI, tempPageSize) {
        $("#txt_userlist_name").focus();
        moduleId = <%=MODULE_ID %>;
 
        $("#span_userList").html("<div style=\"border:1px solid #C7C8C8;width:80%;margin:10px auto;margin-bottom:20px;padding-top:20px;padding-bottom:20px;text-align:center;\"><img src=\"<%=CSP.WebApp.Common.pfunction.getHostPath() %>/images/loading.gif\" align=\"absmiddle\" />&nbsp;&nbsp;Loading ...</div>");
        if (!$("#div_ShowUserList").is(":visible")) { ShowDocumentDivBG2(); }
        $("#div_ShowUserList").css("left", ($(window).width() - $("#div_ShowUserList").width()) / 2);
        $("#div_ShowUserList").css("top", $(window).scrollTop() + 100);
        $("#div_ShowUserList").show();
        var pi = $("#hdUserListPI").val();
        if (tempPI != undefined) { pi = tempPI; }
        var pageSize = $("#hdUserListPageSize").val();
        if (tempPageSize != undefined) { pageSize = tempPageSize; }
        var orgId = $("#hdUserOrigId_userlist").val();
        var usename = escape($("#txt_userlist_name").val());
        var urlp=pi+","+pageSize+","+moduleId+","+usename+","+orgId;
        //参数说明：p 以逗号分隔的当前页码，页显示量，模块编码，用户名，组织机构编码，ou 要排除的用户
        jQuery.get("<%=CSP.WebApp.Common.pfunction.getHostPath() %>/AuthorityMgr/AuthorityAjax.aspx", { key: "ShowUser", p: urlp, ou: "1", net: Math.random() }, function (data) { $("#span_userList").html(data); });
        TRHover();
        $("#div_ShowUserList").css("left", ($(window).width() - $("#div_ShowUserList").width()) / 2);
        $("#div_ShowUserList").css("top", $(window).scrollTop() + 100);
        //将已选中的数据设为选中状态
        if (userListArr.length > 0) {
            for (var i = 0; i < userListArr.length; i++) {
                $("#chb_user_" + userListArr[i]).attr("checked", "true");
            }
        }
    }
    function CloseUserList() {
        $("#div_ShowUserList").hide();
        if (!$(".div_ShowDailg").is(":visible")) { CloseDocumentDivBG(); }
    }
    function SelectUser(uid, chb) {
        if ($(chb).attr("checked")) {
            var t = 0;
            for (var i = 0; i < userListArr.length; i++) {
                if (userListArr[i] == uid) {
                    t = 1;
                    userListArr[i] = uid;
                    userListNameArr[i] = $("#td_user_name_" + uid).html();
                }
            }
            if (t == 0) {
                userListArr[userListArr.length] = uid;
                userListNameArr[userListNameArr.length] = $("#td_user_name_" + uid).html();
            }
        }
        else {
            for (var i = 0; i < userListArr.length; i++) {
                if (userListArr[i] == uid) {
                    userListArr[i] = 0;
                    userListNameArr[i] = "";
                }
            }
        }
    }
    function SelectAllUser(chb) {
        $(".chb_userlist").attr("checked", $(chb).attr("checked"));
        if ($(chb).attr("checked")) {
            jQuery.each(jQuery(".chb_userlist"), function (i, n) {
                var t = 0;
                var uid = $(n).attr("id").toString().substring($(n).attr("id").toString().lastIndexOf("_") + 1);
                for (var i = 0; i < userListArr.length; i++) {
                    if (userListArr[i] == uid) {
                        t = 1;
                        userListArr[i] = uid;
                        userListNameArr[i] = $("#td_user_name_" + uid).html();
                    }
                }
                if (t == 0) { userListArr[userListArr.length] = uid; userListNameArr[userListNameArr.length] = $("#td_user_name_" + uid).html(); }
            });
        }
        else {
            jQuery.each(jQuery(".chb_userlist"), function (i, n) {
                var uid = $(n).attr("id").toString().substring($(n).attr("id").toString().lastIndexOf("_") + 1);
                for (var i = 0; i < userListArr.length; i++) {
                    if (userListArr[i] == uid) {
                        userListArr[i] = 0;
                        userListNameArr[i] = "";
                    }
                }

            });
        }

    }
    //首页 上一页 下一页 最后页
    function UserListPageChange(pi, pageSize) {
        ShowUserList(pi, pageSize);
    }
    //页面索引发生变化
    function UserListPageIndexChange(pageSize) {
        ShowUserList($("#select_userlist_pageindex").val(), pageSize);
    }
    //每页显示数量发生改变
    function UserListPageSizeChange(pi) {
        ShowUserList(1, $("#select_userlist_pagesize").val());
    }

    function TRHover() 
    {
        if ($("#MainContent_UCPagerTool1_span_rowCount").html() == "0") { $(".div_page_bottom_operation_table").hide(); } else { $(".div_page_bottom_operation_table").show(); }
        $(".tr_con_001").hover(function () { $(this).css("background", "#edf2f8"); }, function () { $(this).css("background", "#fff"); });
        $(".tr_con_002").hover(function () { $(this).css("background", "#edf2f8"); }, function () { $(this).css("background", "#F7F7F7"); });   
        $(".div_ShowDailg").css("left", (document.body.clientWidth - $(".div_ShowDailg").width()) / 2);
        $(".div_ShowDailg").css("top", $(window).scrollTop() + 100);
        $(".div_ShowDailg2").css("left", (document.body.clientWidth - $(".div_ShowDailg").width()) / 2);
        $(".div_tips").css("left", (document.body.clientWidth - $(".div_tips").width()) / 2);
        $(".div_tips").css("top", $(window).scrollTop() + 100);
        $(".div_ShowDailg2").css("top", $(window).scrollTop() + 100);
        if ($("#div_detail_001").html() != null) { $("#div_LeavePublicHolidayDetail").css({ left: $("#div_detail_001").offset().left + 120, top: $("#div_detail_001").offset().top - 140 }); }
        $(".span_a").hover(function () { $(this).attr("class", "span_a2"); }, function () { $(this).attr("class", "span_a"); });
        $(".btn_search").attr("title", "查 询");
        $(".btn,.btn_big,.btn_Exit,.btn_more").hover(function () { $(this).attr("title", $(this).val()); });
        $("a").hover(function () { $(this).attr("title", $(this)[0].innerText); });
        //提示用户可输入的文字的个数。(FireFox)
        jQuery("#MainContent_txtRemark").attr("onkeyup", "RemarkKeyPress(this);");
        jQuery("#MainContent_txtleaveaudit").attr("onkeyup", "RemarkKeyPress(this);");
        //IE
        jQuery("#MainContent_txtRemark").keyup(function () { RemarkKeyPress(this); });
        $(".div_leave_close").hover(function () { $(this).attr("class", "div_leave_close2"); }, function () { $(this).attr("class", "div_leave_close"); });
        if (jQuery("#sideBar").height() + 83 < jQuery(window).height()) {
            jQuery("#sideBar").height(jQuery(window).height() - 83);
        }
    }
    jQuery(document).ready(function () { jQuery("#txt_audit_user").click(function () { ShowUserList(); }); });
    $("#div_Set_ProxyAudit").easydrag();
    $("#div_Set_ProxyAudit").setHandler("div_Set_ProxyAudit_Title");

 //UserList 提交
  function SubmitUserList() 
  {
     SelectProxyAuditUser();
  }
  function SelectProxyAuditUser() 
  {
    var tempIdArr = new Array();
    var tempNameArr = new Array();
    for (var i = 0; i < userListArr.length; i++)
        {
        if (userListArr[i] != 0) 
        {
            tempIdArr[tempIdArr.length] = userListArr[i];
            tempNameArr[tempNameArr.length] = userListNameArr[i];
        }
    }
    if (tempIdArr.length == 0) {
        jQuery.dialog.alert('请选择用户');
        return;
    }
    if (tempIdArr.length > 1) 
    {
        jQuery.dialog.alert('只能选择一个用户');
        return;
    }
    jQuery("#<%=BACK_CTRL_USER_IDS %>").val(tempNameArr[0]);
    jQuery("#<%=BACK_CTRL_USER_NAMES %>").val(tempIdArr[0]);
    CloseUserList();
}

//弹出页面遮照层
    function ShowDocumentDivBG2() {
        ShowDocumentDivBG();
    }
    //弹出页面遮照层
    function ShowDocumentDivBG() {
        var bgObj;
        var sWidth, sHeight;
        sWidth = document.body.clientWidth;
        sHeight = document.body.clientHeight;
        bgObj = document.getElementById("div_documentbg");
        bgObj.style.top = "0";
        bgObj.style.background = "#000";
        bgObj.style.opacity = "0.4";
        bgObj.style.filter = "filter: alpha(opacity=40)";
        bgObj.style.left = "0";
        bgObj.style.width = jQuery("body").width() + "px";
        if (jQuery("body").height() < jQuery(window).height()) {
            bgObj.style.height = jQuery(window).height() + "px";
        }
        else {
            bgObj.style.height = sHeight + "px";
        }
        bgObj.style.zIndex = "200";
        $("#div_documentbg").show();
    }
    //弹出页面遮照层
    function CloseDocumentDivBG() {
        $("#div_documentbg").hide();
        jQuery("#div_addLRItem").hide();
        jQuery("#div_modifyLRItem").hide();
        jQuery("#div_ImportLRItem").hide();
        jQuery("#div_ShowLogDetail").hide();
    }