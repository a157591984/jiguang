<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="SysMenuSynchronous.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysMenuSynchronous" %>
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
        <table class="table_search_001">
            <tr>
                <td>
                    基础库：
                </td>
                <td>
                    <asp:Label runat="server" Text="YXFW_DATA_0.9"  ID="lblDataName"></asp:Label> 
                </td>
                <td>
                    对比库：
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlDataName" IsRequired="true" IsAddEmptyItem="true" ClientIDMode="Static"></asp:DropDownList>                
                </td>
                <td>
                    <asp:Button runat="server" Text="执行对比" CssClass="btn" ID="btn_Execution" onclick="btn_Execution_Click" />
                </td>
            </tr>
            <tr>
                <td>
                  ID或名称：
                </td>
                <td>
                    <asp:TextBox ID="txtName"  class="txt" runat="server"></asp:TextBox>                     
                </td>
                <td>
                  系统编码：
                </td>
                <td>
       <%--             <asp:TextBox ID="txtCode"  class="txt" runat="server"></asp:TextBox> --%> 
                    <asp:DropDownList ID="ddlSyscode" runat="server" IsAddEmptyItem="true" ClientIDMode="Static"></asp:DropDownList>                   
                </td>    
                <td>
                    <asp:Button runat="server" Text="查询" CssClass="btn" ID="btn_Search"  onclick="btn_Search_Click" OnClientClick="return btnClient_Click()" />
                </td>              
            </tr>         
        </table>
           <asp:HiddenField ID="strCoverage" runat="server" />

        <%--yyk添加--%>
            <asp:HiddenField ID="updatadata" runat="server" />
        
         <asp:HiddenField ID="hidCoverage" runat="server" />
    </div>
    <div class="clearDiv">
    </div>
    <div class="div_right_listtitle">
        <span style="float:left;">对比结果</span>
        <span style="float:right">
            <asp:Button runat="server" Text="覆盖更新" CssClass="btn" ID="btn_Coverage"  onclick="btn_Coverage_Click" />
            <asp:Button runat="server" Text="增量更新" CssClass="btn" ID="btn_Add"  onclick="btn_Add_Click" />
             <asp:Button runat="server" Text="生成脚本" CssClass="btn" ID="btn_Create"  onclick="btn_Create_Click" />
            <asp:Button runat="server" Text="生成外网脚本" CssClass="btn" ID="btn_Createoutline" OnClick="btn_Createoutline_Click"   />
        </span>&nbsp;
    </div>
     <table class="table_list" cellpadding="0" cellspacing="0">
            <%--<tr  class="tr_title">
                <td style="width:90%;">不存在数据</td>
            </tr>--%>
            <tr class="tr_con_001">
                <td>
                    <div id="MyTable_tableLayout">
                       <div id="MyTable_tableFix">
                         <table id="MyTable_tableFixClone" border="1" cellspacing="0" cellpadding="0">
                       </table>
                     </div>
                     <div id="MyTable_tableHead">
                       <table id="MyTable_tableHeadClone" border="1" cellspacing="0" cellpadding="0">
                       </table>
                     </div>
                     <div id="MyTable_tableColumn">
                      <table id="MyTable_tableColumnClone" border="1" cellspacing="0" cellpadding="0">
                    </table>
                  </div>
                   <div id="MyTable_tableData">
                 <!--主数据-->
                     <ol runat="server" id="ulA">                    
                    </ol>
                      </div>
                     </div>
                </td>                
            </tr>
        </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">   
    <script type="text/javascript" language="javascript">
        function btnClient_Click() {
            if ($("#ddlDataName").find("option:selected").text() == "--请选择--") {
                $.dialog.alert("请先执行对比数据库！");
                return false;
            }
        }
        //锁定方法
        FixTable("MyTable", 2, 1185, 430);
        </script>
</asp:Content>
