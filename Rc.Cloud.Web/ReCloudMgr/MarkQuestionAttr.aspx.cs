using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Rc.BLL.Resources;
using System.Data;
using System.Text;
using Rc.Model.Resources;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class MarkQuestionAttr : System.Web.UI.Page
    {
        protected string ResourceToResourceFolder_Id = string.Empty;
        protected string GradeTerm = string.Empty;
        protected string Subject = string.Empty;
        protected string Resource_Version = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();
            Model_ResourceToResourceFolder model = new Model_ResourceToResourceFolder();
            model = new BLL_ResourceToResourceFolder().GetModel(ResourceToResourceFolder_Id);
            if (model != null)
            {
                GradeTerm = model.GradeTerm;
                Subject = model.Subject;
                Resource_Version = model.Resource_Version;
                //加载来源
                DataTable dt = new DataTable();
                string sql = string.Format(@" select t.Dict_Id,t3.D_Name,t3.D_Order,t3.D_Type from DictRelation_Detail t 
inner join DictRelation t2 on t2.DictRelation_Id=t.DictRelation_Id
inner join Common_Dict t3 on t3.Common_Dict_Id=t.Dict_Id 
where t2.HeadDict_Id='934A3541-116E-438C-B9BA-4176368FCD9B' and t2.SonDict_Id='F3BB0E09-CF73-4696-84BF-007C30B249A1' and t.Parent_Id='{0}'
order by t3.D_Order", Subject);
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddltq_source, dt, "D_Name", "Dict_Id", "--请选择--");
            }
        }

        /// <summary>
        /// 加载知识点一级数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string ScoreId, string GradeTerm, string Subject, string Resource_Version)
        {
            try
            {
                BLL_S_KnowledgePoint bll = new BLL_S_KnowledgePoint();
                DataTable dt = new DataTable();
                string strWhere = " Parent_Id='0' ";
                if (!string.IsNullOrEmpty(GradeTerm)) strWhere += " and t.GradeTerm='" + GradeTerm.Filter() + "' ";
                if (!string.IsNullOrEmpty(GradeTerm)) strWhere += " and t.Subject='" + Subject.Filter() + "' ";
                if (!string.IsNullOrEmpty(GradeTerm)) strWhere += " and t.Resource_Version='" + Resource_Version.Filter() + "' ";
                string sql = string.Format(@"select t.*,tk.TestQuestions_Knowledge_ID,KPNameBasic from S_KnowledgePoint t
left join S_KnowledgePointBasic t1 on t1.S_KnowledgePointBasic_Id=t.S_KnowledgePointBasic_Id
left join [TestQuestions_Knowledge] tk on tk.S_KnowledgePoint_Id=t.S_KnowledgePoint_Id and TestQuestions_Score_ID='{0}' where " + strWhere + " order by KPCode", ScoreId.Filter());
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                DataTable dtAll = bll.GetList("").Tables[0];
                StringBuilder stbHtml = new StringBuilder();
                stbHtml.Append("<div class=\"panel\"><div class=\"panel-body\">");
                stbHtml.Append("<table class=\"table table-hover table-bordered\">");
                stbHtml.Append("<tbody id=\"tb1\">");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow[] drSub = dtAll.Select("Parent_Id='" + dt.Rows[i]["S_KnowledgePoint_Id"].ToString() + "'");
                        stbHtml.Append("<tr>");
                        stbHtml.AppendFormat("<td onselectstart=\"return false\"");
                        stbHtml.AppendFormat("kpId=\"{0}\" pId=\"{1}\" hasChildren=\"{2}\" data-parentidstr=\"{3}\" data-toggle=\"0\" {4} data-scoreid=\"{5}\">"
                        , dt.Rows[i]["S_KnowledgePoint_Id"]
                         , dt.Rows[i]["Parent_Id"]
                        , drSub.Length
                        , ""
                        , drSub.Length > 0 ? "style=\"cursor:pointer;\"" : ""
                        , ScoreId);
                        stbHtml.AppendFormat(dt.Rows[i]["IsLast"].ToString() == "1" ? "<input type=\"checkbox\" class='che' value=\"{0}\" {1} data-scoreid=\"{2}\" data-tkid=\"{3}\" data-attrname=\"{4}\">" : ""
                            , dt.Rows[i]["S_KnowledgePoint_Id"]
                            , !string.IsNullOrEmpty(dt.Rows[i]["TestQuestions_Knowledge_ID"].ToString()) ? "checked" : ""
                            , ScoreId
                            , !string.IsNullOrEmpty(dt.Rows[i]["TestQuestions_Knowledge_ID"].ToString()) ? dt.Rows[i]["TestQuestions_Knowledge_ID"].ToString() : Guid.NewGuid().ToString()
                            , !string.IsNullOrEmpty(dt.Rows[i]["KPName"].ToString()) ? dt.Rows[i]["KPName"].ToString() : dt.Rows[i]["KPNameBasic"].ToString());
                        stbHtml.AppendFormat("<span style=\"padding-left:{0}px\"></span>", "");
                        stbHtml.AppendFormat("{0}{1}", drSub.Length > 0 ? string.Format("<i class=\"fa fa-plus-square-o\" onclick=\"loadSubData(this,'{0}');\" ></i>&nbsp;&nbsp;", dt.Rows[i]["S_KnowledgePoint_Id"]) : "", !string.IsNullOrEmpty(dt.Rows[i]["KPName"].ToString()) ? dt.Rows[i]["KPName"].ToString() : dt.Rows[i]["KPNameBasic"].ToString());
                        stbHtml.Append("</tr>");
                    }
                }
                else
                {
                    stbHtml.Append("<tr><td class='text-center'>没有可匹配的知识点</td></tr>");
                }
                stbHtml.Append("</tbody></table></div></div>");
                return stbHtml.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 加载考点一级数据
        /// </summary>
        [WebMethod]
        public static string GetDataList1(string ScoreId, string GradeTerm, string Subject)
        {
            try
            {
                BLL_S_TestingPoint bll = new BLL_S_TestingPoint();
                DataTable dt = new DataTable();
                string strWhere = " Parent_Id='0' ";
                if (!string.IsNullOrEmpty(GradeTerm)) strWhere += " and t.GradeTerm='" + GradeTerm.Filter() + "' ";
                if (!string.IsNullOrEmpty(GradeTerm)) strWhere += " and t.Subject='" + Subject.Filter() + "' ";
                string sql = string.Format(@"select t.*,tk.TestQuestions_Knowledge_ID,TPNameBasic from S_TestingPoint t
left join S_TestingPointBasic t1 on t1.S_TestingPointBasic_Id=t.S_TestingPointBasic_Id
left join [TestQuestions_Knowledge] tk on tk.S_KnowledgePoint_Id=t.S_TestingPoint_Id and TestQuestions_Score_ID='{0}' where " + strWhere + " order by TPCode", ScoreId.Filter());
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                DataTable dtAll = bll.GetList("").Tables[0];
                StringBuilder stbHtml = new StringBuilder();
                stbHtml.Append("<div class=\"panel\"><div class=\"panel-body\">");
                stbHtml.Append("<table class=\"table table-hover table-bordered\">");
                stbHtml.Append("<tbody id=\"tb1\">");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow[] drSub = dtAll.Select("Parent_Id='" + dt.Rows[i]["S_TestingPoint_Id"].ToString() + "'");
                        stbHtml.Append("<tr>");
                        stbHtml.AppendFormat("<td onselectstart=\"return false\"");
                        stbHtml.AppendFormat("kpId=\"{0}\" pId=\"{1}\" hasChildren=\"{2}\" data-parentidstr=\"{3}\" data-toggle=\"0\" {4} data-scoreid=\"{5}\">"
                        , dt.Rows[i]["S_TestingPoint_Id"]
                         , dt.Rows[i]["Parent_Id"]
                        , drSub.Length
                        , ""
                        , drSub.Length > 0 ? "style=\"cursor:pointer;\"" : ""
                        , ScoreId);
                        stbHtml.AppendFormat(dt.Rows[i]["IsLast"].ToString() == "1" ? "<input type=\"checkbox\" class='che' value=\"{0}\" {1} data-scoreid=\"{2}\" data-tkid=\"{3}\" data-attrname=\"{4}\">" : ""
                            , dt.Rows[i]["S_TestingPoint_Id"]
                            , !string.IsNullOrEmpty(dt.Rows[i]["TestQuestions_Knowledge_ID"].ToString()) ? "checked" : ""
                            , ScoreId
                            , !string.IsNullOrEmpty(dt.Rows[i]["TestQuestions_Knowledge_ID"].ToString()) ? dt.Rows[i]["TestQuestions_Knowledge_ID"].ToString() : Guid.NewGuid().ToString()
                            , !string.IsNullOrEmpty(dt.Rows[i]["TPName"].ToString()) ? dt.Rows[i]["TPName"].ToString() : dt.Rows[i]["TPNameBasic"].ToString());
                        stbHtml.AppendFormat("<span style=\"padding-left:{0}px\"></span>", "");
                        stbHtml.AppendFormat("{0}{1}", drSub.Length > 0 ? string.Format("<i class=\"fa fa-plus-square-o\" onclick=\"loadSubData1(this,'{0}');\" ></i>&nbsp;&nbsp;", dt.Rows[i]["S_TestingPoint_Id"]) : "", !string.IsNullOrEmpty(dt.Rows[i]["TPName"].ToString()) ? dt.Rows[i]["TPName"].ToString() : dt.Rows[i]["TPNameBasic"].ToString());
                        stbHtml.Append("</tr>");
                    }
                }
                else
                {
                    stbHtml.Append("<tr><td class='text-center'>没有可匹配的考点</td></tr>");
                }
                stbHtml.Append("</tbody></table></div></div>");
                return stbHtml.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 加载知识点子集
        /// </summary>
        /// <param name="ScoreId"></param>
        /// <param name="parentId"></param>
        /// <param name="parentIdStr"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetSubDataList(string ScoreId, string parentId, string parentIdStr)
        {
            try
            {
                BLL_S_KnowledgePoint bll = new BLL_S_KnowledgePoint();
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " where  Parent_Id='" + parentId + "'";
                string sql = string.Format(@"select t.*,tk.TestQuestions_Knowledge_ID,KPNameBasic from S_KnowledgePoint t
left join S_KnowledgePointBasic t1 on t1.S_KnowledgePointBasic_Id=t.S_KnowledgePointBasic_Id
left join [TestQuestions_Knowledge] tk on tk.S_KnowledgePoint_Id=t.S_KnowledgePoint_Id and TestQuestions_Score_ID='{0}' " + strWhere + " order by KPCode", ScoreId);
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                DataTable dtAll = bll.GetList("").Tables[0];
                StringBuilder stbHtml = new StringBuilder();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow[] drSub = dtAll.Select("Parent_Id='" + dt.Rows[i]["S_KnowledgePoint_Id"].ToString() + "'");
                    stbHtml.Append("<tr>");
                    stbHtml.AppendFormat("<td onselectstart=\"return false\"");
                    stbHtml.AppendFormat("kpId=\"{0}\" pId=\"{1}\" hasChildren=\"{2}\" data-scoreid=\"{5}\" data-parentidstr=\"{3}\" data-toggle=\"0\" {4}>"
                    , dt.Rows[i]["S_KnowledgePoint_Id"]
                     , dt.Rows[i]["Parent_Id"]
                    , drSub.Length
                    , parentIdStr
                    , drSub.Length > 0 ? "style=\"cursor:pointer;\"" : ""
                    , ScoreId);
                    stbHtml.AppendFormat("<span style=\"padding-left:{0}px\"></span>", 15 * (parentIdStr.Split('&').Length - 1));
                    stbHtml.AppendFormat(dt.Rows[i]["IsLast"].ToString() == "1" ? "<input type=\"checkbox\" class='che' value=\"{0}\" {1} data-scoreid=\"{2}\" data-tkid=\"{3}\" data-attrname=\"{4}\">&nbsp;&nbsp;" : ""
                        , dt.Rows[i]["S_KnowledgePoint_Id"]
                        , !string.IsNullOrEmpty(dt.Rows[i]["TestQuestions_Knowledge_ID"].ToString()) ? "checked" : ""
                        , ScoreId
                        , !string.IsNullOrEmpty(dt.Rows[i]["TestQuestions_Knowledge_ID"].ToString()) ? dt.Rows[i]["TestQuestions_Knowledge_ID"].ToString() : Guid.NewGuid().ToString()
                        , !string.IsNullOrEmpty(dt.Rows[i]["KPName"].ToString()) ? dt.Rows[i]["KPName"].ToString() : dt.Rows[i]["KPNameBasic"].ToString());
                    stbHtml.AppendFormat("{0}{1}", drSub.Length > 0 ? string.Format("<i class=\"fa fa-plus-square-o\" onclick=\"loadSubData(this,'{0}');\" ></i>&nbsp;&nbsp;", dt.Rows[i]["S_KnowledgePoint_Id"]) : "", !string.IsNullOrEmpty(dt.Rows[i]["KPName"].ToString()) ? dt.Rows[i]["KPName"].ToString() : dt.Rows[i]["KPNameBasic"].ToString());
                    stbHtml.Append("</tr>");
                }
                return stbHtml.ToString();


            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 加载考点子集
        /// </summary>
        /// <param name="ScoreId"></param>
        /// <param name="parentId"></param>
        /// <param name="parentIdStr"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetSubDataList1(string ScoreId, string parentId, string parentIdStr)
        {
            try
            {
                BLL_S_TestingPoint bll = new BLL_S_TestingPoint();
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " where  Parent_Id='" + parentId + "'";
                string sql = string.Format(@"select t.*,tk.TestQuestions_Knowledge_ID,TPNameBasic from S_TestingPoint t
left join S_TestingPointBasic t1 on t1.S_TestingPointBasic_Id=t.S_TestingPointBasic_Id
left join [TestQuestions_Knowledge] tk on tk.S_KnowledgePoint_Id=t.S_TestingPoint_Id and TestQuestions_Score_ID='{0}' " + strWhere + " order by TPCode", ScoreId);
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                DataTable dtAll = bll.GetList("").Tables[0];
                StringBuilder stbHtml = new StringBuilder();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow[] drSub = dtAll.Select("Parent_Id='" + dt.Rows[i]["S_TestingPoint_Id"].ToString() + "'");
                    stbHtml.Append("<tr>");
                    stbHtml.AppendFormat("<td onselectstart=\"return false\"");
                    stbHtml.AppendFormat("kpId=\"{0}\" pId=\"{1}\" hasChildren=\"{2}\" data-scoreid=\"{5}\" data-parentidstr=\"{3}\" data-toggle=\"0\" {4}>"
                    , dt.Rows[i]["S_TestingPoint_Id"]
                     , dt.Rows[i]["Parent_Id"]
                    , drSub.Length
                    , parentIdStr
                    , drSub.Length > 0 ? "style=\"cursor:pointer;\"" : ""
                    , ScoreId);
                    stbHtml.AppendFormat("<span style=\"padding-left:{0}px\"></span>", 15 * (parentIdStr.Split('&').Length - 1));
                    stbHtml.AppendFormat(dt.Rows[i]["IsLast"].ToString() == "1" ? "<input type=\"checkbox\" class='che' value=\"{0}\" {1} data-scoreid=\"{2}\" data-tkid=\"{3}\" data-attrname=\"{4}\">&nbsp;&nbsp;" : ""
                        , dt.Rows[i]["S_TestingPoint_Id"]
                        , !string.IsNullOrEmpty(dt.Rows[i]["TestQuestions_Knowledge_ID"].ToString()) ? "checked" : ""
                        , ScoreId
                        , !string.IsNullOrEmpty(dt.Rows[i]["TestQuestions_Knowledge_ID"].ToString()) ? dt.Rows[i]["TestQuestions_Knowledge_ID"].ToString() : Guid.NewGuid().ToString()
                        , !string.IsNullOrEmpty(dt.Rows[i]["TPName"].ToString()) ? dt.Rows[i]["TPName"].ToString() : dt.Rows[i]["TPNameBasic"].ToString());
                    stbHtml.AppendFormat("{0}{1}", drSub.Length > 0 ? string.Format("<i class=\"fa fa-plus-square-o\" onclick=\"loadSubData1(this,'{0}');\" ></i>&nbsp;&nbsp;", dt.Rows[i]["S_TestingPoint_Id"]) : "", !string.IsNullOrEmpty(dt.Rows[i]["TPName"].ToString()) ? dt.Rows[i]["TPName"].ToString() : dt.Rows[i]["TPNameBasic"].ToString());
                    stbHtml.Append("</tr>");
                }
                return stbHtml.ToString();


            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 加载知识点
        /// </summary>
        /// <param name="ScoreId"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string LoadAtrr(string ScoreId, string ResourceToResourceFolder_Id)
        {
            try
            {
                string str = string.Empty;
                string sql = string.Format(@"select TestQuestions_Score_ID,TestQuestions_Knowledge_ID,KPName,KPNameBasic,tk.CreateUser from [TestQuestions_Knowledge] tk
inner join [dbo].[S_KnowledgePoint] sp on sp.S_KnowledgePoint_Id=tk.S_KnowledgePoint_Id
left join S_KnowledgePointBasic t on t.S_KnowledgePointBasic_Id=sp.S_KnowledgePointBasic_Id where TestQuestions_Score_ID='{0}' and ResourceToResourceFolder_Id='{1}' order by tk.CreateTime desc,KPName", ScoreId, ResourceToResourceFolder_Id);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        str += string.Format("<span class=\"tag\">{0}<i data-name=\"removeKnowledge\" data-tkid=\"{1}\">×</i></span>", item["KPNameBasic"], item["TestQuestions_Knowledge_ID"]);
                    }
                    return str;
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
        /// 加载考点
        /// </summary>
        /// <param name="ScoreId"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string LoadTest(string ScoreId, string ResourceToResourceFolder_Id)
        {
            try
            {
                string str = string.Empty;
                string sql = string.Format(@"select TestQuestions_Score_ID,TestQuestions_Knowledge_ID,TPName,TPNameBasic,tk.CreateUser from [TestQuestions_Knowledge] tk
inner join [dbo].[S_TestingPoint] sp on sp.S_TestingPoint_Id=tk.S_KnowledgePoint_Id
left join S_TestingPointBasic t on t.S_TestingPointBasic_Id=sp.S_TestingPointBasic_Id where TestQuestions_Score_ID='{0}' and ResourceToResourceFolder_Id='{1}' order by tk.CreateTime desc,TPName", ScoreId, ResourceToResourceFolder_Id);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        str += string.Format("<span class=\"tag\">{0}<i data-name=\"removeKnowledge\" data-tkid=\"{1}\">×</i></span>", item["TPNameBasic"], item["TestQuestions_Knowledge_ID"]);
                    }
                    return str;
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
                if (new BLL_S_TestQuestions_KP().Delete(TestQuestions_Knowledge_ID))
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
        /// 删除考点
        /// </summary>
        /// <param name="TestQuestions_Knowledge_ID"></param>
        /// <returns></returns>
        [WebMethod]
        public static string DeleteTest(string TestQuestions_Knowledge_ID)
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
        /// <summary>
        /// 异步获取知识点
        /// </summary>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <param name="ScoreId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetQuestionAttr(string ResourceToResourceFolder_Id, string ScoreId)
        {
            try
            {
                StringBuilder stbHtml = new StringBuilder();
                Model_ResourceToResourceFolder model = new Model_ResourceToResourceFolder();
                model = new BLL_ResourceToResourceFolder().GetModel(ResourceToResourceFolder_Id);
                string sqlAttr = @"select TestQuestions_Score_ID,S_TestQuestions_KP_Id,KPName,KPNameBasic,tk.CreateTime from [S_TestQuestions_KP] tk
inner join [dbo].[S_KnowledgePoint] sp on sp.S_KnowledgePoint_Id=tk.S_KnowledgePoint_Id
left join S_KnowledgePointBasic t on t.S_KnowledgePointBasic_Id=sp.S_KnowledgePointBasic_Id
where  TestQuestions_Score_ID='" + ScoreId + "'";
                System.Data.DataTable dtAttr = Rc.Common.DBUtility.DbHelperSQL.Query(sqlAttr).Tables[0];
                System.Data.DataRow[] drAttr = dtAttr.Select("TestQuestions_Score_ID='" + ScoreId + "'", "CreateTime desc,KPName");
                if (drAttr.Length > 0)
                {
                    foreach (var item in drAttr)
                    {
                        stbHtml.AppendFormat("<span class=\"tag\">{0}<i data-name=\"removeKnowledge\" data-tkid=\"{1}\">×</i></span>", item["KPNameBasic"], item["S_TestQuestions_KP_Id"]);
                    }
                }
                if (model != null)
                {
                    stbHtml.AppendFormat("<span class=\"tag_add\" data-name=\"addKnowledgeNew\" data-scoreid=\"{0}\" data-g=\"{1}\" data-s=\"{2}\" data-v=\"{3}\" data-type=\"1\">+</span>"
                           , ScoreId
                           , model.GradeTerm
                           , model.Subject
                           , model.Resource_Version);

                }
                return stbHtml.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        /// <summary>
        /// 异步获取知识点
        /// </summary>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <param name="ScoreId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetQuestionTest(string ResourceToResourceFolder_Id, string ScoreId)
        {
            try
            {
                StringBuilder stbHtml = new StringBuilder();
                Model_ResourceToResourceFolder model = new Model_ResourceToResourceFolder();
                model = new BLL_ResourceToResourceFolder().GetModel(ResourceToResourceFolder_Id);
                string sqlTest = @"select TestQuestions_Score_ID,S_TestQuestions_TP_ID,TPNameBasic,TPName,tk.CreateTime from [S_TestQuestions_TP] tk
inner join [dbo].[S_TestingPoint] sp on sp.S_TestingPoint_Id=tk.S_TestingPoint_Id
left join S_TestingPointBasic t on t.S_TestingPointBasic_Id=sp.S_TestingPointBasic_Id
where  TestQuestions_Score_ID='" + ScoreId + "'";
                System.Data.DataTable dtTest = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTest).Tables[0];
                System.Data.DataRow[] drAttr = dtTest.Select("TestQuestions_Score_ID='" + ScoreId + "'", "CreateTime desc,TPName");
                if (drAttr.Length > 0)
                {
                    foreach (var item in drAttr)
                    {
                        stbHtml.AppendFormat("<span class=\"tag\">{0}<i data-name=\"removeTest\" data-tkid=\"{1}\">×</i></span>", item["TPNameBasic"], item["S_TestQuestions_TP_ID"]);
                    }
                }
                if (model != null)
                {
                    stbHtml.AppendFormat("<span class=\"tag_add\" data-name=\"addTestNew\" data-scoreid=\"{0}\" data-g=\"{1}\" data-s=\"{2}\" data-v=\"{3}\" data-type=\"1\">+</span>"
                           , ScoreId
                           , model.GradeTerm
                           , model.Subject
                           , model.Resource_Version);

                }
                return stbHtml.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 更新试卷所有空的来源
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ChangeAllTQScoreSource(string source, string ResourceToResourceFolder_Id)
        {
            try
            {
                Rc.Cloud.Model.Model_VSysUserRole loginUser = (Rc.Cloud.Model.Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                source = source.Filter();
                string sql = string.Empty;
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();
                if (source == "-1")
                {
                    sql = string.Format(@"delete from S_TQ_Score_AttrExtend where AttrEnum='{0}' and ResourceToResourceFolder_Id='{1}'", TQ_Score_AttrExtend.Source, ResourceToResourceFolder_Id);
                }
                else
                {
                    sql = string.Format(@"delete from S_TQ_Score_AttrExtend where AttrEnum='{0}' and ResourceToResourceFolder_Id='{3}';insert into S_TQ_Score_AttrExtend(S_TQ_Score_Attr_Id,ResourceToResourceFolder_Id,TestQuestions_Id,TestQuestions_Score_Id,AttrEnum,Attr_Value,CreateUser,CreateTime,UpdateUser,UpdateTime)
select newid(),ResourceToResourceFolder_Id,TestQuestions_Id,TestQuestions_Score_ID,'{0}','{1}','{2}',getdate(),'{2}',getdate() from TestQuestions_Score where ResourceToResourceFolder_Id='{3}'", TQ_Score_AttrExtend.Source, source, loginUser.SysUser_ID, ResourceToResourceFolder_Id);
                }
                int row = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql);
                if (row > 0)
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
        /// 更改单个空的来源
        /// </summary>
        /// <param name="scoreId"></param>
        /// <param name="Source"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ChangeSource(string scoreId, string tqId, string Source, string ResourceToResourceFolder_Id)
        {
            try
            {
                Rc.Cloud.Model.Model_VSysUserRole loginUser = (Rc.Cloud.Model.Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                scoreId = scoreId.Filter();
                Source = Source.Filter();
                tqId = tqId.Filter();
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();
                string sql = string.Empty;
                if (Source == "0")//删除来源
                {
                    sql = string.Format(@" delete from S_TQ_Score_AttrExtend where AttrEnum='{0}' and TestQuestions_Score_Id='{1}' and ResourceToResourceFolder_Id='{2}' and TestQuestions_Id='{3}';", TQ_Score_AttrExtend.Source.ToString(), scoreId, ResourceToResourceFolder_Id, tqId);
                }
                else
                {
                    sql = string.Format(@" delete from S_TQ_Score_AttrExtend where AttrEnum='{0}' and TestQuestions_Score_Id='{1}' and ResourceToResourceFolder_Id='{2}'and TestQuestions_Id='{3}';", TQ_Score_AttrExtend.Source.ToString(), scoreId, ResourceToResourceFolder_Id, tqId);
                    sql += string.Format(@"insert into S_TQ_Score_AttrExtend values(newId(),'{0}','{1}','{2}','{3}','{4}','{5}',getdate(),'{5}',getdate())"
                                         , ResourceToResourceFolder_Id
                                         , tqId
                                         , scoreId
                                         , TQ_Score_AttrExtend.Source.ToString()
                                         , Source
                                         , loginUser.SysUser_ID
                                         );
                }
                Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql);
                return "1";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 更改单个空难易度、更新题的最大难易度
        /// </summary>
        /// <param name="scoreId"></param>
        /// <param name="tqId"></param>
        /// <param name="Source"></param>
        /// <param name="ResourceToResourceFolder_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ChangeComplexityText(string scoreId, string tqId, string Source, string ResourceToResourceFolder_Id)
        {
            try
            {
                Model_TestQuestions tq = new BLL_TestQuestions().GetModel(tqId);

                Rc.Cloud.Model.Model_VSysUserRole loginUser = (Rc.Cloud.Model.Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                scoreId = scoreId.Filter();
                Source = Source.Filter();
                tqId = tqId.Filter();
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();
                string sql = string.Empty;
                if (tq.type == "simple")
                {
                    if (Source == "0")//删除空和题的难易度
                    {
                        sql = string.Format(@" delete from S_TQ_Score_AttrExtend where AttrEnum='{0}' and TestQuestions_Score_Id='{1}' and ResourceToResourceFolder_Id='{2}' and TestQuestions_Id='{3}'", TQ_Score_AttrExtend.ComplexityText.ToString(), scoreId, ResourceToResourceFolder_Id, tqId);
                        sql += string.Format(@"delete from S_TQ_AttrExtend where AttrEnum='{0}' and ResourceToResourceFolder_Id='{1}' and TestQuestions_Id='{2}';", TQ_Score_AttrExtend.ComplexityText.ToString(), ResourceToResourceFolder_Id, tqId);
                    }
                    else
                    {
                        sql = string.Format(@" delete from S_TQ_Score_AttrExtend where AttrEnum='{0}' and TestQuestions_Score_Id='{1}' and ResourceToResourceFolder_Id='{2}' and TestQuestions_Id='{3}'", TQ_Score_AttrExtend.ComplexityText.ToString(), scoreId, ResourceToResourceFolder_Id, tqId);
                        sql += string.Format(@"delete from S_TQ_AttrExtend where AttrEnum='{0}' and ResourceToResourceFolder_Id='{1}' and TestQuestions_Id='{2}';", TQ_Score_AttrExtend.ComplexityText.ToString(), ResourceToResourceFolder_Id, tqId);
                        sql += string.Format(@"insert into S_TQ_Score_AttrExtend values(newId(),'{0}','{1}','{2}','{3}','{4}','{5}',getdate(),'{5}',getdate())"
                                             , ResourceToResourceFolder_Id
                                             , tqId
                                             , scoreId
                                             , TQ_Score_AttrExtend.ComplexityText.ToString()
                                             , Source
                                             , loginUser.SysUser_ID
                                             );
                        sql += string.Format(@"insert into S_TQ_AttrExtend values(newid(),'{0}','{1}','{2}',(select top 1 Common_Dict_ID  from S_TQ_Score_AttrExtend t
left join Common_Dict cd on cd.Common_Dict_ID=t.Attr_Value 
where AttrEnum='{2}' and TestQuestions_Id='{1}'  order by d_order desc),'{3}','{4}',getdate(),'{4}',getdate())"
                                  , ResourceToResourceFolder_Id
                                             , tqId
                                             , TQ_Score_AttrExtend.ComplexityText.ToString()
                                             , ""
                                             , loginUser.SysUser_ID);
                    }
                }
                else
                {
                    if (Source == "0")//删除空和题的难易度
                    {
                        sql = string.Format(@" delete from S_TQ_Score_AttrExtend where AttrEnum='{0}' and TestQuestions_Score_Id='{1}' and ResourceToResourceFolder_Id='{2}' and TestQuestions_Id='{3}'", TQ_Score_AttrExtend.ComplexityText.ToString(), scoreId, ResourceToResourceFolder_Id, tqId);
                        sql += string.Format(@"delete from S_TQ_AttrExtend where AttrEnum='{0}' and ResourceToResourceFolder_Id='{1}' and TestQuestions_Id='{2}';", TQ_Score_AttrExtend.ComplexityText.ToString(), ResourceToResourceFolder_Id, tq.Parent_Id);
                    }
                    else
                    {
                        sql = string.Format(@" delete from S_TQ_Score_AttrExtend where AttrEnum='{0}' and TestQuestions_Score_Id='{1}' and ResourceToResourceFolder_Id='{2}' and TestQuestions_Id='{3}'", TQ_Score_AttrExtend.ComplexityText.ToString(), scoreId, ResourceToResourceFolder_Id, tqId);
                        sql += string.Format(@"delete from S_TQ_AttrExtend where AttrEnum='{0}' and ResourceToResourceFolder_Id='{1}' and TestQuestions_Id='{2}';", TQ_Score_AttrExtend.ComplexityText.ToString(), ResourceToResourceFolder_Id, tq.Parent_Id);
                        sql += string.Format(@"insert into S_TQ_Score_AttrExtend values(newId(),'{0}','{1}','{2}','{3}','{4}','{5}',getdate(),'{5}',getdate())"
                                             , ResourceToResourceFolder_Id
                                             , tqId
                                             , scoreId
                                             , TQ_Score_AttrExtend.ComplexityText.ToString()
                                             , Source
                                             , loginUser.SysUser_ID
                                             );
                        sql += string.Format(@"insert into S_TQ_AttrExtend values(newid(),'{0}','{1}','{2}',(select top 1 Common_Dict_ID  from S_TQ_Score_AttrExtend t
left join Common_Dict cd on cd.Common_Dict_ID=t.Attr_Value 
where AttrEnum='{2}' and TestQuestions_Id in (select TestQuestions_Id from TestQuestions where Parent_Id='{1}')  order by d_order desc),'{3}','{4}',getdate(),'{4}',getdate())"
                                  , ResourceToResourceFolder_Id
                                             , tq.Parent_Id
                                             , TQ_Score_AttrExtend.ComplexityText.ToString()
                                             , ""
                                             , loginUser.SysUser_ID);
                    }
                }
                Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql);
                return "1";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 更改试题类型
        /// </summary>
        /// <param name="scoreId"></param>
        /// <param name="Source"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ChangeTQ_Type(string tqId, string TQ_Type, string ResourceToResourceFolder_Id)
        {
            try
            {
                Rc.Cloud.Model.Model_VSysUserRole loginUser = (Rc.Cloud.Model.Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];

                TQ_Type = TQ_Type.Filter();
                tqId = tqId.Filter();
                ResourceToResourceFolder_Id = ResourceToResourceFolder_Id.Filter();
                string sql = string.Empty;
                if (TQ_Type == "0")//删除来源
                {
                    sql = string.Format(@" delete from S_TQ_AttrExtend where AttrEnum='{0}' and  ResourceToResourceFolder_Id='{1}' and TestQuestions_Id='{2}';", TQ_AttrExtend.TQ_Type.ToString(), ResourceToResourceFolder_Id, tqId);
                }
                else
                {
                    sql = string.Format(@" delete from S_TQ_AttrExtend where AttrEnum='{0}' and  ResourceToResourceFolder_Id='{1}' and TestQuestions_Id='{2}';", TQ_AttrExtend.TQ_Type.ToString(), ResourceToResourceFolder_Id, tqId);
                    sql += string.Format(@"insert into S_TQ_AttrExtend values(newId(),'{0}','{1}','{2}','{3}','{4}','{5}',getdate(),'{5}',getdate())"
                                         , ResourceToResourceFolder_Id
                                         , tqId
                                         , TQ_AttrExtend.TQ_Type.ToString()
                                         , TQ_Type
                                         , ""
                                         , loginUser.SysUser_ID
                                         );
                }
                Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql);
                return "1";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}