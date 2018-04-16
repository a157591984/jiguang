using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Rc.Common.StrUtility;
using Rc.Cloud.BLL;


namespace Rc.HospitalConfigManage.Web.Ajax
{
    public partial class SysAjax : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = Request.QueryString["key"];
            StringBuilder strStr = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            DataTable dt = new DataTable();
            string strJson = string.Empty;
            if (string.IsNullOrEmpty(key))
            {
                key = Request["key"];
            }
            switch (key)
            {
                case "DeleteSysModuleByID":
                    #region 根据ID删除模块关联信息
                    string module_id = string.Empty;
                    string syscode = string.Empty;
                    try
                    {
                        module_id = Request["id"].ToString();

                        if (new BLL_SysModule().DeleteSysModuleBySyscodeAndModuleID(module_id))
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append("【删除模块关联信息】模块关联信息ID：" + module_id);
                            new BLL_clsAuth().AddLogFromBS("Ajax/SysAJax.aspx", str.ToString());
                            Response.Write("1");
                        }
                        else
                        {
                            Response.Write("0");
                        }
                    }
                    catch (Exception)
                    {
                        Response.Write("0");
                    }

                    Response.End();
                    #endregion
                    break;

                #region 得到系统数据的JSON串
                case "GetSysCodeToJson":
                    //dt = new BLL_SysProduct().GetList(" SysCode='00002'").Tables[0];
                    // dt = new BLL_SysCode().GetSysCodeListByHospitalID("");
                    Response.Write("{\"ds\":[{\"Value\":\"00001\",\"Text\":\"实施系统\"}]}");
                    break;
                #endregion
                #region 得到一级菜单的JSON串
                case "GetModuleFristToJson":
                    //dt = new BLL_SysModule().GetModuleFirstBySysCode(Request["f"].ToString()).Tables[0];
                    dt = new BLL_SysModule().GetSysModuleForFirstBySysCode(Request["f"].ToString()).Tables[0];
                    Response.Write(Rc.Cloud.Web.Common.pfunction.DtToJson(dt, "MODULEID", "MODULENAME"));
                    break;
                #region 删除系统信息
                case "SysCodeTempDelete":
                    try
                    {
                        string sysCodeT = clsUtility.Decrypt(Request.QueryString["Aid"]);
                        //if (new BLL_SysCode().DeleteSysCodeBySysCode(sysCodeT) == true)
                        //{
                        //    //MS.Authority.clsAuth.AddLogFromBS("", String.Format("删除成功 【系统编码】：{0}", sysCodeT));
                        //    strStr.Append(1);
                        //}
                        //else
                        //{
                        //    //MS.Authority.clsAuth.AddLogFromBS("", String.Format("删除失败 【系统编码】：{0}", sysCodeT));
                        //    strStr.Append(0);
                        //}
                    }
                    catch
                    {
                        //strJson = ex.Message.ToString();
                        strJson = "0";
                    }
                    Response.Write(strStr.ToString());
                    Response.End();
                    break;
                #endregion
                #endregion

