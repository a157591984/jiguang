<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysTasking.aspx.cs" Inherits="Rc.Cloud.Web.Sys.SysTasking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <link href="../SysLib/plugin/bootstrap-3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../SysLib/css/style.css" rel="stylesheet" />
    <script src="../SysLib/js/jquery.min-1.9.1.js"></script>
    <script src="../SysLib/plugin/bootstrap-3.3.7/js/bootstrap.min.js"></script>
    <script src="../SysLib/js/index.js"></script>
    <script src="../SysLib/plugin/layer-3.0.1/layer.js"></script>
    <script src="../SysLib/js/function.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnSave").click(function () {
                _arrTaskNFVal = new Array(), _arrTaskNJXQVal = new Array(), _arrTaskJCBBVal = new Array(), _arrTaskXKVal = new Array();
                $("input[type='checkbox'][name='NF']:checked").each(function () {
                    _arrTaskNFVal.push($(this).val());
                });
                $("input[type='checkbox'][name='NJXQ']:checked").each(function () {
                    _arrTaskNJXQVal.push($(this).val());
                });
                $("input[type='checkbox'][name='JCBB']:checked").each(function () {
                    _arrTaskJCBBVal.push($(this).val());
                });
                $("input[type='checkbox'][name='XK']:checked").each(function () {
                    _arrTaskXKVal.push($(this).val());
                });
                if (_arrTaskNFVal.length == 0) {
                    layer.ready(function () {
                        layer.msg('请选择年份', { icon: 4 });
                    });
                    return false;
                }
                if (_arrTaskNJXQVal.length == 0) {
                    layer.ready(function () {
                        layer.msg('请选择年级学期', { icon: 4 });
                    });
                    return false;
                }
                if (_arrTaskJCBBVal.length == 0) {
                    layer.ready(function () {
                        layer.msg('请选择教材版本', { icon: 4 });
                    });
                    return false;
                }
                if (_arrTaskXKVal.length == 0) {
                    layer.ready(function () {
                        layer.msg('请选择学科', { icon: 4 });
                    });
                    return false;
                }

                var dto = {
                    targetUser: "<%=userId%>",
                    nf: _arrTaskNFVal.toString(),
                    njxq: _arrTaskNJXQVal.toString(),
                    jcbb: _arrTaskJCBBVal.toString(),
                    xk: _arrTaskXKVal.toString(),
                    x: Math.random()
                }
                var index = parent.layer.getFrameIndex(window.name);
                $.ajaxWebService("SysTasking.aspx/TaskingSave", JSON.stringify(dto), function (data) {
                    if (data.d == "1") {
                        parent.layer.ready(function () {
                            parent.layer.msg('任务分配成功', { icon: 1, time: 1000 }, function () {
                                parent.layer.close(index);
                            });
                        });
                    }
                    else {
                        parent.layer.ready(function () {
                            parent.layer.msg('任务分配失败', { icon: 2 });
                        });
                    }
                }, function () {
                    parent.layer.ready(function () {
                        parent.layer.msg('任务分配失败', { icon: 2 });
                    });
                }, false);

            });

        });
    </script>
</head>
<body class="bg_white">
    <form id="form1" runat="server">
        <div class="pa">
                <div class="form-group">
                    <label>年份</label>
                    <div class="checkbox">
                        <%
                            string strYear = string.Empty;
                            int intYear = DateTime.Now.Year;
                            for (int i = intYear - 2; i < intYear + 5; i++)
                            {
                                strYear += "<label><input type=\"checkbox\" name=\"NF\" value=\"" + i + "|" + i + "\" " + (strArrYear.Contains(i.ToString()) ? "checked" : "") + ">" + i + "</label>";
                            }
                            Response.Write(strYear);
                        %>
                    </div>
                </div>
                <div class="form-group">
                    <label>年级学期</label>
                    <div class="checkbox">
                        <asp:Repeater runat="server" ID="rptGradeTerm">
                            <ItemTemplate>

                                <label>
                                    <input type="checkbox" name="NJXQ" value="<%#Eval("Common_Dict_ID") %>|<%#Eval("D_Name") %>" <%#Eval("chk") %> /><%#Eval("D_Name") %></label>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="form-group">
                    <label>教材版本</label>
                    <div class="checkbox">
                        <asp:Repeater runat="server" ID="rptVersion">
                            <ItemTemplate>

                                <label>
                                    <input type="checkbox" name="JCBB" value="<%#Eval("Common_Dict_ID") %>|<%#Eval("D_Name") %>" <%#Eval("chk") %> /><%#Eval("D_Name") %></label>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="form-group">
                    <label>学科</label>
                    <div class="checkbox">
                        <asp:Repeater runat="server" ID="rptSubject">
                            <ItemTemplate>

                                <label>
                                    <input type="checkbox" name="XK" value="<%#Eval("Common_Dict_ID") %>|<%#Eval("D_Name") %>" <%#Eval("chk") %> /><%#Eval("D_Name") %></label>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <input type="button" name="btnSave" value="保存" id="btnSave" class="btn btn-primary" />
            </div>
    </form>
</body>
</html>
