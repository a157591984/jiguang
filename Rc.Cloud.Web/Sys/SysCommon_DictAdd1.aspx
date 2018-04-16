<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysCommon_DictAdd1.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysCommon_DictAdd1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../styles/styles003/layOut.css" rel="stylesheet" type="text/css" />
    <link href="../styles/styles003/style01.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/styles003/sdmenu.css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../scripts/jQuery_Dialog.js"></script>
    <script type="text/javascript" src="../scripts/jquery.easydrag.js"></script>
    <script type="text/javascript" src="../scripts/function.js"></script>
    <link href="../Styles/Dialog.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/PrintStytle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/sdmenu.js"></script>
    <script src="../Scripts/ToolAnalytics.js" type="text/javascript"></script>
    <script src="../Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/ui/jquery.ui.dialog.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" class="table_content">
                <tr>
                    <td class="td_content_001" width="30%">名称<font color="red">*</font>：
                    </td>
                    <td class="td_content_002" width="70%">
                        <asp:TextBox ID="txtD_Name" runat="server" ClientIDMode="Static" CssClass="txt" IsRequired="true" MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <%-- <tr>
            <td class="td_content_001" width="30%">    
                排序：
            </td>
            <td class="td_content_002" width="70%">
                <asp:TextBox ID="txtD_Order" runat="server" CssClass="txt" WatermarkText=""></asp:TextBox>
            </td>
        </tr>--%>
                <tr>
                    <td class="td_content_001" width="30%">类型<font color="red">*</font>：
                    </td>
                    <td class="td_content_002" width="70%">
                        <asp:DropDownList ID="ddlD_Type" runat="server" ClientIDMode="Static" onchange="SelectDateType()" IsRequired="true" IsAddEmptyItem="true" EmptyItemType="Choice" CssClass="txt">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="dValue" style="display: none" clientidmode="Static">
                    <td class="td_content_001" width="30%">值：
                    </td>
                    <td class="td_content_002" width="70%">
                        <asp:TextBox ID="txtD_Value" runat="server" CssClass="txt" WatermarkText="" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td_content_001" width="30%">备注：
                    </td>
                    <td class="td_content_002" width="70%">
                        <asp:TextBox ID="txtRemark" TextMode="MultiLine" Height="50px" Style="width: 95%;"
                            CssClass="txt_area" runat="server" MaxLength="255"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div class="div_page_bottom_operation">
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn" OnClick="btnSave_Click" />
                <input id="Button1" class="btn" type="button" value="取 消" onclick="window.parent.CloseDialog();" />
            </div>
        </div>
    </form>

    <script type="text/javascript">
        function SelectDateType() {
            if ($("#ddlD_Type").val() == "0") {
                $("#dValue").show();
                $.get("../Ajax/SysAjax.aspx", { key: "GetMaxType", net4: Math.random() },
                    function (data) {
                        $("#txtD_Value").val(data)
                    });
            }
            else {
                $("#dValue").hide();
            };
            $("#txtRemark").val(document.getElementById("ddlD_Type").options[document.getElementById("ddlD_Type").selectedIndex].text);
            //$.get("../Ajax/SysAjax.aspx", { key: "GetD_Remark", Aid: document.getElementById("ddlD_Type").options[document.getElementById("ddlD_Type").selectedIndex].text, net4: Math.random() },
            //       function (data) {
            //           $("#txtRemark").val(data)
            //       });
        }
    </script>
</body>
</html>
