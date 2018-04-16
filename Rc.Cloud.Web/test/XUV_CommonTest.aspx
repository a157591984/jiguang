<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XUV_CommonTest.aspx.cs" Inherits="MSWeb.Test.XUV_CommonTest" %>

<%@ Register Src="../UserControl/XUV_CommonDict.ascx" TagName="XUV_CommonDict" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../styles/styles003/layOut.css" rel="stylesheet" type="text/css" />
    <link href="../styles/styles003/style01.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../scripts/jquery-1.4.1.min.js"></script>
    <link href="../styles/Dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../scripts/jQuery_Dialog.js"></script>
    <script type="text/javascript" src="../scripts/jquery.easydrag.js"></script>
    <script type="text/javascript" src="../scripts/function.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%
    // Response.Write(PHHC.PrescriptionReviewManage.Web.Common.pfunction.GetAgeByBirthday("2012-7-30", "2012-9-2"));
            %>
            <table cellpadding="0" cellspacing="0" class="table_content">

                <tr>
                    <td class="td_content_001">年级学期</td>
                    <td class="td_content_002">
                        <input type="text" id="txtV_Common_Dict_Hospital" readonly="readonly"
                            clientidmode="Static" class="txt" style="width: 80%;" runat="server" />
                        <input type="hidden" id="hidV_Common_Dict_Hospital" clientidmode="Static" runat="server" />
                        <input type="button" title="选择医院" onclick="showCommonDict('txtV_Common_Dict_Hospital', 'hidV_Common_Dict_Hospital', 'NJXQ', '0|0|3');"
                            class="btn_info2" />
                    </td>
                </tr>
            </table>
        </div>


        <!-- 载体 -->
        <div class="div_ShowDailg" id="div_Pop_CommonDict" style="width: 800px; height: 570px;">
            <div class="div_ShowDailg_Title" id="div_Pop_CommonDict_Title">
                <div class="div_ShowDailg_Title_left" id="div_Pop_CommonDict_Title_Teft"></div>
                <div class="div_Close_Dailg" id="div5" title="关闭" onclick="CloseDialog();"></div>
                <!--关闭-->
            </div>
            <uc1:XUV_CommonDict ID="XUV_CommonDict1" runat="server" />
        </div>
        <div class="div_documentbg" id="div_documentbg"></div>

    </form>
</body>
</html>
<script language="javascript" type="text/javascript">

    $("#div_Pop_CommonDict").easydrag();
    $("#div_Pop_CommonDict").setHandler("div_Pop_CommonDict_Title");
    //
    function showCommonDict(userControlID, hidUserControlID, D_Type, D_Expand) {
        //var fun = arguments[4] ? arguments[4] : null;
        //        ShowDocumentDivBG_Opacity0();
        SetDialogPositionTop("div_Pop_CommonDict", 30);
        $("#div_Pop_CommonDict").show();
        if (D_Type == "NJXQ") {
            $("#div_Pop_CommonDict_Title_Teft").html("年级学期");
        }
        if (D_Type == "V002") {
            $("#div_Pop_CommonDict_Title_Teft").html("选择医院");
        }
        else if (D_Type == "V003") {
            $("#div_Pop_CommonDict_Title_Teft").html("选择药物分类");
        }
        else if (D_Type == "V010") {
            $("#div_Pop_CommonDict_Title_Teft").html("选择剂型（最多只能选择3条数据)");
        }

        else if (D_Type == "VTEST") {
            $("#div_Pop_CommonDict_Title_Teft").html("选择登录人所在医院的药品(多选)");
        }
        else if (D_Type == "DiseaseStandard_DictName") {
            $("#div_Pop_CommonDict_Title_Teft").html("选择病症");
        }
        //方法名称是：GetDataList_+控件的名称
        //D_Type:控件类型 DictionarySQlMaintenance.DictionarySQlMaintenance_Mark 值
        //D_Expand：扩展属性（以“|”号分隔的多个属性）
        //【0】选择模式         0多选，1单选 默认：多选
        //【1】返回值模式       0按正常模式返回值，1以TABLE方式返回  默认：按正常模式返回值
        //【2】编辑模式         0无编辑，1只添加，2只修改，3添加与修改 默认：无编辑
        //【3】多选最大限制值   0与默认均为：无限制
        //【4】权限   0与默认均为：无限制
        //【5】存储查询条件控件的名称 查询条件的组装为（“字段名1,字段名2|值1,值2”）
        //【6】回调函数
        GetDataList_XUV_CommonDict(userControlID, hidUserControlID, D_Type, D_Expand);
    }

    //点击确定后的，回调函数
    //可在此处直接用AJAX，通过ids names，访问数据库
    function Common_Fun(ids, names) {
        //alert("ids:" + ids + "\r\nnames:" + names);
        $.post("Test_Ajax.aspx",
                { IDs: ids, NAMEs: names },
                function (data, status) {
                    alert(data);
                    alert(status);
                }
        );
    }



</script>
