 <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="XUV_CommonDict.ascx.cs"
    Inherits="Rc.Cloud.Web.UserControl.XUV_CommonDict" %>
<script language="javascript" type="text/javascript">
    var strHospital = "";
    var strD_Type = "";
    var strUserControlID = "";
    var strHidUserControlID = "";
    //0正常模式
    //1以TABLE方式返回 选择给医生分配数据时用
    var strReturnType = "0";
    //选择模式 0 多选，1 单选
    var strCheckMode = "0";
    //选择模式 0无编辑，1只添加，2只修改，3添加与修改
    var strEditMode = "0";
    //多选最大个数限制
    var strMaxSelected = "0";
    //是否支持拼音检索
    var strIsPY = "0";
    var strCondition = "";
    var strEditOnly = "display:none;";
</script>
<div id="div_init" runat="server">
</div>
<div class="clearDiv">
</div>
<div class="div_right_search_pop">
    <table class="table_search_001">
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;名称：
            </td>
            <td>
                <asp:TextBox ID="txtSearchNameS" runat="server" ClientIDMode="Static" CssClass="txt" Style="width: 150px;"></asp:TextBox>
            </td>
            <td>
                <input id="btnSearch" type="button" value="查 询" class="btn" onclick="SearchData<%=strPageName %>()" />
            </td>
            <td id="tdAdd<%=strPageName %>">
                &nbsp;&nbsp;<input type="button" class="btn"  value="新增" onclick="showPop<%=strPageName %>('','','')" />
            </td>
        </tr>
    </table>
</div>
<div class="clearDiv">
</div>
<table style="width: 100%; border: 0px; margin-top: 0px;">
    <tr>
        <td style="width: 40%;" valign="top">
            <div class="div_right_listtitle">
                数据列表
            </div>
            <div class="clearDiv">
            </div>
            <div id="div_Data_left<%=strPageName %>" style="height: 330px; overflow-y: auto; overflow-x: hidden;">
            </div>
            <%-- style="width:100%; height:400px; overflow-y:scroll;" --%>
        </td>
        <td valign="top">
            <div class="div_right_listtitle">
                已选择数据
            </div>
            <div class="clearDiv">
            </div>
            <div id="div_Data_right<%=strPageName %>" style="height: 330px; overflow-y: auto; overflow-x: hidden;">
            </div>
        </td>
    </tr>
</table>
<div class="clearDiv">
</div>
<div class="div_page_bottom_operation">
    <input type="button" class="btn" value="确 定" onclick="ConfrimSelected<%=strPageName %>()" />
    <input type="button" class="btn" value="清除已选" onclick="ClearAllSelected<%=strPageName %>()" />
</div>
<!-- 弹出层操作  	控件 -->
<div class="div_ShowDailg" id="div_Pop" runat="server" style="width: 500px; height: 300px;">
    <div class="div_ShowDailg_Title" id="div_Pop_Title" runat="server">
        <div class="div_ShowDailg_Title_left" id="div_ShowDailg_Title_left<%=strPageName %>">
            新增数据</div>
        <div class="div_Close_Dailg" id="div_Close_Dailg" title="关闭" onclick="ClosePopById<%=strPageName %>();">
        </div>
        <!--关闭-->
    </div>
    <div class="clearDiv" id="div_iframe" runat="server">
    </div>
