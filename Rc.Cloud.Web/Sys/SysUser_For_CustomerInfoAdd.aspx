<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysUser_For_CustomerInfoAdd.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysUser_For_CustomerInfoAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register src="../UserControl/XUV_CommonDict.ascx" tagname="XUV_CommonDict" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../styles/styles003/layOut.css" rel="stylesheet" type="text/css" />
    <link href="../styles/styles003/style01.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/styles003/sdmenu.css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../scripts/jQuery_Dialog.js"></script>
    <script type="text/javascript" src="../scripts/jquery.easydrag.js"></script>
    <script type="text/javascript" src="../scripts/function.js"></script>
    <link href="../Styles/Dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/sdmenu.js"></script>
    <script src="../Scripts/ToolAnalytics.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table cellpadding="0" cellspacing="0" class="table_content">
        <tr>
            <td class="td_content_001" width="20%">
                用户名称：
            </td>
            <td class="td_content_002">
                <asp:Label ID="lbSysUser_Name" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td_content_001" width="20%">
                客户名称：
            </td>
            <td class="td_content_002">
                        <input type="text" id="txtContactName" readonly="readonly" clientidmode="Static"  class="txt"   runat="server"  style="float:left;"/>
                        <input type="hidden" id="hidContactId" clientidmode="Static" runat="server"  />
                        <input type="button" title="选择被拜访人" onclick="showCommonDict('txtContactName','hidContactId','V002');" class="btn_info2" />
                       
                    </td>
        </tr>
    </table>
    <div class="clearDiv">
    </div>
    <div class="div_page_bottom_operation">
        <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="保 存" OnClick="btnSave_Click" />
    </div>   
    </div>
     <!-- 弹出层操作 合用效果控件  -->
        <div class="div_ShowDailg" id="div_Pop_CommonDict" style="width:600px; height:480px;">     
        <div class="div_ShowDailg_Title" id="div_Pop_CommonDict_Title">
            <div class="div_ShowDailg_Title_left" id="div_Pop_CommonDict_Title_Teft"></div>
            <div class="div_Close_Dailg" id="div5" title="关闭" onclick="CloseDialog();"></div><!--关闭-->
        </div>           
         <uc1:XUV_CommonDict ID="XUV_CommonDict1" runat="server" />
        </div>
    </form>
</body>
</html>
  <script type="text/javascript">
      $("#div_Pop_CommonDict").easydrag();
      $("#div_Pop_CommonDict").setHandler("div_Pop_CommonDict_Title"); 
      function showCommonDict(userControlID, hidUserControlID, D_Type) {
          SetDialogPositionTop("div_Pop_CommonDict", 10);
          $("#div_Pop_CommonDict").show();
          if (D_Type == "V002") {
              $("#div_Pop_CommonDict_Title_Teft").html("选择客户");
          }
          //方法名称是：GetDataList_+控件的名称
          //D_Type:1医院,2药物分类
          var Hospital = arguments[4] ? arguments[4] : "";
          var D_Expand = arguments[3] ? arguments[3] : "";
          GetDataList_XUV_CommonDict(userControlID, hidUserControlID, D_Type);
      }
</script>
