using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using System.Web.Services;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
using Rc.BLL.Resources;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class TestPaper_FrameView : Rc.Cloud.Web.Common.InitPage
    {
        public string TestPaper_Frame_Id = string.Empty;
        public string TestPaper_Frame_Name = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            TestPaper_Frame_Id = Request["TestPaper_Frame_Id"].Filter();
            TestPaper_Frame_Name = Server.UrlDecode(Request["TestPaper_Frame_Name"].Filter());
            if (!IsPostBack)
            {
                ltlName.Text = TestPaper_Frame_Name;
                if (!string.IsNullOrEmpty(TestPaper_Frame_Id))
                {
                    LoadData();
                }
            }
        }
        private void LoadData()
        {
            try
            {
                //选择题【N】道，填空题【N】道，解答题【N】道，
                string Temp = "<p style=\"margin:0\">共【{0}】道习题，{1}{2}{3}{4}{5}共【{6}】分</p>";
                string StrSql = string.Format(@"select  CountSelect=(select count(*) from TestPaper_FrameDetail where TestPaper_Frame_Id ='{0}' and ParentId<>'0' and TestQuestions_Type='selection')
,CountFill=(select count(*) from TestPaper_FrameDetail where TestPaper_Frame_Id ='{0}' and ParentId<>'0' and TestQuestions_Type='fill')
,CountTruefasle=(select count(*) from TestPaper_FrameDetail where TestPaper_Frame_Id ='{0}' and ParentId<>'0' and TestQuestions_Type='truefalse')
,CountAnwser=(select count(*) from TestPaper_FrameDetail where TestPaper_Frame_Id ='{0}' and ParentId<>'0' and TestQuestions_Type='answers')
,CountComplex=(select count(*) from TestPaper_FrameDetail where TestPaper_Frame_Id ='{0}' and ParentId<>'0' and TestQuestions_Type='complex')
,SumScore=(select sum(Score) from TestPaper_FrameDetail where TestPaper_Frame_Id ='{0}' and ParentId<>'0')
,CountQuestions=(select count(*) from TestPaper_FrameDetail where TestPaper_Frame_Id ='{0}' and ParentId<>'0')", TestPaper_Frame_Id);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(StrSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ltlTitle.Text = string.Format(Temp
                        , dt.Rows[0]["CountQuestions"]
                        , dt.Rows[0]["CountSelect"].ToString() == "0" ? "" : "选择题【" + dt.Rows[0]["CountSelect"].ToString() + "】道,"
                        , dt.Rows[0]["CountFill"].ToString() == "0" ? "" : "填空题【" + dt.Rows[0]["CountFill"].ToString() + "】道,"
                        , dt.Rows[0]["CountTruefasle"].ToString() == "0" ? "" : "判断题【" + dt.Rows[0]["CountTruefasle"].ToString() + "】道,"
                        , dt.Rows[0]["CountAnwser"].ToString() == "0" ? "" : "解答题【" + dt.Rows[0]["CountAnwser"].ToString() + "】道,"
                        , dt.Rows[0]["CountComplex"].ToString() == "0" ? "" : "综合题【" + dt.Rows[0]["CountComplex"].ToString() + "】道,"
                        , dt.Rows[0]["SumScore"].ToString().clearLastZero());
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [WebMethod]
        public static string GetSxxmbList(string TestPaper_Frame_Id)
        {
            try
            {

                #region 双向细目表
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                //string strSql = string.Empty;
                //string strSqlCount = string.Empty;
                //strSql = @"select * from TestPaper_FrameDetail where TestPaper_Frame_Id ='" + TestPaper_Frame_Id + "' and ParentId<>'0' order by TestQuestions_Num,CreateTime";
                dt = new BLL_TestPaper_FrameDetail().GetList("TestPaper_Frame_Id ='" + TestPaper_Frame_Id + "' and ParentId<>'0' order by TestQuestions_Num,CreateTime").Tables[0]; //Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int inum = 0;
                DataTable dtAttr = new BLL_TestPaper_FrameDetailToTestQuestions_Attr().GetList("TestPaper_Frame_Id='" + TestPaper_Frame_Id + "'").Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        TestQuestions_NumStr = dt.Rows[i]["TestQuestions_NumStr"].ToString(),
                        TestQuestions_Type = Rc.Common.EnumService.GetDescription<Rc.Model.Resources.TestQuestions_Type>(dt.Rows[i]["TestQuestions_Type"].ToString()),
                        KnowledgePoint = GetAttr(dtAttr, dt.Rows[i]["TestPaper_FrameDetail_Id"].ToString(), "1").TrimEnd(','),
                        TargetText = GetAttr(dtAttr, dt.Rows[i]["TestPaper_FrameDetail_Id"].ToString(), "2").TrimEnd(','),
                        ComplexityText = GetAttr(dtAttr, dt.Rows[i]["TestPaper_FrameDetail_Id"].ToString(), "3").TrimEnd(','),
                        Score = dt.Rows[i]["Score"].ToString().clearLastZero(),
                    });
                }
                #endregion

                if (inum > 0)
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
                    err = "error"// ex.Message.ToString()
                });
            }
        }

        public static string GetAttr(DataTable dtAttr, string TestPaper_FrameDetail_Id, string type)
        {
            try
            {
                string temp = string.Empty;
                DataRow[] drAttr = dtAttr.Select("TestPaper_FrameDetail_Id='" + TestPaper_FrameDetail_Id + "' and Attr_Type='" + type + "'");
                foreach (DataRow item in drAttr)
                {
                    if (type == "3")
                    {
                        temp += item["Attr_Value"] + ",";
                    }
                    else
                    {
                        temp += item["Attr_Value"] + "</br>";
                    }
                }
                return temp;

            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}