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
using Rc.BLL.Resources;
using System.Text;
using Rc.Cloud.Web.Common;
namespace Rc.Cloud.Web.teacher
{
    public partial class simpleTestPaper : Rc.Cloud.Web.Common.FInitData
    {
        public string Two_WayChecklist_Id = string.Empty;
        public string strUserGroup_IdActivity = string.Empty;
        public string TestPaper_Frame_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Rc.Common.ConfigHelper.GetConfigBool("IsShowTestAssembly"))
            //{
            //    testPaper.Visible = true;
            //    testPaper2.Visible = true;
            //}
            if (pfunction.GetWebMdlIsShow("ChapterAssembly"))
            {
                testPaper.Visible = true;
                testPaper2.Visible = true;
                cptestPaper.Visible = true;
                cptestPaper2.Visible = true;
            }
            if (pfunction.GetWebMdlIsShow("Two_WayChecklist"))
            {
                testPaper.Visible = true;
                testPaper2.Visible = true;
                twtestPaper.Visible = true;
                twtestPaper2.Visible = true;
            }
            if (pfunction.GetWebMdlIsShow("pHomework")) apHomework.Visible = true;

            strUserGroup_IdActivity = Request["ugid"].Filter();
            if (!IsPostBack)
            {
                string StrSql = @" 	select * from [dbo].[Two_WayChecklist] t1
	left join [dbo].[Two_WayChecklistToTeacher] t2 on t2.Two_WayChecklist_Id=t1.Two_WayChecklist_Id and status='1' and t2.Teacher_Id='" + FloginUser.UserId + @"'
    inner join Two_WayChecklistAuth twoAu on twoAu.Two_WayChecklist_Id=t1.Two_WayChecklist_Id and User_Id ='" + FloginUser.UserId + @"'
	where t1.Two_WayChecklistType<>'1' order by t1.CreateTime,t1.Two_WayChecklist_Name";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(StrSql).Tables[0];
                Two_WayChecklist_Id = GetTwo_WayChecklistOne(dt);
                if (!String.IsNullOrEmpty(Request["Two_WayChecklist_Id"]))
                {
                    Two_WayChecklist_Id = Request["Two_WayChecklist_Id"].ToString().Trim().Filter();
                }
                LoadData(dt);
            }
        }
        /// <summary>
        /// 获取双向细目表
        /// </summary>
        /// <param name="dt"></param>
        private void LoadData(DataTable dt)
        {
            try
            {
                StringBuilder stbHtml = new StringBuilder();
                //string Temp = "<li><div class=\"name {2}\"><a href='##' tt=\"{3}\" tf_id=\"{4}\" onclick=\"ShowList(this,'{0}')\">{1}</a></div></li>";
                string Temp = "";
                Temp += "<li>";
                Temp += "<div tt='{3}' tf_id='{4}' data-id='{0}' class='mtree_link mtree-link-hook {2}'>";
                Temp += "<div class='mtree_indent mtree-indent-hook'></div>";
                Temp += "<div class='mtree_btn mtree-btn-hook'></div>";
                Temp += "<div class='mtree_name mtree-name-hook'>{1}</div>";
                Temp += "</div>";
                Temp += "</li>";

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        if (string.IsNullOrEmpty(item["Two_WayChecklistToTeacher_Id"].ToString()))//添加
                        {
                            stbHtml.AppendFormat(Temp
                                , item["Two_WayChecklist_Id"].ToString()
                                , item["Two_WayChecklist_Name"].ToString()
                                , item["Two_WayChecklist_Id"].ToString() == Two_WayChecklist_Id ? "active" : ""
                                , "1"
                                , item["ParentId"].ToString());
                        }
                        else//修改
                        {
                            stbHtml.AppendFormat(Temp
                                , item["Two_NewWayChecklist_Id"].ToString()
                                , item["Two_WayChecklist_Name"].ToString()
                                , item["Two_NewWayChecklist_Id"].ToString() == Two_WayChecklist_Id ? "active" : ""
                                , "2"
                                , item["ParentId"].ToString());
                        }
                    }
                    this.ltlTwo_WayChecklist.Text = stbHtml.ToString();
                }
            }
            catch (Exception ex)
            {
                this.ltlTwo_WayChecklist.Text = "加载失败";
            }
        }
        /// <summary>
        /// 获取双向细目表第一个选中ID
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string GetTwo_WayChecklistOne(DataTable dt)
        {
            string strTemp = string.Empty;
            if (dt.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(dt.Rows[0]["Two_NewWayChecklist_Id"].ToString()))
                {
                    strTemp = dt.Rows[0]["Two_WayChecklist_Id"].ToString();
                }
                else
                {
                    strTemp = dt.Rows[0]["Two_NewWayChecklist_Id"].ToString();

                }
            }
            return strTemp;
        }
        /// <summary>
        /// 获取双向细目表详情
        /// </summary>
        /// <param name="Two_WayChecklist_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetTwo_WayChecklist(string Two_WayChecklist_Id)
        {
            try
            {

                #region 双向细目表
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                strSql = @"select t.*,t4.Analysis,cd.D_Name from Two_WayChecklistDetail t 
inner join Two_WayChecklistDetail t2 on t.ParentId=t2.Two_WayChecklistDetail_Id 
left join Two_WayChecklist t3 on t3.Two_WayChecklist_Id=t.Two_WayChecklist_Id
inner join TestPaper_Frame t4 on t3.ParentId=t4.TestPaper_Frame_Id 
inner join TestPaper_FrameDetail t5 on t5.TestPaper_FrameDetail_Id=t.TestPaper_FrameDetail_Id
left join Common_Dict cd on cd.Common_Dict_ID=t5.TestQuestionType_Web 
where t.ParentId!='0' and t.Two_WayChecklist_Id='" + Two_WayChecklist_Id + "' order by t2.TestQuestions_Num,t2.CreateTime,t.TestQuestions_Num,t.CreateTime";
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int inum = 0;
                string Analysis = dt.Rows[0]["Analysis"].ToString();

                DataTable dtAttr = new BLL_Two_WayChecklistDetailToAttr().GetList("Two_WayChecklist_Id='" + Two_WayChecklist_Id + "'").Tables[0];
                DataTable dtTQCount = Rc.Common.DBUtility.DbHelperSQL.Query(@"select Two_WayChecklistDetail_Id,count(1) as icount from Two_WayChecklistDetailToTestQuestions
where Two_WayChecklist_Id='" + Two_WayChecklist_Id + "' group by Two_WayChecklistDetail_Id").Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        TestQuestions_NumStr = dt.Rows[i]["TestQuestions_NumStr"].ToString(),
                        TestQuestions_Type = dt.Rows[i]["D_Name"].ToString(),
                        TargetText = GetAttr(dtAttr, dt.Rows[i]["Two_WayChecklistDetail_Id"].ToString(), "2").TrimEnd(','),
                        KnowledgePoint = GetAttr(dtAttr, dt.Rows[i]["Two_WayChecklistDetail_Id"].ToString(), "1").TrimEnd(','),
                        ComplexityText = GetAttr(dtAttr, dt.Rows[i]["Two_WayChecklistDetail_Id"].ToString(), "3").TrimEnd(','),
                        Score = dt.Rows[i]["Score"].ToString().clearLastZero(),
                        SumQuestions = GetQuestions(dtTQCount, dt.Rows[i]["Two_WayChecklistDetail_Id"].ToString()),

                    });
                }
                #endregion

                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        list = listReturn,
                        Analysis = Analysis
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
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }
        /// <summary>
        /// 获取知识点，测量目标，难易度名称
        /// </summary>
        /// <param name="Two_WayChecklist_Id"></param>
        /// <param name="Two_WayChecklistDetail_Id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAttr(DataTable dtAttr, string Two_WayChecklistDetail_Id, string type)
        {
            try
            {
                StringBuilder stbHtml = new StringBuilder();
                //string sql = string.Format(@" select * from Two_WayChecklistDetailToAttr where Two_WayChecklist_Id='{0}' and  Two_WayChecklistDetail_Id='{1}' and Attr_Type='{2}'"
                //   , Two_WayChecklist_Id.Filter()
                //   , Two_WayChecklistDetail_Id.Filter()
                //   , type.Filter());
                //DataTable dtAttr = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                DataRow[] drAttr = dtAttr.Select("Two_WayChecklistDetail_Id='" + Two_WayChecklistDetail_Id + "' and Attr_Type='" + type + "'", "Attr_Value");
                if (drAttr.Length > 0)
                {
                    foreach (DataRow item in drAttr)
                    {
                        if (type == "3")
                        {
                            stbHtml.Append(item["Attr_Value"].ToString() + ",");
                        }
                        else
                        {
                            stbHtml.Append(item["Attr_Value"].ToString() + "</br>");
                        }
                    }
                    return stbHtml.ToString();
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
        /// 获得题的数量
        /// </summary>
        /// <param name="Two_WayChecklistDetail_Id"></param>
        /// <returns></returns>
        public static string GetQuestions(DataTable dtTQCount, string Two_WayChecklistDetail_Id)
        {
            try
            {
                Two_WayChecklistDetail_Id = Two_WayChecklistDetail_Id.Filter();
                //int i = new BLL_Two_WayChecklistDetailToTestQuestions().GetRecordCount("Two_WayChecklistDetail_Id='" + Two_WayChecklistDetail_Id + "'");
                DataRow[] drCount = dtTQCount.Select("Two_WayChecklistDetail_Id='" + Two_WayChecklistDetail_Id + "'");
                if (drCount.Length > 0)
                {
                    return Convert.ToInt32(drCount[0]["icount"]).ToString();

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

        /// <summary>
        /// 恢复默认双向细目表
        /// </summary>
        /// <param name="Two_NewWayChecklist_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string RenewCustom(string Two_NewWayChecklist_Id)
        {
            try
            {
                string Two_WayChecklistToTeacher_Id = string.Empty;
                string Two_WayChecklist_Id = string.Empty;
                Rc.Model.Resources.Model_F_User loginUser = (Rc.Model.Resources.Model_F_User)HttpContext.Current.Session["FLoginUser"];
                string sql = @"select * from [Two_WayChecklistToTeacher] where Two_NewWayChecklist_Id='" + Two_NewWayChecklist_Id.Filter() + "' and Teacher_Id='" + loginUser.UserId + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Two_WayChecklistToTeacher_Id = dt.Rows[0]["Two_WayChecklistToTeacher_Id"].ToString();
                    Two_WayChecklist_Id = dt.Rows[0]["Two_WayChecklist_Id"].ToString();
                    #region 删除老师自定义双向细目表数据
                    string DSql = string.Format(@"update Two_WayChecklistToTeacher set status='0' where Two_WayChecklistToTeacher_Id='{0}';
                                                  delete from Two_WayChecklistDetailToAttr where Two_WayChecklist_Id='{1}';
                                                  delete from Two_WayChecklistDetailToTestQuestions where Two_WayChecklist_Id='{1}';
                                                  delete from Two_WayChecklistDetail where Two_WayChecklist_Id='{1}';
                                                  delete from Two_WayChecklist where Two_WayChecklist_Id='{1}';"
                                                , Two_WayChecklistToTeacher_Id
                                                , Two_NewWayChecklist_Id);
                    #endregion
                    if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(DSql) > 0)
                    {
                        return Two_WayChecklist_Id;
                    }
                    else
                    {
                        return "";
                    }
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