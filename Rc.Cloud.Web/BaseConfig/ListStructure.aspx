<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="ListStructure.aspx.cs" Inherits="Rc.Cloud.Web.BaseConfig.ListStructure" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="div_right_title">
        <div class="div_right_title_icon">
        </div>
       <%=siteMap %>
    </div>
     <div class="clearDiv">
    </div>
     <div class="div_right_search">
        <table class="table_search">
            <tr>
                <td class="td_search_001">表名：
                </td>
                <td>
                    <asp:TextBox ID="txtMark" runat="server" CssClass="txt_Search"></asp:TextBox>
                </td>
                 <td class="td_search_001">表说明：
                </td>
                <td>
                    <asp:TextBox ID="TableDescription" runat="server" CssClass="txt_Search"></asp:TextBox>
                </td>
                 <td class="td_search_001">表说明是否为空：
                </td>
                <td>
                     <asp:DropDownList ID="tdyon" runat="server">
                         <asp:ListItem Value="01">全部</asp:ListItem>
                         <asp:ListItem Value="11">是</asp:ListItem>
                         <asp:ListItem Value="00">否</asp:ListItem>
                     </asp:DropDownList>
                </td>               
                <td>
                    <asp:Button runat="server" CssClass="btn" Text="查 询" ID="btn_Search" OnClick="btn_Search_Click" />
                </td>             
            </tr>
            <tr>
                 <td class="td_search_001">字段名：
                </td>
                <td>
                    <asp:TextBox ID="Field" runat="server" CssClass="txt_Search"></asp:TextBox>
                </td>
                 <td class="td_search_001">字段说名：
                </td>
                <td>
                    <asp:TextBox ID="FieldDescribe" runat="server" CssClass="txt_Search"></asp:TextBox>
                </td>
                <td class="td_search_001">字段是否为空：
                </td>
                <td>
                     <asp:DropDownList ID="yesorno" runat="server">
                         <asp:ListItem Value="01">全部</asp:ListItem>                         
                         <asp:ListItem Value="11">是</asp:ListItem>
                         <asp:ListItem Value="00">否</asp:ListItem>
                     </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
     <div class="clearDiv">
    </div>
     <div class="div_right_listtitle">
      表结构配置
    </div>
    <div class="clearDiv">
     <div class="clearDiv">
    </div>
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
    <div class="div_ShowDailg pop_Small" id="div_Pop">
        <div class="div_ShowDailg_Title" id="div_Pop_Title">
            <div class="div_ShowDailg_Title_left" id="div_ShowDailg_Title_left">
            </div>
            <div class="div_Close_Dailg" id="div_Close_Dailg" title="关闭" onclick="ClosePopByIdAddUser();">
            </div>
            <!--关闭-->
        </div>
        <div class="clearDiv" id="div1">
        </div>
        <div id="divIfram" class="divIfram">
        </div>
    </div>
     <script type="text/javascript">
         $("#div_Pop").easydrag();
         $("#div_Pop").setHandler("div_Pop_Title");
         function ClosePopByIdAddUser() {
             CloseDialogById('div_Pop');
         }
         function AddModule(database,tablename, tableinfo, tableColumn, tablecolumninfo) {
             var src = "ListStructureItem.aspx?tablename=" + tablename + "&tableinfo=" + tableinfo + "&tableColumn=" + tableColumn + "&tablecolumninfo=" + tablecolumninfo + "&database=" + database;
             $("#div_Pop").show();
             $("#div_ShowDailg_Title_left").html("修改表");
             $("#divIfram").html("<iframe  height=\"200\"  frameborder='0' width='100%' style='margin: 0px' src='" + src + "'></iframe>");

             SetDialogPosition("div_Pop", 30);
         }
         function AddModuleColumns(database,tablename, tableColumn, tablecolumninfo) {
             var src = "ListStructureItemColumn.aspx?tablename=" + tablename + "&tableColumn=" + tableColumn + "&tablecolumninfo=" + tablecolumninfo + "&database=" + database;
             $("#div_Pop").show();
             $("#div_ShowDailg_Title_left").html("修改表字段");
             $("#divIfram").html("<iframe  height=\"200\"  frameborder='0' width='100%' style='margin: 0px' src='" + src + "'></iframe>");
             SetDialogPosition("div_Pop", 30);
         }
         function HandelAddUser(sign, strMessage) {
             if (sign == "1") {
                 showTips('操作成功', '', '1');
                 CloseDialogById();
                setTimeout('document.location.reload()', 500);
            }
            else {
                showTipsErr(strMessage, '3')
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
   
</asp:Content>
