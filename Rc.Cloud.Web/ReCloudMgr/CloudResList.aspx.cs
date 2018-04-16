using Newtonsoft.Json;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class CloudResList : Rc.Cloud.Web.Common.InitPage
    {
        public string UserList = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserList = Request["UserList"].Filter();
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //教材版本
                strWhere = " D_Type='3' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, dt, "D_Name", "Common_Dict_ID", "教材版本");
                //文档类型
                strWhere = string.Format(" D_Type='1' AND Common_Dict_ID!='{0}' order by d_order", Rc.Common.Config.Resource_TypeConst.按属性生成的目录);
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Type, dt, "D_Name", "Common_Dict_ID", "文档类型");
                //年级学期
                strWhere = " D_Type='6' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeTerm, dt, "D_Name", "Common_Dict_ID", "年级学期");
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubjects, dt, "D_Name", "Common_Dict_ID", "学科");
            }
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetReList(string GradeTerm, string Subjects, string Resource_Version, string Resource_Type, string ReName, string userid, string Status, int PageSize, int PageIndex)
        {
            try
            {
                GradeTerm = GradeTerm.Filter();
                Subjects = Subjects.Filter();
                Resource_Version = Resource_Version.Filter();
                Resource_Type = Resource_Type.Filter();
                ReName = ReName.Filter();
                userid = userid.Filter();
                #region 资源信息
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strResource_Class = Rc.Common.Config.Resource_ClassConst.云资源;
                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(ReName)) strWhere += " and ResourceFolder_Name like '%" + ReName.Filter() + "%' ";
                if (strResource_Class != "-1") strWhere += " and Resource_Class = '" + strResource_Class.Filter() + "' ";
                if (Resource_Type != "-1") strWhere += " and Resource_Type = '" + Resource_Type.Filter() + "' ";
                if (GradeTerm != "-1") strWhere += " and GradeTerm = '" + GradeTerm.Filter() + "' ";
                if (Subjects != "-1") strWhere += " and Subject = '" + Subjects.Filter() + "' ";
                if (Resource_Version != "-1") strWhere += " and Resource_Version = '" + Resource_Version.Filter() + "' ";
                //if (tp == "0") strWhere += " and ResourceFolder_Id='" + ResourceFolder_Id.Filter() + "' ";

                strWhere += " AND A.ResourceFolder_Level=5 and bk.BookShelvesState='1' ";//管理员维护的书籍目录
                
                if (userid.Split(',').Length > 1)
                {
                    strSqlCount = @"select count(*) from ResourceFolder A left JOIN Bookshelves bk on bk.ResourceFolder_Id=A.ResourceFolder_Id  where 1=1 " + strWhere + " ";
                    strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.CreateTime DESC) row,A.*
,1 UserBuyResources_Id
,t1.D_Name as GradeTermName,t2.D_Name as SubjectName,t3.D_Name as Resource_VersionName,t4.D_Name as Resource_TypeName
from ResourceFolder A 
left join Common_Dict t1 on t1.Common_Dict_Id=A.GradeTerm
left join Common_Dict t2 on t2.Common_Dict_Id=A.Subject
left join Common_Dict t3 on t3.Common_Dict_Id=A.Resource_Version
left join Common_Dict t4 on t4.Common_Dict_Id=A.Resource_Type
left JOIN Bookshelves bk on bk.ResourceFolder_Id=A.ResourceFolder_Id  where 1=1 "
                    + strWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                }
                else
                {
                    if (!string.IsNullOrEmpty(Status) && Status != "-1")
                    {
                        if (Status == "1")
                        {
                            strWhere += " and UserBuyResources_Id<>'' ";
                        }
                        else
                        {
                            strWhere += " and UserBuyResources_Id is null ";
                        }
                    }
                    strSqlCount = @"select count(*) from ResourceFolder A left JOIN Bookshelves bk on bk.ResourceFolder_Id=A.ResourceFolder_Id
 left join UserBuyResources buy on buy.Book_id=A.ResourceFolder_Id and UserId='" + userid + @"' where 1=1 " + strWhere + " ";
                    strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.CreateTime DESC) row,A.*,buy.UserBuyResources_Id
