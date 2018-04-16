<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysMenuManageEdit.aspx.cs"
    Inherits="Rc.HospitalConfigManage.Web.Sys.SysMenuManageEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
</head>
<body class="bg_white">
    <form id="form2" name="form1" runat="server">
        <div class="pa">
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>编码 <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtMODULEID" runat="server" CssClass="form-control" IsRequired="true"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>名称 <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtMODULENAME" runat="server" CssClass="form-control" IsRequired="true"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>父模块编码 <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtPARENTID" runat="server" CssClass="form-control" IsRequired="true"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>SLEVEL <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtSLEVEL" runat="server" CssClass="form-control" IsRequired="true"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>菜单级别 <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtDepth" runat="server" CssClass="form-control" IsRequired="true" WatermarkText=""></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>默认排序</label>
                        <asp:TextBox ID="txtDefaultOrder" runat="server" CssClass="form-control" IsRequired="true" WatermarkText=""></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label>链接地址 <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtURL" runat="server" CssClass="form-control" IsRequired="true" MaxLength="10000"
                    Height="59px" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>是否显示</label>
                        <div class="radio radio_1">
                            <asp:RadioButton ID="rbtISINTREE1" GroupName="ISINTREE" ClientIDMode="Static" value="1"
                                Text="是" runat="server" />
                            <asp:RadioButton ID="rbtISINTREE0" GroupName="ISINTREE"
                                ClientIDMode="Static" value="0" Text="否" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>是否最后一级菜单</label>
                        <div class="radio radio_1">
                            <asp:RadioButton ID="rbtisLast1" value="1" GroupName="isLast" ClientIDMode="Static"
                                Text="是" runat="server" />
                            <asp:RadioButton ID="rbtisLast0" GroupName="isLast" ClientIDMode="Static"
                                value="0" Text="否" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <asp:Button runat="server" CssClass="btn btn-primary" Text="提交" ID="btn" OnClick="btn_Search_Click" />
        </div>
    </form>
</body>
</html>
