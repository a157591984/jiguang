<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true"
    CodeBehind="SysUserModuleFunction.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysUserModuleFunction" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div class="iframe_section pa">
        <%=siteMap%>
        <div class="panel">
            <div class="panel-heading">
                <div class="panel-title">
                    <asp:Label ID="lblDoctorName" runat="server"></asp:Label>
                </div>
            </div>
            <div class="panel-body">
                <div class="form-inline search_bar mb">
                    <label>选择系统</label>
                    <input type="hidden" id="hidSysCode" runat="server" clientidmode="Static" />
                    <select id='selSysCode' class="form-control input-sm" onchange="SetFirstToSel_2('selSysCode','selFirstModule','hidSysCode','hidFirstModule','GetSysCodeToJson','GetModuleFristToJson');">
                        <option value="-1">--请选择--</option>
                    </select>
                    <label>选择一级模块</label>
                    <input type="hidden" id="hidFirstModule" runat="server" clientidmode="Static" />
                    <select id='selFirstModule' class="form-control input-sm" onchange="SetSecondToHid('selFirstModule','hidFirstModule');Search();">
                        <option value="-1">--请选择--</option>
                    </select>
                    <asp:Button ID="btnSearch" Text="查询" CssClass="btn btn-default btn-sm" ClientIDMode="Static" runat="server"
                        OnClick="btnSearch_Click" />
                </div>
                <asp:TreeView ID="tvMF" runat="server"></asp:TreeView>
                <div class="mt">
                    <asp:HiddenField ID="hidCheckedValue" runat="server" />
                    <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary" Text="提交" OnClientClick='setValue();'
                        OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnBack" Text="返回" CssClass="btn btn-default" runat="server" OnClick="btnBack_Click" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function setValue() {
            var arrVal = new Array();
            $("#<%=tvMF.ClientID %> :checkbox:checked").each(function () {
                arrVal.push($(this).val());
            });

            $("#<%=hidCheckedValue.ClientID %>").val(arrVal.join(","));

        }
        function selAll(objControl) {
            var idTemp = "_" + objControl.id;

            $("#<%=tvMF.ClientID %> :checkbox").each(function () {
                //对下级操作 
                var thisId = "_" + this.id;
                if (thisId.indexOf(idTemp) >= 0) {
                    if (this.id != objControl.id) {
                        if ($(objControl).prop("checked")) {
                            $(this).prop("checked", true);
                        }
                        else {
                            $(this).prop("checked", false);
                        }
                    }
                }
            });

            UpIsChecked(objControl.id);
        }

        //上级的选中情况
        function UpIsChecked(id) {

            if (id.lastIndexOf('_') == -1) {
                return false;
            }
            var id_temp = id.substring(0, id.lastIndexOf('_'));
            var checked = false
            $("#<%=tvMF.ClientID %> :checkbox").each(function () {
                //如果上一级的所有下级中有一个选中的上级为选中状态，否则为不选中状态
                if (this.id.indexOf(id_temp) >= 0) {
                    if (this.id != id_temp) {
                        if ($(this).prop("checked")) {
                            checked = true;
                        }
                    }
                }
            });
            $("#" + id_temp).prop("checked", checked);
            if (id_temp.indexOf("_") != -1) UpIsChecked(id_temp);

        }

        SetFirstToSel_Init_2('selSysCode', 'selFirstModule', 'hidSysCode', 'hidFirstModule', 'GetSysCodeToJson', 'GetModuleFristToJson');

        function Search() {
            $("#btnSearch").click();
        }
        $(function () {
            if ($("#selSysCode option:selected").val() == "-1" && $('#selSysCode option').length > 1) {
                $("#selSysCode").get(0).selectedIndex = 1;
                SetFirstToSel_2('selSysCode', 'selFirstModule', 'hidSysCode', 'hidFirstModule', 'GetSysCodeToJson', 'GetModuleFristToJson');
            }
            else {
                $("#selSysCode").change(function () {
                    if ($("#selFirstModule option:selected").val() == "-1" && $('#selFirstModule option').length > 1) {
                        $("#selFirstModule ").get(0).selectedIndex = 1;
                        var sysfirstcode = $("selFirstModule").val();
                        var sysfirstname = $("selFirstModule").text();
                        $("hidFirstModule").val(sysfirstcode + "|" + sysfirstname);
                        SetSecondToHid('selFirstModule', 'hidFirstModule');
                    }
                    Search();
                })
            }
            if ($("#selFirstModule option:selected").val() == "-1" && $('#selFirstModule option').length > 1) {
                $("#selFirstModule ").get(0).selectedIndex = 1;
                SetSecondToHid('selFirstModule', 'hidFirstModule'); Search();
            }
        })
    </script>
</asp:Content>
