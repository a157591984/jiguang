﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="ImportGrade.aspx.cs" Inherits="Rc.Cloud.Web.Sys.ImportGrade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section">
        <div class="pa">
            <%=siteMap%>
            <div class="panel">
                <div class="panel-heading">
                    <div class="panel-title">
                        <asp:Literal runat="server" ID="litTitle"></asp:Literal></td>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-inline search_bar mb">
                        <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control input-sm" />
                        <asp:Button class="btn btn-default btn-sm" type="button" Text="EXCEL导入" ID="btnImp" OnClientClick="return check()" runat="server" OnClick="btnImp_Click" />
                        <input type="button" id="btnBack" class="btn btn-default btn-sm" value="返回" onclick="historyBack();" />
                        <asp:HiddenField runat="server" ID="hidBackurl" ClientIDMode="Static" />
                        <a href="../Upload/Template/年级.xls" class="btn btn-default btn-sm">下载年级模板</a>
                    </div>

                    <div class="form-inline search_bar" id="divHandel" runat="server">
                        <input type="button" class="btn btn-default" onclick="showExcelData('good')" value='只显示验证通过' />
                        <input type="button" class="btn btn-default" onclick="showExcelData('bad')" value='只显示验证没通过' />
                        <input type="button" class="btn btn-default" onclick="showExcelData('all')" value='显示所有' />
                        <asp:Button ID="btnImpRight" runat="server" CssClass="btn btn-default" Text="导入验证通过年级"
                            OnClick="btnImpRight_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" Text="取消"
                            OnClick="btnCancel_Click" />
                    </div>
                    <div id="divBadData">
                        <%=GetBadData() %>
                    </div>
                    <div id="divGoodData">
                        <%=GetGoodData() %>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
    <script language="javascript" type="text/javascript">
        function check() {
            if ($("#<%=fileUpload.ClientID %>").val() == "") {
                layer.msg("请选择需要上传的EXCEL文件。", { time: 2000, icon: 2 });
                return false;
            }
            var doc_name = $("#<%=fileUpload.ClientID %>").val();
            var doc_type = doc_name.substring(doc_name.lastIndexOf("\\") + 1).split(".")[1];
            if (doc_type != "xls") {
                layer.msg("请选择后缀名为.xls文件。", { time: 2000, icon: 2 });
                return false;
            }
            layer.load();
            //return confirm("您将要导入年级信息");
        }
        function showExcelData(type) {
            if (type == "good") {
                $("#divBadData").hide();
                $("#divGoodData").show();
            }
            else if (type == "bad") {
                $("#divBadData").show();
                $("#divGoodData").hide();
            }
            else {
                $("#divBadData").show();
                $("#divGoodData").show();
            }
        }
        function historyBack() {
            b = new Base64();
            var backurl = $("#hidBackurl").val();
            if (backurl != "") {
                window.location.href = b.decode(backurl);
            }
            else {
                window.location.href = "SchoolList.aspx";
            }
        }
    </script>
</asp:Content>
