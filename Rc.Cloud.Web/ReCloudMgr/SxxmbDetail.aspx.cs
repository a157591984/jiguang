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
using Rc.Model.Resources;
using Rc.Cloud.Model;
using System.Text;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class SxxmbDetail : Rc.Cloud.Web.Common.InitPage
    {
        public string Two_WayChecklist_Id = string.Empty;
        public string Two_WayChecklist_Name = string.Empty;
        public string TestPaper_Frame_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {


            Two_WayChecklist_Id = Request["Two_WayChecklist_Id"].Filter();
            Two_WayChecklist_Name = Server.UrlDecode(Request["Two_WayChecklist_Name"]);
            TestPaper_Frame_Id = Request["TestPaper_Frame_Id"].Filter();
            Module_Id = "30100400";
            if (!IsPostBack)
            {
                ltlName.Text = Two_WayChecklist_Name;

            }
        }

        [WebMethod]
        public static string GetList(string Two_WayChecklist_Id)
        {
            try
            {

                string Str = string.Empty;
                string SqlBig = @"select Two_WayChecklistDetail_Id,Two_WayChecklist_Id,ParentId,TestQuestions_Num,TestQuestions_NumStr,TestQuestions_Type from Two_WayChecklistDetail where Two_WayChecklist_Id='" + Two_WayChecklist_Id + "' and ParentId='0' order by TestQuestions_Num,CreateTime";
                string TempBig = "<tr><td colspan=\"8\"><b>{0}</b></td><td class='opera'><a href=\"javascript:EditData('{1}');\">修改</a></td></tr>";
                string TempSmall = "<tr><td>{0}</td><td>{1}</td><td>{7}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td id=\"TQ_{6}\">{8}</td><td class='opera'><a href=\"javascript:UpdateSmall('{6}');\">修改</a></td></tr>";
                DataTable dtBig = Rc.Common.DBUtility.DbHelperSQL.Query(SqlBig).Tables[0];
                DataTable dtAttr = new BLL_Two_WayChecklistDetailToAttr().GetList("Two_WayChecklist_Id='" + Two_WayChecklist_Id + "'").Tables[0];
                if (dtBig.Rows.Count > 0)
                {
                    foreach (DataRow itemBig in dtBig.Rows)
                    {
                        Str += string.Format(TempBig, itemBig["TestQuestions_NumStr"].ToString(), itemBig["Two_WayChecklistDetail_Id"].ToString(), itemBig["TestQuestions_Type"].ToString());
                        string SqlSmall = @"select t.* ,t3.D_Name from Two_WayChecklistDetail t
                                            left join TestPaper_FrameDetail t1 on t1.TestPaper_FrameDetail_Id =t.TestPaper_FrameDetail_Id 
                                            left join Common_Dict t3 on t3.Common_Dict_ID = t1.TestQuestionType_Web where t.Two_WayChecklist_Id='" + Two_WayChecklist_Id + "' and t.ParentId='" + itemBig["Two_WayChecklistDetail_Id"].ToString() + "' order by t.TestQuestions_Num,t.CreateTime";
                        DataTable dtSmall = Rc.Common.DBUtility.DbHelperSQL.Query(SqlSmall).Tables[0];
                        if (dtSmall.Rows.Count > 0)
                        {
                            foreach (DataRow itemSmall in dtSmall.Rows)
                            {

                                Str += string.Format(TempSmall
                                    , itemSmall["TestQuestions_NumStr"].ToString()
                                    , itemSmall["D_Name"].ToString()
                                    , GetAttr(dtAttr, itemSmall["Two_WayChecklistDetail_Id"].ToString(), "2")
                                    , GetAttr(dtAttr, itemSmall["Two_WayChecklistDetail_Id"].ToString(), "1")
                                    , GetAttr(dtAttr, itemSmall["Two_WayChecklistDetail_Id"].ToString(), "3")
                                    , itemSmall["Score"].ToString().clearLastZero()
                                    , itemSmall["Two_WayChecklistDetail_Id"].ToString()
                                    , itemSmall["TestQuestions_Num"].ToString()
                                    , GetQuestions(itemSmall["Two_WayChecklistDetail_Id"].ToString()));
                            }
                        }
                    }
                    return Str + "<span class=\"tag_add\" data-name=\"addKnowledge\" data-BigId=\"\" data-SmallId=\"\" data-question=\"1\">+</span>";
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
        public static string GetAttr(DataTable dtAttr, string Two_WayChecklistDetail_Id, string type)
        {
            try
            {
                string AttrTemp = "<span class=\"tag\">{0}<i data-name=\"removeKnowledge\" data-twoid=\"{2}\" data-question=\"{1}\">&times;</i></span>";
                StringBuilder stbHtml = new StringBuilder();
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
        /// 删除知识点测量目标难易度
        /// </summary>
        /// <param name="Two_WayChecklistDetailToAttr_Id"></param>
        /// <returns></returns>

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
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["loginUser"];
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
                        , loginUser.SysUser_ID
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



        [WebMethod]
        public static string GetQuestions(string Two_WayChecklistDetail_Id)
        {
            try
            {
                Two_WayChecklistDetail_Id = Two_WayChecklistDetail_Id.Filter();
                int i = new BLL_Two_WayChecklistDetailToTestQuestions().GetRecordCount("Two_WayChecklistDetail_Id='" + Two_WayChecklistDetail_Id + "'");
                if (i > 0)
                {
                    return i.ToString();

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

    }
}