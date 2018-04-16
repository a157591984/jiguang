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
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
using System.Text;

namespace Rc.Cloud.Web.Sys
{
    public partial class MeasureTargetList : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "30300100";
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
                    stbHtml.AppendFormat("<a href=\"##\" ajax-value=\"{0}\">{1}</a>", item["Dict_Id"].ToString(), item["D_Name"].ToString());
                }

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
        public static string GetDataList(string Name, string GradeTerm, string Subject, string Resource_Version, int PageIndex, int PageSize)
        {
            try
            {
                BLL_S_MeasureTarget bll = new BLL_S_MeasureTarget();
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " Parent_Id='0' ";
                if (!string.IsNullOrEmpty(Name)) strWhere += " and (MTName like '%" + Name.Filter() + "%' or MTCode like '%" + Name.Filter() + "%') ";
                if (!string.IsNullOrEmpty(GradeTerm)) strWhere += " and GradeTerm='" + GradeTerm.Filter() + "' ";
                if (!string.IsNullOrEmpty(GradeTerm)) strWhere += " and Subject='" + Subject.Filter() + "' ";
                if (!string.IsNullOrEmpty(GradeTerm)) strWhere += " and Resource_Version='" + Resource_Version.Filter() + "' ";

                dt = bll.GetListByPageJoinDict(strWhere, "MTCode", ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = bll.GetRecordCount(strWhere);
                DataTable dtAll = bll.GetList("").Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow[] drSub = dtAll.Select("Parent_Id='" + dt.Rows[i]["S_MeasureTarget_Id"].ToString() + "'");
                    listReturn.Add(new
                    {
                        S_MeasureTarget_Id = dt.Rows[i]["S_MeasureTarget_Id"].ToString(),
                        Parent_Id = dt.Rows[i]["Parent_Id"].ToString().Trim(),
                        MTName = dt.Rows[i]["MTName"].ToString(),
                        MTCode = dt.Rows[i]["MTCode"].ToString(),
                        MTLevel = dt.Rows[i]["MTLevel"].ToString(),
                        MTLevelName = dt.Rows[i]["MTLevelName"].ToString(),
                        parentIdStr = "",
                        paddingLeft = "",
                        hasChildren = drSub.Length
                    });
                }

                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = rCount,
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
        public static string GetSubDataList(string parentId, string parentIdStr)
        {
            try
            {
                BLL_S_MeasureTarget bll = new BLL_S_MeasureTarget();
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " Parent_Id='" + parentId + "' ";
                dt = bll.GetListJoinDict(strWhere, "MTCode").Tables[0];
                DataTable dtAll = bll.GetList("").Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow[] drSub = dtAll.Select("Parent_Id='" + dt.Rows[i]["S_MeasureTarget_Id"].ToString() + "'");
                    listReturn.Add(new
                    {
                        S_MeasureTarget_Id = dt.Rows[i]["S_MeasureTarget_Id"].ToString(),
                        Parent_Id = dt.Rows[i]["Parent_Id"].ToString(),
                        MTName = dt.Rows[i]["MTName"].ToString(),
                        MTCode = dt.Rows[i]["MTCode"].ToString(),
                        MTLevel = dt.Rows[i]["MTLevel"].ToString(),
                        MTLevelName = dt.Rows[i]["MTLevelName"].ToString(),
                        parentIdStr = parentIdStr,
                        paddingLeft = 15 * (parentIdStr.Split('&').Length - 1),
                        hasChildren = drSub.Length
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
        public static string DeleteData(string S_MeasureTarget_Id)
        {
            try
            {
                if (new BLL_S_MeasureTarget().GetRecordCount("Parent_Id='" + S_MeasureTarget_Id.Filter() + "'") > 0)
                {
                    return "2";
                }
                else
                {
                    if (new BLL_S_MeasureTarget().Delete(S_MeasureTarget_Id.Filter()))
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }
    }
}