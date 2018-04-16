<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/student.Master" AutoEventWireup="true" CodeBehind="classInfo.aspx.cs" Inherits="Homework.student.classInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            _ugroupId = "<%=ugroupId%>";
            StrShortGrade = "<option value='1'>一年级</option><option value='2'>二年级</option>" +
                "<option value='3'>三年级</option><option value='4'>四年级</option><option value='5'>五年级</option><option value='6'>六年级</option>";
            $("#ddlGrade").html(StrShortGrade);
            $("#ddlGrade").val($("#HidGrade").val());

            $('.mtree-class-hook').mtree({
                onClick: function (obj) {
                    window.location.href = obj.data('url');
                }
            });
        });
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
                                <div class="mtree_link mtree-link-hook <%#GetStyle(Eval("UserGroup_Id").ToString()) %>" data-url="classInfo.aspx?ugroupId=<%#Eval("UserGroup_Id") %>">
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
                <li class="active"><a href="classInfo.aspx?ugroupId=<%=ugroupId %>">资料</a></li>
                <li><a href="classMember.aspx?ugroupId=<%=ugroupId %>">成员</a></li>
            </ul>
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-xs-2 control-label">创建人：</div>
                    <div class="col-xs-3">
                        <asp:TextBox ReadOnly ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 control-label">班级名称：</div>
                    <div class="col-xs-3">
                        <asp:TextBox ReadOnly ID="txtUserGroup_Name" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 control-label">班号：</div>
                    <div class="col-xs-3">
                        <asp:TextBox ReadOnly ID="txtUserGroup_Id" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 control-label">学段：</div>
                    <div class="col-xs-3">
                        <asp:DropDownList Enabled="false" runat="server" ID="ddlGradeType" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 control-label">年级：</div>
                    <div class="col-xs-3">
                        <select id='ddlGrade' disabled="disabled" runat='server' class="form-control" clientidmode="Static">
                            <option value='-1'>-无-</option>
                        </select>
                        <asp:HiddenField ID="HidGrade" ClientIDMode="Static" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 control-label">入学年份：</div>
                    <div class="col-xs-3">
                        <asp:DropDownList Enabled="false" runat="server" ID="ddlStartSchoolYear" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group hide">
                    <div class="col-xs-2 control-label">班：</div>
                    <div class="col-xs-3">
                        <asp:TextBox ReadOnly ID="txtClass" runat="server" ClientIDMode="Static" CssClass="form-control" onkeyup="this.value=this.value.replace(/\D/g,'');"
                            onblur="this.value=this.value.replace(/\D/g,'');"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 control-label">班的简介：</div>
                    <div class="col-xs-6">
                        <asp:TextBox ReadOnly ID="txtUserGroup_BriefIntroduction" runat="server" ClientIDMode="Static" TextMode="MultiLine" CssClass="form-control" Rows="8"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
