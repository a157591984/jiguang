using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Rc.Cloud.BLL;

using Rc.Cloud.Model;



namespace Rc.Cloud.Web.Sys
{
    public partial class SysMenuManageSy : Rc.Cloud.Web.Common.InitPage
    {

        BLL_SysModule sysModuleBll = new BLL_SysModule();
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "95102000";
            if (!IsPostBack)
            {
                //DataTable dt = new SysCodeBLL().GetDataSet(0, " SysOrder", "DESC", "", new object { }).Tables[0];
                //Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSysCode, dt, "SysName", "SysCode", false);
                MenuListLoad("");
            }
        }

        //加载
        protected void MenuListLoad(string sql)
        {
            try
            {
                StringBuilder whereStr = new StringBuilder();
                //if (moduleNameTxt.Text.ToString() != "")
                //{
                //    whereStr.Append(" and MODULENAME like '%" + moduleNameTxt.Text.Trim() + "%'");
                //}
                if (moduleNameTxt.Text.ToString() != "")
                {
                    whereStr.Append(" and (MODULENAME like '%" + moduleNameTxt.Text.Trim() + "%' or MODULEID like '" + moduleNameTxt.Text.ToString() + "%')");
                }

                DataSet ds = new DataSet();
                ds = sysModuleBll.GetModuleListBySysCode(whereStr.ToString());
                rptModule.DataSource = ds;
                rptModule.DataBind();
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
        }
        //查寻
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            MenuListLoad("");
        }
    }
}