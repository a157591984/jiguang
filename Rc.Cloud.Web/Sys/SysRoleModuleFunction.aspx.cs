using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Rc.Cloud.BLL;

namespace Rc.HospitalConfigManage.Web.Sys
{

    public partial class SysRoleModuleFunction : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90100200";
            //Module_Id = "95105000";
            if (!IsPostBack)
            {
                try
                {
                    //DataTable dt = new DataTable();
                    //dt = new MS.HospitalConfigManage.BLL.Sys.BLL_SysModule().GetModuleFirst().Tables[0];
                    //Rc.Cloud.Web.Common.pfunction.SetDdl(ddlModel, dt, "ModuleName", "ModuleId", false);
                    lblDoctorName.Text = "正在设置角色【" + Request["dname"] + "】的权限";
                    //getDate();
                }
                catch (Exception ex)
                {
                    new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                    throw ex;
                }
            }
        }
        #region 树结构
        private void getDate()
        {
            try
            {
                DataTable dtm = new DataTable();
                DataTable dtf = new DataTable();
                string strDoctorId = string.Empty;
                string strModuleIdLike = string.Empty;
                string strWhere = string.Empty;
                if (Request["id"] != null)
                {
                    strDoctorId = Request["id"].ToString();
                }
                if (hidSysCode.Value != "" && hidFirstModule.Value != "")
                {
                    strModuleIdLike = hidFirstModule.Value.Substring(0, 2);

                    strWhere += " and ISINTREE ='Y' and sm.SysCode='" + hidSysCode.Value.Split('|')[0] + "' and (slevel LIKE '" + strModuleIdLike + "%') ";
                    dtm = new BLL_SysModule().GetModuleListBySysCode(strWhere).Tables[0];
                    dtf = new BLL_SysModuleFunctionRole().GetRoleModuleFunctionBySysCode(strDoctorId, strModuleIdLike, hidSysCode.Value.Split('|')[0]).Tables[0];
                    FullTree(dtm, dtf, tvMF);
                }

            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }
        /// <summary>
        /// 填充树结构
        /// </summary>
        private void FullTree(DataTable dtm, DataTable dtf, TreeView tv)
        {

            tv.Nodes.Clear();
            InitNavigationTree(tv.Nodes, "-1", dtm, dtf);
            tv.ShowLines = true;
            tv.ExpandAll();
        }
        //初始化菜单树
        private void InitNavigationTree(TreeNodeCollection tncCurrent, string cSupMenuSysCode, DataTable dtm, DataTable dtf)
        {
            try
            {
                DataView dvw = new DataView();
                TreeNode nodTemp = new TreeNode();

                dvw.Table = dtm;
                if (cSupMenuSysCode == "-1")
                {
                    dvw.RowFilter = " PARENTID ='0'";
                }
                else
                {
                    dvw.RowFilter = " PARENTID = '" + cSupMenuSysCode + "' ";
                }
                foreach (DataRowView drv in dvw)
                {
                    nodTemp = new TreeNode();
                    string nodText = string.Empty;

                    nodTemp.Value = drv["moduleid"].ToString();
                    DataRow[] dr = dtf.Select(" moduleid='" + drv["moduleid"].ToString() + "'", "functionid");
                    string check = "";
                    foreach (var d_temp in dr)
                    {
                        if (d_temp["mChecked"].ToString() != "-1") check = "checked='checked'";
                    }
                    nodText = @"<input type='checkbox' name='cbValue' ";
                    nodText += @" onclick='selAll(this);' ";
                    nodText += @" value='" + drv["moduleid"].ToString() + "|0'";
                    nodText += @" " + check + " ";
                    nodText += @" id='" + drv["slevel"].ToString().Replace(".", "_") + "'>";
                    nodText += @" " + drv["modulename"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;";

                    if (drv["islast"].ToString() == "1")
                    {
                        if (dr.Count() > 1)
                        {
                            nodText += @"<p>";

                        }
                        for (int i = 0; i < dr.Count(); i++)
                        {
                            if (i == 0)
                            {
                                nodText += @"<span style='padding-left:20px'>";
                            }
                            if (dr[i]["functionid"].ToString() != "0")
                            {
                                check = "";
                                if (dr[i]["fchecked"].ToString() != "-1") check = "checked='checked'";
                                nodText += @"&nbsp;&nbsp;&nbsp;&nbsp;";
                                nodText += @"<input type='checkbox'  name='cbValue' ";
                                nodText += @"onclick='selAll(this);' ";
                                nodText += @" " + check + " ";
                                nodText += @"value='" + dr[i]["moduleid"].ToString() + "|" + dr[i]["functionid"].ToString() + "'";
                                nodText += @"id='" + drv["slevel"].ToString().Replace(".", "_") + "_" + (i + 10).ToString() + "'>&nbsp;";
                                nodText += dr[i]["functionname"].ToString();
                            }
                        }
                        nodText += "</span>";
                    }
                    nodTemp.Text = nodText;
                    //nodTemp.ToolTip = drv["MODULENAME"].ToString();
                    nodTemp.NavigateUrl = "##";
                    tncCurrent.Add(nodTemp);
                    //strTempID = drv["ORG_ARCHI_ID"].ToString();
                    InitNavigationTree(nodTemp.ChildNodes, nodTemp.Value, dtm, dtf);
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string strDoctorId = string.Empty;
                string strModuleIdLike = string.Empty;

                if (Request["id"] != null)
                {
                    strDoctorId = Request["id"].ToString();
                }
                strModuleIdLike = hidFirstModule.Value.Substring(0, 2);
                string strDelSql = string.Empty;
                string strInsertSql = string.Empty;
                string checkeds = string.Empty;
                string s_1 = string.Empty;
                if (hidCheckedValue.Value != "")
                {
                    checkeds = hidCheckedValue.Value.TrimEnd(',');
                }
                if (new BLL_SysModuleFunctionRole().SetRoleModuleFunctionBySysCode(checkeds, strDoctorId, strModuleIdLike, hidSysCode.Value.Split('|')[0]) > 0)
                {
                    string strLog = string.Empty;
                    strLog += "【添加用户】：用户名【" + loginUser.SysRole_Names + "】；角色【" + loginUser.SysRole_Names + "】";
                    new BLL_clsAuth().AddLogFromBS(Module_Id, strLog);

                    ClientScript.RegisterStartupScript(this.GetType(), "Update", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('设置成功',{icon:1,time:1000})})})</script>");
                    getDate();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Update", "<script type='text/javascript'>$(function(){layer.ready(function(){layer.msg('设置失败',{icon:2})})})</script>");
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getDate();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Request["iurl"] != null)
            {
                Response.Redirect(Request["iurl"].ToString());
            }
            else
            {
                Response.Redirect("BasicDoctor.aspx");
            }
        }

        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            getDate();
        }
    }
}