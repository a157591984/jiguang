<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoCompleteDemo.aspx.cs" Inherits="Homework.AutoCompleteDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../styles/styles003/layOut.css" rel="stylesheet" type="text/css" />
    <link href="../styles/styles003/style01.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../SysLib/plugin/auto-complete/js/AutoComplete.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%
    // Response.Write(PHHC.PrescriptionReviewManage.Web.Common.pfunction.GetAgeByBirthday("2012-7-30", "2012-9-2"));
            %>
            <table cellpadding="0" cellspacing="0" class="table_content">
                <%--<tr>
                <td class="td_content_001">
                    选择给药途径
                </td>
                 <td class="td_content_002">
                   
                    <input type="text" id="txtTest2" clientidmode="Static" class="txt"
                        pAutoComplete="True"
                        pAutoCompleteAjaxKey="V_DrugClassLevel2";
                         pAutoCompleteTextColumn="{D_Name}"
                     />
                      <input type="hidden" id="hidtxtTest2" clientidmode="Static" class="txt" />
                </td>
                </tr>--%>
                <tr>
                    <td class="td_content_001">选择
                    </td>
                    <td class="td_content_002">
                        <!-- 控件说明：
                   1.pAutoComplete:使用智能匹配的开关 必填
                   2.pAutoCompleteAjax：使用的AJAX的的CODE 可选 默认：AjaxAutoCompletePaged
                   3.pAutoCompleteAjaxKey：使用的下拉内容的KEY 必填（在表“DictionarySQlMaintenance.DictionarySQlMaintenance_Mark”中有维护）
                   4.AutoCompleteVectors： 智能匹配的载体 可选 默认为 AutoCompleteVectors 
                   5.AutoCompleteIsJP：是否支持简拼1 支持，0 不支持；可选； 默认为0
                   6.AutoCompletePageSize: 可选；默认为10   DictionarySQlMaintenance 
                   7.pAutoCompleteConditionIn: 可选， 以IN的方式过滤条件。  格式："字段名|值,值,值" 
                   8.pAutoCompleteConditionNotIn: 可选， 以NOTIN的方式过滤条件。  格式："字段名|值,值,值" 
                   9.onAutoCompleteSelectedItem:可选, 当选中下拉提示信息时，执行的事件（有三个参数，文本框对象、选中项文本，选中项值）。 默认赋值给文本框和隐藏域
                   10.pAutoCompleteTextColumn: 显示字段，可多个字段，以{}包括。默认（D_Name）
                   11.pAutoCompleteValueColumn：隐藏值，可多个字段，以{}包括。默认（Common_Dict_ID）
                   12.pAutoCompleteWhereColumn:查询字段，可多个字段，以,分割。  默认（D_Name）
                   13.pAutoCompleteIsPY: 拼音开关，1和0. 1，开启，0关闭。默认开启。 注：拼音字段开启后，与查询字段相结合，参与查询。
                   14.pAutoCompleteOrder:排序字段，可多个字段，以,分割。  默认（D_Order,D_Name）
                     -->
                        <input type="text" id="txtTest" clientidmode="Static" class="txt" runat="server"
                            pautocomplete="True"
                            pautocompleteajax="AjaxAutoCompletePaged"
                            pautocompleteajaxkey="NJXQ"
                            pautocompletevectors="AutoCompleteVectors"
                            pautocompleteisjp="1"
                            pautocompletepagesize="10"
                            onautocompleteselecteditem="selectedItem" />

                    </td>
                </tr>
                <tr>
                    <td class="td_content_001">选择老师
                    </td>
                    <td class="td_content_002">
                        <!-- 控件说明：
                   1.pAutoComplete:使用智能匹配的开关 必填
                   2.pAutoCompleteAjax：使用的AJAX的的CODE 可选 默认：AjaxAutoCompletePaged
                   3.pAutoCompleteAjaxKey：使用的下拉内容的KEY 必填（在表“DictionarySQlMaintenance.DictionarySQlMaintenance_Mark”中有维护）
                   4.AutoCompleteVectors： 智能匹配的载体 可选 默认为 AutoCompleteVectors 
                   5.AutoCompleteIsJP：是否支持简拼1 支持，0 不支持；可选； 默认为0
                   6.AutoCompletePageSize: 可选；默认为10
                   7.pAutoCompleteConditionIn: 可选， 以IN的方式过滤条件。  格式："字段名|值,值,值" 
                   8.pAutoCompleteConditionNotIn: 可选， 以NOTIN的方式过滤条件。  格式："字段名|值,值,值" 
                   9.onAutoCompleteSelectedItem:可选, 当选中下拉提示信息时，执行的事件（有三个参数，文本框对象、选中项文本，选中项值）。 默认赋值给文本框和隐藏域
                   10.pAutoCompleteTextColumn: 显示字段，可多个字段，以{}包括。默认（D_Name）
                   11.pAutoCompleteValueColumn：隐藏值，可多个字段，以{}包括。默认（Common_Dict_ID）
                   12.pAutoCompleteWhereColumn:查询字段，可多个字段，以,分割。  默认（D_Name）
                   13.pAutoCompleteIsPY: 拼音开关，1和0. 1，开启，0关闭。默认开启。 注：拼音字段开启后，与查询字段相结合，参与查询。
                   14.pAutoCompleteOrder:排序字段，可多个字段，以,分割。  默认（D_Order,D_Name）
                     -->
                        <input type="hidden" id="hidTeacher" clientidmode="Static" class="txt" />
                        <input type="text" id="txtTeacher" clientidmode="Static" class="txt" runat="server"
                            pautocomplete="True"
                            pautocompleteajax="AjaxAutoCompletePaged"
                            pautocompleteajaxkey="LAOSHI"
                            pautocompleteconditionin="<%=strpAutoCompleteConditionIn %>"
                            pautocompletevectors="AutoCompleteVectors"
                            pautocompleteisjp="1"
                            pautocompletepagesize="20" />

                    </td>
                </tr>
                <tr>
                    <td class="td_content_001">选择(QUAN)
                    </td>
                    <td class="td_content_002">
                        <!-- 控件说明：
                    1.pAutoComplete:使用智能匹配的开关 必填
                    2.pAutoCompleteAjax：使用的AJAX的的CODE 可选 默认：AjaxAutoCompletePaged
                    3.pAutoCompleteAjaxKey：使用的下拉内容的KEY 必填（在表“DictionarySQlMaintenance.DictionarySQlMaintenance_Mark”中有维护）
                    4.AutoCompleteVectors： 智能匹配的载体 可选 默认为 AutoCompleteVectors 
                    5.AutoCompleteIsJP：是否支持简拼1 支持，0 不支持；可选； 默认为0
                    6.AutoCompletePageSize: 可选；默认为10
                    7.pAutoCompleteConditionIn: 可选， 以IN的方式过滤条件。  格式："字段名|值,值,值" 
                    8.pAutoCompleteConditionNotIn: 可选， 以NOTIN的方式过滤条件。  格式："字段名|值,值,值" 
                    9.onAutoCompleteSelectedItem:可选, 当选中下拉提示信息时，执行的事件（有三个参数，文本框对象、选中项文本，选中项值）。 默认赋值给文本框和隐藏域
                    10.pAutoCompleteTextColumn: 显示字段，可多个字段，以{}包括。默认（D_Name）
                    11.pAutoCompleteValueColumn：隐藏值，可多个字段，以{}包括。默认（Common_Dict_ID）
                    12.pAutoCompleteWhereColumn:查询字段，可多个字段，以,分割。  默认（D_Name）
                    13.pAutoCompleteIsPY: 拼音开关，1和0. 1，开启，0关闭。默认开启。 注：拼音字段开启后，与查询字段相结合，参与查询。
                    14.pAutoCompleteOrder:排序字段，可多个字段，以,分割。  默认（D_Order,D_Name）
                    15.onAutoCompleteDataBindInit：数据加载事件。在将查询出来的数据加载到提示层时执行。 可对加载数据进行最后一次处理。
                                                格式：fun(str)  str:加载的数据.
                    16.pAutoCompleteLike: LIKE查询方式标示。  0（默认）,左右like； 1,左like； 2,右like； 3,无like.
                    -->
                        <input type="text" id="Text1" clientidmode="Static" class="txt"
                            pautocomplete="True"
                            pautocompleteajax="AjaxAutoCompletePaged"
                            pautocompleteajaxkey="KHDrug"
                            pautocompletevectors="AutoCompleteVectors"
                            pautocompleteisjp="1"
                            pautocompletepagesize="15"
                            pautocompletetextcolumn="{D_Name}({D_Type}) / {D_Remark}"
                            pautocompletevaluecolumn="{D_Code}"
                            pautocompletewherecolumn="D_Name,D_Type"
                            pautocompleteispy="0"
                            pautocompleteorder="D_Order,D_Name"
                            onautocompletedatabindinit="dataBindInit"
                            pautocompletelike="2" />
                        <input type="hidden" id="Hidden1" clientidmode="Static" class="txt" />
                    </td>
                </tr>
            </table>
            <div id="div_test"></div>
        </div>
        <!--智能匹配载体-->
        <div id="AutoCompleteVectors" class="AutoCompleteVectors">
            <div id="topAutoComplete" class="topAutoComplete">
                简拼/汉字或↑↓
            </div>
            <div id="divAutoComplete" class="divAutoComplete">
                <ul id="AutoCompleteDataList" class="AutoCompleteDataList">
                </ul>
            </div>

        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function selectedItem(con, text, value) {
        //alert()

        $("#div_test").append(text)
        //alert($(con).attr("id"));
        //alert(text);
        //alert(value);
    }
    function dataBindInit(str) {
        str = str.replace(" /  / ", " / ");
        if (str.charAt(str.length - 2) == "/") {
            str = str.substring(0, str.length - 3);
        }
        return str;
    }
    function dataBindInit(str) {
        str = str.replace(" /  / ", " / ");
        if (str.charAt(str.length - 2) == "/") {
            str = str.substring(0, str.length - 3);
        }
        return str;
    }
</script>
