<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysCommon_DictAdd.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysCommon_DictAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>名称 <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtD_Name" runat="server" ClientIDMode="Static" CssClass="form-control" IsRequired="true" MaxLength="100"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>排序</label>
                <asp:TextBox ID="txtD_Order" runat="server" CssClass="form-control" WatermarkText=""></asp:TextBox>
            </div>
            <div class="form-group">
                <label>类型 <span class="text-danger">*</span></label>
                <asp:DropDownList ID="ddlD_Type" runat="server" ClientIDMode="Static" onchange="SelectDateType()" IsRequired="true" IsAddEmptyItem="true" EmptyItemType="Choice" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="form-group hidden" id="dValue">
                <label>值</label>
                <asp:TextBox ID="txtD_Value" runat="server" CssClass="form-control" WatermarkText="" Enabled="false"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>备注</label>
                <asp:TextBox ID="txtRemark" TextMode="MultiLine"
                    CssClass="form-control" Rows="5" runat="server" MaxLength="255"></asp:TextBox>
            </div>
            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
    </form>
</body>
</html>
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
    }
    document.getElementById("ddlD_Type").value = parent.document.getElementById("ddlD_Type").value;
</script>
