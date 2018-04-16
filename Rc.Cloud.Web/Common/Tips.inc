<script type="text/javascript">
  function SubmitDataSuccess(msg, url,seconds) {
  ShowDocumentDivBG();
  $.dialog.tips("<div class='tips_success'>" + msg + "</div>", seconds);
  if (url != '') {
  setTimeout("ResponseRedirect('"+ url +"')", seconds*1000);
  }
  else
  {
  setTimeout("CloseTipsBG()", seconds*1000);
  }
  }
  function ResponseRedirect(url)
  {
  window.location.href = url;
  }
  function SubmitDataError(msg,seconds) 
  { 
    ShowDocumentDivBG();
    $.dialog.tips("<div class='tips_error'>"+ msg +"</div>", seconds);
  setTimeout("CloseTipsBG()", seconds*1000 );
  }
  function showTipsErr(msg,seconds)
  {
  $.dialog.tips("<div class='tips_error'>"+ msg +"</div>", seconds);
  }
  
  function showTips(msg, url,seconds) {
  ;
  $.dialog.tips("<div class='tips_success'>" + msg + "</div>", seconds);
  if (url != '') {
  setTimeout("ResponseRedirect('"+ url +"')", seconds*1000);
  }

  }

  //弹出错误提示，并跳转页面
  function showTipsErrAndTurn(msg, url,seconds) {
  ;
  $.dialog.tips("<div class='tips_error'>" + msg + "</div>", seconds);
  if (url != '') {
  setTimeout("location.reload(true)", seconds*1000);
  }

  }
  //弹出页面遮照层
  function ShowDocumentDivBG()
  {
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
  if (jQuery("body").height() < jQuery(window).height())
    {
        bgObj.style.height = jQuery(window).height() + "px";
    }
    else 
    {
        bgObj.style.height = jQuery("body").height() + "px";
    }
    bgObj.style.zIndex = "200";
    $("#div_documentbg").show();
  }
   //弹出页面遮照层
  function ShowDocumentDivBG1()
  {
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
  if (jQuery("body").height() < jQuery(window).height())
    {
        bgObj.style.height = jQuery(window).height()-30 + "px";
    }
    else 
    {
        bgObj.style.height = jQuery("body").height()-30 + "px";
    }
    bgObj.style.zIndex = "200";
    $("#div_documentbg").show();
  }
    
    function CloseTipsBG() 
    {
        $("#div_documentbg").hide();
    }
  function SubmitData(msg)
  {        
        ShowDocumentDivBG(); 
        $.dialog.tips("<div class='tips_jinggao'>" + msg + "</div>", 60);
 }
  function SubmitData2(msg,seconds)
  {
    ShowDocumentDivBG();
      $.dialog.tips("<div class='tips_jinggao'>" + msg + "</div>", seconds);
      setTimeout("CloseTipsBG()", seconds*1000 );
}
    </script>