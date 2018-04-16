using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Newtonsoft.Json;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;

namespace Rc.Cloud.Web.Principal
{
    public partial class AnalysisGradeList : Rc.Cloud.Web.Common.FInitData
    {
        DataTable dtGrade = new DataTable();
        protected string UserID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserID = FloginUser.UserId;
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            dtGrade = StatsCommonHandle.GetTeacherGradeData();
            if (dtGrade.Rows.Count > 0)
            {
                string ss = dtGrade.Rows[0]["GroupGradeTypeName"].ToString();
                DataView dataView = dtGrade.DefaultView;
                DataTable dtDistinct = dataView.ToTable(true, "GroupGradeType", "GroupGradeTypeName");
                rptStudysection.DataSource = dtDistinct;
                rptStudysection.DataBind();
            }
        }

        /// <summary>
        /// 获取年级列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string GetGradeList(string UserID)
        {
            try
            {
                UserID = UserID.Filter();
                string strWhere = string.Empty;
                List<object> ListReturn = new List<object>();

                DataTable dt = StatsCommonHandle.GetTeacherGradeData();
                foreach (DataRow item in dt.Rows)
                {
                    ListReturn.Add(new
                    {
                        GradeName = item["GradeName"].ToString(),
                        GradeId = item["GradeId"].ToString(),
                        ClassCount = item["ClassCount"].ToString(),
                        StudentCount = item["StudentCount"].ToString()
                    });
                }
                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "",
                        list = ListReturn
                    });
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "暂无数据"
                    });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = ex.Message.ToString()
                });
            }
        }

        protected void rptStudysection_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptProduct = (Repeater)e.Item.FindControl("rptGrade");
                DataRowView rowv = (DataRowView)e.Item.DataItem;
                rptProduct.DataSource = dtGrade.Select("GroupGradeType='" + rowv["GroupGradeType"] + "'");
                rptProduct.DataBind();
            }
        }
    }
}