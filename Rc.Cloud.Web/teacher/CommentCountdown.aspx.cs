using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.teacher
{
    public partial class CommentCountdown : Rc.Cloud.Web.Common.FInitData
    {
        protected string strEndTime = string.Empty;
        protected int intTimeLength = 0;
        protected string isCountdown = string.Empty;
        protected string hwId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hwId = Request["hwId"].Filter();
                Model_HomeWork modelHW = new BLL_HomeWork().GetModel(hwId);
                strEndTime = modelHW.CreateTime.ToString();
                int.TryParse(modelHW.isTimeLength.ToString(), out intTimeLength);
                isCountdown = modelHW.IsCountdown;
                if (modelHW == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>function(){layer.msg('作业数据不存在或已删除!',{ time: 0,icon:2});</script>");
                    return;
                }
                DataTable dt = new BLL_Student_HomeWork().GetHWStuCount(hwId).Tables[0];
                if (dt.Rows.Count == 1)
                {
                    ltlStu_Assign.Text = string.Format("共{0}位同学", dt.Rows[0]["AssignedCount"]);
                    ltlStu_Committed.Text = string.Format("已交{0}位同学", dt.Rows[0]["CommittedCount"]);
                    ltlStu_UnCommitted.Text = string.Format("未交{0}位同学", dt.Rows[0]["UnCommittedCount"]);
                    if (Convert.ToInt16(dt.Rows[0]["CommittedCount"]) == 0)
                    {
                        btnComment.Attributes.Add("disabled", "disabled");
                    }
                }

                string strSql = string.Format(@"select shw.Student_Id,shws.Student_HomeWork_Status
,(CASE WHEN U.TrueName IS NULL THEN U.UserName WHEN U.TrueName = '' THEN U.UserName ELSE U.TrueName END) AS StuName 
from Student_HomeWork shw 
inner join F_User AS U on shw.Student_Id = U.UserId 
inner join Student_HomeWork_Submit shws on shws.Student_HomeWork_Id=shw.Student_HomeWork_Id and Student_HomeWork_Status='1'
where shw.HomeWork_Id='{0}' order by shws.Student_Answer_Time ", hwId);
                rptStu.DataSource = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                rptStu.DataBind();

            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>function(){layer.msg('数据加载失败!',{ time: 0,icon:2});</script>");
                return;
            }
        }

        [WebMethod]
        public static string GetStuSubmitInfo(string hwId)
        {
            try
            {
                List<object> listYJ = new List<object>();
                List<object> listWJ = new List<object>();
                string strSql = string.Format(@"select shw.Student_Id,shws.Student_HomeWork_Status
,(CASE WHEN U.TrueName IS NULL THEN U.UserName WHEN U.TrueName = '' THEN U.UserName ELSE U.TrueName END) AS StuName 
from Student_HomeWork shw 
inner join F_User AS U on shw.Student_Id = U.UserId 
inner join Student_HomeWork_Submit shws on shws.Student_HomeWork_Id=shw.Student_HomeWork_Id 
where shw.HomeWork_Id='{0}' order by shws.Student_Answer_Time ", hwId);

                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                DataRow[] drYJ = dt.Select("Student_HomeWork_Status='1'");
                DataRow[] drWJ = dt.Select("Student_HomeWork_Status='0'");
                List<object> listReturn = new List<object>();
                foreach (DataRow item in drYJ)
                {
                    listReturn.Add(new
                    {
                        StuId = item["Student_Id"],
                        StuName = item["StuName"]
                    });
                }
                if (drYJ.Length == 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "暂无已交数据"
                    });
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        list = listReturn,
                        CommittedCount = drYJ.Length,
                        UnCommittedCount = drWJ.Length
                    });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "err"
                });
            }
        }

        protected void btnComment_Click(object sender, EventArgs e)
        {
            try
            {
                Model_HomeWork modelHW = new BLL_HomeWork().GetModel(hwId);
                Response.Redirect("CheckCommentStatsHelper.aspx?ResourceToResourceFolder_Id=" + modelHW.ResourceToResourceFolder_Id
                    + "&HomeWork_Name=" + modelHW.HomeWork_Name
                    + "&HomeWork_Id=" + modelHW.HomeWork_Id, true);
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>function(){layer.msg('数据加载失败!',{ time: 0,icon:2});</script>");
                return;
            }
        }

    }
}