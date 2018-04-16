<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/teacher.Master" AutoEventWireup="true" CodeBehind="GradeData.aspx.cs" Inherits="Rc.Cloud.Web.teacher.GradeData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        $(function () {
            _ugroupId = "<%=ugroupId%>";
            StrShortGrade = "<option value='1'>一年级</option><option value='2'>二年级</option>" +
                "<option value='3'>三年级</option><option value='4'>四年级</option><option value='5'>五年级</option><option value='6'>六年级</option>";
            StrHightGrade = "<option value='1'>一年级</option><option value='2'>二年级</option>" +
                "<option value='3'>三年级</option>";
            $(".sub_nav ul li a[val='" + _ugroupId + "']").addClass("active");
            $("#ddlGrade").html(StrShortGrade);
            $("#ddlGrade").val($("#HidGrade").val());
            $("#txtUserGroup_BriefIntroduction").bind({
                keyup: function () { this.value = this.value.slice(0, 2000); },
                blur: function () { this.value = this.value.slice(0, 2000); }
            });

            $("#btnBack").click(function () { window.location.href = "GradeList.aspx"; });

            $("#ddlGradeType").change(function () {

                if ($(this).val() == 'f959628d-6abf-4360-9411-8e75f6604a20') {
                    $("#ddlGrade").html(StrShortGrade);
                }
                else if ($(this).val() == "-1") { $("#ddlGrade").html("<option value='-1'>--请选择--</option>"); }
                else {
                    $("#ddlGrade").html(StrHightGrade);
                }
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
                if (isSuccess == 1) window.location.href = "GradeData.aspx?ugroupId=" + _ugroupId;
            });
        }

        function Check() {
            if ($.trim($('#txtUserGroup_Name').val()) == '') {
                layer.alert('年级名称不能为空！', { icon: 4 });
                return false;
            }
            if ($('#ddlGradeType').val() == '-1') {
                layer.alert('请选择学段！', { icon: 4 });
                return false;
            }
            if ($('#ddlStartSchoolYear').val() == '-1') {
                layer.alert('请选择入学年份！', { icon: 4 });
                return false;
            }
            if ($.trim($("#txtSort").val()) == "") {
                layer.alert('顺序不能为空！', { icon: 4 });
                return false;
            }
            $("#HidGrade").val($("#ddlGrade").val());

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
                                    data-url="GradeData.aspx?ugroupId=<%#Eval("UserGroup_Id") %>">
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
                <li class="active"><a href="GradeData.aspx?ugroupId=<%=ugroupId %>">资料</a></li>
                <li><a href="GradeMember.aspx?ugroupId=<%=ugroupId %>">成员</a></li>
                <li><a href="GradeVerifyNotice.aspx?ugroupId=<%=ugroupId %>">消息</a></li>
            </ul>
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-xs-2 control-label">年级名称</div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtUserGroup_Name" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="30"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 form-control-static text-muted">必填</div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 control-label">年级号</div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtUserGroup_Id" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 control-label">学段</div>
                    <div class="col-xs-3">
                        <asp:DropDownList runat="server" ID="ddlGradeType" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                    <div class="col-xs-2 form-control-static text-muted">必填</div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 control-label">年级</div>
                    <div class="col-xs-3">
                        <select id='ddlGrade' runat='server' class="form-control" clientidmode="Static">
                            <option value='-1'>--请选择--</option>
                        </select>
                        <asp:HiddenField ID="HidGrade" ClientIDMode="Static" runat="server" />
                    </div>
                    <div class="col-xs-2 form-control-static text-muted">必填</div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 control-label">入学年份</div>
                    <div class="col-xs-3">
                        <asp:DropDownList runat="server" ID="ddlStartSchoolYear" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                    <div class="col-xs-2 form-control-static text-muted">必填</div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 control-label">顺序</div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtSort" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="6" onkeyup="this.value=this.value.replace(/\D/g,'');" onblur="this.value=this.value.replace(/\D/g,'');">1</asp:TextBox>
                        <p class="help-block">年级在学校中的顺序，如一年级为1，二年级为2......</p>
                    </div>
                    <div class="col-xs-2 form-control-static text-muted">必填</div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 control-label">年级简介</div>
                    <div class="col-xs-6">
                        <asp:TextBox ID="txtUserGroup_BriefIntroduction" runat="server" ClientIDMode="Static" TextMode="MultiLine" CssClass="form-control" Rows="8"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2 col-xs-offset-2">
                        <asp:Button ID="ButtonOK" runat="server" CssClass="btn btn-primary" Text="确定" OnClientClick="return Check();" OnClick="ButtonOK_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>