</div>
<script language="javascript" type="text/javascript">
function ClosePopById<%=strPageName %>()
     {
         CloseDialogById('<%=div_Pop.ClientID %>');
     }
    //点击弹出按扭
    function GetDataList<%=strPageName %>(userControlID, hidUserControlID, D_Type,D_Expand) {
        var Hospital = arguments[4] ? arguments[4] : "";
        strUserControlID = userControlID;
        strHidUserControlID = hidUserControlID;
        strD_Type = D_Type;
      
        
        if (D_Expand!=undefined) {
            if (D_Expand.split("|")[0]!=undefined) {
                strCheckMode = D_Expand.split("|")[0];//扩展属性的第一个参数：选择模式。
            }
            if (D_Expand.split("|")[1]!=undefined) {
               strReturnType = D_Expand.split("|")[1];//扩展属性的第二个参数：返回值类型。
            }
            if (D_Expand.split("|")[2]!=undefined) {
               strEditMode = D_Expand.split("|")[2];//扩展属性的第三个参数：返回值编辑类型。0无编辑，1只添加，2只修改，3添加与修改
            }
            if (D_Expand.split("|")[3]!=undefined) {
               strMaxSelected = D_Expand.split("|")[3];//扩展属性的第四个参数：多选最大限制个数
            }
            if (D_Expand.split("|")[4]!=undefined) {
               strIsPY = D_Expand.split("|")[4];//是否支持简拼查询
            }
            if (D_Expand.split("|")[5]!=undefined) {//为存储查询条件控件的名称
               if ($("#"+D_Expand.split("|")[5])!=undefined) {
                     strCondition = $("#"+D_Expand.split("|")[5]).val() ;//按指定条件查询（条件需要按“字段名1,字段名2|值1,值2”格式定义）
                }
               
            }
        }
        if (strEditMode == "2" || strEditMode=="3" ) {
            strEditOnly = "";
        }
        else {
            strEditOnly="display:none;";
        }
        if (strEditMode=="1" || strEditMode=="3" ) {
            $("#tdAdd<%=strPageName %>").show();
        }
        else{
            $("#tdAdd<%=strPageName %>").hide();
        }

        PopInit<%=strPageName %>();
        GetDataListAll<%=strPageName %>(1, 10)
        GetRightData<%=strPageName %>();
    }
    //初始化已选中的值
    function GetRightData<%=strPageName %>() {
        var Ids = "";
        var IdsTemp = $("#" + strHidUserControlID).val().split(',');

        for (var i = 0; i < IdsTemp.length; i++) {
            Ids += IdsTemp[i].split("|")[0] + ","
        }
        if (Ids!="") {
            Ids = Ids.substr(0, Ids.length - 1);
        }
        
        $.get("../Ajax/UserControlAjax.aspx", { key: "CommonCtrlMultiple"
         , Ids: Ids
         , D_Type : strD_Type
         , net4: Math.random()
        },
         function (data) {

             if (data == "0") {
                 strHtml = "加载失败";
             }
             else if (data == "2") {
                 strHtml = "<table class='table_pop_list' cellpadding='0' cellspacing='0' id='table_Data_right_" + strUserControlID + "'>";
                 strHtml += GetRightTitle<%=strPageName %>();
                 strHtml += "</table>";
                 //strHtml += "<div class='div_nodata_pop'>您还没有选择数据</div>";
             }
             else {

                 objJson = eval("(" + data + ")");

                 strHtml = "<table class='table_pop_list' cellpadding='0' cellspacing='0' id='table_Data_right_" + strUserControlID + "'>";
                 strHtml += GetRightTitle<%=strPageName %>();
                 for (var i = 0; i < objJson.length; i++) {
                     strCss = "";
                     if (i % 2 == 0) { strCss = "tr_pop_002"; } else { strCss = "tr_pop_002" }
                     strHtml += "<tr class='" + strCss + "' did='" + objJson[i].Common_Dict_ID + "' dname='" + objJson[i].D_Name + "' id='trRight_" + strUserControlID + "_" + objJson[i].Common_Dict_ID + "' >";
                     strHtml += "<td><input type=\"button\" class=\"btn_delete\" onclick=\"ClearSelected<%=strPageName %>('" + objJson[i].Common_Dict_ID + "')\" ></td>";
                     strHtml += "<td  style='text-align:left;'>" + objJson[i].D_Name + "</td>";
                     strHtml += "</tr>";
                 }
                 strHtml += "</table>";
             }
             $("#div_Data_right<%=strPageName %>").html(strHtml);
           CheckedLeftCheckbox<%=strPageName %>();
         }
        );
    }
    //点击弹出数据按扭时需要初始化的数据
    function PopInit<%=strPageName %>(userControlID, hidUserControlID) {
        strHtml = "";
        if ($("#hidRightData_" + strUserControlID).val() == undefined) {
            strHtml += "<input type='hidden' id='hidLeftData_" + strUserControlID + "'>";
            $("#<%=div_init.ClientID %>").html(strHtml);
        }

    }
    //根据已选数据选中左边复选框
    function CheckedLeftCheckbox<%=strPageName %>() {
        $("#table_Data_right_" + strUserControlID + " tr").each(function () {
            if (this.id != "") {
                $("#chb_" + strUserControlID + "_" + $(this).attr("did")).attr("checked", true);
            }
        });
    }
    //点击确定按扭
    function ConfrimSelected<%=strPageName %>() {
        var ids = "";
        var names = "";
        if (strReturnType == "1") {//以TABLE方式返回值
            var tabHtml="";
            $("#table_Data_right_" + strUserControlID + " tr").each(function () {
                    if (this.id != "") {
                        ids += $(this).attr("did")  + ",";
                        tabHtml += "<tr did='"+$(this).attr("did")+"'>";
                        tabHtml += "<td>";
                        tabHtml +=  $(this).attr("dname");
                        tabHtml += "</td>";
                        tabHtml += "<td>";
                        var id_1=$(this).attr("did");
                        var val="";
                         $("#" + strUserControlID + " tr").each(function () {
                            var id_2 = $(this).attr("did");
                    
                            if (id_1 == id_2) {
                                val = $(this).find("input").val();
                               
                            }   
                        });

                        tabHtml +=  "<input type='text' class='txt' style=' width:30px;'  value='" + val + "'  onkeyup=\"this.value=this.value.replace(/\D/gi,'');\"  id='txt_" + $(this).attr("did") + "' >";
                        tabHtml += "</td>";
                        tabHtml += "</tr>";
                    }
                });
                $("#" + strUserControlID).html(tabHtml);
        }
        else {
             $("#table_Data_right_" + strUserControlID + " tr").each(function () {
                    if (this.id != "") {
                        ids += $(this).attr("did")  + ",";
                        names += $(this).attr("dname");
                        names += ",";
                    }
                });
                if (names != "") {
                    names = names.substr(0, names.length - 1);
                }
                $("#" + strUserControlID).val(names);
        }
       
       
        if (ids != "") {
            ids = ids.substr(0, ids.length - 1);
        }
       

        $("#" + strHidUserControlID).val(ids);

        //$("#hidRightData_" + strUserControlID + "").val($("#table_Data_right_" + strUserControlID + "").html());
        if ($("#div_Pop_CommonDict")!=undefined) {
        //alert($("#div_Pop_CommonDict"));
            CloseDialogById('div_Pop_CommonDict');
        }
        else {
             CloseDialog();
        }
       
        //alert(ids);alert(names);
    }


    //点击查询按扭
    function SearchData<%=strPageName %>() {
        GetDataListAll<%=strPageName %>(1, 10)
    }
    //页面索引发生变化
    function UcPageIndexChange<%=strPageName %>(PageIndex, PageSize) {
        GetDataListAll<%=strPageName %>(PageIndex, PageSize)
    }

    var PageI=0,PageS=0;
    //获取分页数据
    function GetDataListAll<%=strPageName %>(PageIndex, PageSize) {
       // strUserControlID = userControlID;
        //strHidUserControlID = hidUserControlID;
       PageI=PageIndex;
       PageS=PageSize;
        var D_Name = $("#<%=txtSearchNameS.ClientID %>").val();
        
        var strHtml = "";
        $.get("../Ajax/UserControlAjax.aspx", { key: "CommonCtrlMultiplePaged"
         , userControlID: strUserControlID
         , D_Name: D_Name
         , PageIndex: PageIndex
         , PageSize: PageSize
         , Py:strIsPY
         , strPageName:'<%=strPageName %>'
         , D_Type : strD_Type
         , Condition : strCondition
         , net4: Math.random()
        },
         function (data) {

             if (data == "0") {
                 strHtml = "加载失败";
             }
             else if (data == "2") {
                 strHtml = "<table class='table_pop_list' cellpadding='0' cellspacing='0'>";
                 strHtml += GetLeftTitle<%=strPageName %>();
                 strHtml += "</table>";
                 strHtml += "<div class='div_nodata_pop'>暂无数据</div>";
             }
             else {
                 $("#hidLeftData_" + strUserControlID + "").val(data.split("~")[0])
                 objJson = eval("(" + data.split("~")[0] + ")");

                 strHtml = "<table class='table_pop_list' cellpadding='0' cellspacing='0'>";
                 strHtml += GetLeftTitle<%=strPageName %>();

                 for (var i = 0; i < objJson.length; i++) {
                     strCss = "";
                     if (i % 2 == 0) { strCss = "tr_pop_002"; } else { strCss = "tr_pop_002" }
                     strHtml += "<tr class='" + strCss + "'>";
                     strHtml += "<td style='text-align:center;'><input type='checkbox' name='chb_" + strUserControlID + "' dn='" + objJson[i].Common_Dict_ID + "' id='chb_" + strUserControlID + "_" + objJson[i].Common_Dict_ID + "' ";
                     strHtml += " onclick=ClickRightCheckbox<%=strPageName %>('" + objJson[i].Common_Dict_ID + "')";
                     strHtml += " /></td>";
                     strHtml += "<td style='text-align:left;'>" + objJson[i].D_Name + "</td>";
                     strHtml += "<td style=\"text-align:center;" + strEditOnly + " \" ><input type=\"button\" class=\"btn_modify\" onclick=\"showPop<%=strPageName %>('" + objJson[i].Common_Dict_ID + "','"+objJson[i].D_Name+"','"+objJson[i].D_Remark+"')\" ></td>";
                     strHtml += "</tr>";
                 }
                 strHtml += "</table>";
                 strHtml += data.split("~")[1];

             }
             $("#div_Data_left<%=strPageName %>").html(strHtml);
         }
        );
        CheckedLeftCheckbox<%=strPageName %>();
    }
    //得到左边表头
    function GetLeftTitle<%=strPageName %>() {
        strStr = "<tr class='tr_title'>";
        strStr += "<td style='width:35px'>选择</td>";
        strStr += "<td>数据</td>";
        strStr += "<td style=\"width:35px;text-align:center;" + strEditOnly + " \">修改</td>";
        strStr += "</tr>";
        return strStr;
    }
    //得到右边表头
    function GetRightTitle<%=strPageName %>() {
        
        strStr = "<tr class='tr_title'>";
        strStr += "<td style='width:35px'>移除</td>";
        strStr += "<td>数据</td>";
        strStr += "</tr>";
        return strStr;
    }
    //点击左边复选框
    function ClickRightCheckbox<%=strPageName %>(id) {


        if (strCheckMode == "1") {
            SelectOne<%=strPageName %>(id);
        }

        objLeftJson = eval("(" + GethidLeftData<%=strPageName %>() + ")");
        for (var i = 0; i < objLeftJson.length; i++) {
            if (objLeftJson[i].Common_Dict_ID == id) {
                if ($("#chb_" + strUserControlID + "_" + id).attr("checked")) {
                    AddRight<%=strPageName %>(objLeftJson[i]);
                }
                else {
                    DeleteRight<%=strPageName %>("" + objLeftJson[i].Common_Dict_ID + "");
                }
            }
        }
        //多选最大限制
        if (strMaxSelected > 0) {
            SelectedMaxLimit<%=strPageName %>(id);
        }
    }
    //
    function SelectedMaxLimit<%=strPageName %>(id)
    {
      var SelectedCount = 0;
      $("#table_Data_right_" + strUserControlID + " tr").each(function () {
            if (this.id != "") {
               SelectedCount++;
            }
        });
        if (SelectedCount > strMaxSelected) {
            alert("对不起！最多只能选择【" + strMaxSelected + "】条数据。");
             ClearSelected<%=strPageName %>(id);
        }
    }

    //向右边添加数据（单条）
    function AddRight<%=strPageName %>(obj) {
        var selDrugRouteID = "selDrugRoute_" + obj.Common_Dict_ID + "_" + strUserControlID;
        var selDoseLevel = "selDoseLevel_" + obj.Common_Dict_ID + "_" + strUserControlID;
        rightHtml = "";
        rightHtml += "<tr class='tr_pop_002' did='" + obj.Common_Dict_ID + "' dname='" + obj.D_Name + "' id='trRight_" + strUserControlID + "_" + obj.Common_Dict_ID + "' >";
        rightHtml += "<td><input type=\"button\" class=\"btn_delete\" onclick=\"ClearSelected<%=strPageName %>('" + obj.Common_Dict_ID + "')\" ></td>";
        rightHtml += "<td  style='text-align:left;'>" + obj.D_Name + "</td>";
        rightHtml += "</tr>";

        $("#table_Data_right_" + strUserControlID + "").append(rightHtml);
    }
    //移除右边所有
    function ClearAllSelected<%=strPageName %>() {
        $("#table_Data_right_" + strUserControlID + " tr").each(function () {
            if (this.id != "") {
                $("#chb_" + strUserControlID + "_" + $(this).attr("did")).attr("checked", false);
                DeleteRight<%=strPageName %>($(this).attr("did"));
            }
        });

    }
    //点击右边的移除按扭
    function ClearSelected<%=strPageName %>(id) {
        DeleteRight<%=strPageName %>(id);
        $("#chb_" + strUserControlID + "_" + id).attr("checked", false);
    }
    //移除右边
    function DeleteRight<%=strPageName %>(id) {
        $("#trRight_" + strUserControlID + "_" + id + "").remove();
    }

    //得到左边数据数据（所有）
    function GethidLeftData<%=strPageName %>() {
        return $("#hidLeftData_" + strUserControlID + "").val();
    }
    //只能选择一个
    function SelectOne<%=strPageName %>(id) {
        $("input[@type='checkbox'][name='" + "chb_" + strUserControlID + "']").each(function () {
            if (this.id != "chb_" + strUserControlID + "_" + id) {
                $("#" + this.id).attr("checked", false);
                ClearAllSelected<%=strPageName %>();
            }
        });
    }
    function showPop<%=strPageName %>(id,name,remark) {

            if (id=="") {
                $("#div_ShowDailg_Title_left<%=strPageName %>").html("添加数据");
            }
            else
            {
                $("#div_ShowDailg_Title_left<%=strPageName %>").html("修改数据");
            }
            $("#<%=div_Pop.ClientID %>").css("height","200px");
            $("#<%=div_Pop.ClientID %>").show();
            
            //$("#<%=div_iframe.ClientID %>").html("<iframe  height=\"260\"  frameborder='0' width='100%' style='margin: 0px' src='../DICT/Dict_Edit.aspx?VTYPE="+strD_Type+"&ID="+id+"'></iframe>");
            //SetDialogPositionFixed("<%=div_Pop.ClientID %>",100);
            $("#<%=div_iframe.ClientID %>").html("<iframe  height=\"160\"  frameborder='0' width='100%' style='margin: 0px' src='../DICT/Common_Edit.aspx?VTYPE="+strD_Type+"&ID="+id+"&pageName=<%=strPageName %>'></iframe>");
            SetDialogPositionFixed("<%=div_Pop.ClientID %>",100);
    }
       function Handel<%=strPageName %>(sign,strMessage) {
        if (sign == "1") {
            showTips('操作成功', '', '1');
            SearchData<%=strPageName %>();
            ClosePopById<%=strPageName %>();
        }
        else {
            showTipsErr('操作失败. ' + strMessage, '4')
        }
    }
</script>
