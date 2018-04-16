using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using System.Data;

namespace Rc.Cloud.Web.student
{
    public partial class CheckStuHWAnalysis : Rc.Cloud.Web.Common.FInitData
    {
        protected string StuId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            StuId = FloginUser.UserId;
        }

        /// <summary>
        /// 验证计算
        /// </summary>
        [WebMethod]
        public static string CheckCalculation(string StuId)
        {
            try
            {
//                string strSql = string.Format(@"select isnull(max(t2.CorrectTime),convert(varchar(10),getdate()-1000,23)) as CorrectTime from Student_HomeWork t
//inner join Student_HomeWork_Correct t2 on t2.Student_HomeWork_Id=t.Student_HomeWork_Id
//where t.Student_Id='{0}' and t2.Student_HomeWork_CorrectStatus='1' ", StuId.Filter());
//                DataTable dtStuHWCorrect = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                string strSql = string.Format(@"select isnull(max(t.CreateTime),convert(varchar(10),getdate()-1000,23)) as CreateTime from StatsStuHW_TQ_KP t where t.Student_Id='{0}' ", StuId.Filter());
                DataTable dtStuHWCorrect = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                
                string strSql2 = string.Format(@"select isnull(max(CreateTime),convert(varchar(10),getdate()-1000,23)) as ExecTime from StatsStuHW_Analysis_KP 
where Student_Id='{0}' ", StuId.Filter());
                DataTable dtStuHWAnalysis = Rc.Common.DBUtility.DbHelperSQL.Query(strSql2).Tables[0];

                DateTime dTime1 = Convert.ToDateTime(dtStuHWCorrect.Rows[0]["CreateTime"]);
                DateTime dTime2 = Convert.ToDateTime(dtStuHWAnalysis.Rows[0]["ExecTime"]);

                if (dTime1 > dTime2)
                {
                    strSql = string.Format("exec test_P_StatsStuHWData '{0}' ", StuId);
                    Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime(strSql, 600);
                }
                return "1";
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("", "计算失败：" + ex.Message.ToString());
                return "0";
            }
        }
    }
}