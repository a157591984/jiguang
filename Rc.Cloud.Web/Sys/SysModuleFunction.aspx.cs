using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Rc.Cloud.BLL;


namespace Rc.Cloud.Web.Sys
{
    public partial class SysModuleFunction : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90500300";
            if (!IsPostBack)
            {
                try
                {
                    GetTree();
                }
                catch (Exception ex)
                {
                    new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                    throw ex; 
                }

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetTree();
        }

        private void GetTree()
        {
            try
            {
                DataTable dt = new DataTable();
                if (hidSysCode.Value != "" && hidFirstModule.Value != "")
                {
                    string strWhere = string.Empty;
                    strWhere += " and ISINTREE ='Y' and  sm.SysCode='" + hidSysCode.Value.Split('|')[0] + "' and (slevel LIKE '" + hidFirstModule.Value.Substring(0, 2) + "%') ";
                    dt = new BLL_SysModule().GetModuleListBySysCode(strWhere).Tables[0];
                    FullTree(dt, this.tvModuleFirst);
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex; 
            }
            //tvMenu.Font.

        }
        //#region 树结构
        /// <summary>
        /// 填充树结构
        /// </summary>
        private void FullTree(DataTable dt, TreeView tv)
        {
            tv.Nodes.Clear();
            InitNavigationTree(tv.Nodes, "-1", dt);
            //tv.ExpandDepth = 1;
            tv.ExpandAll();
        }
        //初始化菜单树
        private void InitNavigationTree(TreeNodeCollection tncCurrent, string cSupMenuSysCode, DataTable dt)
        {
            try
            {
                DataView dvw = new DataView();
                TreeNode nodTemp = new TreeNode();
                dvw.Table = dt;
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
                    nodTemp.Value = drv["MODULEID"].ToString() + "|" + drv["isLast"].ToString();

                    string strNodeText = string.Empty;

                    strNodeText = drv["MODULENAME"].ToString();
                    nodTemp.Text = strNodeText;
                    nodTemp.ToolTip = drv["MODULENAME"].ToString();

                    tncCurrent.Add(nodTemp);
                    InitNavigationTree(nodTemp.ChildNodes, nodTemp.Value.Split('|')[0], dt);
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        protected void tvModuleFirst_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (tvModuleFirst.SelectedNode.Value.Split('|')[1] == "1")
                {
                    lblAlert.Text = "正在设置模块【" + tvModuleFirst.SelectedNode.Text + "】的功能";
                    DataTable dtFunctionSelected = new BLL_Sys_Function().GetSysModuleFunctionByModuleID(tvModuleFirst.SelectedNode.Value.Split('|')[0]).Tables[0];
                    DataTable dtFunction = new BLL_Sys_Function().GetAllSys_FunctionList().Tables[0];
                    Rc.Cloud.Web.Common.pfunction.SetCbl(cblFunction, dtFunction, "FunctionName", "FunctionID");
                    Rc.Cloud.Web.Common.pfunction.SetCbl(cblFunction, "FunctionID", dtFunctionSelected);
                    hidModuleId.Value = tvModuleFirst.SelectedNode.Value.Split('|')[0];
                    cblFunction.Visible = true;
                    btnSave.Visible = true;
                }

                else
                {
                    hidModuleId.Value = "";
                    lblAlert.Text = "不可以在模块【" + tvModuleFirst.SelectedNode.Text + "】上设置功能";
                    cblFunction.Visible = false;
                    btnSave.Visible = false;
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex; 
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ModuleId = string.Empty;
                string FunctionIds = string.Empty;
                ModuleId = hidModuleId.Value;
                FunctionIds = Rc.Cloud.Web.Common.pfunction.GetCblCheckedValue(cblFunction, ",");
                if (new BLL_SysModuleFunction().SetModuleFunctionByModuleIdAndSysCode(ModuleId, FunctionIds, hidSysCode.Value.Split('|')[0]))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function () {layer.msg('操作成功',{icon: 1,time:1000})});</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function () {layer.msg('操作失败',{icon: 2})});</script>");
                }
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTree();
        }

       
    }
}