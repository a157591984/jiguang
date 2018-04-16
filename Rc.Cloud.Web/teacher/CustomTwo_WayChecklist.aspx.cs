using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using System.Text;
using System.Data;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
namespace Rc.Cloud.Web.teacher
{
    public partial class CustomTwo_WayChecklist : Rc.Cloud.Web.Common.FInitData
    {
        public string Two_WayChecklist_Id = string.Empty;
        public string Two_WayChecklist_Name = string.Empty;
        public string OperationType = string.Empty;//Add 添加数据 //Update 修改
        public string TestPaper_Frame_Id = string.Empty; //试卷结构标识
        public string strUserGroup_IdActivity = string.Empty;
        public string TWGuid = string.Empty;
        public string IsAdd = string.Empty;
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

            strUserGroup_IdActivity = Request.QueryString["strUserGroup_IdActivity"].Filter();
            Two_WayChecklist_Id = Request.QueryString["Two_WayChecklist_Id"].Filter();
            Two_WayChecklist_Name = Server.UrlDecode(Request.QueryString["Two_WayChecklist_Name"].Filter());
            OperationType = Request.QueryString["OperationType"].Filter();
            TestPaper_Frame_Id = Request.QueryString["TestPaper_Frame_Id"].Filter();

            if (!IsPostBack)
            {
                if (OperationType == "Add")
                {
                    string sql = "select * from Two_WayChecklistToTeacher where Two_WayChecklist_Id='" + Two_WayChecklist_Id + "' and  status='1' and Teacher_Id='" + FloginUser.UserId + "'";
                    DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                    if (dt.Rows.Count == 1)
                    {
                        TWGuid = dt.Rows[0]["Two_NewWayChecklist_Id"].ToString();
                    }
                    else
                    {
                        IsAdd = "Add";
                        TWGuid = Guid.NewGuid().ToString();
                    }
                }
                else
                {
                    TWGuid = Two_WayChecklist_Id;
                }
                ltlName.Text = Two_WayChecklist_Name;

            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="Two_WayChecklist_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetList(string Two_WayChecklist_Id)
        {
            try
            {
                StringBuilder stbHtml = new StringBuilder();
                string SqlBig = @"select Two_WayChecklistDetail_Id,Two_WayChecklist_Id,ParentId,TestQuestions_Num,TestQuestions_NumStr,TestQuestions_Type from Two_WayChecklistDetail where Two_WayChecklist_Id='" + Two_WayChecklist_Id + "' and ParentId='0' order by TestQuestions_Num,CreateTime";
                string TempBig = "<tr><td colspan=\"7\"><b>{0}</b></td><td class='opera'><a href=\"javascript:EditData('{1}');\">修改</a></td></tr>";
                string TempSmall = "<tr><td>{0}</td><td>{1}</td><td class='hide'>{7}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td id=\"TQ_{6}\">{8}</td><td class='opera'><a href=\"javascript:UpdateSmall('{6}');\">修改</a></td></tr>";
                DataTable dtBig = Rc.Common.DBUtility.DbHelperSQL.Query(SqlBig).Tables[0];

                DataTable dtAttr = new BLL_Two_WayChecklistDetailToAttr().GetList("Two_WayChecklist_Id='" + Two_WayChecklist_Id + "'").Tables[0];
                //DataTable dtSmall = new BLL_Two_WayChecklistDetail().GetList("Two_WayChecklist_Id='" + Two_WayChecklist_Id + "'").Tables[0];
                DataTable dtTQCount = Rc.Common.DBUtility.DbHelperSQL.Query(@"select Two_WayChecklistDetail_Id,count(1) as icount from Two_WayChecklistDetailToTestQuestions
where Two_WayChecklist_Id='" + Two_WayChecklist_Id + "' group by Two_WayChecklistDetail_Id").Tables[0];

                if (dtBig.Rows.Count > 0)
                {
                    foreach (DataRow itemBig in dtBig.Rows)
                    {
                        stbHtml.AppendFormat(TempBig, itemBig["TestQuestions_NumStr"].ToString(), itemBig["Two_WayChecklistDetail_Id"].ToString(), itemBig["TestQuestions_Type"].ToString());
                        string SqlSmall = @"select t.* ,t3.D_Name from Two_WayChecklistDetail t
                                            left join TestPaper_FrameDetail t1 on t1.TestPaper_FrameDetail_Id =t.TestPaper_FrameDetail_Id 
                                            left join Common_Dict t3 on t3.Common_Dict_ID = t1.TestQuestionType_Web where t.Two_WayChecklist_Id='" + Two_WayChecklist_Id + "' and t.ParentId='" + itemBig["Two_WayChecklistDetail_Id"].ToString() + "' order by t.TestQuestions_Num,t.CreateTime";
                        DataTable dtSmall = Rc.Common.DBUtility.DbHelperSQL.Query(SqlSmall).Tables[0];
                        DataRow[] drSmall = dtSmall.Select();
                        if (drSmall.Length > 0)
                        {
                            foreach (DataRow itemSmall in drSmall)
                            {
                                stbHtml.AppendFormat(TempSmall
                                    , itemSmall["TestQuestions_NumStr"].ToString()
                                    , itemSmall["D_Name"].ToString()
                                    , GetAttr(dtAttr, itemSmall["Two_WayChecklistDetail_Id"].ToString(), "2")
                                    , GetAttr(dtAttr, itemSmall["Two_WayChecklistDetail_Id"].ToString(), "1")
                                    , GetAttr(dtAttr, itemSmall["Two_WayChecklistDetail_Id"].ToString(), "3")
                                    , itemSmall["Score"].ToString().clearLastZero()
                                    , itemSmall["Two_WayChecklistDetail_Id"].ToString()
                                    , itemSmall["TestQuestions_Num"].ToString()
                                    , GetQuestions(dtTQCount, itemSmall["Two_WayChecklistDetail_Id"].ToString()));
                            }
                        }
                    }
                    stbHtml.Append("<span class=\"tag_add\" data-name=\"addKnowledge\" data-BigId=\"\" data-SmallId=\"\" data-question=\"1\">+</span>");
                    return stbHtml.ToString();
                }
                else
                {
                    return "<tr><td colspan=\"100\"></td>暂无数据</tr>";
                }
            }
            catch (Exception ex)
            {
                return "1";
            }
        }
        /// <summary>
        /// 获取属性的名称
        /// </summary>
        /// <param name="Two_WayChecklist_Id"></param>
        /// <param name="Two_WayChecklistDetail_Id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAttr(DataTable dtAttr, string Two_WayChecklistDetail_Id, string type)
        {
            try
            {
                string AttrTemp = "<span class=\"tag\">{0}<i data-name=\"removeKnowledge\" data-twoid=\"{2}\" data-question=\"{1}\">&times;</i></span>";
                StringBuilder stbHtml = new StringBuilder();
                //string sql = string.Format(@" select * from Two_WayChecklistDetailToAttr where Two_WayChecklist_Id='{0}' and  Two_WayChecklistDetail_Id='{1}' and Attr_Type='{2}'"
                //    , Two_WayChecklist_Id.Filter()
                //    , Two_WayChecklistDetail_Id.Filter()
                //    , type.Filter());
                //DataTable dtAttr = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                DataRow[] drAttr = dtAttr.Select("Two_WayChecklistDetail_Id='" + Two_WayChecklistDetail_Id + "' and Attr_Type='" + type + "'", "Attr_Value");
                if (drAttr.Length > 0)
                {
                    foreach (DataRow item in drAttr)
                    {
                        stbHtml.AppendFormat(AttrTemp
                            , item["Attr_Value"]
                            , item["Two_WayChecklistDetailToAttr_Id"]
                            , Two_WayChecklistDetail_Id);
                    }
                }
                stbHtml.AppendFormat("<span class=\"tag_add\" data-name=\"addKnowledge\" data-question=\"{0}\" data-type=\"{1}\" >+</span>", Two_WayChecklistDetail_Id, type);
                return stbHtml.ToString();

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 获取题的数据
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


        [WebMethod]
        public static string DeleteAttr(string Two_WayChecklistDetailToAttr_Id, string Two_WayChecklist_Id, string Two_WayChecklistDetail_Id)
        {
            try
            {
                Two_WayChecklist_Id = Two_WayChecklist_Id.Filter();
                Two_WayChecklistDetail_Id = Two_WayChecklistDetail_Id.Filter();
                if (new BLL_Two_WayChecklistDetailToAttr().Delete(Two_WayChecklistDetailToAttr_Id.Filter()))
                {
                    int j = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("exec [P_SelectQuestions] '" + Two_WayChecklist_Id + "','" + Two_WayChecklistDetail_Id + "'", 7200);
                    return j.ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 增加知识点测量目标难易度
        /// </summary>
        /// <param name="Two_WayChecklistDetail_Id"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetInsertAttr(string Two_WayChecklistDetail_Id, string Type)
        {
            try
            {
                string temp = "<input type=\"checkbox\" id=\"{0}\" tt=\"{0}\" value=\"{1}\"><label for=\"{0}\">{1}</label>";
                string StrAttr = string.Empty; ;
                string Sql = string.Format(@"select Attr_value from [dbo].[TestPaper_FrameDetailToTestQuestions_Attr] a
inner join [Two_WayChecklistDetail] d on d.TestPaper_FrameDetail_Id=a.TestPaper_FrameDetail_Id and d.Two_WayChecklistDetail_Id='{0}'
where attr_type='{1}' and Attr_Value not in(select Attr_Value from [Two_WayChecklistDetailToAttr] where Two_WayChecklistDetail_Id='{0}' and Attr_Type='{1}')"
                    , Two_WayChecklistDetail_Id.Filter()
                    , Type.Filter());
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(Sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        StrAttr += string.Format(temp
                            , Guid.NewGuid().ToString()
                            , item["Attr_value"].ToString());
                    }
                    return StrAttr;
                }
                else
                {
                    return "暂无可使用的相关试卷属性";
                }
            }
            catch (Exception ex)
            {
                return "暂无可使用的相关试卷属性";
            }
        }


        [WebMethod]
        public static string AddAttr(string Two_WayChecklist_Id, string Two_WayChecklistDetail_Id, string Type, string arrId, string arrKnowledge)
        {
            try
            {
                string sql = string.Empty;
                Model_F_User loginUser = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                string temp = @"INSERT INTO [dbo].[Two_WayChecklistDetailToAttr]
           ([Two_WayChecklistDetailToAttr_Id]
           ,[Two_WayChecklist_Id]
           ,[Two_WayChecklistDetail_Id]
           ,[Attr_Type]
           ,[Attr_Value]
           ,[CreateUser]
           ,[CreateTime])
     VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}');";
                string[] ArrAttr = arrKnowledge.Filter().Split(',');
                string[] ArrId = arrId.Split(',');
                for (int i = 0; i < ArrAttr.Length; i++)
                {
                    sql += string.Format(temp
                        , ArrId[i]
                        , Two_WayChecklist_Id.Filter()
                        , Two_WayChecklistDetail_Id.Filter()
                        , Type
                        , ArrAttr[i]
                        , loginUser.UserId
                        , DateTime.Now);
                }
                if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql) > 0)
                {
                    int j = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("exec [P_SelectQuestions] '" + Two_WayChecklist_Id + "','" + Two_WayChecklistDetail_Id + "'", 7200);
                    return j.ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="TWGuid"></param>
        /// <param name="Two_WayChecklist_Id"></param>
        /// <param name="TestPaper_Frame_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string InitalData(string TWGuid, string Two_WayChecklist_Id, string TestPaper_Frame_Id, string Two_WayChecklist_Name)
        {
            try
            {
                TWGuid = TWGuid.Filter();
                Two_WayChecklist_Id = Two_WayChecklist_Id.Filter();
                TestPaper_Frame_Id = TestPaper_Frame_Id.Filter();
                Model_F_User loginUser = (Model_F_User)HttpContext.Current.Session["FLoginUser"];
                StringBuilder sbSql = new StringBuilder();
                #region 插入双向细目表主表
                sbSql.AppendFormat(@" insert into  Two_WayChecklist (Two_WayChecklist_Id,Two_WayChecklist_Name,ParticularYear,GradeTerm
,Resource_Version,[Subject],[Remark],[Two_WayChecklistType],[CreateUser],[CreateTime],[ParentId])
select '{0}',Two_WayChecklist_Name,[ParticularYear] ,[GradeTerm],[Resource_Version]
,[Subject],Remark,'1','{1}',GetDate(),'{2}' from Two_WayChecklist where Two_WayChecklist_Id='{3}' ;"
                    , TWGuid
                    , loginUser.UserId
                    , TestPaper_Frame_Id
                    , Two_WayChecklist_Id.Filter());
                #endregion
                #region 插入双向细目表主表
                string sql = "select * from Two_WayChecklistDetail where Two_WayChecklist_Id='" + Two_WayChecklist_Id + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                DataRow[] drP = dt.Select("ParentId='0'");
                foreach (DataRow item in drP)
                {
                    string ParentID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat(@" insert into Two_WayChecklistDetail([Two_WayChecklistDetail_Id],[Two_WayChecklist_Id],[ParentId],[TestQuestions_Num]
,[TestQuestions_NumStr],[TestQuestions_Type],[TargetText],[ComplexityText],[KnowledgePoint],[Score]
,[Remark],[Two_WayChecklistType],[CreateUser],[CreateTime],TestPaper_FrameDetail_Id) values('{0}','{1}','0','{2}','{3}','{4}','{5}','{6}','{7}'
,'{8}','{9}','{10}','{11}','{12}','{13}')"
                        , ParentID
                        , TWGuid
                        , item["TestQuestions_Num"].ToString()
                        , item["TestQuestions_NumStr"].ToString()
                        , item["TestQuestions_Type"].ToString()
                        , item["TargetText"].ToString()
                        , item["ComplexityText"].ToString()
                        , item["KnowledgePoint"].ToString()
                        , 0
                        , item["Remark"].ToString()
                        , item["Two_WayChecklistType"].ToString()
                        , loginUser.UserId
                        , DateTime.Now.ToString()
                        , item["TestPaper_FrameDetail_Id"].ToString());
                    DataRow[] dr = dt.Select("ParentId='" + item["Two_WayChecklistDetail_Id"] + "'");
                    if (dr.Length > 0)
                    {
                        foreach (DataRow itemSon in dr)
                        {
                            sbSql.AppendFormat(@" insert into Two_WayChecklistDetail([Two_WayChecklistDetail_Id],[Two_WayChecklist_Id],[ParentId],[TestQuestions_Num]
,[TestQuestions_NumStr],[TestQuestions_Type],[TargetText],[ComplexityText],[KnowledgePoint],[Score]
,[Remark],[Two_WayChecklistType],[CreateUser],[CreateTime],TestPaper_FrameDetail_Id) values (NewId(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'
,'{8}','{9}','{10}','{11}','{12}','{13}');"
                            , TWGuid
                            , ParentID
                            , itemSon["TestQuestions_Num"].ToString()
                        , itemSon["TestQuestions_NumStr"].ToString()
                        , itemSon["TestQuestions_Type"].ToString()
                        , itemSon["TargetText"].ToString()
                        , itemSon["ComplexityText"].ToString()
                        , itemSon["KnowledgePoint"].ToString()
                        , itemSon["Score"].ToString()
                        , itemSon["Remark"].ToString()
                        , itemSon["Two_WayChecklistType"].ToString()
                        , loginUser.UserId
                        , DateTime.Now.ToString()
                        , itemSon["TestPaper_FrameDetail_Id"].ToString());
                        }
                    }
                }
                #endregion
                #region 插入双向细目表与题关系
                sbSql.AppendFormat(@" insert into Two_WayChecklistDetailToTestQuestions ([Two_WayChecklistDetailToTestQuestions_Id]
,[Two_WayChecklist_Id],[Two_WayChecklistDetail_Id],[ResourceToResourceFolder_Id],[TestQuestions_Id],[CreateUser],[CreateTime])
select NEWID(), '{1}',t.Two_WayChecklistDetail_Id ,ResourceToResourceFolder_Id,[TestQuestions_Id]
,'{0}',getdate() from Two_WayChecklistDetailToTestQuestions t
where t.Two_WayChecklist_Id='{2}';"
                    , loginUser.UserId
                    , TWGuid
                    , Two_WayChecklist_Id);
                sbSql.AppendFormat(@" update t set t.Two_WayChecklistDetail_Id=t3.Two_WayChecklistDetail_Id
from Two_WayChecklistDetailToTestQuestions t,Two_WayChecklistDetail t2,Two_WayChecklistDetail t3
where t.Two_WayChecklist_Id='{0}'
and t.Two_WayChecklistDetail_Id=t2.Two_WayChecklistDetail_Id and t2.Two_WayChecklist_Id='{1}'
and t3.TestPaper_FrameDetail_Id=t2.TestPaper_FrameDetail_Id and t3.Two_WayChecklist_Id='{0}'"
                    , TWGuid
                    , Two_WayChecklist_Id);
                #endregion
                #region 插入双向细目表与知识点的关系
                sbSql.AppendFormat(@"insert into [Two_WayChecklistDetailToAttr]([Two_WayChecklistDetailToAttr_Id],[Two_WayChecklist_Id],[Two_WayChecklistDetail_Id]
,[Attr_Type],[Attr_Value],[CreateUser],[CreateTime])
select NEWID(),'{0}',Two_WayChecklistDetail_Id,[Attr_Type],[Attr_Value],'{1}','{2}' from [Two_WayChecklistDetailToAttr] tpAttr
where tpAttr.Two_WayChecklist_Id='{3}';"
                    , TWGuid
                    , loginUser.UserId
                    , DateTime.Now
                    , Two_WayChecklist_Id);
                sbSql.AppendFormat(@" update t set t.Two_WayChecklistDetail_Id=t3.Two_WayChecklistDetail_Id
from Two_WayChecklistDetailToAttr t,Two_WayChecklistDetail t2,Two_WayChecklistDetail t3
where t.Two_WayChecklist_Id='{0}'
and t.Two_WayChecklistDetail_Id=t2.Two_WayChecklistDetail_Id and t2.Two_WayChecklist_Id='{1}'
and t3.TestPaper_FrameDetail_Id=t2.TestPaper_FrameDetail_Id and t3.Two_WayChecklist_Id='{0}'"
                   , TWGuid
                   , Two_WayChecklist_Id);
                #endregion
                #region 插入双向细目表对应老师关系表
                sbSql.AppendFormat(@"INSERT INTO [dbo].[Two_WayChecklistToTeacher]
           ([Two_WayChecklistToTeacher_Id]
           ,[Two_WayChecklist_Id]
           ,Two_NewWayChecklist_Id
           ,[Teacher_Id]
           ,[CreateTime],[Status]) values (NewId(),'{0}','{1}','{2}',getdate(),'1')"
                    , Two_WayChecklist_Id
                    , TWGuid
                    , loginUser.UserId);
                #endregion
                int row = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sbSql.ToString());
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
    }
}