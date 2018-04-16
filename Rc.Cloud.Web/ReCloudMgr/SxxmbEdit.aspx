<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SxxmbEdit.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.SxxmbEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <style type="text/css">
        .bar {
            margin-top: 10px;
            height: 10px;
            background: #159C77;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            index = parent.layer.getFrameIndex(window.name);
            $("#txtTwo_WayChecklist_Name").bind({
                blur: function () { if (this.value.length > 30) this.value = this.value.slice(0, 30) },
                keyup: function () { if (this.value.length > 30) this.value = this.value.slice(0, 30) }
            });
            $("#txtRemark").bind({
                blur: function () { if (this.value.length > 300) this.value = this.value.slice(0, 300) },
                keyup: function () { if (this.value.length > 300) this.value = this.value.slice(0, 300) }
            });
            $("#btnSubmit").click(function () {
                if ($("#ddlYear").val() == "-1") {
                    layer.msg("请选择年份", { time: 2000, icon: 2 }, function () { $("#ddlYear").focus(); });
                    return false;
                }
                if ($("#ddlGradeTerm").val() == "-1") {
                    layer.msg("请选择年级学期", { time: 2000, icon: 2 }, function () { $("#ddlGradeTerm").focus(); });
                    return false;
                }

                if ($("#ddlResource_Version").val() == "-1") {
                    layer.msg("请选择教材版本", { time: 2000, icon: 2 }, function () { $("#ddlResource_Version").focus(); });
                    return false;
                }
                if ($("#ddlSubject").val() == "-1") {
                    layer.msg("请选择学科", { time: 2000, icon: 2 }, function () { $("#ddlSubject").focus(); });
                    return false;
                }
                if ($.trim($("#txtTwo_WayChecklist_Name").val()) == "") {
                    layer.msg("请填写细目表名称", { time: 2000, icon: 2 }, function () { $("#txtTwo_WayChecklist_Name").focus(); });
                    return false;
                }
            })

        })
    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
            <div class="form-group">
                <label>年份<span>*</span></label>
                <asp:DropDownList ID="ddlYear" CssClass="form-control" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>年级学期<span>*</span></label>
                <asp:DropDownList ID="ddlGradeTerm" CssClass="form-control" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>教材版本<span>*</span></label>
                <asp:DropDownList ID="ddlResource_Version" CssClass="form-control" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>学科<span>*</span></label>
                <asp:DropDownList ID="ddlSubject" CssClass="form-control" ClientIDMode="Static" runat="server">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>名称<span>*</span></label>
                <asp:TextBox runat="server" ID="txtTwo_WayChecklist_Name" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>描述</label>
                <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Button ID="btnSubmit" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </form>
</body>
</html>
