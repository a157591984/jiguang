<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrepareLessons.aspx.cs" Inherits="Rc.Cloud.Web.teacher.PrepareLessons" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../js/jquery.min-1.11.1.js"></script>
    <script src="../plugin/bootstrap/js/bootstrap.min.js"></script>
    <script src="../plugin/layer/layer.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <link href="../plugin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../plugin/fontawesome/fontawesome.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../Scripts/plug-in/laydate/laydate.js"></script>
    <script src="../Scripts/jquery-jtemplates.js"></script>
    <script src="../Scripts/function.js"></script>
    <title><%=Rc.Common.ConfigHelper.GetConfigString("WebSiteName") %></title>
    <script type="text/javascript">
        $(function () {
            loadData();
            // 日期
            var StarTime = {
                elem: '#txtStartTime',
                format: 'YYYY-MM-DD',
                choose: function (datas) {
                    EndTime.min = datas; //开始日选好后，重置结束日的最小日期
                    EndTime.start = datas //将结束日的初始值设定为开始日
                }
            }
            var EndTime = {
                elem: '#txtEndTime',
                format: 'YYYY-MM-DD',
                choose: function (datas) {
                    StarTime.max = datas; //结束日选好后，重置开始日的最大日期
                }
            }
            laydate(StarTime);
            laydate(EndTime);
            //window.onload = function () {

            //    document.getElementById('txtRequire').addEventListener('keydown', function (e) {

            //        if (e.keyCode != 13) return;

            //        e.preventDefault();

            //        this.value += '</n>';

            //    });

            //};
            $("#txtResourceFolder_Name").bind({
                blur: function () { if (this.value.length > 100) this.value = this.value.slice(0, 100) },
                keyup: function () { if (this.value.length > 100) this.value = this.value.slice(0, 100) }
            });
            $("#txtRequire").bind({
                blur: function () { if (this.value.length > 200) this.value = this.value.slice(0, 200) },
                keyup: function () { if (this.value.length > 200) this.value = this.value.slice(0, 200) }
            });
            $("#txtNameRule").bind({
                blur: function () { if (this.value.length > 200) this.value = this.value.slice(0, 200) },
                keyup: function () { if (this.value.length > 200) this.value = this.value.slice(0, 200) }
            });
            $("#btnSave").click(function () {
                if ($.trim($("#ddlGrade").val()) == "-1") {
                    layer.msg("请选择年级", { time: 2000, icon: 2 }, function () { $("#ddlGrade").focus(); });
                    return false;
                }
                if ($.trim($("#ddlSubject").val()) == "-1") {
                    layer.msg("请选择学科", { time: 2000, icon: 2 }, function () { $("#ddlSubject").focus(); });
                    return false;
                }
                if ($.trim($("#ddlStage").val()) == "-1") {
                    layer.msg("请选择年级", { time: 2000, icon: 2 }, function () { $("#ddlStage").focus(); });
                    return false;
                }
                if ($.trim($("#txtResourceFolder_Name").val()) == "") {
                    layer.msg("请填写备课名称", { time: 2000, icon: 2 }, function () { $("#txtResourceFolder_Name").focus(); });
                    return false;
                }
                if ($.trim($("#txtStartTime").val()) == "") {
                    layer.msg("请选择计划开始时间", { time: 2000, icon: 2 }, function () { $("#txtStartTime").focus(); });
                    return false;
                }
                if ($.trim($("#txtEndTime").val()) == "") {
                    layer.msg("请选择计划结束时间", { time: 2000, icon: 2 }, function () { $("#txtEndTime").focus(); });
                    return false;
                }
                if ($('#tb1').find('td').length == 1) {
                    layer.msg("请选择参与人", { time: 2000, icon: 2 });
                    return false;
                }
            })
            //选择联系人
            $('.person-charge-hook').on('click', function () {
                var GradeId = $("#ddlGrade").val();
                var SubjectId = $("#ddlSubject").val();
                var ResourceFolder_Id = $("#hidResourceFolder_Id").val();
                if (GradeId == "-1" || GradeId == undefined || SubjectId == "-1" || SubjectId == undefined) {
                    layer.msg('请先选择年级和学科', { icon: 2, time: 4000 });
                    return false;
                }
                else {
                    layer.open({
                        type: 2,
                        title: '选择参与人',
                        area: ['800px', '650px'],
                        content: "SelectPersonCharge.aspx?GradeId=" + GradeId + "&SubjectId=" + SubjectId + "&ResourceFolder_Id=" + ResourceFolder_Id
                    });
                }
            })
        })
        var loadData = function () {
            var dto = {
                ResourceFolder_Id: $("#hidResourceFolder_Id").val(),
                x: Math.random()
            };
            $.ajaxWebService("PrepareLessons.aspx/GetList", JSON.stringify(dto), function (data) {
                var json = $.parseJSON(data.d);
                if (json.err == "null") {
                    $("#tb1").setTemplateElement("template_1", null, { filter_data: false });
                    $("#tb1").processTemplate(json);

                }
                else {
                    $("#tb1").html("<tr><td colspan='100' align='center'>暂无参与人请选择</td></tr>");
                }
            }, function () { });
        }
        var DeletePerson = function (userId, ResourceFolder_Id) {
            var dto = {
                ChargePerson: userId,
                ResourceFolder_Id: ResourceFolder_Id,
                x: Math.random()
            };
            $.ajaxWebService("PrepareLessons.aspx/DeletePerson", JSON.stringify(dto), function (data) {
                if (data.d == "1") {
                    layer.msg("移除成功", { time: 2000, icon: 1 }, function () { loadData(); });
                } else {
                    layer.msg("移除失败", { time: 2000, icon: 2 });
                }
            }, function () { });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container ph prepare_lessons_container">
            <div class="page_title">发起备课</div>
            <div class="row">
                <div class="col-xs-4">
                    <div class="form-group">
                        <label>年级</label>
                        <asp:DropDownList ID="ddlGrade" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-xs-4">
                    <div class="form-group">
                        <label>学科</label>
                        <asp:DropDownList ID="ddlSubject" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-xs-4">
                    <div class="form-group">
                        <label>阶段</label>
                        <asp:DropDownList ID="ddlStage" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label>备课名称</label>
                <asp:TextBox ID="txtResourceFolder_Name" runat="server" CssClass="form-control" ClientIDMode="Static" placeholder="备课名称"></asp:TextBox>
            </div>
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>计划开始时间</label>
                        <asp:TextBox ID="txtStartTime" runat="server" CssClass="form-control" ClientIDMode="Static" placeholder="计划开始时间"></asp:TextBox>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="form-group">
                        <label>计划结束时间</label>
                        <asp:TextBox ID="txtEndTime" runat="server" CssClass="form-control" ClientIDMode="Static" placeholder="计划结束时间"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label>备课要求</label>
                <%--<textarea rows="4" class="form-control" id="txtRequire" runat="server" clientidmode="Static"></textarea>--%>
                <asp:TextBox TextMode="MultiLine" Rows="5" CssClass="form-control" ID="txtRequire" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>参与人</label>
                <div>
                    <p>
                        <button type="button" id="btnSelect" class="btn-link person-charge-hook"><i class="fa fa-plus-circle"></i>&nbsp;选择参与人</button>
                    </p>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>名称</th>
                                <th>班级</th>
                                <th>学科</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody id="tb1">
                        </tbody>
                    </table>
                </div>
                <textarea id="template_1" style="display: none">
                {#foreach $T.list as record}
                <tr>
                    <td>{$T.record.UserName}{#if $T.record.TrueName!=''}({$T.record.TrueName}){#else}{#/if}</td>
                    <td>{$T.record.ClassName}</td>
                    <td>{$T.record.Subject}</td>
                    <td class="opera"><a href="javascript:;" onclick="DeletePerson('{$T.record.UserId}','{$T.record.ResourceFolder_Id}')">移除</a></td>
                </tr>
                {#/for}
                </textarea>
            </div>
            <div class="form-group">
                <label>备注</label>
                <textarea rows="2" class="form-control" placeholder="例：张三负责第N到第N课时" id="txtRemark" runat="server" clientidmode="Static"></textarea>
            </div>
            <div class="form-group">
                <label>课时文件命名规则</label>
                <textarea rows="2" class="form-control" id="txtNameRule" runat="server" clientidmode="Static">为保证课时能按正常顺序显示，给课时文件命名规则为：001-课时名称，001 为实际的第1课时。</textarea>
            </div>
            <asp:HiddenField ID="hidResourceFolder_Id" runat="server" />
            <asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btn btn-primary" ClientIDMode="Static" OnClick="btnSave_Click" />
        </div>
    </form>
</body>
</html>
