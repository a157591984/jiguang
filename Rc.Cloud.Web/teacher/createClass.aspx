<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateClass.aspx.cs" Inherits="Rc.Cloud.Web.teacher.CreateClass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>创建新班级</title>
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/jquery.min-1.11.1.js"></script>
    <script type="text/javascript" src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../plugin/layer/layer.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#txtClassName").bind({
                blur: function () { this.value = js_validate.Filter(this.value); }
            });
            $("#txtClassIntro").bind({
                keyup: function () { this.value = this.value.slice(0, 2000); },
                blur: function () { this.value = this.value.slice(0, 2000); }
            });

            $("#ddlGradeType").change(function () {
                var StrShortGrade = "<option value='1'>一年级</option><option value='2'>二年级</option>" +
                    "<option value='3'>三年级</option><option value='4'>四年级</option><option value='5'>五年级</option><option value='6'>六年级</option>";
                var StrHightGrade = "<option value='1'>一年级</option><option value='2'>二年级</option>" +
                    "<option value='3'>三年级</option>";
                if ($(this).val() == 'f959628d-6abf-4360-9411-8e75f6604a20') {
                    $("#ddlGrade").html(StrShortGrade);
                }
                else if ($(this).val() == "-1") { $("#ddlGrade").html("<option value='-1'>-请选择-</option>"); }
                else {
                    $("#ddlGrade").html(StrHightGrade);
                }
            })
        });
        function Check() {
            if ($.trim($('#txtClassName').val()) == '') {
                layer.ready(function () {
                    layer.msg('班级名称不能为空！', { icon: 4 });
                });
                return false;
            }
            if ($('#ddlGradeType').val() == '-1') {
                layer.ready(function () {
                    layer.msg('请选择学段！', { icon: 4 });
                });
                return false;
            }
            if ($('#ddlStartSchoolYear').val() == '-1') {
                layer.ready(function () {
                    layer.msg('请选择入学年份！', { icon: 4 });
                });
                return false;
            }
            if ($.trim($("#txtSort").val()) == "") {
                layer.ready(function () {
                    layer.msg('顺序不能为空！', { icon: 4 });
                });
                return false;
            }
            $("#HidGrade").val($("#ddlGrade").val());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pv pt">
            <div class="row">
                <div class="col-xs-8">
                    <div class="form-group">
                        <label class="required">班级名称：</label>
                        <asp:TextBox ID="txtClassName" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="30"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-4">
                    <div class="form-group">
                        <label class="required">顺序：</label>
                        <asp:TextBox ID="txtSort" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="6" onkeyup="this.value=this.value.replace(/\D/g,'');" onblur="this.value=this.value.replace(/\D/g,'');">1</asp:TextBox>
                        <%--<p class="help-block">班级在年级中的顺序，如一班为1，二班为2......</p>--%>
                    </div>
                </div>
                <div class="col-xs-4">
                    <div class="form-group">
                        <label class="required">学段：</label>
                        <asp:DropDownList runat="server" ID="ddlGradeType" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-xs-4">

                    <div class="form-group">
                        <label class="required">年级：</label>
                        <select id='ddlGrade' runat='server' class="form-control" clientidmode="Static">
                            <option value='-1'>-请选择-</option>
                        </select>
                        <asp:HiddenField ID="HidGrade" ClientIDMode="Static" runat="server" />
                    </div>
                </div>
                <div class="col-xs-4">

                    <div class="form-group">
                        <label class="required">入学年份：</label>
                        <asp:DropDownList runat="server" ID="ddlStartSchoolYear" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label>班级简介：</label>
                <asp:TextBox ID="txtClassIntro" runat="server" ClientIDMode="Static" TextMode="MultiLine" CssClass="form-control" Rows="8"></asp:TextBox>
            </div>
            <div class="form-group text-right">
                <asp:Button ID="ButtonOK" runat="server" CssClass="btn btn-primary" Text="确定" ClientIDMode="Static" OnClientClick="return Check();" OnClick="ButtonOK_Click" />
            </div>
        </div>
    </form>
</body>
</html>
