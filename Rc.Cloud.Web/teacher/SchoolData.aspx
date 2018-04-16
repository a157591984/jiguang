<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="SchoolData.aspx.cs" Inherits="Rc.Cloud.Web.teacher.SchoolData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        $(function () {
            _ugroupId = "<%=ugroupId%>";

            $("#txtUserGroup_BriefIntroduction").bind({
                keyup: function () { this.value = this.value.slice(0, 2000); },
                blur: function () { this.value = this.value.slice(0, 2000); }
            });

            $("#txtUserGroup_Name").bind({
                blur: function () { this.value = js_validate.Filter(this.value); }
            });

            $('.mtree-class-hook').mtree({
                onClick: function (obj) {
                    window.location.href = obj.data('url');
                }
            });
        });
        function Handel(isSuccess, mes) {
            layer.msg(mes, {
                icon: isSuccess,
                time: 1000 //2秒关闭（如果不配置，默认是3秒）
            }, function () {
                //do something
                if (isSuccess == 1) window.location.href = "SchoolData.aspx?ugroupId=" + _ugroupId;
            });
        }

        function Check() {
            if ($.trim($('#txtUserGroup_Name').val()) == '') {
                layer.alert('学校名称不能为空！', { icon: 4 });
                return false;
            }
            if ($('#txtUserGroup_BriefIntroduction').val() == '') {
                layer.alert('学校简介不能为空！', { icon: 4 });
                return false;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="iframe-container">
        <div class="iframe-sidebar">
            <div class="mtree mtree-class-hook">
                <ul>
                    <asp:Repeater runat="server" ID="rptClass">
                        <ItemTemplate>
                            <li>
                                <div class="mtree_link mtree-link-hook <%#GetStyle(Eval("UserGroup_Id").ToString()) %>"
                                    data-url="SchoolData.aspx?ugroupId=<%#Eval("UserGroup_Id") %>">
                                    <div class="mtree_indent mtree-indent-hook"></div>
                                    <div class="mtree_btn mtree-btn-hook"></div>
                                    <div class="mtree_name mtree-name-hook"><%#Rc.Cloud.Web.Common.pfunction.GetSubstring(Eval("UserGroup_Name").ToString(),15,false) %></div>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="iframe-main pa">
            <ul class="nav nav-tabs mb">
                <li class="active"><a href="SchoolData.aspx?ugroupId=<%=ugroupId %>">资料</a></li>
                <li><a href="SchoolMember.aspx?ugroupId=<%=ugroupId %>">成员</a></li>
                <li><a href="SchoolVerifyNotice.aspx?ugroupId=<%=ugroupId %>">消息</a></li>
            </ul>
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-xs-2 control-label">学校名称：</div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtUserGroup_Name" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="30"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 form-control-static text-muted">必填</div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 control-label">学校号：</div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtUserGroup_Id" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 control-label">学校简介：</div>
                    <div class="col-xs-6">
                        <asp:TextBox ID="txtUserGroup_BriefIntroduction" runat="server" ClientIDMode="Static" TextMode="MultiLine" CssClass="form-control" Rows="8"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 form-control-static text-muted">必填</div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 col-xs-offset-2">
                        <asp:Button ID="ButtonOK" runat="server" CssClass="btn btn-primary" Text="更新" OnClientClick="return Check();" OnClick="ButtonOK_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>
