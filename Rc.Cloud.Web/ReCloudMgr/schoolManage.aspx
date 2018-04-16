<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/sys.Master" AutoEventWireup="true" CodeBehind="schoolManage.aspx.cs" Inherits="Rc.Cloud.Web.ReCloudMgr.schoolManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_right_title">
        <div class="div_right_title_icon">
        </div>
        <div class="div_right_title_001" id="div_right_title_0">云资源管理</div>
        <div class="div_right_title_002"></div>
        <div class="div_right_title_001" id="div_right_title_1">基本数据管理</div>
        <div class="div_right_title_002"></div>
        <div class="div_right_title_001" id="div_right_title_2">学校管理</div>
    </div>
    <div class="clearDiv">
    </div>
    <div class="div_right_search">
        <table class="table_search_001">
            <tbody>
                <tr>
                    <td>学校名称：
                    </td>
                    <td>
                        <input name="ctl00$MainContent$txtName" type="text" maxlength="50" id="ctl00_MainContent_txtName" class="txt_Search myTextBox" isfiltersqlchars="True" isfilterspecialchars="True" myinputtype="TextBox" showerrortype="Inline"><span class="span_format_check" style="display: none;" id="span_format_check_for_ctl00_MainContent_txtName">内容不符要求</span>

                    </td>
                    <td>
                        <input type="submit" name="ctl00$MainContent$btn_Search" value="查 询" onclick="javascript: return Phhc_data_check_for_page_on_submit(false, '');" id="ctl00_MainContent_btn_Search" class="btn MyButton">
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="clearDiv">
    </div>
    <div class="div_right_listtitle">
        学校列表
    </div>
    <div class="clearDiv">
    </div>
    <!--主数据-->
    <table class="table_list" cellpadding="0" cellspacing="0">
        <tbody>
            <tr class="tr_title">
                <td>学校名称</td>
                <td style="width: 15%;">修改日期</td>
                <td style="width: 8%;">操作</td>
            </tr>
            <tr class="tr_con_001" style="background: rgb(255, 255, 255);">
                <td>普雷资源中心与组件系统接口需求 (1)</td>
                <td>2015-09-14 10:31</td>
                <td>&nbsp;&nbsp;<input type="button" title="编辑用户信息" class="btn_modify" onclick="showPopAddDepartment('3501308d-87ce-474c-a1ca-c6c738d1e53a', 1);">|<input type="button" class="btn_delete" title="删除用户信息" onclick="    Delete('3501308d-87ce-474c-a1ca-c6c738d1e53a')"></td>
            </tr>
            <tr class="tr_con_002" style="background: rgb(226, 239, 245);">
                <td>普雷资源中心与组件系统接口需求11 (1)</td>
                <td>2015-09-14 10:31</td>
                <td>&nbsp;&nbsp;<input type="button" title="编辑用户信息" class="btn_modify" onclick="    showPopAddDepartment('ac06121c-207e-439a-adad-fce4a1a8da3b', 1);">|<input type="button" class="btn_delete" title="删除用户信息" onclick="    Delete('ac06121c-207e-439a-adad-fce4a1a8da3b')"></td>
            </tr>
            <tr class="tr_con_001" style="background: rgb(255, 255, 255);">
                <td>系统功能框架 (1)</td>
                <td>2015-09-14 10:31</td>
                <td>&nbsp;&nbsp;<input type="button" title="编辑用户信息" class="btn_modify" onclick="    showPopAddDepartment('d243264a-10d3-4511-98d2-0b117320fe47', 1);">|<input type="button" class="btn_delete" title="删除用户信息" onclick="    Delete('d243264a-10d3-4511-98d2-0b117320fe47')"></td>
            </tr>
            <tr class="tr_con_002" style="background: rgb(247, 247, 247);">
                <td>普雷资源中心与组件系统接口需求</td>
                <td>2015-09-14 10:31</td>
                <td>&nbsp;&nbsp;<input type="button" title="编辑用户信息" class="btn_modify" onclick="    showPopAddDepartment('e591da11-4e2e-4e9d-8bfd-bd38edb2f896', 1);">|<input type="button" class="btn_delete" title="删除用户信息" onclick="    Delete('e591da11-4e2e-4e9d-8bfd-bd38edb2f896')"></td>
            </tr>
        </tbody>
    </table>
    <div class="clearDiv">
    </div>
    <!--分页-->
    <table class="table_pageindex" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td align="center">
                    <table style="margin-top: 10px; margin-bottom: 10px;">
                        <tbody>
                            <tr>
                                <td>
                                    <div class="fenye_btn2">首 页</div>
                                </td>
                                <td>
                                    <div class="fenye_btn2">上一页</div>
                                </td>
                                <td>&nbsp;&nbsp;共 <span style="color: #3C83AF">4</span>条数据</td>
                                <td>&nbsp;&nbsp;第<span id="MainContent_UCPagerTool1_span_page" style="color: #3C83AF"> 1 </span>页/<span id="MainContent_UCPagerTool1_span_curpage">共 <span style="color: #3C83AF">1 </span>页</span></td>
                                <td>
                                    <div class="fenye_btn2">下一页</div>
                                </td>
                                <td>
                                    <div class="fenye_btn2">末 页</div>
                                </td>
                                <td>&nbsp;&nbsp;显示第</td>
                                <td>
                                    <select id="select_pageindex" onchange="PageIndexChange(this);">
                                        <option value="TeacherAudit.aspx?PageIndex=1&amp;PageSize=10&amp;name=" selected="selected">1</option>
                                    </select>
                                </td>
                                <td>页 &nbsp;每页显示数量&nbsp;</td>
                                <td>
                                    <select id="select_pagesize" onchange="PageSizeChange(this);" style="font-size: 13px;">
                                        <option value="TeacherAudit.aspx?PageIndex=1&amp;PageSize=5&amp;name=">5</option>
                                        <option value="TeacherAudit.aspx?PageIndex=1&amp;PageSize=10&amp;name=" selected="selected">10</option>
                                        <option value="TeacherAudit.aspx?PageIndex=1&amp;PageSize=15&amp;name=">15</option>
                                        <option value="TeacherAudit.aspx?PageIndex=1&amp;PageSize=20&amp;name=">20</option>
                                        <option value="TeacherAudit.aspx?PageIndex=1&amp;PageSize=36&amp;name=">36</option>
                                        <option value="TeacherAudit.aspx?PageIndex=1&amp;PageSize=50&amp;name=">50</option>
                                        <option value="TeacherAudit.aspx?PageIndex=1&amp;PageSize=75&amp;name=">75</option>
                                        <option value="TeacherAudit.aspx?PageIndex=1&amp;PageSize=100&amp;name=">100</option>
                                        <option value="TeacherAudit.aspx?PageIndex=1&amp;PageSize=500&amp;name=">500</option>
                                    </select>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    <div class="clearDiv">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
</asp:Content>
