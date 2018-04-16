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
    public partial class PrepareLessons : Rc.Cloud.Web.Common.FInitData
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
                string sql = string.Empty;

                //备课阶段
                InitInfo();

                if (!string.IsNullOrEmpty(ResourceFolder_Id))
                {
                    sql = @"select  distinct t.GradeId,t.GradeName from [dbo].[VW_UserOnClassGradeSchool] t  
inner join VW_UserOnClassGradeSchool t1 on t1.SchoolId=t.SchoolId and t1.userid='" + FloginUser.UserId + @"'
where t.GradeId<>'' ";
                    dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        pfunction.SetDdl(ddlGrade, dt, "GradeName", "GradeId", true);
                    }
                    hidResourceFolder_Id.Value = ResourceFolder_Id;
                    //学科
                    dt = new DataTable();
                    sql = @"select  distinct dc.Common_Dict_Id,dc.D_Name,dc.D_Order from f_user u
inner join [dbo].[Common_Dict] dc on dc.Common_Dict_Id=u.Subject where  Subject<>'' order by dc.D_Order ";
                    dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_Id", true);
                    }
                    LoadData();
                }
                else
                {
                    string Strwhere = string.Empty;
                    switch (FloginUser.UserPost)
                    {
                        #region 校长，副校长，教务主任只显示本学校的
                        case UserPost.校长:
                            Strwhere = @" and SchoolId in (select distinct SchoolId from VW_UserOnClassGradeSchool where UserId='" + FloginUser.UserId + "')";
                            sql = @"select  distinct GradeId,GradeName from [dbo].[VW_UserOnClassGradeSchool]  where  GradeId<>''" + Strwhere;
                            break;
                        case UserPost.副校长:
                            Strwhere = @" and SchoolId in (select distinct SchoolId from VW_UserOnClassGradeSchool where UserId='" + FloginUser.UserId + "')";
                            sql = @"select  distinct GradeId,GradeName from [dbo].[VW_UserOnClassGradeSchool]  where  GradeId<>''" + Strwhere;
                            break;
                        case UserPost.教务主任:
                            Strwhere = @" and SchoolId in (select distinct SchoolId from VW_UserOnClassGradeSchool where UserId='" + FloginUser.UserId + "')";
                            sql = @"select  distinct GradeId,GradeName from [dbo].[VW_UserOnClassGradeSchool]  where  GradeId<>''" + Strwhere;
                            break;
                        #endregion
                        #region 年级组长，教研组长,备课组长只显示本年级
                        case UserPost.教研组长:
                            Strwhere = @" and GradeId in (select distinct GradeId from VW_UserOnClassGradeSchool where UserId='" + FloginUser.UserId + "')";
                            sql = @"select  distinct GradeId,GradeName from [dbo].[VW_UserOnClassGradeSchool]  where  GradeId<>''" + Strwhere;
                            break;
                        case UserPost.年级组长:
                            Strwhere = @" and GradeId in (select distinct GradeId from VW_UserOnClassGradeSchool where UserId='" + FloginUser.UserId + "')";
                            sql = @"select  distinct GradeId,GradeName from [dbo].[VW_UserOnClassGradeSchool]  where  GradeId<>''" + Strwhere;
                            break;
                        case UserPost.备课组长:
                            Strwhere = @" and GradeId in (select distinct GradeId from VW_UserOnClassGradeSchool where UserId='" + FloginUser.UserId + "')";
                            sql = @"select  distinct GradeId,GradeName from [dbo].[VW_UserOnClassGradeSchool]  where  GradeId<>''" + Strwhere;
                            break;
                        #endregion
                        #region 普通老师显示参与备课的年级
                        case UserPost.普通老师:
                            Strwhere = @" where  userId='" + FloginUser.UserId + "' and GradeId<>''";
                            sql = @"select distinct GradeId, GradeName from [VW_UserOnClassGradeSchool]" + Strwhere;
                            break;
                        #endregion
                    }
                    dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        pfunction.SetDdl(ddlGrade, dt, "GradeName", "GradeId", true);
                    }
                    if (FloginUser.UserPost == UserPost.备课组长 || FloginUser.UserPost == UserPost.普通老师)
                    {
                        Strwhere = @" where  u.Userid='" + FloginUser.UserId + "'";
                        sql = @"select dc.Common_Dict_Id,dc.D_Name from f_user u
inner join [Common_Dict] dc on dc.Common_Dict_Id=u.Subject" + Strwhere;
                        dt = new DataTable();
                        dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_Id", true);
                        }
                    }
                    else
                    {
                        dt = new DataTable();
                        sql = @"select  distinct dc.Common_Dict_Id,dc.D_Name,dc.D_Order from f_user u
inner join [dbo].[Common_Dict] dc on dc.Common_Dict_Id=u.Subject where  Subject<>'' order by dc.D_Order ";
                        dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_Id", true);
                        }
                    }
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ResourceFolder_Id))
                {
                    #region 添加
                    Model_PrpeLesson prpeLesson = new Model_PrpeLesson();
                    Model_ResourceFolder resourceFolder = new Model_ResourceFolder();
                    #region 备课表
                    prpeLesson.ResourceFolder_Id = hidResourceFolder_Id.Value;
                    prpeLesson.Grade = ddlGrade.SelectedValue;
                    prpeLesson.Subject = ddlSubject.SelectedValue;
                    prpeLesson.Stage = ddlStage.SelectedValue;
                    prpeLesson.StartTime = Convert.ToDateTime(txtStartTime.Text.TrimEnd());
                    prpeLesson.EndTime = Convert.ToDateTime(txtEndTime.Text.TrimEnd());
                    prpeLesson.NameRule = txtNameRule.Value.TrimEnd();
                    prpeLesson.Remark = txtRemark.Value.TrimEnd();
                    prpeLesson.Require = txtRequire.Text.TrimEnd();
                    prpeLesson.CreateUser = FloginUser.UserId;
                    prpeLesson.CreateTime = DateTime.Now;
                    #endregion
                    #region 负责人表
                    Model_PrpeLesson_Person prpeLesson_Person = new Model_PrpeLesson_Person();
                    prpeLesson_Person.PrpeLesson_Person_Id = Guid.NewGuid().ToString();
                    prpeLesson_Person.ChargePerson = FloginUser.UserId;
                    prpeLesson_Person.CreateUser = FloginUser.UserId;
                    prpeLesson_Person.CreateTime = DateTime.Now;
                    prpeLesson_Person.ResourceFolder_Id = hidResourceFolder_Id.Value;
                    #endregion
                    #region 文件夹
                    resourceFolder.ResourceFolder_Id = hidResourceFolder_Id.Value;
                    resourceFolder.ResourceFolder_ParentId = "0";
                    resourceFolder.ResourceFolder_Level = 0;
                    resourceFolder.ResourceFolder_Name = txtResourceFolder_Name.Text.TrimEnd();
                    resourceFolder.Book_ID = hidResourceFolder_Id.Value;
                    resourceFolder.CreateFUser = FloginUser.UserId;
                    resourceFolder.CreateTime = DateTime.Now;
                    resourceFolder.Subject = ddlSubject.SelectedValue;
                    resourceFolder.Resource_Type = Resource_TypeConst.集体备课文件;
                    resourceFolder.Resource_Class = Resource_ClassConst.自有资源;
                    #endregion
                    if (bll.Add(prpeLesson, prpeLesson_Person, resourceFolder))
                    {
                        string i = "window.opener.loadData();";
                        if (FloginUser.UserPost == UserPost.普通老师)
                        {
                            i = "window.opener.location.reload();";
                        }
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('添加成功', { icon: 1, time: 2000 }, function () {" + i + "window.opener=null;window.close();})</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('添加失败!',{ time: 2000,icon:2});</script>");
                        return;
                    }
                    #endregion
                }
                else
                {
                    Model_PrpeLesson model = new Model_PrpeLesson();
                    model = new BLL_PrpeLesson().GetModel(ResourceFolder_Id);
                    Model_ResourceFolder resourceFolder = new Model_ResourceFolder();
                    resourceFolder = new BLL_ResourceFolder().GetModel(ResourceFolder_Id);
                    #region 文件夹
                    resourceFolder.ResourceFolder_Level = 0;
                    resourceFolder.ResourceFolder_Name = txtResourceFolder_Name.Text.TrimEnd();
                    #endregion
                    #region 备课表
                    model.Grade = ddlGrade.SelectedValue;
                    model.Subject = ddlSubject.SelectedValue;
                    model.Stage = ddlStage.SelectedValue;
                    model.StartTime = Convert.ToDateTime(txtStartTime.Text.TrimEnd());
                    model.EndTime = Convert.ToDateTime(txtEndTime.Text.TrimEnd());
                    model.NameRule = txtNameRule.Value.TrimEnd();
                    model.Remark = txtRemark.Value.TrimEnd();
                    model.Require = txtRequire.Text.TrimEnd();
                    #endregion
                    if (bll.Update(model, resourceFolder))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('修改成功', { icon: 1, time: 2000 }, function () {window.opener.loadData();window.opener=null;window.close();})</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('修改失败!',{ time: 2000,icon:2});</script>");
                        return;
                    }
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
where ResourceFolder_Id='" + ResourceFolder_Id.Filter() + "' and ChargePerson<>'" + loginUser.UserId + "' group by u.UserId,u.UserName,u.TrueName,c.D_Name ";
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