using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using System.Data;
using Rc.BLL.Resources;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class TestPaper_FrameDetail : Rc.Cloud.Web.Common.InitPage
    {
        public string TestPaper_Frame_Id = string.Empty;
        public string TestPaper_Frame_Name = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            TestPaper_Frame_Id = Request["TestPaper_Frame_Id"].Filter();
            TestPaper_Frame_Name = Server.UrlDecode(Request["TestPaper_Frame_Name"]);
            Module_Id = "30100400";
            if (!IsPostBack)
            {

                ltlName.Text = TestPaper_Frame_Name;
            }
        }

        [WebMethod]
        public static string GetList(string TestPaper_Frame_Id)
        {
            try
            {
                string Str = string.Empty;
                string SqlBig = @"select TestPaper_FrameDetail_Id,TestPaper_Frame_Id,ParentId,TestQuestions_Num,TestQuestions_NumStr,TestQuestions_Type from TestPaper_FrameDetail where TestPaper_Frame_Id='" + TestPaper_Frame_Id + "' and ParentId='0' order by TestQuestions_Num,CreateTime";
                string TempBig = "<tr><td colspan=\"8\"><b>{0}</b></td><td class='opera'><a href=\"javascript:EditData('{1}');\">修改</a>&nbsp;<a href=\"javascript:AddSmall('{1}','{2}');\">添加小题</a>&nbsp;<a href=\"javascript:DeleteBigData('{1}');\">删除</a></td></tr>";
                string TempSmall = "<tr><td>{0}</td><td>{1}</td><td>{9}</td><td>{7}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td class='opera'><a href=\"javascript:IntoTQ('{8}','{6}','{0}');\">导入试题</a>&nbsp;<a href=\"javascript:UpdateSmall('{6}');\">修改</a>&nbsp;<a href=\"javascript:DeleteSmallData('{6}','{8}');\">删除</a></td></tr>";
                DataTable dtBig = Rc.Common.DBUtility.DbHelperSQL.Query(SqlBig).Tables[0];
                if (dtBig.Rows.Count > 0)
                {
                    foreach (DataRow itemBig in dtBig.Rows)
                    {
                        Str += string.Format(TempBig, itemBig["TestQuestions_NumStr"].ToString(), itemBig["TestPaper_FrameDetail_Id"].ToString(), itemBig["TestQuestions_Type"].ToString());
                        string SqlSmall = @"select t.*,cd.D_Name from TestPaper_FrameDetail t
                                            left  join Common_Dict cd on cd.Common_Dict_ID=t.TestQuestionType_Web where TestPaper_Frame_Id='" + TestPaper_Frame_Id + "' and ParentId='" + itemBig["TestPaper_FrameDetail_Id"].ToString() + "' order by TestQuestions_Num,CreateTime";
                        DataTable dtSmall = Rc.Common.DBUtility.DbHelperSQL.Query(SqlSmall).Tables[0];
                        if (dtSmall.Rows.Count > 0)
                        {
                            foreach (DataRow itemSmall in dtSmall.Rows)
                            {

                                Str += string.Format(TempSmall
                                    , itemSmall["TestQuestions_NumStr"].ToString()
                                    , Rc.Common.EnumService.GetDescription<Rc.Model.Resources.TestQuestions_Type>(itemSmall["TestQuestions_Type"].ToString())
                                    , GetAttr(itemSmall["TestPaper_FrameDetail_Id"].ToString(), "2").TrimEnd(',')
                                    , GetAttr(itemSmall["TestPaper_FrameDetail_Id"].ToString(), "1").TrimEnd(',')
                                    , GetAttr(itemSmall["TestPaper_FrameDetail_Id"].ToString(), "3").TrimEnd(',')
                                    , itemSmall["Score"].ToString().clearLastZero()
                                    , itemSmall["TestPaper_FrameDetail_Id"].ToString()
                                    , itemSmall["TestQuestions_Num"].ToString()
                                    , TestPaper_Frame_Id
                                    , itemSmall["D_Name"].ToString());
                            }
                        }
                    }
                    return Str;
                }
                else
                {
                    return "<tr><td colspan=\"100\" style=\"text-align:center\">暂无数据</td></tr>";
                }
            }
            catch (Exception ex)
            {

                return "1";
            }
        }
        public static string GetAttr(string TestPaper_FrameDetail_Id, string type)
        {
            try
            {
                string sql = "select * from [TestPaper_FrameDetailToTestQuestions_Attr] where TestPaper_FrameDetail_Id='" + TestPaper_FrameDetail_Id.Filter() + "' and Attr_Type='" + type.Filter() + "'";
                DataTable dtAttr = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                string Str = string.Empty;
                if (dtAttr.Rows.Count > 0)
                {
                    foreach (DataRow item in dtAttr.Rows)
                    {
                        if (type == "3")
                        {
                            Str += item["Attr_Value"] + ",";
                        }
                        else
                        {
                            Str += item["Attr_Value"] + "</br>";
                        }
                    }
                }
                return Str;

            }
            catch (Exception ex)
            {
                return "";
            }
        }
        [WebMethod]
        public static string DeleteBigData(string ParentId)
        {
            try
            {
                if (!string.IsNullOrEmpty(ParentId.Filter()))
                {
                    BLL_TestPaper_FrameDetail bll = new BLL_TestPaper_FrameDetail();
                    if (bll.GetRecordCount("ParentId='" + ParentId + "'") > 0)
                    {
                        return "2";
                    }
                    else
                    {
                        if (bll.Delete(ParentId))
                        {
                            return "1";
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                return "";
            }
        }
        [WebMethod]
        public static string DeleteSmallData(string TestPaper_FrameDetail_Id, string TestPaper_Frame_Id)
        {
            try
            {
                TestPaper_FrameDetail_Id = TestPaper_FrameDetail_Id.Filter();
                TestPaper_Frame_Id = TestPaper_Frame_Id.Filter();
                if (!string.IsNullOrEmpty(TestPaper_FrameDetail_Id) && !string.IsNullOrEmpty(TestPaper_Frame_Id))
                {
                    if (new BLL_TestPaper_FrameToTestpaper().GetRecordCount("TestPaper_Frame_Id='" + TestPaper_Frame_Id + "'") > 0)
                    {
                        return "2";
                    }
                    if (new BLL_TestPaper_FrameToTestpaper().GetRecordCount("TestPaper_Frame_Id='" + TestPaper_FrameDetail_Id + "'") > 0)
                    {
                        return "3";
                    }
                    else
                    {
                        BLL_TestPaper_FrameDetail bll = new BLL_TestPaper_FrameDetail();

                        if (bll.Delete(TestPaper_FrameDetail_Id))
                        {
                            return "1";
                        }
                        else
                        {
                            return "";
                        }
                    }
                }

                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}