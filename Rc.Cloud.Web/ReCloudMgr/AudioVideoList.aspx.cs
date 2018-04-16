﻿using Rc.Cloud.Model;
using Rc.Cloud.Web.Common;
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
using Rc.Cloud.BLL;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using Rc.Common.Config;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class AudioVideoList : Rc.Cloud.Web.Common.InitPage
    {
        protected string strResource_Type = string.Empty;//资源类型（class类型文件、testPaper类型文件、ScienceWord类型文件）
        protected string strResource_Class = string.Empty;//资源类别（云资源、自有资源）
        protected string strGradeTerm = string.Empty;//年级学期
        protected string strSubject = string.Empty;//学科
        protected string strResource_Version = string.Empty;//教材版本
        public string _t = string.Empty;
        protected string s = string.Empty;
        StringBuilder strHtml = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10400100";
            if (!IsPostBack)
            {
                BLL_SysUserTask bll_sysusertask = new BLL_SysUserTask();
                DataTable dt = new DataTable();
                string strWhere = string.Empty;

                //if (loginUser.SysUser_ID == Consts.AdminID || loginUser.SysUser_ID == Consts.CAdminID)
                //{

                //教材版本
                strWhere = " D_Type='3' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, dt, "D_Name", "Common_Dict_ID", "--教材版本--");
                //入学年份
                int years = DateTime.Now.Year;
                Rc.Cloud.Web.Common.pfunction.SetDdlStartSchoolYear(ddlYear, years - 5, years + 1, true, "入学年份");
                //年级学期
                strWhere = " D_Type='6' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeTerm, dt, "D_Name", "Common_Dict_ID", "--年级学期--");
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID", "--学科--");
                //}
                //else
                //{
                //    ddlYear.Items.Clear();
                //    DataTable dtTask = bll_sysusertask.GetList("SysUser_ID='" + loginUser.SysUser_ID + "' and TaskType='NF' order by TaskId ").Tables[0];
                //    ListItem item = null;
                //    item = new ListItem("--请选择--", "-1");//请选择
                //    ddlYear.Items.Add(item);
                //    for (int i = 0; i < dtTask.Rows.Count; i++)
                //    {
                //        ddlYear.Items.Add(new ListItem(dtTask.Rows[i]["TaskId"].ToString(), dtTask.Rows[i]["TaskId"].ToString()));
                //    }
                //    Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeTerm, bll_sysusertask.GetList("SysUser_ID='" + loginUser.SysUser_ID + "' and TaskType='NJXQ' order by TaskName").Tables[0], "TaskName", "TaskId", "--请选择--");
                //    Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, bll_sysusertask.GetList("SysUser_ID='" + loginUser.SysUser_ID + "' and TaskType='XK' order by TaskName").Tables[0], "TaskName", "TaskId", "--请选择--");
                //    Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, bll_sysusertask.GetList("SysUser_ID='" + loginUser.SysUser_ID + "' and TaskType='JCBB' order by TaskName").Tables[0], "TaskName", "TaskId", "--请选择--");
                //}
            }
        }
        /// <summary>
        /// 获取书本目录
        /// </summary>
        /// <param name="DocName"></param>
        /// <param name="strResource_Type"></param>
        /// <param name="strResource_Class"></param>
        /// <param name="strGradeTerm"></param>
        /// <param name="strSubject"></param>
        /// <param name="strResource_Version"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetAudioVideoBook(string BookName, string FileName, string Year, string strGradeTerm, string strSubject, string strResource_Version, int PageIndex, int PageSize)
        {
            try
            {
                BookName = BookName.Filter();
                FileName = FileName.Filter();
                Year = Year.Filter();
                strGradeTerm = strGradeTerm.Filter();
                strSubject = strSubject.Filter();
                strResource_Version = strResource_Version.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                #region 图书信息
                DataTable dtBook = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string StrWhere = " where 1=1 ";
                if (!string.IsNullOrEmpty(BookName))
                {
                    StrWhere += " and BookName like '%" + BookName.TrimEnd() + "'%";
                }
                if (!string.IsNullOrEmpty(FileName))
                {
                    StrWhere += " and AudioVideoBookId in(select AudioVideoBookId from AudioVideoIntro where FileName like '%" + FileName + "%') ";
                }
                if (!string.IsNullOrEmpty(Year) && Year != "-1")
                {
                    StrWhere += " and ParticularYear='" + Year + "'";
                }
                if (!string.IsNullOrEmpty(strGradeTerm) && strGradeTerm != "-1")
                {
                    StrWhere += " and GradeTerm='" + strGradeTerm + "'";
                }
                if (!string.IsNullOrEmpty(strSubject) && strSubject != "-1")
                {
                    StrWhere += " and Subject='" + strSubject + "'";
                }
                if (!string.IsNullOrEmpty(strResource_Version) && strResource_Version != "-1")
                {
                    StrWhere += " and Resource_Version='" + strResource_Version + "'";
                }
                //if (loginUser.SysUser_ID != Consts.AdminID && loginUser.SysUser_ID != Consts.CAdminID)
                //{
                //    //strWhere += " and CreateUser='" + loginUser.SysUser_ID + "' ";
                //    StrWhere += " and ParticularYear in(select TaskId from SysUserTask where TaskType='NF' and SysUser_Id='" + loginUser.SysUser_ID + "') ";
                //    StrWhere += " and Resource_Version in(select TaskId from SysUserTask where TaskType='JCBB' and SysUser_Id='" + loginUser.SysUser_ID + "') ";
                //    StrWhere += " and Subject in(select TaskId from SysUserTask where TaskType='XK' and SysUser_Id='" + loginUser.SysUser_ID + "') ";
                //    StrWhere += " and GradeTerm in(select TaskId from SysUserTask where TaskType='NJXQ' and SysUser_Id='" + loginUser.SysUser_ID + "') ";
                //}
                strSqlCount = @"select count(*) from AudioVideoBook" + StrWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.CreateTime DESC) row,A.* ,cdG.D_Name GradeTermName,cdr.D_Name Resource_VersionName,cdS.D_Name SubjectName from dbo.AudioVideoBook A
left join dbo.Common_Dict cdG on cdg.Common_Dict_ID=A.GradeTerm
left join dbo.Common_Dict cdR on cdr.Common_Dict_ID=A.Resource_Version
left join dbo.Common_Dict cdS on cds.Common_Dict_ID=A.Subject"
                    + StrWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtBook = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtBook.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        BookName = dtBook.Rows[i]["BookName"].ToString(),
                        CreateTime = pfunction.ConvertToLongDateTime(dtBook.Rows[i]["CreateTime"].ToString()),
                        ParticularYear = dtBook.Rows[i]["ParticularYear"].ToString(),
                        GradeTermName = dtBook.Rows[i]["GradeTermName"].ToString(),
                        Resource_VersionName = dtBook.Rows[i]["Resource_VersionName"].ToString(),
                        SubjectName = dtBook.Rows[i]["SubjectName"].ToString(),
                        AudioVideoBookId = dtBook.Rows[i]["AudioVideoBookId"].ToString(),
                        BookNameUrl = HttpContext.Current.Server.UrlEncode(dtBook.Rows[i]["BookName"].ToString())
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

        [WebMethod]
        public static string DeleteData(string AudioVideoBookId)
        {
            try
            {
                if (!string.IsNullOrEmpty(AudioVideoBookId.Filter()))
                {
                    BLL_AudioVideoBook bll = new BLL_AudioVideoBook();
                    if (bll.Delete(AudioVideoBookId))
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