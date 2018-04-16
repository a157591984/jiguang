<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SysUser_For_CustomerInfo.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysUser_For_CustomerInfo" %>
<%@ Register src="../UserControl/XUV_CommonDict.ascx" tagname="XUV_CommonDict" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_right_title">
        <div class="div_right_title_icon">
        </div>
        <%=siteMap%>
    </div>
    <div class="clearDiv">
    </div>
    <div class="div_right_search">
        <table class="table_search_001" >
            <tr>
                <td >
                    实施专员：
                </td>
                <td>
                    <asp:TextBox ID="txtsysUserName" runat="server" CssClass="txt_Search"></asp:TextBox>
                </td>
                <td>
                    <asp:Button runat="server" CssClass="btn" Text="查 询" ID="btn_Search" OnClick="btn_Search_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="clearDiv">
    </div>
    <div class="div_right_listtitle">
        专员列表
    </div>
    <div class="clearDiv">
    </div>
    <!--主数据-->
    <%= GetHtmlData() %>
    <div class="clearDiv">
    </div>
    <!--分页-->
    <%= GetPageIndex() %>
    <div class="clearDiv">
    </div>
    <!-- 弹出层操作-->
    <div class="div_ShowDailg" id="div_Pop" style="width: 800px; height: 600px;">
        <div class="div_ShowDailg_Title" id="div_Pop_Title">
            <div class="div_ShowDailg_Title_left" id="div_ShowDailg_Title_left">
            </div>
            <div class="div_Close_Dailg" id="div_Close_Dailg" title="关闭" onclick="ClosePopByIdAddUser();">
            </div>
            <!--关闭-->
        </div>
        <div class="clearDiv" id="div_iframe">
        </div>
    </div>

      <!------隐藏-弹出层------->
        <div class="div_ShowDailg" id="div_Pop_CommonDict" style="width:800px; height:570px;">     
            <div class="div_ShowDailg_Title" id="div_Pop_CommonDict_Title">
                <div class="div_ShowDailg_Title_left" id="div_Pop_CommonDict_Title_Teft"></div>
                <div class="div_Close_Dailg" id="div5" title="关闭" onclick="CloseDialog();"></div><!--关闭-->
            </div>           
             <uc1:XUV_CommonDict ID="XUV_CommonDict1" runat="server" />
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
   <!--#include file="../Common/Tips.inc"-->
    <script language="javascript" type="text/javascript">
        $("#div_Pop").easydrag();
        $("#div_Pop").setHandler("div_Pop_Title");

        function ClosePopByIdAddUser() {
            CloseDialogById('div_Pop');
        }
        function SetCustomerInfo(sysUser_ID, sysUser_Name) {
            var src = "SysUser_For_CustomerInfoAdd.aspx?SysUser_ID=" + sysUser_ID +"&sysUser_Name="+sysUser_Name;
            $("#div_ShowDailg_Title_left").html("设置客户");
            $("#div_iframe").html("<iframe  height=\"550\"  frameborder='0' width='100%' style='margin: 0px' src='" + src + "'></iframe>");
            $("#div_Pop").show();
            SetDialogPosition("div_Pop", 70);
        }

        function ShowAlert(str) {
            $.dialog.alert(str);
        }

        //弹出层操作后处理
        function Handel(sign, strMessage) {
            if (sign == "1") {
                showTips('操作成功', '<%=ReturnUrl %>', '1');
                ClosePopByIdAddUser();
            }
            else {
                showTipsErr('操作失败. ' + strMessage, '4')
            }
        }

        function showCommonDict(userControlID, hidUserControlID, D_Type, D_Expand, Hospital) {
            //        ShowDocumentDivBG_Opacity0();
            $("#div_Pop_CommonDict").css({ height: "500px", width: "600px" });
            SetDialogPositionTop("div_Pop_CommonDict", 30);
            $("#div_Pop_CommonDict").show();
            if (D_Type == "V020") {
                $("#div_Pop_CommonDict_Title_Teft").html("设置客户");
            }
            //方法名称是：GetDataList_+控件的名称
            //D_Type:1医院,2药物分类
            //D_Expand：扩展属性（以“|”号分隔的多个属性）
            //【0】选择模式         0多选，1单选 默认：多选
            //【1】返回值模式       0按正常模式返回值，1以TABLE方式返回  默认：按正常模式返回值
            //【2】编辑模式         0无编辑，1只添加，2只修改，3添加与修改 默认：无编辑
            GetDataList_XUV_CommonDict(userControlID, hidUserControlID, D_Type, D_Expand, Hospital);
        }  
    </script>
</asp:Content>

