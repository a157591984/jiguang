using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Rc.Common.DBUtility;
using Rc.Common.StrUtility;
using System.Data;
using Rc.Cloud.Web.Common;
using Rc.Common;
using Rc.Common.Config;
using System.Text;
using System.Web.Services;
using Newtonsoft.Json;
namespace Rc.Cloud.Web.teacher
{
    public partial class PrepareLessons_view : Rc.Cloud.Web.Common.FInitData
    {
        public string ResourceFolder_Id = string.Empty;
        public string GradeId = string.Empty;
        public string SubjectId = string.Empty;
        public string guid = string.Empty;
        BLL_PrpeLesson bll = new BLL_PrpeLesson();
        protected void Page_Load(object sender, EventArgs e)
        {
            ResourceFolder_Id = Request.QueryString["ResourceFolder_Id"].Filter();
            GradeId = Request.QueryString["GradeId"].Filter();
            SubjectId = Request.QueryString["SubjectId"].Filter();
            if (!IsPostBack)
            {
                //年级
                DataTable dt = new DataTable();
                string sql = @"select  distinct t.GradeId,t.GradeName from [dbo].[VW_UserOnClassGradeSchool] t  
inner join VW_UserOnClassGradeSchool t1 on t1.SchoolId=t.SchoolId and t1.userid='" + FloginUser.UserId + @"'
where t.GradeId<>'' ";
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    pfunction.SetDdl(ddlGrade, dt, "GradeName", "GradeId", true);
                }
                //学科
                dt = new DataTable();
                sql = @"select  distinct dc.Common_Dict_Id,dc.D_Name from f_user u
inner join [dbo].[Common_Dict] dc on dc.Common_Dict_Id=u.Subject where  Subject<>''";
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_Id", true);
                }
                //备课阶段
                InitInfo();
                LoadData();
                if (!string.IsNullOrEmpty(ResourceFolder_Id))
                {
                    hidResourceFolder_Id.Value = ResourceFolder_Id;
                    ddlGrade.Enabled = false;
                    ddlStage.Enabled = false;
                    ddlSubject.Enabled = false;
                    txtEndTime.Enabled = false;
                    txtStartTime.Enabled = false;

                }
                else
                {
                    hidResourceFolder_Id.Value = Guid.NewGuid().ToString();
                }
            }
        }
        /// <summary>
        /// 备课阶段
        /// </summary>
        private void InitInfo()
        {
            ddlStage.Items.Add(new ListItem("--请选择--", "-1"));//--请选择--;
            foreach (PrpeLessonStageEnum item in Enum.GetValues(typeof(PrpeLessonStageEnum)))
            {
                ddlStage.Items.Add(new ListItem(EnumService.GetDescription(item), item.ToString()));
            }

        }

        public void LoadData()
        {
            try
            {
                Model_PrpeLesson model = new Model_PrpeLesson();
                model = new BLL_PrpeLesson().GetModel(ResourceFolder_Id);
                Model_ResourceFolder resourceFolder = new Model_ResourceFolder();
                resourceFolder = new BLL_ResourceFolder().GetModel(ResourceFolder_Id);
                if (model != null && resourceFolder != null)
                {
                    this.txtResourceFolder_Name.Text = resourceFolder.ResourceFolder_Name;
                    this.txtEndTime.Text = pfunction.ToShortDate(model.EndTime.ToString());
                    this.txtNameRule.Value = model.NameRule;
                    this.txtRemark.Value = model.Remark;
                    this.txtRequire.Text = model.Require;
                    this.txtStartTime.Text = pfunction.ToShortDate(model.StartTime.ToString());
                    ddlGrade.SelectedValue = model.Grade.TrimEnd();
                    ddlStage.SelectedValue = model.Stage;
                    ddlSubject.SelectedValue = model.Subject;
                }
            }
            catch (Exception ex)
            {

            }
        }


        [WebMethod]
        public static string GetList(string ResourceFolder_Id)
        {
            try
            {
                Model_F_User loginUser = HttpContext.Current.Session["FLoginUser"] as Model_F_User;
                DataTable dt = new DataTable();
                BLL_PrpeLesson bll = new BLL_PrpeLesson();
                string sql = @"select  u.UserId,u.UserName,u.TrueName,c.D_Name as Subject from [dbo].[PrpeLesson_Person] p
left join f_user u on u.UserId=p.ChargePerson
left join Common_Dict c on c.Common_Dict_ID=u.Subject
where ResourceFolder_Id='" + ResourceFolder_Id.Filter() + "' group by u.UserId,u.UserName,u.TrueName,c.D_Name ";
                string SqlClass = @" select distinct ClassId,ClassName,userId from [dbo].[VW_UserOnClassGradeSchool] where IType='class'";
                DataTable dtclass = Rc.Common.DBUtility.DbHelperSQL.Query(SqlClass).Tables[0];
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string ClassName = string.Empty;
                    DataRow[] dr = dtclass.Select("UserId='" + dt.Rows[i]["UserId"].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        foreach (DataRow item in dr)
                        {
                            ClassName += item["ClassName"].ToString() + ",";
                        }
                    }
                    listReturn.Add(new
                    {
                        UserId = dt.Rows[i]["UserId"].ToString(),
                        ClassName = !string.IsNullOrEmpty(ClassName.TrimEnd(',')) ? ClassName.TrimEnd(',') : "-",
                        Subject = dt.Rows[i]["Subject"].ToString(),
                        UserName = dt.Rows[i]["UserName"].ToString(),
                        TrueName = dt.Rows[i]["TrueName"].ToString(),
                        ResourceFolder_Id = ResourceFolder_Id,
                    });
                    inum++;
                }
                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        list = listReturn
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
        /// <summary>
        /// 移除负责人
        /// </summary>
        /// <param name="ChargePerson"></param>
        /// <param name="ResourceFolder_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string DeletePerson(string ChargePerson, string ResourceFolder_Id)
        {
            try
            {
                string sql = @"delete from PrpeLesson_Person where ChargePerson='" + ChargePerson.Filter() + "' and ResourceFolder_Id='" + ResourceFolder_Id.Filter() + "'";
                if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql) > 0)
                {
                    return "1";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}