using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using System.Data;
using System.Diagnostics;
using System.Web.Services;
using System.Text;
namespace Rc.Cloud.Web.Evaluation
{
    public partial class CommentReportSummarize : Rc.Cloud.Web.Common.FInitData
    {
        public string HomeWork_Id = string.Empty;
        public string ResourceToResourceFolder_Id = string.Empty;
        public string ClassId = string.Empty;
        protected DataTable dtTQ = new DataTable();//
        protected DataTable dtTQ_StudentAnswer = new DataTable();//
        protected DataTable dtTQ_StudentAnswerNew = new DataTable();//
        protected string strHomeWorkId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HomeWork_Id = Request.QueryString["HomeWork_Id"].Filter();
                ResourceToResourceFolder_Id = Request.QueryString["ResourceToResourceFolder_Id"].Filter();

                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(HomeWork_Id))
                    {
                        BLL_StatsClassHW_Score bllschwso = new BLL_StatsClassHW_Score();
                        DataTable dt = bllschwso.GetList("HomeWork_ID='" + HomeWork_Id + "' ").Tables[0];
                        if (dt.Rows.Count == 0)
                        {
                            string strSql = string.Format(@"select t.*,t2.Resource_Name
,t3.GradeName,t3.ClassName
,t4.D_Name as SubjectName
,(case when t5.TrueName is null then t5.UserName when t5.TrueName='' then t5.UserName else t5.UserName end) as TeacherName
,HW_Score=(select ISNULL(SUM(TestQuestions_Score),0) from TestQuestions_Score where ResourceToResourceFolder_Id=t.ResourceToResourceFolder_Id)
,t.HomeWork_AssignTeacher as TeacherID,t.UserGroup_Id as ClassId
from HomeWork t 
inner join ResourceToResourceFolder t2 on t2.ResourceToResourceFolder_Id=t.ResourceToResourceFolder_Id
inner join VW_ClassGradeSchool t3 on t3.ClassId=t.UserGroup_Id
inner join Common_Dict t4 on t4.Common_Dict_ID=t.SubjectId
inner join F_User t5 on t5.UserId=t.HomeWork_AssignTeacher
where t.HomeWork_Id='{0}'", HomeWork_Id);
                            dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                        }
                        if (dt.Rows.Count > 0)
                        {
                            ltlHomeWork_Name.Text = dt.Rows[0]["Resource_Name"].ToString().ReplaceForFilter();
                            ltlGrade.Text = dt.Rows[0]["GradeName"].ToString();
                            ltlClass.Text = dt.Rows[0]["ClassName"].ToString();
                            ltlSubjectName.Text = dt.Rows[0]["SubjectName"].ToString();
                            ltlTeacherName.Text = dt.Rows[0]["TeacherName"].ToString();
                            ltlSumSore.Text = dt.Rows[0]["HW_Score"].ToString().clearLastZero();
                            strHomeWorkId = " and HomeWork_Id='" + HomeWork_Id + "' ";
                            ClassId = dt.Rows[0]["ClassID"].ToString();
                            GetData();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS(Request.Url.ToString(), string.Format("讲评概述加载失败。作业标识：{0}，资源标识：{1}。错误：{2}"
                            , HomeWork_Id, ResourceToResourceFolder_Id, ex.Message.ToString()));
            }
        }
        public void GetData()
        {
            try
            {
                StringBuilder StbHtml = new StringBuilder();
                string Sql = @"select scs.HomeWork_ID,scs.ClassID,scs.ResourceToResourceFolder_Id,scs.Resource_Name,scs.HW_Score,scs.HighestScore ,scs.LowestScore ,scs.StandardDeviation ,scs.AVGScore ,scs.AssignedCount,scs.ClassAllCount ,scs.CommittedCount,scs.UncommittedCount,scs.CorrectedCount ,scs.UnCorrectedCount 
,selectionSum =(select SUM(tq.TestQuestions_SumScore) from  TestQuestions tq where tq.ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and tq.TestQuestions_Type in('selection','clozeTest') )
 ,selectionCount =(select COUNT(tq.TestQuestions_Type) from  TestQuestions tq where tq.ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and tq.TestQuestions_Type in('selection','clozeTest') )
 ,fillSum =(select SUM(tq.TestQuestions_SumScore) from  TestQuestions tq where tq.ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and tq.TestQuestions_Type='fill' )
 ,fillCount =(select COUNT(tq.TestQuestions_Type) from  TestQuestions tq where tq.ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and tq.TestQuestions_Type='fill' )
 ,answersSum =(select SUM(tq.TestQuestions_SumScore) from  TestQuestions tq where tq.ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and tq.TestQuestions_Type='answers' )
 ,answersCount =(select COUNT(tq.TestQuestions_Type) from  TestQuestions tq where tq.ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and tq.TestQuestions_Type='answers')
 ,truefalseSum =(select SUM(tq.TestQuestions_SumScore) from  TestQuestions tq where tq.ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and tq.TestQuestions_Type='truefalse' )
 ,truefalseCount =(select COUNT(tq.TestQuestions_Type) from  TestQuestions tq where tq.ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and tq.TestQuestions_Type='truefalse')
 ,YXTQ=(select '第'+replace(topicNumber,'.',''),(select '-'+ testIndex from TestQuestions_Score where TestQuestions_Score_ID=stq.TestQuestions_Score_ID and len(testIndex)<>0),'题，'  from StatsClassHW_TQ stq where TQ_Score!=-1 and HomeWork_ID=scs.HomeWork_ID and ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and 100>=ScoreAvgRate and ScoreAvgRate>85 order by TestQuestions_Num,TestQuestions_OrderNum    for XML path(''))
,JHTQ=(select '第'+replace(topicNumber,'.',''),(select '-'+ testIndex from TestQuestions_Score where TestQuestions_Score_ID=stq.TestQuestions_Score_ID and len(testIndex)<>0),'题，'  from StatsClassHW_TQ stq  where TQ_Score!=-1 and HomeWork_ID=scs.HomeWork_ID and ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and 85>=ScoreAvgRate and ScoreAvgRate>70 order by TestQuestions_Num,TestQuestions_OrderNum   for XML path(''))
,YBTQ=(select '第'+replace(topicNumber,'.',''),(select '-'+ testIndex from TestQuestions_Score where TestQuestions_Score_ID=stq.TestQuestions_Score_ID and len(testIndex)<>0),'题，'  from StatsClassHW_TQ stq   where TQ_Score!=-1 and HomeWork_ID=scs.HomeWork_ID and ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and 70>=ScoreAvgRate and ScoreAvgRate>60 order by TestQuestions_Num,TestQuestions_OrderNum   for XML path(''))
,JCTQ=(select '第'+replace(topicNumber,'.',''),(select '-'+ testIndex from TestQuestions_Score where TestQuestions_Score_ID=stq.TestQuestions_Score_ID and len(testIndex)<>0),'题，'  from StatsClassHW_TQ stq   where TQ_Score!=-1 and HomeWork_ID=scs.HomeWork_ID and ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and 60>=ScoreAvgRate and ScoreAvgRate>=0 order by TestQuestions_Num,TestQuestions_OrderNum  for XML path(''))
 ,BZStudentName=(
 select StudentName+'，' from (
 select UserId ,isnull(TrueName,UserName) as StudentName from  
 dbo.VW_UserOnClassGradeSchool where ClassId=scs.ClassID and ClassMemberShipEnum='student' 
 and UserId not in( select Student_Id from Student_HomeWork where HomeWork_Id=scs.HomeWork_ID)
 ) a for XML path('') 
)
 ,TJStudentName= ( select UserName+'，' from (
 select isnull( TrueName,UserName) as UserName,shwSubmit.Student_HomeWork_Status from  Student_HomeWork shw 
 inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id 
 inner join f_user fu on fu.UserId=shw.Student_Id
  where shwSubmit.Student_HomeWork_Status=0 and HomeWork_Id=scs.HomeWork_ID  ) a for XML path('') 
  ) 
   ,PGStudentName= ( select UserName+'，' from (
 select isnull( TrueName,UserName) as UserName,shwSubmit.Student_HomeWork_Status from  Student_HomeWork shw 
inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=shw.Student_HomeWork_Id 
inner join Student_HomeWork_Correct shwCorrect on shwCorrect.Student_HomeWork_Id=shw.Student_HomeWork_Id 
 inner join f_user fu on fu.UserId=shw.Student_Id
  where shwCorrect.Student_HomeWork_CorrectStatus=0 and shwSubmit.Student_HomeWork_Status=1  and HomeWork_Id=scs.HomeWork_ID  ) a for XML path('') 
  )
  ,KPName=(select KPName+'、' from StatsClassHW_KP where HomeWork_ID=scs.HomeWork_ID and ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id for XML path(''))
,KPNameCount=(select COUNT(*) from StatsClassHW_KP where HomeWork_ID=scs.HomeWork_ID and ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id)
,YXKPName=(select KPName+'、' from StatsClassHW_KP where HomeWork_ID=scs.HomeWork_ID and ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and 100>=KPScoreAvgRate and KPScoreAvgRate>85   for XML path(''))
,YXKPNameCount=(select COUNT(*) from StatsClassHW_KP where HomeWork_ID=scs.HomeWork_ID and ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and 100>=KPScoreAvgRate and KPScoreAvgRate>85 )
,JHKPName=(select KPName+'、' from StatsClassHW_KP where HomeWork_ID=scs.HomeWork_ID and ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and 85>=KPScoreAvgRate and KPScoreAvgRate>70   for XML path(''))
,JHKPNameCount=(select COUNT(*) from StatsClassHW_KP where HomeWork_ID=scs.HomeWork_ID and ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and 85>=KPScoreAvgRate and KPScoreAvgRate>70 )
,YBKPName=(select KPName+'、' from StatsClassHW_KP where HomeWork_ID=scs.HomeWork_ID and ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and 70>=KPScoreAvgRate and KPScoreAvgRate>60   for XML path(''))
,YBKPNameCount=(select COUNT(*) from StatsClassHW_KP where HomeWork_ID=scs.HomeWork_ID and ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and 70>=KPScoreAvgRate and KPScoreAvgRate>60)
,JCKPName=(select KPName+'、' from StatsClassHW_KP where HomeWork_ID=scs.HomeWork_ID and ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and 60>=KPScoreAvgRate and KPScoreAvgRate>=0  for XML path(''))
,JCKPNameCount=(select COUNT(*) from StatsClassHW_KP where HomeWork_ID=scs.HomeWork_ID and ResourceToResourceFolder_Id=scs.ResourceToResourceFolder_Id and 60>=KPScoreAvgRate and KPScoreAvgRate>=0) 
 from dbo.StatsClassHW_Score scs where HomeWork_ID='" + HomeWork_Id + "' and scs.ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "' and ClassID='" + ClassId + "'";
                DataTable dtStatsClassHW_Score = Rc.Common.DBUtility.DbHelperSQL.Query(Sql).Tables[0];
                if (dtStatsClassHW_Score.Rows.Count > 0)
                {
                    StbHtml.Append("<div class='page_title'>作业基本情况</div><div class='panel mn'><div class='panel-body'>");
                    StbHtml.AppendFormat("<p>（1）本次作业共计<ins>{0}</ins>分，包括{1}{2}{3}{4}。</p>"
                        , dtStatsClassHW_Score.Rows[0]["HW_Score"].ToString().clearLastZero()
                        , dtStatsClassHW_Score.Rows[0]["selectionSum"].ToString() == "" ? "" : string.Format("选择题共<ins>{0}</ins>题，合计<ins>{1}</ins>分，"
                          , dtStatsClassHW_Score.Rows[0]["selectionCount"].ToString()
                          , dtStatsClassHW_Score.Rows[0]["selectionSum"].ToString().clearLastZero())
                        , dtStatsClassHW_Score.Rows[0]["truefalseSum"].ToString() == "" ? "" : string.Format("判断题共<ins>{0}</ins>题，合计<ins>{1}</ins>分，"
                          , dtStatsClassHW_Score.Rows[0]["truefalseCount"].ToString()
                          , dtStatsClassHW_Score.Rows[0]["truefalseSum"].ToString().clearLastZero())
                        , dtStatsClassHW_Score.Rows[0]["fillSum"].ToString() == "" ? "" : string.Format("填空题共<ins>{0}</ins>题，合计<ins>{1}</ins>分，"
                          , dtStatsClassHW_Score.Rows[0]["fillCount"].ToString()
                          , dtStatsClassHW_Score.Rows[0]["fillSum"].ToString().clearLastZero())
                        , dtStatsClassHW_Score.Rows[0]["answersSum"].ToString() == "" ? "" : string.Format("解答题共<ins>{0}</ins>题，合计<ins>{1}</ins>分，"
                          , dtStatsClassHW_Score.Rows[0]["answersCount"].ToString()
                          , dtStatsClassHW_Score.Rows[0]["answersSum"].ToString().clearLastZero())
                          );
                    string StrTemp = StbHtml.ToString();
                    StbHtml = new StringBuilder();
                    StbHtml.Append(StrTemp.Remove(StrTemp.IndexOf('。') - 1, 1));
                    StbHtml.AppendFormat("<p>（2）本次作业共布置<ins>{0}</ins>份{1}，实际提交<ins>{2}</ins>份{3}，已批改<ins>{4}</ins>份{5}。</p>"
                        , dtStatsClassHW_Score.Rows[0]["AssignedCount"].ToString()
                        , dtStatsClassHW_Score.Rows[0]["AssignedCount"].ToString() == dtStatsClassHW_Score.Rows[0]["ClassAllCount"].ToString() ? "" : string.Format("({0}未布置)"
                          , dtStatsClassHW_Score.Rows[0]["BZStudentName"].ToString().TrimEnd('，'))
                        , dtStatsClassHW_Score.Rows[0]["CommittedCount"].ToString()
                        , dtStatsClassHW_Score.Rows[0]["UncommittedCount"].ToString() == "0" ? "" : string.Format("({0}未提交)"
                          , dtStatsClassHW_Score.Rows[0]["TJStudentName"].ToString().TrimEnd('，'))
                        , dtStatsClassHW_Score.Rows[0]["CorrectedCount"].ToString()
                        , dtStatsClassHW_Score.Rows[0]["UnCorrectedCount"].ToString() == "0" ? "" : string.Format("({0}未批改)"
                          , dtStatsClassHW_Score.Rows[0]["PGStudentName"].ToString().TrimEnd('，'))
                        );
                    StbHtml.Append("</div></div>");
                    StbHtml.Append("<div class='page_title'>知识点掌握情况</div><div class='panel mn'><div class='panel-body'>");
                    StbHtml.AppendFormat("<p>本次作业共涉及知识点：{0}，合计({1})个知识点.其中：</p>"
                        , dtStatsClassHW_Score.Rows[0]["KPName"].ToString().TrimEnd('、')
                        , dtStatsClassHW_Score.Rows[0]["KPNameCount"].ToString()
                        );
                    StbHtml.AppendFormat("<p>（1）掌握情况最好的知识点<mark>[85%-100%]</mark>是<ins>【{0}】</ins>，合计{1}个知识点；</p>"
                        , dtStatsClassHW_Score.Rows[0]["YXKPNameCount"].ToString() == "0" ? "--" : dtStatsClassHW_Score.Rows[0]["YXKPName"].ToString().TrimEnd('、')
                        , dtStatsClassHW_Score.Rows[0]["YXKPNameCount"].ToString()
                        );
                    StbHtml.AppendFormat("<p>（2）掌握情况较好的知识点<mark>[70%-85%]</mark>是<ins>【{0}】</ins>，合计<ins>{1}</ins>个知识点；</p>"
                        , dtStatsClassHW_Score.Rows[0]["JHKPNameCount"].ToString() == "0" ? "--" : dtStatsClassHW_Score.Rows[0]["JHKPName"].ToString().TrimEnd('、')
                        , dtStatsClassHW_Score.Rows[0]["JHKPNameCount"].ToString()
                        );
                    StbHtml.AppendFormat("<p>（3）掌握情况一般的知识点<mark>[60%-70%]</mark>是<ins>【{0}】</ins>，合计<ins>{1}</ins>个知识点；</p>"
                       , dtStatsClassHW_Score.Rows[0]["YBKPNameCount"].ToString() == "0" ? "--" : dtStatsClassHW_Score.Rows[0]["YBKPName"].ToString().TrimEnd('、')
                       , dtStatsClassHW_Score.Rows[0]["YBKPNameCount"].ToString()
                       );
                    StbHtml.AppendFormat("<p>（4）掌握情况较差的知识点<mark>[0-60%]</mark>是<ins>【{0}】</ins>，合计<ins>{1}</ins>个知识点；</p>"
                        , dtStatsClassHW_Score.Rows[0]["JCKPNameCount"].ToString() == "0" ? "--" : dtStatsClassHW_Score.Rows[0]["JCKPName"].ToString().TrimEnd('、')
                        , dtStatsClassHW_Score.Rows[0]["JCKPNameCount"].ToString()
                        );
                    StbHtml.Append("</div></div>");
                    StbHtml.Append("<div class='page_title'>小题答题情况</div><div class='panel mn'><div class='panel-body'>");
                    StbHtml.AppendFormat("<p>本次作业共涉及{0}{1}{2}{3}合计{4}分。其中：</p>"
                        , dtStatsClassHW_Score.Rows[0]["selectionSum"].ToString() == "" ? "" : string.Format("选择题共{0}题，"
                           , dtStatsClassHW_Score.Rows[0]["selectionCount"].ToString())
                        , dtStatsClassHW_Score.Rows[0]["truefalseSum"].ToString() == "" ? "" : string.Format("判断题共{0}题，"
                           , dtStatsClassHW_Score.Rows[0]["truefalseCount"].ToString())
                        , dtStatsClassHW_Score.Rows[0]["fillSum"].ToString() == "" ? "" : string.Format("填空题共{0}题，"
                           , dtStatsClassHW_Score.Rows[0]["fillCount"].ToString())
                        , dtStatsClassHW_Score.Rows[0]["answersSum"].ToString() == "" ? "" : string.Format("解答题共{0}题，"
                           , dtStatsClassHW_Score.Rows[0]["answersCount"].ToString())
                        , dtStatsClassHW_Score.Rows[0]["HW_Score"].ToString().clearLastZero()
                          );
                    StbHtml.AppendFormat("<p>（1）答题情况最好的题目<mark>[85%-100%]</mark>是<ins>【{0}】</ins>；</p>"
                       , string.IsNullOrEmpty(dtStatsClassHW_Score.Rows[0]["YXTQ"].ToString()) ? "--" : dtStatsClassHW_Score.Rows[0]["YXTQ"].ToString().TrimEnd('，')
                       );
                    StbHtml.AppendFormat("<p>（2）答题情况较好的题目<mark>[70%-85%]</mark>是<ins>【{0}】</ins>；</p>"
                        , string.IsNullOrEmpty(dtStatsClassHW_Score.Rows[0]["JHTQ"].ToString()) ? "--" : dtStatsClassHW_Score.Rows[0]["JHTQ"].ToString().TrimEnd('，')
                        );
                    StbHtml.AppendFormat("<p>（3）答题情况一般的题目<mark>[60%-70%]</mark>是<ins>【{0}】</ins>；</p>"
                       , string.IsNullOrEmpty(dtStatsClassHW_Score.Rows[0]["YBTQ"].ToString()) ? "--" : dtStatsClassHW_Score.Rows[0]["YBTQ"].ToString().TrimEnd('，')
                       );
                    StbHtml.AppendFormat("<p>（4）答题情况较差的题目<mark>[0-60%]</mark>是<ins>【{0}】</ins>；</p>"
                        , string.IsNullOrEmpty(dtStatsClassHW_Score.Rows[0]["JCTQ"].ToString()) ? "--" : dtStatsClassHW_Score.Rows[0]["JCTQ"].ToString().TrimEnd('，')
                        );
                    StbHtml.Append("</div></div>");
                }
                string StrSql = @"select  st.HWScoreLevelCount,st.HWScoreLevelCountRate,st.HWScoreLevelRateLeft,st.HWScoreLevelRateRight,HWScoreLevelName
,StudentName=(select StudentName+'('+convert(varchar(100),StudentScore) +'分)，' from StatsClassStudentHW_Score where HWScoreLevelName=st.HWScoreLevelName and st.HomeWork_ID=HomeWork_ID and st.ResourceToResourceFolder_Id=ResourceToResourceFolder_Id  and st.ClassID=ClassID  order by StudentScore for XML path(''))
 from StatsClassHW_ScoreLevel st where st.HomeWork_ID='" + HomeWork_Id + "' and st.ResourceToResourceFolder_Id='" + ResourceToResourceFolder_Id + "'  and st.ClassID='" + ClassId + "' order by HWScoreLevelRight desc";
                DataTable dtStatsClassHW_ScoreLevel = Rc.Common.DBUtility.DbHelperSQL.Query(StrSql).Tables[0];
                if (dtStatsClassHW_ScoreLevel.Rows.Count > 0)
                {
                    StbHtml.Append("<div class='page_title'>成绩情况</div><div class='panel mn'><div class='panel-body'>");
                    if (dtStatsClassHW_Score.Rows.Count > 0)
                    {
                        StbHtml.AppendFormat("<p>本次作业中班级最高分为<ins>{0}</ins>分，最低分为<ins>{1}</ins>分，平均分为<ins>{2}</ins>分，试卷标准差为<ins>{3}</ins>。本次作业成绩情况：</p>"
                            , dtStatsClassHW_Score.Rows[0]["HighestScore"].ToString().clearLastZero()
                            , dtStatsClassHW_Score.Rows[0]["LowestScore"].ToString().clearLastZero()
                            , dtStatsClassHW_Score.Rows[0]["AVGScore"].ToString().clearLastZero()
                            , dtStatsClassHW_Score.Rows[0]["StandardDeviation"].ToString().clearLastZero()
                            );
                    }
                    for (int i = 0; i < dtStatsClassHW_ScoreLevel.Rows.Count; i++)
                    {
                        StbHtml.AppendFormat("<p>（{0}）{1}<mark>[{2}%，{3}%]</mark>，<ins>{4}</ins>人，占比<ins>{5}%</ins>，学生为：<ins>{6}</ins>；</p>"
                            , (i + 1).ToString()
                            , dtStatsClassHW_ScoreLevel.Rows[i]["HWScoreLevelName"].ToString()
                            , dtStatsClassHW_ScoreLevel.Rows[i]["HWScoreLevelRateLeft"].ToString().clearLastZero()
                            , dtStatsClassHW_ScoreLevel.Rows[i]["HWScoreLevelRateRight"].ToString().clearLastZero()
                            , dtStatsClassHW_ScoreLevel.Rows[i]["HWScoreLevelCount"].ToString().clearLastZero()
                            , dtStatsClassHW_ScoreLevel.Rows[i]["HWScoreLevelCountRate"].ToString().clearLastZero()
                            , string.IsNullOrEmpty(dtStatsClassHW_ScoreLevel.Rows[i]["StudentName"].ToString()) ? "--" : dtStatsClassHW_ScoreLevel.Rows[i]["StudentName"].ToString().TrimEnd('，')
                            );
                    }

                    StbHtml.Append("</div></div>");
                }
                ltlHtml.Text = StbHtml.ToString();
            }
            catch (Exception)
            {

            }
        }
    }
}