using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.Model;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using System.Data;

namespace Rc.Cloud.Web.Sys
{
    public partial class SysTasking : Rc.Cloud.Web.Common.InitPage
    {
        protected string userId = string.Empty;
        protected List<string> strArrYear = new List<string>();
        static Model_VSysUserRole modelLoginUser = new Model_VSysUserRole();
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = Request.QueryString["SysUser_ID"];
            modelLoginUser = loginUser;
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            string strSql = string.Format(@"select cd.Common_Dict_ID,cd.D_Name,chk = case  when task.SysUser_ID IS NULL  then '' else 'checked' end 
from dbo.Common_Dict cd left join SysUserTask task on cd.Common_Dict_ID=task.TaskID and task.SysUser_ID='{0}' ", userId);

            string strSqlYear = "select * from SysUserTask where SysUser_ID='" + userId + "' and TaskType='NF' ";
            DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlYear).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strArrYear.Add(dt.Rows[i]["TaskID"].ToString());
            }

            string strGradeTerm = strSql + "where cd.D_Type='6' order by cd.d_order, cd.D_Name ";
            rptGradeTerm.DataSource = Rc.Common.DBUtility.DbHelperSQL.Query(strGradeTerm).Tables[0];
            rptGradeTerm.DataBind();

            string strVersion = strSql + "where cd.D_Type='3' order by  cd.d_order,cd.D_Name ";
            rptVersion.DataSource = Rc.Common.DBUtility.DbHelperSQL.Query(strVersion).Tables[0];
            rptVersion.DataBind();

            string strSubject = strSql + "where cd.D_Type='7' order by  cd.d_order,cd.D_Name ";
            rptSubject.DataSource = Rc.Common.DBUtility.DbHelperSQL.Query(strSubject).Tables[0];
            rptSubject.DataBind();

        }

        [WebMethod]
        public static string TaskingSave(string targetUser, string nf, string njxq, string jcbb, string xk)
        {
            try
            {
                List<Model_SysUserTask> list = new List<Model_SysUserTask>();
                Model_SysUserTask model = new Model_SysUserTask();
                string[] strArrNF = nf.Split(',');
                string[] strArrNJXQ = njxq.Split(',');
                string[] strArrJCBB = jcbb.Split(',');
                string[] strArrXK = xk.Split(',');
                for (int i = 0; i < strArrNF.Length; i++)
                {
                    string[] strObj = strArrNF[i].Split('|');
                    model = new Model_SysUserTask();
                    model.SysUserTask_id = Guid.NewGuid().ToString();
                    model.SysUser_ID = targetUser;
                    model.TaskType = "NF";
                    model.TaskID = strObj[0];
                    model.TaskName = strObj[1];
                    model.CreateTime = DateTime.Now;
                    model.CreateUser = modelLoginUser.SysUser_ID;
                    list.Add(model);
                }
                for (int i = 0; i < strArrNJXQ.Length; i++)
                {
                    string[] strObj = strArrNJXQ[i].Split('|');
                    model = new Model_SysUserTask();
                    model.SysUserTask_id = Guid.NewGuid().ToString();
                    model.SysUser_ID = targetUser;
                    model.TaskType = "NJXQ";
                    model.TaskID = strObj[0];
                    model.TaskName = strObj[1];
                    model.CreateTime = DateTime.Now;
                    model.CreateUser = modelLoginUser.SysUser_ID;
                    list.Add(model);
                }
                for (int i = 0; i < strArrJCBB.Length; i++)
                {
                    string[] strObj = strArrJCBB[i].Split('|');
                    model = new Model_SysUserTask();
                    model.SysUserTask_id = Guid.NewGuid().ToString();
                    model.SysUser_ID = targetUser;
                    model.TaskType = "JCBB";
                    model.TaskID = strObj[0];
                    model.TaskName = strObj[1];
                    model.CreateTime = DateTime.Now;
                    model.CreateUser = modelLoginUser.SysUser_ID;
                    list.Add(model);
                }
                for (int i = 0; i < strArrXK.Length; i++)
                {
                    string[] strObj = strArrXK[i].Split('|');
                    model = new Model_SysUserTask();
                    model.SysUserTask_id = Guid.NewGuid().ToString();
                    model.SysUser_ID = targetUser;
                    model.TaskType = "XK";
                    model.TaskID = strObj[0];
                    model.TaskName = strObj[1];
                    model.CreateTime = DateTime.Now;
                    model.CreateUser = modelLoginUser.SysUser_ID;
                    list.Add(model);
                }

                if (new BLL_SysUserTask().TaskAllocation(list, targetUser, modelLoginUser.SysUser_ID) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

    }
}