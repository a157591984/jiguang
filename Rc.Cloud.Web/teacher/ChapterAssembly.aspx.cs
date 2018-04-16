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
using Newtonsoft.Json;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using System.Threading;
namespace Rc.Cloud.Web.teacher
{
    public partial class ChapterAssembly : Rc.Cloud.Web.Common.FInitData
    {
        public string Subject = string.Empty;
        public string Complext = string.Empty;
        public string Identifier_Id = string.Empty;
        public string strUserGroup_IdActivity = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strUserGroup_IdActivity = Request["ugid"];
            Subject = FloginUser.Subject;
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
            if (string.IsNullOrEmpty(Subject) || Subject == "-1")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "err", "<script type='text/javascript'>layer.msg('您没有设置学科，无法进行组卷，请去个人中心设置学科之后再进行章节组卷。',{icon:2,time:5000},function(){window.location.href='basicSetting.aspx';});</script>");
                return;
            }
            if (!IsPostBack)
            {
                Identifier_Id = Guid.NewGuid().ToString();
                #region 根据字典关系加载年级学期
                string strSql = @"select distinct t2.DictRelation_Id,t2.Parent_Id,t3.D_Name,t3.D_Order from DictRelation t 
inner join DictRelation_Detail t2 on t2.DictRelation_Id=t.DictRelation_Id
inner join Common_Dict t3 on t3.Common_Dict_Id=t2.Parent_Id
where t.HeadDict_Id='722CE025-A876-4880-AAC1-5E416F3BDB1E' and t.SonDict_Id='934A3541-116E-438C-B9BA-4176368FCD9B'
order by t3.D_Order ";
                DataTable dtDict = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                rptGradeTerm.DataSource = dtDict;
                rptGradeTerm.DataBind();
                #endregion
                #region 加载题型
                LoadTQ_Type();
                #endregion
                #region 难易度串
                string sql = "select *from [Common_Dict] where D_type='25' order by D_Order";
                DataTable dtC = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                List<object> listResult = new List<object>();
                if (dtC.Rows.Count > 0)
                {
                    string Structure = string.Empty;
                    Structure += "<div class='col-xs-2 col-xs-offset-1'>试题总数</div>";
                    foreach (DataRow item in dtC.Rows)
                    {
                        //Complext += item["D_Name"].ToString() + "&" + item["Common_Dict_ID"].ToString() + "|";
                        listResult.Add(new
                        {
                            name = item["D_Name"].ToString(),
                            id = item["Common_Dict_ID"].ToString()
                        });
                        Structure += string.Format("<div class='col-xs-1'>{0}</div>"
                            , item["D_Name"].ToString());
                    }
                    //Complext = Complext.TrimEnd('|');
                    Complext = JsonConvert.SerializeObject(listResult);
                    ltlStructure.Text = Structure;
                }

                #endregion
            }
        }
        /// <summary>
        /// 根据学科加载题型
        /// </summary>
        public void LoadTQ_Type()
        {
            string str = string.Empty;

            try
            {
                string temp1 = "";
                string temp = "<label><input type=\"checkbox\" value=\"{0}\" name=\"question_type\" data-index='{3}' data-name=\"{1}\" {2} />{1}</label>";
                string sql = string.Format(@"select t.Dict_Id,t3.D_Name,t3.D_Order from DictRelation_Detail t 
inner join DictRelation t2 on t2.DictRelation_Id=t.DictRelation_Id
inner join Common_Dict t3 on t3.Common_Dict_Id=t.Dict_Id 
where t2.HeadDict_Id='{0}' and t2.SonDict_Id='{1}' and t.Parent_Id='{2}'
order by t3.D_Order ", "934A3541-116E-438C-B9BA-4176368FCD9B", "59254430-B8EA-4186-96E1-A9B923D4AE61", Subject);
                DataTable dtTQ_Type = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dtTQ_Type.Rows.Count > 0)
                {
                    var index = 0;
                    foreach (DataRow dr in dtTQ_Type.Rows)
                    {
                        str += string.Format(temp
                            , dr["Dict_Id"].ToString()
                            , dr["D_Name"].ToString()
                            , IsChecked(dr["Dict_Id"].ToString()) ? "checked" : ""
                            , index++);

                    }

                }
            }
            catch (Exception ex)
            {
                str = "";
            }
            ltlTQ_Type.Text = str;
        }
        /// <summary>
        /// 验证是否是选择题填空题简单题
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool IsChecked(string Id)
        {
            try
            {
                bool TF = false;
                string[] arr = { "20179dee-5e11-4f73-934b-2602e41df493", "19b77ea8-c6be-4de5-91b5-a57c0b0f619e", "44260cc7-15d1-459e-885c-555e77889767" };
                foreach (var item in arr)
                {
                    if (item.Contains(Id))
                    {
                        TF = true;
                    }
                }
                return TF;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 加载教材版本和课本
        /// </summary>
        /// <param name="HeadDict_Id"></param>
        /// <param name="SonDict_Id"></param>
        /// <param name="Parent_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetSubDictList(string HeadDict_Id, string SonDict_Id, string Parent_Id)
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
                foreach (DataRow item in dt.Rows)
                {
                    stbHtml.AppendFormat("<a href=\"##\" ajax-value=\"{0}\" ajax-text=\"{1}\">{1}</a>", item["Dict_Id"].ToString(), item["D_Name"].ToString());
                }

                return stbHtml.ToString();

            }
            catch (Exception ex)
            {
                return stbHtml.ToString();
            }
        }

        /// <summary>
        /// 加载知识点数据
        /// </summary>
        /// <param name="Subject"></param>
        /// <param name="BookType"></param>
        /// <param name="CheckedKPId"></param>
        /// <param name="StuId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetKPList(string Subject, string BookType, string GradeTerm, string Resource_Version)
        {
            StringBuilder stbHtml = new StringBuilder();
            try
            {
                Subject = Subject.Filter();
                BookType = BookType.Filter();
                GradeTerm = GradeTerm.Filter();
                Resource_Version = Resource_Version.Filter();
                string strSql = string.Format(@"select kp.*,kpb.KPNameBasic from [dbo].[S_KnowledgePoint] kp
left join [dbo].[S_KnowledgePointBasic] kpb on kpb.S_KnowledgePointBasic_Id=kp.S_KnowledgePointBasic_Id where kp.Subject='{0}' and kp.GradeTerm='{1}' and kp.Resource_Version='{2}' and kp.Book_Type='{3}' order by kp.KPCode "
                    , Subject
                    , GradeTerm
                    , Resource_Version
                    , BookType);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                stbHtml.Append(InitKPTree("0", 1, dt));
            }
            catch (Exception)
            {

            }
            return stbHtml.ToString();
        }

        protected static StringBuilder InitKPTree(string ParentId, int level, DataTable dt)
        {
            StringBuilder strHtml = new StringBuilder();
            DataRow[] dr = dt.Select("Parent_Id='" + ParentId + "'");
            int num = 0;

            if (dr.Length > 0)
            {
                int subProcess = 0;
                int subMax = dr.Length;
                string liClass = string.Empty;
                foreach (DataRow drv in dr)
                {
                    subProcess++;
                    if (subProcess == 1)
                    {

                        strHtml.AppendFormat("<ul>");

                    }
                    strHtml.Append("<li>");
                    strHtml.AppendFormat("<div class='mtree_link mtree-link-hook' data-id='{0}'>"
                        , drv["S_KnowledgePoint_Id"].ToString());
                    strHtml.Append("<div class='mtree_indent mtree-indent-hook'></div>");
                    strHtml.Append("<div class='mtree_btn mtree-btn-hook'></div>");
                    strHtml.Append("<div class=\"mtree_checkbox mtree-checkbox-hook\">");
                    strHtml.AppendFormat("<input type=\"checkbox\" name=\"name\" tt=\"{1}\" value=\"{0}\" />"
                        , drv["S_KnowledgePoint_Id"].ToString()
                        , drv["IsLast"].ToString());
                    strHtml.Append("</div>");
                    strHtml.AppendFormat("<div class='mtree_name mtree-name-hook'>{0}</div>"
                        , !string.IsNullOrEmpty(drv["KPName"].ToString()) ? drv["KPName"].ToString() : drv["KPNameBasic"].ToString());
                    strHtml.Append("</div>");
                    strHtml.Append(InitKPTree(drv["S_KnowledgePoint_Id"].ToString(), level + 1, dt));
                    strHtml.Append("</li>");
                    if (subProcess == subMax)
                    {
                        strHtml.Append("</ul>");
                    }

                }
            }
            return strHtml;
        }
        [WebMethod]
        public static string Submit(string arryKP, string arryAttr, string identifier, string arrTQType, string totalCountTQ)
        {
            try
            {
                string strKP = string.Empty;
                arryKP = arryKP.Filter();
                arryAttr = arryAttr.Filter();
                identifier = identifier.Filter();
                arrTQType = arrTQType.Filter();
                totalCountTQ = totalCountTQ.Filter();
                for (int i = 0; i <2000; i++)
                {
                    Rc.Cloud.Web.Common.pfunction.WriteToFile(HttpContext.Current.Server.MapPath("/1.txt"), i.ToString(), false);
                }
                int row = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("exec dbo.P_ChapterAssembly '" + arryKP + "','" + arryAttr + "','" + identifier + "','" + arrTQType + "' ", 7200);
                int j =Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("exec P_ChangeQuestions '" + identifier + "'", 7200);
                if (row > 0 && j>0)
                {
                    if (new BLL_ChapterAssembly_TQ().GetRecordCount("TestQuestions_Id<>'' and Identifier_Id='" + identifier + "'") == Convert.ToInt32(totalCountTQ))
                    {
                        return "1";
                    }
                    else
                    {
                        return "2";
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