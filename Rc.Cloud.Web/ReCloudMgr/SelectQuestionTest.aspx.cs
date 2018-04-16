using Newtonsoft.Json;
using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class SelectQuestionTest : Rc.Cloud.Web.Common.InitPage
    {
        public string GradeTerm = string.Empty;
        public string Subject = string.Empty;
        public string ScoreId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GradeTerm = Request.QueryString["GradeTerm"].Filter();
            Subject = Request.QueryString["Subject"].Filter();
            ScoreId = Request.QueryString["ScoreId"].Filter();
            if (!IsPostBack)
            {
                string strSql = @"select distinct t2.DictRelation_Id,t2.Parent_Id,t3.D_Name,t3.D_Order from DictRelation t 
inner join DictRelation_Detail t2 on t2.DictRelation_Id=t.DictRelation_Id
inner join Common_Dict t3 on t3.Common_Dict_Id=t2.Parent_Id
where t.HeadDict_Id='722CE025-A876-4880-AAC1-5E416F3BDB1E' and t.SonDict_Id='934A3541-116E-438C-B9BA-4176368FCD9B'
order by t3.D_Order ";
                DataTable dtDict = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                rptGradeTerm.DataSource = dtDict;
                rptGradeTerm.DataBind();
            }
        }
        [WebMethod]
        public static string GetSubDictList(string HeadDict_Id, string SonDict_Id, string Parent_Id, string defaultId)
        {
            StringBuilder stbHtml = new StringBuilder();
            try
            {
                string strSql = string.Format(@"select t.Dict_Id,t3.D_Name,t3.D_Order from DictRelation_Detail t 
inner join DictRelation t2 on t2.DictRelation_Id=t.DictRelation_Id
inner join Common_Dict t3 on t3.Common_Dict_Id=t.Dict_Id 
where t2.HeadDict_Id='{0}' and t2.SonDict_Id='{1}' and t.Parent_Id='{2}'
order by t3.D_Order ", HeadDict_Id, SonDict_Id, Parent_Id);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(defaultId))
                    {
                        if (i == 0)
                        {
                            stbHtml.AppendFormat("<a href=\"##\" ajax-value=\"{0}\" class=\"{2}\">{1}</a>", dt.Rows[i]["Dict_Id"].ToString(), dt.Rows[i]["D_Name"].ToString(), "active");
                        }
                        else
                        {
                            stbHtml.AppendFormat("<a href=\"##\" ajax-value=\"{0}\" class=\"{2}\">{1}</a>", dt.Rows[i]["Dict_Id"].ToString(), dt.Rows[i]["D_Name"].ToString(), defaultId == dt.Rows[i]["Dict_Id"].ToString() ? "active" : "");
                        }
                    }
                    else
                    {
                        stbHtml.AppendFormat("<a href=\"##\" ajax-value=\"{0}\" class=\"{2}\">{1}</a>", dt.Rows[i]["Dict_Id"].ToString(), dt.Rows[i]["D_Name"].ToString(), defaultId == dt.Rows[i]["Dict_Id"].ToString() ? "active" : "");
                    }

                }
                //foreach (DataRow item in dt.Rows)
                //{
                //    if (string.IsNullOrEmpty(defaultId))
                //    {

                //    }
                //    stbHtml.AppendFormat("<a href=\"##\" ajax-value=\"{0}\" class=\"{2}\">{1}</a>", item["Dict_Id"].ToString(), item["D_Name"].ToString(), defaultId == item["Dict_Id"].ToString() ? "active" : "");
                //}

                return stbHtml.ToString();

            }
            catch (Exception ex)
            {
                return stbHtml.ToString();
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string ScoreId, string GradeTerm, string Subject, string Syllabus, string Test_Category)
        {
            try
            {
                BLL_S_TestingPoint bll = new BLL_S_TestingPoint();
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " Parent_Id='0' ";
                if (!string.IsNullOrEmpty(GradeTerm)) strWhere += " and t.GradeTerm='" + GradeTerm.Filter() + "' ";
                if (!string.IsNullOrEmpty(GradeTerm)) strWhere += " and t.Subject='" + Subject.Filter() + "' ";
                if (!string.IsNullOrEmpty(GradeTerm)) strWhere += " and t.Syllabus='" + Syllabus.Filter() + "' ";
                if (!string.IsNullOrEmpty(GradeTerm)) strWhere += " and t.Test_Category='" + Test_Category.Filter() + "' ";

                string sql = string.Format(@"select t.*,tk.S_TestQuestions_TP_Id,TPNameBasic from S_TestingPoint t
left join S_TestingPointBasic t1 on t1.S_TestingPointBasic_Id=t.S_TestingPointBasic_Id
left join [S_TestQuestions_TP] tk on tk.S_TestingPoint_Id=t.S_TestingPoint_Id and TestQuestions_Score_ID='{0}' where " + strWhere + " order by TPCode", ScoreId.Filter());
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                DataTable dtAll = bll.GetList("").Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow[] drSub = dtAll.Select("Parent_Id='" + dt.Rows[i]["S_TestingPoint_Id"].ToString() + "'");
                    listReturn.Add(new
                    {
                        S_TestingPoint_Id = dt.Rows[i]["S_TestingPoint_Id"].ToString(),
                        Parent_Id = dt.Rows[i]["Parent_Id"].ToString().Trim(),
                        TPName = string.IsNullOrEmpty(dt.Rows[i]["TPNameBasic"].ToString()) ? dt.Rows[i]["TPName"].ToString() : dt.Rows[i]["TPNameBasic"].ToString(),
                        IsLast = dt.Rows[i]["IsLast"].ToString(),
                        parentIdStr = "",
                        paddingLeft = "",
                        hasChildren = drSub.Length,
                        TestQuestions_Knowledge_ID = dt.Rows[i]["S_TestQuestions_TP_Id"].ToString(),
                        IsChecked = !string.IsNullOrEmpty(dt.Rows[i]["S_TestQuestions_TP_Id"].ToString()) ? "checked" : "",
                        ScoreId = ScoreId,
                        tkid = !string.IsNullOrEmpty(dt.Rows[i]["S_TestQuestions_TP_Id"].ToString()) ? dt.Rows[i]["S_TestQuestions_TP_Id"].ToString() : Guid.NewGuid().ToString()
                    });
                }

                if (dt.Rows.Count > 0)
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
        [WebMethod]
        public static string GetSubDataList(string ScoreId, string parentId, string parentIdStr)
        {
            try
            {
                BLL_S_TestingPoint bll = new BLL_S_TestingPoint();
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " where  Parent_Id='" + parentId + "'";
                string sql = string.Format(@"select t.*,tk.S_TestQuestions_TP_Id,TPNameBasic from S_TestingPoint t
left join S_TestingPointBasic t1 on t1.S_TestingPointBasic_Id=t.S_TestingPointBasic_Id
left join [S_TestQuestions_TP] tk on tk.S_TestingPoint_Id=t.S_TestingPoint_Id and TestQuestions_Score_ID='{0}' " + strWhere + " order by TPCode", ScoreId);
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                DataTable dtAll = bll.GetList("").Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow[] drSub = dtAll.Select("Parent_Id='" + dt.Rows[i]["S_TestingPoint_Id"].ToString() + "'");
                    listReturn.Add(new
                    {
                        S_TestingPoint_Id = dt.Rows[i]["S_TestingPoint_Id"].ToString(),
                        Parent_Id = dt.Rows[i]["Parent_Id"].ToString(),
                        TPName = string.IsNullOrEmpty(dt.Rows[i]["TPNameBasic"].ToString()) ? dt.Rows[i]["TPName"].ToString() : dt.Rows[i]["TPNameBasic"].ToString(),
                        IsLast = dt.Rows[i]["IsLast"].ToString(),
                        parentIdStr = parentIdStr,
                        paddingLeft = 15 * (parentIdStr.Split('&').Length - 1),
                        hasChildren = drSub.Length,
                        TestQuestions_Knowledge_ID = dt.Rows[i]["S_TestQuestions_TP_Id"].ToString(),
                        IsChecked = !string.IsNullOrEmpty(dt.Rows[i]["S_TestQuestions_TP_Id"].ToString()) ? "checked" : "",
                        ScoreId = ScoreId,
                        tkid = !string.IsNullOrEmpty(dt.Rows[i]["S_TestQuestions_TP_Id"].ToString()) ? dt.Rows[i]["S_TestQuestions_TP_Id"].ToString() : Guid.NewGuid().ToString()
                    });
                }

                if (dt.Rows.Count > 0)
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
        /// 添加考点
        /// </summary>
        /// <param name="ScoreId"></param>
        /// <param name="AttrId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string AddAtrr(string ScoreId, string AttrId, string TestQuestions_Knowledge_ID)
        {
            try
            {
                ScoreId = ScoreId.Filter();
                AttrId = AttrId.Filter();
                TestQuestions_Knowledge_ID = TestQuestions_Knowledge_ID.Filter();
                Rc.Cloud.Model.Model_VSysUserRole loginUser = (Rc.Cloud.Model.Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                Model_TestQuestions_Score score = new BLL_TestQuestions_Score().GetModel(ScoreId);
                Model_S_TestQuestions_TP model = new Model_S_TestQuestions_TP();
                model.S_TestQuestions_TP_Id = TestQuestions_Knowledge_ID;
                model.S_TestingPoint_Id = AttrId;
                model.TestQuestions_Score_ID = ScoreId;
                model.TestQuestions_Id = score.TestQuestions_Id;
                model.ResourceToResourceFolder_Id = score.ResourceToResourceFolder_Id;
                model.CreateUser = loginUser.SysUser_ID;
                model.CreateTime = DateTime.Now;
                if (new BLL_S_TestQuestions_TP().Add(model))
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
        /// <summary>
        /// 删除知识点
        /// </summary>
        /// <param name="TestQuestions_Knowledge_ID"></param>
        /// <returns></returns>
        [WebMethod]
        public static string DeleteAtrr(string TestQuestions_Knowledge_ID)
        {
            try
            {
                TestQuestions_Knowledge_ID = TestQuestions_Knowledge_ID.Filter();
                if (new BLL_S_TestQuestions_TP().Delete(TestQuestions_Knowledge_ID))
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