                case "SysUserTempDelete":
                    #region 根据ID删除模块关联信息
                    string SysUser_ID = string.Empty;
                    try
                    {
                        SysUserBLL BLL = new SysUserBLL();
                        SysUser_ID = clsUtility.Decrypt(Request["Aid"].ToString());
                        if (BLL.DeleteByPK(SysUser_ID) > 0 && BLL.DeleteSysUserRoleRelationByModuleID(SysUser_ID) == true)
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append("【删除用户关联表SysUserTempDelete】用户ID：" + SysUser_ID);
                            new BLL_clsAuth().AddLogFromBS("Ajax/SysAJax.aspx", str.ToString());
                            Response.Write("1");
                        }
                        else
                        {
                            Response.Write("0");
                        }
                    }
                    catch (Exception)
                    {
                        Response.Write("0");
                    }

                    Response.End();
                    #endregion
                    break;
                case "SysCommon_DictTempDelete":
                    #region 根据ID删除模块关联信息
                    string common_Dict_ID = string.Empty;
                    try
                    {
                        Common_DictBLL BLL = new Common_DictBLL();
                        common_Dict_ID = clsUtility.Decrypt(Request["Aid"].ToString());
                        if (BLL.DeleteByPK(common_Dict_ID) > 0)
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append("【删除字典】字典ID：" + common_Dict_ID);
                            new BLL_clsAuth().AddLogFromBS("Ajax/SysAJax.aspx", str.ToString());
                            Response.Write("1");
                        }
                        else
                        {
                            Response.Write("0");
                        }
                    }
                    catch (Exception)
                    {
                        Response.Write("0");
                    }

                    Response.End();
                    #endregion
                    break;
                #region 得到省的JSON串
                case "GetProvinceToJson":
                    // Response.Write(key);
                    dt = new BLL_DictForHospital().GetHospitalRegional(" D_PartentID='' ").Tables[0];
                    Response.Write(Rc.Cloud.Web.Common.pfunction.DtToJson(dt, "Regional_Dict_ID", "D_Name"));
                    break;
                #endregion
                #region 得到市的JSON串
                case "GetCityToJson":
                    dt = new BLL_DictForHospital().GetHospitalRegional(" D_PartentID='" + Request["province"].ToString() + "' ").Tables[0];
                    Response.Write(Rc.Cloud.Web.Common.pfunction.DtToJson(dt, "Regional_Dict_ID", "D_Name"));
                    break;
                #endregion
                #region 得到县的JSON串
                case "GetCountryToJson":
                    dt = new BLL_DictForHospital().GetHospitalRegional(" D_PartentID='" + Request["city"].ToString() + "' ").Tables[0];
                    //dt = new BLL_DictForHospital().GetHospitalRegional(strWhere.ToString()).Tables[0];
                    Response.Write(Rc.Cloud.Web.Common.pfunction.DtToJson(dt, "Regional_Dict_ID", "D_Name"));
                    break;

                #endregion

                case "CustomVisitAttachmentDelete":
                    #region 根据ID删除文件信息
                    string customVisitAttachmentID = string.Empty;
                    try
                    {
                        BLL_CustomVisitAttachment BLL = new BLL_CustomVisitAttachment();
                        customVisitAttachmentID = Request["Aid"].ToString();
                        if (BLL.DeleteByPK(customVisitAttachmentID) > 0)
                        {
                            Response.Write("1");
                        }
                        else
                        {
                            Response.Write("0");
                        }
                    }
                    catch (Exception)
                    {
                        Response.Write("0");
                    }

                    Response.End();
                    #endregion
                    break;

                case "DeleteFunction":
                    #region 根据ID删除文件信息
                    string functionID = string.Empty;
                    try
                    {
                        BLL_Sys_Function BLL = new BLL_Sys_Function();
                        functionID = Request["FunctionID"].ToString();
                        if (BLL.DeleteSys_FunctionByID(functionID))
                        {
                            Response.Write("1");
                        }
                        else
                        {
                            Response.Write("0");
                        }
                    }
                    catch (Exception)
                    {
                        Response.Write("0");
                    }

                    Response.End();
                    #endregion
                    break;

                case "DeleteBaseSysModuleByID":
                    #region 根据ID删除基库模块关联信息
                    string baseModuleId = string.Empty;
                    string baseSysCode = string.Empty;
                    try
                    {
                        baseModuleId = Request["id"].ToString();
                        baseSysCode = Request["syscode"].ToString();
                        if (new BLL_SysCode().DeleteBaseModule(baseModuleId, baseSysCode))
                        {
                            Response.Write("1");
                        }
                        else
                        {
                            Response.Write("0");
                        }
                    }
                    catch (Exception)
                    {
                        Response.Write("0");
                    }

                    Response.End();
                    #endregion
                    break;
                case "DeleteUpdateDataItem":
                    #region 根据ID删除更新数据项
                    try
                    {
                        if (new Common_DictBLL().DeleteByPK(Request.QueryString["id"]) > 0)
                        {
                            Response.Write("1");
                        }
                        else
                        {
                            Response.Write("0");
                        }
                    }
                    catch (Exception)
                    {
                        Response.Write("0");
                    }

                    Response.End();
                    #endregion
                    break;


                case "GetMaxType":
                    #region 根据ID删除更新数据项
                    try
                    {
                        string type = new Common_DictBLL().GetMaxDType().ToString();
                        Response.Write(type);

                    }
                    catch (Exception)
                    {
                        Response.Write("0");
                    }

                    Response.End();
                    #endregion
                    break;

                case "DeleteDSQL":
                    #region 根据ID删除更新数据项
                    try
                    {
                        if (new BLL_DictionarySQlMaintenance().DeleteByPKNew(Request["Aid"]) > 0)
                        {
                            Response.Write("1");
                        }
                        else
                        {
                            Response.Write("0");
                        }
                    }
                    catch (Exception)
                    {
                        Response.Write("0");
                    }

                    Response.End();
                    #endregion
                    break;

                case "SysCommon_Dict1TempDelete":
                    #region 根据ID删除模块关联信息
                    string common_Dict1_ID = string.Empty;
                    try
                    {
                        BLL_Common_DictNew BLL = new BLL_Common_DictNew();
                        common_Dict1_ID = clsUtility.Decrypt(Request["Aid"].ToString());
                        if (BLL.DeleteByPK(common_Dict1_ID) > 0)
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append("【删除字典】字典ID：" + common_Dict1_ID);
                            new BLL_clsAuth().AddLogFromBS("Ajax/SysAJax.aspx", str.ToString());
                            Response.Write("1");
                        }
                        else
                        {
                            Response.Write("0");
                        }
                    }
                    catch (Exception)
                    {
                        Response.Write("0");
                    }

                    Response.End();
                    #endregion
                    break;


                case "GetD_Remark":
                    #region 根据D_Type得到D_Rmark
                    string R_Type = Request.QueryString["Aid"];

                    BLL_Common_DictNew BLLmain = new BLL_Common_DictNew();

                    Response.Write(BLLmain.GetD_Remark(R_Type));
                    Response.End();
                    #endregion
                    break;
                case "DelFUser":
                    #region 根据UserId删除前台注册用户
                    string UserId = Request.QueryString["UserId"];
                    try
                    {
                        bool res = new Rc.BLL.Resources.BLL_F_User().DelFUser(UserId);
                        if (res)
                        {
                            Response.Write("1");
                        }
                        else
                        {
                            Response.Write("0");
                        }
                    }
                    catch (Exception)
                    {
                        Response.Write("0");
                    }
                    #endregion
                    break;





                default:
                    break;
            }

        }
    }
}