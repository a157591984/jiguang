using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Text;
using System.Web.Services;
using Rc.BLL.Resources;
using Rc.Cloud.Model;
using Rc.Model.Resources;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class RelationPaper : Rc.Cloud.Web.Common.InitPage
    {
        public string TestPaper_Frame_Id = string.Empty;
        public string TestPaper_Frame_Name = string.Empty;
        public string Resource_Version = string.Empty;
        public string Year = string.Empty;
        public string GradeTerm = string.Empty;
        public string Subject = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            TestPaper_Frame_Id = Request["TestPaper_Frame_Id"].Filter();
            Resource_Version = Request["Resource_Version"].Filter();
            Year = Request["Year"].Filter();
            GradeTerm = Request["GradeTerm"].Filter();
            Subject = Request["Subject"].Filter();
            TestPaper_Frame_Name = Request["TestPaper_Frame_Name"].Filter();
            if (!IsPostBack)
            {
                ltlName.Text = "正在给试卷结构【" + Server.UrlDecode(TestPaper_Frame_Name) + "】关联试卷";
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //教材版本
                strWhere = " D_Type='3' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, dt, "D_Name", "Common_Dict_ID", "教材版本");
                //入学年份
                int years = DateTime.Now.Year;
                Rc.Cloud.Web.Common.pfunction.SetDdlStartSchoolYear(ddlYear, years - 5, years + 1, true, "入学年份");
                //年级学期
                strWhere = " D_Type='6' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeTerm, dt, "D_Name", "Common_Dict_ID", "年级学期");
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID", "学科");
                if (!string.IsNullOrEmpty(Year) && !string.IsNullOrEmpty(GradeTerm) && !string.IsNullOrEmpty(Resource_Version) && !string.IsNullOrEmpty(Subject))
                {
                    ddlYear.SelectedValue = Year;
                    ddlGradeTerm.SelectedValue = GradeTerm;
                    ddlResource_Version.SelectedValue = Resource_Version;
                    ddlSubject.SelectedValue = Subject;
                }

            }
        }

        /// <summary>
        /// 递归加载树菜单
        /// </summary>
        /// <param name="TreeIDCurrent"></param>
        /// <param name="TreeLevelCurrent"></param>
        /// <param name="dtAll"></param>
        /// <returns></returns>
        protected static StringBuilder InitNavigationTree(string TreeIDCurrent, string strResource_Class, DataView dvw, int level)
        {
            StringBuilder strHtml = new StringBuilder();
            string strWhere = string.Empty;
            strWhere = " 1=1 ";
            if (TreeIDCurrent == "0")
            {
                dvw.RowFilter = string.Format("  {0} and ResourceFolder_Level ='5'", strWhere);
            }
            else
            {
                dvw.RowFilter = string.Format(" {0} and  ResourceFolder_ParentId = '{1}' ", strWhere, TreeIDCurrent);
            }
            if (dvw.Count > 0)
            {
                int subProcess = 0;
                int subMax = dvw.Count;
                string liClass = string.Empty;
                string url = string.Empty;
                foreach (DataRowView drv in dvw)
                {
                    subProcess++;
                    if (subProcess == 1)
                    {
                        strHtml.AppendFormat(" <ul data-level='{0}'>", level);

                    }
                    url = string.IsNullOrEmpty(drv["File_Suffix"].ToString()) ? "" : string.Format("data-url='TestPaperView.aspx?ResourceToResourceFolder_Id={0}'", drv["ResourceFolder_Id"].ToString());

                    strHtml.Append("<li>");
                    strHtml.AppendFormat("<a href='javascript:;' data-name='mtreeLink' {0}>", url);
                    strHtml.Append("<div data-name='mtreeIndent'></div>");
                    strHtml.Append("<div data-name='mtreeBtn'></div>");
                    strHtml.AppendFormat("<div data-name='mtreeName'>{0}</div>"
                        , string.IsNullOrEmpty(drv["File_Suffix"].ToString()) ? drv["ResourceFolder_Name"].ToString().ReplaceForFilter() :
                        string.Format("<label><input type='checkbox' {2} value='{1}' /></label>{0}", drv["ResourceFolder_Name"].ToString().ReplaceForFilter(), drv["ResourceFolder_Id"].ToString(), drv["CountTest"].ToString() == "0" ? "" : " checked='checked'"));
                    strHtml.Append("</a>");
                    strHtml.Append(InitNavigationTree(drv["ResourceFolder_Id"].ToString(), strResource_Class, dvw, level + 1));
                    strHtml.Append("</li>");
                    if (subProcess == subMax)
                    {
                        strHtml.Append("</ul>");
                    }
                }
            }
            return strHtml;
        }

        /// <summary>
        /// 异步得到树
        /// </summary>
        /// <param name="Two_WayChecklist_Id"></param>
        /// <param name="Year"></param>
        /// <param name="Resource_Version"></param>
        /// <param name="GradeTerm"></param>
        /// <param name="Subject"></param>
        /// <param name="Name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetTree(string TestPaper_Frame_Id, string Year, string Resource_Version, string GradeTerm, string Subject, string Name, string type)
        {
            try
            {
                string Temp = "<ul data-level=\"0\"><li><div class=\"name\"><i class=\"tree_btn fa\"></i><a href=\"javascript:;\" >{0}</a></div>";
                string TempStr = "<li><a href='javascript:;' data-name='mtreeLink' data-url='TestPaperView.aspx?ResourceToResourceFolder_Id={1}'><div data-name='mtreeIndent'></div><div data-name='mtreeBtn'></div><div data-name='mtreeName'><input type='checkbox' checked='checked' value='{1}' /><label>{0}</label></div></a></li>";
                string TempStrs = "<li><a href='javascript:;' data-name='mtreeLink' data-url='TestPaperView.aspx?ResourceToResourceFolder_Id={1}'><div data-name='mtreeIndent'></div><div data-name='mtreeBtn'></div><div data-name='mtreeName'><input type='checkbox' {2} value='{1}' /><label>{0}</label></div></a></li>";
                string StrWhere = string.Empty;
                if (type == "1")
                {
                    string str = string.Empty;
                    StrWhere = " and ResourceFolder_Id in(select ResourceToResourceFolder_Id from TestPaper_FrameToTestpaper where TestPaper_Frame_Id='" + TestPaper_Frame_Id.Filter() + "')";
                    string strsql = @"select ResourceFolder_Id,ResourceFolder_Name,Resource_Type,Resource_Class,File_Suffix,ResourceFolder_Level,ResourceFolder_ParentId from VW_ResourceAndResourceFolder where Resource_Class = '" + Rc.Common.Config.Resource_ClassConst.云资源 + "' and Resource_Type='" + Rc.Common.Config.Resource_TypeConst.testPaper类型文件 + "' " + StrWhere + "  order by ResourceFolder_Level, ResourceFolder_Order";
                    DataTable dtResource = Rc.Common.DBUtility.DbHelperSQL.Query(strsql).Tables[0];
                    if (dtResource.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtResource.Rows)
                        {
                            str += string.Format(TempStr, item["ResourceFolder_Name"].ToString().ReplaceForFilter(), item["ResourceFolder_Id"]);
                        }
                        return str;
                    }
                    else
                    {
                        return string.Format(Temp, "暂无可关联的试卷.....");
                    }
                }
                else
                {

                    if (!string.IsNullOrEmpty(Year) && Year != "-1")
                    {
                        StrWhere += " and ParticularYear='" + Year + "'";
                    }
                    if (!string.IsNullOrEmpty(Resource_Version) && Resource_Version != "-1")
                    {
                        StrWhere += " and Resource_Version='" + Resource_Version + "'";
                    }
                    if (!string.IsNullOrEmpty(GradeTerm) && GradeTerm != "-1")
                    {
                        StrWhere += " and GradeTerm='" + GradeTerm + "'";
                    }
                    if (!string.IsNullOrEmpty(Subject) && Subject != "-1")
                    {
                        StrWhere += " and Subject='" + Subject + "'";
                    }
                    if (!string.IsNullOrEmpty(Name.TrimEnd()))
                    {
                        StrWhere += " and ResourceFolder_Name like '%" + Name + "%' and File_Suffix<>''";
                        string strsql = @"select ResourceFolder_Id,ResourceFolder_Name,Resource_Type,Resource_Class,File_Suffix,ResourceFolder_Level,ResourceFolder_ParentId 
                      ,CountTest=(select count(*) from TestPaper_FrameToTestpaper where TestPaper_Frame_Id='" + TestPaper_Frame_Id + @"' and ResourceToResourceFolder_Id=vw.ResourceFolder_Id) 
                     from VW_ResourceAndResourceFolder vw where Resource_Class = '" + Rc.Common.Config.Resource_ClassConst.云资源 + "' and Resource_Type='" + Rc.Common.Config.Resource_TypeConst.testPaper类型文件 + "' " + StrWhere + "  order by ResourceFolder_Level, ResourceFolder_Order";
                        DataTable dtreName = Rc.Common.DBUtility.DbHelperSQL.Query(strsql).Tables[0];
                        string str = string.Empty;
                        if (dtreName.Rows.Count > 0)
                        {
                            foreach (DataRow item in dtreName.Rows)//checked='checked'
                            {
                                str += string.Format(TempStrs, item["ResourceFolder_Name"].ToString().ReplaceForFilter(), item["ResourceFolder_Id"], item["CountTest"].ToString() == "0" ? "" : "checked='checked'");
                            }
                            return str;
                        }
                        else
                        {
                            return string.Format(Temp, "暂无可关联的试卷.....");
                        }
                    }
                    else
                    {
                        string strsql = @"select ResourceFolder_Id,ResourceFolder_Name,Resource_Type,Resource_Class,File_Suffix,ResourceFolder_Level,ResourceFolder_ParentId 
                      ,CountTest=(select count(*) from TestPaper_FrameToTestpaper where TestPaper_Frame_Id='" + TestPaper_Frame_Id + @"' and ResourceToResourceFolder_Id=vw.ResourceFolder_Id) 
                     from VW_ResourceAndResourceFolder vw where Resource_Class = '" + Rc.Common.Config.Resource_ClassConst.云资源 + "' and Resource_Type='" + Rc.Common.Config.Resource_TypeConst.testPaper类型文件 + "' " + StrWhere + "  order by ResourceFolder_Level, ResourceFolder_Order";
                        DataTable dtre = Rc.Common.DBUtility.DbHelperSQL.Query(strsql).Tables[0];
                        if (dtre.Rows.Count > 0)
                        {
                            DataView dvw = new DataView();
                            dvw.Table = dtre;
                            string temp = InitNavigationTree("0", dtre.Rows[0]["Resource_Class"].ToString(), dvw, 0).ToString();
                            StringBuilder strHtml = new StringBuilder();
                            return temp;
                        }
                        else
                        {
                            return string.Format(Temp, "暂无可关联的试卷.....");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        /// 关联试卷
        /// </summary>
        /// <param name="Two_WayChecklist_Id"></param>
        /// <param name="TestPaper"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetRelationPaper(string TestPaper_Frame_Id, string TestPaper)
        {
            try
            {
                string[] arrRTRF = TestPaper.Split(',');
                string msg = string.Empty;
                if (VerifyTestpaper(TestPaper_Frame_Id, arrRTRF, out msg))
                {
                    return "1";
                }
                else
                {
                    return msg;
                }
            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(TestPaper_Frame_Id, "", string.Format("关联试卷失败：|资源标识{0}|双向细目表Id{1}|方法{2}|错误信息{3}", TestPaper, TestPaper_Frame_Id, "GetRelationPaper", ex.Message.ToString()));
                return "异常：" + ex.Message.ToString();
            }
        }
        /// <summary>
        /// 验证试卷是否有效
        /// </summary>
        /// <param name="Two_WayChecklist_Id"></param>
        /// <param name="arrRTRF"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static bool VerifyTestpaper(string TestPaper_Frame_Id, string[] arrRTRF, out string msg)
        {
            bool flag = true;
            msg = string.Empty;
            try
            {
                BLL_Two_WayChecklistDetail bll = new BLL_Two_WayChecklistDetail();
                BLL_TestQuestions_Score bllTQScore = new BLL_TestQuestions_Score();
                DataTable dtTWC_Count = new DataTable(); // 双向细目表总分、试题数量
                DataTable dtTWC = new DataTable();       // 双向细目表明细
                DataTable dtTQ_Count = new DataTable();  // 习题集资源总分、试题数量
                DataTable dtTQScore = new DataTable();   // 习题集试题明细
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                string strSql = string.Empty;
                strSql = string.Format(@"select count(1) as ICount,sum(Score) as SumScore from TestPaper_FrameDetail 
where ParentId!='0' and TestPaper_Frame_Id='{0}' ", TestPaper_Frame_Id);
                dtTWC_Count = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                DataRow drTWC_Count = dtTWC_Count.Rows[0];
                int twCount = 0;
                double twSumScore = 0;
                int.TryParse(drTWC_Count["ICount"].ToString(), out twCount);
                double.TryParse(drTWC_Count["SumScore"].ToString(), out twSumScore);

                strSql = string.Empty;
                strSql = string.Format(@"select t.* from TestPaper_FrameDetail t 
inner join TestPaper_FrameDetail t2 on t.ParentId=t2.TestPaper_FrameDetail_Id 
where t.ParentId!='0' and t.TestPaper_Frame_Id='{0}' order by t2.TestQuestions_Num,t2.CreateTime,t.TestQuestions_Num,t.CreateTime ", TestPaper_Frame_Id);
                dtTWC = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];

                foreach (var rtrfId in arrRTRF)
                {
                    List<Model_TestPaper_FrameDetailToTestQuestions> listmodel = new List<Model_TestPaper_FrameDetailToTestQuestions>();
                    string strSqlTQ = string.Empty;
                    strSqlTQ = string.Format(@"select max(rtrf.Resource_Name) as Resource_Name,count(1) as ICount ,sum(t.TestQuestions_SumScore) as SumScore from (
select TestQuestions_SumScore,tq.ResourceToResourceFolder_Id from TestQuestions tq 
where tq.TestQuestions_Type!='title' and tq.TestQuestions_Type<>'' and tq.[type]='simple' and tq.ResourceToResourceFolder_Id='{0}' 
union all
select TestQuestions_SumScore,tq.ResourceToResourceFolder_Id from TestQuestions tq 
where tq.Parent_Id='0' and tq.[type]='complex' and tq.ResourceToResourceFolder_Id='{0}' 
)  t inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=t.ResourceToResourceFolder_Id", rtrfId);
                    dtTQ_Count = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlTQ).Tables[0];
                    DataRow drTQ_Count = dtTQ_Count.Rows[0];
                    int tqCount = 0;
                    double tqSumScore = 0;
                    int.TryParse(drTQ_Count["ICount"].ToString(), out tqCount);
                    double.TryParse(drTQ_Count["SumScore"].ToString(), out tqSumScore);
                    if (flag && tqCount != twCount)
                    {
                        flag = false;
                        msg = string.Format("资源【{0}】试题数不等于双向细目表明细数", drTQ_Count["Resource_Name"].ToString().ReplaceForFilter());
                    }
                    if (flag && tqSumScore != twSumScore)
                    {
                        flag = false;
                        msg = string.Format("资源【{0}】总分不等于双向细目表总分", drTQ_Count["Resource_Name"].ToString().ReplaceForFilter());
                    }
                    if (flag)
                    {
                        string strSqlScore = string.Empty;
                        strSqlScore = string.Format(@"select * from (
select tq.TestQuestions_Id,tq.TestQuestions_Type,tq.[type],tq.TestQuestions_Num,tq.topicNumber,tq.TestQuestions_SumScore from TestQuestions tq 
where tq.TestQuestions_Type!='title' and tq.TestQuestions_Type<>'' and tq.[type]='simple' and tq.ResourceToResourceFolder_Id='{0}' 
union all
select tq.TestQuestions_Id,tq.TestQuestions_Type,tq.[type],tq.TestQuestions_Num,tq.topicNumber,tq.TestQuestions_SumScore from TestQuestions tq 
where tq.Parent_Id='0' and tq.[type]='complex' and tq.ResourceToResourceFolder_Id='{0}'
) t order by TestQuestions_Num ", rtrfId);
                        dtTQScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlScore).Tables[0];
                        int row = 0;
                        foreach (DataRow item in dtTWC.Rows)
                        {
                            if (flag && item["TestPaper_FrameType"].ToString() == "simple" && dtTQScore.Rows[row]["type"].ToString() == "complex")
                            {
                                flag = false;
                                msg = string.Format("资源【{0}】第{1}题{2}与双向细目表不匹配"
                                    , drTQ_Count["Resource_Name"].ToString().ReplaceForFilter()
                                    , dtTQScore.Rows[row]["topicNumber"]
                                    , "试题类型");
                            }
                            if (flag && item["TestPaper_FrameType"].ToString() == "complex" && dtTQScore.Rows[row]["type"].ToString() != "complex")
                            {
                                flag = false;
                                msg = string.Format("资源【{0}】第{1}题{2}与双向细目表不匹配"
                                    , drTQ_Count["Resource_Name"].ToString().ReplaceForFilter()
                                    , dtTQScore.Rows[row]["topicNumber"]
                                    , "试题类型");
                            }


                            double twScore = 0; // 双向细目表分值
                            double tqScore = 0; // 试题分值
                            double.TryParse(item["Score"].ToString(), out twScore);
                            double.TryParse(dtTQScore.Rows[row]["TestQuestions_SumScore"].ToString(), out tqScore);
                            if (flag && twScore != tqScore)
                            {
                                flag = false;
                                msg = string.Format("资源【{0}】第{1}题{2}与双向细目表不匹配"
                                    , drTQ_Count["Resource_Name"].ToString().ReplaceForFilter()
                                    , dtTQScore.Rows[row]["topicNumber"]
                                    , "试题分值");
                            }
                            Model_TestPaper_FrameDetailToTestQuestions model = new Model_TestPaper_FrameDetailToTestQuestions();
                            model.TestPaper_FrameDetailToTestQuestions_Id = Guid.NewGuid().ToString();
                            model.TestPaper_Frame_Id = item["TestPaper_Frame_Id"].ToString();
                            model.TestPaper_FrameDetail_Id = item["TestPaper_FrameDetail_Id"].ToString();
                            model.ResourceToResourceFolder_Id = rtrfId;
                            model.TestQuestions_Id = dtTQScore.Rows[row]["TestQuestions_Id"].ToString();
                            model.CreateTime = DateTime.Now;
                            model.CreateUser = loginUser.SysUser_ID;
                            listmodel.Add(model);
                            row++;
                        }
                    }
                    if (flag)
                    {
                        if (new BLL_TestPaper_FrameToTestpaper().GetRecordCount("TestPaper_Frame_Id='" + TestPaper_Frame_Id + "' and ResourceToResourceFolder_Id='" + rtrfId + "'") > 0)
                        {
                            //Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime(string.Format("exec P_GenerateTF_Attr '{0}','{1}'", TestPaper_Frame_Id, loginUser.SysUser_ID), 7200);
                            flag = true;
                        }
                        else
                        {
                            Rc.Model.Resources.Model_TestPaper_FrameToTestpaper model = new Rc.Model.Resources.Model_TestPaper_FrameToTestpaper();
                            model.CreateTime = DateTime.Now;
                            model.CreateUser = loginUser.SysUser_ID;
                            model.ResourceToResourceFolder_Id = rtrfId;
                            model.TestPaper_FrameToTestpaper_Id = Guid.NewGuid().ToString();
                            model.TestPaper_Frame_Id = TestPaper_Frame_Id;
                            model.TestPaper_FrameToTestpaper_Type = "0";
                            if (new BLL_TestPaper_FrameToTestpaper().AddRelationPaper(model, listmodel) > 0)
                            {
                                //Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime(string.Format("exec P_GenerateTF_Attr '{0}','{1}'", TestPaper_Frame_Id, loginUser.SysUser_ID), 7200);
                                flag = true;
                            }
                            else
                            {
                                flag = false;
                                msg = "关联试卷失败";
                            }
                        }
                    }
                }

                if (flag)
                {
                    //执行存储过程 写入试卷结构明细对应试题属性（测量目标、知识点、难易度）
                    Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime(string.Format("exec P_GenerateTF_Attr '{0}','{1}'", TestPaper_Frame_Id, loginUser.SysUser_ID), 7200);
                }

            }
            catch (Exception ex)
            {
                flag = false;
                msg = ex.Message.ToString();
            }
            return flag;
        }
        /// <summary>
        /// 取消关联试卷
        /// </summary>
        /// <param name="Two_WayChecklist_Id"></param>
        /// <param name="TestPaper"></param>
        /// <returns></returns>
        [WebMethod]
        public static string CancelRelationPaper(string TestPaper_Frame_Id, string TestPaper)
        {
            try
            {
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["loginUser"];
                if (new BLL_Two_WayChecklist().GetRecordCount("ParentId='" + TestPaper_Frame_Id.Filter() + "'") > 0)
                {
                    return "2";
                }
                else
                {
                    string[] arr = TestPaper.Split(',');
                    string StrSql = string.Empty;
                    foreach (var item in arr)
                    {
                        StrSql += string.Format(@" delete from TestPaper_FrameToTestpaper where TestPaper_Frame_Id='{0}' and ResourceToResourceFolder_Id='{1}' ; 
delete from TestPaper_FrameDetailToTestQuestions where TestPaper_Frame_Id='{0}' and ResourceToResourceFolder_Id='{1}'; "
                            , TestPaper_Frame_Id
                            , item);
                    }
                    if (StrSql.Length > 0)
                    {
                        StrSql+= string.Format("exec P_GenerateTF_Attr '{0}','{1}'", TestPaper_Frame_Id, loginUser.SysUser_ID);
                    }
                    int i = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime(StrSql.ToString(), 7200);
                    if (i > 0)
                    {
                        return "1";
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {

                return "";
            }
        }

    }

}