,t1.D_Name as GradeTermName,t2.D_Name as SubjectName,t3.D_Name as Resource_VersionName,t4.D_Name as Resource_TypeName
from ResourceFolder A 
left join Common_Dict t1 on t1.Common_Dict_Id=A.GradeTerm
left join Common_Dict t2 on t2.Common_Dict_Id=A.Subject
left join Common_Dict t3 on t3.Common_Dict_Id=A.Resource_Version
left join Common_Dict t4 on t4.Common_Dict_Id=A.Resource_Type
left join UserBuyResources buy on buy.Book_id=A.ResourceFolder_Id and UserId='" + userid + @"'
left JOIN Bookshelves bk on bk.ResourceFolder_Id=A.ResourceFolder_Id  where 1=1 "
                        + strWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                }
                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    string docName = dtRes.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter();
                    docName = pfunction.GetDocFileName(docName);
                    string ispower = string.Empty;
                    if (dtRes.Rows[i]["UserBuyResources_Id"].ToString() == "1")
                    {
                        ispower = "";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(dtRes.Rows[i]["UserBuyResources_Id"].ToString()))
                        {
                            ispower = "<font color='green'>已授权</font>";
                        }
                        else
                        {
                            ispower = "<font color='red'>未授权</font>";
                        }
                    }
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        docId = dtRes.Rows[i]["ResourceFolder_Id"].ToString(),
                        GradeTerm = dtRes.Rows[i]["GradeTermName"].ToString(),
                        Subject = dtRes.Rows[i]["SubjectName"].ToString(),
                        Resource_Version = dtRes.Rows[i]["Resource_VersionName"].ToString(),
                        Resource_Type = dtRes.Rows[i]["Resource_TypeName"].ToString(),
                        docName = docName,
                        docTime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString()),
                        ispower = ispower,
                    });
                }
                #endregion

                if (inum > 0)
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
                    err = "error"//ex.Message.ToString()
                });
            }
        }
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="userlist"></param>
        /// <param name="relist"></param>
        /// <returns></returns>
        [WebMethod]
        public static string Empowers(string userlist, string relist)
        {
            try
            {
                BLL_UserBuyResources bll = new BLL_UserBuyResources();
                List<Model_UserBuyResources> listModel = new List<Model_UserBuyResources>();
                userlist = userlist.TrimEnd(',');
                relist = relist.TrimEnd(',');
                string _dSql = "delete UserBuyResources where BuyType = 'NBSQ' and UserId in ('" + userlist.Replace(",", "','") + "') and book_id in ('" + relist.Replace(",", "','") + "');";
                foreach (string item in userlist.Split(','))
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        foreach (string itemR in relist.Split(','))
                        {
                            if (!string.IsNullOrEmpty(itemR))
                            {
                                if (new BLL_UserBuyResources().GetRecordCount("UserId='" + item + "' and Book_id='" + itemR + "'") > 0)
                                {

                                }
                                else
                                {
                                    Model_UserBuyResources model = new Model_UserBuyResources();
                                    model.UserBuyResources_ID = Guid.NewGuid().ToString();
                                    model.UserId = item;
                                    model.Book_id = itemR;
                                    model.BuyType = "NBSQ";//内部授权
                                    model.CreateTime = DateTime.Now;
                                    listModel.Add(model);
                                }
                            }
                        }
                    }
                }
                if (listModel.Count > 0)
                {
                    //Rc.Common.DBUtility.DbHelperSQL.Query(_dSql);
                    if (bll.AddUserBuyResources(listModel))
                    {
                        return "1";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "1";
                }
            }
            catch (Exception ex)
            {
                return "2";
            }
        }
        /// <summary>
        /// 删除授权
        /// </summary>
        /// <param name="userlist"></param>
        /// <param name="relist"></param>
        /// <returns></returns>
        [WebMethod]
        public static string DeleteEmpowers(string userlist, string relist)
        {
            try
            {
                string _dSql = "delete UserBuyResources where BuyType = 'NBSQ' and UserId in ('" + userlist.Replace(",", "','") + "') and book_id in ('" + relist.Replace(",", "','") + "');";
                if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(_dSql) > 0)
                {
                    return "1";
                }
                else
                {
                    return "2";
                }
            }
            catch (Exception ex)
            {

                return "";
            }
        }
    }
}